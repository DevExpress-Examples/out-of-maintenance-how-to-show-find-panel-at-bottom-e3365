using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Controls;

namespace GridControlWithFindPanel
{
    public class MyGridView : GridView
    {
        public MyGridView()
            : this(null)
        {
        }

        public MyGridView(GridControl grid)
            : base(grid)
        {
            // put your initialization code here
        }

        protected override string ViewName
        {
            get
            {
                return "MyGridView";
            }
        }

        bool showFindPanelOnTop;
        public virtual bool ShowFindPanelOnTop
        {
            get
            {
                return showFindPanelOnTop;
            }
            set
            {
                if (value == showFindPanelOnTop) return;
                showFindPanelOnTop = value;
                HideFindPanel();
                ShowFindPanel();
            }
        }

        public int FindPanelHeight { get; private set; }

        protected override FindControl CreateFindPanel(object findPanelProperties)
        {
            FindControl findControl=new FindControl(this, findPanelProperties);
            FindPanelHeight = findControl.Height;
            return findControl;
        }
    }
}