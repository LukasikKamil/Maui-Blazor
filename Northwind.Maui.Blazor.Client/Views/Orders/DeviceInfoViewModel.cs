using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Maui.Alerts; // Toast
using CommunityToolkit.Maui.Core;

namespace Northwind.Maui.Blazor.Client.Views.Orders;

internal partial class DeviceInfoViewModel : ObservableObject
{
    
    public string DisplayPixelWidth =>
        $"{DeviceDisplay.Current.MainDisplayInfo.Width} pixel width";

    public string DisplayPixelHeight =>
        $"{DeviceDisplay.Current.MainDisplayInfo.Height} pixel height";

    public string DisplayDensity =>
        $"{DeviceDisplay.Current.MainDisplayInfo.Density} pixel density";

    public string DisplayOrientation =>
        $"Orientation is {DeviceDisplay.Current.MainDisplayInfo.Orientation}";

    public string DisplayRotation =>
        $"Rotation is {DeviceDisplay.Current.MainDisplayInfo.Rotation}";

    public string DisplayRefreshRate =>
        $"{DeviceDisplay.Current.MainDisplayInfo.RefreshRate} Hz refresh rate";

    public string DeviceModel => DeviceInfo.Current.Model;

    public string DeviceType => 
        $"{DeviceInfo.Current.DeviceType} {DeviceInfo.Current.Idiom} device";

    public string DeviceManufacturer => DeviceInfo.Current.Manufacturer;

    public string DeviceVersion => DeviceInfo.Current.VersionString;

    public string DevicePlatform => 
        $"Platform is {DeviceInfo.Current.Platform}";

    public string DeviceName => DeviceInfo.Current.Name;

    [RelayCommand]
    private async Task NavigateTo(string pageName)
    {
        await Shell.Current.GoToAsync($"//{pageName}");
    }

    [RelayCommand]
    private async Task PopupToast()
    {
        IToast toast = Toast.Make(message: "This toast pops up.",
                                  duration: ToastDuration.Short,
                                  textSize: 18);

        await toast.Show();
    }
}
