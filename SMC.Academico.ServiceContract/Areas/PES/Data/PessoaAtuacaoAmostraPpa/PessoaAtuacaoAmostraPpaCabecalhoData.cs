using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.ServiceContract.Areas.PES.Data
{
    public class PessoaAtuacaoAmostraPpaCabecalhoData : ISMCMappable
    {
        public long Seq { get; set; }

        public string DescricaoConfiguracaoAvaliacaoPpa { get; set; }

        public string EntidadeResponsavel { get; set; }

        public TipoAvaliacaoPpa TipoAvaliacao { get; set; }

        public string Turma { get; set; }

        public string DescricaoTurma { get; set; }

    }
}
