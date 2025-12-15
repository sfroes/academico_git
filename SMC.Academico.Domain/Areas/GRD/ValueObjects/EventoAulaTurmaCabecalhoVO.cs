using SMC.Framework.Mapper;
using System;

namespace SMC.Academico.Domain.Areas.GRD.ValueObjects
{
    public class EventoAulaTurmaCabecalhoVO : ISMCMappable
    {
        public long SeqCursoOfertaLocalidade { get; set; }
        public int Codigo { get; set; }
        public short Numero { get; set; }
        public string CodigoFormatado { get { return $"{Codigo}.{Numero}"; } }
        public long SeqCicloLetivoInicio { get; set; }
        public string CicloLetivoInicio { get; set; }        
        public string DescricaoConfiguracaoComponente { get; set; }
        public DateTime InicioPeriodoLetivo { get; set; }
        public DateTime FimPeriodoLetivo { get; set; }
        public bool TurmaCancelada { get; set; }
        public bool SomenteLeitura { get; set; }
        public string MensagemFalha { get; set; }
        public int? CodigoUnidadeSeo { get; set; }
        public long? SeqAgendaTurma { get; set; }
        public bool DiarioFechado { get; set; }

    }
}
