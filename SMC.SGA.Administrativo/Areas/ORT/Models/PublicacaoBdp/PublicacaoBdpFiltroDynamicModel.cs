using SMC.Academico.Common.Areas.ORT.Enums;
using SMC.Academico.Common.Constants;
using SMC.Academico.ServiceContract.Areas.CSO.Interfaces;
using SMC.Academico.ServiceContract.Areas.MAT.Interfaces;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Academico.UI.Mvc.Areas.ALN.Lookups;
using SMC.Academico.UI.Mvc.Areas.CAM.Lookups;
using SMC.Academico.UI.Mvc.Areas.CSO.Lookups;
using SMC.Framework;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Html;
using SMC.SGA.Administrativo.Areas.ORT.Controllers;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.ORT.Models
{
    [SMCGroupedPropertyConfiguration(GroupId = "grupoSituacaoMatricula", GroupName = "Situação da matrícula", Size = SMCSize.Grid9_24)]
    public class PublicacaoBdpFiltroDynamicModel : SMCDynamicFilterViewModel
    {
        #region [ DataSources ]

        [SMCDataSource]
        [SMCServiceReference(typeof(IEntidadeService), nameof(IEntidadeService.BuscarUnidadesResponsaveisGPILocalSelect))]
        public List<SMCDatasourceItem> EntidadesResponsaveis { get; set; }

        [SMCIgnoreProp]
        //FIX: Verificar referência
        //[SMCDataSource(nameof(Turno))]
        [SMCDataSource("Turno")]
        [SMCServiceReference(typeof(ICSODynamicService))]
        public List<SMCDatasourceItem> Turnos { get; set; }

        [SMCDataSource]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(ITipoSituacaoMatriculaService), nameof(ITipoSituacaoMatriculaService.BuscarTiposSituacoesMatriculasTokenMatriculadoSelect))]
        public List<SMCSelectListItem> TiposSituacaoMatricula { get; set; }

        #endregion [ DataSources ]

        [SMCHidden]
        public bool AlunoDI { get; set; } = false;

        [SMCHidden]
        [SMCParameter(FILTER.INSTITUICAO_ENSINO, true)]
        public long SeqInstituicaoLogada { get; set; }

        [SMCOrder(0)]
        [SMCFilter(true, true)]
        [SMCHidden(SMCViewMode.List)]
        [SMCSelect(nameof(EntidadesResponsaveis))]
        [SMCSize(SMCSize.Grid14_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid12_24)]
        public List<long?> SeqsEntidadesResponsaveis { get; set; }

        [SMCOrder(1)]
        [CursoOfertaLocalidadeLookup]
        [SMCFilter(true, true)]
        [SMCDependency(nameof(SeqsEntidadesResponsaveis))]
        [SMCSize(SMCSize.Grid10_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid6_24)]
        public CursoOfertaLocalidadeLookupViewModel SeqCursoOfertaLocalidade { get; set; }

        [SMCOrder(2)]
        [SMCDependency(nameof(SeqCursoOfertaLocalidade), nameof(OrientacaoController.BuscarTurnosCursoOfertaLocalidadeSelect), "Orientacao", false)]
        [SMCFilter(true, true)]
        [SMCSelect(nameof(Turnos))]
        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid6_24)]
        public long? SeqTurno { get; set; }

        [SMCDependency("SeqCursoOfertaLocalidade.SeqCursoOferta")]
        [SMCHidden]
        public long? SeqCursoOfertaParam { get; set; }

        [SMCDependency("SeqCursoOfertaLocalidade.SeqLocalidade")]
        [SMCHidden]
        public long? SeqLocalidade { get; set; }

        [SMCDependency("SeqCursoOfertaLocalidade.SeqNivelEnsino")]
        [SMCHidden]
        public long? SeqNivelEnsino { get; set; }

        [AlunoLookup]
        [SMCOrder(3)]
        [SMCDependency(nameof(SeqCursoOfertaLocalidade))]
        [SMCDependency(nameof(SeqsEntidadesResponsaveis))]
        [SMCDependency(nameof(SeqCursoOfertaParam))]
        [SMCDependency(nameof(SeqLocalidade))]
        [SMCDependency(nameof(SeqTurno))]
        [SMCDependency(nameof(SeqNivelEnsino))]
        [SMCDependency(nameof(AlunoDI))]
        [SMCUnique]
        [SMCFilter(true, true)]
        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid12_24)]
        public AlunoLookupViewModel SeqAluno { get; set; }

        [SMCOrder(4)]
        [SMCFilter(true, true)]
        [SMCSize(SMCSize.Grid14_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid6_24)]
        [SMCSelect]
        public List<FiltroSituacaoTrabalhoAcademico> Situacao { get; set; }

        [SMCGroupedProperty("grupoSituacaoMatricula")]
        [CicloLetivoLookup]
        [SMCOrder(5)]
        [SMCFilter(true, true)]
        [SMCHidden(SMCViewMode.List)]
        [SMCSize(SMCSize.Grid12_24)]
        public CicloLetivoLookupViewModel SeqCicloLetivo { get; set; }

        [SMCGroupedProperty("grupoSituacaoMatricula")]
        [SMCOrder(6)]
        [SMCSelect(nameof(TiposSituacaoMatricula))]
        [SMCFilter(true, true)]
        [SMCSize(SMCSize.Grid12_24)]
        [SMCConditionalReadonly(nameof(SeqCicloLetivo), SMCConditionalOperation.Equals, "")]
        [SMCConditionalRequired(nameof(SeqCicloLetivo), SMCConditionalOperation.GreaterThen, 0)]
        [SMCDependency(nameof(SeqCicloLetivo), nameof(OrientacaoController.BuscarTiposSituacoesMatriculasTokenMatriculadoSelect), "Orientacao", true, new[] { nameof(SeqTipoSituacao) })]

        public long? SeqTipoSituacao { get; set; }
    }
}