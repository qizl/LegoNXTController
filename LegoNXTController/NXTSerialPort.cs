﻿using System.Text;
using System.IO.Ports;
using System.Threading;

namespace LegoNXTController
{
    public class NxtTimeout : System.Exception { }
    public class InvalidResult : System.Exception { }
    public class TxTimeout : System.Exception { }
    public class SerialError : System.Exception { }
    public class NotOpen : System.Exception { }
    public class CloseError : System.Exception { }
    public class RxError : System.Exception { }

    public static class NXTSerialPort
    {
        static SerialPort _port = new SerialPort();

        static byte nCommand = 0;
        static byte[] result = null;
        static byte nError = 0;
        static ManualResetEvent mre = new ManualResetEvent(false);

        public static bool Open(string Port)
        {
            bool b = true;

            try
            {
                if (_port.IsOpen)
                    _port.Close();

                _port.PortName = Port;
                _port.WriteTimeout = 500;
                _port.ReadTimeout = 500;
                _port.Encoding = UTF8Encoding.UTF8;
                _port.Open();
                _port.DataReceived += new SerialDataReceivedEventHandler(MyPort_DataReceived);
                _port.ErrorReceived += new SerialErrorReceivedEventHandler(MyPort_ErrorReceived);
            }
            catch
            {
                b = false;
            }

            return b;
        }

        public static bool Close()
        {
            bool b = true;
            try
            {
                if (_port.IsOpen)
                {
                    _port.ErrorReceived -= new SerialErrorReceivedEventHandler(MyPort_ErrorReceived);
                    _port.DataReceived -= new SerialDataReceivedEventHandler(MyPort_DataReceived);
                    _port.Close();
                }
            }
            catch
            {
                b = false;
            }
            return b;
        }

        public static bool IsOpen()
        {
            return _port.IsOpen;
        }

        public static bool Send(byte[] data, int nReply)
        {
            bool b = true;

            if (!_port.IsOpen)
                throw new NotOpen();

            if (_port.BytesToRead > 0)
                Flush();

            if (_port.BytesToWrite > 0)
                throw new TxTimeout();

            try
            {
                if (nReply > 0)
                    _port.ReceivedBytesThreshold = nReply + 2;
                else
                    _port.ReceivedBytesThreshold = 1;

                byte[] BluetoothHeader = new byte[2];
                byte nLen = (byte)(data.Length & 0x3f);  //<= 64 byte packet
                BluetoothHeader[0] = nLen;
                BluetoothHeader[1] = 0;
                _port.Write(BluetoothHeader, 0, BluetoothHeader.Length);

                _port.Write(data, 0, nLen);

                nCommand = data[1];
            }
            catch
            {
                b = false;
            }

            return b;
        }

        public static bool Send(byte[] data)
        {
            return Send(data, 0);
        }

        #region string ToString(err)

        private static string ToString(byte err)
        {
            switch (err)
            {
                case 0x00:
                    return "Success";
                case 0x81:
                    return "No more handles";
                case 0x82:
                    return "No space";
                case 0x83:
                    return "No more files";
                case 0x84:
                    return "End of file expected";
                case 0x85:
                    return "End of file";
                case 0x86:
                    return "Not linear file";
                case 0x87:
                    return "File not found";
                case 0x88:
                    return "Handle all ready closed";
                case 0x89:
                    return "No linear space";
                case 0x8a:
                    return "Undefined error";
                case 0x8b:
                    return "File is busy";
                case 0x8c:
                    return "No write buffers";
                case 0x8d:
                    return "Append not possible";
                case 0x8e:
                    return "File is full";
                case 0x8f:
                    return "File exists";
                case 0x90:
                    return "Module not found";
                case 0x91:
                    return "Out of boundary";
                case 0x92:
                    return "Illegal file name";
                case 0x93:
                    return "Illegal handle";
                case 221:
                    return "low speed protocol";
                case 236:
                    return "Not running";
                case 0xfb:
                    return "data received error: packet too small";
                case 0xfc:
                    return "data received error: bad wrapper size";
                case 0xfd:
                    return "data received error: command mis-match";
                case 0xfe:
                    return "data received error: not a reply";
                case 0xff:
                    return "data received error";
                default:
                    return "unknown";
            }
        }

        #endregion //ToString(err)

        private static void MyPort_ErrorReceived(object sender, SerialErrorReceivedEventArgs e)
        {
            throw new SerialError();
        }

        private static void Flush()
        {
            if (_port.BytesToRead > 0)
            {
                byte[] b = new byte[_port.BytesToRead];
                _port.Read(b, 0, _port.BytesToRead);
            }
        }

        private static void MyPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            nError = 0xff;

            try
            {
                if (e.EventType == SerialData.Chars)
                {
                    if (_port.BytesToRead == 0)
                        return;

                    if (_port.BytesToRead < 4)
                    {
                        nError = 0xfb;
                        Flush();
                    }
                    else
                    {
                        byte[] BluetoothHeader = new byte[2];
                        _port.Read(BluetoothHeader, 0, 2);

                        if ((BluetoothHeader[0] + 256 * BluetoothHeader[1]) == _port.BytesToRead)
                        {
                            result = new byte[_port.BytesToRead];
                            _port.Read(result, 0, _port.BytesToRead);

                            if (result[2] != 0)
                                nError = result[2];
                            else if (result[0] != 0x02)
                                nError = 0xfe;
                            else if (result[1] != nCommand)
                                nError = 0xfd;
                            else
                                nError = 0;
                        }
                        else
                            nError = 0xfc;
                    }
                }

                mre.Set();
            }
            catch
            {
                throw new RxError();
            }
        }

        private static int Buffer2Word(byte[] buffer, byte nIndex)
        {
            return buffer[nIndex] + 256 * buffer[nIndex + 1];
        }

        private static long Buffer2Long(byte[] buffer, byte nIndex)
        {
            long n = 0;
            for (int i = 0; i < 4; i++)
                n += (1 << i * 8) * (long)buffer[nIndex++];

            return n;
        }

        private static long Buffer2SignedLong(byte[] buffer, byte nIndex)
        {
            long n = Buffer2Long(buffer, nIndex);
            if (n < 0x8000)
                return n;
            else
                return n - 0x100000000;
        }


        #region file handling commands

        private static void DeleteFile(string filename)
        {
            byte[] data = new byte[21];

            data[0] = 0x01;
            data[1] = 0x85;
            for (int i = 2; i < 21; i++)
                data[i] = 0;
            char[] ca = filename.ToCharArray();
            for (int i = 0; i < filename.Length; i++)
                data[i + 2] = (byte)ca[i];

            Send(data, 23);
        }

        private static void OpenWrite(string filename)
        {
            byte[] data = new byte[26];

            data[0] = 0x01;
            data[1] = 0x81;
            for (int i = 2; i < 21; i++)
                data[i] = 0;
            char[] ca = filename.ToCharArray();
            for (int i = 0; i < filename.Length; i++)
                data[i + 2] = (byte)ca[i];

            data[22] = 13;
            data[23] = 0;
            data[24] = 0;
            data[25] = 0;

            Send(data, 4);
        }

        private static void WriteOnBrickFile(byte nHandle, byte b1, byte b2, byte b3, byte b4, byte b5)
        {
            byte[] data = new byte[16];

            data[0] = 0x01;
            data[1] = 0x83;
            data[2] = nHandle;

            data[3] = 0x08;
            data[4] = 0x00;
            data[5] = 0x00;
            data[6] = 0x05;
            data[7] = 0x05;
            data[8] = 0x00;
            data[9] = 0x00;
            data[10] = 0x00;

            data[11] = b1;
            data[12] = b2;
            data[13] = b3;
            data[14] = b4;
            data[15] = b5;

            Send(data, 6);
        }

        private static void CloseFile(byte nHandle)
        {
            byte[] data = new byte[3];

            data[0] = 0x01;
            data[1] = 0x84;
            data[2] = nHandle;

            Send(data, 4);
        }

        public static void RunFile(string filename)
        {
            byte[] data = new byte[22];

            data[0] = 0x00;
            data[1] = 0x00;
            for (int i = 2; i < 21; i++)
                data[i] = 0;
            char[] ca = filename.ToCharArray();
            for (int i = 0; i < filename.Length; i++)
                data[i + 2] = (byte)ca[i];

            Send(data, 3);
        }

        public static void DownloadOnBrickFile(string filename, byte b1, byte b2, byte b3, byte b4, byte b5)
        {
            mre.Reset();
            DeleteFile(filename);
            if (!mre.WaitOne(1000, false))
                throw new NxtTimeout();

            mre.Reset();
            OpenWrite(filename);
            if (!mre.WaitOne(1000, false))
                throw new NxtTimeout();

            if (nError > 0)
                throw new InvalidResult();

            byte nFileHandle = result[3];

            mre.Reset();
            WriteOnBrickFile(nFileHandle, b1, b2, b3, b4, b5);
            if (!mre.WaitOne(1000, false))
                throw new NxtTimeout();

            if (nError > 0)
                throw new InvalidResult();

            mre.Reset();
            CloseFile(nFileHandle);
            if (!mre.WaitOne(1000, false))
                throw new NxtTimeout();

            if ((nError > 0) || (nFileHandle != result[3]))
                throw new InvalidResult();
        }

        public static void Run(string filename)
        {
            mre.Reset();
            RunFile(filename);
            if (!mre.WaitOne(1000, false))
                throw new NxtTimeout();
        }

        public static void Stop()
        {
            byte[] data = new byte[2];

            data[0] = 0x00;
            data[1] = 0x01;

            mre.Reset();
            Send(data, 3);
            if (!mre.WaitOne(1000, false))
                throw new NxtTimeout();
        }

        #endregion //file handling commands


        #region motor/output commands

        public enum Motor { A = 0, B = 1, C = 2, All = 0xff };
        public enum MotorMode
        {
            On = 0x01, Brake = 0x02, /* OnBrake = 0x03, */ Regulated = 0x04
        };
        public enum MotorRegulation { Idle = 0, Speed = 1, Sync = 2 };
        public enum MotorRunState { Idle = 0, RampUp = 0x10, Run = 0x20, RampDown = 0x40 };

        public static bool Run(Motor nMotor, int nPower, MotorMode nMode, MotorRegulation nReg, int nTurnRatio, MotorRunState nRunState, long nTacho)
        {
            byte[] data = new byte[12];

            data[0] = 0x80;
            data[1] = 0x04;
            data[2] = (byte)nMotor;
            data[3] = (byte)nPower;
            data[4] = (byte)nMode;
            data[5] = (byte)nReg;
            data[6] = (byte)nTurnRatio;
            data[7] = (byte)nRunState;

            data[8] = (byte)(nTacho & 0xff);
            data[9] = (byte)(nTacho >> 8);
            data[10] = (byte)(nTacho >> 16);
            data[11] = (byte)(nTacho >> 24);

            return Send(data);
        }

        public static void GetOutputState(Motor nMotor)
        {
            byte[] data = new byte[3];

            data[0] = 0x00;
            data[1] = 0x06;
            data[2] = (byte)nMotor;

            Send(data, 25);
        }

        public static void ResetPos(Motor nMotor)
        {
            byte[] data = new byte[4];

            data[0] = 0x80;
            data[1] = 0x0a;
            data[2] = (byte)nMotor;
            data[3] = 0;

            Send(data);
        }

        public static void GetMotorState(Motor port, out byte power, out MotorMode mode, out MotorRegulation regulation, out byte turnRatio, out MotorRunState state, out long tachoLimit, out long tachoCount, out long blockTachoCount, out long rotationCount)
        {
            mre.Reset();
            GetOutputState(port);
            if (!mre.WaitOne(1000, false))
                throw new NxtTimeout();

            if (nError > 0)
                throw new InvalidResult();

            power = result[4];
            mode = (MotorMode)result[5];
            regulation = (MotorRegulation)result[6];
            turnRatio = result[7];
            state = (MotorRunState)result[8];
            tachoLimit = Buffer2Long(result, 9);
            tachoCount = Buffer2SignedLong(result, 13);
            blockTachoCount = Buffer2SignedLong(result, 17);
            rotationCount = Buffer2SignedLong(result, 21);
        }

        public static MotorRunState GetMotorRunState(Motor port)
        {
            mre.Reset();
            GetOutputState(port);
            if (!mre.WaitOne(1000, false))
                throw new NxtTimeout();

            if (nError > 0)
                throw new InvalidResult();

            return (MotorRunState)result[8];
        }

        public static bool IsMotorRunning(Motor port)
        {
            return (GetMotorRunState(port) != MotorRunState.Idle);
        }

        public static long ReadMotorRotationCount(Motor port)
        {
            mre.Reset();
            GetOutputState(port);
            if (!mre.WaitOne(1000, false))
                throw new NxtTimeout();

            if (nError > 0)
                throw new InvalidResult();

            return Buffer2SignedLong(result, 21);
        }

        #endregion //motor/output commands


        #region sensor/input commands

        public enum SensorPort { Port1 = 0, Port2 = 1, Port3 = 2, Port4 = 3 };

        public enum SensorType
        {
            None = 0, Switch = 1, Temperature = 2, Reflection = 3, Angle = 4, LightActive = 5, LightInactive = 6,
            SoundDB = 7, SoundDBA = 8, Custom = 9, LowSpeed = 10, LowSpeed9V = 11
        };

        public enum SensorMode
        {
            Raw = 0, Boolean = 0x20, Transitions = 0x40, Periods = 0x60, FullScale = 0x80,
            Celsius = 0xa0, Fahrenheit = 0xc0, Angle = 0xe0
        };

        public static void SetInputMode(SensorPort nPort, SensorType nType, SensorMode nMode)
        {
            byte[] data = new byte[5];

            data[0] = 0x80;
            data[1] = 0x05;
            data[2] = (byte)nPort;
            data[3] = (byte)nType;
            data[4] = (byte)nMode;

            Send(data);
        }

        public static void GetInputState(SensorPort nPort)
        {
            byte[] data = new byte[3];

            data[0] = 0x00;
            data[1] = 0x07;
            data[2] = (byte)nPort;

            Send(data, 16);
        }

        public static void ResetInput(SensorPort nPort)
        {
            byte[] data = new byte[3];

            data[0] = 0x80;
            data[1] = 0x08;
            data[2] = (byte)nPort;

            Send(data);
        }

        public static void GetLowSpeedStatus(SensorPort nPort)
        {
            byte[] data = new byte[3];

            data[0] = 0x00;
            data[1] = 0x0e;
            data[2] = (byte)nPort;

            Send(data, 4);
        }

        public static void LowSpeedWrite(SensorPort nPort, byte b1, byte b2, byte nRx)
        {
            byte[] data = new byte[7];

            data[0] = 0x80;
            data[1] = 0x0f;
            data[2] = (byte)nPort;
            data[3] = 2;
            data[4] = nRx;
            data[5] = b1;
            data[6] = b2;

            Send(data);
        }

        public static void LowSpeedWrite(SensorPort nPort, byte b1, byte b2, byte b3, byte nRx)
        {
            byte[] data = new byte[8];

            data[0] = 0x80;
            data[1] = 0x0f;
            data[2] = (byte)nPort;
            data[3] = 3;
            data[4] = nRx;
            data[5] = b1;
            data[6] = b2;
            data[7] = b3;

            Send(data);
        }

        public static void LowSpeedRead(SensorPort nPort)
        {
            byte[] data = new byte[3];

            data[0] = 0x00;
            data[1] = 0x10;
            data[2] = (byte)nPort;

            Send(data, 20);
        }

        private static void GetBatteryVoltage()
        {
            byte[] data = new byte[2];

            data[0] = 0x00;
            data[1] = 0x0b;

            Send(data, 5);
        }

        public static int ReadBatteryVoltage()
        {
            mre.Reset();
            GetBatteryVoltage();
            if (!mre.WaitOne(1000, false))
                return -1;

            if ((nError > 0) || (result.Length != 5) || (result[1] != 0x0b))
                return -1;

            return result[3] + 256 * result[4];
        }

        public static byte ReadUltrasonicSensor(SensorPort nPort)
        {
            LowSpeedWrite(nPort, 0x02, 0x42, 1);
            Thread.Sleep(100);

            mre.Reset();
            LowSpeedRead(nPort);
            if (!mre.WaitOne(1000, false))
                throw new NxtTimeout();

            if (nError > 0)
                throw new InvalidResult();

            if (result[3] != 1)
                throw new InvalidResult();

            return result[4];
        }

        public static void GetSensorState(SensorPort port, out SensorType type, out SensorMode mode, out int raw, out int normalized, out int scaled)
        {
            mre.Reset();
            GetInputState(port);
            if (!mre.WaitOne(1000, false))
                throw new NxtTimeout();

            if (nError > 0)
                throw new InvalidResult();

            if (result[4] != 1)
                throw new InvalidResult();

            type = (SensorType)result[6];
            mode = (SensorMode)result[7];
            raw = Buffer2Word(result, 8);
            normalized = Buffer2Word(result, 10);
            scaled = Buffer2Word(result, 12);
        }

        public static int GetSensorRaw(SensorPort port)
        {
            mre.Reset();
            GetInputState(port);
            if (!mre.WaitOne(1000, false))
                throw new NxtTimeout();

            if (nError > 0)
                throw new InvalidResult();

            if (result[4] != 1)
                throw new InvalidResult();

            return Buffer2Word(result, 8);
        }

        #endregion //sensor/input commands


        #region sound commands

        public static bool Beep(int freq, int time)
        {
            byte[] data = new byte[6];

            data[0] = 0x80;
            data[1] = 0x03;
            data[2] = (byte)(freq & 0xff);
            data[3] = (byte)(freq >> 8);
            data[4] = (byte)(time & 0xff);
            data[5] = (byte)(time >> 8);

            return Send(data);
        }

        public static void PlaySoundFile(string filename, bool bLoop)
        {
            byte[] data = new byte[23];

            data[0] = 0x00;
            data[1] = 0x02;
            if (bLoop)
                data[2] = 1;  //TODO: is this TRUE?
            else
                data[2] = 0;
            for (int i = 3; i < 22; i++)
                data[i] = 0;
            char[] ca = filename.ToCharArray();
            for (int i = 0; i < filename.Length; i++)
                data[i + 3] = (byte)ca[i];

            Send(data, 3);
        }

        #endregion //sound commands


        public static void WriteMessage(byte nInbox, string message)
        {
            byte[] data = new byte[message.Length + 4];

            data[0] = 0x80;
            data[1] = 0x09;
            data[2] = nInbox;
            data[3] = (byte)(message.Length + 1);
            char[] ca = message.ToCharArray();
            for (int i = 0; i < message.Length; i++)
                data[i + 4] = (byte)ca[i];
            data[message.Length + 3] = 0;

            Send(data);
        }

        public static bool GetVersion(out string firmwareVersion, out string protocolVersion)
        {
            bool b = true;

            byte[] data = new byte[2];

            data[0] = 0x01;
            data[1] = 0x88;

            mre.Reset();
            Send(data, 7);

            if (!mre.WaitOne(1000, false))
                b = false;

            if (nError > 0)
                b = false;

            firmwareVersion = result[6].ToString() + "." + result[5].ToString();
            protocolVersion = result[4].ToString() + "." + result[3].ToString();

            return b;
        }

        public static long KeepAlive()
        {
            byte[] data = new byte[2];

            data[0] = 0x00;
            data[1] = 0x0d;

            mre.Reset();
            Send(data, 7);
            if (!mre.WaitOne(1000, false))
                throw new NxtTimeout();

            if (nError > 0)
                throw new InvalidResult();

            return Buffer2Long(result, 3);
        }
    }
}
