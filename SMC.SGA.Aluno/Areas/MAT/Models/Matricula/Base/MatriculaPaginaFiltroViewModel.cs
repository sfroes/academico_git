using SMC.Academico.UI.Mvc.Areas.SRC.Models;
using SMC.Academico.UI.Mvc.Models;
using SMC.Framework.UI.Mvc;

namespace SMC.SGA.Aluno.Areas.MAT.Models.Matricula
{
    public class MatriculaPaginaFiltroViewModel : SMCViewModelBase
    {
        public bool RedirecionarHome { get; set; }

        public SMCEncryptedLong SeqConfiguracaoEtapa { get; set; }

        public SMCEncryptedLong SeqConfiguracaoEtapaPagina { get; set; }

        public SMCEncryptedLong SeqIngressante { get; set; }

        public SMCEncryptedLong SeqSolicitacaoMatricula { get; set; }

        public long SeqSolicitacaoServicoEtapa { get; set; }

        public ConfiguracaoEtapaPaginaViewModel ConfiguracaoEtapaPagina { get; set; }

        public ConfiguracaoEtapaViewModel ConfiguracaoEtapa { get; set; }

        public bool EtapaFinalizada { get; set; }

        public SMCEncryptedLong SeqProcesso { get; set; }

        public SMCEncryptedLong SeqEntidadeResponsavel { get; set; }

        public string DescricaoEtapa { get; set; }

        public string BackUrl { get; set; }
    }
}