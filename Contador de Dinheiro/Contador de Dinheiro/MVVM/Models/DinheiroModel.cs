using CommunityToolkit.Mvvm.ComponentModel;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace Contador_de_Dinheiro.MVVM.Models;

[Table("Dinheiro")]
public partial class DinheiroModel : ObservableObject
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    [ForeignKey(typeof(ContagemModel))]
    public int ContagemId { get; set; }

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

    public DinheiroModel() { }
}
