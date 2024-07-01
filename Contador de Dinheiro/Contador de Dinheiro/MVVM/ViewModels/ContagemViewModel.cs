using System.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Contador_de_Dinheiro.MVVM.Models;

namespace Contador_de_Dinheiro.MVVM.ViewModels;

public partial class ContagemViewModel : ObservableObject
{
    public List<DinheiroModel> Notas { get; private set; }
    public List<DinheiroModel> Moedas { get; private set; }

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(SomaTotal))]
    public double somaDasNotas;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(SomaTotal))]
    public double somaDasMoedas;

    public double SomaTotal => SomaDasMoedas + SomaDasNotas;

    public ContagemViewModel()
    {
        Moedas =
        [
            new DinheiroModel(0.05),
            new DinheiroModel(0.1),
            new DinheiroModel(0.25),
            new DinheiroModel(0.5),
            new DinheiroModel(1.0)
        ];

        Notas =
        [
            new DinheiroModel(2.0),
            new DinheiroModel(5.0),
            new DinheiroModel(10.0),
            new DinheiroModel(20.0),
            new DinheiroModel(50.0),
            new DinheiroModel(100.0),
            new DinheiroModel(200.0)
        ];

        // Assina o evento PropertyChanged para todas as moedas e notas
        foreach (var moeda in Moedas)
        {
            moeda.PropertyChanged += OnDinheiroModelPropertyChanged;
        }

        foreach (var nota in Notas)
        {
            nota.PropertyChanged += OnDinheiroModelPropertyChanged;
        }
    }

    // Chamado sempre que uma propriedade de DinheiroModel muda
    private void OnDinheiroModelPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        // Verifica se a propriedade que mudou é 'ValorTotal'
        if (e.PropertyName == nameof(DinheiroModel.ValorTotal))
        {
            CalculaSoma();
        }
    }

    public void CalculaSoma()
    {
        SomaDasMoedas = Moedas.Sum(m => m.ValorTotal);
        SomaDasNotas = Notas.Sum(n  => n.ValorTotal);
    }

    [RelayCommand]
    void Deletar(DinheiroModel d)
    {
        d.Quantidade = 0;
    }
}
