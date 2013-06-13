using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MutoMark.Model
{
    public partial class ResultWindow : Form, IResultViewDataSource, IObserver<Document>, IEditorToolStripDataSource, IEditorToolStripDelegate
    {
        private WatchDog _watchDog;
        private IDisposable _watchDogListener;
        private Document _renderedDocument;

        private ToolStripItem[] _editorItems;

        public ResultWindow(string markdownFile)
        {
            InitializeComponent();

            this.resultView.DataSource = this;

            this.editorToolStrip.ImageList = this.toolStripImages;

            this.DefineEditorItems();
            this.editorToolStrip.DataSource = this;
            this.editorToolStrip.Delegate = this;
            this.editorToolStrip.Reload();

            this._watchDog = new WatchDog(markdownFile, new GitHubProcessor());
            this._watchDogListener = this._watchDog.Subscribe(this);
            this._watchDog.Notify();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            this._watchDogListener.Dispose();
            this._watchDog.Dispose();

            base.OnClosing(e);
        }

        private void UpdateResultView()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(this.UpdateResultView));
            }
            else
            {
                this.resultView.Reload();
                this.editorToolStrip.Focus();
            }
        }

        #region [IResultViewDataSource]
        
        public string HTMLForResultView(ResultView instance)
        {
            return this._renderedDocument.ToString();
        }

        #endregion

        #region [IObserver<Document>]

        public void OnNext(Document value)
        {
            this._renderedDocument = value;
            this.UpdateResultView();
        }

        public void OnCompleted() { }
        public void OnError(Exception error) { }

        #endregion

        #region [IEditorToolStripDataSource]

        public int NumberOfEditorToolStripItems(EditorToolStrip instance)
        {
            return this._editorItems.Length;
        }

        public ToolStripItem EditorToolStripItemForIndex(EditorToolStrip instance, int index)
        {
            return this._editorItems[index];
        }

        #endregion

        #region [IEditorToolStripDelegate]

        public void EditorToolStripItemClicked(EditorToolStrip instance, ToolStripItem item)
        {
            Debug.WriteLine("Item Clicked: {0}", new object[] { item });

            switch (item.Text)
            {
                case "Open File...":
                    (this.Owner as MainWindow).Open();
                    break;
                case "Default":
                case "GitHub":
                    // TODO: Delegate this to a factory or something...
                    if (item.Text == "Default")
                    {
                        this._watchDog.Processor = new DefaultProcessor();
                    }
                    else
                    {
                        this._watchDog.Processor = new GitHubProcessor();
                    }

                    this._watchDog.Notify();
                    break;
            }
        }

        #endregion

        private void DefineEditorItems()
        {
            var styleButton = new ToolStripDropDownButton();            
            styleButton.Text = "Display Style";
            styleButton.ImageKey = "style";
            styleButton.Alignment = ToolStripItemAlignment.Right;
            styleButton.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;

            // TODO: Make dynamic based on implementations
            styleButton.DropDownItems.Add(MakeStyleItem("Default", Keys.Alt | Keys.D1));
            styleButton.DropDownItems.Add(MakeStyleItem("GitHub", Keys.Alt | Keys.D2));

            var saveButton = new ToolStripMenuItem();
            saveButton.ImageKey = "save";
            saveButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            saveButton.Text = saveButton.ToolTipText = "Save HTML...";

            var openButton = new ToolStripMenuItem();
            openButton.ImageKey = "file";
            openButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            openButton.Text = openButton.ToolTipText = "Open File...";
            
            this._editorItems = new ToolStripItem[] {
                styleButton,
                openButton
            };
        }

        private static ToolStripMenuItem MakeStyleItem(string text, Keys shortCut)
        {
            var defaultStyle = new ToolStripMenuItem(text);
            defaultStyle.ShowShortcutKeys = true;
            defaultStyle.ShortcutKeys = shortCut;
            return defaultStyle;
        }
    }
}
