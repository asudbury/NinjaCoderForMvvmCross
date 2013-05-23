using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NinjaCoder.MvvmCross.UserControls
{
    public partial class Logo : UserControl
    {
        public Logo()
        {
            InitializeComponent();

            Version version = this.GetType().Assembly.GetName().Version;

            this.labelVersion.Text += " v" + version.Major + "." + version.Minor + "." + version.Revision;

            this.labelMvvmCross.Text += " v3.0.6";
        }
    }
}
