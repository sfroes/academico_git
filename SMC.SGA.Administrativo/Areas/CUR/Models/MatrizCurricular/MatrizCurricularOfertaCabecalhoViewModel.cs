using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.SGA.Administrativo.Areas.CUR.Models
{
    public class MatrizCurricularOfertaCabecalhoViewModel : SMCViewModelBase
    {
        [SMCKey]
        public long Seq { get; set; }

        public string CurriculoCodigo { get; set; }

        public string CurriculoDescricao { get; set; }

        public string CurriculoDescricaoComplementar { get; set; }

        public string MatrizCodigo { get; set; }

        public string MatrizDescricao { get; set; }

        public string MatrizDescricaoComplementar { get; set; }

        public string Codigo { get; set; }

        public string DescricaoUnidade { get; set; }

        public string DescricaoLocalidade { get; set; }

        public string DescricaoTurno { get; set; }

        public string CurriculoCompleto
        {
            get
            {
                if (string.IsNullOrEmpty(CurriculoDescricaoComplementar))
                    return $"{CurriculoCodigo} - {CurriculoDescricao}";
                else
                    return $"{CurriculoCodigo} - {CurriculoDescricao} - {CurriculoDescricaoComplementar}";
            }
        }

        public string MatrizCompleto
        {
            get
            {
                if (string.IsNullOrEmpty(MatrizDescricaoComplementar))
                    return $"{MatrizCodigo} - {MatrizDescricao}";
                else
                    return $"{MatrizCodigo} - {MatrizDescricao} - {MatrizDescricaoComplementar}";
            }
        }

        public string OfertaCompleto
        {
            get
            {
                return $"{Codigo} - {DescricaoUnidade} / {DescricaoLocalidade} - {DescricaoTurno}";
            }
        }
    }
}