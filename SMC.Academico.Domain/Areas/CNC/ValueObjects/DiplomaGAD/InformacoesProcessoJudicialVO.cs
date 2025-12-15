using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.CNC.ValueObjects
{
    public class InformacoesProcessoJudicialVO : ISMCMappable
    {
        public string NumeroProcessoJudicial { get; set; } // O número do processo deve estar no formato NNNNNNN-DD.AAAA.J.TR.OOOO, onde: - NNNNNNN: número sequencial do processo por unidade de origem. É reiniciado a cada ano. - DD: dígito verificador. - AAAA: ano do ajuizamento do processo - J: órgão ou segmento do Poder Judiciário - TR: tribunal do respectivo segmento do Poder Judiciário e, na Justiça Militar da União, a Circunscrição Judiciária - OOOO: unidade de origem do processo
        public string NomeJuiz { get; set; }
        public string Decisao { get; set; }
    }
}
