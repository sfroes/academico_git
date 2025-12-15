using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Framework.Mapper;
using System;

namespace SMC.Academico.ServiceContract.Areas.APR.Data
{
    public class LancamentoFrequenciaAulaData : ISMCMappable
    {
        public long SeqEventoAula { get; set; }
        /// <summary>
        /// Descrição da aula no formato:
        /// [Sigla]: [data aula] [hora início] às [hora fim]
        /// </summary>
        /// <example>A1: 15/01/2021 10:40 às 11:30</example>
        public string DescricaoFormatada { get; set; }
        /// <summary>
        /// Sigla para apresentação na interface
        /// </summary>
        /// <example>A1</example>
        public string Sigla { get; set; }
        public SituacaoApuracaoFrequencia SituacaoApuracaoFrequencia { get; set; }
        public DateTime Data { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFim { get; set; }
        public DateTime? DataPrimeiraApuracaoFrequencia { get; set; }
        public DateTime? DataLimiteApuracaoFrequencia { get; set; }
        public string UsuarioPrimeiraApuracaoFrequencia { get; set; }
    }
}
