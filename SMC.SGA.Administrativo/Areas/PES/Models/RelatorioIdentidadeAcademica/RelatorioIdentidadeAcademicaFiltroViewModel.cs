using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Html;

namespace SMC.SGA.Administrativo.Areas.PES.Models
{
    public class RelatorioIdentidadeAcademicaFiltroViewModel : SMCViewModelBase
    {
        public bool CriaVinculoInstitucional => true;

        public bool VinculoAtivo => true;

        //FIX: Remover min ao corrigir falha do binder com mestre detalhe inicializado
        [SMCDetail(min: 1)]
        public SMCMasterDetailList<RelatorioIdentidadeAcademicaAlunoDetailViewModel> Alunos { get; set; }

        [SMCDetail(min: 1)]
        public SMCMasterDetailList<RelatorioIdentidadeAcademicaColaboradorDetailViewModel> Colaboradores { get; set; }
    }
}