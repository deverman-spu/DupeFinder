using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        private List<string> getFileList(List<string> fileList)
        {
            DirectoryInfo dir = new DirectoryInfo(txtFolderPath.Text);
            FileInfo[] dirFiles = dir.GetFiles("*.*");

            foreach (FileInfo file in dirFiles)
            {
                fileList.Add(txtFolderPath.Text + "\\" +  file.Name);
            }

            return fileList;
        }

        /** Simple method that opens a folder browser dialog then populates our textbox with the value **/
        private void btnSelectFolder_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                txtFolderPath.Text = folderBrowserDialog.SelectedPath;
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
            List<string> fileList = new List<string>();
            getFileList(fileList);

            foreach (string file in fileList)
            {
                long fileSize = new System.IO.FileInfo(file).Length;

                foreach (string checkFile in fileList)
                {
                    long checkFileSize = new System.IO.FileInfo(checkFile).Length;

                    if ((fileSize == checkFileSize) && (file.Equals(checkFile) == false))
                    {
                        lblStatus.Text = fileSize + "  -  " + checkFileSize;
                        MessageBox.Show(file + "\n\n" + checkFile);
                    }
                }
            }
        }
    }
}
