using SMC.Academico.Common.Areas.DCT.Enums;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.DCT.ValueObjects
{
    public class ColaboradorOrientadorFiltroVO : SMCPagerFilterData, ISMCMappable
    {
        public long? SeqCursoOferta { get; set; }

        public long? SeqLocalidade { get; set; }

        public List<long> SeqEntidadeResponsavel { get; set; }
    }
}