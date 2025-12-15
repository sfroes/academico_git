using SMC.Academico.Common.Areas.ALN.Enums;
using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Academico.UI.Mvc.Areas.CAM.Lookups;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.ALN.Models
{
    public class RelatorioListarViewModel : SMCViewModelBase, ISMCMappable
    {
        [SMCHidden]
        public TipoRelatorio TipoRelatorio { get; set; }

        [SMCHidden]
        public bool? ImprimirComponenteCurricularSemCreditos { get; set; }

        [SMCHidden]
        public bool? ExibirMediaNotas { get; set; }

        [SMCHidden]
        public bool? ExibeProfessor { get; set; }

        [SMCHidden]
        public TipoDeclaracaoDisciplinaCursada? ExibirNaDeclaracao { get; set; }

        [SMCHidden]
        public bool? ExibirEmentasComponentesCurriculares { get; set; }

        [SMCHidden]
        public List<long> SelectedValues { get; set; }

        public SMCPagerModel<RelatorioListarItemViewModel> Alunos { get; set; }

        [SMCHidden]
        public CicloLetivoLookupViewModel SeqCicloLetivo { get; set; }

        [SMCHidden]
        public string NomeUsuarioLogado { get; set; }

        [SMCHidden]
        public string TituloListagem { get; set; }

        [SMCHidden]
        public bool? ExibirCampoAssinatura { get; set; }

        [SMCHidden]
        public long? SeqNivelEnsinoPorGrupoDocumentoAcademico { get; set; }

        [SMCHidden]
        public long? SeqTipoDocumentoAcademico { get; set; }

        [SMCHidden]
        public SMCLanguage? IdiomaDocumentoAcademico { get; set; }
    }
}