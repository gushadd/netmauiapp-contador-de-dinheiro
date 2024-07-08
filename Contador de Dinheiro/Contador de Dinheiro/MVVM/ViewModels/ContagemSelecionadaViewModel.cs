using CommunityToolkit.Mvvm.ComponentModel;
using Contador_de_Dinheiro.MVVM.Models;
using Contador_de_Dinheiro.Services;

namespace Contador_de_Dinheiro.MVVM.ViewModels;

public partial class ContagemSelecionadaViewModel : ObservableObject
{
    [ObservableProperty]
    public ContagemModel contagem;

    public ContagemSelecionadaViewModel(ContagemModel contagem)
    {
        InicializaAsync(contagem);
    }

    private async void InicializaAsync(ContagemModel contagem)
    {
        Contagem = await BancoDeDadosService.GetContagemPorId(contagem.Id);
    }
}
