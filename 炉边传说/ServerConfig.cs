﻿using Engine.Utility;
using System;
using System.Net;
using System.Threading;
using System.Windows.Forms;
namespace 炉边传说
{
    public partial class ServerConfig : Form
    {
        public ServerConfig()
        {
            InitializeComponent();
        }
        Thread ServerThread; 
        /// <summary>
        /// 启动服务器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStart_Click(object sender, EventArgs e)
        {
            Engine.Utility.CardUtility.Init(txtCardPath.Text);
            btnStartTcp.Enabled = false;
            btnStopTcp.Enabled = true;
            Engine.Utility.SystemManager.Init();
            ServerThread = new Thread(TcpSocketServer.StartTcpServer);
            ServerThread.IsBackground = true;
            ServerThread.Start();
        }
        /// <summary>
        /// 终止线程
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStop_Click(object sender, EventArgs e)
        {
            ServerThread.Abort();
            ServerThread = null;
            Engine.Utility.SystemManager.Terminate();
            GC.Collect();
        }
        private void btnStartHttp_Click(object sender, EventArgs e)
        {
            Engine.Utility.CardUtility.Init(txtCardPath.Text);
            btnStartHttp.Enabled = false;
            btnStopHttp.Enabled = true;
            Engine.Utility.SystemManager.Init();
            WebSocketServer.Start();
            //ServerThread = new Thread(WebSocketServer.Start);
            //ServerThread.IsBackground = true;
            //ServerThread.Start();
        }
        private void btnStopHttp_Click(object sender, EventArgs e)
        {
            WebSocketServer.Stop();
            //ServerThread.Abort();
            //ServerThread = null;
            //Engine.Utility.SystemManager.Terminate();
            //GC.Collect();
        }
        /// <summary>
        /// 选择卡牌目录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPickCard_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog cardPath = new FolderBrowserDialog();
            if (cardPath.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                txtCardPath.Text = cardPath.SelectedPath;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ServerConfig_Load(object sender, EventArgs e)
        {
            //DEBUG
            txtCardPath.Text = @"C:\炉石Git\炉石设计\Card";
            IPAddress[] hostipspool = Dns.GetHostAddresses("");
            if (hostipspool.Length >3) lblIP.Text = "IP Address:" + hostipspool[3];
        }




    }
}
