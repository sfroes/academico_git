using SMC.Framework;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System.Collections.Generic;

namespace SMC.SGA.Aluno.Areas.SRC.Models
{
    public class NovaSolicitacaoViewModel : SMCViewModelBase
    {
        #region DataSources

        [SMCDataSource]
        public List<SMCDatasourceItem> TiposServico { get; set; }

        [SMCDataSource]
        public List<SMCDatasourceItem> Processos { get; set; }

        #endregion DataSources

        [SMCSize(SMCSize.Grid24_24)]
        [SMCSelect(nameof(TiposServico))]
        [SMCRequired]
        public long? SeqTipoServico { get; set; }

        [SMCSize(SMCSize.Grid24_24)]
        [SMCSelect(nameof(Processos), NameDescriptionField = "DescricaoProcesso")]
        [SMCDependency(nameof(SeqTipoServico), "BuscarProcessos", "SolicitacaoServico", true)]
        [SMCRequired]
        public long? SeqProcesso { get; set; }
    }
}