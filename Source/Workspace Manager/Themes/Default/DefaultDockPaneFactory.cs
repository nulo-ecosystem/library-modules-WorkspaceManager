﻿using Nulo.Modules.WorkspaceManager.Docking;

namespace Nulo.Modules.WorkspaceManager.Themes.Default {

    public class DefaultDockPaneFactory : DockPanelExtender.IDockPaneFactory {

        public DockPane CreateDockPane(IDockContent content, DockState visibleState, bool show) {
            return new DefaultDockPane(content, visibleState, show);
        }

        public DockPane CreateDockPane(IDockContent content, FloatWindow floatWindow, bool show) {
            return new DefaultDockPane(content, floatWindow, show);
        }

        public DockPane CreateDockPane(IDockContent content, DockPane previousPane, DockAlignment alignment, double proportion, bool show) {
            return new DefaultDockPane(content, previousPane, alignment, proportion, show);
        }

        public DockPane CreateDockPane(IDockContent content, Rectangle floatWindowBounds, bool show) {
            return new DefaultDockPane(content, floatWindowBounds, show);
        }
    }
}