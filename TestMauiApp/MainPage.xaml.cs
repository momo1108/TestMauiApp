using TestMauiApp.ViewModel;

namespace TestMauiApp
{
    public partial class MainPage : ContentPage
    {

        public MainPage(MainViewModel vm)
        {
            InitializeComponent();
            // 메인페이지에 Context를 바인딩하자.
            BindingContext = vm;
        }

    }

}
