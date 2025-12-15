using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.SRC.ValueObjects
{
    public class DadosModalSolicitacaoHistoricoVO : ISMCMappable
    {
        public List<DadosModalSolicitacaoHistoricoItemVO> Historicos { get; set; }
    }
}