using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System;

namespace SMC.Academico.ServiceContract.Areas.PES.Data
{
    public class EnvioNotificacaoDestinatarioFiltroData : SMCPagerFilterData, ISMCMappable
    {
        public long? SeqPessoaAtuacao { get; set; }

        public string Assunto { get; set; }

        public string Remetente { get; set; }

        public DateTime? DataEnvio { get; set; }

        public string UsuarioEnvio { get; set; }
    }
}