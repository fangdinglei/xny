using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Reflection;
namespace FdlWindows.View
{
    public partial class FMain : Form, IViewHolder
    {
        IServiceProvider serviceProvider;
        /// <summary>
        /// 界面缓存最大值
        /// </summary>
        const int MaxSameViewInstance = 3;
        int __uid;

        /// <summary>
        ///用于创建实例 key name value classpath
        /// </summary>
        Dictionary<string, Type> ViewClassType => _ViewData.ViewClassTypes;
        /// <summary>
        ///备用 key name value menupath
        /// </summary>
        Dictionary<string, string> ViewMeunPaths => _ViewData.ViewMeunPaths;


        /// <summary>
        /// 用于名称找节点 key name value treenode
        /// </summary>
        Dictionary<string, TreeNode> ViewNodes = new();
        /// <summary>
        /// key name value view 界面缓存
        /// </summary>
        Dictionary<string, Queue<IView>> Views = new Dictionary<string, Queue<IView>>();
        /// <summary>
        ///用于创建实例获取名称以缓存 key IView value NameofViewInstance
        /// </summary>
        Dictionary<IView, string> NameofViewInstance = new Dictionary<IView, string>();
        Stack<IView> Windows = new Stack<IView>();

        FMainViews _ViewData;
        public int Uid => __uid;
        Action? _closecall;
        public Control Holder => this;
        public FMain(FMainOption op, IServiceProvider serviceProvider, FMainViews viewData)
        {
            this.serviceProvider = serviceProvider;
            Text = op.Title;
            InitializeComponent();
            _ViewData = viewData;
            this.ClientSize = new Size(1200, 650);
            _closecall = op.CloseCall;

            InitViews();
            treeview_views.Nodes[0].ExpandAll();
            //SwitchTo("关于",true);
            //if (DateTime.Now>new DateTime(2022,9,10))
            //{
            //    MessageBox.Show("许可过期");
            //    throw new Exception("禁止访问");
            //}
        }



        /// <summary>
        /// 注册所有View
        /// </summary>
        /// <exception cref="Exception"></exception>
        void InitViews()
        {
            foreach (var att in _ViewData.ViewAttributes.Values)
            {
                AddView(att.Name, att.Title, att.MenuPath, att.UserSelectAble);
            }

        }


        /// <summary>
        /// 打开指定页面
        /// </summary>
        /// <param name="name"></param>
        /// <param name="newwindow">是否放弃当前界面而打开新界面</param>
        /// <param name="par"></param>
        public bool SwitchTo(string name, bool newwindow, params object[] par)
        {
            lock (this)
            {
                if (newwindow)
                {
                    FormExitEventArg arg;
                    IView formold;
                    while (Windows.Count > 0)
                    {
                        formold = Windows.Pop();
                        arg = new FormExitEventArg() { IsForNewWindow = true };
                        formold.OnEvent("Exit", arg);
                        if (arg.Cancel)
                        {
                            Windows.Push(formold);
                            return false;
                        }
                        else
                        {
                            OnViewClose(formold);
                            if (Windows.Count > 0)
                            {
                                try
                                {
                                    Windows.Peek().OnEvent("UnCovered");
                                }
                                catch (Exception)
                                {

                                    throw;
                                }

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
                //显示界面
                var iuserview = GetOrCreatView(name);
                try
                {
                    if (Windows.Count > 0)
                    {
                        try
                        {
                            Windows.Peek().OnEvent("Covered");
                        }
                        catch (Exception)
                        {

                            throw;
                        }
                    }
                    Windows.Push(iuserview);
                    iuserview.View.Visible = true;
                    iuserview.View.BringToFront();
                    iuserview.SetViewHolder(this);
                    iuserview.PrePare(par);
                }
                catch (Exception ex)
                {
                    OnViewClose(iuserview);
                    MessageBox.Show(ex.Message, "界面初始化失败");
                }

                return true;
            }
        }

        #region 创建View模块 
        void OnViewClose(IView view)
        {
            //窗口关闭
            if (Windows.Count > 0)
            {
                try
                {
                    Windows.Peek().OnEvent("UnCovered");
                    Windows.Peek().View.Visible = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "界面UnCovered失败");
                    NameofViewInstance.Remove(view);
                    return;
                }
            }
            var iname = NameofViewInstance[view];
            if (highlighting != null && iname == highlighting.Tag)
            {
                highlighting.BackColor = Color.White;
                highlighting.ForeColor = Color.Black;
                highlighting = null;
            }


            if (IsLoading(view) || Views[iname].Count > MaxSameViewInstance)
            {
                NameofViewInstance.Remove(view);
                MainHolder.Controls.Remove(view.View);
            }
            else
            {
                view.View.Visible = false;
                Views[iname].Enqueue(view);
            }


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
            if (Views.ContainsKey(name) && Views[name].Count > 0)
            {
                var t = Views[name].Dequeue();
                return t;
            }

            IView? _interface = serviceProvider.GetService(ViewClassType[name]) as IView;
            if (_interface == null)
            {
                throw new Exception($"失败{name} @{ViewClassType[name].FullName}没有注册或者不是合适的界面");
            }
            var view = _interface.View;
            view.Dock = DockStyle.Fill;

            var type = view.GetType();
            PropertyInfo? p;
            if ((p = type.GetProperty("AutoScroll")) != null)
            {
                p.SetValue(view, true);
            }
            if ((p = type.GetProperty("TopLevel")) != null)
            {
                p.SetValue(view, false);
            }
            if ((p = type.GetProperty("ControlBox")) != null)
            {
                p.SetValue(view, false);
            }
            if ((p = type.GetProperty("FormBorderStyle")) != null)
            {
                p.SetValue(view, FormBorderStyle.None);
            }

            MainHolder.Controls.Add(view);

            if (!Views.ContainsKey(name))
            {
                Views.Add(name, new Queue<IView>());
            }
            NameofViewInstance.Add(_interface, name);
            return _interface;
        }
        TreeNode FindNodeInNodes(string name, TreeNodeCollection nodes)
        {
            foreach (TreeNode item in nodes)
            {
                if (item.Name == name)
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
        TreeNodeCollection AddMenu(string path)
        {
            var mpath = path.Split('/', StringSplitOptions.RemoveEmptyEntries);
            var root = treeview_views.Nodes;
            TreeNode find = null;
            foreach (var menu in mpath)
            {
                find = FindNodeInNodes(menu, root);
                if (find == null)
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
                    if ((string)find.Tag != "Menu")
                    {
                        throw new Exception("菜单目录" + path + $"的[{menu}]部分已经被界面占用");
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
        /// <param name="userselectable">用户是否可以直接选择此界面</param>
        void AddView(string Name, string title, string menupath, bool userselectable = true)
        {


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

        #endregion

        public void Back()
        {
            lock (this)
            {
                FormExitEventArg arg;
                if (Windows.Count >= 1)
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
                        OnViewClose(view);

                        if (Windows.Count > 0)
                        {
                            try
                            {
                                Windows.Peek().OnEvent("UnCovered");
                            }
                            catch (Exception)
                            {

                                throw;
                            }

                        }
                    }
                }
            }
        }
        /// <summary>
        /// TODO 异步环境下的一致性
        /// </summary>
        /// <param name="it"></param>
        /// <returns></returns>
        public bool Back(IView it)
        {
            lock (this)
            {


                if (IsTopView(it))
                {
                    Back();
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public bool IsTopView(IView it)
        {
            lock (this)
            {
                return Windows.Count > 0 && Windows.Peek() == it;
            }
        }
        public bool IsParentOfView(IView it)
        {
            lock (this)
            {
                return NameofViewInstance.ContainsKey(it);
            }
        }
        #region 事件
        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            _closecall?.Invoke();
        }
        TreeNode highlighting = null;


        private void iTreeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            try
            {
                TreeNode theNode = e.Node;
                if (highlighting == theNode)
                {//没有发生变化
                    return;
                }
                if ((string)theNode.Tag != "Menu")
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

        #region 加载中模块
        HashSet<IView> _ViewLoading = new();
        public void ShowLoading(IView view, Func<Task<bool>> load, Func<Task<bool>>? retry = null,
            Action okcall = null, Action exitcall = null)
        {
            SwitchTo("Loading", false, load, retry, okcall, exitcall, (bool isloading) =>
            {
                lock (_ViewLoading)
                {
                    if (isloading)
                    {
                        _ViewLoading.Add(view);
                    }
                    else
                    {
                        _ViewLoading.Remove(view);
                    }
                }
            });
        }
        public bool IsLoading(IView view)
        {
            lock (_ViewLoading)
            {
                return _ViewLoading.Contains(view);
            }
        }
        #endregion

        public void ShowDatePicker(Action<DateTime, DateTime> call)
        {
            SwitchTo("FDateSelector", false, call);
        }


    }
    public class FMainOption
    {
        public string? Title;
        public uint Uid;
        public Action? CloseCall;
    }
    /// <summary>
    /// 包含界面信息
    /// </summary>
    public class FMainViews
    {
        /// <summary>
        ///用于创建实例 key name value classpath
        /// </summary>
        public Dictionary<string, Type> ViewClassTypes = new();
        /// <summary>
        /// key name value menupath
        /// </summary>
        public Dictionary<string, string> ViewMeunPaths = new();
        public Dictionary<string, AutoDetectViewAttribute> ViewAttributes = new();

    }
    static public class ViewRegister
    {
        /// <summary>
        /// 反射注册所有View
        /// </summary>
        /// <exception cref="Exception"></exception>
        static public void UseFMain(this IServiceCollection serviceCollection, FMainOption fMainOption)
        {
            serviceCollection.AddSingleton<FMain>();
            serviceCollection.AddSingleton(fMainOption);
            FMainViews result = new FMainViews();
            foreach (var item in typeof(ViewRegister).Assembly.GetTypes())
            {
                var att = item.GetCustomAttribute<AutoDetectViewAttribute>();
                if (att == null)
                    continue;
                if (item.IsAssignableTo(typeof(IView)) == false)
                {
                    throw new Exception(att.GetType().Name + " 属性只能加在" + nameof(IView));
                }
                if (att.Name == "Menu")
                    throw new Exception("界面名称不能是Menu");
                if (result.ViewClassTypes.ContainsKey(att.Name))
                    return;
                result.ViewAttributes.Add(att.Name, att);
                result.ViewMeunPaths.Add(att.Name, att.MenuPath);
                result.ViewClassTypes.Add(att.Name, item);
                serviceCollection.TryAddTransient(item);
            }
            serviceCollection.TryAddSingleton<FMainViews>(result);
        }
    }
}
