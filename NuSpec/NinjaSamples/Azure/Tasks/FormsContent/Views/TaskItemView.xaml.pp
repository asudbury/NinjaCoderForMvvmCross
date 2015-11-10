<?xml version="1.0" encoding="utf-8" ?>
<ContentPage x:Class="$rootnamespace$.Views.TaskItemView"
             xmlns="http://xamarin.com/schemas/2014/forms"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             xmlns:viewModels="clr-namespace:$rootnamespace$.Core.ViewModels;assembly=$rootnamespace$.Core"
             Title="Task">

    <ContentPage.Content>

        <StackLayout VerticalOptions="StartAndExpand">

            <Label Text="Name" />
            <Entry Text="{Binding Path=Name}" />

            <Label Text="Notes" />
            <Entry Text="{Binding Path=Notes}"  />

            <Label Text="Complete" />
            <Switch IsToggled="{Binding Path=Complete}"  />

            <Button Text="Save" Command="{Binding SaveCommand}" />
            
            <Button Text="Delete" Command="{Binding DeleteCommand}" />

            <Button Text="Cancel" Command="{Binding CancelCommand}" />

        </StackLayout>
    </ContentPage.Content>
    
</ContentPage>