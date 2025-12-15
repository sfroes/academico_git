using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Framework.Mapper;
using System;

namespace SMC.Academico.ServiceContract.Areas.APR.Data.EntregaOnline
{
    public class HistoricoSituacaoEntregaOnlineItemData : ISMCMappable
    {
        public long Seq { get; set; }
        public SituacaoEntregaOnline SituacaoEntregaOnline { get; set; }
        public DateTime Data { get; set; }
        public string NomeResponsavel { get; set; }
        public string Observacao { get; set; }
    }
}