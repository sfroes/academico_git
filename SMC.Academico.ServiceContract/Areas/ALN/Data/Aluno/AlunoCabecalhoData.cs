using SMC.Framework.Mapper;
using System;

namespace SMC.Academico.ServiceContract.Areas.ALN.Data
{
    public class AlunoCabecalhoData : ISMCMappable
    {
        public long NumeroRegistroAcademico { get; set; }

        public string Nome { get; set; }

        public bool Falecido { get; set; }

        public string DescricaoVinculo { get; set; }

        public string DescricaoFormaIngresso { get; set; }

        public string NomeEntidadeResponsavel { get; set; }

        public DateTime DataAdmissao { get; set; }

        public string DescricaoSituacaoMatricula { get; set; }

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