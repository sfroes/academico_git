using SMC.Academico.Common.Areas.GRD.Enums;
using SMC.Framework.Mapper;
using System;

namespace SMC.Academico.ServiceContract.Areas.GRD.Data
{
    public class EventoAulaTurmaCabecalhoData : ISMCMappable
    {
        public string CodigoFormatado { get; set; }
        public long SeqCicloLetivoInicio { get; set; }
        public string CicloLetivoInicio { get; set; }
        public string DescricaoConfiguracaoComponente { get; set; }
        public DateTime InicioPeriodoLetivo { get; set; }
        public DateTime FimPeriodoLetivo { get; set; }
        public bool SomenteLeitura { get; set; }
        public string MensagemFalha { get; set; }
        public int? CodigoUnidadeSeo { get; set; }
        public long? SeqAgendaTurma { get; set; }
        public long SeqCursoOfertaLocalidade { get; set; }

    }
}
