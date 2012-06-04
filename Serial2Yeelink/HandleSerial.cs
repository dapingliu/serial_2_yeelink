using System;
using System.Collections.Generic;
using System.Text;

using System.Windows.Forms;
using System.ComponentModel;
using System.Collections;
using System.IO.Ports;
using System.Text.RegularExpressions;
using System.Threading;
using System.Net;

namespace Location_Tracking
{
    class HandleSerial
    {

        public string comPort;
        public ToolStripProgressBar toolStripProgressBar;
        public Button btnConnect;
        public TextBox txtStatusLog;
        public string serverURL;
        public string apiKey;
        public string bdRate;
        public Form form1;
        private bool bgWorkedError = false;

        private SerialPort serialPort = new SerialPort();
        public BackgroundWorker bgWorker = new BackgroundWorker();
        private List<TagCollection> tagPings = new List<TagCollection>();
        StringBuilder serialBuffer = new StringBuilder();

        //private System.Windows.Forms.Timer sendTimer = new System.Windows.Forms.Timer();

        //Handle serial communications
        public HandleSerial()
        {

            //Initialise background worker thread
            bgWorker.WorkerReportsProgress = true;
            bgWorker.WorkerSupportsCancellation = true;
            bgWorker.DoWork += new DoWorkEventHandler(bgWorker_DoWork);
            bgWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bgWorker_RunWorkerCompleted);
            bgWorker.ProgressChanged += new ProgressChangedEventHandler(bgWorker_ProgressChanged);

            //Initialise send data in background timer
            //sendTimer.Interval = 1000; //1 sec
            //sendTimer.Tick += new EventHandler(sendDataBackground);
            //sendTimer.Start();

        }

        //Start serial connection
        public void start()
        {

            //Check URL valid
            if (serverURL != "")
            {
                try
                {
                    Uri configuri = new Uri(serverURL);
                }
                catch (UriFormatException ex)
                {
                    statusLogAdd("Server URL Error: " + ex.Message);
                    return;
                }
            }

            //Clear status log box
            txtStatusLog.Clear();

            //Set button status to disconnect
            btnConnect.Text = "断开连接";

            //Pass through serial port to background worker
            Hashtable workerOptions = new Hashtable();
            workerOptions.Add("port", comPort);
            workerOptions.Add("serverURL", serverURL);
            workerOptions.Add("apiKey", apiKey);
            workerOptions.Add("data", "");
            workerOptions.Add("bdRate", bdRate);

            //Init background worker
            bgWorker.RunWorkerAsync(workerOptions);

        }

        //Stop serial connection
        public void stop()
        {

            //Cancel background worker
            bgWorker.CancelAsync();

            //Update button status
            btnConnect.Text = "正在取消...";

        }

        //Do work in background
        void bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {

            //Clear any errors
            bgWorkedError = false;

            //Report process started
            bgWorker.ReportProgress(0, "程序已经启动");

            //Read out passed in options
            Hashtable workeroptions;
            workeroptions = (Hashtable)e.Argument;
            bgWorker.ReportProgress(0, "串口号: " + (string)workeroptions["port"]);
            bgWorker.ReportProgress(0, "上传地址: " + (string)workeroptions["serverURL"]);
            bgWorker.ReportProgress(0, "ApiKey:" + (string)workeroptions["apiKey"]);
            
            //Init serial port
            serialPort.BaudRate = int.Parse((string)workeroptions["bdRate"]);
            serialPort.Parity = Parity.None;
            serialPort.DataBits = 8;
            serialPort.StopBits = StopBits.One;
            serialPort.ReadTimeout = 1000 * 5;
            serialPort.WriteTimeout = 1000 * 5;
            serialPort.Handshake = Handshake.RequestToSend;
            serialPort.PortName = (string)workeroptions["port"];
            serialPort.DtrEnable = false;

            //Try opening port
            try
            {
                serialPort.Open();
            }
            catch (Exception ex)
            {

                serialPort.Close();

                //Report error message
                bgWorker.ReportProgress(0, ex.Message);

                //bgWorkedError = true;
                return;

            }

            bgWorker.ReportProgress(0, "串口已经打开");

            bgWorker.ReportProgress(0, "正在等待串口数据...");

            //Thread.Sleep(2000);

            //Loop waiting for data
            do {

                //If have data
                if (serialPort.BytesToRead > 0)
                {

                    //Read in the data
                    serialBuffer.Append(serialPort.ReadExisting());

                    //Go through the data, one character at a time
                    StringBuilder reportLine = new StringBuilder();
                    int removeCharUpto = 0;
                    int totalChar = serialBuffer.Length;
                    for (int i = 0; i < totalChar; i++)
                    {

                        //If character is a new line
                        if ((serialBuffer[i].ToString() == "\r") || (serialBuffer[i].ToString() == "\n"))
                        {

                            //Have a complete line of data that should be processed
                            //bgWorker.ReportProgress(0, serialBuffer[i].ToString());

                            //Remove \r's
                            if (reportLine.Length > 0)
                            {

                                string line = reportLine.ToString();

                                //Split into parts
                                Match lineMatches = Regex.Match(line, @"^yeelink:((-?([1-9]\d*\.\d*|0\.\d*|0))|(-?[1-9]\d*))$");
                                if (lineMatches.Success)
                                {
                                    //bgWorker.ReportProgress(0, lineMatches.Groups[0].Value.ToString());
                                    string[] data = lineMatches.Groups[0].Value.ToString().Split(':');
                                    workeroptions["data"] = data[1];
                                    //bgWorker.ReportProgress(0, line);
                                    bgWorker.ReportProgress(0, "正在上传数据: " + workeroptions["data"]);
                                    //int signal = int.Parse(lineMatches.Groups[0].Value);

                                    //Process the tag ping
                                    //handleTagPing(lineMatches.Groups[1].Value, lineMatches.Groups[2].Value, signal);

                                }
                                else
                                {
                                    //bgWorker.ReportProgress(0, "Unrecognied data  " + line);
                                }

                            }

                            //Have processed report line, clear it ready for new report
                            reportLine.Length = 0;
                            removeCharUpto = i;

                        }
                        else
                        {

                            //Add character to report line
                            reportLine.Append(serialBuffer[i]);

                        }

                    }

                    //Remove characters that have just been processed from the serial buffer
                    if (removeCharUpto > 0)
                    {
                        serialBuffer.Remove(0, removeCharUpto);
                    }

                    /*
                    //Read in the data
                    //string serialBuffer = serialPort.ReadExisting();

                    //Split it into lines
                    char[] delimiters = new char[] { '\r', '\n' };
                    string[] lines = serialBuffer.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
                    //string[] lines = Regex.Split(serialBuffer, "\r\n");
                    //string[] lines = serialBuffer.Split(new char[] { @"\n" }, StringSplitOptions.None);

                    //For each line
                    foreach (string line in lines)
                    {
                    }

                    */

                }

                //Send data to server via HTTP
                if ((string)workeroptions["data"] != "")
                {
                    sendDataBackground((string)workeroptions["serverURL"], (string)workeroptions["data"], (string)workeroptions["apiKey"]);
                    workeroptions["data"] = "";
                }

                //If not awaiting cancellation
                if (!bgWorker.CancellationPending)
                {
                    //Wait 100 ms before checking for more data
                    Thread.Sleep(100);
                }

            } while (!bgWorker.CancellationPending);

            //Close serial port
            serialPort.Close();

        }

        //Process tag ping
        void handleTagPing(string tagAddr, string readerAddr, int signal)
        {

            //Console.WriteLine("{0} Tag: {1}, Signal: {2}, Reader: {3}", DateTime.Now, tagAddr, signal, readerAddr);

            bgWorker.ReportProgress(0, string.Format("{0} Tag: {1}, Signal: {2}, Reader: {3}", DateTime.Now, tagAddr, signal, readerAddr));

            TagCollection currTag = new TagCollection();

            //Go through each tag already in the list
            bool foundTag = false;
            foreach (TagCollection tagCollectionItem in tagPings)
            {

                //If this tag is already in the list
                if (tagCollectionItem.tagAddr == tagAddr)
                {

                    foundTag = true;
                    currTag = tagCollectionItem;

                    //bgWorker.ReportProgress(0, "Allrady have tag in list");

                }

            }

            //If do not already have the tag in the list
            if (foundTag == false)
            {

                //Add the tag to the list
                TagCollection tagCollectionItem = new TagCollection();
                tagCollectionItem.tagAddr = tagAddr;
                tagCollectionItem.firstSeen = DateTime.UtcNow;
                tagPings.Add(tagCollectionItem);

                //Save current tag item
                currTag = tagCollectionItem;

                //bgWorker.ReportProgress(0, "Added tag to list");

            }

            TagReaderCollection currReader = new TagReaderCollection();

            //Go through all readers already saved for this tag
            bool foundReader = false;
            foreach (TagReaderCollection reader in currTag.readers) {

                //If this reader is already in the list
                if (reader.readerAddr == readerAddr) {
                    foundReader = true;
                    currReader = reader;

                    //bgWorker.ReportProgress(0, "Already have reader in list");

                }

            }

            //If do not already have the reader in the list
            if (foundReader == false)
            {

                //Add the reader to the list
                TagReaderCollection tagReaderCollectionItem = new TagReaderCollection();
                tagReaderCollectionItem.readerAddr = readerAddr;
                currTag.readers.Add(tagReaderCollectionItem);

                //Save current reader item
                currReader = tagReaderCollectionItem;

                //bgWorker.ReportProgress(0, "Added reader to list");

            }

            //Save/update signal for the reader
            currReader.signal = signal;

        }

        //Background work completed
        void bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

            //Set button back to connect
            btnConnect.Text = "连接服务器";

            //Clear process bar status
            toolStripProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Blocks;
            toolStripProgressBar.Value = 0;

            /*
            if (e.Cancelled)
            {

            }
            else if (e.Error != null)
            {

            }
            else if (bgWorkedError == true) {

            }
            else
            {
                //finished with no errors
            }
            */

        }

        //Background work process change
        void bgWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

            /*
            if (e.ProgressPercentage == 0)
            {
                toolStripProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
                toolStripProgressBar.Value = e.ProgressPercentage;
            }
            else
            {
                toolStripProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Blocks;
            }

            toolStripProgressBar.Value = e.ProgressPercentage;
            */

            //Add status message to log
            statusLogAdd(e.UserState.ToString());

        }

        //Add a message to the status log
        void statusLogAdd(string message)
        {

            //If message log is getting too big
            if (txtStatusLog.Text.Length > 10000)
            {
                //Clear message log
                txtStatusLog.Clear();
            }

            //if already has data add a new line
            if (txtStatusLog.Text.Length > 0)
            {
                txtStatusLog.AppendText(Environment.NewLine);
            }

            //Add message to the log
            txtStatusLog.AppendText(message);

            //Scroll to the bottom to see the newly added message
            txtStatusLog.ScrollToCaret();

        }

        //Send data to server in background via HTTP
        //void sendDataBackground(object sender, EventArgs e)
        void sendDataBackground(string serverURL, string data, string apiKey)
        {

            List<TagCollection> tagPingsRemove = new List<TagCollection>();

            StringBuilder postData = new StringBuilder();
            postData.Append("{\"value\":" + data + "}");
            /*
            //Go through all tags in the list
            int tagno = 0;
            foreach (TagCollection tagCollectionItem in tagPings)
            {

                //Check if the tag was last seen over 1.5 second ago
                DateTime firstSeenCheck = tagCollectionItem.firstSeen.AddSeconds(1.5);

                //If tag was last seen over limit
                if (firstSeenCheck.Ticks < DateTime.UtcNow.Ticks)
                {

                    //Console.WriteLine(tagCollectionItem.firstSeen);

                    //Add tag to post data
                    postData.Append("tags[" + tagno.ToString() + "][addr]=" + System.Uri.EscapeDataString(tagCollectionItem.tagAddr) + "&");
                    postData.Append("tags[" + tagno.ToString() + "][firstseen]=" + System.Uri.EscapeDataString(firstSeenCheck.ToString("yyyy-MM-dd HH:mm:ss")) + "&");
                    //http://msdn.microsoft.com/en-us/library/system.globalization.datetimeformatinfo(VS.71).aspx

                    //Go through each reader / signal and adding it to the post data array
                    int readerno = 0;
                    foreach (TagReaderCollection reader in tagCollectionItem.readers)
                    {
                        postData.Append("tags[" + tagno.ToString() + "][readers][" + readerno.ToString() + "][addr]=" + System.Uri.EscapeDataString(reader.readerAddr) + "&");
                        postData.Append("tags[" + tagno.ToString() + "][readers][" + readerno.ToString() + "][signal]=" + System.Uri.EscapeDataString(reader.signal.ToString()) + "&");
                        readerno++;
                    }

                    //Increment post data tag no counter
                    tagno++;

                    //Add tag to the remove list
                    tagPingsRemove.Add(tagCollectionItem);

                }

            }

            //Go through all tags in the remove (just processed)  list
            foreach (TagCollection tagCollectionItem in tagPingsRemove)
            {
                //Remove tag
                tagPings.Remove(tagCollectionItem);
            }
            */

            //If there is post data available
            if (postData.Length > 0) {

                //If there is a server specified
                if (serverURL != "")
                {

                    //Send data via HTTP post to server

                    //Console.WriteLine(postData.ToString());

                    //http://msdn.microsoft.com/en-us/library/system.net.httpwebrequest.aspx

                    const int bufSizeMax = 65536;
                    const int bufSizeMin = 8192;
                    StringBuilder sb;

                    // A WebException is thrown if HTTP request fails
                    try
                    {

                        //bgWorker.ReportProgress(0, "Posting data to server");

                        //Convert post data to bytes
                        byte[] bytes = Encoding.ASCII.GetBytes(postData.ToString());

                        // Create an HttpWebRequest using WebRequest.Create (see .NET docs)!
                        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(serverURL);
                        request.Method = "post";
                        request.Headers.Add("U-ApiKey: " + apiKey);
                        request.ContentType = "application/x-www-form-urlencoded";
                        request.ContentLength = bytes.Length;
                        request.Timeout = 1000 * 5;

                        //Send post data
                        System.IO.Stream sendStream = request.GetRequestStream();
                        sendStream.Write(bytes, 0, bytes.Length);
                        sendStream.Close();
                        
                        // Execute the request and obtain the response stream
                        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                        System.IO.Stream responseStream = response.GetResponseStream();

                        //Responses longer than int size will throw an exception here
                        int length = (int)response.ContentLength;

                        //Use Content-Length if between bufSizeMax and bufSizeMin
                        int bufSize = bufSizeMin;
                        if (length > bufSize)
                            bufSize = length > bufSizeMax ? bufSizeMax : length;

                        // Allocate buffer and StringBuilder for reading response
                        byte[] buf = new byte[bufSize];
                        sb = new StringBuilder(bufSize);

                        // Read response stream until end
                        while ((length = responseStream.Read(buf, 0, buf.Length)) != 0)
                            sb.Append(Encoding.UTF8.GetString(buf, 0, length));

                        //Check the http status code
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            //bgWorker.ReportProgress(0, "Posted data to server OK.");
                            bgWorker.ReportProgress(0, "数据已上传至服务器, 收到响应: " + sb.ToString() + "\n\n");
                        }
                        else
                        {
                            bgWorker.ReportProgress(0, "数据上传错误: 服务器响应HTTP Status Code " + response.StatusCode.ToString());
                            bgWorker.ReportProgress(0, "HTTP数据: " + sb.ToString() + "\n\n");
                        }

                    }
                    catch (Exception ex)
                    {
                        bgWorker.ReportProgress(0, "数据上传错误: " + ex.Message);
                    }

                }

            }

        }

    }

}
