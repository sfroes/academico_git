using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.CNC.ValueObjects
{
    public class PoloVO : ISMCMappable
    {
        public string Nome { get; set; }
        public EnderecoVO Endereco { get; set; }
        public int? CodigoEMEC { get; set; }
        public InformacoesTramitacaoEmecVO InformacoesTramitacaoEmec { get; set; }
    }
}
