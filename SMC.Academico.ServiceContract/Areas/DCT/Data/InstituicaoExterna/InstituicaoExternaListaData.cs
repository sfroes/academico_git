using SMC.Academico.Common.Areas.DCT.Enums;
using SMC.Framework;
using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.DCT.Data
{
    public class InstituicaoExternaListaData : ISMCSeq, ISMCMappable
    {
        public long Seq { get; set; }

        public string Sigla { get; set; }

        public string Nome { get; set; }

        public string DescricaoPais { get; set; }

        public TipoInstituicaoEnsino? TipoInstituicaoEnsino { get; set; }

        public string DescricaoCategoria { get; set; }

        public bool Ativo { get; set; }
    }
}