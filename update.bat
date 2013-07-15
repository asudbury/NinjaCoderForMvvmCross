@echo off

echo -----------------------------
echo Project templates - Portable Libraries 
echo -----------------------------
copy b:\Scorchio\Projects\c#\MvvmCross-Binaries-master\MvvmCross-Binaries-master\VS2012\bin\Release\Mvx\Portable\*.*  b:\Scorchio\Projects\c#\
NinjaCoderForMvvmCross\ProjectTemplates\CoreTemplate\Lib

echo -----------------------------
echo Project templates - Droid Libraries
echo -----------------------------
copy b:\Scorchio\Projects\c#\MvvmCross-Binaries-master\MvvmCross-Binaries-master\VS2012\bin\Release\Mvx\Droid\*.*  b:\Scorchio\Projects\c#\NinjaCoderForMvvmCross\ProjectTemplates\DroidTemplate\Lib

echo -----------------------------
echo Project templates - iOS Libraries
echo -----------------------------
copy b:\Scorchio\Projects\c#\MvvmCross-Binaries-master\MvvmCross-Binaries-master\VS2012\bin\Release\Mvx\Touch\*.*  b:\Scorchio\Projects\c#\NinjaCoderForMvvmCross\ProjectTemplates\IosTemplate\Lib

echo -----------------------------
echo Project templates - Wpf Libraries
echo -----------------------------
copy b:\Scorchio\Projects\c#\MvvmCross-Binaries-master\MvvmCross-Binaries-master\VS2012\bin\Release\Mvx\Wpf\*.*  b:\Scorchio\Projects\c#\NinjaCoderForMvvmCross\ProjectTemplates\WpfTemplate\Lib

echo -----------------------------
echo Project templates - WindowsPhone Libraries 
echo -----------------------------
copy b:\Scorchio\Projects\c#\MvvmCross-Binaries-master\MvvmCross-Binaries-master\VS2012\bin\Release\Mvx\WindowsPhone\*.*  b:\Scorchio\Projects\c#\NinjaCoderForMvvmCross\ProjectTemplates\WindowsPhoneTemplate\Lib

echo -----------------------------
echo Project templates - WindowsStore Libraries
echo -----------------------------
copy b:\Scorchio\Projects\c#\MvvmCross-Binaries-master\MvvmCross-Binaries-master\VS2012\bin\Release\Mvx\WindowsStore\*.*  b:\Scorchio\Projects\c#\NinjaCoderForMvvmCross\ProjectTemplates\WindowsStoreTemplate\Lib

echo -----------------------------
echo Zipper - Amend Project Templates
echo -----------------------------
cd b:\Scorchio\Projects\c#\NinjaCoderForMvvmCross\Zipper\bin\Debug\

echo -----------------------------
echo Zipper - Amend Core Template
echo -----------------------------
Zipper b:\Scorchio\Projects\c#\NinjaCoderForMvvmCross\ProjectTemplates\Zips\MvvmCross.Core.zip b:\Scorchio\Projects\c#\NinjaCoderForMvvmCross\ProjectTemplates\CoreTemplate\Lib

echo -----------------------------
echo Zipper - Amend Core Tests Template
echo -----------------------------
Zipper b:\Scorchio\Projects\c#\NinjaCoderForMvvmCross\ProjectTemplates\Zips\MvvmCross.Core.Tests.zip b:\Scorchio\Projects\c#\NinjaCoderForMvvmCross\ProjectTemplates\CoreTemplate\Lib

echo -----------------------------
echo Zipper - Amend Droid Template
echo -----------------------------
Zipper b:\Scorchio\Projects\c#\NinjaCoderForMvvmCross\ProjectTemplates\Zips\MvvmCross.Droid.zip b:\Scorchio\Projects\c#\NinjaCoderForMvvmCross\ProjectTemplates\DroidTemplate\Lib

echo -----------------------------
echo Zipper - Amend iOS Template
echo -----------------------------
Zipper b:\Scorchio\Projects\c#\NinjaCoderForMvvmCross\ProjectTemplates\Zips\MvvmCross.iOS.zip b:\Scorchio\Projects\c#\NinjaCoderForMvvmCross\ProjectTemplates\IosTemplate\Lib

echo -----------------------------
echo Zipper - Amend WindowsPhone Template
echo -----------------------------
Zipper b:\Scorchio\Projects\c#\NinjaCoderForMvvmCross\ProjectTemplates\Zips\MvvmCross.WindowsPhone.zip b:\Scorchio\Projects\c#\NinjaCoderForMvvmCross\ProjectTemplates\WindowsPhoneTemplate\Lib

echo -----------------------------
echo Zipper - Amend WindowsStore Template
echo -----------------------------
Zipper b:\Scorchio\Projects\c#\NinjaCoderForMvvmCross\ProjectTemplates\Zips\MvvmCross.WindowsStore.zip b:\Scorchio\Projects\c#\NinjaCoderForMvvmCross\ProjectTemplates\WindowsStoreTemplate\Lib

echo -----------------------------
echo Zipper - Amend Wpf Template
echo -----------------------------
Zipper b:\Scorchio\Projects\c#\NinjaCoderForMvvmCross\ProjectTemplates\Zips\MvvmCross.Wpf.zip b:\Scorchio\Projects\c#\NinjaCoderForMvvmCross\ProjectTemplates\WpfTemplate\Lib







