# LibUsbCore
This is a .NET Core Class Library that allows you to access USB devices.
Using this library you can write code that will work on both Windows on Linux machines. I needed this to write software that accesses USB devices from my Raspberry Pi and I didn't want to have to choose a different framework.

It is based upon LibUsbDotNet which uses is based upon .NET and relies upon Mono.NET. I had problems compiling it in VS2019 as it was using an older MSBuild and it also had some missing references. So I went back to barebones: a simple Class Library that relies soley upon libusb.

Please note: this Class Library acts as a wrapper across libusb - a library written in C that provides the true underlying USB functionality. 

