using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.ALN.Data
{
    public class IngressanteProcessoSeletivoListaData : ISMCMappable
    {
        public long SeqSolicitacaoServico { get; set; }

        public long SeqIngressante { get; set; }

        public string NomeInstituicaoEnsino { get; set; }

        public string DescricaoProcesso { get; set; }

        public string DescricaoVinculo { get; set; }

        public string DescricaoFormaIngresso { get; set; }

        public string DescricaoOferta { get; set; }
    }
}