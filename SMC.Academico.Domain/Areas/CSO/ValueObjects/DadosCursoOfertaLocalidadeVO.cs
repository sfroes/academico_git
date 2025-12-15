using SMC.Academico.Domain.Areas.CNC.ValueObjects;
using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.CSO.ValueObjects
{
    public class DadosCursoOfertaLocalidadeVO : ISMCMappable
    {
        public long Seq { get; set; }

        public string NomeCursoCurriculo { get; set; }

        public long SeqModalidade { get; set; }

        public int? CodigoOrgaoRegulador { get; set; }

        public EnderecoVO Endereco { get; set; }
    }
}
