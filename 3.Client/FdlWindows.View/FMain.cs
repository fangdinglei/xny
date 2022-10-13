
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using System.Linq;

namespace FdlWindows.View
{
    public partial class FMain : Form,IViewHolder
    {
        /// <summary>
        /// 界面缓存最大值
        /// </summary>
        const int MaxSameViewInstance=3;
        int __uid;
        /// <summary>
        /// key name value view
        /// </summary>
        Dictionary<string,Queue<IView>> Views = new  Dictionary<string, Queue<IView>>();
        /// <summary>
        /// key IView value NameofViewInstance
        /// </summary>
        Dictionary<IView, string> NameofViewInstance =  new Dictionary<IView, string>();
        /// <summary>
        /// key name value classpath
        /// </summary>
        Dictionary<string, string> ViewClassPaths = new Dictionary<string, string>();
        /// <summary>
        /// key name value menupath
        /// </summary>
        Dictionary<string, string> ViewMeunPaths = new Dictionary<string, string>();
        /// <summary>
        /// key name value treenode
        /// </summary>
        Dictionary<string,TreeNode> ViewNodes = new Dictionary<string,TreeNode>();

        Stack<IView> Windows = new Stack<IView>();

        public Control Holder => this;

        public int Uid => __uid;
        Action? _closecall;
        public FMain(int uid,string title,Action? closecall)
        { 
            Text=title;
            InitializeComponent(); 
            InitViews(); 
            this.ClientSize = new  Size(1200, 650); 
            _closecall = closecall;
            //SwitchTo("关于",true);
            //if (DateTime.Now>new DateTime(2022,9,10))
            //{
            //    MessageBox.Show("许可过期");
            //    throw new Exception("禁止访问");
            //}
        }
      
        void InitViews()
        {
            foreach (var item in this.GetType().Assembly.GetTypes())
            {
                var att = item.GetCustomAttribute<AutoDetectViewAttribute>();
                if (att == null)
                    continue;
                AddView(att.Name,att.Title,att.MenuPath,item.FullName,att.UserSelectAble);
            } 
            treeview_views.Nodes[0].ExpandAll(); 
        }

        /// <summary>
        /// 获取一个界面或者创建一个界面
        /// <br/>
        /// 没有则抛出异常
        /// </summary>
        /// <param name="name"></param>
        /// <exception cref="Exception"/>
        /// <returns></returns>
        IView GetOrCreatView(string name)
        {
            if (Views.ContainsKey(name)&& Views[name].Count>0)
                return Views[name].Dequeue();
            Assembly assembly = GetType().Assembly;
            Form form = assembly.CreateInstance(ViewClassPaths[name]) as Form;
            form.Dock = DockStyle.Fill;
            form.AutoScroll = true;
            form.TopLevel = false;
            form.ControlBox = false;
            form.FormBorderStyle = FormBorderStyle.None;
            MainHolder.Controls.Add(form); 
            if ((form as IView) ==null)
            {
                throw new Exception($"反射失败{name} @{ViewClassPaths[name]} 不是合适的界面");
            }
            if (!Views.ContainsKey(name))
            {
                Views.Add(name,new Queue<IView>());
            }
            NameofViewInstance.Add(form as IView, name);
            return form as IView;
        } 
        /// <summary>
        /// 打开指定页面
        /// </summary>
        /// <param name="name"></param>
        /// <param name="newwindow">是否放弃当前界面而打开新界面</param>
        /// <param name="par"></param>
        public bool SwitchTo(string name, bool newwindow, params object[] par)
        { 
            if (newwindow)
            {
               
                FormExitEventArg arg;
                IView formold;
                while (Windows.Count > 0)
                {
                    formold = Windows.Pop();
                    arg = new FormExitEventArg();
                    formold.OnEvent("Exit", arg);
                    if (arg.Cancel)
                    {
                        Windows.Push(formold);
                        return false;
                    }
                    else
                    {
                        var iname= NameofViewInstance[formold];
                        formold.View.Visible = false;
                        if (Views[iname].Count>MaxSameViewInstance)
                        {
                            NameofViewInstance.Remove(formold);
                        }
                        else
                        {
                            Views[iname].Enqueue(formold);
                        }
                       
                    } 
                }  
                if (ViewNodes.ContainsKey(name))
                {//draw 高亮选中的节点
                    if (highlighting != null)
                    {
                        highlighting.BackColor = Color.White;
                        highlighting.ForeColor = Color.Black;
                    }
                    highlighting = ViewNodes[name];
                    treeview_views.SelectedNode = highlighting;
                    highlighting.BackColor = Color.Blue;
                    highlighting.ForeColor = Color.White;
                }
                else
                {
                    throw new Exception("该界面只能附加在其他界面上");
                }  
            } 
            if (Windows.Count > 0)
            {
                Windows.Peek().OnEvent("Covered");
                Windows.Peek().View.Visible = false;
            }
            //显示界面
            var iuserview = GetOrCreatView(name);  
            iuserview.SetViewHolder(this);
            iuserview.PrePare(par);
            Windows.Push(iuserview);
            iuserview.View.Visible = true;
            return true;
        }

        TreeNode FindNodeInNodes(string name,TreeNodeCollection nodes) {
            foreach (TreeNode item in nodes)
            {
                if (item.Name==name)
                {
                    return item;
                }
            }
            return null;
        }
        /// <summary>
        /// 添加一个菜单
        /// </summary>
        /// <param name="path"></param>
        public TreeNodeCollection AddMenu(string  path) {
            var mpath = path.Split('/', StringSplitOptions.RemoveEmptyEntries);
            var root = treeview_views.Nodes;
            TreeNode find=null;
            foreach (var menu in mpath)
            {
                find = FindNodeInNodes(menu,root);
                if (find==null)
                {
                    TreeNode newNode = new TreeNode();
                    newNode.Name = menu;
                    newNode.Text = menu;//设置节点名称
                    newNode.Tag = "Menu";//保存接口路径
                    root.Add(newNode);
                    find = newNode;
                }
                else
                {
                    if ((string)find.Tag!= "Menu")
                    {
                        throw new Exception("菜单目录"+ path+$"的[{menu}]部分已经被界面占用");
                    }
                }
                root = find.Nodes;
            }
            return root;
        }
        /// <summary>
        /// /添加一个界面
        /// </summary>
        /// <param name="Name">名称，必须是唯一的</param>
        /// <param name="title">显示的名称 可以重复</param>
        /// <param name="classpath">界面的全路径</param>
        /// <param name="userselectable">用户是否可以直接选择此界面</param>
        public void AddView(string Name,string title,string menupath, string classpath, bool userselectable = true)
        {
            if (Name=="Menu")
                throw new Exception("界面名称不能是Menu");
            if (ViewClassPaths.ContainsKey(Name))
                return;
            ViewMeunPaths.Add(Name,menupath);
            ViewClassPaths.Add(Name, classpath);
          
            if (userselectable)
            {
                TreeNodeCollection root = AddMenu(menupath);
                TreeNode newNode = new TreeNode();
                newNode.Name = Name;
                newNode.Text = title;//设置节点名称
                newNode.Tag = Name;//保存接口路径
                //添加到根节点下
                root.Add(newNode);
                ViewNodes.Add(Name, newNode);
            } 
        }
        public void Back() {
            FormExitEventArg arg;
            if (Windows.Count > 1)
            {
                IView view = Windows.Pop();
                arg = new FormExitEventArg();
                view.OnEvent("Exit", arg);
                if (arg.Cancel)
                {
                    Windows.Push(view);
                    return;
                }
                else
                {
                    view.View.Visible = false;
                    //窗口关闭
                    if (Windows.Count > 0)
                    {
                        Windows.Peek().OnEvent("UnCovered");
                        Windows.Peek().View.Visible = true;
                    }
                    var iname = NameofViewInstance[view]; 
                    if (Views[iname].Count > MaxSameViewInstance)
                    {
                        NameofViewInstance.Remove(view);
                    }
                    else
                    {
                        Views[iname].Enqueue(view);
                    }
                }
            }
        }
        public bool IsTopView(IView it)
        {
            return Windows.Count>0&&Windows.Peek() ==it;
        }

        #region 事件
        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            _closecall();
        }
        TreeNode highlighting = null;


        private void iTreeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            try
            {
                TreeNode theNode = e.Node;
                if (highlighting==theNode)
                {//没有发生变化
                    return;
                }
                if ((string)theNode.Tag!="Menu")
                { 
                    SwitchTo(e.Node.Tag as string, true);
                } 
            }
            catch (Exception ex)
            {
                MessageBox.Show("加载页面失败！" + ex);
            }
        }

        /// <summary>
        /// 点击返回按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Back_Click(object sender, EventArgs e)
        {
            Back(); 
        }
        
        #region 窗口大小改变
        private void FMain_ResizeEnd(object sender, EventArgs e)
        {
            //可优化的
            foreach (var item in Windows.ToArray())
            {
                var iface = item as IView;
                if (iface != null)
                    iface.OnEvent("SizeChanged");
            }
        }

        private void FMain_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Maximized)
            {
                FMain_ResizeEnd(sender, e);
            }
            else if (WindowState == FormWindowState.Minimized)
            {
                FMain_ResizeEnd(sender, e);
            }
        }


        #endregion

        #endregion

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (Windows.Count == 0)
                return;
            Windows.Peek().OnTick();
        }
         
    }

}
