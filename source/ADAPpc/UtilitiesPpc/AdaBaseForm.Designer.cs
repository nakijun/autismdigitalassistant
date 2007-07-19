namespace UtilitiesPpc
{
    partial class AdaBaseForm
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
            this._setting.Dispose();
            this._languageRegistryState.Dispose();

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
            this.SuspendLayout();
            // 
            // AdaBaseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(240, 294);
            this.Name = "AdaBaseForm";
            this.Closed += new System.EventHandler(this.AdaBaseForm_Closed);
            this.GotFocus += new System.EventHandler(this.AdaBaseForm_GotFocus);
            this.Load += new System.EventHandler(this.AdaBaseForm_Load);
            this.ResumeLayout(false);

        }

        #endregion
    }
}