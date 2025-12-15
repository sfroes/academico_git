using SMC.Academico.ServiceContract.Areas.ALN.Interfaces;
using SMC.Academico.ServiceContract.Areas.CSO.Interfaces;
using SMC.Academico.ServiceContract.Areas.MAT.Interfaces;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Academico.UI.Mvc.Areas.CAM.Lookups;
using SMC.Academico.UI.Mvc.Areas.CSO.Lookups;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Html;
using SMC.SGA.Administrativo.Areas.ALN.Controllers;
using SMC.SGA.Administrativo.Areas.CSO.Controllers;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.ALN.Models
{
    public class AlunoFiltroDynamicModel : SMCDynamicFilterViewModel
    {
        #region [ DataSources ]

        [SMCDataSource]
        [SMCServiceReference(typeof(ITipoVinculoAlunoService), nameof(ITipoVinculoAlunoService.BuscarTipoVinculoAlunoNaInstituicaoSelect))]
        public List<SMCSelectListItem> TiposVinculoAluno { get; set; }

        [SMCDataSource]
        [SMCServiceReference(typeof(ISituacaoMatriculaService), nameof(ISituacaoMatriculaService.BuscarSituacoesMatriculasDaInstiuicaoSelect))]
        public List<SMCDatasourceItem> SituacoesMatricula { get; set; }

        /// <summary>
        /// Entidades resposáveis por curso segundo BI_CSO_002.NV01
        /// </summary>
        [SMCDataSource]
        [SMCServiceReference(typeof(IEntidadeService), nameof(IEntidadeService.BuscarEntidadesResponsaveisVisaoOrganizacionalSelect))]
        public List<SMCDatasourceItem> EntidadesResponsaveis { get; set; }

        [SMCDataSource]
        [SMCServiceReference(typeof(ICursoOfertaLocalidadeService), nameof(ICursoOfertaLocalidadeService.BuscarEntidadesSuperioresSelect))]
        public List<SMCDatasourceItem> Localidades { get; set; }

        [SMCDataSource]
        [SMCServiceReference(typeof(IInstituicaoNivelService), nameof(IInstituicaoNivelService.BuscarNiveisEnsinoReconhecidoLDBSelect))]
        public List<SMCDatasourceItem> NiveisEnsino { get; set; }

        #endregion [ DataSources ]

        [CicloLetivoLookup]
        [SMCFilter(true, true)]
        [SMCSize(SMCSize.Grid5_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid4_24)]
        public CicloLetivoLookupViewModel SeqCicloLetivoIngresso { get; set; }

        #region [BI_CSO_002]

        /// <summary>
        /// Sequencial da entidade responsável com o nome esperado pelo lookup (para facilitar o depency)
        /// e mapeado para o nome adequando para os dtos
        /// </summary>
        [SMCFilter(true, true)]
        [SMCMapProperty("SeqEntidadesResponsaveis")]
        [SMCSelect(nameof(EntidadesResponsaveis))]
        [SMCSize(SMCSize.Grid9_24, SMCSize.Grid24_24, SMCSize.Grid18_24, SMCSize.Grid6_24)]
        public List<long> SeqsGruposProgramasResponsaveis { get; set; }

        [SMCHidden]
        public bool ApenasAtivas { get; set; } = true;

        [SMCHidden]
        public bool UsarNomeReduzido { get; set; } = false;

        [SMCHidden]
        public bool UsarSeqEntidade { get; set; } = true;

        [SMCFilter(true, true)]
        [SMCSelect(nameof(Localidades), autoSelectSingleItem: true)]
        [SMCSize(SMCSize.Grid10_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid10_24)]
        public long? SeqLocalidade { get; set; }

        [SMCFilter(true, true)]
        [SMCSelect(nameof(NiveisEnsino))]
        [SMCSize(SMCSize.Grid7_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid4_24)]
        public long? SeqNivelEnsino { get; set; }

        [CursoOfertaLookup]
        [SMCDependency(nameof(SeqsGruposProgramasResponsaveis))]
        [SMCDependency(nameof(SeqLocalidade))]
        [SMCDependency(nameof(SeqNivelEnsino))]
        [SMCFilter(true, true)]
        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid16_24, SMCSize.Grid14_24)]
        public CursoOfertaLookupViewModel SeqCursoOferta { get; set; }

        [SMCConditionalReadonly(nameof(SeqCursoOferta), SMCConditionalOperation.Equals, "", RuleName = "Rule1")]
        [SMCConditionalReadonly(nameof(SeqLocalidade), SMCConditionalOperation.Equals, "", RuleName = "Rule2")]
        [SMCConditionalRule("Rule1 && Rule2")]
        [SMCDependency(nameof(SeqCursoOferta), nameof(TurnoController.BuscarTurnosPorCursoOfertaOuLocalidadeSelect), "Turno", "CSO", false, includedProperties: new[] { nameof(SeqLocalidade) })]
        [SMCDependency(nameof(SeqLocalidade), nameof(TurnoController.BuscarTurnosPorCursoOfertaOuLocalidadeSelect), "Turno", "CSO", false, includedProperties: new[] { nameof(SeqCursoOferta) })]
        [SMCFilter(true, true)]
        [SMCSelect]
        [SMCSize(SMCSize.Grid5_24, SMCSize.Grid24_24, SMCSize.Grid8_24, SMCSize.Grid5_24)]
        public long? SeqTurno { get; set; }

        #endregion [BI_CSO_002]

        [SMCDependency(nameof(SeqNivelEnsino), nameof(AlunoController.BuscarTipoVinculoAluno), "Aluno", false)]
        [SMCFilter(true, true)]
        [SMCSelect(nameof(TiposVinculoAluno))]
        [SMCSize(SMCSize.Grid8_24, SMCSize.Grid24_24, SMCSize.Grid8_24, SMCSize.Grid5_24)]
        public long? SeqTipoVinculoAluno { get; set; }

        [SMCConditionalReadonly(nameof(SeqNivelEnsino), "", RuleName = "RFormaIngressoNivelEnsino")]
        [SMCConditionalReadonly(nameof(SeqTipoVinculoAluno), "", RuleName = "RFormaIngressoTipoVinculoAluno")]
        [SMCConditionalRule("RFormaIngressoNivelEnsino || RFormaIngressoTipoVinculoAluno")]
        [SMCDependency(nameof(SeqNivelEnsino), nameof(AlunoController.BuscarFormasIngresso), "Aluno", true, nameof(SeqTipoVinculoAluno))]
        [SMCDependency(nameof(SeqTipoVinculoAluno), nameof(AlunoController.BuscarFormasIngresso), "Aluno", true, nameof(SeqNivelEnsino))]
        [SMCFilter(true, true)]
        [SMCSelect]
        [SMCSize(SMCSize.Grid9_24, SMCSize.Grid24_24, SMCSize.Grid8_24, SMCSize.Grid5_24)]
        public long? SeqFormaIngresso { get; set; }

        [CicloLetivoLookup]
        [SMCFilter(true, true)]
        [SMCSize(SMCSize.Grid7_24, SMCSize.Grid24_24, SMCSize.Grid8_24, SMCSize.Grid4_24)]
        public CicloLetivoLookupViewModel SeqCicloLetivoSituacaoMatricula { get; set; }

        [SMCConditionalReadonly(nameof(SeqCicloLetivoSituacaoMatricula), SMCConditionalOperation.Equals, "")]
        [SMCConditionalRequired(nameof(SeqCicloLetivoSituacaoMatricula), SMCConditionalOperation.NotEqual, "")]
        [SMCDependency(nameof(SeqNivelEnsino), nameof(AlunoController.BuscarSituacoesMatricula), "Aluno", false, nameof(SeqTipoVinculoAluno))]
        [SMCDependency(nameof(SeqTipoVinculoAluno), nameof(AlunoController.BuscarSituacoesMatricula), "Aluno", false, nameof(SeqNivelEnsino))]
        [SMCFilter(true, true)]
        [SMCSelect(nameof(SituacoesMatricula))]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid5_24)]
        public List<long> SeqsSituacaoMatriculaCicloLetivo { get; set; }

        [FormacaoEspecificaLookup]
        [SMCConditionalReadonly(nameof(SeqCursoOferta), "")]
        [SMCDependency(nameof(SeqCursoOferta))]
        [SMCFilter(true, true)]
        [SMCSize(SMCSize.Grid18_24, SMCSize.Grid24_24, SMCSize.Grid18_24, SMCSize.Grid10_24)]
        public FormacaoEspecificaLookupViewModel SeqFormacaoEspecifica { get; set; }

        [SMCFilter(true, true)]
        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid4_24, SMCSize.Grid4_24, SMCSize.Grid4_24)]
        public long? NumeroRegistroAcademico { get; set; }

        [SMCFilter(false)]
        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid4_24, SMCSize.Grid4_24, SMCSize.Grid4_24)]
        public int? CodigoAlunoMigracao { get; set; }

        [SMCFilter(true, true)]
        [SMCMapProperty("Nome")]
        [SMCMaxLength(100)]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid8_24, SMCSize.Grid6_24, SMCSize.Grid8_24)]
        public string NomeFiltro { get; set; }

        [SMCCpf]
        [SMCFilter(true, true)]
        [SMCSize(SMCSize.Grid5_24, SMCSize.Grid4_24, SMCSize.Grid5_24, SMCSize.Grid4_24)]
        public string Cpf { get; set; }

        [SMCFilter(true, true)]
        [SMCMaxLength(255)]
        [SMCSize(SMCSize.Grid5_24, SMCSize.Grid4_24, SMCSize.Grid5_24, SMCSize.Grid4_24)]
        public string NumeroPassaporte { get; set; }

    
    }
}