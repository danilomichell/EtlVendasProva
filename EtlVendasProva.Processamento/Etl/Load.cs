//using System.Data;
//using System.Diagnostics;
//using EtlVendasProva.Data.Context;
//using Microsoft.EntityFrameworkCore;

//namespace EtlLocadora.Processamento.Etl
//{
//    public class Load
//    {
//        public Load(Transform transform, VendasDwContext context)
//        {
//            CarregarDmTempo(transform.DmTempo, context);
//            //            CarregarDmGravadora(transform.DmGravadoras, context);
//            //            CarregarDmSocios(transform.DmSocios, context);
//            //            CarregarDmArtista(transform.DmArtistas, context);
//            //            CarregarDmTitulo(transform.DmTitulos, context);
//            //            CarregaFtLocacoes(transform.FtLocacoes, context);
//        }

//        public void CarregarDmTempo(List<DmTempo> tempos, VendasDwContext context)
//        {
//            Console.WriteLine("Iniciando cargda dos tempos");
//            var sw = new Stopwatch();
//            sw.Start();
//            foreach (var item in tempos)
//            {
//                using var command = context.Database.GetDbConnection().CreateCommand();
//                if (command.Connection!.State != ConnectionState.Open) command.Connection.Open();
//                var tempoExist = context.DmTempo.FirstOrDefault(x => x.IdTempo == item.IdTempo);
//                var cmd = tempoExist != null ? $@"UPDATE DW_VENDAS.DM_TEMPO 
//                                                    SET DATA_Venda = '{item.Data}'
//                                                        SG_MES = '{item.SgMes}',
//                                                        NM_MES = '{item.NmMes}',
//                                                        TRIMESTRE = '{item.Trimestre}'
//                                                    WHERE ID_TEMPO = {item.NmMesano}" : $@"INSERT INTO DW_VENDAS.DM_TEMPO
//                                                    (ID_TEMPO,DATA_VENDA,SG_MES,NM_MES,TRIMESTRE)
//                                                    VALUES
//                                                    ({item.IdTempo}, {item.DataVenda},{item.SgMes},{item.NmMes},{item.Trimestre})";
//                command.CommandText = cmd;
//                command.ExecuteNonQuery();
//                command.Connection.Close();
//            }

//            //        public void CarregarDmGravadora(List<DmGravadora> gravadoras, LocadoraDwContext context)
//            //        {
//            //            var sw = new Stopwatch();
//            //            sw.Start();
//            //            foreach (var item in gravadoras)
//            //            {
//            //                var itemExist = context.DmGravadora.FirstOrDefault(x => x.IdGrav == item.IdGrav);
//            //                if (itemExist != null)
//            //                {
//            //                    itemExist.NacBras = item.NacBras;
//            //                    itemExist.NomGrav = item.NomGrav;
//            //                    itemExist.UfGrav = item.UfGrav;
//            //                    context.DmGravadora.Update(itemExist);
//            //                }
//            //                else
//            //                {
//            //                    context.DmGravadora.Add(item);
//            //                }
//            //            }
//            //            context.SaveChanges();
//            //            sw.Stop();
//            //            Console.WriteLine($"Finalizando carga das gravadoras" +
//            //                              $" - Tempo de carga: {sw.Elapsed.TotalSeconds} segundos.");
//            //        }
//            //        public void CarregarDmSocios(List<DmSocio> socios, LocadoraDwContext context)
//            //        {
//            //            var sw = new Stopwatch();
//            //            sw.Start();
//            //            foreach (var item in socios)
//            //            {
//            //                var itemExist = context.DmSocio.FirstOrDefault(x => x.IdSoc == item.IdSoc);
//            //                if (itemExist != null)
//            //                {
//            //                    itemExist.NomSoc = item.NomSoc;
//            //                    itemExist.TipoSocio = item.TipoSocio;
//            //                    context.DmSocio.Update(itemExist);
//            //                }
//            //                else
//            //                {
//            //                    context.DmSocio.Add(item);
//            //                }
//            //            }
//            //            context.SaveChanges();
//            //            sw.Stop();
//            //            Console.WriteLine($"Finalizando carga dos sócios" +
//            //                              $" - Tempo de carga: {sw.Elapsed.TotalSeconds} segundos.");
//            //        }

//            //        public void CarregarDmArtista(List<DmArtista> artistas, LocadoraDwContext context)
//            //        {
//            //            var sw = new Stopwatch();
//            //            sw.Start();
//            //            foreach (var item in artistas)
//            //            {
//            //                var itemExist = context.DmArtista.FirstOrDefault(x => x.IdArt == item.IdArt);
//            //                if (itemExist != null)
//            //                {
//            //                    itemExist.NacBras = item.NacBras;
//            //                    itemExist.NomArt = item.NomArt;
//            //                    itemExist.TpoArt = item.TpoArt;
//            //                    context.DmArtista.Update(itemExist);
//            //                }
//            //                else
//            //                {
//            //                    context.DmArtista.Add(item);
//            //                }
//            //            }
//            //            context.SaveChanges();
//            //            sw.Stop();
//            //            Console.WriteLine($"Finalizando carga dos artistas" +
//            //                              $" - Tempo de carga: {sw.Elapsed.TotalSeconds} segundos.");
//            //        }

//            //        public void CarregarDmTitulo(List<DmTitulo> titulos, LocadoraDwContext context)
//            //        {
//            //            var sw = new Stopwatch();
//            //            sw.Start();
//            //            foreach (var item in titulos)
//            //            {
//            //                var itemExist = context.DmTitulo.FirstOrDefault(x => x.IdTitulo == item.IdTitulo);
//            //                if (itemExist != null)
//            //                {
//            //                    itemExist.TpoTitulo = item.TpoTitulo;
//            //                    itemExist.ClaTitulo = item.ClaTitulo;
//            //                    itemExist.DscTitulo = item.DscTitulo;
//            //                    context.DmTitulo.Update(itemExist);
//            //                }
//            //                else
//            //                {
//            //                    context.DmTitulo.Add(item);
//            //                }
//            //            }
//            //            context.SaveChanges();
//            //            sw.Stop();
//            //            Console.WriteLine($"Finalizando carga dos títulos" +
//            //                              $" - Tempo de carga: {sw.Elapsed.TotalSeconds} segundos.");
//            //        }

//            //        public void CarregaFtLocacoes(List<FtLocacoes> ftLocacoes, LocadoraDwContext context)
//            //        {
//            //            var sw = new Stopwatch();
//            //            sw.Start();
//            //            var valores = context.FtLocacoes.ToList();
//            //            if (valores.Count != 0)
//            //            {
//            //                context.RemoveRange(valores);
//            //                context.SaveChanges();
//            //            }
//            //            context.FtLocacoes.AddRange(ftLocacoes);
//            //            context.SaveChanges();
//            //            sw.Stop();
//            //            Console.WriteLine($"Finalizando carga das locações" +
//            //                              $" - Tempo de carga: {sw.Elapsed.TotalSeconds} segundos.");
//            //        }
//        }
//    }
