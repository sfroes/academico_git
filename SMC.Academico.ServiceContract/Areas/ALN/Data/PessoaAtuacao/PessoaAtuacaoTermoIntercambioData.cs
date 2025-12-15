using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.ALN.Data
{
    public class PessoaAtuacaoTermoIntercambioData : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqTermoIntercambio { get; set; }

        public long SeqInstituicaoEnsinoExterna { get; set; }

        public long SeqTipoOrientacao { get; set; }

        public string DescricaoTipoOrientacao { get; set; }

        public DateTime? DataInicio { get; set; }

        public DateTime? DataFim { get; set; }

        public string DescricaoTipoIntercambio { get; set; }

        public string DescricaoTermoIntercambio { get; set; }

        public string InstituicaoExterna { get; set; }

        public List<IngressanteOrientacaoData> OrientacaoParticipacoesColaboradores { get; set; }

        #region [ Campos dynamic ]

        public bool PermiteOrientacao { get; set; }

        #endregion [ Campos dynamic ]
    }
}