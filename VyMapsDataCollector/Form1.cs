using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VyMapsDataCollector.Models;
using VyMapsDataCollector.Utils;
//using System.Runtime.InteropServices;

namespace VyMapsDataCollector
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        private async void btnStart_Click(object sender, EventArgs e)
        {
            //await Task.Run(() => Bll.BeginDataCollect());
            await Bll.BeginDataCollect();
        }

        void SetProxySettings()
        {
            ProxyUtils.ProxyList = rtbProxyList.Text;
            ProxyUtils.ForcedProxy = txtBoxForcedProxy.Text;
            ProxyUtils.TimeOutSeconds = Convert.ToInt32(txtBoxTimeout.Text);
        }

        private void btnSubmitProxy_Click(object sender, EventArgs e)
        {
            SetProxySettings();
            MessageBox.Show("Proxy Ayarları Kaydedildi !");
        }

        //[DllImport("kernel32.dll", SetLastError = true)]
        //[return: MarshalAs(UnmanagedType.Bool)]
        //static extern bool AllocConsole();
        private void Form1_Load(object sender, EventArgs e)
        {
            //AllocConsole();
            Control.CheckForIllegalCrossThreadCalls = false;
            SetProxySettings();
            Setting setting = Bll.CurrentSetting;
            if (setting != null)
            {
                if (!string.IsNullOrWhiteSpace(setting.LastCountryName))
                {
                    txtboxLastCountry.Text = setting.LastCountryName;
                }
                if (!string.IsNullOrWhiteSpace(setting.LastDistrictName))
                {
                    txtboxLastDistrict.Text = setting.LastDistrictName;
                }
                if (!string.IsNullOrWhiteSpace(setting.LastSectorName))
                {
                    txtboxLastSector.Text = setting.LastSectorName;
                }
                if (!string.IsNullOrWhiteSpace(setting.LastCompanyName))
                {
                    txtboxLastCompany.Text = setting.LastCompanyName;
                }
            }
            "Program Başlatıldı. Bu Pencereyi Kapatmayın İşlem Logları Burada Gözükecek...".WriteLog();

        }

        private void chckUseProxy_CheckedChanged(object sender, EventArgs e)
        {
            ProxyUtils.UseProxy = chckUseProxy.Checked;
        }

        private void btnStop_Click(object sender, EventArgs e)
        {

        }

        private void chckReadCountries_CheckedChanged(object sender, EventArgs e)
        {
            Bll.BypassCountries = !chckReadCountries.Checked;
        }

        private void chckReadDistricts_CheckedChanged(object sender, EventArgs e)
        {

            Bll.BypassDistricts = !chckReadDistricts.Checked;
        }

        private void chckReadSectors_CheckedChanged(object sender, EventArgs e)
        {

            Bll.BypassSectors = !chckReadSectors.Checked;
        }

        private void chckReadCompanies_CheckedChanged(object sender, EventArgs e)
        {

            Bll.BypassCompanies = !chckReadCompanies.Checked;
        }
        private void chckReadFromLastCountry_CheckedChanged(object sender, EventArgs e)
        {
            Bll.ResumeFromLast = chckReadFromLastCountry.Checked;
        }

        private void chckUseTorProxy_CheckedChanged(object sender, EventArgs e)
        {
            ProxyUtils.UseTorNetworkAsProxy = chckUseProxy.Checked;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Bll.ParallelLimit = Convert.ToInt32(txtBoxParallelLimit.Text);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Bll.CurrentSetting.LastCountryName = txtboxLastCountry.Text;
            Bll.CurrentSetting.LastDistrictName = txtboxLastDistrict.Text;
            Bll.CurrentSetting.LastSectorName = txtboxLastSector.Text;
            Bll.CurrentSetting.LastCountryName = txtboxLastCountry.Text;
        }

    }
}
