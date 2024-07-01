using CommunityToolkit.Mvvm.ComponentModel;

namespace Contador_de_Dinheiro.MVVM.Models;

public partial class DinheiroModel : ObservableObject
{
    public double Valor { get; set; }

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ValorTotal))]
    private int quantidade;

    public double ValorTotal => Valor * Quantidade;

    public DinheiroModel(double valor)
    {
        Valor = valor;
        quantidade = 0;
    }
}
