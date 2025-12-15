using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System;

namespace SMC.Academico.ServiceContract.Areas.ORG.Data
{
    public class EntidadeConfiguracaoNotificacaoListarData : ISMCMappable
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
