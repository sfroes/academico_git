using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.CNC.ValueObjects
{
    public class DadosRegistroVO : ISMCMappable
    {
        public IesRegistradoraVO IesRegistradora { get; set; }
        public LivroRegistroVO LivroRegistro { get; set; }
        public List<string> DeclaracoesAcercaProcesso { get; set; } // Utilizada quando o diploma for do tipo Decisão Judicial Propriedade opcional para o preenchimento de declarações sobre o processo judicial, circunstâncias de emissão, ausência de informações, ou qualquer declaração que julgar pertinente.
        public string InformacoesAdicionais { get; set; }
        public List<InformacaoAssinanteVO> Assinantes { get; set; }
        public InformacoesProcessoJudicialVO InformacoesProcessoJudicial { get; set; }
    }
}
