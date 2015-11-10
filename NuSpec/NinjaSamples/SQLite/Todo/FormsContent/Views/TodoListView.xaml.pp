<?xml version="1.0" encoding="utf-8" ?>
<ContentPage x:Class="$rootnamespace$.Views.TodoListView"
             xmlns="http://xamarin.com/schemas/2014/forms"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             xmlns:viewModels="clr-namespace:$rootnamespace$.Core.ViewModels;assembly=$rootnamespace$.Core"
             Title="Todo List">

    <ContentPage.Content>
        <ListView SelectedItem="{Binding SelectedItem}" ItemTapped="{Binding UpdateItemCommand}" >
          <ListView.ItemTemplate>
            <DataTemplate>
              <ViewCell>
                 <StackLayout Padding="20,0,0,0" HorizontalOptions="StartAndExpand" Orientation="Horizontal">
                    <Label Text="{Binding Name}" YAlign="Center" />
                 </StackLayout>
              </ViewCell>
            </DataTemplate>
          </ListView.ItemTemplate>
        </ListView>
        
        <Button Text="Add" Command="{Binding AddItemCommand}" />
                    
    </ContentPage.Content>
    
</ContentPage>
