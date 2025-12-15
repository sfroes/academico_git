using SMC.Academico.Common.Areas.ORG.Constants;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.SGA.Administrativo.Areas.ORG.Views.InstituicaoNivelSistemaOrigem.App_LocalResources;
using SMC.Framework;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using System.Collections.Generic;
using SMC.Academico.Common.Areas.ORG.Enums;

namespace SMC.SGA.Administrativo.Areas.ORG.Models
{
    public class InstituicaoNivelSistemaOrigemDynamicModel : SMCDynamicViewModel
    {
        #region [ DataSources ]

        [SMCIgnoreProp]
        [SMCDataSource]
        [SMCServiceReference(typeof(IInstituicaoNivelSistemaOrigemService), nameof(IInstituicaoNivelSistemaOrigemService.BuscarSistemaOrigemGADSelect))]
        public List<SMCDatasourceItem<string>> SistemaOrigemGAD { get; set; }

        [SMCIgnoreProp]
        [SMCDataSource]
        [SMCServiceReference(typeof(IInstituicaoNivelService), nameof(IInstituicaoNivelService.BuscarNiveisEnsinoDaInstituicaoSelect))]
        public List<SMCDatasourceItem> InstituicaoNiveisEnsino { get; set; }
        #endregion

        [SMCHidden]
        public override long Seq { get; set; }

        [SMCFilter(true, true)]
        [SMCRequired]
        [SMCSelect(nameof(InstituicaoNiveisEnsino))]
        [SMCHidden(SMCViewMode.List)]        
        [SMCSize(SMCSize.Grid8_24, SMCSize.Grid24_24, SMCSize.Grid8_24, SMCSize.Grid8_24)]
        public long? SeqInstituicaoNivel { get; set; }

        [SMCFilter(true, true)]
        [SMCSelect(nameof(SistemaOrigemGAD))]
        [SMCRequired]
        [SMCHidden(SMCViewMode.List)]
        [SMCSize(SMCSize.Grid10_24)]
        public string TokenSistemaOrigemGAD { get; set; }
                
        [SMCSize(SMCSize.Grid12_24)]
        [SMCHidden(SMCViewMode.Insert | SMCViewMode.Edit | SMCViewMode.Filter)]
        public string DescricaoInstituicaoNivel { get; set; }

        [SMCSize(SMCSize.Grid12_24)]
        [SMCHidden(SMCViewMode.Insert | SMCViewMode.Edit | SMCViewMode.Filter)]
        public string DescricaoSistemaOrigemGAD { get; set; }

        [SMCRequired]
        [SMCFilter(true, true)]
        [SMCSelect]
        [SMCSize(SMCSize.Grid6_24)]
        public UsoSistemaOrigem UsoSistemaOrigem { get; set; }

        public override void ConfigureDynamic(ref SMCDynamicOptions options)
        {
            options.EditInModal()
                   .DisableInitialListing(true)
                   .Messages(x => string.Format(UIResource.Listar_Excluir_Confirmacao,
                                ((InstituicaoNivelSistemaOrigemDynamicModel)x).DescricaoSistemaOrigemGAD,
                                ((InstituicaoNivelSistemaOrigemDynamicModel)x).DescricaoInstituicaoNivel))
                   
                   .Service<IInstituicaoNivelSistemaOrigemService>
                           (index: nameof(IInstituicaoNivelSistemaOrigemService.BuscarInstituicaoNivelSistemaOrigemGAD),
                            save: nameof(IInstituicaoNivelSistemaOrigemService.SalvarInstituicaoNivelSIstemaOrigemGAD))
                   .Tokens(tokenInsert: UC_ORG_002_11_01.MANTER_SISTEMA_ORIGEM_GADINSTITUICAO_NIVEL, 
                           tokenEdit: UC_ORG_002_11_01.MANTER_SISTEMA_ORIGEM_GADINSTITUICAO_NIVEL,
                           tokenRemove: UC_ORG_002_11_01.MANTER_SISTEMA_ORIGEM_GADINSTITUICAO_NIVEL,
                           tokenList: UC_ORG_002_11_01.MANTER_SISTEMA_ORIGEM_GADINSTITUICAO_NIVEL);
        }
    }
}
