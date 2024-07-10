using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.Input;
using Contador_de_Dinheiro.MVVM.Models;
using Contador_de_Dinheiro.MVVM.Views;
using Contador_de_Dinheiro.Services;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace Contador_de_Dinheiro.MVVM.ViewModels;

public partial class ContagensSalvasViewModel
{
    public ObservableCollection<ContagemModel> ContagensSalvas { get; private set; }

    public ContagensSalvasViewModel()
    {
        ContagensSalvas = new();
    }

    public async void CarregaContagens()
    {
        var contagens = await BancoDeDadosService.GetContagens();

        ContagensSalvas.Clear();

        foreach (var contagem in contagens) 
        {
            ContagensSalvas.Add(contagem);
        }
    }

    [RelayCommand]
    async Task FiltraContagens()
    {
        var popup = new FiltrarContagensPopup();
        var criterioDeOrdenacao = await Shell.Current.CurrentPage.ShowPopupAsync(popup);
        List<ContagemModel> temp = new List<ContagemModel>(ContagensSalvas);

        if (criterioDeOrdenacao != null)
        {
            switch(criterioDeOrdenacao.ToString())
            {
                case "ValorDesc":
                    temp = temp.OrderByDescending(contagem => contagem.SomaTotal).ToList();
                    break;
                case "ValorAsc":
                    temp = temp.OrderBy(contagem => contagem.SomaTotal).ToList();
                    break;
                case "DataDesc":
                    temp = temp.OrderByDescending(contagem => contagem.Data).ToList();
                    break;
                case "DataAsc":
                    temp = temp.OrderBy(contagem => contagem.Data).ToList();
                    break;
            }
        }

        ContagensSalvas.Clear();

        foreach (var contagem in temp)
        {
            Debug.WriteLine("odoido");
            ContagensSalvas.Add(contagem);
        }
    }

    [RelayCommand]
    async Task NovaContagem()
    {
        await Shell.Current.Navigation.PushAsync(new ContagemView());
    }

    [RelayCommand]
    async Task VizualizaContagem(ContagemModel contagem)
    {
        await Shell.Current.Navigation.PushAsync(new ContagemSelecionadaView(contagem));
    }

    [RelayCommand]
    async Task DeletaContagem(ContagemModel contagem)
    {
        await BancoDeDadosService.DeletaContagem(contagem);
        CarregaContagens();
    }
}
