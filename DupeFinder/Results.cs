using System;
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
    public partial class Results : Form
    {
        public Results()
        {
            InitializeComponent();
        }

        /** Getter and setter for our results rich text box **/
        public string RichTextBoxValue
        {
            get
            {
                return rtbResults.Text;
            }
            set
            {
                rtbResults.Text = value;
            }
        }
    }
}
