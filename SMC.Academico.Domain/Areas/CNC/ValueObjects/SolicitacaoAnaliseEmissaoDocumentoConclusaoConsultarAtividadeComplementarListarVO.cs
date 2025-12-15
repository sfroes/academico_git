using SMC.Framework.Mapper;
using System;

namespace SMC.Academico.Domain.Areas.CNC.ValueObjects
{
    public class SolicitacaoAnaliseEmissaoDocumentoConclusaoConsultarAtividadeComplementarListarVO : ISMCMappable
    {
        public string Codigo { get; set; }
        public DateTime? DataInicio { get; set; }
        public DateTime? DataFim { get; set; }
        public string Descricao { get; set; }
        public string CargaHoraria { get; set; }
        public string Etiqueta { get; set; }
        public string NomeDocente { get; set; }
        public string TitulacaoDocente { get; set; }
    }
}
