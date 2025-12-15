using SMC.Framework;
using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.CSO.Data
{
    public class CursoOfertaLocalidadeTurnoData : ISMCMappable, ISMCSeq
    {
        public long Seq { get; set; }

        public long SeqCursoOfertaLocalidade { get; set; }

        public long SeqTurno { get; set; }

        public string Descricao { get; set; }

        public bool Ativo { get; set; }
    }
}