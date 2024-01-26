# .NET MAUI
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

![Platform API Image]("./images/1_api.png")