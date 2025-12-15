using SMC.Academico.Common.Areas.ORG.Enums;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Academico.ServiceContract.Interfaces;
using SMC.Framework;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Html;
using SMC.SGA.Administrativo.Areas.ORG.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SMC.SGA.Administrativo.Areas.ORG.Views.InstituicaoNivelCalendario.App_LocalResources;
using System.Drawing;
using SMC.Academico.Common.Constants;

namespace SMC.SGA.Administrativo.Areas.ORG.Models
{
    public class InstituicaoNivelCalendarioDynamicModel : SMCDynamicViewModel
    {
        #region DataSources


        [SMCDataSource]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(IInstituicaoNivelService), nameof(IInstituicaoNivelService.BuscarNiveisEnsinoDaInstituicaoSelect))]
        public List<SMCDatasourceItem> InstituicaoNiveisEnsino { get; set; }

        [SMCDataSource]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(ITipoEventoService), nameof(ITipoEventoService.BuscarTiposEventosCalendarioAGD), values: new[] { nameof(SeqCalendarioAgd) })]
        public List<SMCDatasourceItem> TiposEventos { get; set; }

        [SMCIgnoreProp]
        [SMCDataSource]
        [SMCServiceReference(typeof(ICalendarioService), nameof(ICalendarioService.BuscarCalendariosUnidadeResponsavelSelect), values: new[] { nameof(SeqUnidadeResponsavelAgd) })]
        public List<SMCDatasourceItem> Calendarios { get; set; }

        #endregion

        #region Propriedades Auxiliares

        [SMCRequired]
        [SMCHidden]
        [SMCDependency(nameof(SeqInstituicaoLogada), nameof(InstituicaoNivelCalendarioController.BuscarSeqUnidadeResponsavelAgd), "InstituicaoNivelCalendario", false)]
        public long? SeqUnidadeResponsavelAgd { get; set; }

        [SMCHidden]
        [SMCParameter(FILTER.INSTITUICAO_ENSINO, true)]
        public long SeqInstituicaoLogada { get; set; }

        #endregion


        [SMCHidden]
        [SMCKey]
        public override long Seq { get; set; }

        [SMCRequired]
        [SMCSelect(nameof(InstituicaoNiveisEnsino))]
        [SMCSize(SMCSize.Grid8_24, SMCSize.Grid24_24, SMCSize.Grid8_24, SMCSize.Grid8_24)]
        public long SeqInstituicaoNivel { get; set; }


        [SMCSelect(nameof(Calendarios))]
        [SMCRequired]
        [SMCSize(SMCSize.Grid8_24, SMCSize.Grid24_24, SMCSize.Grid8_24, SMCSize.Grid8_24)]
        [SMCDependency(nameof(SeqUnidadeResponsavelAgd), nameof(InstituicaoNivelCalendarioController.BuscarCalendariosAgd), "InstituicaoNivelCalendario", false)]
        public long SeqCalendarioAgd { get; set; }

        [SMCRequired]
        [SMCSelect]
        [SMCSize(SMCSize.Grid8_24, SMCSize.Grid24_24, SMCSize.Grid8_24, SMCSize.Grid8_24)]
        public UsoCalendario UsoCalendario { get; set; }

        [SMCDetail(SMCDetailType.Tabular, min:1)]
        [SMCSize(SMCSize.Grid24_24)]
        public SMCMasterDetailList<InstituicaoNivelTipoEventoViewModel> TiposEvento { get; set; }

        public override void ConfigureDynamic(ref SMCDynamicOptions options)
        {
            options.Messages(x => string.Format(UIResource.Listar_Excluir_Confirmacao,
                            ((InstituicaoNivelCalendarioListarDynamicModel)x).UsoCalendario.SMCGetDescription(),
                            ((InstituicaoNivelCalendarioListarDynamicModel)x).DscNivelEnsino));

            options.Service<IInstituicaoNivelCalendarioService>(edit: nameof(IInstituicaoNivelCalendarioService.BuscarInstituicaoNivelCalendario),
                                                                index: nameof(IInstituicaoNivelCalendarioService.BuscarListaInstituicaoNivelCalendario));
        }
    }
}