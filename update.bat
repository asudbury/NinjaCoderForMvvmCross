@echo off

echo -----------------------------
echo Portable Libraries 
echo -----------------------------
copy C:\Scorchio\Projects\c#\MvvmCross-Binaries-master\MvvmCross-Binaries-master\VS2012\bin\Release\Mvx\Portable\*.*  C:\Scorchio\Projects\c#\NinjaCoderMvvmCross\ProjectTemplates\CoreTemplate\Lib

echo -----------------------------
echo Droid Libraries
echo -----------------------------
copy C:\Scorchio\Projects\c#\MvvmCross-Binaries-master\MvvmCross-Binaries-master\VS2012\bin\Release\Mvx\Droid\*.*  C:\Scorchio\Projects\c#\NinjaCoderMvvmCross\ProjectTemplates\DroidTemplate\Lib

echo -----------------------------
echo iOS Libraries
echo -----------------------------
copy C:\Scorchio\Projects\c#\MvvmCross-Binaries-master\MvvmCross-Binaries-master\VS2012\bin\Release\Mvx\Touch\*.*  C:\Scorchio\Projects\c#\NinjaCoderMvvmCross\ProjectTemplates\IosTemplate\Lib

echo -----------------------------
echo Wpf Libraries
echo -----------------------------
copy C:\Scorchio\Projects\c#\MvvmCross-Binaries-master\MvvmCross-Binaries-master\VS2012\bin\Release\Mvx\Wpf\*.*  C:\Scorchio\Projects\c#\NinjaCoderMvvmCross\ProjectTemplates\WpfTemplate\Lib

echo -----------------------------
echo WindowsPhone Libraries 
echo -----------------------------
copy C:\Scorchio\Projects\c#\MvvmCross-Binaries-master\MvvmCross-Binaries-master\VS2012\bin\Release\Mvx\WindowsPhone\*.*  C:\Scorchio\Projects\c#\NinjaCoderMvvmCross\ProjectTemplates\WindowsPhoneTemplate\Lib

echo -----------------------------
echo WindowsStore Libraries
echo -----------------------------
copy C:\Scorchio\Projects\c#\MvvmCross-Binaries-master\MvvmCross-Binaries-master\VS2012\bin\Release\Mvx\WindowsStore\*.*  C:\Scorchio\Projects\c#\NinjaCoderMvvmCross\ProjectTemplates\WindowsStoreTemplate\Lib
