using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Academico.ServiceContract.Areas.APR.Interfaces;
using SMC.Academico.UI.Mvc.Areas.DCT.Lookups;
using SMC.Framework;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.SGA.Administrativo.Areas.ORT.Controllers;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.ORT.Models
{
    public class MembroLancamentoNotaBancaExaminadoraViewModel : SMCViewModelBase
    {
        #region [ Hidden ]

        [SMCHidden]
        public bool Ativo { get => true; }

        [SMCHidden]
        public string Instituicao { get; set; }

        [SMCHidden]
        public long Seq { get; set; }

        [SMCHidden]
        public long SeqAplicacaoAvaliacao { get; set; }

        [SMCHidden]
        public bool? Presidiu { get; set; }

        #endregion [ Hidden ]

        #region [ Data Source ]

        [SMCHidden]
        [SMCIgnoreProp]
        [SMCDataSource]
        public List<SMCDatasourceItem> InstituicoesExternas { get; set; }

        [SMCDataSource]
        [SMCIgnoreProp]
        [SMCHidden]
        [SMCServiceReference(typeof(IInstituicaoNivelTipoMembroBancaService), nameof(IInstituicaoNivelTipoMembroBancaService.BuscarTiposMembroBancaSelect), values: new[] { nameof(LancamentoNotaBancaExaminadoraDynamicModel.SeqNivelEnsino) })]
        public List<SMCDatasourceItem> TiposMembroBanca { get; set; }

        #endregion [ Data Source ]

        [SMCOrder(0)]
        [ColaboradorLookup]
        [SMCUnique]
        [SMCConditional(SMCConditionalBehavior.Required, nameof(NomeColaborador), SMCConditionalOperation.Equals, null, "")]
        [SMCSize(SMCSize.Grid8_24)]
        public ColaboradorLookupViewModel SeqColaborador { get; set; }

        [SMCOrder(1)]
        [SMCReadOnly]
        [SMCConditionalDisplay(nameof(SeqColaborador), SMCConditionalOperation.Equals, null, "", RuleName = "R1")]
        [SMCConditionalDisplay(nameof(NomeColaborador), SMCConditionalOperation.NotEqual, null, "", RuleName = "R2")]
        [SMCConditionalRule("R1 && R2")]
        [SMCConditionalReadonly(nameof(NomeColaborador), SMCConditionalOperation.NotEqual, "", PersistentValue = true)]
        [SMCSize(SMCSize.Grid7_24)]
        public string NomeColaborador { get; set; }

        [SMCOrder(1)]
        [SMCSize(SMCSize.Grid4_24)]
        [SMCRequired]
        [SMCSelect(nameof(TiposMembroBanca), AutoSelectSingleItem = true)]
        public TipoMembroBanca TipoMembroBanca { get; set; }

        [SMCSelect]
        [SMCOrder(2)]
        [SMCRequired]
        [SMCSize(SMCSize.Grid3_24)]
        public bool? Participou { get; set; }

        [SMCOrder(3)]
        [SMCDependency(nameof(SeqColaborador), nameof(AvaliacaoTrabalhoAcademicoController.BuscarInstituicaoExternaColaborador), "AvaliacaoTrabalhoAcademico", false, includedProperties: new string[] { nameof(Ativo) })]
        [SMCSelect(nameof(InstituicoesExternas), AutoSelectSingleItem = false, NameDescriptionField = nameof(Instituicao))]
        [SMCSize(SMCSize.Grid8_24)]
        public long? SeqInstituicaoExterna { get; set; }

        [SMCOrder(4)]
        [SMCConditionalDisplay(nameof(SeqInstituicaoExterna), SMCConditionalOperation.Equals, null, "", RuleName = "R1")]
        [SMCConditionalDisplay(nameof(NomeInstituicaoExterna), SMCConditionalOperation.NotEqual, null, "", RuleName = "R2")]
        [SMCConditionalRule("R1 && R2")]
        [SMCConditionalReadonly(nameof(NomeInstituicaoExterna), SMCConditionalOperation.NotEqual, "", PersistentValue = true)]
        [SMCSize(SMCSize.Grid7_24)]
        public string NomeInstituicaoExterna { get; set; }

        [SMCOrder(5)]
        [SMCSize(SMCSize.Grid8_24)]
        [SMCMaxLength(255)]
        [SMCConditionalReadonly(nameof(SeqInstituicaoExterna), SMCConditionalOperation.Equals, "", PersistentValue = false)]
        public string ComplementoInstituicao { get; set; }
    }
}