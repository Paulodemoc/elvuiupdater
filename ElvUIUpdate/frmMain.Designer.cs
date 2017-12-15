namespace ElvUIUpdate
{
    partial class frmMain
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.pgBar = new System.Windows.Forms.ProgressBar();
            this.lbProgress = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // pgBar
            // 
            this.pgBar.Location = new System.Drawing.Point(12, 29);
            this.pgBar.Name = "pgBar";
            this.pgBar.Size = new System.Drawing.Size(440, 23);
            this.pgBar.Step = 1;
            this.pgBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.pgBar.TabIndex = 0;
            // 
            // lbProgress
            // 
            this.lbProgress.AutoSize = true;
            this.lbProgress.Location = new System.Drawing.Point(17, 9);
            this.lbProgress.Name = "lbProgress";
            this.lbProgress.Size = new System.Drawing.Size(0, 13);
            this.lbProgress.TabIndex = 1;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(464, 63);
            this.Controls.Add(this.lbProgress);
            this.Controls.Add(this.pgBar);
            this.ForeColor = System.Drawing.Color.White;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMain";
            this.Text = "ElvUI Updater";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar pgBar;
        private System.Windows.Forms.Label lbProgress;
    }
}

