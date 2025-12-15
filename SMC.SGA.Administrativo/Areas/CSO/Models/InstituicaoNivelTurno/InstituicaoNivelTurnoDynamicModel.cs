using SMC.Academico.Common.Areas.CSO.Constants;
using SMC.Academico.ServiceContract.Areas.CSO.Interfaces;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Html;
using SMC.SGA.Administrativo.Areas.CSO.Views.InstituicaoNivelTurno.App_LocalResources;
using System;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.CSO.Models
{
    [SMCGroupedPropertyConfiguration(GroupId = "LimiteHorario", Size = SMCSize.Grid24_24)]
    public class InstituicaoNivelTurnoDynamicModel : SMCDynamicViewModel
    {
        [SMCHidden]
        [SMCKey]
        [SMCOrder(0)]
        [SMCRequired]
        public override long Seq { get; set; }

        [SMCFilter(true, true)]
        [SMCHidden(SMCViewMode.List)]
        [SMCRequired]
        [SMCSelect("InstituicaoNiveis")]
        [SMCSize(SMCSize.Grid8_24)]
        public long SeqInstituicaoNivel { get; set; }

        [SMCDataSource()]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(IInstituicaoNivelService), "BuscarNiveisEnsinoDaInstituicaoSelect")]
        public List<SMCDatasourceItem> InstituicaoNiveis { get; set; }

        [SMCHidden(SMCViewMode.Insert | SMCViewMode.Edit | SMCViewMode.Filter)]
        [SMCInclude("InstituicaoNivel.NivelEnsino")]
        [SMCMapProperty("InstituicaoNivel.NivelEnsino.Descricao")]
        [SMCSortable(true, true, "InstituicaoNivel.NivelEnsino.Descricao")]
        public string DescricaoInstituicaoNivel { get; set; }

        [SMCFilter(true, true)]
        [SMCHidden(SMCViewMode.List)]
        [SMCRequired]
        [SMCSelect("Turnos", "Seq", "Descricao", SortBy = SMCSortBy.Description)]
        [SMCSize(SMCSize.Grid7_24)]
        public long SeqTurno { get; set; }

        [SMCDataSource("Turno")]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(ICSODynamicService))]
        public List<SMCDatasourceItem> Turnos { get; set; }

        [SMCHidden(SMCViewMode.Insert | SMCViewMode.Edit | SMCViewMode.Filter)]
        [SMCInclude("Turno")]
        [SMCMapProperty("Turno.Descricao")]
        [SMCSortable(true, true, "Turno.Descricao")]
        public string DescricaoTurno { get; set; }

        [SMCIgnoreProp(SMCViewMode.List | SMCViewMode.Filter)]
        [SMCRequired]
        [SMCGroupedProperty("LimiteHorario")]
        [SMCSize(SMCSize.Grid12_24)]
        public TimeSpan HoraLimiteInicio { get; set; }

        [SMCIgnoreProp(SMCViewMode.List | SMCViewMode.Filter)]
        [SMCRequired]
        [SMCGroupedProperty("LimiteHorario")]
        [SMCSize(SMCSize.Grid12_24)]
        public TimeSpan HoraLimiteFim { get; set; }

        public override void ConfigureDynamic(ref SMCDynamicOptions options)
        {
            options.EditInModal()
                   .Service<IInstituicaoNivelTurnoService>(save: nameof(IInstituicaoNivelTurnoService.Salvar))
                   .Messages(x => string.Format(UIResource.Listar_Excluir_Confirmacao,
                                ((InstituicaoNivelTurnoDynamicModel)x).DescricaoTurno,
                                ((InstituicaoNivelTurnoDynamicModel)x).DescricaoInstituicaoNivel))
                   //.Detail<InstituicaoNivelTurnoDynamicModel>("_DetailList", t => t.DescricaoInstituicaoNivel, "_DetailListHeader")
                   .RegisterControls(RegisterHelperControls.Fields, RegisterHelperControls.DataSelector)
                   .Tokens(tokenList: UC_CSO_003_06_01.MANTER_TURNO_INSTITUICAO_NIVEL,
                            tokenInsert: UC_CSO_003_06_01.MANTER_TURNO_INSTITUICAO_NIVEL,
                            tokenEdit: UC_CSO_003_06_01.MANTER_TURNO_INSTITUICAO_NIVEL,
                            tokenRemove: UC_CSO_003_06_01.MANTER_TURNO_INSTITUICAO_NIVEL);
        }
    }
}