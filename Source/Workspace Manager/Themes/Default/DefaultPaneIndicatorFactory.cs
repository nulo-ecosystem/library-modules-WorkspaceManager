﻿using Nulo.Modules.WorkspaceManager.Docking;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Nulo.Modules.WorkspaceManager.Themes.Default {

    internal class DefaultPaneIndicatorFactory : DockPanelExtender.IPaneIndicatorFactory {

        public DockPanel.IPaneIndicator CreatePaneIndicator(ThemeBase theme) {
            return new DefaultPaneIndicator(theme);
        }

        [ToolboxItem(false)]
        private class DefaultPaneIndicator : PictureBox, DockPanel.IPaneIndicator {
            private readonly Bitmap _bitmapPaneDiamond;
            private readonly Bitmap _bitmapPaneDiamondLeft;
            private readonly Bitmap _bitmapPaneDiamondRight;
            private readonly Bitmap _bitmapPaneDiamondTop;
            private readonly Bitmap _bitmapPaneDiamondBottom;
            private readonly Bitmap _bitmapPaneDiamondFill;
            private readonly Bitmap _bitmapPaneDiamondHotSpot;
            private readonly Bitmap _bitmapPaneDiamondHotSpotIndex;

            private static readonly DockPanel.HotSpotIndex[] _hotSpots = [
                new DockPanel.HotSpotIndex(1, 0, DockStyle.Top),
                new DockPanel.HotSpotIndex(0, 1, DockStyle.Left),
                new DockPanel.HotSpotIndex(1, 1, DockStyle.Fill),
                new DockPanel.HotSpotIndex(2, 1, DockStyle.Right),
                new DockPanel.HotSpotIndex(1, 2, DockStyle.Bottom)
            ];

            private GraphicsPath _displayingGraphicsPath;

            public DefaultPaneIndicator(ThemeBase theme) {
                _bitmapPaneDiamond = theme.ImageService.Dockindicator_PaneDiamond;
                _bitmapPaneDiamondLeft = theme.ImageService.Dockindicator_PaneDiamond_Fill;
                _bitmapPaneDiamondRight = theme.ImageService.Dockindicator_PaneDiamond_Fill;
                _bitmapPaneDiamondTop = theme.ImageService.Dockindicator_PaneDiamond_Fill;
                _bitmapPaneDiamondBottom = theme.ImageService.Dockindicator_PaneDiamond_Fill;
                _bitmapPaneDiamondFill = theme.ImageService.Dockindicator_PaneDiamond_Fill;
                _bitmapPaneDiamondHotSpot = theme.ImageService.Dockindicator_PaneDiamond_Hotspot;
                _bitmapPaneDiamondHotSpotIndex = theme.ImageService.DockIndicator_PaneDiamond_HotspotIndex;
                _displayingGraphicsPath = DrawHelper.CalculateGraphicsPathFromBitmap(_bitmapPaneDiamond);

                SizeMode = PictureBoxSizeMode.AutoSize;
                Image = _bitmapPaneDiamond;
                Region = new Region(DisplayingGraphicsPath);
            }

            public GraphicsPath DisplayingGraphicsPath {
                get { return _displayingGraphicsPath; }
            }

            public DockStyle HitTest(Point pt) {
                if(!Visible) { return DockStyle.None; }

                pt = PointToClient(pt);
                if(!ClientRectangle.Contains(pt)) { return DockStyle.None; }

                for(int i = _hotSpots.GetLowerBound(0); i <= _hotSpots.GetUpperBound(0); i++) {
                    if(_bitmapPaneDiamondHotSpot.GetPixel(pt.X, pt.Y) == _bitmapPaneDiamondHotSpotIndex.GetPixel(_hotSpots[i].X, _hotSpots[i].Y)) {
                        return _hotSpots[i].DockStyle;
                    }
                }

                return DockStyle.None;
            }

            private DockStyle m_status = DockStyle.None;

            public DockStyle Status {
                get { return m_status; }
                set {
                    m_status = value;
                    if(m_status == DockStyle.None) {
                        Image = _bitmapPaneDiamond;
                    } else if(m_status == DockStyle.Left) {
                        Image = _bitmapPaneDiamondLeft;
                    } else if(m_status == DockStyle.Right) {
                        Image = _bitmapPaneDiamondRight;
                    } else if(m_status == DockStyle.Top) {
                        Image = _bitmapPaneDiamondTop;
                    } else if(m_status == DockStyle.Bottom) {
                        Image = _bitmapPaneDiamondBottom;
                    } else if(m_status == DockStyle.Fill) {
                        Image = _bitmapPaneDiamondFill;
                    }
                }
            }
        }
    }
}