using SMC.Academico.Domain.Areas.ORG.Models;
using SMC.Academico.Domain.Areas.ORT.Models;
using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.ORT.ValueObjects
{
    public class OrientacaoVO : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqEntidadeInstituicao { get; set; }

        public long SeqNivelEnsino { get; set; }

        public long? SeqTipoOrientacao { get; set; }

        [SMCMapProperty("TipoOrientacao.Descricao")]
        public string SeqTipoOrientacaoDescriptionField { get; set; }

        public long? SeqTipoTermoIntercambio { get; set; }

        public long SeqTipoVinculoAluno { get; set; }

        public List<OrientacaoColaboradorVO> OrientacoesColaborador { get; set; }

        public List<OrientacaoPessoaAtuacaoVO> OrientacoesPessoaAtuacao { get; set; }

        public InstituicaoEnsino InstituicaoEnsino { get; set; }

        public NivelEnsino NivelEnsino { get; set; }

        public TipoOrientacao TipoOrientacao { get; set; }

        public string NomesAlunosExclucao { get; set; }

        public string MensagemIformativa { get; set; }

        public string TipoTermoIntercambioNameDescriptionField { get; set; }
    }
}