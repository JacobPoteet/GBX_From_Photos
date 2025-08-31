namespace GBX_From_Photos
{
    partial class MainForm
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
            this.btnSelectFolder = new System.Windows.Forms.Button();
            this.txtSelectedFolder = new System.Windows.Forms.TextBox();
            this.btnStartProcessing = new System.Windows.Forms.Button();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.lblTotalPhotos = new System.Windows.Forms.Label();
            this.lblProcessed = new System.Windows.Forms.Label();
            this.lblRemaining = new System.Windows.Forms.Label();
            this.lblSuccessful = new System.Windows.Forms.Label();
            this.lblSkipped = new System.Windows.Forms.Label();
            this.lblErrors = new System.Windows.Forms.Label();
            this.lblSuccessRate = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSelectFolder
            // 
            this.btnSelectFolder.Location = new System.Drawing.Point(12, 12);
            this.btnSelectFolder.Name = "btnSelectFolder";
            this.btnSelectFolder.Size = new System.Drawing.Size(120, 30);
            this.btnSelectFolder.TabIndex = 0;
            this.btnSelectFolder.Text = "Select Folder";
            this.btnSelectFolder.UseVisualStyleBackColor = true;
            this.btnSelectFolder.Click += new System.EventHandler(this.btnSelectFolder_Click);
            // 
            // txtSelectedFolder
            // 
            this.txtSelectedFolder.Location = new System.Drawing.Point(138, 12);
            this.txtSelectedFolder.Name = "txtSelectedFolder";
            this.txtSelectedFolder.ReadOnly = true;
            this.txtSelectedFolder.Size = new System.Drawing.Size(450, 30);
            this.txtSelectedFolder.TabIndex = 1;
            this.txtSelectedFolder.Text = "No folder selected";
            // 
            // btnStartProcessing
            // 
            this.btnStartProcessing.Enabled = false;
            this.btnStartProcessing.Location = new System.Drawing.Point(12, 48);
            this.btnStartProcessing.Name = "btnStartProcessing";
            this.btnStartProcessing.Size = new System.Drawing.Size(120, 30);
            this.btnStartProcessing.TabIndex = 2;
            this.btnStartProcessing.Text = "Start Processing";
            this.btnStartProcessing.UseVisualStyleBackColor = true;
            this.btnStartProcessing.Click += new System.EventHandler(this.btnStartProcessing_Click);
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(12, 88);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(576, 30);
            this.progressBar.TabIndex = 3;
            // 
            // lblTotalPhotos
            // 
            this.lblTotalPhotos.AutoSize = true;
            this.lblTotalPhotos.Location = new System.Drawing.Point(6, 25);
            this.lblTotalPhotos.Name = "lblTotalPhotos";
            this.lblTotalPhotos.Size = new System.Drawing.Size(100, 20);
            this.lblTotalPhotos.TabIndex = 4;
            this.lblTotalPhotos.Text = "Total Photos: 0";
            // 
            // lblProcessed
            // 
            this.lblProcessed.AutoSize = true;
            this.lblProcessed.Location = new System.Drawing.Point(6, 55);
            this.lblProcessed.Name = "lblProcessed";
            this.lblProcessed.Size = new System.Drawing.Size(90, 20);
            this.lblProcessed.TabIndex = 5;
            this.lblProcessed.Text = "Processed: 0";
            // 
            // lblRemaining
            // 
            this.lblRemaining.AutoSize = true;
            this.lblRemaining.Location = new System.Drawing.Point(6, 85);
            this.lblRemaining.Name = "lblRemaining";
            this.lblRemaining.Size = new System.Drawing.Size(100, 20);
            this.lblRemaining.TabIndex = 6;
            this.lblRemaining.Text = "Remaining: 0";
            // 
            // lblSuccessful
            // 
            this.lblSuccessful.AutoSize = true;
            this.lblSuccessful.Location = new System.Drawing.Point(6, 25);
            this.lblSuccessful.Name = "lblSuccessful";
            this.lblSuccessful.Size = new System.Drawing.Size(100, 20);
            this.lblSuccessful.TabIndex = 7;
            this.lblSuccessful.Text = "Successful: 0";
            // 
            // lblSkipped
            // 
            this.lblSkipped.AutoSize = true;
            this.lblSkipped.Location = new System.Drawing.Point(6, 55);
            this.lblSkipped.Name = "lblSkipped";
            this.lblSkipped.Size = new System.Drawing.Size(85, 20);
            this.lblSkipped.TabIndex = 8;
            this.lblSkipped.Text = "Skipped: 0";
            // 
            // lblErrors
            // 
            this.lblErrors.AutoSize = true;
            this.lblErrors.Location = new System.Drawing.Point(6, 85);
            this.lblErrors.Name = "lblErrors";
            this.lblErrors.Size = new System.Drawing.Size(70, 20);
            this.lblErrors.TabIndex = 9;
            this.lblErrors.Text = "Errors: 0";
            // 
            // lblSuccessRate
            // 
            this.lblSuccessRate.AutoSize = true;
            this.lblSuccessRate.Location = new System.Drawing.Point(6, 115);
            this.lblSuccessRate.Name = "lblSuccessRate";
            this.lblSuccessRate.Size = new System.Drawing.Size(120, 20);
            this.lblSuccessRate.TabIndex = 10;
            this.lblSuccessRate.Text = "Success Rate: 0.0%";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblTotalPhotos);
            this.groupBox1.Controls.Add(this.lblProcessed);
            this.groupBox1.Controls.Add(this.lblRemaining);
            this.groupBox1.Location = new System.Drawing.Point(12, 130);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(280, 120);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Processing Status";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblSuccessful);
            this.groupBox2.Controls.Add(this.lblSkipped);
            this.groupBox2.Controls.Add(this.lblErrors);
            this.groupBox2.Controls.Add(this.lblSuccessRate);
            this.groupBox2.Location = new System.Drawing.Point(308, 130);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(280, 150);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Results";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 300);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.btnStartProcessing);
            this.Controls.Add(this.txtSelectedFolder);
            this.Controls.Add(this.btnSelectFolder);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Photo to GPX Converter";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSelectFolder;
        private System.Windows.Forms.TextBox txtSelectedFolder;
        private System.Windows.Forms.Button btnStartProcessing;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label lblTotalPhotos;
        private System.Windows.Forms.Label lblProcessed;
        private System.Windows.Forms.Label lblRemaining;
        private System.Windows.Forms.Label lblSuccessful;
        private System.Windows.Forms.Label lblSkipped;
        private System.Windows.Forms.Label lblErrors;
        private System.Windows.Forms.Label lblSuccessRate;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
    }
}
