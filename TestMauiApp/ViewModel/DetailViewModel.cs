using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace TestMauiApp.ViewModel;

[QueryProperty("Todo", "Text")]
public partial class DetailViewModel: ObservableObject
{
    [ObservableProperty]
    string todo;

    [RelayCommand]
    async Task GoBack()
    {
        await Shell.Current.GoToAsync("../");
    }
}
