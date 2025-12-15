using SMC.Academico.Common.Areas.DCT.Enums;
using SMC.Framework;
using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.DCT.ValueObjects
{
    public class InstituicaoExternaListaVO : ISMCSeq, ISMCMappable
    {
        public long Seq { get; set; }

        public string Sigla { get; set; }

        public string Nome { get; set; }

        public int CodigoPais { get; set; }

        public string DescricaoPais { get; set; }

        public TipoInstituicaoEnsino? TipoInstituicaoEnsino { get; set; }

        [SMCMapProperty("CategoriaInstituicaoEnsino.Descricao")]
        public string DescricaoCategoria { get; set; }

        public bool Ativo { get; set; }

        public bool EhInstituicaoEnsino { get; set; }
    }
}