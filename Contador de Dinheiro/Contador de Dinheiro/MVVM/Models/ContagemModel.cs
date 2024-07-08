using SQLite;
using SQLiteNetExtensions.Attributes;

namespace Contador_de_Dinheiro.MVVM.Models;

[Table("Contagem")]
public class ContagemModel
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    [OneToMany(CascadeOperations = CascadeOperation.All)]
    public List<DinheiroModel> Notas { get; set; }

    [OneToMany(CascadeOperations = CascadeOperation.All)]
    public List<DinheiroModel> Moedas { get; set; }

    public double SomaDasMoedas { get; set; }
    public double SomaDasNotas { get; set; }
    public double SomaTotal { get; set; }
    public DateTime Data { get; set; }
    public string Nome { get; set; }

    public ContagemModel(List<DinheiroModel> notas, List<DinheiroModel> moedas, double somaDasNotas, double somaDasMoedas, double somaTotal, string nome)
    {
        Notas = notas;
        Moedas = moedas;
        SomaDasMoedas = somaDasMoedas;
        SomaDasNotas = somaDasNotas;
        SomaTotal = somaTotal;
        Nome = nome;
        Data = DateTime.Now;
    }

    public ContagemModel() { }
}
