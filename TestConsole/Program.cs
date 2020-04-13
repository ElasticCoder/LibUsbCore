using LibUsbDotNet.Info;
using LibUsbDotNet.LibUsb;
using LibUsbDotNet.Main;
using System;
using System.Text;
using System.Linq;

namespace TestConsole
{
    class Program
    {
        private const int timeout = 3000;

        static void Main(string[] args)
        {
            try
            {
                using var context = new UsbContext();
                var devices = context.List();
                Console.WriteLine();
                var selectedDeviceNum = 0;

                while (true)
                {
                    Console.WriteLine();
                    Console.WriteLine("Select a device (or X to exit):");
                    ListDevices(devices);
                    var input = Console.ReadLine();
                    if (input.ToLower().Trim() == "x") return;
                    if (int.TryParse(input, out selectedDeviceNum)) break;
                    Console.WriteLine($"Invalid number: select a device from 0 to {devices.Count}");
                }

                var selectedDevice = devices[selectedDeviceNum];
                selectedDevice.Open();
                var writeEndpoint = selectedDevice.OpenEndpointWriter(WriteEndpointID.Ep01);
                var readEnpoint = selectedDevice.OpenEndpointReader(ReadEndpointID.Ep01);

                while (true)
                {
                    Console.WriteLine();
                    Console.WriteLine("Enter the data to send (or a blank line to exit):");
                    var input = Console.ReadLine();
                    if (string.IsNullOrEmpty(input)) break;

                    var buffer = new byte[1];
                    var readBuffer = new byte[64];

                    buffer[0] = 0x2B;
                    
                    writeEndpoint.Write(buffer, timeout, out var bytesWritten);
                    readEnpoint.Read(readBuffer, timeout, out var readBytes);

                    if (readBytes == 0) Console.WriteLine("null");
                    Console.WriteLine(string.Join(" ", readBuffer.Take(readBytes).Select(b => b.ToString())));
                }
            }
            catch (Exception error)
            {
                Console.WriteLine($"An {error.GetType()} occurred:");
                while (error != null)
                {
                    Console.WriteLine(error.Message);
                    error = error.InnerException;
                }
            }
        }

        private static void ListDevices(UsbDeviceCollection devices)
        {
            Console.WriteLine("Registered USB devices:");
            for (var num = 0; num < devices.Count; num++)
            {
                var device = devices[num];
                Console.WriteLine($"{num}: {device}");
                Console.Write(GetDescriptorReport(device).ToString());
            }
        }

        private static StringBuilder GetDescriptorReport(IUsbDevice usbRegistry)
        {
            StringBuilder sbReport = new StringBuilder();

            if (!usbRegistry.TryOpen()) return sbReport;

            sbReport.AppendLine(usbRegistry.Info.ToString());
            foreach (UsbConfigInfo cfgInfo in usbRegistry.Configs)
            {
                sbReport.AppendLine(string.Format("CONFIG #{1}\r\n{0}", cfgInfo.ToString(), cfgInfo.ConfigurationValue));
                foreach (UsbInterfaceInfo interfaceInfo in cfgInfo.Interfaces)
                {
                    sbReport.AppendLine(string.Format("INTERFACE ({1},{2})\r\n{0}", interfaceInfo.ToString(), interfaceInfo.Number, interfaceInfo.AlternateSetting));

                    foreach (UsbEndpointInfo endpointInfo in interfaceInfo.Endpoints)
                    {
                        sbReport.AppendLine(string.Format("ENDPOINT 0x{1:X2}\r\n{0}", endpointInfo.ToString(), endpointInfo.EndpointAddress));
                    }
                }
            }
            usbRegistry.Close();

            return sbReport;
        }
    }
}
