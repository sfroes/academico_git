using SMC.Academico.Common.Areas.DCT.Enums;
using SMC.Academico.ServiceContract.Areas.ALN.Interfaces;
using SMC.Academico.ServiceContract.Areas.CSO.Interfaces;
using SMC.Academico.ServiceContract.Areas.MAT.Interfaces;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Academico.ServiceContract.Areas.ORT.Interfaces;
using SMC.Academico.UI.Mvc.Areas.ALN.Lookups;
using SMC.Academico.UI.Mvc.Areas.CAM.Lookups;
using SMC.Academico.UI.Mvc.Areas.CSO.Lookups;
using SMC.Academico.UI.Mvc.Areas.DCT.Lookups;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Html;
using SMC.SGA.Administrativo.Areas.CAM.Controllers;
using SMC.SGA.Administrativo.Areas.CSO.Controllers;
using SMC.SGA.Administrativo.Areas.ORT.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMC.SGA.Administrativo.Areas.ORT.Models
{
    [SMCGroupedPropertyConfiguration(GroupId = "FiltroOfertaCurso", Size = SMCSize.Grid24_24)]
    [SMCGroupedPropertyConfiguration(GroupId = "FiltroOrientacao", Size = SMCSize.Grid24_24, CssClass = "smc-size-md-24, smc-size-xs-24, smc-size-sm-24")]
    [SMCGroupedPropertyConfiguration(GroupId = "FiltroSituacaoMatricula", Size = SMCSize.Grid9_24, CssClass = "smc-size-md-9, smc-size-xs-24, smc-size-sm-24")]
    public class OrientacaoFiltroDynamicModel : SMCDynamicFilterViewModel
    {
        #region [ DataSources ]

        [SMCDataSource]
        [SMCServiceReference(typeof(ISituacaoMatriculaService), nameof(ISituacaoMatriculaService.BuscarSituacoesMatriculasDaInstiuicaoSelect))]
        public List<SMCDatasourceItem> SituacoesMatricula { get; set; }

        [SMCDataSource]
        [SMCServiceReference(typeof(IInstituicaoNivelService), nameof(IInstituicaoNivelService.BuscarNiveisEnsinoReconhecidoLDBSelect))]
        public List<SMCDatasourceItem> NiveisEnsino { get; set; }

        [SMCDataSource]
        [SMCServiceReference(typeof(ICursoService), nameof(ICursoService.BuscarHierarquiaSuperiorCursoSelect))]
        public List<SMCDatasourceItem> EntidadesResponsaveis { get; set; }

        [SMCDataSource]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(ICursoOfertaLocalidadeService), nameof(ICursoOfertaLocalidadeService.BuscarLocalidadesAtivasSelect))]
        public List<SMCDatasourceItem> Localidades { get; set; }

        [SMCDataSource]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(IInstituicaoNivelTipoOrientacaoService), nameof(IInstituicaoNivelTipoOrientacaoService.BuscarTiposOrientacaoPermiteManutencaoManualSelect))]
        public List<SMCDatasourceItem> TiposOrientacoes { get; set; }

        [SMCDataSource]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(ITipoSituacaoMatriculaService), nameof(ITipoSituacaoMatriculaService.BuscarTiposSituacoesMatriculasSelect))]
        public List<SMCSelectListItem> TiposSituacoesMatricula { get; set; }

        [SMCHidden]
        public bool trazerSelecionado { get; set; } = false;

        [SMCDataSource]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(ITurnoService), nameof(ITurnoService.BuscarTunos))]
        public List<SMCDatasourceItem> Turnos { get; set; }

        #endregion [ DataSources ]

        #region BI_CSO_002

        /// <summary>
        /// Sequencial da entidade responsável com o nome esperado pelo lookup (para facilitar o depency)
        /// e mapeado para o nome adequando para os dtos
        /// </summary>
        [SMCGroupedProperty("FiltroOfertaCurso")]
        [SMCFilter(true, true)]
        [SMCMapProperty("SeqsEntidadesResponsaveisHierarquiaItem")]
        [SMCSelect(nameof(EntidadesResponsaveis))]
        [SMCSize(SMCSize.Grid9_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid10_24)]
        public List<long?> SeqsEntidadesResponsaveis { get; set; }

        [SMCHidden]
        public bool TelaPesquisa { get; set; } = true;

        [CursoOfertaLocalidadeLookup]
        [SMCDependency(nameof(SeqsEntidadesResponsaveis))]
        [SMCGroupedProperty("FiltroOfertaCurso")]
        [SMCFilter(true, true)]
        [SMCSize(SMCSize.Grid9_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid10_24)]
        public CursoOfertaLocalidadeLookupViewModel SeqCursoOfertaLocalidade { get; set; }

        [SMCDependency(nameof(SeqCursoOfertaLocalidade), nameof(OrientacaoController.BuscarTurnosCursoOfertaLocalidadeSelect), "Orientacao", false)]
        [SMCGroupedProperty("FiltroOfertaCurso")]
        [SMCFilter(true, true)]
        [SMCSelect(nameof(Turnos))]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid4_24)]
        public long? SeqTurno { get; set; }

        #endregion [BI_CSO_002]

        #region Corpo do Filtro        

        /// <summary>
        /// Trazer somente os colaboradores que tenham a participação do tipo orientador - Isso para o Lookup de colaborador
        /// </summary>
        [SMCHidden]
        public bool Oritentador { get; set; } = false;

        [SMCHidden]
        public TipoAtividadeColaborador TipoAtividade { get; set; } = TipoAtividadeColaborador.Orientacao;

        /// <summary>
        /// Verificar se o tipo de entidade permite vinculo
        /// </summary>
        [SMCHidden]
        public bool TipoEntidadePermiteVinculo { get; set; }

        [AlunoLookup]
        [SMCDependency(nameof(TelaPesquisa))]
        [SMCFilter(true, true)]
        [SMCSize(SMCSize.Grid9_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid9_24)]
        public AlunoLookupViewModel SeqPessoaAtuacao { get; set; }

        [ColaboradorLookup]
        [SMCDependency(nameof(TipoAtividade))]
        [SMCDependency(nameof(Oritentador))]
        [SMCFilter(true, true)]
        [SMCSize(SMCSize.Grid9_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid9_24)]
        public ColaboradorLookupViewModel SeqColaborador { get; set; }

        [SMCFilter(true, true)]
        [SMCSelect(nameof(TiposOrientacoes))]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid6_24)]
        public long? SeqTipoOrientacao { get; set; }

        #endregion

        #region BI_ALN_003
        ///Filtros comentados até a validação dos filtros serem discutidos

        [CicloLetivoLookup]
        [SMCFilter(true, true)]
        [SMCGroupedProperty("FiltroSituacaoMatricula")]
        [SMCSize(SMCSize.Grid10_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid12_24)]
        public CicloLetivoLookupViewModel SeqCicloLetivo { get; set; }

        [SMCGroupedProperty("FiltroSituacaoMatricula")]
        [SMCConditionalReadonly(nameof(SeqCicloLetivo), SMCConditionalOperation.Equals, "")]
        [SMCConditionalRequired(nameof(SeqCicloLetivo), SMCConditionalOperation.GreaterThen, 0)]
        [SMCDependency(nameof(SeqCicloLetivo), nameof(OrientacaoController.BuscarTiposSituacoesMatriculasTokenMatriculadoSelect), "Orientacao", true, new[] { nameof(SeqTipoSituacaoMatricula)})]
        [SMCFilter(true, true)]
        [SMCSelect(nameof(TiposSituacoesMatricula))]
        [SMCSize(SMCSize.Grid14_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid12_24)]
        public long? SeqTipoSituacaoMatricula { get; set; }

        /// <summary>
        /// Somente ira exibir orientações do tipo de orientação que permita a manutenção manual
        /// </summary>
        [SMCHidden]
        public bool? PermiteManutencaoManual { get; set; } = true;

        #endregion
    }
}