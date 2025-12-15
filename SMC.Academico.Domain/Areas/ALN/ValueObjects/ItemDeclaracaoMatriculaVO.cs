using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.ALN.ValueObjects
{
    public class ItemDeclaracaoMatriculaVO : ISMCMappable
    {
        public long? SeqPessoaAtuacao { get; set; }
        public string NomeAluno { get; set; }
        public string NomeInstituicaoEnsino { get; set; }
        public string NomeEntidade { get; set; }
        public string NomeCurso { get; set; }
        public short? NumeroCicloLetivo { get; set; }
        public short? AnoCicloLetivo { get; set; }
        public string DescricaoComponenteCurricular { get; set; }
        public string DescricaoRegimeLetivo { get; set; }
        public short? QuantidadeCargaHoraria { get; set; }
        public short? QuantidadeCreditos { get; set; }
        public string NomeProfessor { get; set; }
        public string DataAdmissao { get; set; }
        public string DataPrevisaoConclusao { get; set; }
        public bool Turma { get; set; }
        public string DescricaoTurmaConfiguracaoComponente { get; set; }
        public bool CicloLetivoAtual { get; set; }
        public bool CicloLetivoFuturo { get; set; }
        public long SeqTipoComponenteCurricular { get; set; }
        public string Orientadores { get; set; }
        public bool ContemTurmas { get; set; }
        public bool ContemAtividades { get; set; }
        public long SeqInstituicaoEnsino { get; set; }
        public string DataInicioCicloLetivo { get; set; }
        public string DataFimCicloLetivo { get; set; }
    }
}