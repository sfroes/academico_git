using SMC.Framework.Mapper;
using System;

namespace SMC.Academico.ServiceContract.Areas.CNC.Data.SolicitacaoDocumentoConclusao
{
    public class SolicitacaoAnaliseEmissaoDocumentoConclusaoConsultarHistoricoListarData : ISMCMappable
    {
        public string Codigo { get; set; }

        public string Periodo { get; set; }

        public string Disciplina { get; set; }

        public string CargaHoraria { get; set; }

        public string Nota { get; set; }

        public string FormaIntegralizacao { get; set; }

        public string Etiqueta { get; set; }

        public string NomeDocente { get; set; }

        public string TitulacaoDocente { get; set; }
    }
}
