using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.ServiceContract.Areas.PES.Data
{
    public class PessoaAtuacaoAmostraPpaFiltroData : SMCPagerFilterData, ISMCMappable
    {
        public long SeqConfiguracaoAvaliacaoPpa { get; set; }
        public long? SeqConfiguracaoAvaliacaoPpaTurma { get; set; }
    }
}
