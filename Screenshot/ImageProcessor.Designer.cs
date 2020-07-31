using System.Drawing;

namespace Screenshot
{
    partial class ImageProcessor
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
            this.pbCapture = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbCapture)).BeginInit();
            this.SuspendLayout();
            // 
            // pbCapture
            // 
            this.pbCapture.Name = "pbCapture";
            this.pbCapture.Size = new System.Drawing.Size(429, 208);
            this.pbCapture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbCapture.TabIndex = 0;
            this.pbCapture.TabStop = false;
            // 
            // ImageProcessor
            // 
            this.TopMost = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Location = initialPoint; // can only set this when we are printing a specific window and not the screen
            this.Size = size;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.AutoSize = false;
            this.ClientSize = new System.Drawing.Size(593, 304);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Controls.Add(this.pbCapture);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "ImageProcessor";
            this.Text = "ImageProcessor";
            ((System.ComponentModel.ISupportInitialize)(this.pbCapture)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbCapture;
    }
}