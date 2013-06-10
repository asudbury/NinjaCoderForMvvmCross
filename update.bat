@echo off

echo -----------------------------
echo Project templates - Portable Libraries 
echo -----------------------------
copy C:\Scorchio\Projects\c#\MvvmCross-Binaries-master\MvvmCross-Binaries-master\VS2012\bin\Release\Mvx\Portable\*.*  C:\Scorchio\Projects\c#\NinjaCoderMvvmCross\ProjectTemplates\CoreTemplate\Lib

echo -----------------------------
echo Project templates - Droid Libraries
echo -----------------------------
copy C:\Scorchio\Projects\c#\MvvmCross-Binaries-master\MvvmCross-Binaries-master\VS2012\bin\Release\Mvx\Droid\*.*  C:\Scorchio\Projects\c#\NinjaCoderMvvmCross\ProjectTemplates\DroidTemplate\Lib

echo -----------------------------
echo Project templates - iOS Libraries
echo -----------------------------
copy C:\Scorchio\Projects\c#\MvvmCross-Binaries-master\MvvmCross-Binaries-master\VS2012\bin\Release\Mvx\Touch\*.*  C:\Scorchio\Projects\c#\NinjaCoderMvvmCross\ProjectTemplates\IosTemplate\Lib

echo -----------------------------
echo Project templates - Wpf Libraries
echo -----------------------------
copy C:\Scorchio\Projects\c#\MvvmCross-Binaries-master\MvvmCross-Binaries-master\VS2012\bin\Release\Mvx\Wpf\*.*  C:\Scorchio\Projects\c#\NinjaCoderMvvmCross\ProjectTemplates\WpfTemplate\Lib

echo -----------------------------
echo Project templates - WindowsPhone Libraries 
echo -----------------------------
copy C:\Scorchio\Projects\c#\MvvmCross-Binaries-master\MvvmCross-Binaries-master\VS2012\bin\Release\Mvx\WindowsPhone\*.*  C:\Scorchio\Projects\c#\NinjaCoderMvvmCross\ProjectTemplates\WindowsPhoneTemplate\Lib

echo -----------------------------
echo Project templates - WindowsStore Libraries
echo -----------------------------
copy C:\Scorchio\Projects\c#\MvvmCross-Binaries-master\MvvmCross-Binaries-master\VS2012\bin\Release\Mvx\WindowsStore\*.*  C:\Scorchio\Projects\c#\NinjaCoderMvvmCross\ProjectTemplates\WindowsStoreTemplate\Lib

echo -----------------------------
echo Zipper - Amend Project Templates
echo -----------------------------
cd C:\Scorchio\Projects\c#\NinjaCoderMvvmCross\Zipper\bin\Debug\

echo -----------------------------
echo Zipper - Amend Core Template
echo -----------------------------
Zipper C:\Scorchio\Projects\c#\NinjaCoderMvvmCross\ProjectTemplates\Zips\MvvmCross.Core.zip C:\Scorchio\Projects\c#\NinjaCoderMvvmCross\ProjectTemplates\CoreTemplate\Lib

echo -----------------------------
echo Zipper - Amend Droid Template
echo -----------------------------
Zipper C:\Scorchio\Projects\c#\NinjaCoderMvvmCross\ProjectTemplates\Zips\MvvmCross.Droid.zip C:\Scorchio\Projects\c#\NinjaCoderMvvmCross\ProjectTemplates\DroidTemplate\Lib

echo -----------------------------
echo Zipper - Amend iOS Template
echo -----------------------------
Zipper C:\Scorchio\Projects\c#\NinjaCoderMvvmCross\ProjectTemplates\Zips\MvvmCross.iOS.zip C:\Scorchio\Projects\c#\NinjaCoderMvvmCross\ProjectTemplates\IosTemplate\Lib

echo -----------------------------
echo Zipper - Amend WindowsPhone Template
echo -----------------------------
Zipper C:\Scorchio\Projects\c#\NinjaCoderMvvmCross\ProjectTemplates\Zips\MvvmCross.WindowsPhone.zip C:\Scorchio\Projects\c#\NinjaCoderMvvmCross\ProjectTemplates\WindowsPhoneTemplate\Lib

echo -----------------------------
echo Zipper - Amend WindowsStore Template
echo -----------------------------
Zipper C:\Scorchio\Projects\c#\NinjaCoderMvvmCross\ProjectTemplates\Zips\MvvmCross.WindowsStore.zip C:\Scorchio\Projects\c#\NinjaCoderMvvmCross\ProjectTemplates\WindowsStoreTemplate\Lib

echo -----------------------------
echo Zipper - Amend Wpf Template
echo -----------------------------
Zipper C:\Scorchio\Projects\c#\NinjaCoderMvvmCross\ProjectTemplates\Zips\MvvmCross.Wpf.zip C:\Scorchio\Projects\c#\NinjaCoderMvvmCross\ProjectTemplates\WpfTemplate\Lib







