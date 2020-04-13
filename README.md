# LibUsbCore
This is a .NET Core Class Library that allows you to access USB devices.
Using this library you can write code that will work on both Windows on Linux machines. Who'd have thought that such a ubiquitous device class (USB) would be so difficult to program in .NET Core? I needed this to write software that accesses USB devices from my Raspberry Pi and I didn't want to have to choose a different framework.

## If you just want to start coding
If you want to skip all the reading and learning I had to do, jump to the Quick Start section.

## Background
It is based upon LibUsbDotNet which uses is based upon .NET and relies upon [Mono.NET](https://www.mono-project.com/). I had problems compiling it in VS2019 as it was using an older MSBuild and it also had some missing references. So I went back to barebones: a simple Class Library that relies soley upon [libusb](https://libusb.info/).

Please note: this Class Library acts as a wrapper across libusb - a library written in C that provides the true underlying USB functionality. 

## Running on Windows
Note: libusb still requires you to install WinUsb support as [documented here](https://github.com/libusb/libusb/wiki). If you do not install it and you try and open a device for reading or writing you will get a NotSupported Exception as libusb can enumerate devices [but not open them](https://stackoverflow.com/questions/17350177/libusb-open-returns-libusb-error-not-supported-on-windows-7). 

WinUsb is the Microsoft generic implementation for supporting a USB device. More information on Windows and libusb [is written here](https://github.com/libusb/libusb/wiki/Windows). 

There is a quick solution. I used [Zadig](https://zadig.akeo.ie/) to replace the default driver for my device with a WinUsb driver.

## Acknowledgements
Thank you to Travis Robinson for building the original LibUsbDotNet.

## References
- [LibUsb](https://libusb.info/) - includes the runtime binaries
- [Zadig](https://zadig.akeo.ie/) - replaces default drivers with WinUsb drivers allowing libusb to open them for reading/writing.
- [LibUsb source](https://github.com/libusb/libusb) - the source code for LibUsb
- [LibUsbDotNet help file](http://libusbdotnet.sourceforge.net/V2/Index.html) - includes some sample code
- [LibUsbDotNet on GitHub](https://github.com/LibUsbDotNet/LibUsbDotNet) - an export of the library which is not from the original author. I had trouble compiling this but it does have some useful [help text](https://github.com/LibUsbDotNet/LibUsbDotNet/blob/master/README.md).
- [Device.Net](https://github.com/MelbourneDeveloper/Device.Net) - a similar library - but it is full .NET framework
