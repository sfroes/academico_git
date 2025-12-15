using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.CUR.ValueObjects
{
    public class GrupoCurricularCabecalhoVO: ISMCMappable
    {
        public string CodigoCurriculo { get; set; }

        public string DescricaoCurriculo { get; set; }

        public string SiglaCurso { get; set; }

        public string NomeCurso { get; set; }

        public bool CurriculoAtivo { get; set; }
    }
}
