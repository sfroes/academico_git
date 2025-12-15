using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.GRD.Data
{
    public class DetalheDivisaoTurmaGradeProfessorData : ISMCMappable
    {
        public long SeqPessoaAtuacao { get; set; }

        public string Nome { get; set; }

        public string NomeSocial { get; set; }

        public long? SeqPessoaAtuacaoProfessorSubstituto { get; set; }

        public string NomeProfessorSubstituto { get; set; }

        public string NomeSocialProfessorSubstituto { get; set; }

        public long SeqPessoaAtuacaoOrdenacao { get; set; }

        public string NomeProfessor { get; set; }

        public string Formacao { get; set; }

        public string FormacaoProfessorSubstituto { get; set; }

        public string DescricaoFormacao { get; set; }

        public int? CargaHoraria { get; set; }

        public int? CargaHorariaProfessorSubstituto { get; set; }

        public int? ValorCargaHoraria { get; set; }
    }
}
