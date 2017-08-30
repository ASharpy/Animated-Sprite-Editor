namespace Animated_Sprite_Editor
{
    partial class Form1
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AddToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveASGIFToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveImagesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel2 = new System.Windows.Forms.Panel();
            this.SpriteListDelete = new System.Windows.Forms.Button();
            this.play = new System.Windows.Forms.Button();
            this.Select = new System.Windows.Forms.CheckBox();
            this.SpriteSheet = new System.Windows.Forms.PictureBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.menuStrip1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SpriteSheet)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.MenuBar;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.MdiWindowListItem = this.fileToolStripMenuItem;
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1354, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AddToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.removeToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // AddToolStripMenuItem
            // 
            this.AddToolStripMenuItem.Name = "AddToolStripMenuItem";
            this.AddToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.AddToolStripMenuItem.Text = "Add Image";
            this.AddToolStripMenuItem.Click += new System.EventHandler(this.AddToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveASGIFToolStripMenuItem,
            this.saveImagesToolStripMenuItem});
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.saveToolStripMenuItem.Text = "Save Image";
            // 
            // saveASGIFToolStripMenuItem
            // 
            this.saveASGIFToolStripMenuItem.Name = "saveASGIFToolStripMenuItem";
            this.saveASGIFToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.saveASGIFToolStripMenuItem.Text = "Save AS GIF";
            this.saveASGIFToolStripMenuItem.Click += new System.EventHandler(this.saveASGIFToolStripMenuItem_Click);
            // 
            // saveImagesToolStripMenuItem
            // 
            this.saveImagesToolStripMenuItem.Name = "saveImagesToolStripMenuItem";
            this.saveImagesToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.saveImagesToolStripMenuItem.Text = "Save Images";
            // 
            // removeToolStripMenuItem
            // 
            this.removeToolStripMenuItem.Name = "removeToolStripMenuItem";
            this.removeToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.removeToolStripMenuItem.Text = "Remove Image";
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.SpriteListDelete);
            this.panel2.Controls.Add(this.play);
            this.panel2.Controls.Add(this.Select);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 24);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1354, 75);
            this.panel2.TabIndex = 5;
            this.panel2.Click += new System.EventHandler(this.panel2_Click);
            // 
            // SpriteListDelete
            // 
            this.SpriteListDelete.Location = new System.Drawing.Point(355, 21);
            this.SpriteListDelete.Name = "SpriteListDelete";
            this.SpriteListDelete.Size = new System.Drawing.Size(97, 23);
            this.SpriteListDelete.TabIndex = 2;
            this.SpriteListDelete.Text = "Delete Sprite List";
            this.SpriteListDelete.UseVisualStyleBackColor = true;
            this.SpriteListDelete.Click += new System.EventHandler(this.SpriteListDelete_Click);
            // 
            // play
            // 
            this.play.Location = new System.Drawing.Point(220, 21);
            this.play.Name = "play";
            this.play.Size = new System.Drawing.Size(75, 23);
            this.play.TabIndex = 1;
            this.play.Text = "Play";
            this.play.UseVisualStyleBackColor = true;
            this.play.Click += new System.EventHandler(this.play_Click);
            // 
            // Select
            // 
            this.Select.Appearance = System.Windows.Forms.Appearance.Button;
            this.Select.AutoSize = true;
            this.Select.Location = new System.Drawing.Point(72, 21);
            this.Select.Name = "Select";
            this.Select.Size = new System.Drawing.Size(77, 23);
            this.Select.TabIndex = 0;
            this.Select.Text = "Select Sprite";
            this.Select.UseVisualStyleBackColor = true;
            // 
            // SpriteSheet
            // 
            this.SpriteSheet.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.SpriteSheet.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.SpriteSheet.Cursor = System.Windows.Forms.Cursors.Default;
            this.SpriteSheet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SpriteSheet.Location = new System.Drawing.Point(0, 99);
            this.SpriteSheet.Name = "SpriteSheet";
            this.SpriteSheet.Size = new System.Drawing.Size(1354, 547);
            this.SpriteSheet.TabIndex = 6;
            this.SpriteSheet.TabStop = false;
            this.SpriteSheet.Click += new System.EventHandler(this.SpriteSheet_Click);
            this.SpriteSheet.Paint += new System.Windows.Forms.PaintEventHandler(this.SpriteSheet_Paint);
            this.SpriteSheet.MouseDown += new System.Windows.Forms.MouseEventHandler(this.SpriteSheet_MouseDown);
            this.SpriteSheet.MouseMove += new System.Windows.Forms.MouseEventHandler(this.SpriteSheet_MouseMove);
            this.SpriteSheet.MouseUp += new System.Windows.Forms.MouseEventHandler(this.SpriteSheet_MouseUp);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AllowDrop = true;
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 646);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(1354, 134);
            this.flowLayoutPanel1.TabIndex = 8;
            this.flowLayoutPanel1.WrapContents = false;
            this.flowLayoutPanel1.DragDrop += new System.Windows.Forms.DragEventHandler(this.flowLayoutPanel1_DragDrop);
            this.flowLayoutPanel1.DragEnter += new System.Windows.Forms.DragEventHandler(this.flowLayoutPanel1_DragEnter);
            this.flowLayoutPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.flowLayoutPanel1_Paint);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1354, 780);
            this.Controls.Add(this.SpriteSheet);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.flowLayoutPanel1);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Animated Sprite Editor";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SpriteSheet)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AddToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeToolStripMenuItem;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.PictureBox SpriteSheet;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.CheckBox Select;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button play;
        private System.Windows.Forms.ToolStripMenuItem saveASGIFToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveImagesToolStripMenuItem;
        private System.Windows.Forms.Button SpriteListDelete;
    }
}

