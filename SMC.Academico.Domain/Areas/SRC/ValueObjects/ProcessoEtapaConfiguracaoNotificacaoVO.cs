using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.Academico.Domain.Areas.SRC.Models;
using SMC.Framework.Mapper;
using SMC.Notificacoes.ServiceContract.Areas.NTF.Data;
//using SMC.Notificacoes.UI.Mvc.Models;
using System;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.SRC.ValueObjects
{
    public class ProcessoEtapaConfiguracaoNotificacaoVO : ISMCMappable
    {

        public long Seq { get; set; }
  
        public long SeqProcessoEtapa { get; set; }

        public long SeqProcessoUnidadeResponsavel { get; set; }

        public long SeqTipoNotificacao { get; set; }

        public long SeqConfiguracaoTipoNotificacao { get; set; }

        public long SeqProcesso { get; set; }

        public long SeqServico { get; set; }

        public long SeqProcessoEtapaSgf { get; set; }

        public bool EnvioAutomatico { get; set; }

        public bool ProcessoEncerrado { get; set; }

        public SituacaoEtapa SituacaoEtapa { get; set; }

        public bool CamposReadyOnly { get; set; }

        public string DescricaoTipoNotificacao { get; set; }

        public bool TipoNotificacaoPermiteAgendamento { get; set; }

        public ConfiguracaoNotificacaoEmailData ConfiguracaoNotificacao { get; set; }

        public List<ParametroEnvioNotificacao> ParametrosEnvioNotificacao { get; set; }

        public TipoNotificacaoVO TipoNotificacao { get; set; }

        public ProcessoEtapaDetalheVO ProcessoEtapa { get; set; }

        public ProcessoUnidadeResponsavel ProcessoUnidadeResponsavel { get; set; }

        public bool PossuiRegistroEnvioNotificacao { get; set; }

        public bool HabilitaBtnComPermissaoManutencaoProcesso { get; set; }

        public bool NotificacaoObrigatoriaNaEtapa { get; set; }
    }
}