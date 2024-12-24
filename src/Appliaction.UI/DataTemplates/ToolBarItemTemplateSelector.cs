using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Controls.Templates;
using Avalonia.Data;
using Ursa.Controls;
using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Appliaction.UI.ViewModels;

namespace Appliaction.UI.Converters;

public class ToolBarItemTemplateSelector : IDataTemplate
{

    public static ToolBarItemTemplateSelector Instance { get; } = new();
    public Control? Build(object? param)
    {
        if (param is null) return null;
        if (param is ToolBarSeparatorViewModel)
        {
            return new ToolBarSeparator();
        }
        if (param is ToolBarButtonItemViewModel)
        {
            return new Button()
            {
                [!ContentControl.ContentProperty] = new Binding() { Path = "Content" },
                [!Button.CommandProperty] = new Binding() { Path = "Command" },
                [!ToolBar.OverflowModeProperty] = new Binding() { Path = nameof(ToolBarItemViewModel.OverflowMode) },
            };
        }
        if (param is ToolBarCheckBoxItemViweModel)
        {
            return new CheckBox()
            {
                [!ContentControl.ContentProperty] = new Binding() { Path = "Content" },
                [!ToggleButton.IsCheckedProperty] = new Binding() { Path = "IsChecked" },
                [!ToolBar.OverflowModeProperty] = new Binding() { Path = nameof(ToolBarItemViewModel.OverflowMode) },
            };
        }
        if (param is ToolBarComboBoxItemViewModel)
        {
            return new ComboBox()
            {
                [!ContentControl.ContentProperty] = new Binding() { Path = "Content" },
                [!SelectingItemsControl.SelectedItemProperty] = new Binding() { Path = "SelectedItem" },
                [!ItemsControl.ItemsSourceProperty] = new Binding() { Path = "Items" },
                [!ToolBar.OverflowModeProperty] = new Binding() { Path = nameof(ToolBarItemViewModel.OverflowMode) },
            };
        }
        return new Button() { Content = "Undefined Item" };
    }

    public bool Match(object? data)
    {
        return data is ToolBarItemViewModel;
    }
}

public abstract class ToolBarItemViewModel : ObservableObject
{
    public OverflowMode OverflowMode { get; set; }
}

public class ToolBarButtonItemViewModel : ToolBarItemViewModel
{
    public string? Content { get; set; }
    public ICommand? Command { get; set; }

    public ToolBarButtonItemViewModel()
    {
        Command = new AsyncRelayCommand(async () => { await MessageBox.ShowOverlayAsync(Content ?? string.Empty); });
    }
}

public class ToolBarCheckBoxItemViweModel : ToolBarItemViewModel
{
    public string? Content { get; set; }
    public bool IsChecked { get; set; }
    public ICommand? Command { get; set; }

    public ToolBarCheckBoxItemViweModel()
    {
        Command = new AsyncRelayCommand(async () => { await MessageBox.ShowOverlayAsync(Content ?? string.Empty); });
    }
}

public class ToolBarComboBoxItemViewModel : ToolBarItemViewModel
{
    public string? Content { get; set; }
    public ObservableCollection<string>? Items { get; set; }

    private string? _selectedItem;

    public string? SelectedItem
    {
        get => _selectedItem;
        set
        {
            SetProperty(ref _selectedItem, value);
            _ = MessageBox.ShowOverlayAsync(value ?? string.Empty);
        }
    }
}

public class ToolBarSeparatorViewModel : ToolBarItemViewModel
{
}