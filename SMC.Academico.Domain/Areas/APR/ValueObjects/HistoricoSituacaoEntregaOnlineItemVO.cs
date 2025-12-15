using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Framework.Mapper;
using System;

namespace SMC.Academico.Domain.Areas.APR.ValueObjects
{
    public class HistoricoSituacaoEntregaOnlineItemVO : ISMCMappable
    {
        public long Seq { get; set; }
        public SituacaoEntregaOnline SituacaoEntregaOnline { get; set; }
        public DateTime Data { get; set; }
        public string NomeResponsavel { get; set; }
        public string Observacao { get; set; }
    }
}