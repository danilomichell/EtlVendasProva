using System.Diagnostics;
using EtlVendasProva.Data.Context;
using EtlVendasProva.Data.Domain.Entities.Relacional;
using Microsoft.EntityFrameworkCore;

namespace EtlVendasProva.Processamento.Etl
{
    public class Extract
    {

        public List<DateOnly> Tempo { get; private set; } = new();
        public List<Clientes> Clientes { get; private set; } = new();
        public List<Produtos> Produtos { get; private set; } = new();
        public List<Vendedores> Vendedores { get; private set; } = new();
        public List<Vendas> Vendas { get; private set; } = new();
        public Extract(VendasContext context)
        {
            ExtrairTempo(context);
            ExtrairCliente(context);
            ExtrairProdutos(context);
            ExtrairVendedores(context);
            ExtrairVendas(context);
        }

        private void ExtrairTempo(VendasContext context)
        {
            Console.WriteLine("Iniciando extração do Tempo");
            var sw = new Stopwatch();
            sw.Start();
            Tempo = context.Vendas.Select(x => x.Data).Distinct().ToList();
            sw.Stop();

            Console.WriteLine($"Finalizando extração do Tempo" +
                              $" - Total extraido: {Tempo.Count}" +
                              $" - Tempo de extração: {sw.Elapsed.TotalSeconds} segundos.");

        }

        private void ExtrairCliente(VendasContext context)
        {
            Console.WriteLine("Iniciando extração dos clientes");
            var sw = new Stopwatch();
            sw.Start();
            Clientes = context.Clientes.Distinct().ToList();
            sw.Stop();

            Console.WriteLine($"Finalizando extração dos clientes" +
                              $" - Total extraido: {Tempo.Count}" +
                              $" - Tempo de extração: {sw.Elapsed.TotalSeconds} segundos.");

        }
        private void ExtrairProdutos(VendasContext context)
        {
            Console.WriteLine("Iniciando extração dos produtos");
            var sw = new Stopwatch();
            sw.Start();
            Produtos = context.Produtos.Distinct().ToList();
            sw.Stop();

            Console.WriteLine($"Finalizando extração dos produtos" +
                              $" - Total extraido: {Tempo.Count}" +
                              $" - Tempo de extração: {sw.Elapsed.TotalSeconds} segundos.");

        }

        private void ExtrairVendedores(VendasContext context)
        {
            Console.WriteLine("Iniciando extração dos vendedores");
            var sw = new Stopwatch();
            sw.Start();
            Vendedores = context.Vendedores.Include(x => x.Vendas).Distinct().ToList();
            sw.Stop();

            Console.WriteLine($"Finalizando extração dos vendedores" +
                              $" - Total extraido: {Tempo.Count}" +
                              $" - Tempo de extração: {sw.Elapsed.TotalSeconds} segundos.");

        }
        
        private void ExtrairVendas(VendasContext context)
        {
            Console.WriteLine("Iniciando extração dos vendedores");
            var sw = new Stopwatch();
            sw.Start();
            Vendas = context.Vendas.Include(x => x.Itensvenda)
                                     .ThenInclude(x => x.IdprodutoNavigation)
                                     .Include(x=>x.IdclienteNavigation)
                                     .Include(x=>x.IdvendedorNavigation)
                                     .ToList();
            sw.Stop();

            Console.WriteLine($"Finalizando extração dos vendedores" +
                              $" - Total extraido: {Tempo.Count}" +
                              $" - Tempo de extração: {sw.Elapsed.TotalSeconds} segundos.");

        }
    }
}
