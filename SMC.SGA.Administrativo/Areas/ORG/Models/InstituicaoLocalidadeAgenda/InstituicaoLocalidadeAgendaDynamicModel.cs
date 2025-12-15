using SMC.Academico.Common.Areas.ORG.Constants;
using SMC.Academico.Common.Constants;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Calendarios.UI.Mvc.Areas.CLD.Lookups;
using SMC.Framework;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.ORG.Models
{
    public class InstituicaoLocalidadeAgendaDynamicModel : SMCDynamicViewModel
    {
        #region DataSource

        [SMCServiceReference(typeof(IHierarquiaEntidadeService), nameof(IHierarquiaEntidadeService.BuscarEntidadesHierarquiaSelect))]
        [SMCDataSource]
        [SMCIgnoreProp]
        public List<SMCDatasourceItem> Localidades { get; set; }

        [SMCServiceReference(typeof(ITipoAgendaService), nameof(ITipoAgendaService.BuscarTiposAgendaSelect), values: new string[] { nameof(SeqInstituicaoEnsino) })]
        [SMCDataSource]
        [SMCIgnoreProp]
        public List<SMCDatasourceItem> TiposAgenda { get; set; }

        #endregion DataSource

        [SMCReadOnly(SMCViewMode.Insert | SMCViewMode.Edit)]
        [SMCKey]
        [SMCIgnoreProp(SMCViewMode.Filter)]
        [SMCSize(SMCSize.Grid3_24)]
        public override long Seq { get; set; }

        [SMCRequired]
        [SMCHidden]
        [SMCParameter(FILTER.INSTITUICAO_ENSINO, true)]
        public long SeqInstituicaoEnsino { get; set; }

        [SMCSize(SMCSize.Grid8_24)]
        [SMCFilter(true, true)]
        [SMCSelect(nameof(Localidades), AutoSelectSingleItem = true)]
        [SMCRequired]
        public long? SeqEntidadeLocalidade { get; set; }

        [SMCSize(SMCSize.Grid8_24)]
        [SMCFilter(true, true)]
        [SMCSelect(nameof(TiposAgenda), AutoSelectSingleItem = true)]
        [SMCRequired]
        public long? SeqTipoAgenda { get; set; }

        [AgendaLookup]
        [SMCSize(SMCSize.Grid12_24)]
        [SMCIgnoreProp(SMCViewMode.Filter)]
        [SMCRequired]
        [SMCInclude(ignore: true)]
        public AgendaLookupViewModel AgendaAgd { get; set; }

        [SMCIgnoreProp]
        public long SeqAgendaAgd { get { return AgendaAgd?.Seq ?? 0; } set { AgendaAgd = AgendaAgd ?? new AgendaLookupViewModel(); AgendaAgd.Seq = value; } }

        public override void ConfigureDynamic(ref SMCDynamicOptions options)
        {
            options.EditInModal()
                   .ModalSize(SMCModalWindowSize.Auto)
                   .Tokens(tokenInsert: UC_ORG_002_05_02.MANTER_LOCALIDADE_INSTITUICAO_NIVEL,
                           tokenEdit: UC_ORG_002_05_02.MANTER_LOCALIDADE_INSTITUICAO_NIVEL,
                           tokenRemove: UC_ORG_002_05_02.MANTER_LOCALIDADE_INSTITUICAO_NIVEL,
                           tokenList: UC_ORG_002_05_01.PESQUISAR_LOCALIDADE_INSTITUICAO)
                   .Service<IInstituicaoLocalidadeAgendaService>(save: nameof(IInstituicaoLocalidadeAgendaService.SalvarInstituicaoLocalidadeAgenda),
                                                                 delete: nameof(IInstituicaoLocalidadeAgendaService.ExcluirInstituicaoLocalidadeAgenda));
        }
    }
}