using System.Data;
using System.Diagnostics;
using EtlVendasProva.Data.Context;
using EtlVendasProva.Data.Domain.Entities.Dw;
using EtlVendasProva.Processamento.Etl;
using Microsoft.EntityFrameworkCore;

namespace EtlVendasProva.Processamento.Etl
{
    public class Load
    {
        public Load(Transform transform, VendasDwContext context)
        {
            CarregarDmTempo(transform.DmTempo, context);
            CarregarDmClientes(transform.DmClientes, context);
            CarregarDmProduto(transform.DmProduto, context);
            CarregarDmVendedor(transform.DmVendedor, context);
            CarregaFtVendas(transform.FtVendas, context);
        }

        public void CarregarDmTempo(List<DmTempo> tempos, VendasDwContext context)
        {
            Console.WriteLine("Iniciando cargda dos tempos");
            var sw = new Stopwatch();
            sw.Start();
            foreach (var item in tempos)
            {
                using var command = context.Database.GetDbConnection().CreateCommand();
                if (command.Connection!.State != ConnectionState.Open) command.Connection.Open();
                var tempoExist = context.DmTempo.FirstOrDefault(x => x.IdTempo == item.IdTempo);
                var cmd = tempoExist != null
                    ? $@"UPDATE dimensional.DM_TEMPO 
                                                    SET DATA_VENDA = '{item.DataVenda}',
                                                        SG_MES = '{item.SgMes}',
                                                        NM_MES = '{item.NmMes}',
                                                        TRIMESTRE = '{item.Trimestre}',
                                                        NU_MES = '{item.NuMes}'
                                                    WHERE ID_TEMPO = {item.IdTempo}"
                    : $@"INSERT INTO dimensional.DM_TEMPO
                                                    (ID_TEMPO,DATA_VENDA,SG_MES,NM_MES,TRIMESTRE,NU_MES)
                                                    VALUES
                                                    ({item.IdTempo}, '{item.DataVenda}','{item.SgMes}','{item.NmMes}','{item.Trimestre}',{item.NuMes})";
                command.CommandText = "set datestyle = 'iso, dmy';" + cmd;
                command.ExecuteNonQuery();
                command.Connection.Close();
            }
            context.SaveChanges();
            sw.Stop();
            Console.WriteLine($"Finalizando carga dos tempos" +
                              $" - Tempo de carga: {sw.Elapsed.TotalSeconds} segundos.");
        }

        public void CarregarDmClientes(List<DmClientes> clientes, VendasDwContext context)
        {
            Console.WriteLine("Iniciando cargda dos clientes");
            var sw = new Stopwatch();
            sw.Start();
            foreach (var item in clientes)
            {
                var itemExist = context.DmClientes.FirstOrDefault(x => x.IdCliente == item.IdCliente);
                if (itemExist != null)
                {
                    itemExist.ClasseCliente = item.ClasseCliente;
                    itemExist.EstadoCliente = item.EstadoCliente;
                    itemExist.NomeCliente = item.NomeCliente;
                    itemExist.SexoCliente = item.SexoCliente;
                    context.DmClientes.Update(itemExist);
                }
                else
                {
                    context.DmClientes.Add(item);
                }
            }
            context.SaveChanges();
            sw.Stop();
            Console.WriteLine($"Finalizando carga das clientes" +
                              $" - Tempo de carga: {sw.Elapsed.TotalSeconds} segundos.");
        }
        public void CarregarDmProduto(List<DmProduto> produtos, VendasDwContext context)
        {
            Console.WriteLine("Iniciando cargda dos produtos");
            var sw = new Stopwatch();
            sw.Start();
            foreach (var item in produtos)
            {
                var itemExist = context.DmProduto.FirstOrDefault(x => x.IdProduto == item.IdProduto);
                if (itemExist != null)
                {
                    itemExist.NomeProduto = item.NomeProduto;
                    itemExist.PrecoProduto = item.PrecoProduto;
                    itemExist.ClasseProduto = item.ClasseProduto;
                    context.DmProduto.Update(itemExist);
                }
                else
                {
                    context.DmProduto.Add(item);
                }
            }
            context.SaveChanges();
            sw.Stop();
            Console.WriteLine($"Finalizando carga dos produtos" +
                              $" - Tempo de carga: {sw.Elapsed.TotalSeconds} segundos.");
        }

        public void CarregarDmVendedor(List<DmVendedor> vendedor, VendasDwContext context)
        {
            Console.WriteLine("Iniciando cargda dos vendedores");
            var sw = new Stopwatch();
            sw.Start();
            foreach (var item in vendedor)
            {
                var itemExist = context.DmVendedor.FirstOrDefault(x => x.IdVendedor == item.IdVendedor);
                if (itemExist != null)
                {
                    itemExist.NomeVendedor = item.NomeVendedor;
                    itemExist.NivelVendedor = item.NivelVendedor;
                    context.DmVendedor.Update(itemExist);
                }
                else
                {
                    context.DmVendedor.Add(item);
                }
            }
            context.SaveChanges();
            sw.Stop();
            Console.WriteLine($"Finalizando carga dos vendedores" +
                              $" - Tempo de carga: {sw.Elapsed.TotalSeconds} segundos.");
        }


        public void CarregaFtVendas(List<FtVendas> ftVendas, VendasDwContext context)
        {
            Console.WriteLine("Iniciando cargda dos Vendas");
            var sw = new Stopwatch();
            sw.Start();
            var valores = context.FtVendas.ToList();
            if (valores.Count != 0)
            {
                context.RemoveRange(valores);
                context.SaveChanges();
            }
            context.FtVendas.AddRange(ftVendas);
            context.SaveChanges();
            sw.Stop();
            Console.WriteLine($"Finalizando carga das Vendas" +
                              $" - Tempo de carga: {sw.Elapsed.TotalSeconds} segundos.");
        }
    }
}

