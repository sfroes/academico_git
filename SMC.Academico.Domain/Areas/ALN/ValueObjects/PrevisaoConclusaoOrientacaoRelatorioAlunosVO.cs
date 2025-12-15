using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.ALN.ValueObjects
{
    public class PrevisaoConclusaoOrientacaoRelatorioAlunosVO : ISMCMappable
    {
        public long SeqEntidade { get; set; }
        public long SeqAluno { get; set; }
        public string Nome { get; set; }
        public string DescricaoVinculo { get; set; }
        public string DescricaoNivel { get; set; }
        public string DescricaoSituacaoMatricula { get; set; }
        public string DataAdmissao { get; set; }
        public string DataPrevisaoConclusao { get; set; }
        public string DataLimiteConclusao { get; set; }
        public string NomeColaborador { get; set; }
        public string DataInicioOrientacao { get; set; }
        public string DataFimOrientacao { get; set; }
    }
}
