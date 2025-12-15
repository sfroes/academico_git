using SMC.Academico.Common.Areas.DCT.Enums;
using SMC.Framework.DataAnnotations;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System;

namespace SMC.Academico.ServiceContract.Areas.DCT.Data
{
    public class ColaboradorVinculoCursoFiltroData : SMCPagerFilterData, ISMCMappable
    {
        public long? SeqCursoOfertaLocalidade { get; set; }

        public TipoAtividadeColaborador? TipoAtividadeColaborador { get; set; }

        public long[] SeqsEntidadesVinculo { get; set; }

        public long? SeqCursoOferta { get; set; }

        public long[] SeqsCursoOferta { get; set; }

        public long[] SeqsCursoOfertaLocalidade { get; set; }

        public long? SeqColaboradorVinculo { get; set; }

        public long? SeqEntidadeVinculo { get; set; }
    }
}