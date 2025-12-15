using SMC.Framework.Mapper;
using System;

namespace SMC.Academico.Domain.Areas.CNC.ValueObjects
{
    public class InformacoesTramitacaoEmecVO : ISMCMappable
    {
        public int NumeroProcesso { get; set; }
        public string TipoProcesso { get; set; }
        public DateTimeOffset DataCadastro { get; set; }
        public DateTimeOffset DataProtocolo { get; set; }
    }
}
