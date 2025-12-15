using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.CUR.ValueObjects
{
    public class MatrizCurricularConfiguracaoComponenteVO : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqCurriculoCursoOfertaGrupo { get; set; }

        public string DescricaoCurriculoCursoOfertaGrupo { get; set; }

        public string DescricaoTipoConfiguracaoGrupoCurricular { get; set; }

        public FormatoConfiguracaoGrupo? FormatoConfiguracaoGrupoGrupoCurricular { get; set; }

        public string QuantidadeFormatada { get; set; }

        public IEnumerable<MatrizCurricularConfiguracaoComponenteDivisaoVO> DivisaoMatrizCurricularGrupo { get; set; }
    }
}
