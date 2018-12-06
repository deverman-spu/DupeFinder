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

        /** Create instance of our results form so we can send data to it **/
        Results resultForm = new Results();

        public Main()
        {
            InitializeComponent();
            backgroundWorker1.WorkerReportsProgress = true;
            backgroundWorker1.WorkerSupportsCancellation = true;
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
        private void ShowResults()
        {
                resultForm.StartPosition = FormStartPosition.CenterParent;
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

            if (chkRecursive.Checked == true)
            {
                foreach (string file in Directory.EnumerateFiles(txtFolderPath.Text, "*.*", SearchOption.AllDirectories))
                {
                    fileList.Add(file);
                }
            }
            else if (chkRecursive.Checked == false)
            {
                foreach (string file in Directory.EnumerateFiles(txtFolderPath.Text, "*.*", SearchOption.TopDirectoryOnly))
                {
                    fileList.Add(file);
                }
            }


            totalFiles = fileList.Count;

            foreach (string file in fileList)
            {
                long fileSize = new System.IO.FileInfo(file).Length;
                bool needParent = true;
                TreeNode parentNode = new TreeNode(file);

                foreach (string checkFile in fileList)
                {
                    long checkFileSize = new System.IO.FileInfo(checkFile).Length;
                    TreeNode childNode = new TreeNode(checkFile);

                    if (alreadyChecked.Contains(file) == false)
                    { 
                        if ((fileSize == checkFileSize) && (file.Equals(checkFile) == false))
                        {
                            if (radByHash.Checked == true)
                            {
                                if (CompareBytes(file, checkFile) == true)
                                {
                                    if (needParent == true)
                                    {
                                        tempTreeView.Nodes.Add(parentNode);
                                        needParent = false;
                                    }

                                    tempTreeView.SelectedNode = parentNode;

                                    if (tempTreeView.SelectedNode != null)
                                    {
                                        tempTreeView.SelectedNode.Nodes.Add(childNode);
                                        alreadyChecked.Add(checkFile);
                                    }
                                }
                            }
                            else if (radByteByByte.Checked == true)
                            {
                                if (CompareMD5(file, checkFile) == true)
                                {
                                    if (needParent == true)
                                    {
                                        tempTreeView.Nodes.Add(parentNode);
                                        needParent = false;
                                    }

                                    tempTreeView.SelectedNode = parentNode;

                                    if (tempTreeView.SelectedNode != null)
                                    {
                                        tempTreeView.SelectedNode.Nodes.Add(childNode);
                                        alreadyChecked.Add(checkFile);
                                    }
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
                    worker.ReportProgress((int)Math.Floor((((double)currentFile / (double)totalFiles) * 100)), new Tuple<string, TreeView>(file, tempTreeView));
                    currentFile++;
                }
            }
        }

        /** Updates the progress of the scan **/
        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.progressBar1.Value = e.ProgressPercentage;
            string currentFile = "";

            var args = (Tuple<string, TreeView>)e.UserState;
            string file = args.Item1;
            TreeView tempTreeView = args.Item2;
            

            if (fileList.IndexOf(file) < fileList.Count - 1)
            {
                currentFile = fileList[fileList.IndexOf(file) + 1];
            }
            else
            {
                currentFile = file;
            }

            if (currentFile.Length > 67)
            {
                currentFile = currentFile.Substring(0, currentFile.IndexOf('\\', currentFile.IndexOf('\\') + 1) + 1) + "..." + currentFile.Substring(currentFile.Substring(0, currentFile.LastIndexOf("\\")).LastIndexOf("\\")); ;
            }
    
            lblCurrentFile.Text = currentFile;

                        if (tempTreeView.Nodes.Count != 0 && !tempTreeView.Nodes[0].ToString().Equals(null))
            {
                var ar = System.Array.CreateInstance(typeof(TreeNode), tempTreeView.Nodes.Count);
                tempTreeView.Nodes.CopyTo(ar, 0);

                foreach (TreeNode item in ar)
                {
                    if (item.Text != "")
                    {
                        resultForm.resultsTreeView.Nodes.Add((TreeNode)item.Clone());
                    }
                }
                tempTreeView.Nodes.Clear();
            }
        }

        /** Updates label based on the result of the scan **/
        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            btnCompareFiles.Enabled = true;
            btnCancel.Enabled = false;
            chkRecursive.Enabled = true;
            radByHash.Enabled = true;
            radByteByByte.Enabled = true;

            if (e.Cancelled == true)
            {
                lblStatus.Text = "";
                lblCurrentFile.Text = "Canceled!";
                ShowResults();
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
                ShowResults();
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
            if (txtFolderPath.Text == "")
            {
                MessageBox.Show("Error: Choose a directory!", "DupeFinder", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            btnCancel.Enabled = true;
            btnCompareFiles.Enabled = false;
            chkRecursive.Enabled = false;
            radByHash.Enabled = false;
            radByteByByte.Enabled = false;
            lblCurrentFile.Text = "";
            lblStatus.Text = "Current File:";

            if (resultForm != null)
            {
                resultForm.ClearResults();
            }
            
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
