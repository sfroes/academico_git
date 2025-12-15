using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.Domain.Areas.PES.ValueObjects
{
    public class ConfiguracaoAvaliacaoPpaTurmaFiltroVO : SMCPagerFilterData, ISMCMappable
    {           
        public long SeqConfiguracaoAvaliacaoPpa { get; set; }
        public long SeqConfiguracaoAvaliacaoPpaTurma { get; set; }
    }
}
