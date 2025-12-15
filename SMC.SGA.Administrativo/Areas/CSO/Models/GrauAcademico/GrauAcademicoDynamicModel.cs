using SMC.Academico.Common.Areas.CSO.Constants;
using SMC.Academico.ServiceContract.Areas.CSO.Interfaces;
using SMC.Academico.UI.Mvc.Areas.ORG.Lookups;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.CSO.Models
{
    public class GrauAcademicoDynamicModel : SMCDynamicViewModel
    {
        /// <summary>
        /// Construtor padrão, define a propriedade Ativo para true
        /// </summary>
        public GrauAcademicoDynamicModel()
        {
            this.Ativo = true;
        }

        [SMCKey]
        [SMCFilter(true, true)]
        [SMCSortable(true)]
        [SMCOrder(0)]
        [SMCReadOnly(SMCViewMode.Edit | SMCViewMode.Insert)]
        [SMCRequired]
        [SMCSize(Framework.SMCSize.Grid2_24)]
        public override long Seq { get; set; }

        [SMCDescription]
        [SMCFilter(true, true)]
        [SMCSortable(true, true)]
        [SMCOrder(1)]
        [SMCRequired]
        [SMCMaxLength(100)]
        [SMCSize(Framework.SMCSize.Grid22_24)]
        public string Descricao { get; set; }
                
        [SMCOrder(2)]
        [SMCMaxLength(100)]
        [SMCHidden(SMCViewMode.List | SMCViewMode.Filter)]
        [SMCSize(Framework.SMCSize.Grid16_24)]
        public string DescricaoXSD { get; set; }

        [SMCRadioButtonList]
        [SMCMapForceFromTo]
        [SMCOrder(3)]
        [SMCStep(2)]
        [SMCHidden(SMCViewMode.Filter)]
        [SMCSize(SMCSize.Grid7_24)]
        public bool Ativo { get; set; }

        [SMCRequired]
        [SMCOrder(4)]
        [SMCHidden(SMCViewMode.List | SMCViewMode.Filter)]
        [SMCSize(SMCSize.Grid24_24)]
        [NivelEnsinoLookup(MinItens = 1)]
        public List<NivelEnsinoLookupViewModel> NiveisEnsino { get; set; }

        public override void ConfigureDynamic(ref SMCDynamicOptions options)
        {
            options.EditInModal()
                   .Service<IGrauAcademicoService>(save: nameof(IGrauAcademicoService.SalvarGrauAcademico))
                   .Tokens(tokenInsert: UC_CSO_001_03_02.MANTER_GRAU_ACADEMICO,
                           tokenEdit: UC_CSO_001_03_02.MANTER_GRAU_ACADEMICO,
                           tokenRemove: UC_CSO_001_03_02.MANTER_GRAU_ACADEMICO,
                           tokenList: UC_CSO_001_03_01.PESQUISAR_GRAU_ACADEMICO);
        }
    }
}