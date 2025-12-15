using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.APR.ValueObjects
{
    public class EntregaOnlineParticipanteVO : ISMCMappable
    {
        public long Seq { get; set; }

        public ResponsavelEntregaOnline ResponsavelEntregaLegenda { get; set; }

        public bool ResponsavelEntrega { get; set; }

        public long NumeroRA { get; set; }

        public string NomeAluno { get; set; }

        public decimal? Nota { get; set; }

        public string Comentario { get; set; }

        public long SeqAlunoHistorico { get; set; }
    }
}