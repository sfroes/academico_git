using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.CUR.ValueObjects
{
    public class MatrizCurricularRelatorioVO : ISMCMappable
    {
        public List<MatrizCurricularRelatorioCabecalhoVO> MatrizCurricularCabecalho { get; set; }

        public List<MatrizCurricularRelatorioDadosVO> MatrizCurricularDados { get; set; }

        public List<MatrizCurricularRelatorioGruposVO> MatrizCurricularGrupos { get; set; }
    }
}
