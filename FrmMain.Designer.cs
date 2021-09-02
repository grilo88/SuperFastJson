
namespace SuperFastJson
{
    partial class FrmMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.BtnBenchmark = new System.Windows.Forms.Button();
            this.BtnBenchmark2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // BtnBenchmark
            // 
            this.BtnBenchmark.Location = new System.Drawing.Point(225, 157);
            this.BtnBenchmark.Name = "BtnBenchmark";
            this.BtnBenchmark.Size = new System.Drawing.Size(148, 23);
            this.BtnBenchmark.TabIndex = 0;
            this.BtnBenchmark.Text = "Benchmark";
            this.BtnBenchmark.UseVisualStyleBackColor = true;
            this.BtnBenchmark.Click += new System.EventHandler(this.BtnBenchmark_Click);
            // 
            // BtnBenchmark2
            // 
            this.BtnBenchmark2.Location = new System.Drawing.Point(225, 199);
            this.BtnBenchmark2.Name = "BtnBenchmark2";
            this.BtnBenchmark2.Size = new System.Drawing.Size(148, 23);
            this.BtnBenchmark2.TabIndex = 1;
            this.BtnBenchmark2.Text = "Benchmark 2";
            this.BtnBenchmark2.UseVisualStyleBackColor = true;
            this.BtnBenchmark2.Click += new System.EventHandler(this.BtnBenchmark2_Click);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(627, 477);
            this.Controls.Add(this.BtnBenchmark2);
            this.Controls.Add(this.BtnBenchmark);
            this.Name = "FrmMain";
            this.Text = "Protótipo SuperFastJson";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button BtnBenchmark;
        private System.Windows.Forms.Button BtnBenchmark2;
    }
}

