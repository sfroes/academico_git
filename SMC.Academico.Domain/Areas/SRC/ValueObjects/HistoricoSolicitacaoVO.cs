using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.SRC.ValueObjects
{
    public class HistoricoSolicitacaoVO : ISMCMappable
    {
        public List<HistoricoSolicitacaoEtapaVO> Etapas { get; set; }

        public List<HistoricoSolicitacaoEtapaItemVO> Historicos { get; set; }
    }
}