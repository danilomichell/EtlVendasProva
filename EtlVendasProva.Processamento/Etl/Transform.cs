using System.Diagnostics;
using EtlVendasProva.Data.Domain.Entities.Dw;
using EtlVendasProva.Data.Domain.Entities.Relacional;
using EtlVendasProva.Processamento.Etl;

namespace EtlVendasProva.Processamento.Etl
{
    public class Transform
    {
        public List<DmTempo> DmTempo { get; private set; } = new();
        public List<DmClientes> DmClientes { get; private set; } = new();
        public List<DmProduto> DmProduto { get; private set; } = new();

        public List<DmVendedor> DmVendedor { get; private set; } = new();
        public List<FtVendas> FtVendas { get; private set; } = new();

        public Transform(Extract extracao)
        {
            TransformarTempo(extracao.Tempo);
            TransformarClientes(extracao.Clientes);
            TransformarVendedor(extracao.Vendedores);
            TransformarProdutos(extracao.Produtos);
            //            TransformarGravadoras(extracao.Gravadoras);
        }

        private void TransformarTempo(List<DateOnly> tempo)
        {
            Console.WriteLine("Iniciando transformação do tempo");
            var sw = new Stopwatch();
            sw.Start();
            foreach (var item in tempo)
            {
                DmTempo.Add(new DmTempo
                {
                    IdTempo = item.Year * 10000 + item.Month * 100 + item.Day,
                    DataVenda = item,
                    NmMes = NomeMes(item.Month),
                    NuMes = item.Month,
                    SgMes = NomeMes(item.Month)[..3],
                    Trimestre = item.Month <= 3 ? "Primeiro" :
                        item.Month <= 6 ? "Segundo" :
                        item.Month <= 9 ? "Terceiro" : "Quarto"
                });
            }

            sw.Stop();

            Console.WriteLine($"Finalizando transformação do tempo" +
                              $" - Tempo de transformação: {sw.Elapsed.TotalSeconds} segundos.");

        }

        private void TransformarClientes(List<Clientes> clientes)
        {
            Console.WriteLine("Iniciando transformação dos clientes");
            var sw = new Stopwatch();
            sw.Start();
            foreach (var item in clientes)
            {
                DmClientes.Add(new DmClientes()
                {
                    IdCliente = item.Idcliente,
                    NomeCliente = item.Cliente,
                    EstadoCliente = item.Estado,
                    SexoCliente = item.Sexo,
                    ClasseCliente = item.Status
                });
            }

            sw.Stop();

            Console.WriteLine($"Finalizando transformação dos clientes" +
                              $" - Tempo de transformação: {sw.Elapsed.TotalSeconds} segundos.");
        }

        private void TransformarProdutos(List<Produtos> produtos)
        {
            Console.WriteLine("Iniciando transformação dos produtos");
            var sw = new Stopwatch();
            sw.Start();
            foreach (var item in produtos)
            {
                DmProduto.Add(new DmProduto
                {
                    IdProduto = item.Idproduto,
                    NomeProduto = item.Produto,
                    PrecoProduto = item.Preco,
                    ClasseProduto = item.Preco < 500 ? "Popular" : item.Preco < 3000 ? "Media" : "Alta"
                });
            }

            sw.Stop();

            Console.WriteLine($"Finalizando transformação dos produtos" +
                              $" - Tempo de transformação: {sw.Elapsed.TotalSeconds} segundos.");
        }

        private void TransformarVendedor(List<Vendedores> vendedores)
        {
            Console.WriteLine("Iniciando transformação dos vendedores");
            var sw = new Stopwatch();
            sw.Start();
            foreach (var item in vendedores)
            {
                DmVendedor.Add(new DmVendedor
                {
                    IdVendedor = item.Idvendedor,
                    NomeVendedor = item.Nome,
                    NivelVendedor = VendasTotais(item.Vendas.ToList())
                });
            }

            sw.Stop();

            Console.WriteLine($"Finalizando transformação dos vendedores" +
                              $" - Tempo de transformação: {sw.Elapsed.TotalSeconds} segundos.");
        }

        private void TransformarFtLocacoes(List<Vendas> vendas)
        {
            Console.WriteLine("Iniciando transformação das Locações");
            var sw = new Stopwatch();
            sw.Start();

            foreach (var venda in vendas)
            {
                foreach (var item in venda.Itensvenda)
                {
                    FtVendas.Add(new FtVendas
                    {
                        IdVendedor = venda.Idvendedor,
                        IdProduto = item.Idproduto,
                        IdCliente = venda.Idcliente,
                        IdTempo = venda.Data.Year * 10000 + venda.Data.Month * 100 + venda.Data.Day,
                        DescontoTotal = venda.Itensvenda.Sum(x => x.Desconto),
                        ValTotalVenda = venda.Itensvenda.Sum(x => x.Valortotal),
                        ValUnitarioProduto = item.Valorunitario,
                        QtdVendasRealizadas = venda.Itensvenda.Sum(x => x.Quantidade)
                    });
                }
            }

            sw.Stop();

            Console.WriteLine($"Finalizando transformação das Locações" +
                              $" - Tempo de transformação: {sw.Elapsed.TotalSeconds} segundos.");
        }

        private string NomeMes(int mes)
        {
            return mes switch
            {
                1 => "Janeiro",
                2 => "Fevereiro",
                3 => "Março",
                4 => "Abril",
                5 => "Maio",
                6 => "Junho",
                7 => "Julho",
                8 => "Agosto",
                9 => "Setembro",
                10 => "Outubro",
                11 => "Novembro",
                _ => "Dezembro",
            };
        }

        public string VendasTotais(List<Vendas> vendas)
        {
            var vendasTotais = vendas.Sum(x => x.Total);
            return vendasTotais switch
            {
                <= 199000 => "Nivel 1",
                <= 299000 => "Nivel 2",
                _ => "Nivel 2"
            };
        }
    }
}

