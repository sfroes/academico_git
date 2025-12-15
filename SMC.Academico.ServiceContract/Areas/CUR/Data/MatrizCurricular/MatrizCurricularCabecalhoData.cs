using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.CUR.Data
{
    public class MatrizCurricularCabecalhoData : ISMCMappable
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
    }
}