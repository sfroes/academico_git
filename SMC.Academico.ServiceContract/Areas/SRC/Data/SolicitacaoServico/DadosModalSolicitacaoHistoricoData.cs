using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.SRC.Data
{
    public class DadosModalSolicitacaoHistoricoData : ISMCMappable
    {
        public List<DadosModalSolicitacaoHistoricoItemData> Historicos { get; set; }
    }
}