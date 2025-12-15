using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.SRC.Data
{
    public class SolicitacaoDispensaRestricaoSolicitacaoSimultaneaData : ISMCMappable
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
    }
}