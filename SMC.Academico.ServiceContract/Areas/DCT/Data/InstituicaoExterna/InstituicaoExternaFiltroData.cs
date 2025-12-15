using SMC.Academico.Common.Areas.DCT.Enums;
using SMC.Academico.Common.Areas.ORT.Enums;
using SMC.Framework.DataAnnotations;
using SMC.Framework.Mapper;
using SMC.Framework.Model;

namespace SMC.Academico.ServiceContract.Areas.DCT.Data
{
    public class InstituicaoExternaFiltroData : SMCPagerFilterData, ISMCMappable
    {
        public bool RetornarInstituicaoEnsinoLogada { get; set; }

        public bool ListarSomenteInstituicoesEnsino { get; set; }

        /// <summary>
        /// Sequencial da instituição ensino logada
        /// </summary>
        public long SeqInstituicaoEnsino { get; set; }

        public string Sigla { get; set; }

        public string Nome { get; set; }

        public long? CodigoPais { get; set; }

        public TipoInstituicaoEnsino? TipoInstituicaoEnsino { get; set; }

        public long? SeqCategoriaInstituicaoEnsino { get; set; }

        public bool? Ativo { get; set; }

        public long? SeqColaborador { get; set; }

        public long[] SeqsColaboradores { get; set; }

        [SMCKeyModel]
        public long[] Seqs { get; set; }

        public long? SeqTermoIntercambio { get; set; }

        public long? SeqInstituicaoEnsinoExterna { get; set; }

        public TipoParticipacaoOrientacao? TipoParticipacaoOrientacao { get; set; }

        public long? SeqNivelEnsino { get; set; }

        public long? SeqTipoVinculoAluno { get; set; }

        public long? Seq { get; set; }

        public long? SeqTipoOrientacao { get; set; }
    }
}