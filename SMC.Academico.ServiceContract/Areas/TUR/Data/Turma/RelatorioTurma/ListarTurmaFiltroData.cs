using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.TUR.Data
{
    public class ListarTurmaFiltroData : ISMCMappable
    {
        public long? SeqCicloLetivo { get; set; }

        public long? SeqNivelEnsino { get; set; }

        public List<long> SeqsEntidadesResponsaveis { get; set; }

        public long? SeqLocalidade { get; set; }

        public long? SeqCursoOferta { get; set; }

        public long? SeqTurno { get; set; }

        public long? SeqTipoTurma { get; set; }
    }
}