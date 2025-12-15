using SMC.Academico.Domain.Areas.CNC.ValueObjects;
using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.ORG.ValueObjects
{
    public class DadosInstituicaoEnsinoVO : ISMCMappable
    {
        public string Nome { get; set; }
        public string Cnpj { get; set; }
        public int CodigoMEC { get; set; }
        public long SeqMantenedora { get; set; }
        public EnderecoVO Endereco { get; set; }
    }
}
