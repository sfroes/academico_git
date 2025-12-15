using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.SRC.Data
{
    public class ProcessoEtapaDetalheData : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqProcesso { get; set; }

        public string DescricaoEtapa { get; set; }

        public long SeqEtapaSgf { get; set; }

        public DateTime? DataInicio { get; set; }

        public DateTime? DataFim { get; set; }

        public SituacaoEtapa SituacaoEtapa { get; set; }

        public bool ExibirAcessoEtapa { get; set; }

        public string Token { get; set; }

        public List<EscalonamentoData> Escalonamentos { get; set; }

        public List<ProcessoEtapaConfiguracaoNotificacaoData> ConfiguracoesNotificacao { get; set; }

        public short Ordem { get; set; }

        public DateTime? DataEncerramento { get; set; }

        public TipoPrazoEtapa? TipoPrazoEtapa { get; set; }

        public bool HabilitaBtnComPermissaoManutencaoProcesso { get; set; }

        public bool HabilitaEncerrarEtapa { get; set; }

        public string InstructionEncerrarEtapa { get; set; }

        public bool HabilitaExcluirEtapa { get; set; }

        public string InstructionExcluirEtapa { get; set; }
    }
}