#if ANDROID

using Microsoft.Maui.Controls.Compatibility.Platform.Android;
            
#endif

namespace Contador_de_Dinheiro
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();


            //if (File.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "contagens.db3")))
            //{
            //    File.Delete(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "contagens.db3"));
            //}


            MainPage = new AppShell();
        }
    }
}
