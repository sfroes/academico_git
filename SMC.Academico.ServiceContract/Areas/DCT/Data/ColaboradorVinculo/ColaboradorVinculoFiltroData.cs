using SMC.Academico.Common.Areas.DCT.Enums;
using SMC.Framework.DataAnnotations;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System;

namespace SMC.Academico.ServiceContract.Areas.DCT.Data
{
    public class ColaboradorVinculoFiltroData : SMCPagerFilterData, ISMCMappable
    {
        public long? Seq { get; set; }

        public long? SeqPessoa { get; set; }

        public long? SeqEntidadeVinculo { get; set; }

        public long? SeqTipoVinculoColaborador { get; set; }

        public DateTime? DataInicio { get; set; }

        public DateTime? DataFim { get; set; }

        public long? SeqCursoOfertaLocalidade { get; set; }

        public TipoAtividadeColaborador? TipoAtividade { get; set; }

        public long SeqColaborador { get; set; }
    }
}