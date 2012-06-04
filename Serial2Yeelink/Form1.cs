/* License: Creative Commons Attribution-Noncommercial 3.0 
 * http://creativecommons.org/licenses/by-nc/3.0/
 * 
 * http://www.ns-tech.co.uk/active-rfid-tracking-system/
*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using System.IO.Ports;

namespace Location_Tracking
{
    public partial class Form1 : Form
    {

        private HandleSerial handleSerial;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            //Get a list of serial port names.
            string[] ports = SerialPort.GetPortNames();

            //Add ports to the dropdown
            cboxSerialPort.Items.AddRange(ports);

            //Saved COM port
            string savedCOMPort = Properties.Settings.Default.COMPort;

            //If there is a saved com port, use it
            if (savedCOMPort != null)
            {
                //Make saved com port active
                cboxSerialPort.SelectedText = savedCOMPort;
            }

            //Saved auto connect
            bool savedAutoConnect = Properties.Settings.Default.AutoConnect;

            //Make saved auto connect setting active
            cboxAutoConnect.Checked = savedAutoConnect;

            //Make saved server URL setting active
            txtServerURL.Text = Properties.Settings.Default.ServerURL;

            txtApiKey.Text = Properties.Settings.Default.ApiKey;

            cboxBdRate.Text = Properties.Settings.Default.BdRate;

            //Init serial connection class
            handleSerial = new HandleSerial();

            //Pass through process bar
            handleSerial.toolStripProgressBar = toolStripProgressBar;

            //Pass through status log window
            handleSerial.txtStatusLog = txtStatusLog;

            //Pass through connect button
            handleSerial.btnConnect = btnConnect;

            //Pass through form
            handleSerial.form1 = this;

            //If auto connect specified when loading
            if (cboxAutoConnect.Checked == true)
            {

                //Attempt to start
                start();

            }

        }

        //Form close handler
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {

            //Save COM port selected
            Properties.Settings.Default.COMPort = cboxSerialPort.Text;

            //Save autoconnect setting
            Properties.Settings.Default.AutoConnect = cboxAutoConnect.Checked;

            //Save server URL
            Properties.Settings.Default.ServerURL = txtServerURL.Text;

            Properties.Settings.Default.ApiKey = txtApiKey.Text;

            Properties.Settings.Default.BdRate = cboxBdRate.Text;

            //Save settings
            Properties.Settings.Default.Save();

            //If connection open
            if ( (btnConnect.Text == "Disconnect") || (true) )
            {

                //Close serial port
                handleSerial.stop();

                //If background worker is still busy
                while (handleSerial.bgWorker.IsBusy == true)
                {

                    //Sleep for 100 ms
                    System.Threading.Thread.Sleep(100);

                    //Process message queue
                    Application.DoEvents();

                }

            }

        }

        //Serial port changed
        private void cboxSerialPort_TextChanged(object sender, EventArgs e)
        {

            //If no serial port specified, can not connect, disable connect button
            if (cboxSerialPort.Text == "")
            {
                btnConnect.Enabled = false;
            }
            else
            {
                btnConnect.Enabled = true;
            }

        }

        //Clear text in the debug text box
        private void btnClearInfo_Click(object sender, EventArgs e)
        {
            txtStatusLog.Clear();
        }

        //If connected status changes
        private void btnConnect_TextChanged(object sender, EventArgs e)
        {

            //If in ready to connect status, enable changing serial port, otherwise disable
            if (btnConnect.Text == "Connect")
            {
                cboxSerialPort.Enabled = true;
                txtServerURL.Enabled = true;
                txtApiKey.Enabled = true;
                cboxBdRate.Enabled = true;
            }
            else
            {
                cboxSerialPort.Enabled = false;
                txtServerURL.Enabled = false;
                txtApiKey.Enabled = false;
                cboxBdRate.Enabled = false;
            }

            //If if connect or disconnect status enable changing the button otherwise dissable (e.g. for intermediary stages)
            if ((btnConnect.Text == "Connect") || (btnConnect.Text == "Disconnect"))
            {
                btnConnect.Enabled = true;
            }
            else
            {
                btnConnect.Enabled = false;
            }
        }

        //Connect / disconnect button clicked
        private void btnConnect_Click(object sender, EventArgs e)
        {

            //If button is clicked to start connection
            if (btnConnect.Text == "连接服务器")
            {

                //Attempt to start
                start();

            }
            else
            {
                //Disconnect connection
                handleSerial.stop();
            }

        }

        //Attempt start
        private void start()
        {

            //Pass through com port
            handleSerial.comPort = cboxSerialPort.Text;

            //Pass through server URL
            handleSerial.serverURL = txtServerURL.Text;

            //Pass through apikey
            handleSerial.apiKey = txtApiKey.Text;

            handleSerial.bdRate = cboxBdRate.Text;

            //Attempt to connect via serial
            handleSerial.start();

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.ns-tech.co.uk/active-rfid-tracking-system/");
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void cboxAutoConnect_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.yeelink.net");
        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.ns-tech.co.uk/blog/2010/02/active-rfid-tracking-system/");
        }

    }
}