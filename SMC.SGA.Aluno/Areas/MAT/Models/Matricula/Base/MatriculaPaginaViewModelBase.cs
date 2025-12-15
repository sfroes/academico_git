using SMC.Academico.UI.Mvc.Areas.MAT.Models;
using SMC.Formularios.UI.Mvc.Model;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System.Collections.Generic;

namespace SMC.SGA.Aluno.Areas.MAT.Models.Matricula
{
    public class MatriculaPaginaViewModelBase : TemplatePaginaViewModel
    {
        [SMCHidden]
        public long SeqSolicitacaoHistoricoNavegacao { get; set; }

        [SMCHidden]
        public long SeqSolicitacaoMatricula { get; set; }

        [SMCHidden]
        public virtual string ActionSalvarEtapa { get; set; }

        [SMCHidden]
        public bool EtapaFinalizada { get; set; }

        [SMCHidden]
        public long SeqSolicitacaoServicoEtapa { get; set; }

        [SMCHidden]
        public long SeqPessoaAtuacao { get; set; }
        
        [SMCHidden]
        public bool ExibeEntidadeResponsavel { get; set; }

        [SMCHidden]
        public bool ExibeNivelEnsino { get; set; }

        [SMCHidden]
        public bool ExibeVinculo { get; set; }

        [SMCHidden]
        public bool ExibeDescricaoCurso { get; set; }

        public List<PessoaAtuacaoBloqueioViewModel> Bloqueios { get; set; }

        #region Menu das páginas

        public string DescricaoProcesso { get; set; }

        public string DescricaoUnidadeResponsavel { get; set; }

        public string DescricaoNivelEnsino { get; set; }

        public string DescricaoVinculo { get; set; }

        public string DescricaoEtapa { get; set; }

        public string DescricaoCurso { get; set; }

        [SMCHidden]
        public long? SeqSituacaoFinalSucesso { get; set; }

        [SMCHidden]
        public long? SeqSituacaoFinalSemSucesso { get; set; }

        [SMCHidden]
        public long? SeqSituacaoFinalCancelada { get; set; }

        #endregion Menu das páginas
    }
}