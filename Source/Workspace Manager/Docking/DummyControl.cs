﻿namespace Nulo.Modules.WorkspaceManager.Docking {

    [ToolboxItem(false)]
    internal sealed class DummyControl : Control {

        public DummyControl() {
            SetStyle(ControlStyles.Selectable, false);
            ResetBackColor();
        }

        public override void ResetBackColor() {
            BackColor = SystemColors.ControlLight;
        }
    }
}