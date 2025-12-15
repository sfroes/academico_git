using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.TUR.ValueObjects
{
    public class TurmaColaboradorRelatorioVO : ISMCMappable
    {
        public long SeqTurma { get; set; }

        public long SeqPessoaColaborador { get; set; }

        public string NomeColaborador { get; set; }

        public string NomeSocialColaborador { get; set; }
    }   
}
