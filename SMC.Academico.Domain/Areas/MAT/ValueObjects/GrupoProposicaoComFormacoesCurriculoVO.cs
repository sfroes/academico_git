using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.MAT.ValueObjects
{
    public class GrupoProposicaoComFormacoesCurriculoVO : ISMCMappable
    {
        public int CodGrupoProposicoes { get; set; }

        public List<FormacaoCurriculoVO> FormacoesCurriculo { get; set; }
    }
}