﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Optimizer
{
    public partial class MessagerForm : System.Windows.Forms.Form
    {
        MainForm _main;
        MessageType _type;

        private void Confirm()
        {
            if (_type == MessageType.Error)
            {
                this.Close();
            }
            if (_type == MessageType.Optimize)
            {
                OptimizeForm f = new OptimizeForm(!chkPrint.Checked, !chkSensors.Checked);
                f.ShowDialog();
                f.BringToFront();
                this.Close();
            }
            if (_type == MessageType.Startup)
            {
                _main.RemoveAllStartupItems();
            }
            if (_type == MessageType.Restart)
            {
                Utilities.Reboot();
            }
            if (_type == MessageType.Hosts)
            {
                _main.RemoveAllHostsEntries();
            }
            if (_type == MessageType.Integrator)
            {
                _main.RemoveAllDesktopItems();
            }
        }

        internal MessagerForm(MainForm main, MessageType m, string text)
        {
            InitializeComponent();
            Options.ApplyTheme(this);

            _main = main;
            _type = m;

            lblMessage.Text = text;

            if (_type == MessageType.Error)
            {
                btnNo.Visible = false;
                btnYes.Text = "OK";

                this.AcceptButton = btnNo;
                this.AcceptButton = btnYes;
                this.CancelButton = btnNo;
                this.CancelButton = btnYes;
            }
            if (_type == MessageType.Optimize)
            {
                chkPrint.Checked = true;
                chkPrint.Visible = true;
                chkSensors.Checked = false;
                chkSensors.Visible = true;
            }
        }

        private void btnNo_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnYes_Click(object sender, EventArgs e)
        {
            Confirm();
            this.Close();
        }

        private void Messager_Load(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;
            this.BringToFront();
        }
    }
}
