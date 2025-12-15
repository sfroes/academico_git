using SMC.Academico.Common.Areas.ORG.Enums;
using SMC.Academico.Common.Constants;
using SMC.Academico.ServiceContract.Areas.CSO.Interfaces;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Academico.UI.Mvc.Areas.CSO.Lookups;
using SMC.Framework;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Html;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.CSO.Models
{
    public class CursoUnidadeFiltroDynamicModel : SMCDynamicFilterViewModel
    {
        #region [ DataSources ]

        [SMCDataSource]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(ICursoUnidadeService), nameof(ICursoUnidadeService.BuscarUnidadesSemEntidadePaiSelect), values: new string[] { nameof(RemoveEntidadePai) })]
        public List<SMCDatasourceItem> Unidades { get; set; }

        [SMCDataSource]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(IInstituicaoNivelModalidadeService), nameof(IInstituicaoNivelModalidadeService.BuscarModalidadesPorInstituicaoLogadaSelect), values: new string[] { nameof(SeqInstituicaoEnsino) })]
        public List<SMCDatasourceItem> Modalidades { get; set; }

        [SMCDataSource]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(ICursoOfertaLocalidadeService), nameof(ICursoOfertaLocalidadeService.BuscarEntidadesSuperioresSelect))]
        public List<SMCDatasourceItem> Localidades { get; set; }

        [SMCDataSource]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(IInstituicaoNivelService), nameof(IInstituicaoNivelService.BuscarTipoOrgaoReguladorInstituicao), values: new string[] { nameof(SeqInstituicaoEnsino) })]
        public List<SMCSelectListItem> OrgaoRegulador { get; set; }

        #endregion [ DataSources ]

        [SMCHidden]
        public bool RemoveEntidadePai { get; set; } = true;

        [SMCHidden]
        [SMCStep(0)]
        public bool ApenasEntidadesComCategoriasAtivas { get; set; } = true;

        [SMCHidden]
        [SMCStep(0)]
        public bool ApenasNiveisEnsinoReconhecidosLDB { get; set; } = true;

        [SMCHidden]
        [SMCStep(0)]
        public bool ExibirPrimeiroCursoOfertaLocalidadeAtivo { get; set; } = true;

        [SMCHidden]
        [SMCStep(0)]
        [SMCParameter(FILTER.INSTITUICAO_ENSINO, true)]
        public long SeqInstituicaoEnsino { get; set; }

        /// <summary>
        /// Sequencial do curso quando o curso unidade é acionado por um item da listagem de cursos
        /// </summary>
        [SMCIgnoreProp]
        [SMCParameter]
        public long? SeqEntidade { get; set; }

        [CursoLookup]
        [SMCDependency(nameof(ApenasEntidadesComCategoriasAtivas))]
        [SMCDependency(nameof(ApenasNiveisEnsinoReconhecidosLDB))]
        [SMCParameter(nameof(SeqEntidade))]
        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid12_24)]
        [SMCFilter(true, true)]
        public CursoLookupViewModel Curso { get; set; }

        [SMCHidden]
        [SMCDependency("Curso.Seq")]
        public long? SeqCurso { get; set; }

        [SMCHidden]
        [SMCDependency("Curso.Nome")]
        public string Nome { get; set; }

        [SMCHidden]
        [SMCDependency("Curso.SeqSituacaoAtual")]
        public long? SeqSituacaoAtual { get; set; }

        [SMCHidden]
        [SMCDependency("Curso.SeqNivelEnsino")]
        public long? SeqNivelEnsino { get; set; }

        [CursoOfertaLookup]
        [SMCDependency(nameof(Nome))]
        [SMCDependency(nameof(SeqSituacaoAtual))]
        [SMCDependency(nameof(SeqNivelEnsino))]
        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid12_24)]
        [SMCFilter(true, true)]
        public CursoOfertaLookupViewModel SeqCursoOferta { get; set; }

        [SMCSelect(nameof(Unidades))]
        [SMCSize(SMCSize.Grid10_24, SMCSize.Grid24_24, SMCSize.Grid10_24, SMCSize.Grid10_24)]
        [SMCFilter(true, true)]
        public long? SeqUnidade { get; set; }

        [SMCSelect(nameof(Localidades))]
        [SMCSize(SMCSize.Grid10_24, SMCSize.Grid24_24, SMCSize.Grid10_24, SMCSize.Grid10_24)]
        [SMCFilter(true, true)]
        public long? SeqLocalidade { get; set; }

        [SMCSelect(nameof(Modalidades))]
        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid24_24, SMCSize.Grid4_24, SMCSize.Grid4_24)]
        [SMCFilter(true, true)]
        public long? SeqModalidade { get; set; }

        [SMCSelect(nameof(OrgaoRegulador))]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid6_24)]
        public TipoOrgaoRegulador? SeqTipoOrgaoRegulador { get; set; }

        [SMCSize(SMCSize.Grid5_24, SMCSize.Grid24_24, SMCSize.Grid5_24, SMCSize.Grid5_24)]
        public int? CodigoOrgaoRegulador { get; set; }

        public override void InitializeModel(SMCViewMode viewMode)
        {
            base.InitializeModel(viewMode);

            if (viewMode == SMCViewMode.Filter && SeqCurso == null && Curso != null)
            {
                SeqCurso = Curso.Seq;
            }
        }
    }
}