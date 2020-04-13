using LibUsbDotNet.Info;
using LibUsbDotNet.LibUsb;
using System;
using System.Text;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                UsbContext context = new UsbContext();
                var devices = context.List();

                Console.WriteLine("Registered USB devices:");
                foreach (var device in devices)
                {
                    Console.WriteLine($"{device.ToString()}");

                    Console.WriteLine(GetDescriptorReport(device).ToString());
                }

                Console.WriteLine();
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

        private static StringBuilder GetDescriptorReport(IUsbDevice usbRegistry)
        {
            StringBuilder sbReport = new StringBuilder();

            if (!usbRegistry.TryOpen()) return sbReport;

            // sbReport.AppendLine(string.Format("{0} OSVersion:{1} LibUsbDotNet Version:{2} DriverMode:{3}", usbRegistry.Info.SerialNumber, Environment.OSVersion, LibUsbDotNetVersion, null));
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
