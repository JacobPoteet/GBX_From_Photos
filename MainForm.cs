using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using MetadataExtractor;
using MetadataExtractor.Formats.Exif;

namespace GBX_From_Photos
{
    public partial class MainForm : Form
    {
        private string selectedFolder = string.Empty;
        private readonly List<string> supportedExtensions = new() { ".jpg", ".jpeg", ".png", ".heic" };
        private readonly string outputFolder = "output";
        private readonly string logFileName = "errors.log";

        public MainForm()
        {
            InitializeComponent();
            InitializeOutputFolder();
        }

        private void InitializeOutputFolder()
        {
            if (!Directory.Exists(outputFolder))
            {
                Directory.CreateDirectory(outputFolder);
            }
        }

        private void btnSelectFolder_Click(object sender, EventArgs e)
        {
            using (var folderDialog = new FolderBrowserDialog())
            {
                folderDialog.Description = "Select folder containing photos with GPS data";
                folderDialog.ShowNewFolderButton = false;

                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    selectedFolder = folderDialog.SelectedPath;
                    txtSelectedFolder.Text = selectedFolder;
                    btnStartProcessing.Enabled = true;
                    ResetStatus();
                }
            }
        }

        private void btnStartProcessing_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(selectedFolder))
            {
                MessageBox.Show("Please select a folder first.", "No Folder Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            btnStartProcessing.Enabled = false;
            btnSelectFolder.Enabled = false;
            
            Task.Run(() => ProcessPhotosAsync());
        }

        private async Task ProcessPhotosAsync()
        {
            try
            {
                var photoProcessor = new PhotoProcessor();
                var progress = new Progress<ProcessingProgress>(UpdateProgress);
                
                var result = await photoProcessor.ProcessPhotosAsync(selectedFolder, supportedExtensions, outputFolder, progress);
                
                Invoke(() => 
                {
                    UpdateFinalStatus(result);
                    btnStartProcessing.Enabled = true;
                    btnSelectFolder.Enabled = true;
                });
            }
            catch (Exception ex)
            {
                Invoke(() => 
                {
                    MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    btnStartProcessing.Enabled = true;
                    btnSelectFolder.Enabled = true;
                });
            }
        }

        private void UpdateProgress(ProcessingProgress progress)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<ProcessingProgress>(UpdateProgress), progress);
                return;
            }

            progressBar.Value = progress.Percentage;
            lblTotalPhotos.Text = $"Total Photos: {progress.TotalPhotos}";
            lblProcessed.Text = $"Processed: {progress.ProcessedPhotos}";
            lblRemaining.Text = $"Remaining: {progress.RemainingPhotos}";
            lblSuccessful.Text = $"Successful: {progress.SuccessfulPhotos}";
            lblSkipped.Text = $"Skipped: {progress.SkippedPhotos}";
            lblErrors.Text = $"Errors: {progress.ErrorPhotos}";
            lblSuccessRate.Text = $"Success Rate: {progress.SuccessRate:F1}%";
        }

        private void UpdateFinalStatus(ProcessingResult result)
        {
            progressBar.Value = 100;
            
            string message = $"Processing complete!\n\n" +
                           $"Total photos found: {result.TotalPhotos}\n" +
                           $"Successfully processed: {result.SuccessfulPhotos}\n" +
                           $"Skipped (no GPS): {result.SkippedPhotos}\n" +
                           $"Errors: {result.ErrorPhotos}\n" +
                           $"Success rate: {result.SuccessRate:F1}%\n\n" +
                           $"GPX file saved to: {result.GpxFilePath}\n" +
                           $"Error log saved to: {result.LogFilePath}";

            if (result.SuccessfulPhotos > 0)
            {
                MessageBox.Show(message, "Processing Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(message + "\n\nNo photos with GPS data were found.", "Processing Complete", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ResetStatus()
        {
            progressBar.Value = 0;
            lblTotalPhotos.Text = "Total Photos: 0";
            lblProcessed.Text = "Processed: 0";
            lblRemaining.Text = "Remaining: 0";
            lblSuccessful.Text = "Successful: 0";
            lblSkipped.Text = "Skipped: 0";
            lblErrors.Text = "Errors: 0";
            lblSuccessRate.Text = "Success Rate: 0.0%";
        }
    }

    public class ProcessingProgress
    {
        public int TotalPhotos { get; set; }
        public int ProcessedPhotos { get; set; }
        public int RemainingPhotos { get; set; }
        public int SuccessfulPhotos { get; set; }
        public int SkippedPhotos { get; set; }
        public int ErrorPhotos { get; set; }
        public int Percentage { get; set; }
        public double SuccessRate { get; set; }
    }

    public class ProcessingResult
    {
        public int TotalPhotos { get; set; }
        public int SuccessfulPhotos { get; set; }
        public int SkippedPhotos { get; set; }
        public int ErrorPhotos { get; set; }
        public double SuccessRate { get; set; }
        public string GpxFilePath { get; set; } = string.Empty;
        public string LogFilePath { get; set; } = string.Empty;
    }
}
