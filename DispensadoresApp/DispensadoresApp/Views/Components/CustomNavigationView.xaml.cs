using Xamarin.Forms;


namespace DispensadoresApp.Views
{
    public partial class CustomNavigationView : NavigationPage
    {
        public CustomNavigationView() : base()
        {
            InitializeComponent();
        }
        public CustomNavigationView(Page root) : base(root)
        {
            InitializeComponent();
        }
    }
}