using LibUsbDotNet.LibUsb;
using System;

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
                    Console.WriteLine(device.ToString());
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
    }
}
