using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace TestMauiApp.ViewModel;

// ObservableObject 내부적으로 이벤트 관련 기능이 포함되어 있다.
public partial class MainViewModel : ObservableObject
{
    // 소스 제너레이터 사용. 솔루션 익스플로러에서 dependency에서 analyzer 쪽에 생성되는 코드가 나온다.
    [ObservableProperty]
    string? text;

    [ObservableProperty]
    ObservableCollection<string> items = [];

    [RelayCommand]
    void Add()
    {
        // 입력값이 없거나 whitespace면 return 한다.
        if (string.IsNullOrWhiteSpace(Text)) return;
        
        // TodoList에 추가 후 입력값을 초기화 한다.
        Items.Add(Text);
        Text = string.Empty;
    }
}