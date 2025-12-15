using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.SRC.Data
{
    public class DadosRelatorioSolicitacoesBloqueioData : ISMCMappable
    {
        public long Seq { get; set; }

        public string Processo { get; set; }

        [SMCMapForceFromTo]
        public List<DadosRelatorioSolicitacoesBloqueioItemData> Bloqueios { get; set; }
    }
}
