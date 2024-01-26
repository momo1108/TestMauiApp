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

