using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.CNC.Data
{
    public class IntegralizacaoConsultaHistoricoData : ISMCMappable
    {
        public IntegralizacaoMatrizCurricularOfertaCabecalhoData DadosCabecalho { get; set; }

        public List<IntegralizacaoMatrizDivisaoData> HistoricoEscolarComMatriz { get; set; }

        public string MensagemComponentesCursados { get; set; }

        public List<IntegralizacaoHistoricoSemMatrizData> HistoricoEscolarSemMatriz { get; set; }
    }
}
