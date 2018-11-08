using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.Cryptography;
using System.IO;
using System.Windows.Forms;

namespace DupeFinder
{
    public partial class Main : Form
    {
        /** Generic list to hold all of our files with their paths **/
        List<string> fileList = new List<string>();

        public Main()
        {
            InitializeComponent();
            backgroundWorker1.WorkerReportsProgress = true;
            backgroundWorker1.WorkerSupportsCancellation = true;
        }

        /** Writes our header to our output file **/
        private void WriteHeader()
        {
            using (var fileWrite = new StreamWriter(txtFolderPath.Text + "\\results.txt", false))
            {
                fileWrite.WriteLine("______                 ______ _           _            ______                _ _       ");
                fileWrite.WriteLine("|  _  \\                |  ___(_)         | |           | ___ \\              | | |      ");
                fileWrite.WriteLine("| | | |_   _ _ __   ___| |_   _ _ __   __| | ___ _ __  | |_/ /___  ___ _   _| | |_ ___ ");
                fileWrite.WriteLine("| | | | | | | '_ \\ / _ \\  _| | | '_ \\ / _` |/ _ \\ '__| |    // _ \\/ __| | | | | __/ __|");
                fileWrite.WriteLine("| |/ /| |_| | |_) |  __/ |   | | | | | (_| |  __/ |    | |\\ \\  __/\\__ \\ |_| | | |_\\__ \\");
                fileWrite.WriteLine("|___/  \\__,_| .__/ \\___\\_|   |_|_| |_|\\__,_|\\___|_|    \\_| \\_\\___||___/\\__,_|_|\\__|___/");
                fileWrite.WriteLine("            | |                                                                        ");
                fileWrite.WriteLine("            |_|                                                                        ");
                fileWrite.WriteLine("");
                fileWrite.Close();
            }
        }

        /** Writes our duplicate file names with parent file to the results file **/
        private void WriteResultWithParent(string parentFile, string dupeFile)
        {
            using (var fileWrite = new StreamWriter(txtFolderPath.Text + "\\results.txt", true))
            {
                fileWrite.WriteLine(" ____________________________________________________________________________________________________________________________________________________________________________________________");
                fileWrite.WriteLine("|--- " + parentFile);
                fileWrite.WriteLine("|");
                fileWrite.WriteLine("|------ " + dupeFile);
                fileWrite.Close();
            }
        }

        /** Writes our duplicate file names without parent file to the results file **/
        private void WriteResultWithoutParent(string dupeFile)
        {
            using (var fileWrite = new StreamWriter(txtFolderPath.Text + "\\results.txt", true))
            {
                fileWrite.WriteLine("|");
                fileWrite.WriteLine("|------ " + dupeFile);
                fileWrite.Close();
            }
        }

        /** Computes the MD5 hash of the specified file and save it in a byte array. **/
        private byte[] GetMD5(string file)
        {
            MD5 hashMD5 = MD5.Create();

            using (FileStream fileStream = File.OpenRead(file))
            {
                return hashMD5.ComputeHash(fileStream);
            }
        }

        /** Compares the MD5 hashes of two files and return true if they are equal. **/
        private bool CompareMD5(string file, string checkFile)
        {
            byte[] fileMD5 = GetMD5(file);
            byte[] checkFileMD5 = GetMD5(checkFile);

            if (fileMD5.Length == checkFileMD5.Length)
            {
                for (int i = 0; i < fileMD5.Length; i++)
                {
                    if (fileMD5[i] != checkFileMD5[i])
                    {
                        return false;
                    }
                }
                return true;
            }
            return false;
        }

        /** Compares the files byte-by-byte and return true if they are equal **/
        private bool CompareBytes(string file, string checkFile)
        {
            FileInfo fileInfo = new FileInfo(file);
            FileInfo checkFileInfo = new FileInfo(checkFile);

            using (FileStream fsFile = fileInfo.OpenRead())
            using (FileStream fsCheckFile = checkFileInfo.OpenRead())
            using (BufferedStream bsFile = new BufferedStream(fsFile))
            using (BufferedStream bsCheckFile = new BufferedStream(fsCheckFile))
            {
                for (int i = 0; i < fileInfo.Length; i++)
                {
                    if (bsFile.ReadByte() != bsCheckFile.ReadByte())
                    {
                        return false;
                    }
                }
            }
            return true;
        }
   
        /** Populates our results form with the results from the file and shows the form **/
        private void ShowResults(string message)
        {
            Results resultForm = new Results();
            resultForm.RichTextBoxValue = message;
            resultForm.ShowDialog();
        }

        /** Opens a folder browser dialog then populates our textbox with the value **/
        private void btnSelectFolder_Click(object sender, EventArgs e)
        {
            lblCurrentFile.Text = "";

            if (fdbMain.ShowDialog() == DialogResult.OK)
            {
                txtFolderPath.Text = fdbMain.SelectedPath;
            }
        }

        /** Background worker function that drives our comparisons **/
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            List<string> alreadyChecked = new List<string>();
            int totalFiles = 0;
            int currentFile = 1;
            fileList.Clear();

            foreach (string file in Directory.EnumerateFiles(txtFolderPath.Text, "*.*", SearchOption.AllDirectories))
            {
                fileList.Add(file);
            }

            totalFiles = fileList.Count;

            foreach (string file in fileList)
            {
                long fileSize = new System.IO.FileInfo(file).Length;
                bool needParent = true;

                foreach (string checkFile in fileList)
                {
                    long checkFileSize = new System.IO.FileInfo(checkFile).Length;

                    if (alreadyChecked.Contains(file) == false)
                    { 
                        if ((fileSize == checkFileSize) && (file.Equals(checkFile) == false))
                        {
                            if (CompareBytes(file, checkFile) == true)
                            {
                                if (needParent == true)
                                {
                                    WriteResultWithParent(file, checkFile);
                                    alreadyChecked.Add(checkFile);
                                    needParent = false;
                                }
                                else
                                {
                                    WriteResultWithoutParent(checkFile);
                                    alreadyChecked.Add(checkFile);
                                }
                            }
                        }
                    }
                }
                if(worker.CancellationPending == true)
                {
                    e.Cancel = true;
                    break;
                }
                else
                {
                    worker.ReportProgress((int)Math.Floor((((double)currentFile / (double)totalFiles) * 100)), file);
                    currentFile++;
                }
            }
        }

        /** Updates the progress of the scan **/
        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.progressBar1.Value = e.ProgressPercentage;
            string file = e.UserState.ToString();

            if (file.Length > 67)
            {
                file = file.Substring(0, file.IndexOf('\\', file.IndexOf('\\') + 1) + 1) + "..." + file.Substring(file.Substring(0, file.LastIndexOf("\\")).LastIndexOf("\\")); ;
            }
    
            lblCurrentFile.Text = (file);
        }

        /** Updates label based on the result of the scan **/
        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            btnCompareFiles.Enabled = true;
            btnCancel.Enabled = false;

            if (e.Cancelled == true)
            {
                lblStatus.Text = "";
                lblCurrentFile.Text = "Canceled!";
                ShowResults(File.ReadAllText(txtFolderPath.Text + "\\" + "results.txt"));
            }
            else if (e.Error != null)
            {
                lblStatus.Text = "";
                lblCurrentFile.Text = "Error: " + e.Error.Message;
            }
            else
            {
                lblStatus.Text = "";
                lblCurrentFile.Text = "Finished!";
                ShowResults(File.ReadAllText(txtFolderPath.Text + "\\" + "results.txt"));
            }
        }

        /** Cancel the async background worker **/
        private void btnCancel_Click(object sender, EventArgs e)
        {
            btnCancel.Enabled = false;

            if (backgroundWorker1.WorkerSupportsCancellation == true)
            {
                backgroundWorker1.CancelAsync();
            }
        }

        /** Driver function that initiates calls to the other functions **/
        private void btnCompareFiles_Click(object sender, EventArgs e)
        {
            btnCancel.Enabled = true;
            btnCompareFiles.Enabled = false;
            lblCurrentFile.Text = "";
            lblStatus.Text = "Current File:";
            WriteHeader();
            
            if (backgroundWorker1.IsBusy != true)
            {
                backgroundWorker1.RunWorkerAsync();
            }
        }

        /** Exits the application **/
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }
    }
}
