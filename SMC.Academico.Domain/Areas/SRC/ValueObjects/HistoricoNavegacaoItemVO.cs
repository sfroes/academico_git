using SMC.Framework.Mapper;
using System;

namespace SMC.Academico.Domain.Areas.SRC.ValueObjects
{
    public class HistoricoNavegacaoItemVO : ISMCMappable
    {
        public string Pagina { get; set; }
        public DateTime DataEntrada { get; set; }
        public DateTime? DataSaida { get; set; }
    }
}