using Contador_de_Dinheiro.MVVM.Models;
using Contador_de_Dinheiro.MVVM.ViewModels;

namespace Contador_de_Dinheiro.MVVM.Views;

public partial class ContagemView : ContentPage
{
	public ContagemView()
	{
		InitializeComponent();
        BindingContext = new ContagemViewModel();
	}

    public ContagemView(ContagemModel? contagem)
    {
        InitializeComponent();
        BindingContext = new ContagemViewModel(contagem);
    }
}