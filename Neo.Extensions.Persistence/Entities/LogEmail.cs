using System;
using System.Collections.Generic;
using System.Text;

namespace Neo.Extensions.Persistence.Entities
{
    public class LogEmail
    {
        public Guid Id { get; set; }
        public DateTime DataCadastro { get; set; }
        public string Destino { get; set; }
        public string Assunto { get; set; }
        public string Situacao { get; set; }
        public string DescricaoErro { get; set; }
        public DateTime? DataEnvio { get; set; }
    }
}
