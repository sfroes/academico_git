using SMC.Framework.Mapper;
using System;

namespace SMC.Academico.ReportHost.Areas.SRC.Models
{
    public class DadosRelatorioSolicitacoesBloqueioVO : ISMCMappable
    {
        public long Seq { get; set; }

        public long? SeqSolicitacaoServicoEtapa { get; set; }

        public DateTime DataInclusao { get; set; }

        public string NumeroProtocolo { get; set; }

        public string Solicitante { get; set; }

        public string Processo { get; set; }

        public string DataInclusaoFormatada { get { return this.DataInclusao.ToString("dd/MM/yy"); } }

        public string EtapaAtual { get; set; }

        public string SituacaoAtual { get; set; }
    }
}