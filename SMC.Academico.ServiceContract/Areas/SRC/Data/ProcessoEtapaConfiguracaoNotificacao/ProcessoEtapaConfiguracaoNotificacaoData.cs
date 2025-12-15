using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.Framework.Mapper;
using SMC.Notificacoes.ServiceContract.Areas.NTF.Data;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.SRC.Data
{
    public class ProcessoEtapaConfiguracaoNotificacaoData : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqProcessoEtapa { get; set; }

        public long SeqProcessoUnidadeResponsavel { get; set; }

        public long SeqTipoNotificacao { get; set; }

        public long SeqConfiguracaoTipoNotificacao { get; set; }

        public long SeqProcesso { get; set; }

        public long SeqServico { get; set; }

        public bool ProcessoEncerrado { get; set; }

        public SituacaoEtapa SituacaoEtapa { get; set; }

        public bool CamposReadyOnly { get; set; }

        public bool EnvioAutomatico { get; set; }

        public string DescricaoTipoNotificacao { get; set; }

        public bool TipoNotificacaoPermiteAgendamento { get; set; }

        public ConfiguracaoNotificacaoEmailData ConfiguracaoNotificacao { get; set; }

        public virtual List<ParametroEnvioNotificacaoItemData> ParametrosEnvioNotificacao { get; set; }

        public virtual TipoNotificacaoData TipoNotificacao { get; set; }

        public virtual ProcessoUnidadeResponsavelData ProcessoUnidadeResponsavel { get; set; }

        public bool PossuiRegistroEnvioNotificacao { get; set; }

        public bool HabilitaBtnComPermissaoManutencaoProcesso { get; set; }

        public bool NotificacaoObrigatoriaNaEtapa { get; set; }
    }
}