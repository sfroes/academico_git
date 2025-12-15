using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.ALN.ValueObjects
{
    public class PessoaAtuacaoTermoIntercambioVO : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqTermoIntercambio { get; set; }

        [SMCMapProperty("Orientacao.SeqTipoOrientacao")]
        public long? SeqTipoOrientacao { get; set; }

        [SMCMapProperty("TermoIntercambio.SeqParceriaIntercambioInstituicaoExterna")]
        public long SeqInstituicaoEnsinoExterna { get; set; }

        public long SeqColaborador { get; set; }

        public long? SeqOrientacao { get; set; }

        public long SeqPessoaAtuacao { get; set; }

        [SMCMapProperty("Orientacao.OrientacoesColaborador")]
        public List<IngressanteOrientacaoVO> OrientacaoParticipacoesColaboradores { get; set; }

        public DateTime? DataInicio { get; set; }

        public DateTime? DataFim { get; set; }

        public string DescricaoTipoIntercambio { get; set; }

        public string DescricaoTermoIntercambio { get; set; }

        public string InstituicaoExterna { get; set; }

        #region [ Campos edição ]

        [SMCMapProperty("Orientacao.TipoOrientacao.Descricao")]
        public string DescricaoTipoOrientacao { get; set; }

        public bool ExistePeriodo { get; set; }

        public bool PermiteOrientacao { get; set; }

        public bool RequerOrientacao { get; set; }

        #endregion [ Campos edição ]
    }
}