using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using System.Drawing;
using DevExpress.XtraGrid.Views.Grid;

namespace GridControlWithFindPanel
{
    public class MyGridViewInfo : GridViewInfo
    {
        public MyGridViewInfo(GridView gridView)
            : base(gridView)
        {
        }

        public override void CalcRects(Rectangle bounds, bool partital)
        {          
            bool inTop = (this.View as MyGridView).ShowFindPanelOnTop;
            Rectangle r = Rectangle.Empty;
            Rectangle myBounds = bounds;
            if (!inTop)
            {
                myBounds.Height = bounds.Height - (this.View as MyGridView).FindPanelHeight;
            }
            ViewRects.Bounds = myBounds;
            ViewRects.Scroll = CalcScrollRect();
            ViewRects.Client = CalcClientRect();
            FilterPanel.Bounds = Rectangle.Empty;
            if (!partital)
            {
                CalcRectsConstants();
            }
            if (View.OptionsView.ShowIndicator)
            {
                ViewRects.IndicatorWidth = Math.Max(View.IndicatorWidth, ViewRects.MinIndicatorWidth);
            }
            int minTop = ViewRects.Client.Top;
            int maxBottom = ViewRects.Client.Bottom;
            if (View.OptionsView.ShowViewCaption)
            {
                r = ViewRects.Client;
                r.Y = minTop;
                r.Height = CalcViewCaptionHeight(ViewRects.Client);
                ViewRects.ViewCaption = r;
                minTop = ViewRects.ViewCaption.Bottom;
            }
            if (inTop)
                minTop = UpdateFindControlVisibility(new Rectangle(ViewRects.Client.X, minTop, ViewRects.Client.Width, maxBottom - minTop), false).Y;
            else
                UpdateFindControlVisibility(new Rectangle(ViewRects.Client.X, ViewRects.Client.Bottom, ViewRects.Client.Width, maxBottom - minTop), false);

            if (View.OptionsView.ShowGroupPanel)
            {
                r = ViewRects.Client;
                r.Y = minTop;
                r.Height = CalcGroupPanelHeight();
                ViewRects.GroupPanel = r;
                minTop = ViewRects.GroupPanel.Bottom;
            }
            minTop = CalcRectsColumnPanel(minTop);
            if (View.IsShowFilterPanel)
            {
                r = ViewRects.Client;
                int fPanel = GetFilterPanelHeight();
                r.Y = maxBottom - fPanel;
                r.Height = fPanel;
                FilterPanel.Bounds = r;
                maxBottom = r.Top;
            }
            if (View.OptionsView.ShowFooter)
            {
                r = ViewRects.Client;
                r.Height = GetFooterPanelHeight();
                r.Y = maxBottom - r.Height;
                ViewRects.Footer = r;
                maxBottom = r.Top;
            }
            r = ViewRects.Client;
            r.Y = minTop;
            r.Height = maxBottom - minTop;
            ViewRects.Rows = r;
        }
    }
}
