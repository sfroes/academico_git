using System;
using System.Collections.Generic;
using SMC.Academico.ServiceContract.Areas.ALN.Interfaces;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Academico.UI.Mvc.Areas.CAM.Lookups;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.SGA.Administrativo.Areas.ALN.Controllers;

namespace SMC.SGA.Administrativo.Areas.ALN.Models
{
    public class RelatorioPrevisaoConclusaoOrientacaoFiltroViewModel : SMCPagerViewModel, ISMCMappable
    {
        #region [ DataSources ]

        [SMCDataSource]
        [SMCServiceReference(typeof(IEntidadeService), nameof(IEntidadeService.BuscarEntidadesResponsaveisVisaoOrganizacionalSelect))]
        public List<SMCDatasourceItem> EntidadesResponsaveis { get; set; }

        [SMCDataSource]
        [SMCServiceReference(typeof(IInstituicaoNivelService), nameof(IInstituicaoNivelService.BuscarNiveisEnsinoReconhecidoLDBSelect))]
        public List<SMCDatasourceItem> NiveisEnsino { get; set; }

        [SMCDataSource]
        [SMCServiceReference(typeof(ITipoVinculoAlunoService), nameof(ITipoVinculoAlunoService.BuscarTipoVinculoAlunoNaInstituicaoSelect))]
        public List<SMCDatasourceItem> TiposVinculoAluno { get; set; }

        #endregion [ DataSources ]

        [SMCRequired]
        [CicloLetivoLookup]
        [SMCSize(SMCSize.Grid5_24, SMCSize.Grid24_24, SMCSize.Grid5_24, SMCSize.Grid5_24)]
        public CicloLetivoLookupViewModel SeqCicloLetivoIngresso { get; set; }

        [SMCFilter(true, true)]
        [SMCSelect(nameof(EntidadesResponsaveis))]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid12_24, SMCSize.Grid6_24, SMCSize.Grid6_24)]
        public List<long> SeqsEntidadesResponsaveis { get; set; }

        [SMCFilter(true, true)]
        [SMCSelect(nameof(NiveisEnsino))]
        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid24_24, SMCSize.Grid4_24, SMCSize.Grid4_24)]
        public long? SeqNivelEnsino { get; set; }

        [SMCDependency(nameof(SeqNivelEnsino), nameof(RelatorioPrevisaoConclusaoOrientacaoController.BuscarTipoVinculoAluno), "RelatorioPrevisaoConclusaoOrientacao", false)]
        [SMCFilter(true, true)]
        [SMCSelect(nameof(TiposVinculoAluno))]
        [SMCSize(SMCSize.Grid5_24, SMCSize.Grid24_24, SMCSize.Grid5_24, SMCSize.Grid5_24)]
        public List<long?> SeqsTipoVinculoAluno { get; set; }

        [SMCSize(SMCSize.Grid4_24)]
        public DateTime? PrazoEncerrado { get; set; }
    }
}