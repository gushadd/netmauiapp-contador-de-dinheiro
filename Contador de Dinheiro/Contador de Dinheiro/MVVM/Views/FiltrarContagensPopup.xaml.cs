using CommunityToolkit.Maui.Views;

namespace Contador_de_Dinheiro.MVVM.Views;

public partial class FiltrarContagensPopup : Popup
{
	public FiltrarContagensPopup()
	{
		InitializeComponent();
	}

    private void Cancelar_Clicked(object sender, EventArgs e)
    {
        Close();
    }

    private void Ordenar_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if(sender is RadioButton radioButton && e.Value)
        {
            string valor = radioButton.Value.ToString();
            Close(valor);
        }
    }
}