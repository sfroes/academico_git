using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.ServiceContract.Areas.FIN.Data
{
    public class AdesaoContratoDadosData : ISMCMappable
    {
        public long SeqSolicitacaoMatricula { get; set; }

        public long SeqSolicitacaoServicoEtapa { get; set; }

        public long SeqSituacaoFinalEtapaSucesso { get; set; }

        public long SeqConfiguracaoEtapa { get; set; }

        public long SeqPessoaAtuacao { get; set; }
    }
}
