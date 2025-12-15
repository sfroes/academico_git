using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.ALN.Data
{
    public class PessoaAtuacaoTermoIntercambiListaData : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqPessoaAtuacao { get; set; }

        public long SeqTermoIntercambio { get; set; }

        public long? SeqNivelEnsino { get; set; }

        public long? SeqTipoVinculo { get; set; }

        public string Nome { get; set; }

        public string NomeSocial { get; set; }

        public TipoAtuacao TipoAtuacao { get; set; }

        public string TipoVinculoAlunoDescricao { get; set; }

        public string NomeEntidadeResponsavel { get; set; }

        public string DescricaoNivelEnsino { get; set; }

        public string DescricaoTermoIntercambio { get; set; }

        public string DescricaoTipoTermo { get; set; }

        public string InstituicaoExternaNome { get; set; }

        public int TipoMobilidade { get; set; }

        public List<IngressanteOrientacaoData> OrientacaoParticipacoesColaboradores { get; set; }

        public IList<PessoaAtuacaoTermoIntercambioPeriodoData> Periodos { get; set; }

        #region [ Campos dynamic ]

        public bool PermiteOrientacao { get; set; }

        #endregion [ Campos dynamic ]
    }
}