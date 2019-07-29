namespace FormSandbox
{
    partial class frmStart
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
            this.btnStartColorMatch = new System.Windows.Forms.Button();
            this.btnStartMinesweeper = new System.Windows.Forms.Button();
            this.btnStartTetris = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnStartColorMatch
            // 
            this.btnStartColorMatch.Location = new System.Drawing.Point(99, 112);
            this.btnStartColorMatch.Name = "btnStartColorMatch";
            this.btnStartColorMatch.Size = new System.Drawing.Size(99, 23);
            this.btnStartColorMatch.TabIndex = 0;
            this.btnStartColorMatch.Text = "color match";
            this.btnStartColorMatch.UseVisualStyleBackColor = true;
            this.btnStartColorMatch.Click += new System.EventHandler(this.btnStartColorMatch_Click);
            // 
            // btnStartMinesweeper
            // 
            this.btnStartMinesweeper.Location = new System.Drawing.Point(99, 159);
            this.btnStartMinesweeper.Name = "btnStartMinesweeper";
            this.btnStartMinesweeper.Size = new System.Drawing.Size(99, 23);
            this.btnStartMinesweeper.TabIndex = 1;
            this.btnStartMinesweeper.Text = "minesweeper";
            this.btnStartMinesweeper.UseVisualStyleBackColor = true;
            this.btnStartMinesweeper.Click += new System.EventHandler(this.btnStartMinesweeper_Click);
            // 
            // btnStartTetris
            // 
            this.btnStartTetris.Location = new System.Drawing.Point(99, 204);
            this.btnStartTetris.Name = "btnStartTetris";
            this.btnStartTetris.Size = new System.Drawing.Size(99, 23);
            this.btnStartTetris.TabIndex = 2;
            this.btnStartTetris.Text = "tetris";
            this.btnStartTetris.UseVisualStyleBackColor = true;
            this.btnStartTetris.Click += new System.EventHandler(this.btnStartTetris_Click);
            // 
            // frmStart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.btnStartTetris);
            this.Controls.Add(this.btnStartMinesweeper);
            this.Controls.Add(this.btnStartColorMatch);
            this.Name = "frmStart";
            this.Text = "frmStart";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnStartColorMatch;
        private System.Windows.Forms.Button btnStartMinesweeper;
        private System.Windows.Forms.Button btnStartTetris;
    }
}