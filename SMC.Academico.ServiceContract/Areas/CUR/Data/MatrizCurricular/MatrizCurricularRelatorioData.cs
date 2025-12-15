using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.CUR.Data
{
    public class MatrizCurricularRelatorioData : ISMCMappable
    {
        public List<MatrizCurricularRelatorioCabecalhoData> MatrizCurricularCabecalho { get; set; }

        public List<MatrizCurricularRelatorioDadosData> MatrizCurricularDados { get; set; }

        public List<MatrizCurricularRelatorioGruposData> MatrizCurricularGrupos { get; set; }
    }
}
