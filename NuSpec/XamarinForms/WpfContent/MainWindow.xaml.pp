<controls:WpfApplicationPage x:Class="$rootnamespace$.MainWindow"
                             xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
                             xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
                             xmlns:controls="clr-namespace:Xamarin.Controls;assembly=Xamarin.Forms.Xaml.UnitTests"
                             xmlns:local="clr-namespace:$rootnamespace$"
                             xmlns:mui="http://firstfloorsoftware.com/ModernUI"
                             Title="Main Window"
                             Width="480"
                             Height="800"
                             Style="{StaticResource BlankWindow}">
  <Grid>
    <Grid.RowDefinitions>
      <RowDefinition Height="*" />
      <RowDefinition Height="Auto" />
    </Grid.RowDefinitions>

    <ContentPresenter x:Name="contentPresenter" Grid.Row="0" />

    <StackPanel Grid.Row="1" Orientation="Horizontal">
      <ListView ItemsSource="{Binding ApplicationBar.Buttons, RelativeSource={RelativeSource FindAncestor, AncestorType={x:Type local:MainWindow}}}">

        <ListView.ItemTemplate>
          <DataTemplate>
            <mui:ModernButton Command="{Binding }" />
          </DataTemplate>
        </ListView.ItemTemplate>

      </ListView>

      <!--  back button  -->
      <mui:ModernButton HorizontalAlignment="Left"
                        VerticalAlignment="Top"
                        Click="BackClick"
                        EllipseDiameter="24"
                        IconData="F1 M 33,22L 33,26L 19.75,26L 27,33L 20.5,33L 11,24L 20.5,15L 27,15L 19.75,22L 33,22 Z"
                        IconHeight="12"
                        IconWidth="12"
                        WindowChrome.IsHitTestVisibleInChrome="True" />

    </StackPanel>
  </Grid>
</controls:WpfApplicationPage>

