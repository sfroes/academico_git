using SMC.Formularios.Common.Areas.TMP.Enums;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System;
using System.Collections.Generic;

namespace SMC.SGA.Aluno.Areas.SRC.Models
{
    public class SolicitacaoServicoFiltroViewModel : SMCPagerViewModel
    {
        #region DataSources

        [SMCDataSource]
        public List<SMCDatasourceItem> TiposServico { get; set; }

        [SMCDataSource]
        public List<SMCDatasourceItem> Servicos { get; set; }

        #endregion DataSources

        [SMCSize(SMCSize.Grid3_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid3_24)]
        [SMCFilter(true, false)]
        [SMCMaxLength(20)]
        public string NumeroProtocolo { get; set; }

        [SMCSize(SMCSize.Grid5_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid5_24)]
        [SMCFilter(true, false)]
        [SMCSelect(nameof(TiposServico))]
        public long? SeqTipoServico { get; set; }

        [SMCSize(SMCSize.Grid10_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid10_24)]
        [SMCFilter(true, false)]
        [SMCSelect(nameof(Servicos))]
        [SMCDependency(nameof(SeqTipoServico), "BuscarServicos", "SolicitacaoServico", true)]
        public long? SeqServico { get; set; }

        [SMCSize(SMCSize.Grid3_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid3_24)]
        [SMCFilter(true, false)]
        [SMCSelect]
        public CategoriaSituacao? CategoriaSituacao { get; set; }

        [SMCDateTimeMode(SMCDateTimeMode.Date)]
        [SMCFilter(true, false)]
        [SMCSize(SMCSize.Grid3_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid3_24)]
        [SMCSortable(true, true, SMCSortDirection.Descending)]
        public DateTime? DataSolicitacao { get; set; }
    }
}