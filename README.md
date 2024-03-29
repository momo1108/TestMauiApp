# .NET MAUI
이 프로젝트는 .NET 유튜브 채널의 `.NET MAUI for Beginners` 코스를 수강하면서 만든 프로젝트입니다.
(https://www.youtube.com/playlist?list=PLdo4fOcmZ0oUBAdL2NwBpDs32zwGqb9DY)

Build Cross-Platform Native Application with .NET MAUI
- Desktop
- Mobile

마이크로 소프트에서 만든 프레임워크. 같은 코드 베이스를 이용해 Window, Mac, Android, ios 에서 사용 가능한 애플리케이션을 만들 수 있다.

Visual Studio 에서 생산적인 Tooling을 제공한다.

이번 튜토리얼에서 진행할 내용
- .NET MAUI 프로젝트 만들기
- UI 만들기
- Advanced MVVM Architecture & Data Binding
- Platform Integration
- Navigation
- and More..

MAUI에서는 C#을 이용해 native API에 접근할 수 있다.

XAML(XML 기반 Markup. 데이터 바인딩 기능 등을 가지고있다) 을 이용해 native cross-platform user interface 를 개발하거나, 순수 C# 으로 개발할 수 있다.

우리가 어떤 코드를 작성하든(ex. 버튼, 스피너, 슬라이더 등) .NET MAUI는 native control을 생성하고 렌더링한다.

Platform별로 활용되는 것들

- ios : UIKit
- Android : Widgets
- MacOS : Catalyst
- Windows : App SDK & WinUI 3

다시말해 같은 코드 베이스(Shared Code)로 다양한 플랫폼에 사용 가능한 개발(UI, Resources, Platform Features, Business Logic)을 할 수 있다.

Shared Business Logic
- Models
- View Models
- RESTful Service Calls
- Databases

.NET MAUI는 UI 뿐 아니라 다양한 Platform API들을 지원해준다.

![Platform API Image : Captured From https://www.youtube.com/watch?v=Hh279ES_FNQ&list=PLdo4fOcmZ0oUBAdL2NwBpDs32zwGqb9DY&index=1](/images/1_api.png)

위처럼 다양한 API들을 하나의 Common API로 사용할 수 있다.

## 설치
Visual Studio 2022 Community 인스톨러를 사용해 설치한다. .NET MAUI 체크 후 설치

## 프로젝트 생성
템플릿은 .NET MAUI App 선택 후 프로젝트를 생성한다.

샘플 프로젝트를 실행해 보기 위해서는 윈도우의 경우 `시스템 - 개발자용 - 개발자모드` 를 활성화해야 한다.

생성한 프로젝트를 Solution Explorer 에서 더블 클릭해보면 프로젝트 시스템에 cross platform 기능들이 직접 빌드된걸 볼 수 있다.

TargetFramework 에 다양한 platform(Tizen - 삼성과 여러회사가 같이 개발한	운영체제)들이 있다.

프로젝트 생성 후 Dependencies를 살펴보면 모든 Platform 의 dependency가 존재하는 걸 볼 수 있다.

아래의 코드처럼 cross platform 에 공유되는 app 정보가 설정되어있다.

```xml
<!-- Display name -->
<ApplicationTitle>TestMauiApp</ApplicationTitle>

<!-- App Identifier -->
<ApplicationId>com.companyname.testmauiapp</ApplicationId>

<!-- Versions -->
<ApplicationDisplayVersion>1.0</ApplicationDisplayVersion>
<ApplicationVersion>1</ApplicationVersion>
```

이런 정보들을 한곳에 정의해놓고 각 플랫폼에 자동으로 cascade down되어 compile 하고 deploy 할 때 자동으로 세팅한다.

```xml
<SupportedOSPlatformVersion Condition="$([MSBuild]::GetTargetPlatformIdentifier('$(TargetFramework)')) == 'ios'">11.0</SupportedOSPlatformVersion>
<SupportedOSPlatformVersion Condition="$([MSBuild]::GetTargetPlatformIdentifier('$(TargetFramework)')) == 'maccatalyst'">13.1</SupportedOSPlatformVersion>
<SupportedOSPlatformVersion Condition="$([MSBuild]::GetTargetPlatformIdentifier('$(TargetFramework)')) == 'android'">21.0</SupportedOSPlatformVersion>
<SupportedOSPlatformVersion Condition="$([MSBuild]::GetTargetPlatformIdentifier('$(TargetFramework)')) == 'windows'">10.0.17763.0</SupportedOSPlatformVersion>
<TargetPlatformMinVersion Condition="$([MSBuild]::GetTargetPlatformIdentifier('$(TargetFramework)')) == 'windows'">10.0.17763.0</TargetPlatformMinVersion>
<SupportedOSPlatformVersion Condition="$([MSBuild]::GetTargetPlatformIdentifier('$(TargetFramework)')) == 'tizen'">6.5</SupportedOSPlatformVersion>
```

이런식으로 사용 가능한 OS 버전도 명시가 가능하다.

ItemGroup을 보면 자동 생성되는 크로스 플랫폼 앱 리소스들이 존재한다.

```xml
<ItemGroup>
	<!-- App Icon -->
	<MauiIcon Include="Resources\AppIcon\appicon.svg" ForegroundFile="Resources\AppIcon\appiconfg.svg" Color="#512BD4" />

	<!-- Splash Screen -->
	<MauiSplashScreen Include="Resources\Splash\splash.svg" Color="#512BD4" BaseSize="128,128" />

	<!-- Images -->
	<MauiImage Include="Resources\Images\*" />
	<MauiImage Update="Resources\Images\dotnet_bot.png" Resize="True" BaseSize="300,185" />

	<!-- Custom Fonts -->
	<MauiFont Include="Resources\Fonts\*" />

	<!-- Raw Assets (also remove the "Resources\Raw" prefix) -->
	<MauiAsset Include="Resources\Raw\**" LogicalName="%(RecursiveDir)%(Filename)%(Extension)" />
</ItemGroup>
```

폰트와 이미지 설정, 사이즈 조절(svg도 가능) 등 다양한 작업도 가능하다.

### Platforms
Platforms 폴더는 개발자들에게 특정 플랫폼 별 native api를 접근할 수 있게 해준다.

각 플랫폼 폴더별로 scaffolding code(프로토타입, 테스트용 코드)가 있다.

예를 들어 안드로이드폴더의 `AndroidManifest.xml`에는 여러 permission 이라던가 app resource를 정의하고 있다.

`MainActivity.cs` 같은 startup code 들도 있다. 사람이 보기 편하게 최대한 압축해놓은 상태이다.

만약 특정 플랫폼에서 무언가를 변경하고 싶다면 이 폴더에서 하면된다.

### Resources
cross-platform 간 공유하는 resource들이 들어있다.

ex. 폰트, 이미지, raw assets

이런 리소스들을 .NET MAUI가 플랫폼 별로 컴파일 시 맞는 위치에 놓아준다.

AppIcon 처럼 svg 파일을 각 플랫폼에 맞게 .png 변환 후 스케일링을 하는 등의 작업같은 것들도 해준다.

---

### MauiProgram.cs
어플리케이션의 시작은 `MauiProgram.cs` 이다.

```cs
using Microsoft.Extensions.Logging;

namespace TestMauiApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
```

MauiProgram 클래스 내의 CreateMauiApp 이 실행되고 MauiApp 이 return 된다.

내부 코드의 첫 줄에서는 Builder를 생성하는데 ASP.NET Core와 비슷한 구조를 가진다.

다음 코드는 빌더에게 이 App을 사용한다고 알려주고 font를 설정한다.

이쪽 코드에서 추가적인 configure가 가능한데, 예를들어 [activity lifecycle](https://developer.android.com/guide/components/activities/activity-lifecycle?hl=ko), service, dependency service 등이 있다.

---

### App.xaml
앱 내부에는 뭐가 있을까?

`App.xaml` 에 들어가보면 `Resources/Styles/Colors.xaml`, `Resources/Styles/Styles.xaml` 등의 App-wide Resource 들이 있는걸 볼 수 있다.

```xaml
<?xml version = "1.0" encoding = "UTF-8" ?>
<Application xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             xmlns:local="clr-namespace:TestMauiApp"
             x:Class="TestMauiApp.App">
    <Application.Resources>
        <ResourceDictionary>
            <ResourceDictionary.MergedDictionaries>
                <ResourceDictionary Source="Resources/Styles/Colors.xaml" />
                <ResourceDictionary Source="Resources/Styles/Styles.xaml" />
            </ResourceDictionary.MergedDictionaries>
        </ResourceDictionary>
    </Application.Resources>
</Application>
```

Colors.xaml 에는 다양한 색상이 정의되어있고, Styles.xaml 에는 이런 색상정보를 활용한 테마 등이 설정되어있기 때문에 Colors.xaml 을 조금만 수정해줘도 다양한 변화를 줄 수 있다.

Visual Studio 에서 App.xaml 파일의 왼쪽 화살표 버튼을 눌러보면 `App.xaml.cs` 가 있다. 이런 것을 보통 *code behind* 라고 부른다. 모든 xaml.cs 는 xaml과 연관되어 있다고 보면 된다.

```cs
namespace TestMauiApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }
    }
}
```

내부의 코드를 보면 어플리케이션의 메인 페이지가 AppShell로 셋되는 것을 볼 수 있다. AppShell은 무엇일까?

나의 어플리케이션에 특화된 Shell이라고 보면 된다.

AppShell 의 장점은 어플리케이션이 로드될 때 Template 을 lazily load 할 수 있게 해준다는 것이다.

```xaml
<?xml version="1.0" encoding="UTF-8" ?>
<Shell
    x:Class="TestMauiApp.AppShell"
    xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
    xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
    xmlns:local="clr-namespace:TestMauiApp"
    Shell.FlyoutBehavior="Disabled"
    Title="TestMauiApp">

    <ShellContent
        Title="Home"
        ContentTemplate="{DataTemplate local:MainPage}"
        Route="MainPage" />

</Shell>
```

startup code에는 현재 페이지를 나타내는 Single shell piece of content 가 사용되고 있다.

여기에 그냥 item을 추가함으로서 쉽게 flyout navigation(`<FlyoutItem>`) 이나 top, bottom tab(`Tab`, `TabBar`) 외에도 메뉴 등을 추가할 수 있다.

이는 아주 유연한 방식이며 eye-based navigation 이 가능하다.

startup 코드에는 앱의 Route 가 MainPage 로 설정되어 있으며 이게 나의 MainRoute가 되는 것이다. ContentTemplate 에 MainPage 가 있고 Home 으로 Title 이 설정되어 있다.

이 MainPage(`MainPage.xaml`) 를 찾아가보면 ContentPage > ScrollView > VerticalStackLayout > 이미지, 라벨, 버튼 의 구조로 이루어져있다.

이 페이지 또한 code behind(`MainPage.xaml.cs`)를 보면 startup code가 존재한다.

```cs
namespace TestMauiApp
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        private void OnCounterClicked(object sender, EventArgs e)
        {
            count++;

            if (count == 1)
                CounterBtn.Text = $"Clicked {count} time";
            else
                CounterBtn.Text = $"Clicked {count} times";

            SemanticScreenReader.Announce(CounterBtn.Text);
        }
    }

}
```

버튼 클릭횟수를 저장할 count 필드와 onclick 이벤트 핸들러 함수가 정의되어있다.

전체적인 구조를 알아보았으니 어플리케이션을 실행해보자.

최상단에서 두번째 메뉴바에 실행관련 버튼에서 dropdown 을 눌러보면 실행할 수 있는 Framework 메뉴가 있다.

![debug framework](/images/2_debug.png)

Windows 11 은 내부적으로 Windows의 Android Subsystem을 사용해 Android 에뮬레이터를 사용할 필요 없이 디버깅이 가능하다. 이를 이용해 Windows 11 기기에서 바로 Windows 와 Android deploy가 가능하다.

Windows 10 이어도 Tools - Android - Android Device Manager 를 지원해주기 때문에 걱정할 필요 없다.(https://learn.microsoft.com/ko-kr/xamarin/android/get-started/installation/android-emulator/#Hardware_Acceleration)

세팅에 있어서 가속 설정이 좀 어려운데, 문서들을 참조해서 잘 따라가보자.(https://learn.microsoft.com/ko-kr/xamarin/android/get-started/installation/android-emulator/hardware-acceleration?pivots=windows)

~~왜 했는데도 느리지???????????????????~~ 실행되고 나서 내부 동작만 빨라지나?

그 외에 Windows Home edition에서는 쓸 수 없는 기능들을 사용할 수 있게 스크립트를 작성하는 방법들도 있더라.

- Hyper V : https://lastcard.tistory.com/141
- gpedit.msc : https://kiuas.tistory.com/9

iOS 의 경우에는 2가지 어플리케이션 배포 방법이 있다.(강의 영상 날짜 기준 22.6.2)

- Mac 에 원격 접속(connect remotely)을 하고 remoted simulator 를 통해 debug 와 deploy
- Windows Machine 에 직접적으로 iOS 기기를 plug 하고 iOS Hot Restart 사용해 그 기기에 직접적으로 deploy 할 수 있다.(단, Apple Developer Account 가 필요)

Mac 은 Mac 기기를 사용해서만 deploy 를 해야한다. 해당 문서를 따로 살펴보자.

---

### 실행해보기
여기까지 확인해본 내용을 토대로 직접 실행을 해보자.

일단 Windows Framework 를 선택 후 F5 를 눌러 실행!

만약 실행중인 창이 오버랩되는게 싫다면 Visual Studio 내부의 Preview 기능을 활용가능하다.(우측의 세로 메뉴 선택 후 Pin해서 고정)

좌측의 Live Visual Tree 메뉴를 사용하면 어플리케이션 내부의 모든 content 들을 각각 Tree 형태로 출력해주고 클릭으로 해당 소스를 확인할 수 있다.

웹 브라우저의 개발자 도구 처럼 마우스오버하면 화면에 어떤 element 인지 표시도 해준다.

---

## UI 만들기
UI 를 만들고 각각의 다른 레이아웃과 컨트롤들을 이용해보자.

TodoList 를 만들면서 위의 내용을 수행해보자.

MainPage.xaml 을 수정해서 홈페이지를 변경해보자.

```xaml
<?xml version="1.0" encoding="utf-8" ?>
<ContentPage xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             x:Class="TestMauiApp.MainPage">

    <Grid RowDefinitions="100, Auto, *"
          ColumnDefinitions=".75*, .25*"
          Padding="10"
          RowSpacing="10"
          ColumnSpacing="10">
        <Image Grid.ColumnSpan="2"
               Source="logo1.png"
               BackgroundColor="Transparent" />

        <Entry Placeholder="Enter task"
               Grid.Row="1" />
        <Button Text="Add"
                Grid.Row="1"
                Grid.Column="1" />
        <CollectionView Grid.Row="2" Grid.ColumnSpan="2">
            <CollectionView.ItemsSource>
                <x:Array Type="{x:Type x:String}">
                    <x:String>Apples</x:String>
                    <x:String>Bananas</x:String>
                    <x:String>Oranges</x:String>
                </x:Array>
            </CollectionView.ItemsSource>
            <!--단순 텍스트를 예쁘게 출력하기 위해 템플릿 생성 후 데이터바인딩-->
            <CollectionView.ItemTemplate> 
                <DataTemplate>
                    <SwipeView>
                        <SwipeView.RightItems> <!--left, top, bottom 도 가능-->
                            <SwipeItems>
                                <SwipeItem Text="Delete"
                                           BackgroundColor="Red"/>
                            </SwipeItems>
                        </SwipeView.RightItems>
                        <Grid Padding="0,5">
                            <Frame>
                                <Label Text="{Binding .}"
                                   FontSize="20" />
                            </Frame>
                        </Grid>
                    </SwipeView>
                </DataTemplate>
                
            </CollectionView.ItemTemplate>
        </CollectionView>
    </Grid>
</ContentPage>
```

일단 TodoList를 출력할 세번째 행인 `CollectionView` 도 스크롤을 지원하므로 `ScrollView` 를 삭제하고 새로운 화면을 만들었다. 

전체적인 구조를 Grid로써 생성했고, 그리드의 속성을 설정했다.

- `RowDefinitions` : 행의 개수, 크기에 대한 정의
  - `숫자`는 해상도에 따라 설정되는 듯 하다.
  - `Auto` 는 필요한 만큼의 크기로만 자동으로 설정되는 듯 하다.
  - `*` 는 기본적으로 비율에 사용된다
    - `".75*, .25*"` : 75% 와 25%, `".75*, *"` : 75% 와 100%
    - 딱 하나만 사용되는 경우에는 남은 공간 전체를 채우는 역할을 하는 듯 하다.(flex: auto; 역할과 비슷한듯)
- `ColumnDefinitions` : 열의 개수, 크기에 대한 정의
- `Padding` : Grid 내부 padding
- `RowSpacing` : 행간 여백 설정
- `ColumnSpacing` : 열간 여백 설정

Grid 내부 요소로 3개의 행을 설정했는데, 첫 행은 로고 이미지, 두번째 행은 Todo Item 입력창과 추가버튼, 마지막 행은 Todo Item List 이다.

열을 2개로 설정해 놓았기 때문에, 하나의 요소로 전체 열을 채워 사용하고 싶은 경우 `Grid.ColumnSpan` 속성을 사용해 병합할 열 개수를 지정한다.

또한 행과 열 개수가 정해진 Grid의 경우 행과 열 기준 첫번째 요소 다음 부터는 몇번째 요소인지 순서를 직접 지정해줘야 한다. 그렇지않으면 모두 첫번째로 지정된다.

행과 열 순서 지정은 `Grid.Row`, `Grid.Column` 을 활용한다. 순서는 index 와 같이 0부터 시작한다.

TodoList 출력을 위해 `CollectionView` 를 사용했고, 이 역시 행 순서 지정과 열 병합을 지정한다.

`CollectionView` 에 출력에 사용할 데이터인 `ItemSource` 와 출력될 형태를 나타내는 `ItemTemplate` 을 설정한다.

`ItemTemplate` 에 `DataTemplate` 을 사용해 데이터 바인딩을 진행할 수 있게 한다.

Swipe를 통해 삭제버튼을 보이게 하기 위해서 `SwipeView` 를 사용하고, 기본적인 형태는 `Grid` 로 설정 후 스와이프 시 오른쪽에 버튼을 출력하기 위해 `SwipeView.RightItems` 를 사용한다.

여기까지 기본적인 UI를 구성했다. 튜토리얼에서는 XAML을 활용해 UI를 구성했지만, C# 을 활용해 UI를 구성하는 방법도 있다고 하니 나중에 필요해지면 알아보도록 하자.

## MVVM 과 XAML 을 활용한 Data Binding
이제 MVVM(Model-View-ViewModel)[^1]과 XAML을 활용하여 responsive, reactive 한 어플리케이션으로 만들어보자.

이 구조는 XAML로 어플리케이션을 개발할 때 많이 사용되는 구조이다.

Data Binding 을 지원해 UI와 code behind 소통함으로써 control 과 data 의 flow 를 관리할 수 있다.

`View`는 버튼, 라벨, 엔트리 등을 사용해 데이터를 어떻게 디스플레이 할까에 사용된다.

`View Model`은 완전히 decoupled 된 code behind 라고 생각하면 된다. View Model은 **무엇을 디스플레이 할지** 를 나타낸다고 보면 된다. 객체나 문자열들, 버튼이 클릭되면 발생할 이벤트, 라벨에 무엇을 출력할지 등을 포함한다.

.NET MAUI 내부의 Binding System 은 이것들을 모두 합쳐 우리의 UI 와 code behind 가 서로 업데이트 할 수 있게 해준다.

단순히 값들 뿐 아니라 이벤트 핸들러 같은 개념도 포함된다.

이제 어플리케이션에 MVVM을 적용해보자.

일단 Todo Item 입력창에 데이터 바인딩을 하기위한 방법을 살펴보자.

ViewModel 폴더를 만들고 `MainViewModel.cs` 파일을 생성했다.

```cs
using System.ComponentModel;

namespace TestMauiApp.ViewModel;

public class MainViewModel : INotifyPropertyChanged
{
    string text;
    public string Text
    {
        get => text;
        set 
        { 
            text = value;
            OnPropertyChanged(nameof(Text));
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    void OnPropertyChanged(string name) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}
```

어플리케이션에 MVVM을 구현하는 전통적인 방법은 INotifyPropertyChanged 인터페이스를 상속받아서 이벤트핸들러를 구현하는 방법이다.

인터페이스의 PropertyChangedEventHandler 는 .NET MAUI 가 자동으로 참조하며, 우리는 UI 업데이트를 원하는 시점에 .NET MAUI에게 notify 할 수 있다.

커스텀 메서드에 보다시피 직접 해당 이벤트를 실행하는 방식이다.

그 다음은 데이터바인딩을 위한 Text 필드를 만들고 세터에 OnPropertyChagned 메서드를 사용하면 UI와 code behind 에서 Text를 세팅할 때 마다 PropertyChanged 알림을 통해 Entry 가 자동으로 업데이트되도록 한다.

여기까지가 기본적인 방식이나, 커뮤니티가 MS가 만든 좋은 패키지들이 있다.

Nuget을 이용해 패키지를 추가해보자.

Solution Explorer 에서 프로젝트를 우클릭하고 Manage Nuget Packages 를 선택하자.

![우클릭 스샷](/images/3_solutionexplorer.png)

Browse 메뉴에서 CommunityToolkit.Mvvm 을 검색하자.

강의에서는 8.0.0 버전을 사용했다.

이 패키지는 .NET 어플리케이션에 사용 가능하다. 또 훌륭한 소스코드 생성기능을 제공해준다. 설치를 완료하고 View Model 코드를 수정해보자.

```cs
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace TestMauiApp.ViewModel;

public partial class MainViewModel : ObservableObject
{
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

    [RelayCommand]
    void Delete(string s)
    {
        if(Items.Contains(s))
        {
            Items.Remove(s);
        }
    }
}
```

string text 필드를 제외한 모든 코드를 삭제 후, CommunityToolkit.Mvvm.ComponentModel 에서 제공하는 ObservableObject를 상속받자.

ObservableObject 엔 내부적으로 이벤트 관련 기능이 포함되어 있다.

이제 입력창에 바인딩할 필드인 text 에 소스 제너레이터를 사용해보자. `[ObservableProperty]` 를 사용해 이전에 직접 지정했었던 getter 와 setter, event invoke 까지 모두 자동으로 소스코드가 생성된다.

관련 코드들은 솔루션 익스플로러에서 dependency에서 analyzer 쪽에 생성되는 걸 확인할 수 있다.

마찬가지로 Todo Item들을 저장할 필드도 생성하자. System.Collections.ObjectModel 에서 제공하는 ObservableCollection 을 사용한다.

이제 추가를 위한 이벤트 핸들러를 만들자. 마찬가지로 소스 제너레이터 `[RelayCommand]` 를 통해 우리는 기능과 관련된 코드만 작성하면 된다.

Add 와 Delete 메서드 모두 ObservableProperty 인 Items 에 변화를 일으키므로, 실행될 때 마다 UI에 변화가 있을 것이다.

이제 MainPage.xaml 에 변경된 내용에 맞게 코드를 수정해보자.

```xaml
<ContentPage xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             x:Class="TestMauiApp.MainPage"
             xmlns:viewmodel="clr-namespace:TestMauiApp.ViewModel"
             x:DataType="viewmodel:MainViewModel">
```

ContentPage 에 ViewModel 컨텍스트를 추가하고, x:DataType 을 통해 ContentPage 와 ViewModel 을 연결한다.

TodoList에 해당되는 CollectionView 에 데이터를 바인딩한다.

```xaml
<CollectionView Grid.Row="2"
                Grid.ColumnSpan="2"
                ItemsSource="{Binding Items}">
```

CollectionView.ItemsSource 을 삭제하고 대신 속성값으로 바인딩을 한다.

CollectionView.ItemsSource 내부에 설정했었던 Type 을 이제 템플릿에 사용되는 DataTemplate 에 지정해야 한다.

DataTemplate은 MainViewModel 이 아닌 string 이 bound 돼있기 때문이다.

```xaml
<DataTemplate x:DataType="{x:Type x:String}">
```

이제 Entry 와 Button 에 각각 text 필드와 Add 이벤트핸들러를 바인딩하자. 각각 `Text` 속성과 `Command` 속성을 사용하면 된다.

```xaml
<Entry Placeholder="Enter task"
        Grid.Row="1"
        Text="{Binding Text}"/>
<Button Text="Add"
        Command="{Binding AddCommand}"
        Grid.Row="1"
        Grid.Column="1" />
```

마지막으로 SwipeItem 에 Delete 이벤트핸들러를 바인딩하는데, 해당 요소가 CollectionView, DataTemplate 의 하위 요소이므로 Binding 시에 상위 요소들의 바인딩값(Items)나 타입(x:Type x:String)이 아닌 MainViewModel 의 Delete 이벤트핸들러를 바인딩하도록 명시해줘야 한다.

또 이벤트핸들러에 전달될 값을 `CommandParameter` 속성에 명시해준다.

```xaml
<SwipeItem Text="Delete"
    BackgroundColor="Red"
    Command="{Binding Source={RelativeSource AncestorType={x:Type viewmodel:MainViewModel}}, Path=DeleteCommand}"
    CommandParameter="{Binding .}"/>
```

이제 MainPage 의 code behind 에서 Context 를 바인딩한다.

```cs
using TestMauiApp.ViewModel;

namespace TestMauiApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage(MainViewModel vm)
        {
            InitializeComponent();
            BindingContext = vm;
        }
    }
}
```

최종적으로 `MauiProgram.cs` 에 dependency service를 사용해 system을 등록하자.

```cs
using Microsoft.Extensions.Logging;
using TestMauiApp.ViewModel;

namespace TestMauiApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddSingleton<MainViewModel>();

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
```

Singleton 서비스는 한번만 생성되는 global static 같은 개념이다.

Transient 서비스는 매번 생성되는 개념이다.(나중에 사용한다)

MainPage 와 MainViewModel 을 등록했다.

이로써 MVVM이 결합된 어플리케이션을 만들었다.

## Navigation
네비게이션을 통해 다른 페이지로 이동해보자. 페이지 이동 과정에 데이터도 전달할 수 있다.

.NET MAUI 에는 몇가지의 네비게이션 방법이 존재한다. 그중에서도 URI Shell-based Navigation 을 중점적으로 알아보자.

Shell 이란 것은 우리 어플리케이션의 구조를 담당한다. 또한 Shell은 constructor injection, dependency injection 뿐 아니라 URI-based Navigation 을 제공한다.

웹과 비슷하게 `/주소` 형태로 자세한 경로들을 설정 가능하고, 간단한 데이터 타입이나 복잡한 객체도 query parameter 도 전달할 수 있다.

새로운 페이지를 만들어보자. TodoList의 아이템을 클릭하면 DetailPage 로 이동하게 만들고 싶다. 

솔루션 익스플로러에서 프로젝트 우클릭 후 Add - New Items 에서 템플릿 중 .NET MAUI 템플릿의 XAML Content Page를 선택해 DetailPage.xaml 을 만들어보자.

이제 DetailPage 의 ViewModel을 생성한 후 mvvm toolkit에 맞게 코드를 수정해주고, DetailPage 의 code behind 에도 BindingContext 를 설정해준다.

DetailPage 와 DetailViewModel 또한 서비스에 추가해주어야 하는데, 메인페이지는 한번 글로벌로 생성해놓고 메모리에 보관하며 사용하지만 디테일페이지는 매번 새로 생성하고 싶다.

따라서 이 경우에는 `AddTransient` 를 사용해준다.

다음으론 AppShell에 기본페이지인 Main 말고 Detail 페이지를 추가하자.

먼저 code behind 에 새로운 Route 를 추가해보자.

```cs
namespace TestMauiApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(DetailPage), typeof(DetailPage));
        }
    }
}
```

RegisterRoute 함수를 통해 페이지의 경로를 등록한다.

이제 네비게이션을 위한 이벤트 핸들링이 필요한데, 메인페이지에서 TodoList 를 디스플레이하는 CollectionView 의 내부 속성이 존재한다.(SelectionChanged, SelectionMode 등)

하지만 우리의 경우 아이템을 선택하는 개념보다는 그냥 탭을 통해 이동하는게 목적이기 때문에, CollectionView 의 Selection 은 사용하지 않도록 설정한다.

대신 Label 을 감싸는 Frame 자체에 클릭을 인식하는 built-in 기능(Frame.GestureRecognizers)을 추가해주자. 이 기능은 다양한 제스쳐를 지원한다.(Tap, Swipe, Pinch, Pan, Drop, Drag, ...)

```xaml
<Frame>
    <Frame.GestureRecognizers>
        <TapGestureRecognizer Command="{Binding Source={RelativeSource AncestorType={x:Type viewmodel:MainViewModel}}, Path=TapCommand}"
                              CommandParameter="{Binding .}"/>
    </Frame.GestureRecognizers>
    <Label Text="{Binding .}"
        FontSize="20" />
</Frame>
```

이벤트핸들러는 Delete와 똑같은 방법으로 등록한다. 이제 MainViewModel 에서 라우팅을 담당할 Tap 메서드를 만들어보자.

```cs
[RelayCommand]
async Task Tap(string s)
{
    await Shell.Current.GoToAsync($"{nameof(DetailPage)}?Text={s}",
        new Dictionary<string, object>
        {
            {"hello", new object()} 
        });
}
```

라우팅이 완전히 될때까지 기다리게 하도록 async / await 를 사용하고, Shell.Current.GoToAsync 함수를 통해 실제 라우팅이 진행된다.

이때, query parameter 에 간단한 데이터는 직접 `Key=Value` 형태로 전달이 가능하고, 복잡한 객체같은 경우 두번째 파라미터에 Dictionary 형태로 전달이 가능하다.

전달한 query 값은 DetailPage 의 code behind 혹은 DetailViewModel 에서 직접 사용이 가능하다.

```cs
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
```

DetailViewModel 에 간단하게 `[QueryProperty("사용할이름", "전달받은이름")]` 코드 제너레이터를 사용해 전달받을 수 있다.

또한 메인으로 돌아가기 위한 방법으로 같은 함수를 사용하지만, 직접 메인 경로를 입력할 필요 없이 페이지들이 스택에 쌓여있는걸 활용할 수 있다.

`../` 는 이전 페이지, `../../` 는 2 페이지 전 이런 식이다.

마찬가지로 DetailPage 에 ViewModel 설정을 하고 바인딩을하자.

```xaml
<?xml version="1.0" encoding="utf-8" ?>
<ContentPage xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             x:Class="TestMauiApp.DetailPage"
             xmlns:viewmodel="clr-namespace:TestMauiApp.ViewModel"
             x:DataType="viewmodel:DetailViewModel"
             Title="DetailPage">
    <VerticalStackLayout>
        <Label 
            Text="{Binding Todo}"
            VerticalOptions="Center" 
            HorizontalOptions="Center" />
        <Button Text="GoBack" Command="{Binding GoBackCommand}" />
    </VerticalStackLayout>
</ContentPage>
```

## Native API 기능
Native API 는 말하자면 각 플랫폼(Windows, Mac, Android, iOS)에 특화된 것들이라고 할 수 있다.

.NET MAUI 에서는 C# 안에서 Native API 에 접근이 가능하다.(iOS, Android for .NET, Windows App SDK, UI3 시스템 덕분에)

또한 UI 를 개발할 때와 마찬가지로, 공통적인 플랫폼 API(ex. Geolocation, Sensor, connectivity, etc..) 들을 개발자가 사용할 수 있도록 하나의 API로 만들어놓았다.

이제 우리의 어플리케이션에 거의 모든 어플리케이션에 흔히 사용되는 connectivity 를 결합해보자.

TodoList 에 아이템을 추가할 때, 인터넷에 연결되지 않으면 안되게 해보자.

Microsoft.Maui 네임스페이스에는 ApplicationModel, Authentication, Controls, Devices 등 다양한 기능들을 가진 namespace 들이 있다.

네트워크를 체크할 때 `Connectivity.NetworkAccess` 이렇게 클래스를 직접 사용할 수도 있는데, 강의에서는 뷰모델을 테스트가 가능하게 만들기 위해 인터페이스인 `IConnectivity` 를 필드로 사용했다.(무슨말인지 잘 모르겠네)

MainViewModel의 constructor 에 인터페이스 IConnectivity 를 추가하고 필드를 설정한다.(강의에서는 직접 constuctor를 호출하고 안에서 초기화했다. 어떻게 constuctor 가 동작해서 저게 설정되는지는 잘 모르겠다.)

```cs
public partial class MainViewModel(IConnectivity connectivity) : ObservableObject
{
    private readonly IConnectivity connectivity = connectivity;
    ...
}
```

그 후 Add 메서드에서 네트워크 연결을 체크하는 코드를 작성해보자.

```cs
[RelayCommand]
async Task Add()
{
    if (string.IsNullOrWhiteSpace(Text)) return;

    if(connectivity.NetworkAccess != NetworkAccess.Internet)
    {
        await Shell.Current.DisplayAlert("Error!", "No Internet has connected.", "Got it");
        return;
    }
        
    Items.Add(Text);
    Text = string.Empty;
}
```

`connectivity.NetworkAccess` 는 현재 네트워크 연결 상태를 나타낸다. `NetworkAccess.Internet` 는 정상 네트워크 연결시의 enum 값이며, NetworkAccess 의 enum 값들은 다음과 같다.

```cs
public enum NetworkAccess
{
	/// <summary>The state of the connectivity is not known.</summary>
	Unknown = 0,

	/// <summary>No connectivity.</summary>
	None = 1,

	/// <summary>Local network access only.</summary>
	Local = 2,

	/// <summary>Limited internet access.</summary>
	ConstrainedInternet = 3,

	/// <summary>Local and Internet access.</summary>
	Internet = 4
}
```

정상적인 네트워크 연결상태라면, Internet 이라는 enum 값을 가지는 것이다. 그렇지 않은 경우 다른 값을 가지므로 두 값을 비교해서 판단한다.

네트워크 연결을 체크하고 알람 기능을 사용하는데, 이는 async 함수이므로 Add 메서드도 이에 맞게 async 키워드와 Task 를 return 하도록 변경했다.

여기까지 동작관련 코드들은 변경이 완료됐다.

이제 `MauiProgram.cs` 에서 메인 어플리케이션에 Connectivity 서비스를 등록해야 한다.

```cs
using Microsoft.Extensions.Logging;
using TestMauiApp.ViewModel;

namespace TestMauiApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            // 다른 코드들...

            builder.Services.AddSingleton<IConnectivity>(Connectivity.Current);

            // 다른 코드들...
        }
    }
}
```

IConnectivity 서비스를 등록할 때 `Connectivity.Current`(Connectivity 클래스가 static 으로 사용될 때 기본 구현체를 제공해줌) 를 사용하게 한다.

이로써 서비스 등록을 마쳤고 네트워크 연결 상태에 따라 동작을 하게 만들었다. 위 코드로 크로스 플랫폼에서 동작이 가능하다.

디버깅은 내부적으로 네트워크 연결에 의존하는 부분이 있기 때문에, 테스트를 할 때는 Debugging을 사용하지 않고 실행하는게 좋다.(Ctrl + F5)

## Windows 환경에서 창 크기 고정
윈도우 환경에서만 창의 크기를 고정하고 싶은 경우에는 어떻게 해야할까?

구글링을 해본 결과, 대부분의 답변이 `Platforms/Windows` 경로에서 코드를 작성하고있었다. 여기에 있는 코드가 플랫폼별로 따로 빌드되는 것이니 당연한건가?

어쨌든 방법을 살펴보니, Windows 플랫폼의 폴더에 있는 `App.xaml` 의 code behind, 즉 `App.xaml.cs` 에서 관련 조작이 진행되더라.

```cs
using Microsoft.UI;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Windows.Graphics;
using WinRT.Interop;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace TestMauiApp.WinUI
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public partial class App : MauiWinUIApplication
    {
        Microsoft.UI.Xaml.Window nativeWindow;
        int screenWidth, screenHeight;
        const int desiredWidth = 1000;
        const int desiredHeight = 1720;
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();

            Microsoft.Maui.Handlers.WindowHandler.Mapper.AppendToMapping(nameof(IWindow), (handler, view) =>
            {
                IWindow mauiWindow = handler.VirtualView;
                nativeWindow = handler.PlatformView;
                nativeWindow.Activated += OnWindowActivated;
                nativeWindow.Activate();

                // allow Windows to draw a native titlebar which respects IsMaximizable/IsMinimizable
                nativeWindow.ExtendsContentIntoTitleBar = false;

                IntPtr windowHandle = WinRT.Interop.WindowNative.GetWindowHandle(nativeWindow);
                WindowId windowId = Microsoft.UI.Win32Interop.GetWindowIdFromWindow(windowHandle);
                AppWindow appWindow = Microsoft.UI.Windowing.AppWindow.GetFromWindowId(windowId);

                // set a specific window size
                appWindow.MoveAndResize(new RectInt32((screenWidth - desiredWidth) / 2, (screenHeight - desiredHeight) / 2, desiredWidth, desiredHeight));

                if (appWindow.Presenter is OverlappedPresenter p)
                {
                    p.IsResizable = false;
                    // these only have effect if XAML isn't responsible for drawing the titlebar.
                    p.IsMaximizable = false;
                    p.IsMinimizable = false;
                }
            });
        }


        private void OnWindowActivated(object sender, Microsoft.UI.Xaml.WindowActivatedEventArgs args)
        {
            // Retrieve the screen resolution
            var displayInfo = DeviceDisplay.Current.MainDisplayInfo;
            screenWidth = (int)displayInfo.Width;
            screenHeight = (int)displayInfo.Height;
            // Remove this event handler since it is not needed anymore
            nativeWindow.Activated -= OnWindowActivated;
        }

        protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
    }

}
```

## 배포하기
배포에 관련된 내용은 강의에 포함되어있지 않아서 도큐먼트를 보고 정리해보았다.(https://learn.microsoft.com/ko-kr/dotnet/maui/deployment/?view=net-maui-8.0)

### Android
#### 테스팅
안드로이드 플랫폼에 앱을 테스팅할수 있는 방법은 2가지가 있다.

첫번째는 강의에서 사용한 안드로이드 에뮬레이터를 사용하는 것이고, 두번째는 안드로이드 기기를 개발자 모드를 사용하도록 설정하고 개발 PC에 연결하는 것이다.

안드로이드 에뮬레이터는 Visual Studio 2022에서 계속 사용했으니 패스하고, 두번째방법을 알아보자.(https://learn.microsoft.com/ko-kr/dotnet/maui/android/device/setup?view=net-maui-8.0)

내 핸드폰은 갤럭시 S8이다. 삼성폰은 OS가 같을테니 참조바란다. 개발자 모드가 켜지지 않은 경우 1번부터 진행하면 된다.

1. 설정 - 휴대전화 정보 - 소프트웨어 정보 - 빌드번호 7번 클릭 후 패턴입력
2. 설정 - 개발자 옵션 - 디버깅 탭의 USB 디버깅 활성화
3. PC에 안드로이드 기기 연결
4. Visual Studio에서 Android Emulators 대신 Android Local Devies 에서 기기 선택
5. 어플리케이션 실행(내 노트북에선 2~3분 걸렸음)

#### 배포
아래의 이미지는 .NET MAUI 의 Android 앱 배포와 관련된 단계를 나타내는 다이어그램이다.
![build and deploy in MAUI](/images/4_build-and-deploy-steps.png)

> [!NOTE]
> Android용 .NET MAUI 앱을 게시할 때 APK(Android 패키지) 또는 AAB(Android 앱 번들) 파일을 생성합니다. APK는 Android 디바이스에 앱을 설치하는 데 사용되며, AAB는 Google Play에 앱을 게시하는 데 사용됩니다.

배포 준비 단계는 별다를게 없고, 차이점은 배포 방법에 있다.

- Google Play 등 다양한 Android 마켓플레이스를 통해서 설치
- 웹 사이트의 링크를 클릭해서 설치
- 앱 패키지를 디바이스에 파일 공유를 통해서 설치

마켓 플레이스에 앱을 등록하기 위해서는 더 추가적인 프로세스가 필요하다.

일단 앱을 직접 설치해서 테스팅하는 방법부터 알아보자.

Solution Explorer 에서 프로젝트를 우클릭하고 속성(Properties) 메뉴로 들어가자.

1. Android - Options - Android Package Format 의 Release 항목도 `apk` 로 변경해주자.
    ![5_build-and-deploy-steps.png](/images/5_ad-hoc-change-package-format.png)
2. 디버그 대상을 안드로이드 에뮬레이터에 생성한 가상 기기로 설정하자(ex. Pixel 5 - API 30)
    ![6_build-and-deploy-steps.png](/images/6_select-android-deployment.png)
3. Debug 항목을 드랍다운을 눌러서 Release 로 변경해준다.
    ![7_release-configuration.png](/images/7_release-configuration.png)
4. Solution Explorer 에서 프로젝트 우클릭 - Publish 선택하면 Archive Manager 가 앱을 Archive 한다.(노트북에서 한 4~5분 걸린듯)
    ![8_publish-menu-item.png](/images/8_publish-menu-item.png)
5. Archiving이 끝나면 Distribute... 버튼 클릭
    ![9_archive-manager-distribute.png](/images/9_archive-manager-distribute.png)
6. Select Channel 창에서 Ad Hoc 선택
    ![10_distribution-select-channel-ad-hoc.png](/images/10_distribution-select-channel-ad-hoc.png)
7. Signing Identity 선택창에서 나의 Signing Identity 를 새로 만든다.
    ![11_create-new-ad-hoc-signing-identity.png](/images/11_create-new-ad-hoc-signing-identity.png)
8. 입력 내용은 본인 마음대로 입력하고 생성하자. 난 패스워드도 똑같이 mauitest 로 설정함.
    ![12_create-android-keystore.png](/images/12_create-android-keystore.png)
9. 생성한 Signing Identity 선택 후 Save As 클릭
    ![13_save-ad-hoc.png](/images/13_save-ad-hoc.png)
10. 저장할 위치와 이름을 잘 확인 후 저장 - 패스워드 입력
11. 안드로이드 기기의 보안 설정(내 경우 삼성 갤럭시 S8)은 설정 - 생체 인식 및 보안 - 출처를 알 수 없는 앱 설치 에서 `내 파일` 허용
12. 생성된 apk 파일은 핸드폰으로 옮겨서 본인 안드로이드 기기의 탐색기(내 핸드폰은 `내 파일`)에서 직접 실행하면 설치가 된다.

테스트 결과 내 핸드폰(갤럭시 S8)에는 설치가 되지 않았다.

뭔가 테스트한 안드로이드 에뮬레이터의 가상기기의 버전과 뭔가 맞지 않는게 있나보다.

다른 버전도 여러개 설정해봤는데, 이상하게도 디버그가 제대로 동작하질 않는다. 왜그러지...

### Windows
윈도우에서의 배포에 대한 개요는 도큐먼트를 참조하자.(https://learn.microsoft.com/ko-kr/dotnet/maui/windows/deployment/overview?view=net-maui-8.0)

배포에는 몇가지 방법이 있는데, 먼저 .NET CLI 를 사용해 배포하는 방법과 Visual Studio 를 사용해 배포하는 방법이 있다.

또한 배포하는 앱의 형태도 MSIX 앱 패키지(packaged app) 또는 실행 파일(unpackaged app) 형태가 있다. MSIX 에 대한 내용은 관련 도큐먼트를 참조하자.(https://learn.microsoft.com/ko-kr/windows/msix/overview)

#### 매니페스트
MSIX 앱 패키지의 경우, 프로젝트의 `Platforms\Windows\Package.appxmanifest`(매니페스트라 칭함) 파일에 의해 구성된다.

이 매니페스트는 우리 어플리케이션을 configure 하고 display 하기위한 역할로서 MSIX Installer, Microsoft Store, Windows 에서 사용된다.

.NET MAUI 에서는 어플리케이션 이름, 아이콘 등 크로스 플랫폼 공유 세팅을 사용하는데, 이는 build-time 에 매니페스트 안의 내용으로 세팅된다.

물론 이런 세팅들 외에도, 사용자들에게 좀더 나은 설치 관리자 환경을 제공하기 위해 앱 패키지를 구성하는데 사용되는 매니페스트를 수정해야 한다.

Microsoft Store 와 Windows 에서 앱이 어떻게 display 되는지 영향을 줄 `Platforms\Windows\Package.appxmanifest` 파일을 수정하는 방법으로는 Visual Studio 의 **Manifest Designer** 기능을 활용하는 방법, **XML editor** 를 사용하여 수정하는 방법이 있다.

- `Manifest Designer` 를 사용하려면, `Solution Explorer` 에서 `Platforms\Windows\Package.appxmanifest` 을 우클릭하고 `Properties` 를 클릭한다.
- `XML editor` 를 사용하려면, `Solution Explorer` 에서 `Platforms\Windows\Package.appxmanifest` 을 우클릭하고 `View Code` 를 클릭한다.

도큐먼트에 명시된 참조사항

> [!NOTE]
> The Manifest Designer for .NET MAUI projects can't edit app capabilities. For the time being, you'll need to use the XML editor.

좀더 자세한 앱 매니페스트 세팅이 궁금하다면 [App manifest schema reference](https://learn.microsoft.com/en-us/uwp/schemas/appxpackage/uapmanifestschema/root-elements) 도큐먼트를 참조하자.

#### Visual Studio 를 사용하여 폴더에 배포하기
1. `Solution Explorer` 에서 프로젝트 우클릭 후 `Publish` 선택
    ![vs-right-click-publish.png](/images/14_vs-right-click-publish.png)
2. **Select distribution method** : 실행된 창에서 `Sideloading` 선택 후 `Next` 클릭(Enable automatic updates 선택 시 뒤에서 설치 파일 경로를 명시해주어야 한다.)
    ![vs-1-how-distribute.png](/images/15_vs-1-how-distribute.png)
3. **Select signing method** : `Yes, select a certificate` 선택 후 `Create` 버튼 클릭(임시 테스트용 인증서 생성)
    ![vs-2_1-create-cert.png](/images/16_vs-2-cert-sign.png)
4. **Create a Self-Signed Test Certificate** : 퍼블리셔 이름과 인증서 파일의 패스워드 설정 후 `OK` 클릭
    ![vs-2_1-create-cert.png](/images/17_vs-2_1-create-cert.png)
5. 다시 **Select signing method** 창으로 돌아와서 인증서 옆의 `Trust` 버튼 클릭 후 `Next` 클릭
    ![trust_and_select_cert.png](/images/18_trust_and_select_cert.png)
6. **Select and configure packages** : 앱 패키지의 버전을 설정한다.(기본은 `0.0.0.0`) `Automatically Increment` 체크박스는 publish 될 때 마다 버전을 올려준다.
    `Publishing profile` 선택 후 `<New..>` 선택
    ![vs-4-configure.png](/images/20_vs-4-configure.png)
7. **Create a new MSIX Publish Profile** : Configuration 을 `Release` 로 선택하고, Target Runtime 을 `win10-x64` 로 설정 후 `OK` 클릭
    ![vs-4_1-publish-profile.png](/images/21_vs-4_1-publish-profile.png)
    만약 이전에 `Enable automatic updates` 을 선택한 경우 여기서 installer 위치 설정이 진행된다.
8. 다시 **Select and configure packages** 창으로 돌아와서 `Create` 버튼 클릭하면 끝.
    ![vs-5-configure-done.png](/images/22_vs-5-configure-done.png)

위 방식의 한계점은 다음과 같다.
- 게시된 앱은 게시 폴더에서 실행 파일로 직접 실행하려고 하면 작동하지 않습니다.
- 앱을 실행하는 방법은 먼저 패키지된 MSIX 파일을 통해 설치하는 것입니다.

#### .NET CLI 를 사용하여 unpackaged app 배포하기
먼저 프로젝트의 빌드 세팅을 구성한다.

아래의 `<PropertyGroup>` 노드를 프로젝트 파일에 추가한다. 이 노드는 타겟 프레임워크가 `Windows` 이고 `Release` 로 설정됐을때만 처리된다.

```xaml
<PropertyGroup Condition="$([MSBuild]::GetTargetPlatformIdentifier('$(TargetFramework)')) == 'windows' and '$(RuntimeIdentifierOverride)' != ''">
    <RuntimeIdentifier>$(RuntimeIdentifierOverride)</RuntimeIdentifier>
</PropertyGroup>
```

이제 Visual Studio 의 개발자 콘솔에서 프로젝트 폴더로 이동한다. 이후 `dotnet publish` 명령어를 실행하는데, 옵션은 아래를 참고하자.

|Parameter|Value|
|:--|:--|
|`-f`|The target framework, which is `net8.0-windows{version}`. This value is a Windows TFM, such as `net8.0-windows10.0.19041.0`. Ensure that this value is identical to the value in the `<TargetFrameworks>` node in your .csproj file.|
|`-c`|The build configuration, which is `Release`.|
|`-p:WindowsPackageType=None`|Indicates to the publish command that there should be no package.|
|`-p:RuntimeIdentifierOverride=win10-x64` or `-p:RuntimeIdentifierOverride=win10-x86`|Avoids the bug detailed in [WindowsAppSDK Issue #3337](https://github.com/microsoft/WindowsAppSDK/issues/3337). Choose the `-x64` or `-x86` version of the parameter based on your target platform.|
|`-p:WindowsPackageType`|The package type, which is `None` for unpackaged apps.|
|`-p:WindowsAppSDKSelfContained`|The deployment mode for your app, which can be framework-dependent or self-contained. This value should be `true` for self-contained apps. For more information about framework-dependent apps and self-contained apps, see [Windows App SDK deployment overview](https://learn.microsoft.com/en-us/windows/apps/package-and-deploy/deploy-overview).|

> [!WARNING]
> .NET MAUI Solution을 배포할 때 사용하는 `dotnet publish` 커맨드는 우리 Solution 의 각 프로젝트들을 따로 배포하기 때문에, 우리 Solution 에 다른 프로젝트 타입들을 추가한 경우 문제가 발생할 수 있다. 따라서 `dotnet publish` 커맨드는 우리 .NET MAUI 프롲게트에 scope 되어야 한다.
> 원문 : Attempting to publish a .NET MAUI solution will result in the `dotnet publish` command attempting to publish each project in the solution individually, which can cause issues when you've added other project types to your solution. Therefore, the `dotnet publish` command should be scoped to your .NET MAUI app project.

> [!TIP]
> .NET 8 버전에서 `dotnet publish` 커맨드는 기본적으로 `Release` configuration 으로 설정되어있다. 따라서 커맨드라인 명령어에서 이 build configuration 은 생략 가능하다.
> 원문 : In .NET 8, the `dotnet publish` command defaults to the `Release` configuration. Therefore, the build configuration can be omitted from the command line.

배포과정에서 앱이 빌드되고, 실행 파일이 `bin\Release\net8.0-windows10.0.19041.0\win10-x64\publish` 폴더에 복사된다. 이 폴더 안에는 `exe` 파일이 존재하며 이것이 빌드된 앱이다. 이 실행파일로 앱을 실행할 수 있고, 폴더 전체를 복사해서 다른 기기에서 실행할 수도 있다.

이렇게 빌드된 앱이 `packaged app` 과 다른점은 폴더 내에 `.NET Runtime` 이 포함되지 않는다는 점이다. 결국 앱을 실행하기 위해서는 먼저 실행하는 기기에 `.NET Runtime` 이 먼저 설치되어야 한다. 모든 런타임 구성요소들을 앱에 포함시키고 싶은 경우, 배포 명령어를 사용할 때 `-p:WindowsAppSDKSelfContained` 옵션을 추가하면 된다.

도큐먼트에서 제공되는 예시 명령어는 다음과 같다.

```Console
dotnet publish -f net8.0-windows10.0.19041.0 -c Release -p:RuntimeIdentifierOverride=win10-x64 -p:WindowsPackageType=None -p:WindowsAppSDKSelfContained=true
```

`dotnet publish` 커맨드의 자세한 정보는 [dotnet publish 도큐먼트](https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-publish) 를 참조하자.

이제 위의 내용을 정리하여 내 노트북과 프로젝트에 적용시켜 보자.

먼저 타겟 프레임워크를 설정해야 하는데 내 노트북은 윈도우11 이라 그냥 도큐먼트의 버전을 그대로 써보자.

그다음 build configuration 을 Release 로 설정하는 것은 .NET 8 부터 생략 가능하다고 하니 이것도 생략해보자.

64비트 컴퓨터로 사용할 것이니 `-p:RuntimeIdentifierOverride=win10-x64` 옵션을 사용하자. Unpackaged App 의 경우 `-p:WindowsPackageType=None` 을 사용하자.

`-p:WindowsAppSDKSelfContained` 옵션은 위에서 설명한대로 런타임의 포함여부를 설정하는데, 포함시키는 경우는 `self-contained` 제외하는 경우는 `framework-dependent` 라고 부른다.

두가지는 각각 장단점을 가지고 있다.

||Deploy framework-dependent|Deploy self-contained|
|:--|:--|:--|
|**장점**|*Small deployment*. Only your app and its other dependencies are distributed. The Windows App SDK runtime and Framework package are installed automatically by framework-dependent apps that are packaged; or as part of the Windows App SDK runtime installer by framework-dependent apps that are either packaged with external location or unpackaged.<br><br>*Serviceable*. Servicing updates to the Windows App SDK are installed automatically via the Windows App SDK Framework package without any action required of the app.|*Control Windows App SDK version*. You control which version of the Windows App SDK is deployed with your app. Servicing updates of the Windows App SDK won't impact your app unless you rebuild and redistribute it.<br><br>*Isolated from other apps*. Apps and users can't uninstall your Windows App SDK dependency without uninstalling your entire app.<br><br>*Xcopy deployment*. Since the Windows App SDK dependencies are carried by your app, you can deploy your app by simply xcopy-ing your build output, without any additional installation requirements.|
|**단점**|Additional installation dependencies. Requires installation of the Windows App SDK runtime and/or Framework package, which can add complexity to app installation.<br><br>Shared dependencies. Risk that shared dependencies are uninstalled. Apps or users uninstalling the shared components can impact the user experience of other apps that share the dependency.<br><br>Compatibility risk. Risk that servicing updates to the Windows App SDK introduce breaking changes. While servicing updates should provide backward compatibility, it's possible that regressions are introduced.|*Larger deployments (unpackaged apps only)*. Because your app includes the Windows App SDK, the download size and hard drive space required are greater than would be the case for a framework-dependent version.<br><br>*Performance (unpackaged apps only)*. Slower to load, and uses more memory since code pages aren't shared with other apps.<br><br>*Not serviceable*. The Windows App SDK version distributed with your app can be updated only by releasing a new version of your app. You're responsible for integrating servicing updates of the Windows App SDK into your app.|

일단 나는 포함시키는것으로 해보자. 크기는 좀 커지겠지만 확실한 방법이니..

완성된 나의 배포 명령어! `dotnet publish -f net8.0-windows10.0.19041.0 -p:RuntimeIdentifierOverride=win10-x64 -p:WindowsPackageType=None -p:WindowsAppSDKSelfContained=true`

실행하니 백신에 실행파일이 걸려서 복사에 실패했다.... 백신 끄고 다시해보자.

1분도 안돼서 완료됐다. 실행 결과는 프로젝트의 `bin\Release\net8.0-windows10.0.19041.0\win10-x64\publish` 경로에 생성된다. 실행파일을 실행해보니 잘 동작한다. 압축파일을 만들어서 집에서도 테스트해보자. 후후

집에서 실행해본 결과, .NET 환경 설치창이 자동으로 뜨더라. 런타임을 포함시킨다고 해도 기본적인 .NET 설치는 필요한가 보다.



---

## Resources
- .NET MAUI 도큐먼트 : https://learn.microsoft.com/ko-kr/dotnet/maui/?view=net-maui-8.0
- 좋은 .NET MAUI 라이브러리, 리소스가 정리된 깃헙(awesome-dotnet-maui) : https://github.com/jsuarezruiz/awesome-dotnet-maui


[^1]: UI와 UI가 아닌 코드를 분리하기 위한 아키텍처 디자인 패턴이다. https://learn.microsoft.com/ko-kr/windows/uwp/data-binding/data-binding-and-mvvm