using System.ComponentModel;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Contador_de_Dinheiro.MVVM.Models;
using Contador_de_Dinheiro.MVVM.Views;

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
        //Moedas =
        //[
        //    new DinheiroModel(0.05, "cinco_centavos_light.png", "cinco_centavos_dark.png"),
        //    new DinheiroModel(0.10, "dez_centavos_light.png", "dez_centavos_dark.png"),
        //    new DinheiroModel(0.25, "vinte_e_cinco_centavos_light.png", "vinte_e_cinco_centavos_dark.png"),
        //    new DinheiroModel(0.50, "cinquenta_centavos_light.png", "cinquenta_centavos_dark.png"),
        //    new DinheiroModel(1, "um_real_light.png", "um_real_dark.png")
        //];

        //Notas =
        //[
        //    new DinheiroModel(2, "dois_reais_light.png", "dois_reais_dark.png"),
        //    new DinheiroModel(5, "cinco_reais_light.png", "cinco_reais_dark.png"),
        //    new DinheiroModel(10, "dez_reais_light.png", "dez_reais_dark.png"),
        //    new DinheiroModel(20, "vinte_reais_light.png", "vinte_reais_dark.png"),
        //    new DinheiroModel(50, "cinquenta_reais_light.png", "cinquenta_reais_dark.png"),
        //    new DinheiroModel(100, "cem_reais_light.png", "cem_reais_dark.png"),
        //    new DinheiroModel(200, "duzentos_reais_light.png", "duzentos_reais_dark.png")
        //];

        Moedas =
        [
            new DinheiroModel(0.05),
            new DinheiroModel(0.10),
            new DinheiroModel(0.25),
            new DinheiroModel(0.50),
            new DinheiroModel(1)
        ];

        Notas =
        [
            new DinheiroModel(2),
            new DinheiroModel(5),
            new DinheiroModel(10),
            new DinheiroModel(20),
            new DinheiroModel(50),
            new DinheiroModel(100),
            new DinheiroModel(200)
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

    [RelayCommand]
    async Task Adicionar(DinheiroModel d)
    {
        var popup = new AdicionarQuantidadePopup();
        var resultado = await Shell.Current.CurrentPage.ShowPopupAsync(popup);

        if (resultado != null && int.TryParse(resultado.ToString(), out int quantidade)) 
        {
            d.Quantidade += quantidade;
        }
    }
}
