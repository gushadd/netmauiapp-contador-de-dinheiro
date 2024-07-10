using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Contador_de_Dinheiro.MVVM.Models;
using Contador_de_Dinheiro.MVVM.Views;
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

    public async void InicializaAsync(ContagemModel contagem)
    {
        Contagem = await BancoDeDadosService.GetContagemPorId(contagem.Id);
    }

    [RelayCommand]
    async Task Editar()
    {
        await Shell.Current.Navigation.PushAsync(new ContagemView(Contagem));
    }
}
