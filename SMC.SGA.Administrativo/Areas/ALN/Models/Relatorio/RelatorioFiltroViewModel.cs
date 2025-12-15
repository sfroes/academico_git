using SMC.Academico.Common.Areas.ALN.Enums;
using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Academico.UI.Mvc.Areas.CAM.Lookups;
using SMC.Academico.UI.Mvc.Areas.CSO.Lookups;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Html;
using SMC.SGA.Administrativo.Areas.ALN.Controllers;
using SMC.SGA.Administrativo.Areas.CSO.Controllers;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.ALN.Models
{
    public class RelatorioFiltroViewModel : SMCPagerViewModel, ISMCMappable
    {
        #region [ DataSources ]

        public List<SMCDatasourceItem> EntidadesResponsaveis { get; set; }

        public List<SMCDatasourceItem> Localidades { get; set; }

        public List<SMCDatasourceItem> NiveisEnsino { get; set; }

        public List<SMCSelectListItem> TiposRelatorio { get; set; }

        public List<SMCDatasourceItem> TiposVinculos { get; set; }

        public List<SMCDatasourceItem> SituacoesMatricula { get; set; }

        public List<SMCDatasourceItem> NiveisEnsinoPorGrupoDocumentoAcademico { get; set; }

        public List<SMCDatasourceItem> TiposDocumentoAcademico { get; set; }

        public List<SMCDatasourceItem> IdiomasDocumentoAcademico { get; set; }

        #endregion [ DataSources ]

        #region Parametros dos Relatórios

        [SMCConditionalDisplay(nameof(TipoRelatorio), SMCConditionalOperation.Equals, TipoRelatorio.HistoricoEscolar, true)]
        [SMCConditionalRequired(nameof(TipoRelatorio), SMCConditionalOperation.Equals, TipoRelatorio.HistoricoEscolar, true)]
        [SMCSelect]
        public bool? ImprimirComponenteCurricularSemCreditos { get; set; }

        [SMCConditionalDisplay(nameof(TipoRelatorio), SMCConditionalOperation.Equals, TipoRelatorio.HistoricoEscolar, true)]
        [SMCConditionalRequired(nameof(TipoRelatorio), SMCConditionalOperation.Equals, TipoRelatorio.HistoricoEscolar, true)]
        [SMCSelect]
        public bool? ExibirMediaNotas { get; set; }

        [SMCConditionalDisplay(nameof(TipoRelatorio), SMCConditionalOperation.Equals, TipoRelatorio.HistoricoEscolarInterno, true)]
        [SMCConditionalRequired(nameof(TipoRelatorio), SMCConditionalOperation.Equals, TipoRelatorio.HistoricoEscolarInterno, true)]
        [SMCSelect]
        public bool? ExibeProfessor { get; set; }

        [SMCConditionalDisplay(nameof(TipoRelatorio), SMCConditionalOperation.Equals, TipoRelatorio.DeclaracaoDisciplinasCursadas, true)]
        [SMCConditionalRequired(nameof(TipoRelatorio), SMCConditionalOperation.Equals, TipoRelatorio.DeclaracaoDisciplinasCursadas, true)]
        [SMCSelect]
        public TipoDeclaracaoDisciplinaCursada? ExibirNaDeclaracao { get; set; }

        [SMCConditionalDisplay(nameof(TipoRelatorio), SMCConditionalOperation.Equals, TipoRelatorio.DeclaracaoDisciplinasCursadas, true)]
        [SMCConditionalRequired(nameof(TipoRelatorio), SMCConditionalOperation.Equals, TipoRelatorio.DeclaracaoDisciplinasCursadas, true)]
        [SMCSelect]
        public bool? ExibirEmentasComponentesCurriculares { get; set; }

        [SMCConditionalDisplay(nameof(TipoRelatorio), SMCConditionalOperation.Equals, TipoRelatorio.ListagemAssinatura, true)]
        [SMCConditionalRequired(nameof(TipoRelatorio), SMCConditionalOperation.Equals, TipoRelatorio.ListagemAssinatura, true)]
        [SMCSelect]
        public bool? ExibirCampoAssinatura { get; set; }

        [CicloLetivoLookup]
        [SMCConditional(SMCConditionalBehavior.Visibility | SMCConditionalBehavior.Required, nameof(TipoRelatorio), SMCConditionalOperation.Equals, TipoRelatorio.DeclaracaoMatricula, true)]
        public CicloLetivoLookupViewModel SeqCicloLetivo { get; set; }

        [SMCConditionalDisplay(nameof(TipoRelatorio), SMCConditionalOperation.Equals, TipoRelatorio.ListagemAssinatura)]
        [SMCMaxLength(150)]
        public string TituloListagem { get; set; }

        [SMCConditionalDisplay(nameof(TipoRelatorio), SMCConditionalOperation.Equals, TipoRelatorio.DeclaracaoGenerica, true)]
        [SMCConditionalRequired(nameof(TipoRelatorio), SMCConditionalOperation.Equals, TipoRelatorio.DeclaracaoGenerica, true)]
        [SMCSelect(nameof(NiveisEnsinoPorGrupoDocumentoAcademico))]
        public long? SeqNivelEnsinoPorGrupoDocumentoAcademico { get; set; }

        [SMCConditionalDisplay(nameof(TipoRelatorio), SMCConditionalOperation.Equals, TipoRelatorio.DeclaracaoGenerica, true)]
        [SMCConditionalRequired(nameof(TipoRelatorio), SMCConditionalOperation.Equals, TipoRelatorio.DeclaracaoGenerica, true)]
        [SMCSelect(nameof(TiposDocumentoAcademico))]
        [SMCDependency(nameof(SeqNivelEnsinoPorGrupoDocumentoAcademico), nameof(RelatorioController.PreencherTiposDocumentoAcademico), "Relatorio", "ALN", true)]
        public long? SeqTipoDocumentoAcademico { get; set; }

        [SMCConditionalDisplay(nameof(TipoRelatorio), SMCConditionalOperation.Equals, TipoRelatorio.DeclaracaoGenerica, true)]
        [SMCConditionalRequired(nameof(TipoRelatorio), SMCConditionalOperation.Equals, TipoRelatorio.DeclaracaoGenerica, true)]
        [SMCSelect(nameof(IdiomasDocumentoAcademico))]
        [SMCDependency(nameof(SeqNivelEnsinoPorGrupoDocumentoAcademico), nameof(RelatorioController.PreencherIdiomasDocumentoAcademico), "Relatorio", "ALN", true, includedProperties: new[] { nameof(SeqTipoDocumentoAcademico) })]
        [SMCDependency(nameof(SeqTipoDocumentoAcademico), nameof(RelatorioController.PreencherIdiomasDocumentoAcademico), "Relatorio", "ALN", true, includedProperties: new[] { nameof(SeqNivelEnsinoPorGrupoDocumentoAcademico) })]
        public SMCLanguage? IdiomaDocumentoAcademico { get; set; }

        #endregion Parametros dos Relatórios

        [SMCSelect(nameof(TiposRelatorio))]
        [SMCRequired]
        public TipoRelatorio TipoRelatorio { get; set; }

        [SMCFilter(true, true)]
        [SMCSize(SMCSize.Grid3_24, SMCSize.Grid24_24, SMCSize.Grid3_24, SMCSize.Grid3_24)]
        [SMCConditionalRequired(nameof(Nome), SMCConditionalOperation.Equals, "", RuleName = "R1")]
        [SMCConditionalRequired(nameof(SeqCicloLetivoIngresso), SMCConditionalOperation.Equals, "", RuleName = "R2")]
        [SMCConditionalRequired(nameof(SeqSituacaoMatricula), SMCConditionalOperation.Equals, "", RuleName = "R3")]
        [SMCConditionalRequired(nameof(SeqEntidadesResponsaveis), SMCConditionalOperation.Equals, "", RuleName = "R4")]
        [SMCConditionalRequired(nameof(SeqCursoOferta), SMCConditionalOperation.Equals, "", RuleName = "R5")]
        [SMCConditionalRequired(nameof(CodigoAlunoMigracao), SMCConditionalOperation.Equals, "", RuleName = "R6")]
        [SMCConditionalRule("R1 && R2 && R3 && R4 && R3 && R5 && R6")]
        public long? NumeroRegistroAcademico { get; set; }

        [SMCFilter(true, true)]
        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid24_24, SMCSize.Grid4_24, SMCSize.Grid4_24)]
        [SMCConditionalRequired(nameof(Nome), SMCConditionalOperation.Equals, "", RuleName = "R1")]
        [SMCConditionalRequired(nameof(SeqCicloLetivoIngresso), SMCConditionalOperation.Equals, "", RuleName = "R2")]
        [SMCConditionalRequired(nameof(SeqSituacaoMatricula), SMCConditionalOperation.Equals, "", RuleName = "R3")]
        [SMCConditionalRequired(nameof(SeqEntidadesResponsaveis), SMCConditionalOperation.Equals, "", RuleName = "R4")]
        [SMCConditionalRequired(nameof(SeqCursoOferta), SMCConditionalOperation.Equals, "", RuleName = "R5")]
        [SMCConditionalRequired(nameof(NumeroRegistroAcademico), SMCConditionalOperation.Equals, "", RuleName = "R6")]
        [SMCConditionalRule("R1 && R2 && R3 && R4 && R3 && R5 && R6")]
        public int? CodigoAlunoMigracao { get; set; }

        [SMCFilter(true, true)]
        [SMCSize(SMCSize.Grid10_24, SMCSize.Grid24_24, SMCSize.Grid10_24, SMCSize.Grid10_24)]
        [SMCConditionalRequired(nameof(NumeroRegistroAcademico), SMCConditionalOperation.Equals, "", RuleName = "R1")]
        [SMCConditionalRequired(nameof(SeqCicloLetivoIngresso), SMCConditionalOperation.Equals, "", RuleName = "R2")]
        [SMCConditionalRequired(nameof(SeqSituacaoMatricula), SMCConditionalOperation.Equals, "", RuleName = "R3")]
        [SMCConditionalRequired(nameof(SeqEntidadesResponsaveis), SMCConditionalOperation.Equals, "", RuleName = "R4")]
        [SMCConditionalRequired(nameof(SeqCursoOferta), SMCConditionalOperation.Equals, "", RuleName = "R5")]
        [SMCConditionalRequired(nameof(CodigoAlunoMigracao), SMCConditionalOperation.Equals, "", RuleName = "R6")]
        [SMCConditionalRule("R1 && R2 && R3 && R4 && R3 && R5 && R6")]
        public string Nome { get; set; }


        [CicloLetivoLookup]
        [SMCFilter(true, true)]
        [SMCSize(SMCSize.Grid7_24, SMCSize.Grid24_24, SMCSize.Grid7_24, SMCSize.Grid7_24)]
        [SMCConditionalRequired(nameof(NumeroRegistroAcademico), SMCConditionalOperation.Equals, "", RuleName = "R1")]
        [SMCConditionalRequired(nameof(Nome), SMCConditionalOperation.Equals, "", RuleName = "R2")]
        [SMCConditionalRequired(nameof(SeqCursoOferta), SMCConditionalOperation.Equals, "", RuleName = "R3")]
        [SMCConditionalRequired(nameof(SeqEntidadesResponsaveis), SMCConditionalOperation.Equals, "", RuleName = "R4")]
        [SMCConditionalRequired(nameof(SeqSituacaoMatricula), SMCConditionalOperation.Equals, "", RuleName = "R5")]
        [SMCConditionalRequired(nameof(CodigoAlunoMigracao), SMCConditionalOperation.Equals, "", RuleName = "R6")]
        [SMCConditionalRule("R1 && R2 && (!R3 || !R4) && R5 && R6")]
        public CicloLetivoLookupViewModel SeqCicloLetivoIngresso { get; set; }


        [SMCDependency(nameof(TipoRelatorio), "BuscarSituacoesMatricula", "Relatorio", false)]
        [SMCFilter(true, true)]
        [SMCSelect(nameof(SituacoesMatricula))]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid6_24)]
        [SMCConditionalReadonly(nameof(TipoRelatorio), SMCConditionalOperation.NotEqual, new object[] { TipoRelatorio.DeclaracaoDisciplinasCursadas, TipoRelatorio.DeclaracaoMatricula, TipoRelatorio.HistoricoEscolar, TipoRelatorio.HistoricoEscolarInterno, TipoRelatorio.IdentidadeEstudantil, TipoRelatorio.ListagemAssinatura, TipoRelatorio.DeclaracaoGenerica }, RuleName = "Readonly")]
        [SMCConditionalRequired(nameof(NumeroRegistroAcademico), SMCConditionalOperation.Equals, "", RuleName = "R1")]
        [SMCConditionalRequired(nameof(Nome), SMCConditionalOperation.Equals, "", RuleName = "R2")]
        [SMCConditionalRequired(nameof(SeqCursoOferta), SMCConditionalOperation.Equals, "", RuleName = "R3")]
        [SMCConditionalRequired(nameof(SeqEntidadesResponsaveis), SMCConditionalOperation.Equals, "", RuleName = "R4")]
        [SMCConditionalRequired(nameof(SeqCicloLetivoIngresso), SMCConditionalOperation.Equals, "", RuleName = "R5")]
        [SMCConditionalRequired(nameof(CodigoAlunoMigracao), SMCConditionalOperation.Equals, "", RuleName = "R6")]
        [SMCConditionalRule("R1 && R2 && (!R3 || !R4) && R5 && R6")]
        [SMCConditionalRule("Readonly")]
        public long? SeqSituacaoMatricula { get; set; }

        /// <summary>
        /// Sequencial da entidade responsável com o nome esperado pelo lookup (para facilitar o depency)
        /// e mapeado para o nome adequando para os dtos
        /// </summary>
        [SMCFilter(true, true)]
        [SMCMapProperty("SeqEntidadesResponsaveis")]
        [SMCSelect(nameof(EntidadesResponsaveis))]
        [SMCSize(SMCSize.Grid8_24, SMCSize.Grid24_24, SMCSize.Grid8_24, SMCSize.Grid8_24)]
        [SMCConditionalRequired(nameof(NumeroRegistroAcademico), SMCConditionalOperation.Equals, "", RuleName = "R1")]
        [SMCConditionalRequired(nameof(Nome), SMCConditionalOperation.Equals, "", RuleName = "R2")]
        [SMCConditionalRequired(nameof(SeqCicloLetivoIngresso), SMCConditionalOperation.Equals, "", RuleName = "R3")]
        [SMCConditionalRequired(nameof(SeqSituacaoMatricula), SMCConditionalOperation.Equals, "", RuleName = "R4")]
        [SMCConditionalRequired(nameof(SeqCursoOferta), SMCConditionalOperation.Equals, "", RuleName = "R5")]
        [SMCConditionalRequired(nameof(CodigoAlunoMigracao), SMCConditionalOperation.Equals, "", RuleName = "R6")]
        [SMCConditionalRule("R1 && R2 && (R3 || R4) && R5 && R6")]
        public long? SeqEntidadesResponsaveis { get; set; }

        [SMCCssClass("hidden")]
        [SMCDependency(nameof(SeqEntidadesResponsaveis), "PreencherSeqsEntidadesResponsaveis", "Relatorio", "ALN", false)]
        [SMCSelect()]
        public List<long> SeqsEntidadesResponsaveis { get; set; }

        [CursoOfertaLookup]
        [SMCDependency(nameof(SeqsEntidadesResponsaveis))]
        [SMCDependency(nameof(SeqNivelEnsino))]
        [SMCFilter(true, true)]
        [SMCSize(SMCSize.Grid10_24, SMCSize.Grid24_24, SMCSize.Grid10_24, SMCSize.Grid10_24)]
        [SMCConditionalRequired(nameof(NumeroRegistroAcademico), SMCConditionalOperation.Equals, "", RuleName = "R1")]
        [SMCConditionalRequired(nameof(Nome), SMCConditionalOperation.Equals, "", RuleName = "R2")]
        [SMCConditionalRequired(nameof(SeqCicloLetivoIngresso), SMCConditionalOperation.Equals, "", RuleName = "R3")]
        [SMCConditionalRequired(nameof(SeqSituacaoMatricula), SMCConditionalOperation.Equals, "", RuleName = "R4")]
        [SMCConditionalRequired(nameof(SeqEntidadesResponsaveis), SMCConditionalOperation.Equals, "", RuleName = "R5")]
        [SMCConditionalRequired(nameof(CodigoAlunoMigracao), SMCConditionalOperation.Equals, "", RuleName = "R6")]
        [SMCConditionalRule("R1 && R2 && (R3 || R4) && R5 && R6")]
        public CursoOfertaLookupViewModel SeqCursoOferta { get; set; }

        [SMCFilter(true, true)]
        [SMCSelect(nameof(Localidades))]
        [SMCSize(SMCSize.Grid9_24, SMCSize.Grid24_24, SMCSize.Grid9_24, SMCSize.Grid9_24)]
        public long? SeqLocalidade { get; set; }

        [SMCDependency(nameof(SeqCursoOferta), nameof(TurnoController.BuscarTurnosPorCursoOfertaOuLocalidadeSelect), "Turno", "CSO", false, includedProperties: new[] { nameof(SeqLocalidade) })]
        [SMCDependency(nameof(SeqLocalidade), nameof(TurnoController.BuscarTurnosPorCursoOfertaOuLocalidadeSelect), "Turno", "CSO", false, includedProperties: new[] { nameof(SeqCursoOferta) })]
        [SMCConditionalReadonly(nameof(SeqCursoOferta), SMCConditionalOperation.Equals, "", RuleName = "R1")]
        [SMCConditionalReadonly(nameof(SeqLocalidade), SMCConditionalOperation.Equals, "", RuleName = "R2")]
        [SMCConditionalRule("R1 && R2")]
        [SMCFilter(true, true)]
        [SMCSelect]
        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid24_24, SMCSize.Grid4_24, SMCSize.Grid4_24)]
        public long? SeqTurno { get; set; }

        [SMCFilter(true, true)]
        [SMCSelect(nameof(NiveisEnsino))]
        [SMCConditionalReadonly(nameof(TipoRelatorio), SMCConditionalOperation.Equals, TipoRelatorio.DeclaracaoGenerica)]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid6_24)]
        [SMCDependency(nameof(SeqNivelEnsinoPorGrupoDocumentoAcademico), nameof(RelatorioController.PreencherNivelEnsinoSelecionado), "Relatorio", "ALN", false, includedProperties: new[] { nameof(TipoRelatorio) })]
        [SMCDependency(nameof(TipoRelatorio), nameof(RelatorioController.PreencherNivelEnsinoSelecionado), "Relatorio", "ALN", false, includedProperties: new[] { nameof(SeqNivelEnsinoPorGrupoDocumentoAcademico) })]
        public long? SeqNivelEnsino { get; set; }

        [SMCFilter(true, true)]
        [SMCSelect(nameof(TiposVinculos))]
        [SMCSize(SMCSize.Grid5_24, SMCSize.Grid24_24, SMCSize.Grid5_24, SMCSize.Grid5_24)]
        public long? SeqTipoVinculoAluno { get; set; }

        [SMCHidden]
        public List<long> SelectedValues { get; set; }
    }
}