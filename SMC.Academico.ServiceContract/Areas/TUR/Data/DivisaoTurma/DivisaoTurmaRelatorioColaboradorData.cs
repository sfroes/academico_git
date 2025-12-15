using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.TUR.Data
{
    public class DivisaoTurmaRelatorioColaboradorData : ISMCMappable
    {
        public long SeqDivisaoTurma { get; set; }

        public long SeqPessoaAtuacaoColaborador { get; set; }

        public string NomeColaborador { get; set; }

        public string NomeSocialColaborador { get; set; }
    }
}
