using SMC.Academico.Common.Areas.SRC.Constants;
using SMC.Academico.UI.Mvc.Areas.CUR.Lookups;
using SMC.Framework;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Html;
using System.Collections.Generic;

namespace SMC.Academico.UI.Mvc.Areas.SRC.Models
{
    public class SolicitacaoDispensaItensCursadosViewModel : SolicitacaoServicoPaginaViewModelBase
    {
        #region Data Source

        [SMCDataSource(SMCStorageType.Session)]
        public List<SMCDatasourceItem> CiclosLetivos { get; set; }

        [SMCDataSource(SMCStorageType.Session)]
        public List<SMCDatasourceItem> Titulacoes { get; set; }

        #endregion Data Source

        [SMCHidden]
        public override string Token => TOKEN_SOLICITACAO_SERVICO.SOLICITACAO_DISPENSA_ITENS_CURSADOS;

        [SMCDetail(type: SMCDetailType.Modal, windowSize: SMCModalWindowSize.Large, DisplayAsGrid = true)]
        [SMCSize(SMCSize.Grid24_24)]
        public SMCMasterDetailList<SolicitacaoDispensaItensCursadosOutrasInstituicoesViewModel> ItensCursadosOutrasInstituicoes { get; set; }

        [DispensaComponenteCurricularLookup]
        [SMCDependency(nameof(SeqPessoaAtuacao))]
        [SMCSize(SMCSize.Grid24_24)]
        public List<DispensaComponenteCurricularLookupViewModel> ItensCursadosNestaInstituicao { get; set; }

        public decimal DispensaTotalCargaHorariaHoras { get; set; }

        public decimal DispensaTotalCargaHorariaHorasAula { get; set; }

        public decimal DispensaTotalCreditos { get; set; }

        public decimal CursadosTotalCargaHorariaHoras { get; set; }

        public decimal CursadosTotalCargaHorariaHorasAula { get; set; }

        public decimal CursadosTotalCreditos { get; set; }        
    }
}