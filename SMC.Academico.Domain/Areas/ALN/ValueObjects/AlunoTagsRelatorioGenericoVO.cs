using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.Framework.Mapper;
using System;

namespace SMC.Academico.Domain.Areas.ALN.ValueObjects
{
    public class AlunoTagsRelatorioGenericoVO : ISMCMappable
    {
        public long SeqDadosPessoais { get; set; }
        public long SeqPessoa { get; set; }
        public bool ExibirNomeSocial { get; set; }
        public string NomeCivil { get; set; }
        public string NomeSocial { get; set; }
        public string NomeAlunoOficial { get; set; }
        public int? CodigoAlunoMigracao { get; set; }
        public string Cpf { get; set; }
        public string NomeCidade { get; set; }
        public string NomeCidadeEntidadeVinculo { get; set; }
        public short NumCicloLetivo { get; set; }
        public short AnoCicloLetivo { get; set; }
        public string NomeCurso { get; set; }
        public short? SemestrePrevisaoFormatura { get; set; }
        public short? AnoPrevisaoFormatura { get; set; }
        public string Data { get; set; }
        public string Turno { get; set; }
        public Sexo Sexo { get; set; }
        public DateTime DataAdmissao { get; set; }
        public DateTime? DataPrevisaoConclusao { get; set; }
        public DateTime? DataLimiteConclusao { get; set; }
        public string GrauAcademico { get; set; }
        public string FormaIngresso { get; set; }
        public string Local { get; set; }
        public string Unidade { get; set; }
        public int? CodigoUnidadeSeo { get; set; }
        public string SituacaoEnade { get; set; }
        public string SituacaoMatricula { get; set; }
        public short? ConclusaoAno { get; set; }
        public short? ConclusaoSemestre { get; set; }
        public short IngressoAno { get; set; }
        public short IngressoSemestre { get; set; }
        public string AreaConhecimento { get; set; }
        public int? CodigoCursoOfertaLocalidade { get; set; }
        public int SemestresMatriculados { get; set; }
        public int Perioodo { get; set; }
        public long SeqNivelEnsino { get; set; }
    }
}
