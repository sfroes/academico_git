using SMC.Academico.Common.Areas.SRC.Constants;
using SMC.Academico.UI.Mvc.Areas.SRC.Lookups;
using SMC.Academico.UI.Mvc.Areas.SRC.Models;
using SMC.Framework;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.SRC.Models
{
    public class AtendimentoReaberturaViewModel : SolicitacaoServicoPaginaViewModelBase
    {
        [SMCDataSource]
        public List<SMCDatasourceItem> Processos { get; set; }

        [SMCHidden]
        public override string Token => TOKEN_SOLICITACAO_SERVICO.ATENDIMENTO_PADRAO_ATENDIMENTO_REABERTURA;

        [SMCHidden]
        public bool? GrupoEscalonamentoAtivo { get; set; } = true;

        [SMCSelect(nameof(Processos))]
        [SMCSize(Framework.SMCSize.Grid12_24)]
        public long? SeqProcessoEscalonamentoReabertura { get; set; }

        [SMCHidden]
        public bool SeqProcessoSomenteLeitura { get; set; } = true;

        [GrupoEscalonamentoLookup]
        [SMCConditionalReadonly(nameof(SeqProcessoEscalonamentoReabertura), SMCConditionalOperation.Equals, "")]
        [SMCDependency(nameof(GrupoEscalonamentoAtivo))]
        [SMCDependency(nameof(SeqProcessoEscalonamentoReabertura))]
        [SMCDependency(nameof(SeqProcessoSomenteLeitura))]
        [SMCSize(Framework.SMCSize.Grid12_24)]
        public GrupoEscalonamentoLookupViewModel SeqGrupoEscalonamentoMatricula { get; set; }
    }
}