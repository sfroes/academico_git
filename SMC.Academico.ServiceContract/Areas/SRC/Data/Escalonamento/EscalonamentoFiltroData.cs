using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.SRC.Data
{
    public class EscalonamentoFiltroData : SMCPagerFilterData, ISMCMappable
    {
        public long? SeqProcesso { get; set; }
    }
}