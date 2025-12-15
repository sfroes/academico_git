using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.SGA.Administrativo.Areas.GRD.Models
{
    public class DetalheDivisaoTurmaGradeProfessorViewModel : SMCViewModelBase
    {
        [SMCHidden]
        public long SeqPessoaAtuacao { get; set; }

        [SMCHidden]
        public string Nome { get; set; }

        [SMCHidden]
        public string NomeSocial { get; set; }

        [SMCHidden]
        public long? SeqPessoaAtuacaoProfessorSubstituto { get; set; }

        [SMCHidden]
        public string NomeProfessorSubstituto { get; set; }

        [SMCHidden]
        public string NomeSocialProfessorSubstituto { get; set; }

        [SMCHidden]
        public long SeqPessoaAtuacaoOrdenacao { get; set; }

        public string NomeProfessor { get; set; }

        [SMCHidden]
        public string Formacao { get; set; }

        [SMCHidden]
        public string FormacaoProfessorSubstituto { get; set; }

        [SMCValueEmpty("-")]
        public string DescricaoFormacao { get; set; }

        [SMCHidden]
        public int? CargaHoraria { get; set; }

        [SMCHidden]
        public int? CargaHorariaProfessorSubstituto { get; set; }

        [SMCValueEmpty("-")]
        public int? ValorCargaHoraria { get; set; }
    }
}