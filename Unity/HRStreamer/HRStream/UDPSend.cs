using System.Collections;
using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Diagnostics;
using System.Threading.Tasks;

namespace HRStream
{
    class UDPSend
    {
        private static int localPort;

        // prefs
        private string IP;  // define in init
        public int port;  // define in init

        // "connection" things
        IPEndPoint remoteEndPoint;
        UdpClient client;

        // gui
        string strMessage = "";

        // init
        public void init(int port)
        {
            // Endpunkt definieren, von dem die Nachrichten gesendet werden.
            Debug.WriteLine("UDPSend.init()");

            // define
            IP = "127.0.0.1";
            this.port = port;

            // ----------------------------
            // Senden
            // ----------------------------
            remoteEndPoint = new IPEndPoint(IPAddress.Parse(IP), port);
            client = new UdpClient();

            // status
            Debug.WriteLine("Sending to " + IP + " : " + port);
            Debug.WriteLine("Testing: nc -lu " + IP + " : " + port);

        }


        // sendData
        public async void sendString(string message)
        {
            try
            {
                //if (message != "")
                //{
                Debug.WriteLine("Sending Msg >>>"+message);
               // Daten mit der UTF8-Kodierung in das Binärformat kodieren.
                byte[] data = Encoding.UTF8.GetBytes(message);

                // Den message zum Remote-Client senden.
                await client.SendAsync(data, data.Length, remoteEndPoint);

                //}
            }
            catch (Exception err)
            {
                Debug.WriteLine(err.ToString());
            }
        }


        // endless test
        public async void sendEndless(string testStr)
        {
            do
            {
                sendString(testStr);
                await Task.Delay(1000);
            }
            while (true);
        }
    }
}
