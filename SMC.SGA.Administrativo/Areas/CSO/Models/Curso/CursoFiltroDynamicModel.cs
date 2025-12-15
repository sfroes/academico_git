using SMC.Academico.ServiceContract.Areas.CSO.Interfaces;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Framework;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.CSO.Models
{
    public class CursoFiltroDynamicModel : SMCDynamicFilterViewModel
    {
        #region [ DataSources ]

        [SMCDataSource]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(ICursoService), nameof(ICursoService.BuscarHierarquiaSuperiorCursoSelect))]
        public List<SMCDatasourceItem> EntidadesSuperior { get; set; }

        [SMCDataSource]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(IInstituicaoNivelService), nameof(IInstituicaoNivelService.BuscarNiveisEnsinoReconhecidoLDBSelect))]
        public List<SMCDatasourceItem> NiveisEnsino { get; set; }

        [SMCDataSource]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(ICursoService), nameof(ICursoService.BuscarSituacoesCursoSelect))]
        public List<SMCDatasourceItem> Situacoes { get; set; }

        #endregion [ DataSources ]

        [SMCHidden]
        public long SeqTipoEntidade { get; set; }

        [SMCHidden]
        public bool ExibirPrimeiroCursoOfertasAtivas { get { return true; } }

        /// <summary>
        /// Filtro informado quando é chamado por um item na lista de programas
        /// </summary>
        [SMCHidden]
        [SMCParameter]
        public long? SeqPrograma { get; set; }

        [SMCOrder(0)]
        [SMCFilter(true, true)]
        [SMCSize(SMCSize.Grid2_24, SMCSize.Grid24_24, SMCSize.Grid4_24, SMCSize.Grid2_24)]
        public long? Seq { get; set; }

        [SMCOrder(1)]
        [SMCFilter(true, true)]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid11_24, SMCSize.Grid6_24)]
        [SMCSelect(nameof(EntidadesSuperior), SortBy = SMCSortBy.Description)]
        [SMCParameter]
        public List<long> SeqEntidade { get; set; }

        [SMCFilter(true, true)]
        [SMCMaxLength(100)]
        [SMCOrder(2)]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid9_24, SMCSize.Grid6_24)]
        [SMCSortable(true, true)]
        public string Nome { get; set; }

        [SMCOrder(3)]
        [SMCFilter(true, true)]
        [SMCSelect(nameof(NiveisEnsino), SortBy = SMCSortBy.Description)]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid6_24)]
        public List<long> SeqNivelEnsino { get; set; }

        [SMCOrder(4)]
        [SMCFilter(true, true)]
        [SMCSelect(nameof(Situacoes), SortBy = SMCSortBy.Description)]
        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid4_24)]
        public long? SeqSituacaoAtual { get; set; }

        public override void InitializeModel(SMCViewMode viewMode)
        {
            base.InitializeModel(viewMode);

            if (viewMode == SMCViewMode.Filter && SeqEntidade.SMCCount() == 0 && SeqPrograma.HasValue)
            {
                SeqEntidade = new List<long>();
                SeqEntidade.Add(SeqPrograma.Value);
            }
        }
    }
}