﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace LegoNXTController
{
    public partial class FormMain : Form
    {
        #region Methods
        public FormMain()
        {
            InitializeComponent();
        }

        private void addMessage(string message)
        {
            this.lbxOutput.Items.Add(message);
        }
        #endregion

        #region Ports
        private void btnOpenPort_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.cbxPorts.Text) || NXTSerialPort.IsOpen())
                return;

            bool b = NXTSerialPort.Open(this.cbxPorts.Text);
            this.addMessage("端口" + this.cbxPorts.Text + "打开" + (b ? "成功" : "失败") + "！");
            if (b)
            {
                if (!NXTSerialPort.Beep(400, 200))
                    this.addMessage("NXT通讯失败！");
                else
                {
                    this.btnReadBattery_Click(sender, e);
                    this.btnReadVersion_Click(sender, e);
                }
            }
        }

        private void btnClosePort_Click(object sender, EventArgs e)
        {
            if (NXTSerialPort.IsOpen())
                this.addMessage("端口" + this.cbxPorts.Text + "关闭" + (NXTSerialPort.Close() ? "成功" : "失败") + "！");
            else
                this.addMessage("端口" + this.cbxPorts.Text + "已关闭！");
        }
        #endregion

        #region Base Datas
        private void btnReadBattery_Click(object sender, EventArgs e)
        {
            double nMilliVolts = NXTSerialPort.ReadBatteryVoltage();
            if (nMilliVolts == -1)
                this.addMessage("电量读取失败！");
            else
                this.addMessage("电量：" + (nMilliVolts / 1000).ToString("0.00") + " volts");
        }

        private void btnReadVersion_Click(object sender, EventArgs e)
        {
            string firmwareVersion;
            string protocolVersion;
            if (NXTSerialPort.GetVersion(out firmwareVersion, out protocolVersion))
                this.addMessage("固件版本： " + firmwareVersion + "，协议版本： " + protocolVersion);
            else
                this.addMessage("版本信息读取失败！");
        }
        #endregion

        #region Control
        private void btnForwards_Click(object sender, EventArgs e)
        {
            this.run(70, 70);
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.run(-70, -70);
        }

        private void btnLeft_Click(object sender, EventArgs e)
        {
            this.run(70, -70);
        }

        private void btnRight_Click(object sender, EventArgs e)
        {
            this.run(-70, 70);
        }

        private void run(int b, int c)
        {
            long nTacho = 50;
            NXTSerialPort.ResetPos(NXTSerialPort.Motor.B);
            NXTSerialPort.ResetPos(NXTSerialPort.Motor.C);
            bool isSucess = NXTSerialPort.Run(NXTSerialPort.Motor.B, b, NXTSerialPort.MotorMode.On, NXTSerialPort.MotorRegulation.Idle, 0, NXTSerialPort.MotorRunState.Run, nTacho)
                   && NXTSerialPort.Run(NXTSerialPort.Motor.C, c, NXTSerialPort.MotorMode.On, NXTSerialPort.MotorRegulation.Idle, 0, NXTSerialPort.MotorRunState.Run, nTacho);

            if (!isSucess)
                this.addMessage("Run Error!");
        }
        #endregion

        #region Form Events
        private void FormMain_Load(object sender, EventArgs e)
        {
            this.btnOpenPort_Click(sender, e);
        }

        private void txtSKFocus_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
                this.btnLeft_Click(sender, e);
            if (e.KeyCode == Keys.Right)
                this.btnRight_Click(sender, e);
            if (e.KeyCode == Keys.Up)
                this.btnForwards_Click(sender, e);
            if (e.KeyCode == Keys.Down)
                this.btnBack_Click(sender, e);
            if (e.KeyCode == Keys.Space)
                NXTSerialPort.Beep(400, 200);

            Thread.Sleep(100);
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (NXTSerialPort.IsOpen())
                NXTSerialPort.Close();
        }
        #endregion
    }
}