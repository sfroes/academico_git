using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Academico.Common.Areas.DCT.Constants;
using SMC.Academico.ServiceContract.Areas.DCT.Interfaces;
using SMC.Academico.UI.Mvc.Areas.CUR.Lookups;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.SGA.Administrativo.Areas.DCT.Controllers;
using SMC.SGA.Administrativo.Areas.DCT.Views.ColaboradorAptoComponente.App_LocalResources;
using System.Runtime.CompilerServices;

namespace SMC.SGA.Administrativo.Areas.DCT.Models
{
    public class ColaboradorAptoComponenteDynamicModel : SMCDynamicViewModel
    {
        #region Propriedades Auxiliares

        [SMCHidden]
        public TipoGestaoDivisaoComponente[] TiposGestaoDivisaoComponente { get { return new TipoGestaoDivisaoComponente[] { TipoGestaoDivisaoComponente.Turma }; } }

        [SMCHidden]
        [SMCDependency(nameof(SeqAtuacaoColaborador), nameof(ColaboradorAptoComponenteController.ValidarFormacaoAcademica), "ColaboradorAptoComponente", false)]
        public bool? PossuiFormacaoAcademica { get; set; }

        [SMCHidden]
        public long SeqComponenteCurricular
        {
            get { return this.ComponenteCurricular?.Seq ?? 0; }
        }

        #endregion

        [SMCHidden]
        [SMCKey]
        [SMCRequired]
        public override long Seq { get; set; }

        [SMCConditionalDisplay(nameof(PossuiFormacaoAcademica), SMCConditionalOperation.Equals, false)]
        [SMCHidden(SMCViewMode.Filter | SMCViewMode.List)]
        [SMCDisplay]
        [SMCHideLabel]
        [SMCCssClass("smc-size-md-24 smc-size-xs-24 smc-size-sm-24 smc-sga-mensagem-informativa smc-sga-mensagem")]
        public string MensagemInformativa { get { return UIResource.MSG_Nao_Possui_Formacao_Academica; }}

        [SMCHidden]
        [SMCParameter]
        [SMCRequired]
        public long SeqAtuacaoColaborador { get; set; }

        [SMCHidden(SMCViewMode.Filter | SMCViewMode.List)]
        [SMCReadOnly]
        [SMCSize(SMCSize.Grid24_24)]
        [SMCDependency(nameof(SeqAtuacaoColaborador), nameof(ColaboradorAptoComponenteController.BuscarNomeColaborador), "ColaboradorAptoComponente", false)]
        public string NomeColaborador { get; set; }


        [SMCSize(SMCSize.Grid24_24)]
        [SMCHidden(SMCViewMode.Filter | SMCViewMode.List)]
        [ComponenteCurricularLookup]
        [SMCDependency(nameof(TiposGestaoDivisaoComponente))]
        [SMCRequired]
        public ComponenteCurricularColaboradorAptoComppnenteLookupViewModel ComponenteCurricular { get; set; }

        [SMCHidden(SMCViewMode.Filter | SMCViewMode.Insert | SMCViewMode.Edit)]
        [SMCDescription]
        public string DescricaoComponenteCurricular { get; set; }

        public override void ConfigureDynamic(ref SMCDynamicOptions options)
        {
            options.HeaderIndex(nameof(ColaboradorAptoComponenteController.CabecalhoColaboradorAptoComponente))
                   .IgnoreFilterGeneration()
                   .EditInModal()
                   .RequiredIncomingParameters(new[] { nameof(SeqAtuacaoColaborador) })
                   .ButtonBackIndex("Index", "Colaborador")
                   .Service<IColaboradorAptoComponenteService>(index: nameof(IColaboradorAptoComponenteService.BuscarColadoradorAptoComponentes),
                                                               save: nameof(IColaboradorAptoComponenteService.SalvarColadoradorAptoComponente))
                   .Tokens(tokenInsert: UC_DCT_001_08_02.ASSOCIAR_COMPONENTE_APTO_LECIONAR,
                           tokenEdit: UC_DCT_001_08_02.ASSOCIAR_COMPONENTE_APTO_LECIONAR,
                           tokenRemove: UC_DCT_001_08_02.ASSOCIAR_COMPONENTE_APTO_LECIONAR,
                           tokenList: UC_DCT_001_08_01.PESQUISAR_COMPONENTE_APTO_LECIONAR);
        }
    }
}