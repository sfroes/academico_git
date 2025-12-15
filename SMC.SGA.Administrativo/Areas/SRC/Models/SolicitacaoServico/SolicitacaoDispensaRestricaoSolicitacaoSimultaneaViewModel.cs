using SMC.Framework.Mapper;

namespace SMC.SGA.Administrativo.Areas.SRC.Models
{
    public class SolicitacaoDispensaRestricaoSolicitacaoSimultaneaViewModel : ISMCMappable
    {
        public long SeqSolicitacaoServico { get; set; }

        public long SeqServico { get; set; }
        public long SeqTemplateProcessoSGF { get; set; }

        public long SeqSituacaoEtapaAtualSGF { get; set; }

        public long SeqEtapaAtualSGF { get; set; }
        public string NumeroProtocolo { get; set; }
        public string Processo { get; set; }

        public string Servico { get; set; }
        public short Ordem { get; set; }
        public string SituacaoAtual { get; set; }

        public string DescricaoCompleta
        {
            get
            {
                return $"{NumeroProtocolo} - {Processo} - {Ordem}º Etapa - {SituacaoAtual}";
            }
        }
    }
}