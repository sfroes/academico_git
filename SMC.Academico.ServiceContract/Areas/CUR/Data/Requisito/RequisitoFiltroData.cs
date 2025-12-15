using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Framework.Mapper;
using SMC.Framework.Model;

namespace SMC.Academico.ServiceContract.Areas.CUR.Data
{
    public class RequisitoFiltroData : SMCPagerFilterData, ISMCMappable
    {
        public long SeqMatrizCurricular { get; set; }

        public long SeqCurriculoCursoOferta { get; set; }

        public long? SeqDivisaoCurricularItem { get; set; }

        public long? SeqComponenteCurricular { get; set; }

        public TipoRequisitoAssociado Associado { get; set; }

        public TipoRequisito TipoRequisito { get; set; }

        public TipoRequisitoItem TipoRequisitoItem { get; set; }

        public long? ItemSeqDivisaoCurricularItem { get; set; }

        public long? ItemSeqComponenteCurricular { get; set; }

        public OutroRequisito OutroRequisitoItem { get; set; }

        public short? QuantidadeOutroRequisito { get; set; }
    }
}
