using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.Framework.Mapper;
using System;

namespace SMC.Academico.Domain.Areas.SRC.ValueObjects
{
    public class GrupoEscalonamentoListaItemVO : ISMCMappable
    {
        public long Seq { get; set; }
        public long SeqProcessoEtapa { get; set; }

        public long SeqServico { get; set; }

        public string TokenServico { get; set; }

        public long SeqProcesso { get; set; }

        public long SeqGrupoEscalonamento { get; set; }

        public string DescricaoEtapa { get; set; }

        public SituacaoEtapa SituacaoEtapa { get; set; }

        public long SeqEscalonamento { get; set; }

        public DateTime? DataInicio { get; set; }

        public DateTime? DataFim { get; set; }

        public DateTime? DataEncerramento { get; set; }

        public int QuantidadeParcelas { get; set; }

        public bool SolicitacaoFinalizadaComSucesso { get; set; }

        public bool HabilitaBtnComPermissaoManutencaoProcesso { get; set; }
    }
}