using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.CNC.ValueObjects
{
    public class RegistroVO : ISMCMappable 
    {
        public DadosPrivadosDiplomadoVO DadosPrivadosDiplomado { get; set; }
        public TermoResponsabilidadeVO TermoResponsabilidade { get; set; }
        public List<DocumentacaoComprobatoriaVO> DocumentacaoComprobatoria { get; set; }
        public bool IsSegundaVia { get; set; }
    }
}
