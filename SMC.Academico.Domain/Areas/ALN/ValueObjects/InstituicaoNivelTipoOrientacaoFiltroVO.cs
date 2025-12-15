using SMC.Academico.Common.Areas.ALN.Enums;
using SMC.Framework.Mapper;
using SMC.Framework.Model;

namespace SMC.Academico.Domain.Areas.ALN.ValueObjects
{
    public class InstituicaoNivelTipoOrientacaoFiltroVO : SMCPagerFilterData, ISMCMappable
    {
        public long? SeqNivelEnsino { get; set; }

        public long? SeqInstituicaoNivelTipoVinculoAluno { get; set; }

        public long? SeqTipoVinculoAluno { get; set; }

        public long? SeqTipoOrientacao { get; set; }

        public long? SeqTipoIntercambio { get; set; }

        public bool? PossuiTipoIntercambio { get; set; }

        public long[] SeqsTipoTermoIntercambio { get; set; }

        public CadastroOrientacao? CadastroOrientacaoIngressante { get; set; }

        public CadastroOrientacao[] CadastroOrientacoesIngressante { get; set; }

        public CadastroOrientacao? CadastroOrientacaoAluno { get; set; }

        public CadastroOrientacao[] CadastroOrientacoesAluno { get; set; }

        public long? SeqTermoIntercambio { get; set; }

        public bool? PermiteManutencaoManual { get; set; }

        public long? SeqInstituicaoNivelTipoTermoIntercambio { get; set; }
    }
}