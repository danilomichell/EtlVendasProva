using System.Diagnostics;
using EtlVendasProva.Data.Context;

namespace EtlVendasProva.Processamento.Etl
{
    public class Extract
    {

        public List<DateTime> Tempo { get; private set; } = new();
        public Extract(VendasContext context)
        {
            ExtrairTempo(context);
        }

        private void ExtrairTempo(VendasContext context)
        {
            Console.WriteLine("Iniciando extração do Tempo");
            var sw = new Stopwatch();
            sw.Start();
           // Tempo = context.Vendas.Select(x => x.Data).Distinct().ToList();
            sw.Stop();

            Console.WriteLine($"Finalizando extração do Tempo" +
                              $" - Total extraido: {Tempo.Count}" +
                              $" - Tempo de extração: {sw.Elapsed.TotalSeconds} segundos.");

        }
    }
}
