﻿using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.IO;
using System.Windows.Forms;

namespace DupeFinder
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        /** This function just populates the generic list with all files in the given folder
         *  prefixing the filename with the filepath. It then returns this generic list to the
         *  calling function.
         **/
        private List<string> GetFileList(List<string> fileList)
        {
            DirectoryInfo dir = new DirectoryInfo(txtFolderPath.Text);
            FileInfo[] dirFiles = dir.GetFiles("*.*");

            foreach (FileInfo file in dirFiles)
            {
                fileList.Add(txtFolderPath.Text + "\\" +  file.Name);
            }

            return fileList;
        }

        /** Simple function that writes our header to our output file **/
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

        /** Simple function that writes our duplicate file names with parent file to the results file **/
        private void WriteResultWithParent(string parentFile, string dupeFile)
        {
            using (var fileWrite = new StreamWriter(txtFolderPath.Text + "\\results.txt", true))
            {
                fileWrite.WriteLine(" ______________________________________________________________________________________");
                fileWrite.WriteLine("|--- " + parentFile);
                fileWrite.WriteLine("|");
                fileWrite.WriteLine("|------ " + dupeFile);
                fileWrite.Close();
            }
        }

        /** Simple function that writes our duplicate file names without parent file to the results file **/
        private void WriteResultWithoutParent(string dupeFile)
        {
            using (var fileWrite = new StreamWriter(txtFolderPath.Text + "\\results.txt", true))
            {
                fileWrite.WriteLine("|");
                fileWrite.WriteLine("|------ " + dupeFile);
                fileWrite.Close();
            }
        }

        /** This function will compute the MD5 hash of the specified file and save it in a byte array.
         * 
         *  This function is only called if we have already determined that the file sizes are equal.
         **/
        private byte[] GetMD5(string file)
        {
            MD5 hashMD5 = MD5.Create();

            using (FileStream fileStream = File.OpenRead(file))
            {
                return hashMD5.ComputeHash(fileStream);
            }
        }

        /** This function will compare the MD5 hashes of two files and return true if they are equal. **/
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

        /** This function will compare the files byte-by-byte and return true if they are equal **/
        private bool CompareBytes(string file, string checkFile)
        {
            FileInfo fileInfo = new FileInfo(file);
            FileInfo checkFileInfo = new FileInfo(file);

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
   
        /** This function populates our results form with the results from the file and shows the form **/
        private void ShowResults(string message)
        {
            Results resultForm = new Results();
            resultForm.RichTextBoxValue = message;
            resultForm.ShowDialog();
        }

        /** Simple function that opens a folder browser dialog then populates our textbox with the value **/
        private void btnSelectFolder_Click(object sender, EventArgs e)
        {
            if (fdbMain.ShowDialog() == DialogResult.OK)
            {
                txtFolderPath.Text = fdbMain.SelectedPath;
            }
        }

        /** This function will create a generic list, populate that list with all files in the specified folder,
         *  then loop through each file comparing it to all of the other files based on file length in bytes.
         *  
         *  We only consider the files equal if the length in bytes is the same and the name is different. 
         *  (We already know that the file is equal to itself)
         **/
        private void btnCompareFiles_Click(object sender, EventArgs e)
        {
            lblStatus.Text = "Scanning...";

            List<string> fileList = new List<string>();
            GetFileList(fileList);

            WriteHeader();

            foreach (string file in fileList)
            {
                long fileSize = new System.IO.FileInfo(file).Length;
                bool needParent = true;

                foreach (string checkFile in fileList)
                {
                    long checkFileSize = new System.IO.FileInfo(checkFile).Length;

                    if ((fileSize == checkFileSize) && (file.Equals(checkFile) == false))
                    {
                        if (CompareMD5(file, checkFile) == true)
                        {
                            if (needParent == true)
                            {
                                WriteResultWithParent(file, checkFile);
                                needParent = false;
                            }
                            else
                            {
                                WriteResultWithoutParent(checkFile);
                            }
                        }
                    }
                }
            }
            lblStatus.Text = "Finished!";
            ShowResults(File.ReadAllText(txtFolderPath.Text + "\\" + "results.txt"));
        }

        /** Exits the application **/
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }
    }
}
