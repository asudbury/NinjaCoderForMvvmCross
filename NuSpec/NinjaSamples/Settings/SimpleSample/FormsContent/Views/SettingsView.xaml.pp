<?xml version="1.0" encoding="utf-8" ?>
<ContentPage x:Class="$rootnamespace$.Views.SettingsView"
             xmlns="http://xamarin.com/schemas/2014/forms"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             xmlns:viewModels="clr-namespace:$rootnamespace$.Core.ViewModels;assembly=$rootnamespace$.Core"
             Title="SettingsView Page">

    <ContentView>
        <Label HorizontalOptions="Center"
               Text="{Binding Greeting}"
               VerticalOptions="Center" />
    </ContentView>
</ContentPage>