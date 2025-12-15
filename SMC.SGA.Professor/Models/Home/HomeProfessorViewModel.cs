using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System.Collections.Generic;

namespace SMC.SGA.Professor.Models
{
    public class HomeProfessorViewModel : SMCViewModelBase
    {
        #region [ DataSources ]

        [SMCDataSource]
        [SMCMapForceFromTo]
        public List<SMCDatasourceItem> CiclosLetivo { get; set; }


        [SMCDataSource]
        [SMCMapForceFromTo]
        public List<SMCDatasourceItem> NiveisEnsino { get; set; }

        [SMCDataSource]
        [SMCMapForceFromTo]
        public List<SMCDatasourceItem> Localidades { get; set; }

        [SMCDataSource]
        [SMCMapForceFromTo]
        public List<SMCDatasourceItem> CursosFiltro { get; set; }

        [SMCDataSource]
        [SMCMapForceFromTo]
        public List<SMCDatasourceItem> Turnos { get; set; }

        #endregion [ DataSources ]

        [SMCFilter(true, true)]
        [SMCSelect(nameof(CiclosLetivo))]
        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid12_24, SMCSize.Grid12_24, SMCSize.Grid12_24)]
        public long? SeqCicloLetivoInicio { get; set; }

        [SMCFilter(true, true, AdvancedFilter = true)]
        [SMCSelect(nameof(NiveisEnsino))]
        [SMCSize(SMCSize.Grid12_24)]
        public long? SeqNivelEnsino { get; set; }

        [SMCFilter(true, true, AdvancedFilter = true)]
        [SMCSelect(nameof(Localidades))]
        [SMCSize(SMCSize.Grid12_24)]
        public long? SeqLocalidade { get; set; }

        [SMCFilter(true, true, AdvancedFilter = true)]
        [SMCSelect(nameof(CursosFiltro))]
        [SMCSize(SMCSize.Grid12_24)]
        public long? SeqCurso { get; set; }

        [SMCFilter(true, true, AdvancedFilter = true)]
        [SMCSelect(nameof(Turnos))]
        [SMCSize(SMCSize.Grid12_24)]
        public long? SeqTurno { get; set; }

        public List<HomeCursoViewModel> Cursos { get; set; }

        #region CPA

        public bool ExibirBannerAvaliacaoSemestralCpa { get; set; }

        public string UrlAvaliacaoSemestralCpa { get; set; }

        #endregion

        #region TESTE_FILE

        [SMCFilter(true, true)]
        [SMCFile(AreaDownload = "", ActionDownload = "DownloadFileGuid", ControllerDownload = "Home", HideDescription = true)]
        public SMCUploadFile Arquivo { get; set; }

        public string LinkUrl { get; set; }

        #endregion
    }
}