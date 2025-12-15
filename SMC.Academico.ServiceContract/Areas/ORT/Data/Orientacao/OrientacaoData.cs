using SMC.Framework;
using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.ORT.Data
{
    public class OrientacaoData : ISMCMappable, ISMCSeq​​
    {
        public long Seq { get; set; }

        public long SeqEntidadeInstituicao { get; set; }

        public long SeqNivelEnsino { get; set; }

        public long SeqTipoOrientacao { get; set; }

        public string SeqTipoOrientacaoDescriptionField { get; set; }

        public long? SeqTipoTermoIntercambio { get; set; }

        public long SeqTipoVinculoAluno { get; set; }

        //public DateTime DataInicioOrientacao { get; set; }

        //public DateTime? DataFimOrientacao { get; set; }

        public List<OrientacaoPessoaAtuacaoData> OrientacoesPessoaAtuacao { get; set; }

        public List<OrientacaoColaboradorData> OrientacoesColaborador { get; set; }

        public string NomesAlunosExclucao { get; set; }

        public string MensagemIformativa { get; set; }

        public string TipoTermoIntercambioNameDescriptionField { get; set; }
    }
}