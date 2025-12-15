using SMC.Academico.Common.Areas.DCT.Enums;
using SMC.Academico.Domain.Areas.DCT.Models;
using SMC.Framework;
using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.DCT.ValueObjects
{
    public class ColaboradorVinculoAtividadeVO : ISMCSeq, ISMCMappable
    {
        public long Seq { get; set; }

        public TipoAtividadeColaborador TipoAtividadeColaborador { get; set; }
    }
}