using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Academico.ServiceContract.Areas.PES.Data;
using SMC.Academico.UI.Mvc.Areas.ALN.Lookups;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMC.SGA.Administrativo.Areas.CSO.Models
{
    public class InstituicaoNivelModalidadeLocalidadeMasterDetailViewModel : SMCViewModelBase
    {

        #region DataSource

        [SMCDataSource()]
        [SMCIgnoreJson]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(IInstituicaoTipoEntidadeService), nameof(IInstituicaoTipoEntidadeService.BuscarTipoEntidadesSuperioresCursoUnidadeDaInstituicaoSelect))]
        public List<SMCDatasourceItem> TiposEntidadeLocalidades { get; set; }

        #endregion

        [SMCHidden]
        public long Seq { get; set; }

        [SMCHidden(SMCViewMode.List | SMCViewMode.Filter)]
        [SMCOrder(3)]
        [SMCRequired]
        [SMCSelect(nameof(TiposEntidadeLocalidades))]
        [SMCSize(SMCSize.Grid22_24)]
        public long SeqTipoEntidadeLocalidade { get; set; }

        [SMCHidden]
        public long SeqInstituicaoNivelModalidade { get; set; }

        [SMCDescription]
        [SMCHidden(SMCViewMode.Insert | SMCViewMode.Edit | SMCViewMode.Filter)]
        [SMCInclude("TipoEntidadeLocalidade")]
        [SMCMapProperty("TipoEntidadeLocalidade.Descricao")]
        [SMCOrder(3)]
        public string DescricaoTipoEntidadeLocalidade { get; set; }
    }
}