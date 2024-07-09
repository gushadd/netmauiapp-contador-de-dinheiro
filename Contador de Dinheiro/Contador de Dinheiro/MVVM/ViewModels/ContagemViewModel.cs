using System.ComponentModel;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Maui.Alerts;
using Contador_de_Dinheiro.MVVM.Models;
using Contador_de_Dinheiro.MVVM.Views;
using CommunityToolkit.Maui.Core;
using Contador_de_Dinheiro.Services;

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
    public string Nome {  get; set; }

    private int _contagemId; 

    // Construtor caso não haja parâmetro
    public ContagemViewModel()
    {
        _contagemId = 0;

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

        AssinaEventoPropertyChangedParaNotasEMoedas();
    }

    // Construtor caso haja parâmetro
    public ContagemViewModel(ContagemModel? contagem)
    {
        if (contagem != null)
        {
            Moedas = contagem.Moedas;
            Notas = contagem.Notas;
            SomaDasNotas = contagem.SomaDasNotas;
            SomaDasMoedas = contagem.SomaDasMoedas;
            Nome = contagem.Nome;
            _contagemId = contagem.Id;
        }

        AssinaEventoPropertyChangedParaNotasEMoedas();
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

    private void AssinaEventoPropertyChangedParaNotasEMoedas()
    {
        foreach (var moeda in Moedas)
        {
            moeda.PropertyChanged += OnDinheiroModelPropertyChanged;
        }

        foreach (var nota in Notas)
        {
            nota.PropertyChanged += OnDinheiroModelPropertyChanged;
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

    [RelayCommand]
    async Task Salvar()
    {
        string nome = Nome;

        if (string.IsNullOrWhiteSpace(nome))
        {
            nome = "Contagem sem nome";
        }

        ContagemModel contagem = new(Notas, Moedas, SomaDasNotas, SomaDasMoedas, SomaTotal, nome)
        {
            Id = _contagemId // Preserva o Id da contagem, se existente
        };

        try
        {
            // Salva contagem nova
            if (_contagemId == 0)
            {
                await BancoDeDadosService.SalvaContagem(contagem);
                var toast = Toast.Make("Contagem salva com sucesso!", ToastDuration.Short, 14);
                await toast.Show();
            }
            // Atualiza contagem existente
            else
            {
                await BancoDeDadosService.AtualizaContagem(contagem);
                var toast = Toast.Make("Contagem atualizada com sucesso!", ToastDuration.Short, 14);
                await toast.Show();
            }

        }
        catch (Exception ex)
        {
            var toast = Toast.Make($"Ocorreu um erro ao salvar {ex.Message}", ToastDuration.Short, 14);
            await toast.Show();
        }      
    }
}
