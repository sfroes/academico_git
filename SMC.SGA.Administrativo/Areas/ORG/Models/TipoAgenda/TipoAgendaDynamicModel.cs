using SMC.Academico.Common.Areas.ORG.Constants;
using SMC.Academico.Common.Constants;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Framework;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;

namespace SMC.SGA.Administrativo.Areas.ORG.Models
{
    public class TipoAgendaDynamicModel : SMCDynamicViewModel
    {
        [SMCFilter(true, true)]
        [SMCKey]
        [SMCReadOnly(SMCViewMode.Edit | SMCViewMode.Insert)]
        [SMCRequired]
        [SMCSize(SMCSize.Grid4_24)]
        [SMCSortable(true)]
        public override long Seq { get; set; }

        [SMCHidden]
        [SMCParameter(FILTER.INSTITUICAO_ENSINO, true)]
        [SMCRequired]
        public long SeqInstituicaoEnsino { get; set; }

        [SMCDescription]
        [SMCFilter(true, true)]
        [SMCMaxLength(100)]
        [SMCRequired]
        [SMCSize(SMCSize.Grid20_24)]
        [SMCSortable(true, true)]
        public string Descricao { get; set; }

        [SMCHidden(SMCViewMode.Filter)]
        [SMCRadioButtonList]
        [SMCRequired]
        [SMCSize(SMCSize.Grid5_24)]
        [SMCSortable(true)]
        public bool? EventoLetivo { get; set; }

        [SMCHidden(SMCViewMode.Filter)]
        [SMCRadioButtonList]
        [SMCRequired]
        [SMCSize(SMCSize.Grid5_24)]
        [SMCSortable(true)]
        public bool? DiaUtil { get; set; }

        [SMCHidden(SMCViewMode.Filter)]
        [SMCRadioButtonList]
        [SMCRequired]
        [SMCSize(SMCSize.Grid14_24)]
        [SMCSortable(true)]
        public bool? ReplicarLancamentoPorLocalidade { get; set; }

        public override void ConfigureDynamic(ref SMCDynamicOptions options)
        {
            options.EditInModal()
                   .Tokens(tokenInsert: UC_ORG_001_11_02.MANTER_TIPO_AGENDA,
                           tokenEdit: UC_ORG_001_11_02.MANTER_TIPO_AGENDA,
                           tokenRemove: UC_ORG_001_11_02.MANTER_TIPO_AGENDA,
                           tokenList: UC_ORG_001_11_01.PESQUISAR_TIPO_AGENDA)
                   .Service<ITipoAgendaService>(save: nameof(ITipoAgendaService.SalvarTipoAgenda));
        }
    }
}