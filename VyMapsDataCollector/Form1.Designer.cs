namespace VyMapsDataCollector
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.txtboxLastCompany = new System.Windows.Forms.TextBox();
            this.txtboxLastSector = new System.Windows.Forms.TextBox();
            this.txtboxLastDistrict = new System.Windows.Forms.TextBox();
            this.txtboxLastCountry = new System.Windows.Forms.TextBox();
            this.chckReadCompanies = new System.Windows.Forms.CheckBox();
            this.chckReadSectors = new System.Windows.Forms.CheckBox();
            this.chckReadDistricts = new System.Windows.Forms.CheckBox();
            this.chckReadCountries = new System.Windows.Forms.CheckBox();
            this.chckReadFromLastCountry = new System.Windows.Forms.CheckBox();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lblAktiveTask = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.txtBoxParallelLimit = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chckUseTorProxy = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtBoxTimeout = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtBoxForcedProxy = new System.Windows.Forms.TextBox();
            this.rtbProxyList = new System.Windows.Forms.RichTextBox();
            this.chckUseProxy = new System.Windows.Forms.CheckBox();
            this.btnSubmitProxy = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(640, 507);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(632, 481);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Veri Topla";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.txtboxLastCompany);
            this.groupBox1.Controls.Add(this.txtboxLastSector);
            this.groupBox1.Controls.Add(this.txtboxLastDistrict);
            this.groupBox1.Controls.Add(this.txtboxLastCountry);
            this.groupBox1.Controls.Add(this.chckReadCompanies);
            this.groupBox1.Controls.Add(this.chckReadSectors);
            this.groupBox1.Controls.Add(this.chckReadDistricts);
            this.groupBox1.Controls.Add(this.chckReadCountries);
            this.groupBox1.Controls.Add(this.chckReadFromLastCountry);
            this.groupBox1.Controls.Add(this.btnStop);
            this.groupBox1.Controls.Add(this.btnStart);
            this.groupBox1.Location = new System.Drawing.Point(8, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(616, 191);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Kontrol Paneli";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 128);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(94, 13);
            this.label9.TabIndex = 22;
            this.label9.Text = "Son Kalınan Şirket";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 105);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(98, 13);
            this.label8.TabIndex = 21;
            this.label8.Text = "Son Kalınan Sektör";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 82);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(94, 13);
            this.label7.TabIndex = 20;
            this.label7.Text = "Son Kalınan Bölge";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 59);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(89, 13);
            this.label6.TabIndex = 19;
            this.label6.Text = "Son Kalınan Ülke";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(434, 151);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(176, 23);
            this.button2.TabIndex = 18;
            this.button2.Text = "Ayarları Kaydet";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // txtboxLastCompany
            // 
            this.txtboxLastCompany.Location = new System.Drawing.Point(110, 125);
            this.txtboxLastCompany.Name = "txtboxLastCompany";
            this.txtboxLastCompany.Size = new System.Drawing.Size(500, 20);
            this.txtboxLastCompany.TabIndex = 17;
            this.txtboxLastCompany.Text = "Son Kayda Ulaşılamadı";
            // 
            // txtboxLastSector
            // 
            this.txtboxLastSector.Location = new System.Drawing.Point(110, 102);
            this.txtboxLastSector.Name = "txtboxLastSector";
            this.txtboxLastSector.Size = new System.Drawing.Size(500, 20);
            this.txtboxLastSector.TabIndex = 16;
            this.txtboxLastSector.Text = "Son Kayda Ulaşılamadı";
            // 
            // txtboxLastDistrict
            // 
            this.txtboxLastDistrict.Location = new System.Drawing.Point(110, 79);
            this.txtboxLastDistrict.Name = "txtboxLastDistrict";
            this.txtboxLastDistrict.Size = new System.Drawing.Size(500, 20);
            this.txtboxLastDistrict.TabIndex = 15;
            this.txtboxLastDistrict.Text = "Son Kayda Ulaşılamadı";
            // 
            // txtboxLastCountry
            // 
            this.txtboxLastCountry.Location = new System.Drawing.Point(110, 56);
            this.txtboxLastCountry.Name = "txtboxLastCountry";
            this.txtboxLastCountry.Size = new System.Drawing.Size(500, 20);
            this.txtboxLastCountry.TabIndex = 14;
            this.txtboxLastCountry.Text = "Son Kayda Ulaşılamadı";
            // 
            // chckReadCompanies
            // 
            this.chckReadCompanies.AutoSize = true;
            this.chckReadCompanies.Checked = true;
            this.chckReadCompanies.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chckReadCompanies.Location = new System.Drawing.Point(509, 23);
            this.chckReadCompanies.Name = "chckReadCompanies";
            this.chckReadCompanies.Size = new System.Drawing.Size(87, 17);
            this.chckReadCompanies.TabIndex = 13;
            this.chckReadCompanies.Text = "Firmaları Oku";
            this.chckReadCompanies.UseVisualStyleBackColor = true;
            this.chckReadCompanies.CheckedChanged += new System.EventHandler(this.chckReadCompanies_CheckedChanged);
            // 
            // chckReadSectors
            // 
            this.chckReadSectors.AutoSize = true;
            this.chckReadSectors.Checked = true;
            this.chckReadSectors.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chckReadSectors.Location = new System.Drawing.Point(410, 23);
            this.chckReadSectors.Name = "chckReadSectors";
            this.chckReadSectors.Size = new System.Drawing.Size(93, 17);
            this.chckReadSectors.TabIndex = 12;
            this.chckReadSectors.Text = "Sektörleri Oku";
            this.chckReadSectors.UseVisualStyleBackColor = true;
            this.chckReadSectors.CheckedChanged += new System.EventHandler(this.chckReadSectors_CheckedChanged);
            // 
            // chckReadDistricts
            // 
            this.chckReadDistricts.AutoSize = true;
            this.chckReadDistricts.Checked = true;
            this.chckReadDistricts.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chckReadDistricts.Location = new System.Drawing.Point(315, 23);
            this.chckReadDistricts.Name = "chckReadDistricts";
            this.chckReadDistricts.Size = new System.Drawing.Size(89, 17);
            this.chckReadDistricts.TabIndex = 11;
            this.chckReadDistricts.Text = "Bölgeleri Oku";
            this.chckReadDistricts.UseVisualStyleBackColor = true;
            this.chckReadDistricts.CheckedChanged += new System.EventHandler(this.chckReadDistricts_CheckedChanged);
            // 
            // chckReadCountries
            // 
            this.chckReadCountries.AutoSize = true;
            this.chckReadCountries.Checked = true;
            this.chckReadCountries.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chckReadCountries.Location = new System.Drawing.Point(225, 23);
            this.chckReadCountries.Name = "chckReadCountries";
            this.chckReadCountries.Size = new System.Drawing.Size(84, 17);
            this.chckReadCountries.TabIndex = 10;
            this.chckReadCountries.Text = "Ülkeleri Oku";
            this.chckReadCountries.UseVisualStyleBackColor = true;
            this.chckReadCountries.CheckedChanged += new System.EventHandler(this.chckReadCountries_CheckedChanged);
            // 
            // chckReadFromLastCountry
            // 
            this.chckReadFromLastCountry.AutoSize = true;
            this.chckReadFromLastCountry.Checked = true;
            this.chckReadFromLastCountry.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chckReadFromLastCountry.Location = new System.Drawing.Point(110, 155);
            this.chckReadFromLastCountry.Name = "chckReadFromLastCountry";
            this.chckReadFromLastCountry.Size = new System.Drawing.Size(170, 17);
            this.chckReadFromLastCountry.TabIndex = 6;
            this.chckReadFromLastCountry.Text = "Son Kalınan Yerden Devam Et";
            this.chckReadFromLastCountry.UseVisualStyleBackColor = true;
            this.chckReadFromLastCountry.CheckedChanged += new System.EventHandler(this.chckReadFromLastCountry_CheckedChanged);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(84, 19);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(72, 23);
            this.btnStop.TabIndex = 5;
            this.btnStop.Text = "Duraklat";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(6, 19);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(72, 23);
            this.btnStart.TabIndex = 4;
            this.btnStart.Text = "Başlat";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.groupBox3);
            this.tabPage3.Controls.Add(this.groupBox2);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(632, 481);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Ayarlar";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lblAktiveTask);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.button1);
            this.groupBox3.Controls.Add(this.txtBoxParallelLimit);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Location = new System.Drawing.Point(297, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(327, 475);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Genel Ayarlar";
            // 
            // lblAktiveTask
            // 
            this.lblAktiveTask.AutoSize = true;
            this.lblAktiveTask.Location = new System.Drawing.Point(78, 44);
            this.lblAktiveTask.Name = "lblAktiveTask";
            this.lblAktiveTask.Size = new System.Drawing.Size(13, 13);
            this.lblAktiveTask.TabIndex = 8;
            this.lblAktiveTask.Text = "0";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(17, 43);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(61, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "Aktif İşlem :";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(10, 446);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(301, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "Ayarlarını Kaydet";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtBoxParallelLimit
            // 
            this.txtBoxParallelLimit.Location = new System.Drawing.Point(129, 21);
            this.txtBoxParallelLimit.Name = "txtBoxParallelLimit";
            this.txtBoxParallelLimit.Size = new System.Drawing.Size(174, 20);
            this.txtBoxParallelLimit.TabIndex = 1;
            this.txtBoxParallelLimit.Text = "10";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 23);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(123, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Paralel İşlem Kapasitesi: \r\n";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.chckUseTorProxy);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.txtBoxTimeout);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.txtBoxForcedProxy);
            this.groupBox2.Controls.Add(this.rtbProxyList);
            this.groupBox2.Controls.Add(this.chckUseProxy);
            this.groupBox2.Controls.Add(this.btnSubmitProxy);
            this.groupBox2.Location = new System.Drawing.Point(8, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(283, 475);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Proxy Ayarları";
            // 
            // chckUseTorProxy
            // 
            this.chckUseTorProxy.AutoSize = true;
            this.chckUseTorProxy.Checked = true;
            this.chckUseTorProxy.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chckUseTorProxy.Enabled = false;
            this.chckUseTorProxy.Location = new System.Drawing.Point(5, 422);
            this.chckUseTorProxy.Name = "chckUseTorProxy";
            this.chckUseTorProxy.Size = new System.Drawing.Size(117, 17);
            this.chckUseTorProxy.TabIndex = 5;
            this.chckUseTorProxy.Text = "Tor Network Kullan";
            this.chckUseTorProxy.UseVisualStyleBackColor = true;
            this.chckUseTorProxy.CheckedChanged += new System.EventHandler(this.chckUseTorProxy_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 346);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(119, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Proxy Zaman Aşımı (sn):";
            // 
            // txtBoxTimeout
            // 
            this.txtBoxTimeout.Location = new System.Drawing.Point(131, 343);
            this.txtBoxTimeout.Name = "txtBoxTimeout";
            this.txtBoxTimeout.Size = new System.Drawing.Size(146, 20);
            this.txtBoxTimeout.TabIndex = 1;
            this.txtBoxTimeout.Text = "5";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(106, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Proxy Listesi";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 372);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Bu Proxy\'i Kullan:";
            // 
            // txtBoxForcedProxy
            // 
            this.txtBoxForcedProxy.Location = new System.Drawing.Point(100, 369);
            this.txtBoxForcedProxy.Name = "txtBoxForcedProxy";
            this.txtBoxForcedProxy.Size = new System.Drawing.Size(177, 20);
            this.txtBoxForcedProxy.TabIndex = 3;
            // 
            // rtbProxyList
            // 
            this.rtbProxyList.Location = new System.Drawing.Point(7, 40);
            this.rtbProxyList.Name = "rtbProxyList";
            this.rtbProxyList.Size = new System.Drawing.Size(270, 297);
            this.rtbProxyList.TabIndex = 2;
            this.rtbProxyList.Text = resources.GetString("rtbProxyList.Text");
            // 
            // chckUseProxy
            // 
            this.chckUseProxy.AutoSize = true;
            this.chckUseProxy.Checked = true;
            this.chckUseProxy.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chckUseProxy.Location = new System.Drawing.Point(6, 399);
            this.chckUseProxy.Name = "chckUseProxy";
            this.chckUseProxy.Size = new System.Drawing.Size(84, 17);
            this.chckUseProxy.TabIndex = 1;
            this.chckUseProxy.Text = "Proxy Kullan";
            this.chckUseProxy.UseVisualStyleBackColor = true;
            this.chckUseProxy.CheckedChanged += new System.EventHandler(this.chckUseProxy_CheckedChanged);
            // 
            // btnSubmitProxy
            // 
            this.btnSubmitProxy.Location = new System.Drawing.Point(9, 446);
            this.btnSubmitProxy.Name = "btnSubmitProxy";
            this.btnSubmitProxy.Size = new System.Drawing.Size(268, 23);
            this.btnSubmitProxy.TabIndex = 0;
            this.btnSubmitProxy.Text = "Proxy Ayarlarını Kaydet";
            this.btnSubmitProxy.UseVisualStyleBackColor = true;
            this.btnSubmitProxy.Click += new System.EventHandler(this.btnSubmitProxy_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(640, 507);
            this.Controls.Add(this.tabControl1);
            this.Name = "Form1";
            this.Text = "VyMapsDataCollector";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox chckUseProxy;
        private System.Windows.Forms.Button btnSubmitProxy;
        private System.Windows.Forms.RichTextBox rtbProxyList;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtBoxForcedProxy;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtBoxTimeout;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtboxLastCompany;
        private System.Windows.Forms.TextBox txtboxLastSector;
        private System.Windows.Forms.TextBox txtboxLastDistrict;
        private System.Windows.Forms.CheckBox chckReadCompanies;
        private System.Windows.Forms.CheckBox chckReadSectors;
        private System.Windows.Forms.CheckBox chckReadDistricts;
        private System.Windows.Forms.CheckBox chckReadCountries;
        private System.Windows.Forms.TextBox txtboxLastCountry;
        private System.Windows.Forms.CheckBox chckReadFromLastCountry;
        private System.Windows.Forms.CheckBox chckUseTorProxy;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtBoxParallelLimit;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblAktiveTask;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
    }
}

