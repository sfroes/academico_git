using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Academico.ServiceContract.Areas.APR.Interfaces;
using SMC.Academico.UI.Mvc.Areas.DCT.Lookups;
using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.Framework;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.SGA.Administrativo.Areas.ORT.Controllers;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.ORT.Models
{
    public class AvaliacaoTrabalhoAcademicoMembroBancaViewModel : SMCViewModelBase
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
        public long SeqAvaliacao { get; set; }

        [SMCHidden]
        public string Nome { get; set; }

        [SMCHidden]
        public bool? Participou { get; set; }

        [SMCHidden]
        public string DescricaoMembro { get; set; }

        [SMCHidden]
        public Sexo? Sexo { get; set; }

        [SMCHidden]
        public bool AvaliacaoPossuiApuracao { get; set; }

        [SMCHidden]
        public bool? Presidiu { get; set; }

        #endregion [ Hidden ]

        #region [ Data Source ]

        [SMCHidden]
        [SMCIgnoreProp]
        [SMCDataSource]
        public List<SMCDatasourceItem> InstituicoesExternas { get; set; }

        [SMCDataSource]
        [SMCHidden]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(IInstituicaoNivelTipoMembroBancaService), nameof(IInstituicaoNivelTipoMembroBancaService.BuscarTiposMembroBancaSelect), values: new[] { nameof(AvaliacaoTrabalhoAcademicoBancaExaminadoraDynamicModel.SeqNivelEnsino) })]
        public List<SMCDatasourceItem> TiposMembroBanca { get; set; }

        #endregion [ Data Source ]

        [SMCOrder(0)]
        [ColaboradorLookup]
        [SMCRequired]
        [SMCUnique]
        [SMCDependency("DataInicio")]
        [SMCDependency("DataFim")]
        [SMCSize(SMCSize.Grid16_24)]
        public ColaboradorLookupViewModel SeqColaborador { get; set; }

        [SMCOrder(1)]
        [SMCReadOnly]
        [SMCConditionalDisplay(nameof(SeqColaborador), SMCConditionalOperation.Equals, null, "", RuleName = "R1")]
        [SMCConditionalDisplay(nameof(NomeColaborador), SMCConditionalOperation.NotEqual, null, "", RuleName = "R2")]
        [SMCConditionalRule("R1 && R2")]
        [SMCConditionalReadonly(nameof(NomeColaborador), SMCConditionalOperation.NotEqual, "", PersistentValue = true)]
        [SMCSize(SMCSize.Grid8_24)]
        public string NomeColaborador { get; set; }

        [SMCOrder(2)]
        [SMCSize(SMCSize.Grid8_24)]
        [SMCRequired]
        [SMCSelect(nameof(TiposMembroBanca), AutoSelectSingleItem = true)]
        public TipoMembroBanca TipoMembroBanca { get; set; }

        [SMCOrder(3)]
        [SMCDependency(nameof(SeqColaborador), nameof(AvaliacaoTrabalhoAcademicoController.BuscarInstituicaoExternaColaborador), "AvaliacaoTrabalhoAcademico", false, includedProperties: new[] { nameof(Ativo) })]
        [SMCSelect(nameof(InstituicoesExternas), AutoSelectSingleItem = false, NameDescriptionField = nameof(Instituicao))]
        [SMCSize(SMCSize.Grid12_24)]
        public long? SeqInstituicaoExterna { get; set; }

        [SMCOrder(4)]
        [SMCReadOnly]
        [SMCConditionalDisplay(nameof(SeqInstituicaoExterna), SMCConditionalOperation.Equals, null, "", RuleName = "R1")]
        [SMCConditionalDisplay(nameof(NomeInstituicaoExterna), SMCConditionalOperation.NotEqual, null, "", RuleName = "R2")]
        [SMCConditionalRule("R1 && R2")]
        [SMCConditionalReadonly(nameof(NomeInstituicaoExterna), SMCConditionalOperation.NotEqual, "", PersistentValue = true)]
        [SMCSize(SMCSize.Grid12_24)]
        public string NomeInstituicaoExterna { get; set; }

        [SMCOrder(5)]
        [SMCMaxLength(255)]
        [SMCSize(SMCSize.Grid12_24)]
        [SMCConditionalReadonly(nameof(SeqInstituicaoExterna), SMCConditionalOperation.Equals, "", PersistentValue = false, RuleName = "R1")]
        [SMCConditionalReadonly("AlunoFormado", SMCConditionalOperation.NotEqual, "", PersistentValue = true, RuleName = "R2")]
        [SMCDependency(nameof(SeqInstituicaoExterna), nameof(AvaliacaoTrabalhoAcademicoBancaExaminadoraController.PreencherComplementoInstituicao), "AvaliacaoTrabalhoAcademicoBancaExaminadora", false, includedProperties: new string[] { "NotaConceito", nameof(ComplementoInstituicao), "AlunoFormado" })]
        [SMCConditionalRule("R1 || R2")]
        public string ComplementoInstituicao { get; set; }
    }
}