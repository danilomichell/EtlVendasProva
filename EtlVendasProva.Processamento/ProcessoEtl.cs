using System.Data;
using EtlVendasProva.Data.Context;
using EtlVendasProva.Processamento.Etl;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace EtlVendasProva.Processamento
{
    public class ProcessoEtl : IProcessoEtl
    {
        private readonly VendasContext _context;
        private readonly VendasDwContext _dwContext;

        public ProcessoEtl(VendasContext context, VendasDwContext dwContext)
        {
            _context = context;
            _dwContext = dwContext;
        }
        public void Processar(int opt)
        {
            switch (opt)
            {
                case 1:
                    Exclude();
                    break;
                case 2:
                    ProcessarEtl();
                    break;
            }
        }

        private void ProcessarEtl()
        {
            var extracao = new Extract(_context);

            var trasformacao = new Transform(extracao);

            _ = new Load(trasformacao, _dwContext);

            Console.WriteLine("Finalizado o ETL");
        }


        private void Exclude()
        {

            Truncate(TableName(_dwContext.FtVendas));

            //Truncate(TableName(_dwContext.DmArtista));

            //Truncate(TableName(_dwContext.DmGravadora));

            //Truncate(TableName(_dwContext.DmSocio));

            Truncate(TableName(_dwContext.DmTempo));

            //Truncate(TableName(_dwContext.DmTitulo));
        }

        private void Truncate(string tableName)
        {
            var cmd = $"delete from {tableName}";
            using var command = _dwContext.Database.GetDbConnection().CreateCommand();
            if (command.Connection!.State != ConnectionState.Open)
            {
                command.Connection.Open();
            }

            command.CommandText = cmd;
            command.ExecuteNonQuery();
            command.Connection.Close();
        }

        private static string GetName(IReadOnlyAnnotatable entityType, string defaultSchemaName = "dwlocadora")
        {
            var schema = entityType.FindAnnotation("Relational:Schema")!.Value;
            var tableName = entityType.GetAnnotation("Relational:TableName").Value!.ToString();
            var schemaName = schema == null ? defaultSchemaName : schema.ToString();
            var name = $"{schemaName}.{tableName}";
            return name;
        }

        private static string TableName<T>(DbSet<T> dbSet) where T : class
        {
            var entityType = dbSet.EntityType;
            return GetName(entityType);
        }
    }
    public interface IProcessoEtl
    {
        void Processar(int opt);
    }
}
