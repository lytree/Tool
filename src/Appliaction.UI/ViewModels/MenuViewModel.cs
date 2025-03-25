using System;
using System.Collections.ObjectModel;

namespace Appliaction.UI.ViewModels;

public class MenuViewModel : ViewModelBase
{
    public ObservableCollection<MenuItemViewModel> MenuItems { get; set; }

    public MenuViewModel()
    {
        MenuItems = new ObservableCollection<MenuItemViewModel>()
        {
            new() { MenuHeader = "Introduction", Key = MenuKeys.MenuKeyIntroduction, IsSeparator = false },
        };
    }
}
public static class MenuKeys
{
    public const string MenuKeyIntroduction = "Introduction";
}