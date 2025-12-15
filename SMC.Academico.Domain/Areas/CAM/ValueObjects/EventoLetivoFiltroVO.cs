using SMC.Academico.Common.Areas.ALN.Enums;
using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.CAM.ValueObjects
{
    public class EventoLetivoFiltroVO : ISMCMappable
    {
        public long? Seq { get; set; }

        public long? SeqInstituicaoEnsino { get; set; }

        public long? SeqTipoAgenda { get; set; }

        public long? SeqTipoEvento { get; set; }

        public string Descricao { get; set; }

        public int? AnoCiclo { get; set; }

        public int? NumeroCiclo { get; set; }

        public long? SeqNivelEnsino { get; set; }

        public List<long> SeqsEntidadesResponsaveis { get; set; }

        public long? SeqLocalidade { get; set; }

        public long? SeqCursoOferta { get; set; }

        public long? SeqModalidade { get; set; }

        public TipoAluno? TipoAluno { get; set; }
    }
}