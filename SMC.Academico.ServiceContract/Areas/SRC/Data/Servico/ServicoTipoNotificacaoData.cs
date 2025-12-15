using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.SRC.Data
{
    public class ServicoTipoNotificacaoData : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqServico { get; set; }

        public long SeqTipoNotificacao { get; set; }

        public long SeqEtapaSgf { get; set; }

        public bool Obrigatorio { get; set; }
    }
}