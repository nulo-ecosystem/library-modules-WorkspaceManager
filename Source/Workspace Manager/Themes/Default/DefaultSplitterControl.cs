﻿using Nulo.Modules.WorkspaceManager.Docking;

namespace Nulo.Modules.WorkspaceManager.Themes.Default {

    [ToolboxItem(false)]
    internal class DefaultSplitterControl(DockPane pane) : DockPane.SplitterControlBase(pane) {
        private readonly SolidBrush _horizontalBrush = pane.DockPanel.Theme.PaintingService.GetBrush(pane.DockPanel.Theme.ColorPalette.MainWindowActive.Background);
        private int SplitterSize { get; } = pane.DockPanel.Theme.Measures.SplitterSize;

        protected override void OnPaint(PaintEventArgs e) {
            base.OnPaint(e);

            Rectangle rect = ClientRectangle;
            if(rect.Width <= 0 || rect.Height <= 0) { return; }

            switch(Alignment) {
                case DockAlignment.Right:
                case DockAlignment.Left: {
                    Debug.Assert(SplitterSize == rect.Width);
                    e.Graphics.FillRectangle(_horizontalBrush, rect);
                }
                break;

                case DockAlignment.Bottom:
                case DockAlignment.Top: {
                    Debug.Assert(SplitterSize == rect.Height);
                    e.Graphics.FillRectangle(_horizontalBrush, rect);
                }
                break;
            }
        }
    }
}