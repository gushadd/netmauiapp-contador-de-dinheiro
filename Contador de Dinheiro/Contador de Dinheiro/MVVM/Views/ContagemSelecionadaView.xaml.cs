using Contador_de_Dinheiro.MVVM.Models;
using Contador_de_Dinheiro.MVVM.ViewModels;

namespace Contador_de_Dinheiro.MVVM.Views;

public partial class ContagemSelecionadaView : ContentPage
{
    private ContagemModel _contagem;
    private ContagemSelecionadaViewModel _viewModel;

	public ContagemSelecionadaView(ContagemModel contagem)
	{
        InitializeComponent();
        _contagem = contagem;
        _viewModel = new ContagemSelecionadaViewModel(contagem);
        BindingContext = _viewModel;
    }

    protected override void OnAppearing()
    {
        _viewModel.InicializaAsync(_contagem);
    }
}