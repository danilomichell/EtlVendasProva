using System.Diagnostics;
using EtlVendasProva.Data.Domain.Entities.Dw;
using EtlVendasProva.Processamento.Etl;

namespace EtlVendasProva.Processamento.Etl
{
    public class Transform
    {
        public List<DmTempo> DmTempo { get; private set; } = new();
        //        public List<DmSocio> DmSocios { get; private set; } = new();
        //        public List<DmTitulo> DmTitulos { get; private set; } = new();
        //        public List<DmArtista> DmArtistas { get; private set; } = new();
        //        public List<DmGravadora> DmGravadoras { get; private set; } = new();
        //        public List<FtLocacoes> FtLocacoes { get; private set; } = new();

        public Transform(Extract extracao)
        {
            TransformarTempo(extracao.Tempo);
            //            TransformarSocios(extracao.Socios);
            //            TransformarTitulos(extracao.Titulos);
            //            TransformarArtistas(extracao.Artistas);
            //            TransformarGravadoras(extracao.Gravadoras);
            //            TransformarFtLocacoes(extracao.Locacoes);
        }

        private void TransformarTempo(List<DateTime> tempo)
        {
            Console.WriteLine("Iniciando transformação do tempo");
            var sw = new Stopwatch();
            sw.Start();
            foreach (var item in tempo)
            {
                DmTempo.Add(new DmTempo
                {
                    IdTempo = item.Year * 100 + item.Month,
                    DataVenda = item,
                    NmMes = NomeMes(item.Month),
                    NuMes = item.Month,
                    SgMes = NomeMes(item.Month)[..3],
                    Trimestre = item.Month <= 3 ? "Primeiro" : item.Month <= 6 ? "Segundo" : item.Month <= 9 ? "Terceiro" : "Quarto"
                });
            }
            sw.Stop();

            Console.WriteLine($"Finalizando transformação do tempo" +
                              $" - Tempo de transformação: {sw.Elapsed.TotalSeconds} segundos.");

        }
        //        private void TransformarSocios(List<Socios> socios)
        //        {
        //            Console.WriteLine("Iniciando transformação dos Socios");
        //            var sw = new Stopwatch();
        //            sw.Start();
        //            foreach (var item in socios)
        //            {
        //                DmSocios.Add(new DmSocio
        //                {
        //                    IdSoc = item.CodSoc,
        //                    NomSoc = item.NomSoc,
        //                    TipoSocio = item.StaSoc
        //                });
        //            }
        //            sw.Stop();

        //            Console.WriteLine($"Finalizando transformação dos Socios" +
        //                              $" - Tempo de transformação: {sw.Elapsed.TotalSeconds} segundos.");
        //        }
        //        private void TransformarTitulos(List<Titulos> titulos)
        //        {
        //            Console.WriteLine("Iniciando transformação dos Titulos");
        //            var sw = new Stopwatch();
        //            sw.Start();
        //            foreach (var item in titulos)
        //            {
        //                DmTitulos.Add(new DmTitulo
        //                {
        //                    IdTitulo = item.CodTit,
        //                    TpoTitulo = item.TpoTit,
        //                    DscTitulo = item.DscTit,
        //                    ClaTitulo = item.ClaTit
        //                });
        //            }
        //            sw.Stop();

        //            Console.WriteLine($"Finalizando transformação dos Titulos" +
        //                              $" - Tempo de transformação: {sw.Elapsed.TotalSeconds} segundos.");
        //        }
        //        private void TransformarArtistas(List<Artistas> artistas)
        //        {
        //            Console.WriteLine("Iniciando transformação dos Artistas");
        //            var sw = new Stopwatch();
        //            sw.Start();
        //            foreach (var item in artistas)
        //            {
        //                DmArtistas.Add(new DmArtista
        //                {
        //                    IdArt = item.CodArt,
        //                    NacBras = item.NacBras,
        //                    NomArt = item.NomArt,
        //                    TpoArt = item.TpoArt
        //                });
        //            }
        //            sw.Stop();

        //            Console.WriteLine($"Finalizando transformação dos Artistas" +
        //                              $" - Tempo de transformação: {sw.Elapsed.TotalSeconds} segundos.");
        //        }
        //        private void TransformarGravadoras(List<Gravadoras> gravadoras)
        //        {
        //            Console.WriteLine("Iniciando transformação das Gravadoras");
        //            var sw = new Stopwatch();
        //            sw.Start();
        //            foreach (var item in gravadoras)
        //            {
        //                DmGravadoras.Add(new DmGravadora
        //                {
        //                    IdGrav = item.CodGrav,
        //                    NomGrav = item.NomGrav,
        //                    UfGrav = item.UfGrav,
        //                    NacBras = item.NacBras
        //                });
        //            }
        //            sw.Stop();

        //            Console.WriteLine($"Finalizando transformação das Gravadoras" +
        //                              $" - Tempo de transformação: {sw.Elapsed.TotalSeconds} segundos.");
        //        }

        //        private void TransformarFtLocacoes(List<Locacoes> locacoes)
        //        {
        //            Console.WriteLine("Iniciando transformação das Locações");
        //            var sw = new Stopwatch();
        //            sw.Start();

        //            foreach (var locacao in locacoes)
        //            {
        //                foreach (var item in locacao.ItensLocacoes)
        //                {
        //                    var ftLocacao = new FtLocacoes
        //                    {
        //                        IdGrav = item.Copias.CodTitNavigation.CodGrav,
        //                        IdArt = item.Copias.CodTitNavigation.CodArt,
        //                        IdSoc = locacao.CodSoc,
        //                        IdTitulo = item.Copias.CodTit,
        //                        IdTempo = locacao.DatLoc.Year * 100 + locacao.DatLoc.Month,
        //                        ValorArrecadado = locacao.ItensLocacoes.Sum(x => x.ValLoc),
        //                        TempoDevolucao = locacao.StaPgto is "P" ? 0.0M : Math.Round((decimal)Math.Abs(DateTime.Now.Subtract(locacao.DatVenc).TotalDays)),
        //                        MultaAtraso = locacao.StaPgto is "P" ? 0.0M : CalcularTempAtraso(locacao)
        //                    };
        //                    FtLocacoes.Add(ftLocacao);
        //                }
        //            }

        //            sw.Stop();

        //            Console.WriteLine($"Finalizando transformação das Locações" +
        //                              $" - Tempo de transformação: {sw.Elapsed.TotalSeconds} segundos.");
        //        }

        //        private decimal CalcularTempAtraso(Locacoes locacao)
        //        {
        //            var tempoAtrasado = Math.Abs(DateTime.Now.Subtract(locacao.DatVenc).TotalDays);

        //            decimal valorMulta = locacao.ValLoc;

        //            if (tempoAtrasado > 1)
        //            {
        //                tempoAtrasado -= 1;

        //                valorMulta += Convert.ToDecimal(tempoAtrasado * Convert.ToDouble(locacao.ValLoc * 0.40M));
        //            }
        //            return Math.Round(valorMulta, 2);
        //        }

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
    }
}
