using SMC.Academico.ServiceContract.Areas.ORG.Data;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.ServiceContract.Areas.ORT.Data
{
    public class OrientacaoListarData : SMCPagerFilterData, ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqEntidadeInstituicao { get; set; }

        public long SeqNivelEnsino { get; set; }

        public long SeqTipoOrientacao { get; set; }

        public List<OrientacaoColaboradorData> OrientacoesColaborador { get; set; }

        public List<OrientacaoPessoaAtuacaoData> OrientacoesPessoaAtuacao { get; set; }

        public InstituicaoEnsinoData InstituicaoEnsino { get; set; }

        public NivelEnsinoData NivelEnsino { get; set; }

        public TipoOrientacaoData TipoOrientacao { get; set; }

        public string NomesAlunosExclucao { get; set; }

        public long? SeqTipoTermoIntercambio { get; set; }
    }
}
