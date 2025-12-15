using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Framework;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.SGA.Administrativo.Areas.ORG.Models;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.CSO.Models
{
    public class InstituicaoTipoEntidadeFormacaoEspecificaFiltroDynamicModel : SMCDynamicFilterViewModel
    {
        [SMCRequired]
        [SMCFilter(true, false)]
        [SMCParameter(IsFilter = true)]
        [SMCOrder(0)]
        [SMCSelect("InstituicoesTipoEntidade", NameDescriptionField = "DescricaoInstituicaoTipoEntidade")]
        [SMCSize(SMCSize.Grid8_24)]
        public long SeqInstituicaoTipoEntidade { get; set; }

        [SMCDataSource()]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(IInstituicaoTipoEntidadeService), nameof(IInstituicaoTipoEntidadeService.BuscarInstituicaoTiposEntidadeSelect))]
        public List<SMCDatasourceItem> InstituicoesTipoEntidade { get; set; }

        [SMCHidden]
        [SMCFilter(true, false)]
        [SMCParameter(IsFilter = true)]
        public string DescricaoInstituicaoTipoEntidade { get; set; }
    }
}