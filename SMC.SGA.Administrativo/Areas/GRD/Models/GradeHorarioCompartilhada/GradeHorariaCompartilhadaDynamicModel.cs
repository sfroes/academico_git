using SMC.Academico.Common.Areas.GRD.Constants;
using SMC.Academico.ServiceContract.Areas.GRD.Interfaces;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Academico.UI.Mvc.Areas.CAM.Lookups;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Html;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.GRD.Models
{
    public class GradeHorariaCompartilhadaDynamicModel : SMCDynamicViewModel
    {
        #region DataSource

        [SMCIgnoreProp]
        [SMCServiceReference(typeof(IEntidadeService), nameof(IEntidadeService.BuscarUnidadesResponsaveisGPILocalSelect))]
        [SMCDataSource]
        public List<SMCDatasourceItem> EntidadesResponsaveis { get; set; }

        #endregion DataSource

        [SMCKey]
        [SMCHidden]
        public override long Seq { get; set; }

        [CicloLetivoLookup]
        [SMCReadOnly(SMCViewMode.Edit)]
        [SMCRequired]
        [SMCSize(SMCSize.Grid6_24)]
        public CicloLetivoLookupViewModel SeqCicloLetivo { get; set; }

        [SMCReadOnly(SMCViewMode.Edit)]
        [SMCRequired]
        [SMCSelect(nameof(EntidadesResponsaveis))]
        [SMCSize(SMCSize.Grid18_24)]
        [SMCMapProperty("SeqEntidadeResponsavel")]
        public long SeqsEntidadesResponsaveis { get; set; }

        [SMCRequired]
        [SMCSize(SMCSize.Grid15_24)]
        public string Descricao { get; set; }

        [SMCDetail(min: 2)]
        [SMCSize(SMCSize.Grid24_24)]
        public SMCMasterDetailList<GradeHorariaCompartilhadaItemViewModel> Itens { get; set; }

        public override void ConfigureDynamic(ref SMCDynamicOptions options)
        {
            options
                .EditInModal()
                .Detail<GradeHorariaCompartilhadaListarDynamicModel>("_DetailList")
                .ModalSize(SMCModalWindowSize.Large)
                .Tokens(tokenInsert: UC_GRD_001_04_02.MANTER_GRADE_COMPARTILHADA,
                        tokenEdit: UC_GRD_001_04_02.MANTER_GRADE_COMPARTILHADA,
                        tokenRemove: UC_GRD_001_04_02.MANTER_GRADE_COMPARTILHADA,
                        tokenList: UC_GRD_001_04_01.PESQUISAR_GRADE_COMPARTILHADA)
                .Service<IGradeHorariaCompartilhadService>(
                    index: nameof(IGradeHorariaCompartilhadService.BuscarGradesHorariasCompartilhada),
                    save: nameof(IGradeHorariaCompartilhadService.SalvarGradeHorariaCompartilhada),
                    edit: nameof(IGradeHorariaCompartilhadService.BuscarGradeHorariaCompartilhada));
        }
    }
}