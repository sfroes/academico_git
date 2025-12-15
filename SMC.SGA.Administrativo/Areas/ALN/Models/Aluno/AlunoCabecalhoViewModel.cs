using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System;

namespace SMC.SGA.Administrativo.Areas.ALN.Models
{
    public class AlunoCabecalhoViewModel : SMCViewModelBase
    {
        public long NumeroRegistroAcademico { get; set; }

        public string Nome { get; set; }

        public bool Falecido { get; set; }

        public string DescricaoVinculo { get; set; }

        public string NomeEntidadeResponsavel { get; set; }

        public DateTime DataAdmissao { get; set; }

        public string DescricaoSituacaoMatricula { get; set; }

        public string DescricaoFormaIngresso { get; set; }

        public string TipoVinculoAluno { get; set; }

        public string DescricaoNivelEsino { get; set; }

        [SMCValueEmpty("-")]
        public DateTime? DataInicioTermoIntercambio { get; set; }

        [SMCValueEmpty("-")]
        public DateTime? DataFimTermoIntercambio { get; set; }

        public string DescricaoInstituicaoExterna { get; set; }

        public bool ExigePeriodoIntercambioTermo { get; set; }

        public bool ExigeParceriaIntercambioIngresso { get; set; }
        public bool ConcedeFormacao { get; set; }

        [SMCValueEmpty("-")]
        public int? CodigoAlunoMigracao { get; set; }

        [SMCValueEmpty("-")]
        public DateTime? DataPrevisaoConclusao { get; set; }

        [SMCValueEmpty("-")]
        public DateTime? DataLimiteConclusao { get; set; }
    }
}