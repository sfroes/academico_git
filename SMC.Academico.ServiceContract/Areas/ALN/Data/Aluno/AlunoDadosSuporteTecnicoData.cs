using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.ALN.Data
{
    public class AlunoDadosSuporteTecnicoData : ISMCMappable
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