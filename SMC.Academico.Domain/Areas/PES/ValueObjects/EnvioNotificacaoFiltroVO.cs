using System;
using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Framework.Mapper;
using SMC.Framework.Model;

namespace SMC.Academico.Domain.Areas.PES.ValueObjects
{
    public class EnvioNotificacaoFiltroVO : SMCPagerFilterData, ISMCMappable
    {
        public long? Seq { get; set; }
        public string Assunto { get; set; }
        public string Remetente { get; set; }
        public TipoAtuacao? TipoAtuacao { get; set; }
        public string UsuarioEnvio { get; set; }
        public DateTime? DataEnvio { get; set; }
        public long? SeqPessoaAtuacao { get; set; }
    }
}