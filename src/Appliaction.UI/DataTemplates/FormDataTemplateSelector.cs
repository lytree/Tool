using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Appliaction.UI.ViewModels;
using System.ComponentModel.DataAnnotations;
using CommunityToolkit.Mvvm.ComponentModel;
using Irihi.Avalonia.Shared.Contracts;
using System.Collections.ObjectModel;
namespace Appliaction.UI.Converters;

public class FormDataTemplateSelector : ResourceDictionary, IDataTemplate
{
    public Control? Build(object? param)
    {
        if (param is null) return null;
        var type = param.GetType();
        if (this.TryGetResource(type, null, out var template) && template is IDataTemplate dataTemplate)
        {
            return dataTemplate.Build(param);
        }
        return null;
    }

    public bool Match(object? data)
    {
        return data is IFromItemViewModel;
    }
}


public class DataModel : ObservableObject
{
    private DateTime _date;

    private string _email = string.Empty;
    private string _name = string.Empty;

    private double _number;

    public DataModel()
    {
        Date = DateTime.Today;
    }

    [MinLength(10)]
    public string Name
    {
        get => _name;
        set => SetProperty(ref _name, value);
    }

    [Range(0.0, 10.0)]
    public double Number
    {
        get => _number;
        set => SetProperty(ref _number, value);
    }

    [EmailAddress]
    public string Email
    {
        get => _email;
        set => SetProperty(ref _email, value);
    }

    public DateTime Date
    {
        get => _date;
        set => SetProperty(ref _date, value);
    }
}

public interface IFormElement
{

}

public interface IFormGroupViewModel : IFormGroup, IFormElement
{
    public string? Title { get; set; }
    public ObservableCollection<IFromItemViewModel> Items { get; set; }
}

public interface IFromItemViewModel : IFormElement
{
    public string? Label { get; set; }
}

public partial class FormGroupViewModel : ObservableObject, IFormGroupViewModel
{
    [ObservableProperty] private string? _title;
    public ObservableCollection<IFromItemViewModel> Items { get; set; } = [];
}

public partial class FormTextViewModel : ObservableObject, IFromItemViewModel
{
    [ObservableProperty] private string? _label;
    [ObservableProperty] private string? _value;
}

public partial class FormAgeViewModel : ObservableObject, IFromItemViewModel
{
    [ObservableProperty] private uint? _age;
    [ObservableProperty] private string? _label;
}

public partial class FormDateRangeViewModel : ObservableObject, IFromItemViewModel
{
    [ObservableProperty] private DateTime? _end;
    [ObservableProperty] private string? _label;
    [ObservableProperty] private DateTime? _start;
}
