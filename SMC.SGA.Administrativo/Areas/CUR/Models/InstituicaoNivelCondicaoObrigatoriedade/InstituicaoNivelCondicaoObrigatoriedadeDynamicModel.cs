using SMC.Academico.ServiceContract.Areas.CUR.Interfaces;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Framework;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using System;
using System.Collections.Generic;
using SMC.SGA.Administrativo.Areas.CUR.Views.InstituicaoNivelCondicaoObrigatoriedade.App_LocalResources;
using System.Linq;
using System.Web;
using SMC.Framework.Mapper;
using SMC.Academico.Common.Areas.CUR.Constants;

namespace SMC.SGA.Administrativo.Areas.CUR.Models
{
    public class InstituicaoNivelCondicaoObrigatoriedadeDynamicModel : SMCDynamicViewModel
    {

        #region DataSource

        [SMCDataSource]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(IInstituicaoNivelService), nameof(IInstituicaoNivelService.BuscarNiveisEnsinoDaInstituicaoSelect))]
        public List<SMCDatasourceItem> InstituicaoNiveis { get; set; }

        [SMCDataSource("CondicaoObrigatoriedade")]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(ICURDynamicService))]
        public List<SMCDatasourceItem> CondicoesObrigatoriedade { get; set; }

        #endregion DataSource

        [SMCKey]
        [SMCHidden]
        [SMCRequired]
        public override long Seq { get; set; }

        [SMCSortable(true, true)]
        [SMCRequired]
        [SMCSelect(nameof(InstituicaoNiveis), SortBy = SMCSortBy.Description)]
        [SMCSize(SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid6_24)]
        public long SeqInstituicaoNivel { get; set; }

        [SMCSortable(true, true)]
        [SMCRequired]
        [SMCSelect("CondicaoObrigatoriedade",autoSelectSingleItem: true, SortBy = SMCSortBy.Description)]
        [SMCSize(SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid18_24)]
        public long SeqCondicaoObrigatoriedade { get; set; }

        #region Campos para mensagem

        [SMCInclude("CondicaoObrigatoriedade")]
        [SMCMapProperty("CondicaoObrigatoriedade.Descricao")]
        [SMCIgnoreProp]
        public string DescricaoCondicaoObrigatoriedade { get; set; }

        [SMCInclude("InstituicaoNivel.NivelEnsino")]
        [SMCMapProperty("InstituicaoNivel.NivelEnsino.Descricao")]
        [SMCIgnoreProp]
        public string DescricaoInstituicaoNivel { get; set; }

        #endregion Campos para mensagem

        #region Configuracoes

        public override void ConfigureDynamic(ref SMCDynamicOptions options)
        {
            options
                .EditInModal()
                .Messages(x => string.Format(UIResource.Listar_Excluir_Confirmacao,
                           ((InstituicaoNivelCondicaoObrigatoriedadeDynamicModel)x).DescricaoCondicaoObrigatoriedade,
                           ((InstituicaoNivelCondicaoObrigatoriedadeDynamicModel)x).DescricaoInstituicaoNivel))
                   .Tokens(tokenList: UC_CUR_004_04_01.MANTER_CONDICAO_OBRIGATORIEDADE_INSTITUICAO_NIVEL_ENSINO,
                           tokenInsert: UC_CUR_004_04_01.MANTER_CONDICAO_OBRIGATORIEDADE_INSTITUICAO_NIVEL_ENSINO,
                           tokenEdit: UC_CUR_004_04_01.MANTER_CONDICAO_OBRIGATORIEDADE_INSTITUICAO_NIVEL_ENSINO,
                           tokenRemove: UC_CUR_004_04_01.MANTER_CONDICAO_OBRIGATORIEDADE_INSTITUICAO_NIVEL_ENSINO);

        }

        #endregion Configuracaoes


    }
}