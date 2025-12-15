using SMC.Academico.Common.Areas.DCT.Enums;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.DCT.Data
{
    public class ColaboradorAptoComponenteFiltroData : ISMCMappable
    {
        public long? SeqAtuacaoColaborador { get; set; }
    }
}