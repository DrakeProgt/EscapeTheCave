using System;
using System.Collections.Generic;
using Windows.Foundation;
using Windows.Devices.Bluetooth;
using Windows.Devices.Bluetooth.GenericAttributeProfile;
using Windows.Devices.Enumeration;
using Windows.UI.Popups;
using Windows.Security.Cryptography;
using System.Text;
using Windows.Storage.Streams;
using System.Threading.Tasks;
using System.Diagnostics;


public class BLEHeart
{
    bool subscribedForNotifications = false;
    DeviceInformation device;
    GattDeviceService service;
    GattCharacteristic characteristics;
    BluetoothLEDevice bluetoothLeDevice;
    public string HR = "Null";    

    public BLEHeart()
    {
    }

    private void Characteristic_ValueChanged(GattCharacteristic sender, GattValueChangedEventArgs args)
    {
        // BT_Code: An Indicate or Notify reported that the value has changed.
        // Display the new value with a timestamp.
        HR = FormatValueByPresentation(args.CharacteristicValue);
        //var message = $"{DateTime.Now:hh:mm:ss.FFF}: \n{newValue}";
    }

    public async Task<List<String>> Search()
    {
        List<String> DevIDs = new List<string>();
        var devices = await DeviceInformation.FindAllAsync(
            GattDeviceService.GetDeviceSelectorFromUuid(GattServiceUuids.HeartRate),
            new string[] { "System.Devices.ContainerId" });

        if (devices.Count > 0)
        {
            foreach (var device in devices)
            {
                DevIDs.Add(device.Id);
            }
            return DevIDs;
        }
        else
        {
            var dialog = new MessageDialog("Could not find any Heart Rate devices. Please make sure your device is paired and powered on!");
            await dialog.ShowAsync();
            return null;
        }
    }
        
    public async Task<int> Connect(String ID)
    {
                Debug.WriteLine("Initializing device...");
                try
                {
                    // BT_Code: BluetoothLEDevice.FromIdAsync must be called from a UI thread because it may prompt for consent.
                    bluetoothLeDevice = await BluetoothLEDevice.FromIdAsync(ID);
                    if (bluetoothLeDevice == null)
                    {
                        var dialog = new MessageDialog("Failed to connect to device.");
                        await dialog.ShowAsync();
                        return 0;
                    }
                    else
                    {
                        GattDeviceServicesResult result = await bluetoothLeDevice.GetGattServicesAsync(BluetoothCacheMode.Uncached);
                        if (result.Status == GattCommunicationStatus.Success)
                        {
                            Debug.WriteLine("Connected & Found services");
                            return 1;
                        }
                        return 0;
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Retrieving device properties failed with message: " + ex.Message);
                    return 0;
                }
    }

    public void Disconnect()
    {
        service.Dispose();
        bluetoothLeDevice = null;
        GC.Collect();
    }

    public async void Unsubscribe(/*TypedEventHandler<GattCharacteristic, GattValueChangedEventArgs> action*/)
    {
        try
        {
            //characteristics = service.GetCharacteristics(GattCharacteristicUuids.HeartRateMeasurement)[0];
            GattCharacteristicsResult x = await service.GetCharacteristicsForUuidAsync(GattCharacteristicUuids.HeartRateMeasurement);
            characteristics = x.Characteristics[0];

            // BT_Code: Must write the CCCD in order for server to send notifications.
            // We receive them in the ValueChanged event handler.
            // Note that this sample configures either Indicate or Notify, but not both.
            var result = await characteristics.WriteClientCharacteristicConfigurationDescriptorAsync(GattClientCharacteristicConfigurationDescriptorValue.None);
            if (result == GattCommunicationStatus.Success)
            {
                subscribedForNotifications = false;
                if (subscribedForNotifications)
                {
                    characteristics.ValueChanged -= Characteristic_ValueChanged;
                    characteristics = null;
                    subscribedForNotifications = false;
                }
                Debug.WriteLine("Successfully un-registered for notifications");
            }
            else
            {
                Debug.WriteLine($"Error un-registering for notifications: {result}");
            }
        }
        catch (UnauthorizedAccessException ex)
        {
            // This usually happens when a device reports that it support notify, but it actually doesn't.
            Debug.WriteLine(ex.Message);
            var dialog = new MessageDialog(ex.Message);
            await dialog.ShowAsync();
        }
        catch (ObjectDisposedException oex)
        {
            Debug.WriteLine(oex.Message);
        }
    }

    public async void Subscribe(String ID/*, TypedEventHandler<GattCharacteristic, GattValueChangedEventArgs> action*/)
    {
        service = await GattDeviceService.FromIdAsync(ID);
        characteristics = service.GetCharacteristics(GattCharacteristicUuids.HeartRateMeasurement)[0];
        if (!subscribedForNotifications)
        {
            // initialize status
            GattCommunicationStatus status = GattCommunicationStatus.Unreachable;
            var cccdValue = GattClientCharacteristicConfigurationDescriptorValue.None;
            if (characteristics.CharacteristicProperties.HasFlag(GattCharacteristicProperties.Indicate))
            {
                cccdValue = GattClientCharacteristicConfigurationDescriptorValue.Indicate;
            }

            else if (characteristics.CharacteristicProperties.HasFlag(GattCharacteristicProperties.Notify))
            {
                cccdValue = GattClientCharacteristicConfigurationDescriptorValue.Notify;
            }

            try
            {
                // BT_Code: Must write the CCCD in order for server to send indications.
                // We receive them in the ValueChanged event handler.
                status = await characteristics.WriteClientCharacteristicConfigurationDescriptorAsync(cccdValue);

                if (status == GattCommunicationStatus.Success)
                {
                    if (!subscribedForNotifications)
                    {
                        characteristics.ValueChanged += Characteristic_ValueChanged;
                        subscribedForNotifications = true;
                    }
                    Debug.WriteLine("Successfully subscribed for value changes");
                }
                else
                {
                    Debug.WriteLine($"Error registering for value changes: {status}");
                    
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                // This usually happens when a device reports that it support indicate, but it actually doesn't.
                Debug.WriteLine(ex.Message);
                var dialog = new MessageDialog(ex.Message);
                await dialog.ShowAsync();
            }
        }
    }

    private static ushort ParseHeartRateValue(byte[] data)
    {
        // Heart Rate profile defined flag values
        const byte heartRateValueFormat = 0x01;

        byte flags = data[0];
        bool isHeartRateValueSizeLong = ((flags & heartRateValueFormat) != 0);

        if (isHeartRateValueSizeLong)
        {
            return BitConverter.ToUInt16(data, 1);
        }
        else
        {
            return data[1];
        }
    }

    public string FormatValueByPresentation(IBuffer buffer)
    {
        GattPresentationFormat format = null;//characteristics.PresentationFormats[0];
        // BT_Code: For the purpose of this sample, this function converts only UInt32 and
        // UTF-8 buffers to readable text. It can be extended to support other formats if your app needs them.
        byte[] data;
        CryptographicBuffer.CopyToByteArray(buffer, out data);
        if (format != null)
        {
            if (format.FormatType == GattPresentationFormatTypes.UInt32 && data.Length >= 4)
            {
                return BitConverter.ToInt32(data, 0).ToString();
            }
            else if (format.FormatType == GattPresentationFormatTypes.Utf8)
            {
                try
                {
                    return Encoding.UTF8.GetString(data);
                }
                catch (ArgumentException)
                {
                    return "(error: Invalid UTF-8 string)";
                }
            }
            else
            {
                // Add support for other format types as needed.
                return "Unsupported format: " + CryptographicBuffer.EncodeToHexString(buffer);
            }
        }
        else if (data != null)
        {
            // We don't know what format to use. Let's try some well-known profiles, or default back to UTF-8.
            if (characteristics.Uuid.Equals(GattCharacteristicUuids.HeartRateMeasurement))
            {
                try
                {
                    //return "Heart Rate: " + ParseHeartRateValue(data).ToString();
                    return ParseHeartRateValue(data).ToString();
                }
                catch (ArgumentException)
                {
                    return "Heart Rate: (unable to parse)";
                }
            }
            else
            {
                try
                {
                    return "Unknown format: " + Encoding.UTF8.GetString(data);
                }
                catch (ArgumentException)
                {
                    return "Unknown format";
                }
            }
        }
        else
        {
            return "Empty data received";
        }
    }
}
