using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using MihoDriver;

namespace MihoInterface
{
    public partial class Miho : Form
    {
        public Miho()
        {
            InitializeComponent();
            Driver.Setup().ContinueWith(
                (task) => statusLabel.Text = "Ready"
            );
        }
    }
}
