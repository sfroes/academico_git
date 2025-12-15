using SMC.Academico.Common.Areas.SRC.Constants;
using SMC.Academico.UI.Mvc.Areas.SRC.Models;
using SMC.Framework;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.SRC.Models
{
    public class AtendimentoDispensaAgrupamentoViewModel : SolicitacaoServicoPaginaViewModelBase
    {
        [SMCDataSource]
        public List<SMCDatasourceItem> ItensOrigensInternas { get; set; }

        [SMCDataSource]
        public List<SMCDatasourceItem> ItensOrigensExternas { get; set; }

        [SMCDataSource]
        public List<SMCDatasourceItem> ItensDestinos { get; set; }

        [SMCHidden]
        public override string Token => TOKEN_SOLICITACAO_SERVICO.SOLICITACAO_DISPENSA_AGRUPAMENTO_ITENS;

        public List<SolicitacaoDispensaGrupoViewModel> Grupos { get; set; }
    }
}