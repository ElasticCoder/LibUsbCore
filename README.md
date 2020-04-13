# LibUsbCore
This is a .NET Core Class Library that allows you to access USB devices.
Using this library you can write code that will work on both Windows on Linux machines. I needed this to write software that accesses USB devices from my Raspberry Pi and I didn't want to have to choose a different framework.

It is based upon LibUsbDotNet which uses is based upon .NET and relies upon [Mono.NET](https://www.mono-project.com/). I had problems compiling it in VS2019 as it was using an older MSBuild and it also had some missing references. So I went back to barebones: a simple Class Library that relies soley upon [libusb](https://libusb.info/).

Please note: this Class Library acts as a wrapper across libusb - a library written in C that provides the true underlying USB functionality. 

### Acknowledgements
Thank you to Travis Robinson for building the original LibUsbDotNet.

### References
- [LibUsb](https://libusb.info/) - includes the runtime binaries
- [LibUsb source](https://github.com/libusb/libusb) - the source code for LibUsb
- [LibUsbDotNet help file](http://libusbdotnet.sourceforge.net/V2/Index.html) - includes some sample code
- [LibUsbDotNet on GitHub](https://github.com/LibUsbDotNet/LibUsbDotNet) - an export of the library which is not from the original author. I had trouble compiling this but it does have some useful [help text](https://github.com/LibUsbDotNet/LibUsbDotNet/blob/master/README.md).

