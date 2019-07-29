namespace FormSandbox
{
    partial class frmTetris
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
            this.pbx = new System.Windows.Forms.PictureBox();
            this.bgWorker = new System.ComponentModel.BackgroundWorker();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pbx)).BeginInit();
            this.SuspendLayout();
            this.KeyPreview = true;
            // 
            // pbx
            // 
            this.pbx.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbx.Location = new System.Drawing.Point(12, 12);
            this.pbx.Name = "pbx";
            this.pbx.Size = new System.Drawing.Size(210, 360);
            this.pbx.TabIndex = 0;
            this.pbx.TabStop = false;
            // 
            // bgWorker
            // 
            this.bgWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgWorker_DoWork);
            this.bgWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgWorker_ProgressChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(228, 27);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(54, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // frmTetris
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(294, 393);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.pbx);
            this.Name = "frmTetris";
            this.Text = "frmTetris";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmTetris_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.pbx)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pbx;
        private System.ComponentModel.BackgroundWorker bgWorker;
        private System.Windows.Forms.Button button1;
    }
}