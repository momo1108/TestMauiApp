﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace TestMauiApp.ViewModel;

// ObservableObject 내부적으로 이벤트 관련 기능이 포함되어 있다.
public partial class MainViewModel(IConnectivity connectivity) : ObservableObject
{
    private readonly IConnectivity connectivity = connectivity;

    // 소스 제너레이터 사용. 솔루션 익스플로러에서 dependency에서 analyzer 쪽에 생성되는 코드가 나온다.
    [ObservableProperty]
    string? text;

    [ObservableProperty]
    ObservableCollection<string> items = [];

    [RelayCommand]
    async Task Add()
    {
        // 입력값이 없거나 whitespace면 return 한다.
        if (string.IsNullOrWhiteSpace(Text)) return;

        if (connectivity.NetworkAccess != NetworkAccess.Internet)
        {
            await Shell.Current.DisplayAlert("Error!", $"No Internet has connected.\n{connectivity.NetworkAccess}\n{NetworkAccess.Internet}", "Got it");
            return;
        }
        
        // TodoList에 추가 후 입력값을 초기화 한다.
        Items.Add(Text);
        Text = string.Empty;
    }

    [RelayCommand]
    void Delete(string s)
    {
        if(Items.Contains(s))
        {
            Items.Remove(s);
        }
    }

    [RelayCommand]
    async Task Tap(string s)
    {
        await Shell.Current.GoToAsync($"{nameof(DetailPage)}?Text={s}");
    }
}