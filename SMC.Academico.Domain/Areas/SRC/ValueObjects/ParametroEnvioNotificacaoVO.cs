using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.SRC.ValueObjects
{
    public class ParametroEnvioNotificacaoVO : ISMCMappable
    {
        public long SeqProcesso { get; set; }

        public long SeqServico { get; set; }

        public long SeqProcessoEtapaConfiguracaoNotificacao { get; set; }

        public bool ProcessoEncerrado { get; set; }

        public bool BotoesDesabilitados { get; set; }

        public long SeqProcessoEtapa { get; set; }

        public long SeqTipoNotificacao { get; set; }

        public string TipoNotificacao { get; set; }

        public string EntidadeResponsavel { get; set; }

        public bool ExigeEscalonamento { get; set; }

        public long SeqEtapaSgf { get; set; }

        public List<ParametroEnvioNotificacaoItemVO> ParametroEnvioNotificacaoItem { get; set; }
    }
}