using Contador_de_Dinheiro.MVVM.ViewModels;

namespace Contador_de_Dinheiro.MVVM.Views;

public partial class ContagensSalvasView : ContentPage
{
    ContagensSalvasViewModel viewModel;

	public ContagensSalvasView()
	{
		InitializeComponent();
        viewModel = new();
        BindingContext = viewModel;
	}

    protected override void OnAppearing()
    {
        viewModel.CarregaContagens();
    }
}