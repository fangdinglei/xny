using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FdlWindows.View
{
    public partial class PageController : UserControl
    {
        public PageController()
        {
            InitializeComponent();
            OnPageChanged += () =>
            {
                button1.Enabled = Page != 1;
                button3.Enabled = Page != TotalPage;
                label1.Text = $"{Page}/{TotalPage}";
            };
            OnRecordCountChanged += () =>
            {
                label1.Text = $"{Page}/{TotalPage}";
            };
            OnPageSizeChanged += () => {
                label1.Text = $"{Page}/{TotalPage}";
            };
        }

        int _page=1;
        int _pageSize=10;
        int _recordcount=0;

        /// <summary>
        /// 1-based
        /// </summary>
        public int Page {
            get { return _page; }
            set {
                if (value<1)
                {
                    value = 1;
                }
                if (value>TotalPage)
                {
                    value = TotalPage;
                }
                if (_page!=value)
                {
                    _page = value;
                    OnPageChanged();
                }
            }
        }
        public int TotalPage { 
            get=>Math.Max(1, (RecordCount + PageSize - 1) / PageSize);
        }
        public int PageSize { 
            get => _pageSize;
            set
            {
                if (value < 1)
                {
                    value=1;
                }
                if (_pageSize!=value)
                {
                    _pageSize = value;
                    OnRecordCountChanged();
                }
            }
        }
        public int RecordCount {
            get => _recordcount; 
            set {
                if (value < 0)
                {
                    throw new Exception(nameof(RecordCount)+"不能小于0");
                }
                if (_recordcount!=value)
                {
                    _recordcount = value;
                    OnRecordCountChanged();
                }
             
            }
        }

        public event Action OnPageSizeChanged;
        public event Action OnPageChanged;
        public event Action OnRecordCountChanged;

        private void button1_Click(object sender, EventArgs e)
        {
            if (Page>1)
            {
                Page--;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (Page <TotalPage)
            {
                Page++;
            }
        }
    }
}
