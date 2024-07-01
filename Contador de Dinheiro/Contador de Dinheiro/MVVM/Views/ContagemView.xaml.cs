using Contador_de_Dinheiro.MVVM.ViewModels;

namespace Contador_de_Dinheiro.MVVM.Views;

public partial class ContagemView : ContentPage
{
	public ContagemView()
	{
		InitializeComponent();
        BindingContext = new ContagemViewModel();
	}
}