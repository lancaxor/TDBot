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
    public partial class ConfigForm : Form
    {
        ConfigWorker config;
        private Boolean CloseTray = false;

        public ConfigForm()
        {
            InitializeComponent();
            config = new ConfigWorker();
            GetCloseTray();
            SetStartConfig();
        }

        private void SetStartConfig()
        {
            this.cbMinimizeToTray.Checked = CloseTray;
        }

        public Boolean GetCloseTray()           //from config
        {
            Boolean parseRes = Boolean.TryParse(config.GetConfig("closetray"), out CloseTray);
            if (!parseRes) CloseTray = false;
            return this.CloseTray;
        }

        public void SetConfig(String configName, String configValue)
        {
            config.SetConfig(configName, configValue);
        }

        private void cbMinimizeToTray_CheckedChanged(object sender, EventArgs e)
        {
            config.SetConfig("closetray", (CloseTray=this.cbMinimizeToTray.Checked).ToString());
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            config.SaveConfig();
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            config.ReloadConfig();
            GetCloseTray();
            SetStartConfig();
            this.Close();
        }

        private void ConfigForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.btnCancel_Click(this, null);
        }
    }
}
