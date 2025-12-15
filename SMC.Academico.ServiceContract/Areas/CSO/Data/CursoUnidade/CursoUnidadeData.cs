using SMC.Academico.ServiceContract.Areas.ORG.Data;
using SMC.Framework;
using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.CSO.Data
{
    public class CursoUnidadeData : EntidadeData, ISMCMappable, ISMCSeq​​
    {
        [SMCMapProperty("SeqCursoHidden")]
        public long SeqCurso { get; set; }

        /// <summary>
        /// Sequencial do HierarquiaEntidadeItem que representa a Unidade
        /// </summary>
        public long SeqUnidade { get; set; }
    }
}