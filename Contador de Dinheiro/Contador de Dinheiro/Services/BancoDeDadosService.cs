
using Contador_de_Dinheiro.MVVM.Models;
using SQLite;
using SQLiteNetExtensions.Extensions;
using System.Diagnostics;

namespace Contador_de_Dinheiro.Services;

public class BancoDeDadosService
{
    static SQLiteAsyncConnection bancoDeDados;

    static async Task Init()
    {
        if (bancoDeDados != null) { return; }

        var caminhoDoBanco = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "contagens.db3");
        bancoDeDados = new(caminhoDoBanco);

        await bancoDeDados.CreateTableAsync<DinheiroModel>();
        await bancoDeDados.CreateTableAsync<ContagemModel>();
    }

    public static async Task SalvaContagem(ContagemModel contagem)
    {
        await Init();
        //bancoDeDados.InsertWithChildren(contagem);

        await bancoDeDados.InsertAsync(contagem);

        foreach (var nota in contagem.Notas)
        {
            nota.ContagemId = contagem.Id;
            await bancoDeDados.InsertAsync(nota);
        }

        foreach (var moeda in contagem.Moedas)
        {
            moeda.ContagemId = contagem.Id;
            await bancoDeDados.InsertAsync(moeda);
        }
    }

    public static async Task<List<ContagemModel>> GetContagens()
    {
        await Init();       
        List<ContagemModel> contagens = await bancoDeDados.Table<ContagemModel>().ToListAsync();
        return contagens;
    }

    public static async Task<ContagemModel> GetContagemPorId(int id)
    {
        await Init();
        var query = bancoDeDados.Table<ContagemModel>().Where(c => c.Id == id);
        var contagem = await query.FirstOrDefaultAsync();

        if (contagem != null)
        {
            contagem.Notas = await bancoDeDados.Table<DinheiroModel>().Where(d => d.ContagemId == id && d.Valor > 1).ToListAsync();
            contagem.Moedas = await bancoDeDados.Table<DinheiroModel>().Where(d => d.ContagemId == id && d.Valor <= 1).ToListAsync();
        }

        return contagem;
    }
}
