using SMC.Academico.Common.Areas.ORT.Constants;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Academico.ServiceContract.Areas.ORT.Interfaces;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.SGA.Administrativo.Areas.ORT.Views.InstituicaoNivelTipoTrabalho.App_LocalResources;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.ORT.Models
{
    public class InstituicaoNivelTipoTrabalhoDynamicModel : SMCDynamicViewModel
    {
        [SMCKey]
        [SMCHidden]
        [SMCSortable(false)]
        public override long Seq { get; set; }

        [SMCOrder(1)]
        [SMCRequired]
        [SMCSelect("InstituicaoNiveis", autoSelectSingleItem: true)]
        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid12_24)]
        [SMCHidden(SMCViewMode.List)]
        public long SeqInstituicaoNivel { get; set; }

        [SMCIgnoreProp]
        [SMCDataSource()]
        [SMCServiceReference(typeof(IInstituicaoNivelService), "BuscarNiveisEnsinoDaInstituicaoSelect")]
        public List<SMCDatasourceItem> InstituicaoNiveis { get; set; }

        [SMCHidden(SMCViewMode.Insert | SMCViewMode.Edit | SMCViewMode.Filter)]
        [SMCInclude("InstituicaoNivel.NivelEnsino")]
        [SMCMapProperty("InstituicaoNivel.NivelEnsino.Descricao")]
        [SMCSortable(true, true, "InstituicaoNivel.NivelEnsino.Descricao")]
        public string DescricaoInstituicaoNivel { get; set; }

        [SMCOrder(2)]
        [SMCRequired]
        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid12_24)]
        [SMCSelect("TiposTrabalho", autoSelectSingleItem: true, SortBy = SMCSortBy.Description)]
        [SMCHidden(SMCViewMode.List)]
        public long SeqTipoTrabalho { get; set; }

        [SMCIgnoreProp]
        [SMCDataSource("TipoTrabalho")]
        [SMCServiceReference(typeof(IORTDynamicService))]
        public List<SMCDatasourceItem> TiposTrabalho { get; set; }

        [SMCHidden(SMCViewMode.Insert | SMCViewMode.Edit | SMCViewMode.Filter)]
        [SMCInclude("TipoTrabalho")]
        [SMCMapProperty("TipoTrabalho.Descricao")]
        [SMCSortable(true, true, "TipoTrabalho.Descricao")]
        public string DescricaoTipoTrabalho { get; set; }

        [SMCOrder(3)]
        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid12_24)]
        [SMCHidden(SMCViewMode.List)]
        [SMCMinValue(1)]
        [SMCMaxValue(99)]
        public short? QuantidadeMaximaAlunos { get; set; }

        [SMCOrder(4)]
        [SMCRequired]
        [SMCRadioButtonList]
        [SMCSize(Framework.SMCSize.Grid24_24, Framework.SMCSize.Grid24_24, Framework.SMCSize.Grid24_24, Framework.SMCSize.Grid12_24)]
        [SMCHidden(SMCViewMode.List)]
        public bool GeraFinanceiroEntregaTrabalho { get; set; }

        [SMCOrder(5)]
        [SMCRequired]
        [SMCRadioButtonList]
        [SMCSize(Framework.SMCSize.Grid24_24, Framework.SMCSize.Grid24_24, Framework.SMCSize.Grid24_24, Framework.SMCSize.Grid12_24)]
        [SMCHidden(SMCViewMode.List)]
        public bool PublicacaoBibliotecaObrigatoria { get; set; }

        [SMCOrder(6)]
        [SMCRequired]
        [SMCSize(SMCSize.Grid12_24)]
        [SMCRadioButtonList]
        public bool PermiteInclusaoManual { get; set; }

        [SMCOrder(7)]
        [SMCConditionalRequired(nameof(PublicacaoBibliotecaObrigatoria), SMCConditionalOperation.Equals, true, RuleName = "R1")]
        [SMCConditionalRequired(nameof(GeraFinanceiroEntregaTrabalho), SMCConditionalOperation.Equals, true, RuleName = "R2")]
        [SMCConditionalRule("R1 && R2")]
        [SMCConditionalReadonly(nameof(PublicacaoBibliotecaObrigatoria), SMCConditionalOperation.NotEqual, true, RuleName = "R4")]
        [SMCConditionalReadonly(nameof(GeraFinanceiroEntregaTrabalho), SMCConditionalOperation.NotEqual, true, RuleName = "R3")]
        [SMCConditionalRule("R3 || R4")]
        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid12_24)]
        [SMCSelect("TiposTrabalho", autoSelectSingleItem: false, SortBy = SMCSortBy.Description)]
        [SMCHidden(SMCViewMode.List)]
        public long? SeqTipoTrabalhoCancelamento { get; set; }

        [SMCOrder(8)]
        [SMCRequired]
        [SMCSize(SMCSize.Grid12_24)]
        [SMCRadioButtonList]
        public bool TrabalhoQualificacao { get; set; }

        public override void ConfigureDynamic(ref SMCDynamicOptions options)
        {
            options.EditInModal()
                   .Messages(x => string.Format(UIResource.Listar_Excluir_Confirmacao,
                                ((InstituicaoNivelTipoTrabalhoDynamicModel)x).DescricaoTipoTrabalho,
                                ((InstituicaoNivelTipoTrabalhoDynamicModel)x).DescricaoInstituicaoNivel))
                   .Tokens(tokenInsert: UC_ORT_004_02_01.MANTER_TIPO_TRABALHO_INSTITUICAO_NIVEL,
                                 tokenEdit: UC_ORT_004_02_01.MANTER_TIPO_TRABALHO_INSTITUICAO_NIVEL,
                                 tokenRemove: UC_ORT_004_02_01.MANTER_TIPO_TRABALHO_INSTITUICAO_NIVEL,
                                 tokenList: UC_ORT_004_02_01.MANTER_TIPO_TRABALHO_INSTITUICAO_NIVEL);
        }
    }
}