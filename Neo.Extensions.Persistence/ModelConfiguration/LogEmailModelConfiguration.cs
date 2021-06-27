using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Neo.Extensions.Persistence.Entities;

namespace Neo.Extensions.Persistence.ModelConfiguration
{   
    public class LogEmailModelConfiguration : Microsoft.EntityFrameworkCore.IEntityTypeConfiguration<Entities.LogEmail>
    {

        public void Configure(EntityTypeBuilder<LogEmail> builder)
        {

            builder.ToTable("LOG_EMAIL");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName("ID");
            builder.Property(x => x.DataCadastro).HasColumnName("DATA_CADASTRO");
            builder.Property(x => x.Destino).HasColumnName("DESTINO");
            builder.Property(x => x.Assunto).HasColumnName("ASSUNTO");
            builder.Property(x => x.Situacao).HasColumnName("SITUACAO");
            builder.Property(x => x.DataEnvio).HasColumnName("DATA_ENVIO");
            builder.Property(x => x.DescricaoErro).HasColumnName("DESCRICAO_ERRO");

        }
    }
}
