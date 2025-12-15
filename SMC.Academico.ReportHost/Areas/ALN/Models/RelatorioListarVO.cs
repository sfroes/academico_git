using SMC.Academico.Common.Areas.ALN.Enums;
using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Academico.UI.Mvc.Areas.CAM.Lookups;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc;
using System.Collections.Generic;

namespace SMC.Academico.ReportHost.Areas.ALN.Models
{
    public class RelatorioListarVO : SMCViewModelBase, ISMCMappable
    {
        public List<long> SelectedValues { get; set; }

        public CicloLetivoLookupViewModel SeqCicloLetivo { get; set; }

        public string NomeUsuarioLogado { get; set; }

        public long SeqInstituicaoEnsino { get; set; }

        public TipoRelatorio TipoRelatorio { get; set; }

        public bool? ImprimirComponenteCurricularSemCreditos { get; set; }

        public bool? ExibirMediaNotas { get; set; }

        public bool? ExibeProfessor { get; set; }

        public string TituloListagem { get; set; }

        public bool? ExibirCampoAssinatura { get; set; }

        public TipoDeclaracaoDisciplinaCursada? ExibirNaDeclaracao { get; set; }

        public bool? ExibirEmentasComponentesCurriculares { get; set; }

        public long? SeqNivelEnsinoPorGrupoDocumentoAcademico { get; set; }

        public long? SeqTipoDocumentoAcademico { get; set; }

        public SMCLanguage? IdiomaDocumentoAcademico { get; set; }
    }
}