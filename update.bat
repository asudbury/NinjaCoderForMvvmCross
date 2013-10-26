@echo off

echo -----------------------------
echo Project templates - Core Libraries 
echo -----------------------------
copy b:\Scorchio\Projects\c#\MvvmCross-Binaries-master\MvvmCross-Binaries-master\VS2012\bin\Release\Mvx\Portable\*.dll  b:\Scorchio\Projects\c#\NinjaCoderForMvvmCross\ProjectTemplates\CoreTemplate\Lib
del b:\Scorchio\Projects\c#\NinjaCoderForMvvmCross\ProjectTemplates\CoreTemplate\Lib\Cirrious.MvvmCross.Plugins.Sqlite.dll

echo -----------------------------
echo Project templates - Core Test Libraries 
echo -----------------------------

copy b:\Scorchio\Projects\c#\MvvmCross-Binaries-master\MvvmCross-Binaries-master\VS2012\bin\Release\Mvx\Console\Cirrious.MvvmCross.dll  b:\Scorchio\Projects\c#\NinjaCoderForMvvmCross\ProjectTemplates\CoreTestsTemplate\Lib
copy b:\Scorchio\Projects\c#\MvvmCross-Binaries-master\MvvmCross-Binaries-master\VS2012\bin\Release\Mvx\Console\Cirrious.CrossCore.dll  b:\Scorchio\Projects\c#\NinjaCoderForMvvmCross\ProjectTemplates\CoreTestsTemplate\Lib
copy b:\Scorchio\Projects\c#\MvvmCross-Binaries-master\MvvmCross-Binaries-master\VS2012\bin\Release\Mvx\Console\Moq.dll  b:\Scorchio\Projects\c#\NinjaCoderForMvvmCross\ProjectTemplates\CoreTestsTemplate\Lib
copy b:\Scorchio\Projects\c#\MvvmCross-Binaries-master\MvvmCross-Binaries-master\VS2012\bin\Release\Mvx\Console\nunit.framework.dll  b:\Scorchio\Projects\c#\NinjaCoderForMvvmCross\ProjectTemplates\CoreTestsTemplate\Lib
copy b:\Scorchio\Projects\c#\MvvmCross-Binaries-master\MvvmCross-Binaries-master\VS2012\bin\Release\Mvx\Console\Cirrious.MvvmCross.Test.Core.dll  b:\Scorchio\Projects\c#\NinjaCoderForMvvmCross\ProjectTemplates\CoreTestsTemplate\Lib

echo -----------------------------
echo Project templates - Droid Libraries
echo -----------------------------

copy b:\Scorchio\Projects\c#\MvvmCross-Binaries-master\MvvmCross-Binaries-master\VS2012\bin\Release\Mvx\Droid\Cirrious.MvvmCross.dll  b:\Scorchio\Projects\c#\NinjaCoderForMvvmCross\ProjectTemplates\DroidTemplate\Lib
copy b:\Scorchio\Projects\c#\MvvmCross-Binaries-master\MvvmCross-Binaries-master\VS2012\bin\Release\Mvx\Droid\Cirrious.CrossCore.dll  b:\Scorchio\Projects\c#\NinjaCoderForMvvmCross\ProjectTemplates\DroidTemplate\Lib
copy b:\Scorchio\Projects\c#\MvvmCross-Binaries-master\MvvmCross-Binaries-master\VS2012\bin\Release\Mvx\Droid\Cirrious.CrossCore.Droid.dll  b:\Scorchio\Projects\c#\NinjaCoderForMvvmCross\ProjectTemplates\DroidTemplate\Lib
copy b:\Scorchio\Projects\c#\MvvmCross-Binaries-master\MvvmCross-Binaries-master\VS2012\bin\Release\Mvx\Droid\Cirrious.MvvmCross.Droid.dll  b:\Scorchio\Projects\c#\NinjaCoderForMvvmCross\ProjectTemplates\DroidTemplate\Lib
copy b:\Scorchio\Projects\c#\MvvmCross-Binaries-master\MvvmCross-Binaries-master\VS2012\bin\Release\Mvx\Portable\Cirrious.MvvmCross.Plugins.Messenger.dll  b:\Scorchio\Projects\c#\NinjaCoderForMvvmCross\ProjectTemplates\DroidTemplate\Lib
copy b:\Scorchio\Projects\c#\MvvmCross-Binaries-master\MvvmCross-Binaries-master\VS2012\bin\Release\Mvx\Droid\Cirrious.MvvmCross.Binding.dll  b:\Scorchio\Projects\c#\NinjaCoderForMvvmCross\ProjectTemplates\DroidTemplate\Lib
copy b:\Scorchio\Projects\c#\MvvmCross-Binaries-master\MvvmCross-Binaries-master\VS2012\bin\Release\Mvx\Droid\Cirrious.MvvmCross.Binding.Droid.dll  b:\Scorchio\Projects\c#\NinjaCoderForMvvmCross\ProjectTemplates\DroidTemplate\Lib
copy b:\Scorchio\Projects\c#\MvvmCross-Binaries-master\MvvmCross-Binaries-master\VS2012\bin\Release\Mvx\Droid\Cirrious.MvvmCross.Localization.dll  b:\Scorchio\Projects\c#\NinjaCoderForMvvmCross\ProjectTemplates\DroidTemplate\Lib
copy b:\Scorchio\Projects\c#\MvvmCross-Binaries-master\MvvmCross-Binaries-master\VS2012\bin\Release\Mvx\Droid\*Plugins*Droid.dll  b:\Scorchio\Projects\c#\NinjaCoderForMvvmCross\ProjectTemplates\DroidTemplate\Lib
del b:\Scorchio\Projects\c#\NinjaCoderForMvvmCross\ProjectTemplates\DroidTemplate\Lib\Cirrious.MvvmCross.Plugins.Sqlite.Droid.dll

echo -----------------------------
echo Project templates - iOS Libraries
echo -----------------------------

copy b:\Scorchio\Projects\c#\MvvmCross-Binaries-master\MvvmCross-Binaries-master\VS2012\bin\Release\Mvx\Touch\Cirrious.MvvmCross.dll  b:\Scorchio\Projects\c#\NinjaCoderForMvvmCross\ProjectTemplates\IosTemplate\Lib
copy b:\Scorchio\Projects\c#\MvvmCross-Binaries-master\MvvmCross-Binaries-master\VS2012\bin\Release\Mvx\Touch\Cirrious.CrossCore.dll  b:\Scorchio\Projects\c#\NinjaCoderForMvvmCross\ProjectTemplates\IosTemplate\Lib
copy b:\Scorchio\Projects\c#\MvvmCross-Binaries-master\MvvmCross-Binaries-master\VS2012\bin\Release\Mvx\Touch\Cirrious.CrossCore.Touch.dll  b:\Scorchio\Projects\c#\NinjaCoderForMvvmCross\ProjectTemplates\IosTemplate\Lib
copy b:\Scorchio\Projects\c#\MvvmCross-Binaries-master\MvvmCross-Binaries-master\VS2012\bin\Release\Mvx\Touch\Cirrious.MvvmCross.Touch.dll  b:\Scorchio\Projects\c#\NinjaCoderForMvvmCross\ProjectTemplates\IosTemplate\Lib
copy b:\Scorchio\Projects\c#\MvvmCross-Binaries-master\MvvmCross-Binaries-master\VS2012\bin\Release\Mvx\Portable\Cirrious.MvvmCross.Plugins.Messenger.dll  b:\Scorchio\Projects\c#\NinjaCoderForMvvmCross\ProjectTemplates\IosTemplate\Lib
copy b:\Scorchio\Projects\c#\MvvmCross-Binaries-master\MvvmCross-Binaries-master\VS2012\bin\Release\Mvx\Touch\Cirrious.MvvmCross.Binding.dll  b:\Scorchio\Projects\c#\NinjaCoderForMvvmCross\ProjectTemplates\IosTemplate\Lib
copy b:\Scorchio\Projects\c#\MvvmCross-Binaries-master\MvvmCross-Binaries-master\VS2012\bin\Release\Mvx\Touch\Cirrious.MvvmCross.Binding.Touch.dll  b:\Scorchio\Projects\c#\NinjaCoderForMvvmCross\ProjectTemplates\IosTemplate\Lib
copy b:\Scorchio\Projects\c#\MvvmCross-Binaries-master\MvvmCross-Binaries-master\VS2012\bin\Release\Mvx\Touch\Cirrious.MvvmCross.Localization.dll  b:\Scorchio\Projects\c#\NinjaCoderForMvvmCross\ProjectTemplates\IosTemplate\Lib
copy b:\Scorchio\Projects\c#\MvvmCross-Binaries-master\MvvmCross-Binaries-master\VS2012\bin\Release\Mvx\Touch\*Plugins*Touch.dll  b:\Scorchio\Projects\c#\NinjaCoderForMvvmCross\ProjectTemplates\IosTemplate\Lib
del b:\Scorchio\Projects\c#\NinjaCoderForMvvmCross\ProjectTemplates\IosTemplate\Lib\Cirrious.MvvmCross.Plugins.Sqlite.Touch.dll

echo -----------------------------
echo Project templates - Wpf Libraries
echo -----------------------------

copy b:\Scorchio\Projects\c#\MvvmCross-Binaries-master\MvvmCross-Binaries-master\VS2012\bin\Release\Mvx\Wpf\Cirrious.MvvmCross.dll  b:\Scorchio\Projects\c#\NinjaCoderForMvvmCross\ProjectTemplates\WpfTemplate\Lib
copy b:\Scorchio\Projects\c#\MvvmCross-Binaries-master\MvvmCross-Binaries-master\VS2012\bin\Release\Mvx\Wpf\Cirrious.CrossCore.dll  b:\Scorchio\Projects\c#\NinjaCoderForMvvmCross\ProjectTemplates\WpfTemplate\Lib
copy b:\Scorchio\Projects\c#\MvvmCross-Binaries-master\MvvmCross-Binaries-master\VS2012\bin\Release\Mvx\Wpf\Cirrious.MvvmCross.Localization.dll  b:\Scorchio\Projects\c#\NinjaCoderForMvvmCross\ProjectTemplates\WpfTemplate\Lib
copy b:\Scorchio\Projects\c#\MvvmCross-Binaries-master\MvvmCross-Binaries-master\VS2012\bin\Release\Mvx\Wpf\Cirrious.MvvmCross.Wpf.dll  b:\Scorchio\Projects\c#\NinjaCoderForMvvmCross\ProjectTemplates\WpfTemplate\Lib
copy b:\Scorchio\Projects\c#\MvvmCross-Binaries-master\MvvmCross-Binaries-master\VS2012\bin\Release\Mvx\Wpf\*Plugins*Wpf.dll  b:\Scorchio\Projects\c#\NinjaCoderForMvvmCross\ProjectTemplates\WpfTemplate\Lib

echo -----------------------------
echo Project templates - WindowsPhone Libraries 
echo -----------------------------

copy b:\Scorchio\Projects\c#\MvvmCross-Binaries-master\MvvmCross-Binaries-master\VS2012\bin\Release\Mvx\WindowsPhone\Cirrious.MvvmCross.dll  b:\Scorchio\Projects\c#\NinjaCoderForMvvmCross\ProjectTemplates\WindowsPhoneTemplate\Lib
copy b:\Scorchio\Projects\c#\MvvmCross-Binaries-master\MvvmCross-Binaries-master\VS2012\bin\Release\Mvx\WindowsPhone\Cirrious.CrossCore.dll  b:\Scorchio\Projects\c#\NinjaCoderForMvvmCross\ProjectTemplates\WindowsPhoneTemplate\Lib
copy b:\Scorchio\Projects\c#\MvvmCross-Binaries-master\MvvmCross-Binaries-master\VS2012\bin\Release\Mvx\WindowsPhone\Cirrious.CrossCore.WindowsPhone.dll  b:\Scorchio\Projects\c#\NinjaCoderForMvvmCross\ProjectTemplates\WindowsPhoneTemplate\Lib
copy b:\Scorchio\Projects\c#\MvvmCross-Binaries-master\MvvmCross-Binaries-master\VS2012\bin\Release\Mvx\WindowsPhone\Cirrious.MvvmCross.WindowsPhone.dll  b:\Scorchio\Projects\c#\NinjaCoderForMvvmCross\ProjectTemplates\WindowsPhoneTemplate\Lib
copy b:\Scorchio\Projects\c#\MvvmCross-Binaries-master\MvvmCross-Binaries-master\VS2012\bin\Release\Mvx\Portable\Cirrious.MvvmCross.Plugins.Messenger.dll  b:\Scorchio\Projects\c#\NinjaCoderForMvvmCross\ProjectTemplates\WindowsPhoneTemplate\Lib
copy b:\Scorchio\Projects\c#\MvvmCross-Binaries-master\MvvmCross-Binaries-master\VS2012\bin\Release\Mvx\WindowsPhone\*Plugins*WindowsPhone.dll  b:\Scorchio\Projects\c#\NinjaCoderForMvvmCross\ProjectTemplates\WindowsPhoneTemplate\Lib
del b:\Scorchio\Projects\c#\NinjaCoderForMvvmCross\ProjectTemplates\WindowsPhoneTemplate\Lib\Cirrious.MvvmCross.Plugins.Sqlite.WindowsPhone.dll

echo -----------------------------
echo Project templates - WindowsStore Libraries
echo -----------------------------

copy b:\Scorchio\Projects\c#\MvvmCross-Binaries-master\MvvmCross-Binaries-master\VS2012\bin\Release\Mvx\WindowsStore\Cirrious.MvvmCross.dll  b:\Scorchio\Projects\c#\NinjaCoderForMvvmCross\ProjectTemplates\WindowsStoreTemplate\Lib
copy b:\Scorchio\Projects\c#\MvvmCross-Binaries-master\MvvmCross-Binaries-master\VS2012\bin\Release\Mvx\WindowsStore\Cirrious.CrossCore.dll  b:\Scorchio\Projects\c#\NinjaCoderForMvvmCross\ProjectTemplates\WindowsStoreTemplate\Lib
copy b:\Scorchio\Projects\c#\MvvmCross-Binaries-master\MvvmCross-Binaries-master\VS2012\bin\Release\Mvx\WindowsStore\Cirrious.CrossCore.WindowsStore.dll  b:\Scorchio\Projects\c#\NinjaCoderForMvvmCross\ProjectTemplates\WindowsStoreTemplate\Lib
copy b:\Scorchio\Projects\c#\MvvmCross-Binaries-master\MvvmCross-Binaries-master\VS2012\bin\Release\Mvx\WindowsStore\Cirrious.MvvmCross.WindowsStore.dll  b:\Scorchio\Projects\c#\NinjaCoderForMvvmCross\ProjectTemplates\WindowsStoreTemplate\Lib
copy b:\Scorchio\Projects\c#\MvvmCross-Binaries-master\MvvmCross-Binaries-master\VS2012\bin\Release\Mvx\Portable\Cirrious.MvvmCross.Plugins.Messenger.dll  b:\Scorchio\Projects\c#\NinjaCoderForMvvmCross\ProjectTemplates\WindowsStoreTemplate\Lib
copy b:\Scorchio\Projects\c#\MvvmCross-Binaries-master\MvvmCross-Binaries-master\VS2012\bin\Release\Mvx\WindowsStore\*Plugins*WindowsStore.dll  b:\Scorchio\Projects\c#\NinjaCoderForMvvmCross\ProjectTemplates\WindowsStoreTemplate\Lib

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







