using SMC.Academico.Common.Areas.FIN.Enums;
using SMC.Framework;
using SMC.Framework.Mapper;
using System;

namespace SMC.Academico.Domain.Areas.FIN.ValueObjects
{
    public class BeneficioEnvioNotificacaoVO : ISMCMappable
    {
        public long Seq { get; set; }

        public string DescricaoTipoNotificacao { get; set; }

        public string Assunto { get; set; }

        public bool? SucessoEnvio { get; set; }

        public DateTime? DataProcessamento { get; set; }
    }
}
