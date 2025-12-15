using SMC.Academico.Common.Constants;
using SMC.Academico.ServiceContract.Areas.CSO.Interfaces;
using SMC.Academico.ServiceContract.Areas.MAT.Interfaces;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Academico.ServiceContract.Areas.ORT.Interfaces;
using SMC.Academico.UI.Mvc.Areas.ALN.Lookups;
using SMC.Academico.UI.Mvc.Areas.CAM.Lookups;
using SMC.Academico.UI.Mvc.Areas.CSO.Lookups;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Html;
using SMC.SGA.Administrativo.Areas.ORT.Controllers;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.ORT.Models
{
    [SMCGroupedPropertyConfiguration(GroupId = "FiltroSituacaoMatricula", Size = SMCSize.Grid9_24, CssClass = "smc-size-md-9, smc-size-xs-24, smc-size-sm-24")]
    public class TrabalhoAcademicoFiltroDynamicModel : SMCDynamicFilterViewModel
    {
        #region [ DataSources ]

        [SMCDataSource]
        [SMCServiceReference(typeof(IEntidadeService), nameof(IEntidadeService.BuscarUnidadesResponsaveisGPILocalSelect))]
        public List<SMCDatasourceItem> EntidadesResponsaveis { get; set; }

        [SMCIgnoreProp]
        //FIX: Verificar referências
        //[SMCDataSource(nameof(Turno))]
        [SMCDataSource("Turno")]
        [SMCServiceReference(typeof(ICSODynamicService))]
        public List<SMCDatasourceItem> Turnos { get; set; }

        [SMCDataSource]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(ITipoSituacaoMatriculaService), nameof(ITipoSituacaoMatriculaService.BuscarTiposSituacoesMatriculasTokenMatriculadoSelect))]
        public List<SMCSelectListItem> TiposSituacaoMatricula { get; set; }

        [SMCIgnoreProp]
        [SMCDataSource("TipoTrabalho")]
        [SMCServiceReference(typeof(IORTDynamicService))]
        public List<SMCDatasourceItem> TiposTrabalho { get; set; }

        #endregion [ DataSources ]

        #region [ Hidden ]

        [SMCKey]
        [SMCHidden]
        public long Seq { get; set; }

        [SMCHidden]
        [SMCParameter(FILTER.INSTITUICAO_ENSINO, true)]
        public long SeqInstituicaoLogada { get; set; }

        [SMCHidden]
        public bool AlunoDI { get; set; } = false;

        #endregion [ Hidden ]

        /// <summary>
        /// Sequencial da entidade responsável com o nome esperado pelo lookup (para facilitar o depency)
        /// e mapeado para o nome adequando para os dtos
        /// </summary>
        [SMCOrder(0)]
        [SMCFilter(true, true)]
        [SMCHidden(SMCViewMode.List)]
        [SMCMapProperty(nameof(SeqsEntidadesResponsaveis))]
        [SMCSelect(nameof(EntidadesResponsaveis))]
        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid12_24)]
        public List<long?> SeqsEntidadesResponsaveis { get; set; }

        [SMCOrder(1)]
        [CursoOfertaLocalidadeLookup]
        [SMCDependency(nameof(SeqsEntidadesResponsaveis))]
        [SMCSize(SMCSize.Grid8_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid8_24)]
        [SMCFilter(true, true)]
        public CursoOfertaLocalidadeLookupViewModel SeqCursoOfertaLocalidade { get; set; }

        [SMCOrder(2)]
        [SMCDependency(nameof(SeqCursoOfertaLocalidade), nameof(OrientacaoController.BuscarTurnosCursoOfertaLocalidadeSelect), "Orientacao", false)]
        [SMCFilter(true, true)]
        [SMCSelect(nameof(Turnos))]
        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid4_24)]
        public long? SeqTurno { get; set; }

        [AlunoLookup]
        [SMCOrder(3)]
        [SMCDependency(nameof(AlunoDI))]
        [SMCUnique]
        [SMCSize(SMCSize.Grid9_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid9_24)]
        [SMCFilter(true, true)]
        public AlunoLookupViewModel SeqAluno { get; set; }

        [SMCIgnoreProp]
        public string DescricaoCicloLetivo { get; set; }

        [SMCOrder(4)]
        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid4_24)]
        [SMCSelect(nameof(TiposTrabalho), autoSelectSingleItem: true)]
        [SMCHidden(SMCViewMode.List)]
        [SMCFilter(true, true)]
        public long? SeqTipoTrabalho { get; set; }

        [SMCOrder(5)]
        [SMCSize(SMCSize.Grid11_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid11_24)]
        [SMCFilter(true, true)]
        public string Titulo { get; set; }

        [SMCGroupedProperty("FiltroSituacaoMatricula")]
        [CicloLetivoLookup]
        [SMCOrder(6)]
        [SMCFilter(true, true)]
        [SMCHidden(SMCViewMode.List)]
        [SMCSize(SMCSize.Grid10_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid12_24)]
        public CicloLetivoLookupViewModel SeqCicloLetivo { get; set; }

        [SMCGroupedProperty("FiltroSituacaoMatricula")]
        [SMCOrder(7)]
        [SMCConditionalReadonly(nameof(SeqCicloLetivo), SMCConditionalOperation.Equals, "")]
        [SMCConditionalRequired(nameof(SeqCicloLetivo), SMCConditionalOperation.GreaterThen, 0)]
        [SMCDependency(nameof(SeqCicloLetivo), nameof(TrabalhoAcademicoController.BuscarTiposSituacoesMatriculasTokenMatriculadoSelect), "TrabalhoAcademico", true)]
        [SMCSelect(nameof(TiposSituacaoMatricula))]
        [SMCSize(SMCSize.Grid14_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid12_24)]
        [SMCFilter(true, true)]
        public long? SeqTipoSituacao { get; set; }
    }
}