using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TDBot
{
    public partial class Form1 : Form
    {
        ConfigForm configurator;
        bool ButtonClose = false;
        NotifyIcon icon;

        public Form1()
        {
            InitializeComponent();
            configurator = new ConfigForm();
            SetTray();          //set notify config
            configurator.GetCloseTray();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure to stop bot and exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                ButtonClose = true;
                Close();
            }else{
                ButtonClose = false;
            }
        }        

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (ButtonClose) return;        //exit anyway
            if (configurator.GetCloseTray())      //hide to tray
            {
                this.Hide();
                icon.Visible = true;
                icon.ShowBalloonTip(2000,"Minimized", "The application has been minimized to tray.", ToolTipIcon.Info);
                e.Cancel = true;
            }
            else
            {           //exit app cuz no tray setted
                return;
            }
        }

        private void SetTray()
        {
            icon = new NotifyIcon();
            icon.Icon = new Icon("notify.ico");
            icon.DoubleClick += new EventHandler(icon_DoubleClick);
        }

        void icon_DoubleClick(object sender, EventArgs e)
        {
            this.Show();
            icon.Visible = false;
        }

        private void btnConfig_Click(object sender, EventArgs e)
        {
            configurator.ShowDialog();
        }
    }
}
