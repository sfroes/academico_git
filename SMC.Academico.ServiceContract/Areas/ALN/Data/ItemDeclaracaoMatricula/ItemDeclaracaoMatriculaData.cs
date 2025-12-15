using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.ALN.Data
{
    public class ItemDeclaracaoMatriculaData : ISMCMappable
    {
        public long? SeqPessoaAtuacao { get; set; }
        public string NomeAluno { get; set; }
        public string NomeInstituicaoEnsino { get; set; }
        public string NomeEntidade { get; set; }
        public string NomeCurso { get; set; }
        public int? NumeroCicloLetivo { get; set; }
        public int? AnoCicloLetivo { get; set; }
        public string DescricaoComponenteCurricular { get; set; }
        public string DescricaoRegimeLetivo { get; set; }
        public int? QuantidadeCargaHoraria { get; set; }
        public int? QuantidadeCreditos { get; set; }
        public string NomeProfessor { get; set; }
        public string DataAdmissao { get; set; }
        public string DataPrevisaoConclusao { get; set; }
        public bool Turma { get; set; }
        public string DescricaoTurmaConfiguracaoComponente { get; set; }
        public bool CicloLetivoAtual { get; set; }
        public bool CicloLetivoFuturo { get; set; }
        public string Orientadores { get; set; }
        public bool ContemTurmas { get; set; }
        public bool ContemAtividades { get; set; }
        public long SeqInstituicaoEnsino { get; set; }
        public string DataInicioCicloLetivo { get; set; }
        public string DataFimCicloLetivo { get; set; }
    }
}