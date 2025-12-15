using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.Framework.Mapper;
using System;

namespace SMC.Academico.ServiceContract.Areas.SRC.Data
{
    public class HistoricoSolicitacaoEtapaItemData : ISMCMappable
    {
        public SituacaoFinalEtapa SituacaoFinalEtapa { get; set; }

        public string Etapa { get; set; }

        public string Situacao { get; set; }

        public DateTime DataInicio { get; set; }

        public DateTime? DataFim { get; set; }

        public string UsuarioResponsavel { get; set; }

        public string Observacao { get; set; }
    }
}