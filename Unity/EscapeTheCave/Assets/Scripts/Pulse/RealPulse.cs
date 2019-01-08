using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
//using System.Diagnostics;
using System.Collections.Generic;
using UnityEngine;

public class RealPulse : MonoBehaviour {
    private static RealPulse instance;
    Thread receiveThread;
    UdpClient client;
    public int port = 33333, HR = 0;
    public List<int> allReceivedUDPPackets = new List<int>();
    IPEndPoint anyIP;

    public static RealPulse GetInstance()
    {
        if (instance == null)
        {
            instance = new RealPulse();
        }

        return instance;
    }

    public void init(int port)
    {
        Debug.Log("UDPSend.init()");
        Debug.Log("Receiving on to 127.0.0.1 : " + port);
        client = new UdpClient(port);
        anyIP = new IPEndPoint(IPAddress.Any, port);
        this.port = port;


        /*
        receiveThread = new Thread(
            new ThreadStart(ReceiveData));
        receiveThread.IsBackground = true;
        receiveThread.Start();
        */
    }

    public int getHR()
    {
        return HR;
    }

    public void ReceiveData()
    {
        
        while (true)
        {
                byte[] data = client.Receive(ref anyIP);

                // Bytes mit der UTF8-Kodierung in das Textformat kodieren.
                string text_HR = Encoding.UTF8.GetString(data);

                // Den abgerufenen Text anzeigen.
                Debug.Log(">> " + text_HR);

                //Konvertiere & Speicher Wert
                Int32.TryParse(text_HR, out HR);

                // Merke erhaltene Daten
                allReceivedUDPPackets.Add(HR);
        }
    }
    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}
}
