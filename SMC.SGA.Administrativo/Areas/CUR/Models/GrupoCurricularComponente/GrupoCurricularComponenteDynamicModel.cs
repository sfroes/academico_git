using SMC.Academico.Common.Areas.CUR.Constants;
using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Academico.ServiceContract.Areas.CUR.Interfaces;
using SMC.Academico.UI.Mvc.Areas.CUR.Lookups;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.Model;
using SMC.Framework.Security;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.SGA.Administrativo.Areas.CUR.Views.GrupoCurricularComponente.App_LocalResources;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.CUR.Models
{
    public class GrupoCurricularComponenteDynamicModel : SMCDynamicViewModel, ISMCSeq
    {
        #region [ DataSources ]

        [SMCDataSource]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(IInstituicaoNivelTipoComponenteCurricularService),
            nameof(IInstituicaoNivelTipoComponenteCurricularService.BuscarTipoComponenteCurricularPorGrupoSelect),
            values: new string[] { nameof(SeqGrupoCurricular) })]
        public List<SMCDatasourceItem> TiposComponenteCurricular { get; set; }

        #endregion [ DataSources ]

        [SMCKey]
        [SMCHidden]
        public override long Seq { get; set; }

        [SMCHidden]
        [SMCParameter]
        public long SeqGrupoCurricular { get; set; }

        [SMCHidden]
        [SMCParameter]
        public long SeqCurriculo { get; set; }

        [SMCHidden]
        [SMCParameter]
        public long SeqNivelEnsino { get; set; }

        [SMCHidden]
        [SMCParameter]
        public FormatoConfiguracaoGrupo FormatoConfiguracaoGrupo { get; set; }

        [SMCHidden]
        [SMCParameter]
        public bool? Ativo { get { return true; } }

        [SMCCssClass("smc-sga-mensagem smc-sga-mensagem-informativa")]
        [SMCDisplay]
        [SMCHideLabel]
        [SMCSize(SMCSize.Grid24_24)]
        public string MensagemInformativa { get; set; } = UIResource.MSG_Informativa;

        [SMCRequired]
        [SMCSelect(nameof(TiposComponenteCurricular))]
        [SMCSize(SMCSize.Grid4_24)]
        public long SeqTipoComponenteCurricular { get; set; }

        //FIX: Remover o conditional read only ao corrigir o dependency com required do lookup
        [ComponenteCurricularLookup]
        [SMCDependency(nameof(Ativo))]
        [SMCDependency(nameof(FormatoConfiguracaoGrupo))]
        [SMCDependency(nameof(SeqGrupoCurricular))]
        [SMCDependency(nameof(SeqNivelEnsino))]
        [SMCDependency(nameof(SeqTipoComponenteCurricular))]
        [SMCConditionalReadonly(nameof(SeqTipoComponenteCurricular), SMCConditionalOperation.Equals, "", PersistentValue = true)]
        [SMCRequired]
        [SMCSize(SMCSize.Grid10_24)]
        public GrupoCurricularComponenteViewModel ComponenteCurricular { get; set; }

        [SMCHidden]
        public long SeqComponenteCurricular
        {
            get { return ComponenteCurricular?.Seq ?? 0; }
        }

        [SMCRequired]
        [SMCSize(SMCSize.Grid4_24)]
        [SMCMinValue(1)]
        [SMCParameter]
        public short QuantidadeExigida { get; set; }

        [SMCRequired]
        [SMCSize(SMCSize.Grid6_24)]
        [SMCRadioButtonList]
        public bool PermiteOrigemDispensaMesmoCurriculo { get; set; }

        public override void ConfigureDynamic(ref SMCDynamicOptions options)
        {
            options.EditInModal()
                   .RedirectIndexTo("Index", "GrupoCurricular", model => new
                   {
                       SeqCurriculo = SMCDESCrypto.EncryptNumberForURL((model as GrupoCurricularComponenteDynamicModel).SeqCurriculo)
                   })
                   .Tokens(tokenInsert: UC_CUR_001_01_07.ASSOCIAR_COMPONENTE_CURRICULAR_GRUPO,
                           tokenEdit: UC_CUR_001_01_07.ASSOCIAR_COMPONENTE_CURRICULAR_GRUPO,
                           tokenRemove: UC_CUR_001_01_07.ASSOCIAR_COMPONENTE_CURRICULAR_GRUPO);
        }
    }
}