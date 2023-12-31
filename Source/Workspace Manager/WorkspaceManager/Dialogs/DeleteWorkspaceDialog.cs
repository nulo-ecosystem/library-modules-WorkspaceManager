﻿namespace Nulo.Modules.WorkspaceManager {

    internal partial class DeleteWorkspaceDialog : Form {
        public int IndexWorkspace { get; private set; }

        public DeleteWorkspaceDialog(List<string> layouts, Texts texts) {
            InitializeComponent();
            foreach(var layout in layouts) { UserWorkspaceOptions.Items.Add(layout); }

            Text = texts.DeleteWorkspaceTitle;
            LabelSelected.Text = $"{texts.DeleteWorkspaceSelect}:";
            DeleteButton.Text = texts.CommandDelete;
        }

        private void RemoveButton_Click(object sender, System.EventArgs e) {
            if(UserWorkspaceOptions.SelectedIndex == -1) { return; }

            IndexWorkspace = UserWorkspaceOptions.SelectedIndex;
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}