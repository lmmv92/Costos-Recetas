using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CostosRecetas.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CostosRecetas.ViewModels;

public partial class SettingsViewModel : ObservableObject
{
    [ObservableProperty]
    bool toggleScreenOn = Preferences.Default.Get(Constants.KeepScreenOn, false);

    [RelayCommand]
    public void ToggleKeepScreenOn() {
        Preferences.Default.Set(Constants.KeepScreenOn, ToggleScreenOn);
    }
}
