using SMC.Academico.Common.Areas.TUR.Constants;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Academico.ServiceContract.Areas.TUR.Interfaces;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.SGA.Administrativo.Areas.TUR.Views.InstituicaoNivelTipoTurma.App_LocalResources;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.TUR.Models
{
    public class InstituicaoNivelTipoTurmaDynamicModel : SMCDynamicViewModel
    {
        #region DataSource

        [SMCDataSource]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(IInstituicaoNivelService), nameof(IInstituicaoNivelService.BuscarNiveisEnsinoDaInstituicaoSelect))]
        public List<SMCDatasourceItem> InstituicaoNiveis { get; set; }

        [SMCDataSource("TipoTurma")]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(ITURDynamicService))]
        public List<SMCDatasourceItem> TiposTurma { get; set; }

        #endregion DataSource

        [SMCKey]
        [SMCHidden]
        [SMCRequired]
        public override long Seq { get; set; }

        [SMCRequired]
        [SMCFilter(true, true)]
        [SMCSortable(false, true, "InstituicaoNivel.NivelEnsino.Descricao")]
        [SMCSelect(nameof(InstituicaoNiveis), SortBy = SMCSortBy.Description)]
        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid12_24)]
        public long SeqInstituicaoNivel { get; set; }

        [SMCRequired]
        [SMCFilter(true, true)]
        [SMCSortable(false, true, "TipoTurma.Descricao")]
        [SMCSelect("TipoTurma", autoSelectSingleItem: true, SortBy = SMCSortBy.Description)]
        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid12_24)]
        public long SeqTipoTurma { get; set; }

        //public AssociacaoOfertaMatriz AssociacaoOfertaMatriz { get; set; }

        #region Campos para mensagem

        [SMCInclude("TipoTurma")]
        [SMCMapProperty("TipoTurma.Descricao")]
        [SMCIgnoreProp]
        public string DescricaoTipoTurma { get; set; }

        [SMCInclude("InstituicaoNivel.NivelEnsino")]
        [SMCMapProperty("InstituicaoNivel.NivelEnsino.Descricao")]
        [SMCIgnoreProp]
        public string DescricaoInstituicaoNivel { get; set; }

        #endregion Campos para mensagem

        public override void ConfigureDynamic(ref SMCDynamicOptions options)
        {
            options
                .EditInModal()
                .Messages(x => string.Format(UIResource.Listar_Excluir_Confirmacao,
                             ((InstituicaoNivelTipoTurmaDynamicModel)x).DescricaoTipoTurma,
                             ((InstituicaoNivelTipoTurmaDynamicModel)x).DescricaoInstituicaoNivel))
                   .Tokens(tokenList: UC_TUR_002_01_01.MANTER_TIPO_TURMA_INSTITUICAOO_NIVEL,
                           tokenInsert: UC_TUR_002_01_01.MANTER_TIPO_TURMA_INSTITUICAOO_NIVEL,
                           tokenEdit: UC_TUR_002_01_01.MANTER_TIPO_TURMA_INSTITUICAOO_NIVEL,
                           tokenRemove: UC_TUR_002_01_01.MANTER_TIPO_TURMA_INSTITUICAOO_NIVEL);
        }
    }
}