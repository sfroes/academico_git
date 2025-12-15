using SMC.Framework.UI.Mvc;

namespace SMC.SGA.Administrativo.Areas.CUR.Models
{
    public class MatrizCurricularCabecalhoViewModel : SMCViewModelBase
    {
        #region [ Curso ]

        public string CodigoCurso { get; set; }

        public string DescricaoCurso { get; set; }

        #endregion [ Curso ]

        #region [ Currículo ]

        public string CodigoCurriculo { get; set; }

        public string DescricaoCurriculo { get; set; }

        public string DescricaoComplementarCurriculo { get; set; }

        public bool SituacaoCurriculo { get; set; }

        #endregion [ Currículo ]

        #region [ Ofeta de Curso ]

        public string CodigoOfertaCurso { get; set; }

        public string DescricaoOfertaCurso { get; set; }

        #endregion [ Ofeta de Curso ]

        #region [ Matriz Curricular ]

        public string CodigoMatrizCurricular { get; set; }

        public string DescricaoMatrizCurricular { get; set; }

        public string DescricaoComplementarMatrizCurricular { get; set; }

        #endregion [ Matriz Curricular ]

        #region [ Campos Formatados ]

        public string DescricaoCursoFormatada
        {
            get
            {
                if (string.IsNullOrEmpty(CodigoCurso))
                    return $"{DescricaoCurso}";
                else
                    return $"{CodigoCurso} - {DescricaoCurso}";
            }
        }

        public string DescricaoOfertaCursoFormatada
        {
            get
            {
                if (string.IsNullOrEmpty(CodigoOfertaCurso))
                    return $"{DescricaoOfertaCurso}";
                else
                    return $"{CodigoOfertaCurso} - {DescricaoOfertaCurso}";
            }
        }

        public string DescricaoCurriculoFormatada
        {
            get
            {
                if (string.IsNullOrEmpty(DescricaoComplementarCurriculo))
                    return $"{CodigoCurriculo} - {DescricaoCurriculo}";
                else
                    return $"{CodigoCurriculo} - {DescricaoCurriculo} - {DescricaoComplementarCurriculo}";
            }
        }

        public string DescricaoMatrizFormatada
        {
            get
            {
                if (string.IsNullOrEmpty(DescricaoComplementarMatrizCurricular))
                    return $"{DescricaoMatrizCurricular}";
                else
                    return $"{DescricaoMatrizCurricular} - {DescricaoComplementarMatrizCurricular}";
            }
        }

        #endregion [ Campos Formatados ]
    }
}