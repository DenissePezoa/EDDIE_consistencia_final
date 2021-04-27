using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using WatsonWebsocket;
using InterfazRastreoOcular;

namespace PluginGazeCloud
{
    /// <summary>
    /// Eye tracking plugin for the javascript library GazeCloudAPI.
    /// Communication is achieved through sockets using localhosts and the port 3000
    /// </summary>
    public class PluginGazeCloud : IEyeTracking
    {
        private Dictionary<string, string> _Data = new Dictionary<string, string>();
        public Dictionary<string, string> Data
        {
            get { return _Data; }
            set
            {
                _Data = value;
                OnPropertyChanged(this, new PropertyChangedEventArgs(nameof(Data)));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
            //else
            //{
            //    Console.WriteLine("handler null");
            //}
        }

        public bool OpenConnection()
        {
            try
            {
                WatsonWsServer server = new WatsonWsServer("127.0.0.1", 3000, false);
                server.ClientConnected += ClientConnected;
                server.ClientDisconnected += ClientDisconnected;
                server.MessageReceived += MessageReceived;
                server.Logger = Logger;
                server.Start();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        //public string pluginName()
        //{
        //    return "GazeCloud";
        //}

        //public string pluginVersion()
        //{
        //    return "1.0.0";
        //}

        //  Watson Websocket methods.   
        public void Logger(string msg)
        {
            Console.WriteLine(msg);
        }

        public void ClientConnected(object sender, ClientConnectedEventArgs args)
        {
            Console.WriteLine("Client connected: " + args.IpPort);
        }

        public void ClientDisconnected(object sender, ClientDisconnectedEventArgs args)
        {
            Console.WriteLine("Client disconnected: " + args.IpPort);
        }

        public void MessageReceived(object sender, MessageReceivedEventArgs args)
        {
            string msg = "(null)";
            if (args.Data != null && args.Data.Length > 0) msg = Encoding.UTF8.GetString(args.Data);
            if (!msg.Equals("OK"))
            {
                var aux = msg.Split(',').ToList();
                Dictionary<string, string> auxDict = new Dictionary<string, string>
                {
                    { "X_Coordinate", aux.ElementAt(0) },
                    { "Y_Coordinate", aux.ElementAt(1) },
                    { "Timestamp", aux.ElementAt(2) }
                };
                Data = auxDict;
            }
        }
    }
}
