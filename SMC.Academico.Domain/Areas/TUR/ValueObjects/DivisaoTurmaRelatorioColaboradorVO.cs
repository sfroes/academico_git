using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.TUR.ValueObjects
{
    public class DivisaoTurmaRelatorioColaboradorVO : ISMCMappable
    {
        public long SeqDivisaoTurma { get; set; }

        public long SeqPessoaAtuacaoColaborador { get; set; }

        public string NomeColaborador { get; set; }

        public string NomeSocialColaborador { get; set; }
    }
}
