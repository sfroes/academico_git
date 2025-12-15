using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.ORG.Data
{
    public class TipoEntidadeFiltroData : ISMCMappable
    {
        public bool? EntidadeExternada { get; set; }

        public string Token { get; set; }

        public bool? PermiteAtoNormativo { get; set; }

        public long? SeqInstituicaoEnsino { get; set; }
    }
}