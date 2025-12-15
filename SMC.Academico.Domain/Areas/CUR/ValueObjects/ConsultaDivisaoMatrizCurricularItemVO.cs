using SMC.Framework;
using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.CUR.ValueObjects
{
    public class ConsultaDivisaoMatrizCurricularItemVO : ISMCMappable, ISMCSeq
    {
        /// <summary>
        /// Sequencial da divisão da matriz curricular
        /// </summary>
        public long Seq { get; set; }

        public short NumeroDivisaoCurricularItem { get; set; }

        public string DescricaoDivisaoCurricularItem { get; set; }

        public IEnumerable<ConsultaDivisaoMatrizCurricularComponenteItemVO> ConfiguracoesComponentes { get; set; }

        /// <summary>
        /// Hierarquia dos componentes associados à divisão
        /// </summary>
        public IEnumerable<GrupoCurricularListaVO> ComponentesGrupos { get; set; }

        /// <summary>
        /// Hierarquia dos grupos associados à divisão
        /// </summary>
        public IEnumerable<GrupoCurricularListaVO> ConfiguracoesGrupos { get; set; }
    }
}
