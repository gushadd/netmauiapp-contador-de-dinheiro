using Contador_de_Dinheiro.MVVM.Models;
using Contador_de_Dinheiro.MVVM.ViewModels;

namespace Contador_de_Dinheiro.MVVM.Views;

public partial class ContagemSelecionadaView : ContentPage
{
	public ContagemSelecionadaView(ContagemModel contagem)
	{
		InitializeComponent();
        BindingContext = new ContagemSelecionadaViewModel(contagem);
	}
}