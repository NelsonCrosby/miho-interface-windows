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
        private Driver mihoDriver = null;
        private bool closing = false;

        public Miho()
        {
            InitializeComponent();
            Driver.Setup().ContinueWith(
                (task) => BeginInvoke(new MethodInvoker(
                    () =>
                    {
                        statusLabel.Text = "Ready";
                        btnStart.Enabled = true;
                    }
                ))
            );
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (mihoDriver == null)
            {
                btnStart.Enabled = false;
                btnStart.Text = "Starting...";

                mihoDriver = new Driver();
                mihoDriver.WaitForStart().ContinueWith(
                    (task) =>  BeginInvoke(new MethodInvoker(
                        () =>
                        {
                            statusLabel.Text = "Running";
                            btnStart.Text = "Stop";
                            btnStart.Enabled = true;
                        }
                    ))
                );

                mihoDriver.WaitForStop().ContinueWith(
                    (task) => closing ? null : BeginInvoke(new MethodInvoker(
                        () =>
                        {
                            mihoDriver = null;
                            statusLabel.Text = "Ready";
                            btnStart.Text = "Start";
                            btnStart.Enabled = true;
                        }
                    ))
                );
            }
            else
            {
                btnStart.Enabled = false;
                btnStart.Text = "Stopping...";
                mihoDriver.Stop();
            }
        }

        private void Miho_FormClosing(object sender, FormClosingEventArgs e)
        {
            closing = true;
            if (mihoDriver != null)
            {
                mihoDriver.Stop();
            }
        }
    }
}
