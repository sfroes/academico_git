using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.TUR.Data
{
    public class TurmaColaboradorRelatorioData : ISMCMappable
    {
        public long SeqTurma { get; set; }

        public long SeqPessoaColaborador { get; set; }

        public string NomeColaborador { get; set; }

        public string NomeSocialColaborador { get; set; }
    }
}
