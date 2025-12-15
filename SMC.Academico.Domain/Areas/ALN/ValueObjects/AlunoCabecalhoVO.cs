using SMC.Framework.Mapper;
using System;

namespace SMC.Academico.Domain.Areas.ALN.ValueObjects
{
    public class AlunoCabecalhoVO : ISMCMappable
    {
        public long SeqNivelEnsino { get; set; }

        public long SeqTipoVinculoAluno { get; set; }
        public long SeqInstituicaoEnsino { get; set; }

        public long? SeqTipoTermoIntercambio { get; set; }

        public long NumeroRegistroAcademico { get; set; }

        public string Nome { get; set; }

        public string NomeSocial { get; set; }

        public bool Falecido { get; set; }

        public string DescricaoVinculo { get; set; }

        public string DescricaoFormaIngresso { get; set; }

        public string NomeEntidadeResponsavel { get; set; }

        public DateTime DataAdmissao { get; set; }

        public string DescricaoSituacaoMatricula { get; set; }

        public string DescricaoTipoTermoIntercambio { get; set; }

        public string TipoVinculoAluno { get; set; }

        public string DescricaoNivelEsino { get; set; }

        public DateTime? DataInicioTermoIntercambio { get; set; }

        public DateTime? DataFimTermoIntercambio { get; set; }

        public string DescricaoInstituicaoExterna { get; set; }

        public bool ExigePeriodoIntercambioTermo { get; set; }

        public bool ExigeParceriaIntercambioIngresso { get; set; }
        public bool ConcedeFormacao { get; set; }

        public int? CodigoAlunoMigracao { get; set; }

        public DateTime? DataPrevisaoConclusao { get; set; }

        public DateTime? DataLimiteConclusao { get; set; }
    }
}