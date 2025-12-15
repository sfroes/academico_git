using SMC.Framework.Mapper;
using System;

namespace SMC.Academico.Domain.Areas.ORG.ValueObjects
{
    public class EntidadeConfiguracaoNotificacaoListarVO : ISMCMappable
    {
        public long Seq { get; set; }

        public long? SeqEntidade { get; set; }

        public string DescricaoEntidade { get; set; }

        public long? SeqTipoNotificacao { get; set; }

        public string DescricaoTipoNotificacao { get; set; }

        public DateTime? DataInicioValidade { get; set; }

        public DateTime? DataFimValidade { get; set; }
    }
}
