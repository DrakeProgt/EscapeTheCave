using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
//using Windows.Devices.Bluetooth.GenericAttributeProfile;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// Die Elementvorlage "Leere Seite" wird unter https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x407 dokumentiert.

namespace HRStream
{
    /// <summary>
    /// Eine leere Seite, die eigenständig verwendet oder zu der innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        BLEHeart BLE = new BLEHeart();
        UDPSend UDP = new UDPSend();
        //string DeviceMAC = "a0:9e:1a:2e:a0:83";//OH1
        //string DeviceMAC = "f5:8d:0e:26:ad:c0";//scrosche
        string DeviceMAC;
        string DeviceID = null;
        bool simstatus;
        bool connected;
        int HR;
        string[] macs;
        public MainPage()
        {
            this.InitializeComponent();
            
            simstatus = (bool)btnSwitch.IsChecked;
            UDP.init(33333);
            connected = false;
            slideHR.IsEnabled = (bool)btnSwitch.IsChecked;
            
            macs = File.ReadAllLines(@"Assets\MAC.txt");
            foreach (string mac in macs)
            {
                comboMAC.Items.Add(mac);
            }

            if ((bool)btnSwitch.IsChecked)
            {
                simulate();
            }
            else
            {
                FindAndConnect();
            }
        }

        private async void simulate()
        {
            Random rand = new Random();
            HR = 60;
            do
            {
                HR += rand.Next(-8, 11);
                UDP.sendString(HR.ToString());
                txtHR.Text = HR.ToString();
                slideHR.Value = HR;
                await Task.Delay(1500);
            }
            while (simstatus);
        }
        private async void Reconnect() {
            BLE.Unsubscribe();
            BLE.Disconnect();
            await Task.Delay(2000);
            await Connect();
            await Subscribe();
        }

        private async void FindAndConnect()
        {
            int found = await Search();
            if (found == 1)
            {
                await Connect();
                if (connected)
                {
                    await Subscribe();
                    await Task.Delay(1500);
                    int rep = 0;
                    while (!simstatus)
                    {
                        await Task.Delay(1000);
                        if (rep >= 10)
                        {
                            Reconnect();
                            rep = 0;
                        }
                        if (BLE.HR == HR.ToString())
                        {
                            rep++;
                        }
                        else
                        {
                            rep = 0;
                        }
                        HR = BLE.HR != "Null" ? Int32.Parse(BLE.HR) : 0;
                        txtHR.Text = BLE.HR;
                        UDP.sendString(BLE.HR);
                    }
                }
            }
        }

        private async Task Subscribe()
        {
            BLE.Subscribe(DeviceID);
            ringSubscribe.IsActive = true;
            txtSub.Text = "Subscribed";
        }

        /*
        private async void Characteristic_ValueChanged(GattCharacteristic sender, GattValueChangedEventArgs args)
        {
            // BT_Code: An Indicate or Notify reported that the value has changed.
            // Display the new value with a timestamp.
            HR = BLE.FormatValueByPresentation(args.CharacteristicValue);
            //var message = $"{DateTime.Now:hh:mm:ss.FFF}: \n{newValue}";
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
                () => txtHR.Text = HR);
            SendUDP();
        }
        

        private Task<string> GetHR()
        {
            return BLE.HR;
        }
*/
        private async Task<int> Search()
        {
            ringSearch.IsActive = true;
            List<string> devices = await BLE.Search();
            Debug.WriteLine("Device Not Found");
            txtSearch.Text = "Device Not Found";
            foreach (string id in devices)
            {
                if (id.Contains(DeviceMAC))
                {
                    Debug.WriteLine("Found " + DeviceMAC);
                    txtSearch.Text = "Found Device: "+ DeviceMAC;
                    DeviceID = id;
                    ringSearch.IsActive = false;
                    return 1;
                }
            }
            btnSwitch.IsChecked = true;
            ToggleButton_Tapped(null, null);
            ringSearch.IsActive = false;
            return 0;
        }

        private async Task Connect()
        {
            ringConn.IsActive = true;
            if (await BLE.Connect(DeviceID) == 1)
            {
                txtConn.Text = "Connected!";
                connected = true;
            }
            else
            {
                txtConn.Text = "Failed to connect!\nSimulating...";
                btnSwitch.IsChecked = true;
                ToggleButton_Tapped(null,null);
                connected = false;
            }
            ringConn.IsActive = false;
        }

        private async void SendUDP()
        {
            try
            {
                // Create the DatagramSocket and establish a connection to the echo server.
                var clientDatagramSocket = new Windows.Networking.Sockets.DatagramSocket();


                // The server hostname that we will be establishing a connection to. In this example, the server and client are in the same process.
                var hostName = new Windows.Networking.HostName("127.0.0.1");


                await clientDatagramSocket.BindServiceNameAsync("33333");


                // Send a request to the echo server.
                string request = HR.ToString();
                using (var serverDatagramSocket = new Windows.Networking.Sockets.DatagramSocket())
                {
                    using (Stream outputStream = (await serverDatagramSocket.GetOutputStreamAsync(hostName, "33333")).AsStreamForWrite())
                    {
                        using (var streamWriter = new StreamWriter(outputStream))
                        {
                            await streamWriter.WriteLineAsync(request);
                            await streamWriter.FlushAsync();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Windows.Networking.Sockets.SocketErrorStatus webErrorStatus = Windows.Networking.Sockets.SocketError.GetStatus(ex.GetBaseException().HResult);
            }
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {

            if (btnStop.Content.Equals("Stop")&&connected)
            {
                BLE.Unsubscribe();
                BLE.Disconnect();
                ringSubscribe.IsActive = false;
                btnStop.Content = "Start";
            }else if(btnStop.Content.Equals("Start") && !connected)
            {
                await Connect();
                await Subscribe();
                btnStop.Content = "Stop";
                ringSubscribe.IsActive = true;
            }

        }


        private void slideHR_ValueChanged(object sender, Windows.UI.Xaml.Controls.Primitives.RangeBaseValueChangedEventArgs e)
        {
            HR = (int)slideHR.Value;
            txtHR.Text = slideHR.Value.ToString();
        }

        private void switchSimulate_Toggled_1(object sender, RoutedEventArgs e)
        {/*
            simstatus = switchSimulate.IsOn;
            slideHR.IsEnabled = switchSimulate.IsOn;
            if (switchSimulate.IsOn)
            {
                simulate();
            }
            else
            {
                FindAndConnect();
            }
            */
        }

        private void ToggleButton_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            simstatus = (bool)btnSwitch.IsChecked;
            slideHR.IsEnabled = (bool)btnSwitch.IsChecked;
            if ((bool)btnSwitch.IsChecked)
            {
                simulate();
            }
            else
            {
                FindAndConnect();
            }
        }
        private void comboMAC_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DeviceMAC = comboMAC.SelectedItem.ToString().Split('_')[1];
        }
    }
}
//"a0:9e:1a:2e:a0:83"
