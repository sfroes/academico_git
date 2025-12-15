using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Framework;
using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.CUR.Data
{
    public class MatrizCurricularConfiguracaoComponenteData : ISMCMappable, ISMCSeq
    {
        public long Seq { get; set; }

        public long SeqCurriculoCursoOfertaGrupo { get; set; }

        public string DescricaoCurriculoCursoOfertaGrupo { get; set; }

        public string DescricaoTipoConfiguracaoGrupoCurricular { get; set; }

        public FormatoConfiguracaoGrupo? FormatoConfiguracaoGrupoGrupoCurricular { get; set; }

        public string QuantidadeFormatada { get; set; }

        public List<MatrizCurricularConfiguracaoComponenteDivisaoData> DivisaoMatrizCurricularGrupo { get; set; }
    }
}
