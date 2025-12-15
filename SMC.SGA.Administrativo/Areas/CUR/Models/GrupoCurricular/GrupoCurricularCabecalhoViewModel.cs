using SMC.Framework.Mapper;

namespace SMC.SGA.Administrativo.Areas.CUR.Models
{
    public class GrupoCurricularCabecalhoViewModel : ISMCMappable
    {
        public string CodigoCurriculo { get; set; }

        public string DescricaoCurriculo { get; set; }

        public string DescricaoCurriculoFormatada
        {
            get
            {
                if (string.IsNullOrEmpty(CodigoCurriculo))
                    return $"{DescricaoCurriculo}";
                else
                    return $"{CodigoCurriculo} - {DescricaoCurriculo}";
            }
        }

        public string SiglaCurso { get; set; }

        public string NomeCurso { get; set; }

        public string DescricaoCursoFormatada
        {
            get
            {
                if (string.IsNullOrEmpty(SiglaCurso))
                    return $"{NomeCurso}";
                else
                    return $"{SiglaCurso} - {NomeCurso}";
            }
        }

        public bool CurriculoAtivo { get; set; }
    }
}