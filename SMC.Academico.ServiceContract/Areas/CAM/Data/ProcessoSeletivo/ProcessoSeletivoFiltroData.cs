using SMC.Framework.Mapper;
using SMC.Framework.Model;

namespace SMC.Academico.ServiceContract.Areas.CAM.Data
{
    public class ProcessoSeletivoFiltroData : SMCPagerFilterData, ISMCMappable
    {
        public long? Seq { get; set; }

        public long SeqCampanha { get; set; }

        public string Descricao { get; set; }

        public long? SeqNivelEnsino { get; set; }

        public long? SeqTipoProcessoSeletivo { get; set; }
    }
}