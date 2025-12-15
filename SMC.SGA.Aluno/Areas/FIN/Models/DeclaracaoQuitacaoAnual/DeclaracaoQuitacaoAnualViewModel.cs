using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System.Collections.Generic;

namespace SMC.SGA.Aluno.Areas.FIN.Models
{
    public class DeclaracaoQuitacaoAnualViewModel : SMCViewModelBase, ISMCMappable
    {
        [SMCDataSource]
        public List<SMCDatasourceItem<string>> ListaCpfPagante { get; set; }

        [SMCHidden]
        public int AnoReferencia { get; set; }

        [SMCRequired]
        [SMCSelect(nameof(ListaCpfPagante), AutoSelectSingleItem = true)]
        [SMCSize(SMCSize.Grid6_24)]
        public string Cpf { get; set; }
    }
}