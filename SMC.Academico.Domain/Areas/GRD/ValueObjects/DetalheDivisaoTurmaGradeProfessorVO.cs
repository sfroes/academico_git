using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.GRD.ValueObjects
{
    public class DetalheDivisaoTurmaGradeProfessorVO : ISMCMappable
    {
        public long SeqPessoaAtuacao { get; set; }

        public string Nome { get; set; }

        public string NomeSocial { get; set; }

        public long? SeqPessoaAtuacaoProfessorSubstituto { get; set; }

        public string NomeProfessorSubstituto { get; set; }

        public string NomeSocialProfessorSubstituto { get; set; }

        public long SeqPessoaAtuacaoOrdenacao
        {
            get
            {
                return SeqPessoaAtuacaoProfessorSubstituto.HasValue ? SeqPessoaAtuacaoProfessorSubstituto.Value : SeqPessoaAtuacao;
            }
        }

        public string NomeProfessor
        {
            get
            {
                var nomeCompleto = string.Empty;

                if (SeqPessoaAtuacaoProfessorSubstituto.HasValue)
                {
                    if (!string.IsNullOrEmpty(NomeSocialProfessorSubstituto))
                    {
                        if (!string.IsNullOrEmpty(NomeProfessorSubstituto))
                            nomeCompleto += $"{SeqPessoaAtuacaoProfessorSubstituto} - {NomeSocialProfessorSubstituto} ({NomeProfessorSubstituto})";
                        else
                            nomeCompleto += $"{SeqPessoaAtuacaoProfessorSubstituto} - {NomeSocialProfessorSubstituto}";
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(NomeProfessorSubstituto))
                            nomeCompleto += $"{SeqPessoaAtuacaoProfessorSubstituto} - {NomeProfessorSubstituto}";
                    }

                    nomeCompleto += $" (substituto(a) de ";
                }

                if (!string.IsNullOrEmpty(NomeSocial))
                {
                    if (!string.IsNullOrEmpty(Nome))
                        nomeCompleto += $"{SeqPessoaAtuacao} - {NomeSocial} ({Nome})";
                    else
                        nomeCompleto += $"{SeqPessoaAtuacao} - {NomeSocial}";
                }
                else
                {
                    if (!string.IsNullOrEmpty(Nome))
                        nomeCompleto += $"{SeqPessoaAtuacao} - {Nome}";
                }

                if (SeqPessoaAtuacaoProfessorSubstituto.HasValue)
                {
                    nomeCompleto += ")";
                }

                return nomeCompleto;
            }
        }

        public string Formacao { get; set; }

        public string FormacaoProfessorSubstituto { get; set; }

        public string DescricaoFormacao
        {
            get
            {
                return SeqPessoaAtuacaoProfessorSubstituto.HasValue ? FormacaoProfessorSubstituto : Formacao;
            }
        }

        public int? CargaHoraria { get; set; }

        public int? CargaHorariaProfessorSubstituto { get; set; }

        public int? ValorCargaHoraria
        {
            get
            {
                return SeqPessoaAtuacaoProfessorSubstituto.HasValue ? CargaHorariaProfessorSubstituto.Value : CargaHoraria;
            }
        }
    }
}
