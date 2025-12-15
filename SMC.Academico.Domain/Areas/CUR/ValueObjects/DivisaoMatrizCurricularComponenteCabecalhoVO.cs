using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.CUR.ValueObjects
{
    public class DivisaoMatrizCurricularComponenteCabecalhoVO : ISMCMappable
    {
        #region [ MatrizCurricular ]

        [SMCMapProperty("Codigo")]
        public string CodigoMatriz { get; set; }
        [SMCMapProperty("Descricao")]
        public string DescricaoMatriz { get; set; }
        [SMCMapProperty("DescricaoComplementar")]
        public string DescricaoMatrizComplementar { get; set; }

        #endregion

        #region [ GrupoCurricularComponente ]

        [SMCMapProperty("ComponenteCurricular.Codigo")]
        public string CodigoComponente { get; set; }
        [SMCMapProperty("ComponenteCurricular.Descricao")]
        public string DescricaoComponente { get; set; }
        [SMCMapProperty("ComponenteCurricular.CargaHoraria")]
        public short? CargaHorariaComponente { get; set; }
        [SMCMapProperty("ComponenteCurricular.Creditos")]
        public short? CreditosComponente { get; set; }

        #endregion

        public string Formato { get; set; }
        public List<string> GruposPertecentes { get; set; }
    }
}
