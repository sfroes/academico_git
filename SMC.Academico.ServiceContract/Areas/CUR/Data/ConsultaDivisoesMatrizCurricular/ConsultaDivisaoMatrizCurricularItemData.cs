using SMC.Framework;
using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.CUR.Data
{
    public class ConsultaDivisaoMatrizCurricularItemData : ISMCMappable, ISMCSeq
    {
        /// <summary>
        /// Sequencial da divisão da matriz curricular
        /// </summary>
        public long Seq { get; set; }

        public short NumeroDivisaoCurricularItem { get; set; }

        public string DescricaoDivisaoCurricularItem { get; set; }

        public List<ConsultaDivisaoMatrizCurricularComponenteItemData> ConfiguracoesComponentes { get; set; }

        /// <summary>
        /// Hierarquia dos componentes associados à divisão
        /// </summary>
        public List<GrupoCurricularListaData> ComponentesGrupos { get; set; }

        /// <summary>
        /// Hierarquia dos grupos associados à divisão
        /// </summary>
        public List<GrupoCurricularListaData> ConfiguracoesGrupos { get; set; }
    }
}
