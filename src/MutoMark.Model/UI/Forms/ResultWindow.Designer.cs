namespace MutoMark.Model
{
    partial class ResultWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ResultWindow));
            this.toolStripImages = new System.Windows.Forms.ImageList(this.components);
            this.resultView = new MutoMark.Model.ResultView();
            this.editorToolStrip = new MutoMark.Model.EditorToolStrip();
            this.SuspendLayout();
            // 
            // toolStripImages
            // 
            this.toolStripImages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("toolStripImages.ImageStream")));
            this.toolStripImages.TransparentColor = System.Drawing.Color.Transparent;
            this.toolStripImages.Images.SetKeyName(0, "save");
            this.toolStripImages.Images.SetKeyName(1, "source");
            this.toolStripImages.Images.SetKeyName(2, "style");
            this.toolStripImages.Images.SetKeyName(3, "file");
            // 
            // resultView
            // 
            this.resultView.DataSource = null;
            this.resultView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.resultView.Location = new System.Drawing.Point(0, 25);
            this.resultView.Name = "resultView";
            this.resultView.Size = new System.Drawing.Size(786, 522);
            this.resultView.TabIndex = 3;
            // 
            // editorToolStrip
            // 
            this.editorToolStrip.DataSource = null;
            this.editorToolStrip.Delegate = null;
            this.editorToolStrip.Location = new System.Drawing.Point(0, 0);
            this.editorToolStrip.Margin = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.editorToolStrip.Name = "editorToolStrip";
            this.editorToolStrip.Size = new System.Drawing.Size(786, 25);
            this.editorToolStrip.TabIndex = 2;
            this.editorToolStrip.Text = "editorToolStrip1";
            // 
            // ResultWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(786, 547);
            this.Controls.Add(this.resultView);
            this.Controls.Add(this.editorToolStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ResultWindow";
            this.Text = "MutoMark!";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private EditorToolStrip editorToolStrip;
        private ResultView resultView;
        private System.Windows.Forms.ImageList toolStripImages;
    }
}