using System;
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
                    //if (!NXTSerialPort.PlaySoundFile("hello.rso", false))
                    this.addMessage("NXT通讯失败！");
                else
                {
                    this.tabControl1.SelectedIndex = 2;
                    this.txtSKFocus.Focus();

                    this.btnReadBattery_Click(sender, e);
                    this.btnReadVersion_Click(sender, e);
                }
            }
        }

        private void btnClosePort_Click(object sender, EventArgs e)
        {
            if (NXTSerialPort.IsOpen())
            {
                NXTSerialPort.PlaySoundFile("goodbye.rso", false);
                this.addMessage("端口" + this.cbxPorts.Text + "关闭" + (NXTSerialPort.Close() ? "成功" : "失败") + "！");
            }
            else
                this.addMessage("端口" + this.cbxPorts.Text + "已关闭！");
        }
        #endregion

        #region Base Commands
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

        private void btnPlay_Click(object sender, EventArgs e)
        {
            this.addMessage(NXTSerialPort.PlaySoundFile(this.cbxSoundFileName.Text + ".rso", false).ToString());
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            this.addMessage(NXTSerialPort.Run(this.cbxProgramName.Text + ".rxe").ToString());
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            this.addMessage(NXTSerialPort.Stop().ToString());
        }
        #endregion

        #region Directions
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
            this.run(-70, 70);
        }

        private void btnRight_Click(object sender, EventArgs e)
        {
            this.run(70, -70);
        }

        /// <summary>
        /// Run Interval,ms
        /// </summary>
        private static float runInterval = 100;
        private static DateTime lastRunTime;
        private void run(int left, int right)
        {
            if (lastRunTime == default(DateTime) || (DateTime.Now - lastRunTime).TotalMilliseconds >= runInterval)
            {
                long nTacho = 50;
                NXTSerialPort.ResetPos(NXTSerialPort.Motor.B);
                NXTSerialPort.ResetPos(NXTSerialPort.Motor.C);
                bool isSucess = NXTSerialPort.Run(NXTSerialPort.Motor.B, right, NXTSerialPort.MotorMode.On, NXTSerialPort.MotorRegulation.Idle, 0, NXTSerialPort.MotorRunState.Run, nTacho)
                       && NXTSerialPort.Run(NXTSerialPort.Motor.C, left, NXTSerialPort.MotorMode.On, NXTSerialPort.MotorRegulation.Idle, 0, NXTSerialPort.MotorRunState.Run, nTacho);

                if (!isSucess)
                    this.addMessage("Run Error!");
                lastRunTime = DateTime.Now;
            }
        }

        private void btnRotateA_Click(object sender, EventArgs e)
        {
            this.rotateA(60);
        }
        private void rotateA(int i)
        {
            if (lastRunTime == default(DateTime) || (DateTime.Now - lastRunTime).TotalMilliseconds >= runInterval)
            {
                long nTacho = 50;
                NXTSerialPort.ResetPos(NXTSerialPort.Motor.A);
                bool isSucess = NXTSerialPort.Run(NXTSerialPort.Motor.A, i, NXTSerialPort.MotorMode.On, NXTSerialPort.MotorRegulation.Idle, 0, NXTSerialPort.MotorRunState.Run, nTacho);

                if (!isSucess)
                    this.addMessage("RotateA Error!");
                lastRunTime = DateTime.Now;
            }
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

            if (e.KeyCode == Keys.A)
                this.rotateA(-70);
            if (e.KeyCode == Keys.D)
                this.rotateA(70);

            Thread.Sleep(100);
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (NXTSerialPort.IsOpen())
            {
                NXTSerialPort.PlaySoundFile("Goodbye.rso", false);
                NXTSerialPort.Close();
            }
        }
        #endregion
    }
}
