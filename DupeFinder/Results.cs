///
/// The results form. A form that will display the results of the search in a treeView object..
/// 

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

        /// <summary>
        /// Generate results form and initalize components.
        /// </summary>
        public Results()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Function that clears the results treeview when form is closed.
        /// </summary>
        public void ClearResults()
        {
            resultsTreeView.Nodes.Clear();
        }
    }
}
