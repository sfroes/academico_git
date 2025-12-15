using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.PES.Data
{
    public class MotivoBloqueioData : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqTipoBloqueio { get; set; }

        public string Descricao { get; set; }

        public string Token { get; set; }

        public string OrientacoesDesbloqueio { get; set; }

        public FormaBloqueio FormaBloqueio { get; set; }

        public FormaBloqueio FormaDesbloqueio { get; set; }

        public bool PermiteDesbloqueioTemporario { get; set; }

        public TipoBloqueioData TipoBloqueio { get; set; }

        public bool PermiteItem { get; set; }

        public bool IntegracaoLegado { get; set; }

        public string TokenPermissaoCadastro { get; set; }

        public string TokenPermissaoDesbloqueio { get; set; }
    }
}