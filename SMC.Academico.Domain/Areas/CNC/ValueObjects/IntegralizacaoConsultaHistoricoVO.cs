using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.CNC.ValueObjects
{
    public class IntegralizacaoConsultaHistoricoVO : ISMCMappable
    {
        public IntegralizacaoMatrizCurricularOfertaCabecalhoVO DadosCabecalho { get; set; }

        public List<IntegralizacaoMatrizDivisaoVO> HistoricoEscolarComMatriz { get; set; }
        
        public string MensagemComponentesCursados { get; set; }

        public List<IntegralizacaoHistoricoSemMatrizVO> HistoricoEscolarSemMatriz { get; set; }        
    }
}
