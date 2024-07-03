using CommunityToolkit.Maui.Views;

namespace Contador_de_Dinheiro.MVVM.Views;

public partial class AdicionarQuantidadePopup : Popup
{
	public AdicionarQuantidadePopup()
	{
		InitializeComponent();
	}

    private void Cancelar_Clicked(object sender, EventArgs e)
    {
        Close();
    }

    private void Adicionar_Clicked(object sender, EventArgs e)
    {
        Close(QuantidadeEntry.Text);
    }
}