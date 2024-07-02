using CommunityToolkit.Mvvm.ComponentModel;

namespace Contador_de_Dinheiro.MVVM.Models;

public partial class DinheiroModel : ObservableObject
{
    public double Valor { get; set; }

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ValorTotal))]
    private int quantidade;

    public double ValorTotal => Valor * Quantidade;

    public string CaminhoDoIconeLight { get; set; }
    public string CaminhoDoIconeDark { get; set; }

    public DinheiroModel(double valor, string caminhoDoIconeLight, string caminhoDoIconeDark)
    {
        Valor = valor;
        quantidade = 0;
        CaminhoDoIconeLight = caminhoDoIconeLight;
        CaminhoDoIconeDark = caminhoDoIconeDark;
    }
}
