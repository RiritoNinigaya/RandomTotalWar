﻿namespace RandomTW
{
    partial class MainFrm
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.pbx1 = new System.Windows.Forms.PictureBox();
            this.OFDlg = new System.Windows.Forms.OpenFileDialog();
            this.SFDlg = new System.Windows.Forms.SaveFileDialog();
            this.btnGen = new System.Windows.Forms.Button();
            this.tbx_x = new System.Windows.Forms.TextBox();
            this.tbx_y = new System.Windows.Forms.TextBox();
            this.tbx_Regs = new System.Windows.Forms.TextBox();
            this.tbx_F = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tbx_rvr = new System.Windows.Forms.TextBox();
            this.tbx_1_r = new System.Windows.Forms.TextBox();
            this.tbxUnits = new System.Windows.Forms.TextBox();
            this.tbx_1_pw = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.chbx_hist = new System.Windows.Forms.CheckBox();
            this.chbx_New = new System.Windows.Forms.CheckBox();
            this.label12 = new System.Windows.Forms.Label();
            this.btn_CstmFacs = new System.Windows.Forms.Button();
            this.tbx_Name = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tbx_Dph = new System.Windows.Forms.TextBox();
            this.pbx2 = new System.Windows.Forms.PictureBox();
            this.tabMaps = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.pbx3 = new System.Windows.Forms.PictureBox();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.pbx4 = new System.Windows.Forms.PictureBox();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.pbx5 = new System.Windows.Forms.PictureBox();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.pbx6 = new System.Windows.Forms.PictureBox();
            this.tabPage7 = new System.Windows.Forms.TabPage();
            this.pbx7 = new System.Windows.Forms.PictureBox();
            this.sttStr = new System.Windows.Forms.StatusStrip();
            this.tlStrPrgrssBr = new System.Windows.Forms.ToolStripProgressBar();
            this.tlStrSttLbl = new System.Windows.Forms.ToolStripStatusLabel();
            ((System.ComponentModel.ISupportInitialize)(this.pbx1)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbx2)).BeginInit();
            this.tabMaps.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbx3)).BeginInit();
            this.tabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbx4)).BeginInit();
            this.tabPage6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbx5)).BeginInit();
            this.tabPage5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbx6)).BeginInit();
            this.tabPage7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbx7)).BeginInit();
            this.sttStr.SuspendLayout();
            this.SuspendLayout();
            // 
            // pbx1
            // 
            this.pbx1.BackColor = System.Drawing.Color.Transparent;
            this.pbx1.Location = new System.Drawing.Point(0, 0);
            this.pbx1.Name = "pbx1";
            this.pbx1.Size = new System.Drawing.Size(311, 248);
            this.pbx1.TabIndex = 0;
            this.pbx1.TabStop = false;
            // 
            // OFDlg
            // 
            this.OFDlg.DefaultExt = "tga";
            this.OFDlg.FileName = "openFileDialog1";
            this.OFDlg.InitialDirectory = "C:\\Users\\92710\\Desktop\\C#\\TGAappl\\TGAappl\\bin\\Debug";
            //this.OFDlg.FileOk += new System.ComponentModel.CancelEventHandler(this.OFDlg_FileOk);
            // 
            // SFDlg
            // 
            this.SFDlg.DefaultExt = "tga";
            this.SFDlg.InitialDirectory = "C:\\Users\\92710\\Desktop\\C#\\TGAappl\\TGAappl\\bin\\Debug";
            this.SFDlg.FileOk += new System.ComponentModel.CancelEventHandler(this.SFDlg_FileOk);
            // 
            // btnGen
            // 
            this.btnGen.Location = new System.Drawing.Point(499, 86);
            this.btnGen.Name = "btnGen";
            this.btnGen.Size = new System.Drawing.Size(88, 23);
            this.btnGen.TabIndex = 3;
            this.btnGen.Text = "OK";
            this.btnGen.UseVisualStyleBackColor = true;
            this.btnGen.Click += new System.EventHandler(this.btnGen_Click);
            // 
            // tbx_x
            // 
            this.tbx_x.Location = new System.Drawing.Point(55, 21);
            this.tbx_x.Name = "tbx_x";
            this.tbx_x.Size = new System.Drawing.Size(48, 20);
            this.tbx_x.TabIndex = 4;
            this.tbx_x.Text = "200";
            // 
            // tbx_y
            // 
            this.tbx_y.Location = new System.Drawing.Point(55, 43);
            this.tbx_y.Name = "tbx_y";
            this.tbx_y.Size = new System.Drawing.Size(48, 20);
            this.tbx_y.TabIndex = 4;
            this.tbx_y.Text = "150";
            // 
            // tbx_Regs
            // 
            this.tbx_Regs.Location = new System.Drawing.Point(201, 21);
            this.tbx_Regs.Name = "tbx_Regs";
            this.tbx_Regs.Size = new System.Drawing.Size(37, 20);
            this.tbx_Regs.TabIndex = 5;
            this.tbx_Regs.Text = "100";
            // 
            // tbx_F
            // 
            this.tbx_F.Location = new System.Drawing.Point(201, 43);
            this.tbx_F.Name = "tbx_F";
            this.tbx_F.Size = new System.Drawing.Size(37, 20);
            this.tbx_F.TabIndex = 5;
            this.tbx_F.Text = "19";
            this.tbx_F.TextChanged += new System.EventHandler(this.tbx_F_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Width";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Height";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.tbx_rvr);
            this.groupBox1.Controls.Add(this.tbx_1_r);
            this.groupBox1.Controls.Add(this.tbxUnits);
            this.groupBox1.Controls.Add(this.tbx_1_pw);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.chbx_hist);
            this.groupBox1.Controls.Add(this.chbx_New);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.btn_CstmFacs);
            this.groupBox1.Controls.Add(this.tbx_Name);
            this.groupBox1.Controls.Add(this.btnGen);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.tbx_x);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.tbx_Dph);
            this.groupBox1.Controls.Add(this.tbx_y);
            this.groupBox1.Controls.Add(this.tbx_Regs);
            this.groupBox1.Controls.Add(this.tbx_F);
            this.groupBox1.Location = new System.Drawing.Point(30, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(606, 126);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            // 
            // tbx_rvr
            // 
            this.tbx_rvr.Location = new System.Drawing.Point(488, 46);
            this.tbx_rvr.Name = "tbx_rvr";
            this.tbx_rvr.Size = new System.Drawing.Size(48, 20);
            this.tbx_rvr.TabIndex = 4;
            this.tbx_rvr.Text = "50";
            // 
            // tbx_1_r
            // 
            this.tbx_1_r.Location = new System.Drawing.Point(488, 25);
            this.tbx_1_r.Name = "tbx_1_r";
            this.tbx_1_r.Size = new System.Drawing.Size(48, 20);
            this.tbx_1_r.TabIndex = 4;
            this.tbx_1_r.Text = "30";
            // 
            // tbxUnits
            // 
            this.tbxUnits.Location = new System.Drawing.Point(144, 90);
            this.tbxUnits.Name = "tbxUnits";
            this.tbxUnits.Size = new System.Drawing.Size(144, 20);
            this.tbxUnits.TabIndex = 15;
            this.tbxUnits.Text = "Rnd_RTW_Aurun";
            // 
            // tbx_1_pw
            // 
            this.tbx_1_pw.Location = new System.Drawing.Point(350, 46);
            this.tbx_1_pw.Name = "tbx_1_pw";
            this.tbx_1_pw.Size = new System.Drawing.Size(48, 20);
            this.tbx_1_pw.TabIndex = 4;
            this.tbx_1_pw.Text = "2,0";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(416, 50);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(57, 13);
            this.label6.TabIndex = 6;
            this.label6.Text = "rivers(max)";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(416, 28);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(72, 13);
            this.label13.TabIndex = 6;
            this.label13.Text = "smooth radius";
            // 
            // chbx_hist
            // 
            this.chbx_hist.AutoSize = true;
            this.chbx_hist.Checked = true;
            this.chbx_hist.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbx_hist.Location = new System.Drawing.Point(21, 69);
            this.chbx_hist.Name = "chbx_hist";
            this.chbx_hist.Size = new System.Drawing.Size(117, 17);
            this.chbx_hist.TabIndex = 14;
            this.chbx_hist.Text = "native start position";
            this.chbx_hist.UseVisualStyleBackColor = true;
            // 
            // chbx_New
            // 
            this.chbx_New.AutoSize = true;
            this.chbx_New.Checked = true;
            this.chbx_New.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbx_New.Location = new System.Drawing.Point(22, 92);
            this.chbx_New.Name = "chbx_New";
            this.chbx_New.Size = new System.Drawing.Size(116, 17);
            this.chbx_New.TabIndex = 13;
            this.chbx_New.Text = "generate new units";
            this.chbx_New.UseVisualStyleBackColor = true;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(304, 50);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(41, 13);
            this.label12.TabIndex = 6;
            this.label12.Text = "erosion";
            // 
            // btn_CstmFacs
            // 
            this.btn_CstmFacs.Location = new System.Drawing.Point(244, 40);
            this.btn_CstmFacs.Name = "btn_CstmFacs";
            this.btn_CstmFacs.Size = new System.Drawing.Size(24, 23);
            this.btn_CstmFacs.TabIndex = 12;
            this.btn_CstmFacs.Text = "...";
            this.btn_CstmFacs.UseVisualStyleBackColor = true;
            this.btn_CstmFacs.Click += new System.EventHandler(this.btn_CstmFacs_Click);
            // 
            // tbx_Name
            // 
            this.tbx_Name.Location = new System.Drawing.Point(364, 89);
            this.tbx_Name.Name = "tbx_Name";
            this.tbx_Name.Size = new System.Drawing.Size(136, 20);
            this.tbx_Name.TabIndex = 8;
            this.tbx_Name.Text = "Rnd_mod_1";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(311, 28);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(34, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "depth";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(109, 46);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(74, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Factions(2-19)";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(109, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(93, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Provinces(3 - 199)";
            // 
            // tbx_Dph
            // 
            this.tbx_Dph.Location = new System.Drawing.Point(350, 22);
            this.tbx_Dph.Name = "tbx_Dph";
            this.tbx_Dph.Size = new System.Drawing.Size(48, 20);
            this.tbx_Dph.TabIndex = 4;
            this.tbx_Dph.Text = "127";
            // 
            // pbx2
            // 
            this.pbx2.BackColor = System.Drawing.Color.Transparent;
            this.pbx2.Location = new System.Drawing.Point(0, 0);
            this.pbx2.Name = "pbx2";
            this.pbx2.Size = new System.Drawing.Size(276, 248);
            this.pbx2.TabIndex = 0;
            this.pbx2.TabStop = false;
            // 
            // tabMaps
            // 
            this.tabMaps.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabMaps.Controls.Add(this.tabPage1);
            this.tabMaps.Controls.Add(this.tabPage2);
            this.tabMaps.Controls.Add(this.tabPage3);
            this.tabMaps.Controls.Add(this.tabPage4);
            this.tabMaps.Controls.Add(this.tabPage6);
            this.tabMaps.Controls.Add(this.tabPage5);
            this.tabMaps.Controls.Add(this.tabPage7);
            this.tabMaps.Location = new System.Drawing.Point(30, 144);
            this.tabMaps.Name = "tabMaps";
            this.tabMaps.SelectedIndex = 0;
            this.tabMaps.Size = new System.Drawing.Size(427, 279);
            this.tabMaps.TabIndex = 8;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.pbx1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(419, 253);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "heights";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.pbx2);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(419, 253);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "ground_types";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.pbx3);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(419, 253);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "climates";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // pbx3
            // 
            this.pbx3.BackColor = System.Drawing.Color.Transparent;
            this.pbx3.Location = new System.Drawing.Point(0, 0);
            this.pbx3.Name = "pbx3";
            this.pbx3.Size = new System.Drawing.Size(311, 248);
            this.pbx3.TabIndex = 0;
            this.pbx3.TabStop = false;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.pbx4);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(419, 253);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "regions";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // pbx4
            // 
            this.pbx4.BackColor = System.Drawing.Color.Transparent;
            this.pbx4.Location = new System.Drawing.Point(0, 0);
            this.pbx4.Name = "pbx4";
            this.pbx4.Size = new System.Drawing.Size(315, 248);
            this.pbx4.TabIndex = 0;
            this.pbx4.TabStop = false;
            // 
            // tabPage6
            // 
            this.tabPage6.Controls.Add(this.pbx5);
            this.tabPage6.Location = new System.Drawing.Point(4, 22);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Size = new System.Drawing.Size(419, 253);
            this.tabPage6.TabIndex = 5;
            this.tabPage6.Text = "features";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // pbx5
            // 
            this.pbx5.BackColor = System.Drawing.Color.Transparent;
            this.pbx5.Location = new System.Drawing.Point(0, 0);
            this.pbx5.Name = "pbx5";
            this.pbx5.Size = new System.Drawing.Size(311, 248);
            this.pbx5.TabIndex = 0;
            this.pbx5.TabStop = false;
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.pbx6);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Size = new System.Drawing.Size(419, 253);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "temperatures";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // pbx6
            // 
            this.pbx6.BackColor = System.Drawing.Color.Transparent;
            this.pbx6.Location = new System.Drawing.Point(0, 0);
            this.pbx6.Name = "pbx6";
            this.pbx6.Size = new System.Drawing.Size(311, 248);
            this.pbx6.TabIndex = 0;
            this.pbx6.TabStop = false;
            // 
            // tabPage7
            // 
            this.tabPage7.Controls.Add(this.pbx7);
            this.tabPage7.Location = new System.Drawing.Point(4, 22);
            this.tabPage7.Name = "tabPage7";
            this.tabPage7.Size = new System.Drawing.Size(419, 253);
            this.tabPage7.TabIndex = 6;
            this.tabPage7.Text = "rainfall";
            this.tabPage7.UseVisualStyleBackColor = true;
            // 
            // pbx7
            // 
            this.pbx7.BackColor = System.Drawing.Color.Transparent;
            this.pbx7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbx7.Location = new System.Drawing.Point(0, 0);
            this.pbx7.Name = "pbx7";
            this.pbx7.Size = new System.Drawing.Size(311, 248);
            this.pbx7.TabIndex = 0;
            this.pbx7.TabStop = false;
            // 
            // sttStr
            // 
            this.sttStr.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tlStrPrgrssBr,
            this.tlStrSttLbl});
            this.sttStr.Location = new System.Drawing.Point(0, 465);
            this.sttStr.Name = "sttStr";
            this.sttStr.Size = new System.Drawing.Size(663, 22);
            this.sttStr.TabIndex = 9;
            this.sttStr.Text = "statusStrip1";
            // 
            // tlStrPrgrssBr
            // 
            this.tlStrPrgrssBr.AutoSize = false;
            this.tlStrPrgrssBr.Name = "tlStrPrgrssBr";
            this.tlStrPrgrssBr.Size = new System.Drawing.Size(500, 16);
            // 
            // tlStrSttLbl
            // 
            this.tlStrSttLbl.Name = "tlStrSttLbl";
            this.tlStrSttLbl.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.tlStrSttLbl.Size = new System.Drawing.Size(146, 17);
            this.tlStrSttLbl.Spring = true;
            this.tlStrSttLbl.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.tlStrSttLbl.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.tlStrSttLbl.ToolTipText = "ololo";
            // 
            // MainFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(663, 487);
            this.Controls.Add(this.sttStr);
            this.Controls.Add(this.tabMaps);
            this.Controls.Add(this.groupBox1);
            this.Name = "MainFrm";
            this.Text = "Random Total War";
            this.Load += new System.EventHandler(this.MainFrm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbx1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbx2)).EndInit();
            this.tabMaps.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbx3)).EndInit();
            this.tabPage4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbx4)).EndInit();
            this.tabPage6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbx5)).EndInit();
            this.tabPage5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbx6)).EndInit();
            this.tabPage7.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbx7)).EndInit();
            this.sttStr.ResumeLayout(false);
            this.sttStr.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbx1;
        private System.Windows.Forms.OpenFileDialog OFDlg;
        private System.Windows.Forms.SaveFileDialog SFDlg;
        private System.Windows.Forms.Button btnGen;
        private System.Windows.Forms.TextBox tbx_x;
        private System.Windows.Forms.TextBox tbx_y;
        private System.Windows.Forms.TextBox tbx_Regs;
        private System.Windows.Forms.TextBox tbx_F;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox pbx2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbx_Dph;
        private System.Windows.Forms.TextBox tbx_Name;
        private System.Windows.Forms.TextBox tbx_1_pw;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox tbx_1_r;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button btn_CstmFacs;
        private System.Windows.Forms.TabControl tabMaps;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.PictureBox pbx3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.PictureBox pbx4;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.TabPage tabPage6;
        private System.Windows.Forms.PictureBox pbx5;
        private System.Windows.Forms.CheckBox chbx_New;
        private System.Windows.Forms.PictureBox pbx6;
        private System.Windows.Forms.TabPage tabPage7;
        private System.Windows.Forms.PictureBox pbx7;
        private System.Windows.Forms.CheckBox chbx_hist;
        private System.Windows.Forms.TextBox tbxUnits;
        private System.Windows.Forms.StatusStrip sttStr;
        private System.Windows.Forms.ToolStripProgressBar tlStrPrgrssBr;
        public System.Windows.Forms.ToolStripStatusLabel tlStrSttLbl;
        private System.Windows.Forms.TextBox tbx_rvr;
        private System.Windows.Forms.Label label6;
    }
}

