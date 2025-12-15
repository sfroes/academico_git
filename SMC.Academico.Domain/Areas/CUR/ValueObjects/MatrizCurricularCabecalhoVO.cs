using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.CUR.ValueObjects
{
    public class MatrizCurricularCabecalhoVO : ISMCMappable
    {
        #region [ Curso ]

        public long SeqCurso { get; set; }

        public string CodigoCurso { get { return string.Format("{0:d4}", SeqCurso); } }

        public string DescricaoCurso { get; set; }

        #endregion [ Curso ]

        #region [ Currículo ]        

        public string CodigoCurriculo { get; set; }

        public string DescricaoCurriculo { get; set; }

        public string DescricaoComplementarCurriculo { get; set; }

        public bool SituacaoCurriculo { get; set; }

        #endregion [ Currículo ]

        #region [ Ofeta de Curso ]

        public long SeqCursoOferta { get; set; }

        public string CodigoOfertaCurso { get { return string.Format("{0:d4}", SeqCursoOferta); } }

        public string DescricaoOfertaCurso { get; set; }

        #endregion [ Ofeta de Curso ]

        #region [ Matriz Curricular ]

        public long SeqMatrizCurricular { get; set; }

        public string CodigoMatrizCurricular { get; set; }

        public string DescricaoMatrizCurricular { get; set; }

        public string DescricaoComplementarMatrizCurricular { get; set; }

        public string UnidadeMatrizCurricular { get; set; }

        public string LocalidadeMatrizCurricular { get; set; }

        public string TurnoMatrizCurricular { get; set; }

        #endregion [ Matriz Curricular ]
    }
}