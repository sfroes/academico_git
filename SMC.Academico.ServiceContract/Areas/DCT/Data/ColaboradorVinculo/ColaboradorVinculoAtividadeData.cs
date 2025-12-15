using SMC.Academico.Common.Areas.DCT.Enums;
using SMC.Framework;
using SMC.Framework.Mapper;
using System;

namespace SMC.Academico.ServiceContract.Areas.DCT.Data
{
    public class ColaboradorVinculoAtividadeData : ISMCSeq, ISMCMappable
    {
        public long Seq { get; set; }

        public TipoAtividadeColaborador TipoAtividadeColaborador { get; set; }
    }
}