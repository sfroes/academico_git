using SMC.Academico.ServiceContract.Areas.ORG.Data;
using SMC.Framework.Mapper;
using System;

namespace SMC.Academico.ReportHost.Areas.MAT.Models
{
    public class DeclaracaoPagamentoFiltroVO : ISMCMappable
    {
        public DateTime DataFim { get; set; }
        public DateTime DataInicio { get; set; }
        public long SeqAlunoLogado { get; set; }
    }
}