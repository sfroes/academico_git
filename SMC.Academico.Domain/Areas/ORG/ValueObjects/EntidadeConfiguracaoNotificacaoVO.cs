using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Notificacoes.ServiceContract.Areas.NTF.Data;
using System;

namespace SMC.Academico.Domain.Areas.ORG.ValueObjects
{
    public class EntidadeConfiguracaoNotificacaoVO : SMCPagerFilterData, ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqConfiguracaoTipoNotificacao { get; set; }

        public long SeqEntidade { get; set; }

        public string DescricaoEntidade { get; set; }

        public long SeqTipoNotificacao { get; set; }

        public string DescricaoTipoNotificacao { get; set; }

        public DateTime DataInicioValidade { get; set; }

        public DateTime? DataFimValidade { get; set; }

        public string TokenTipoNotificacao { get; set; }

        public long SeqTipoNotificacaoComparacao { get; set; }

        public ConfiguracaoNotificacaoEmailData ConfiguracaoNotificacao { get; set; }

        public string Observacao { get; set; }

        public bool ExisteRegistroEnvioNotificacaoConfiguracao { get; set; }

        public long? SeqLayoutMensagemEmail { get; set; }

    }
}