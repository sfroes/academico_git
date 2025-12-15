using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.Academico.UI.Mvc.Areas.CUR.Lookups
{
    public class GrupoCurricularLookupFiltroViewModel : SMCLookupFilterViewModel
    {
        [SMCKey]
        [SMCHidden]
        public long SeqCurriculoCursoOferta { get; set; }

        [SMCHidden]
        public bool? DesconsiderarItensQueNaoPermitemCadastroDispensa { get; set; }

        [SMCHidden]
        public bool? DesconsiderarItensCursadosAprovacaoOuDispensadosAluno { get; set; }

        [SMCHidden]
        public bool? DesconsiderarItensVinculadosAoCurriculoCursoOferta { get; set; }

        [SMCHidden]
        public bool? DesconsiderarGruposTodosItensObrigatorios { get; set; }

        [SMCHidden]
        public long? SeqPessoaAtuacao { get; set; }

        [SMCHidden]
        public bool FiltrarFormacoesEspecificasAluno { get; set; }

        [SMCHidden]
        public bool PermitirSelecionarGruposComComponentes { get; set; }
    }
}