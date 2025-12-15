using SMC.Framework;
using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.CSO.ValueObjects
{
    public class CursoOfertaLocalidadeTurnoVO : ISMCMappable, ISMCSeq
    {
        public long Seq { get; set; }

        public long SeqCursoOfertaLocalidade { get; set; }

        public long SeqTurno { get; set; }

        [SMCMapProperty("Turno.Descricao")]
        public string Descricao { get; set; }

        public bool Ativo { get; set; }
    }
}