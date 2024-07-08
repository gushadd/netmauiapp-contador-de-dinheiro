using Contador_de_Dinheiro.MVVM.Models;
using Contador_de_Dinheiro.Services;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace Contador_de_Dinheiro.MVVM.ViewModels;

public class ContagensSalvasViewModel
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
}
