using SMC.Academico.Common.Areas.ALN.Enums;
using SMC.Framework.Mapper;
using SMC.Framework.Model;

namespace SMC.Academico.ServiceContract.Areas.ALN.Data
{
    public class InstituicaoNivelTipoOrientacaoFiltroData : SMCPagerFilterData, ISMCMappable
    {
        public long? SeqNivelEnsino { get; set; }

        public long? SeqTipoVinculoAluno { get; set; }

        public long? SeqTipoOrientacao { get; set; }

        public long? SeqTipoIntercambio { get; set; }

        public long? SeqTermoIntercambio { get; set; }

        public bool? PossuiTipoIntercambio { get; set; }

        public CadastroOrientacao[] CadastroOrientacoesIngressante { get; set; }

        public CadastroOrientacao[] CadastroOrientacoesAluno { get; set; }

        public bool? PermiteManutencaoManual { get; set; }

        public bool? ExcetoParceriaIntercambio { get; set; }

        public long? SeqInstituicaoNivelTipoTermoIntercambio { get; set; }
    }
}