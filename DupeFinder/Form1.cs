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

        private List<string> getFileList(List<string> fileList)
        {
            DirectoryInfo dir = new DirectoryInfo(txtFolderPath.Text);
            FileInfo[] dirFiles = dir.GetFiles("*.*");

            foreach (FileInfo file in dirFiles)
            {
                fileList.Add(file.Name);
            }

            return fileList;
        }

        private void btnSelectFolder_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                txtFolderPath.Text = folderBrowserDialog.SelectedPath;
            }
        }

        private void btnCompareFiles_Click(object sender, EventArgs e)
        {
            List<string> fileList = new List<string>();
            getFileList(fileList);

            string str = "";

            foreach (string file in fileList)
            {
                str += file + "\n";
            }
            MessageBox.Show(str);
        }
    }
}
