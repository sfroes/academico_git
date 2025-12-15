using SMC.Academico.Common.Areas.CNC.Enums;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.ORT.Data
{
    public class ConfiguracaoNumeracaoTrabalhoFiltroData : SMCPagerFilterData, ISMCMappable
    {
        public List<long?> SeqsEntidadesResponsaveis { get; set; }
        public long? SeqEntidadeResponsavel { get; set; }

    }
}
