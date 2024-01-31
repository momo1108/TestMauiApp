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
}
```

string text 필드를 제외한 모든 코드를 삭제 후, CommunityToolkit.Mvvm.ComponentModel 에서 제공하는 ObservableObject를 상속받자.

ObservableObject 엔 내부적으로 이벤트 관련 기능이 포함되어 있다.

이제 입력창에 바인딩할 필드인 text 에 소스 제너레이터를 사용해보자. `[ObservableProperty]` 를 사용해 이전에 직접 지정했었던 getter 와 setter, event invoke 까지 모두 자동으로 소스코드가 생성된다.

관련 코드들은 솔루션 익스플로러에서 dependency에서 analyzer 쪽에 생성되는 걸 확인할 수 있다.

마찬가지로 Todo Item들을 저장할 필드도 생성하자. System.Collections.ObjectModel 에서 제공하는 ObservableCollection 을 사용한다.

이제 추가를 위한 이벤트 핸들러를 만들자. 마찬가지로 소스 제너레이터 `[RelayCommand]` 를 통해 우리는 기능과 관련된 코드만 작성하면 된다.

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

이제 Entry 와 Button 에 각각 데이터와 이벤트핸들러를 바인딩하자.

```xaml
<Entry Placeholder="Enter task"
        Grid.Row="1"
        Text="{Binding Text}"/>
<Button Text="Add"
        Command="{Binding AddCommand}"
        Grid.Row="1"
        Grid.Column="1" />
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

[^1]: UI와 UI가 아닌 코드를 분리하기 위한 아키텍처 디자인 패턴이다. https://learn.microsoft.com/ko-kr/windows/uwp/data-binding/data-binding-and-mvvm