using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.SRC.ValueObjects
{
    public class AtendimentoDispensaAgrupamentoItensVO : ISMCMappable
    {
        public List<SMCDatasourceItem> ItensOrigensInternas { get; set; }

        public List<SMCDatasourceItem> ItensOrigensExternas { get; set; }

        public List<SMCDatasourceItem> ItensDestinos { get; set; }
    }
}