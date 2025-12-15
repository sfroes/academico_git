using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.ORT.Data
{
    public class InstituicaoNivelTipoTrabalhoFiltroData : ISMCMappable
    {
        public long? SeqInstituicaoNivel { get; set; }

        public long? SeqInstituicaoEnsino { get; set; }

        public long? SeqNivelEnsino { get; set; }

        public bool? PermiteInclusaoManual { get; set; }
    }
}