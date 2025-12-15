using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.ALN.ValueObjects
{
    public class AlunoDadosSuporteTecnicoVO : ISMCMappable
    {
        public long NumeroRegistroAcademico { get; set; }

        public string DescricaoPessoaAtuacao { get; set; }

        public string NomeCurso { get; set; }

        public string DescricaoTurno { get; set; }

        public string DescricaoUnidade { get; set; }

        public int? CodigoUnidadeSeoLocalidade { get; set; }

        public int? CodigoOrigemFinanceira { get; set; }
    }
}