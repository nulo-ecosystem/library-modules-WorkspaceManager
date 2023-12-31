﻿namespace Nulo.Modules.WorkspaceManager.Docking {

    public class DockContentCollection : ReadOnlyCollection<IDockContent> {
        private static readonly List<IDockContent> _emptyList = new(0);

        internal DockContentCollection() : base(new List<IDockContent>()) {
        }

        internal DockContentCollection(DockPane pane) : base(_emptyList) {
            m_dockPane = pane;
        }

        private readonly DockPane m_dockPane;

        private DockPane DockPane {
            get { return m_dockPane; }
        }

        public new IDockContent this[int index] {
            get {
                return DockPane == null ? Items[index] as IDockContent : GetVisibleContent(index);
            }
        }

        internal int Add(IDockContent content) {
#if DEBUG
            if(DockPane != null) { throw new InvalidOperationException(); }
#endif
            if(Contains(content)) { return IndexOf(content); }
            Items.Add(content);
            return Count - 1;
        }

        internal void AddAt(IDockContent content, int index) {
#if DEBUG
            if(DockPane != null) { throw new InvalidOperationException(); }
#endif
            if(index < 0 || index > Items.Count - 1) { return; }
            if(Contains(content)) { return; }
            Items.Insert(index, content);
        }

        public new bool Contains(IDockContent content) {
            return DockPane == null ? Items.Contains(content) : GetIndexOfVisibleContents(content) != -1;
        }

        public new int Count {
            get {
                return DockPane == null ? base.Count : CountOfVisibleContents;
            }
        }

        public new int IndexOf(IDockContent content) {
            return DockPane == null ? !Contains(content) ? -1 : Items.IndexOf(content) : GetIndexOfVisibleContents(content);
        }

        internal void Remove(IDockContent content) {
            if(DockPane != null) { throw new InvalidOperationException(); }
            if(!Contains(content)) { return; }
            Items.Remove(content);
        }

        private int CountOfVisibleContents {
            get {
#if DEBUG
                if(DockPane == null) { throw new InvalidOperationException(); }
#endif
                int count = 0;
                foreach(IDockContent content in DockPane.Contents) {
                    if(content.DockHandler.DockState == DockPane.DockState) { count++; }
                }
                return count;
            }
        }

        private IDockContent GetVisibleContent(int index) {
#if DEBUG
            if(DockPane == null) { throw new InvalidOperationException(); }
#endif
            int currentIndex = -1;
            foreach(IDockContent content in DockPane.Contents) {
                if(content.DockHandler.DockState == DockPane.DockState) { currentIndex++; }
                if(currentIndex == index) { return content; }
            }
            throw new ArgumentOutOfRangeException();
        }

        private int GetIndexOfVisibleContents(IDockContent content) {
#if DEBUG
            if(DockPane == null) { throw new InvalidOperationException(); }
#endif
            if(content == null) { return -1; }
            int index = -1;
            foreach(IDockContent c in DockPane.Contents) {
                if(c.DockHandler.DockState == DockPane.DockState) {
                    index++;
                    if(c == content) { return index; }
                }
            }
            return -1;
        }
    }
}