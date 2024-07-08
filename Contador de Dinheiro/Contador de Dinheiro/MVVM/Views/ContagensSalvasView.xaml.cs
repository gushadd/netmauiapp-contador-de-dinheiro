using Contador_de_Dinheiro.MVVM.Models;
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

    private async void Contagens_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection != null && e.CurrentSelection.Count > 0)
        {
            var contagemSelecionada = e.CurrentSelection[0] as ContagemModel;

            if (contagemSelecionada != null)
            {
                await Navigation.PushAsync(new ContagemSelecionadaView(contagemSelecionada));
                ((CollectionView)sender).SelectedItem = null;
            }
        }
    }
}