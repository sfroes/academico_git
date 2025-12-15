using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.CUR.Data
{
    public class RequisitoListaData : ISMCMappable
    {
        public long Seq { get; set; }

        public long? SeqMatrizCurricular { get; set; }

        public long? SeqCurriculoCursoOferta { get; set; }

        public long? SeqComponenteCurricular { get; set; }

        public string DescricaoComponenteCurricular { get; set; }

        public long? SeqDivisaoCurricularItem { get; set; }

        public string DescricaoDivisaoCurricularItem { get; set; }

        public bool UnicaMatrizAssociada { get; set; }

        public TipoRequisitoAssociado Associado { get; set; }

        public List<RequisitoItemData> Itens { get; set; }

        public List<MatrizCurricularCabecalhoData> MatrizesCurriculares { get; set; }
    }
}