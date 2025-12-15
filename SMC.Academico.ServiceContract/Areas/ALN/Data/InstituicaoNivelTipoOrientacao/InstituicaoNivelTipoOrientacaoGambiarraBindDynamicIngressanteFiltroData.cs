using SMC.Academico.Common.Areas.ALN.Enums;
using SMC.Framework.Mapper;
using SMC.Framework.Model;

namespace SMC.Academico.ServiceContract.Areas.ALN.Data
{
    /// <summary>
    /// Ao corrigir o erro de bind de datasource do Dynamic remover essa classe e utilizar a InstituicaoNivelTipoOrientacaoFiltroData
    /// SMC.SGA.Administrativo.Areas.ALN.Models.IngressanteDynamicModel.TiposOrientacaoPessoaAtuacao
    /// </summary>
    public class InstituicaoNivelTipoOrientacaoGambiarraBindDynamicIngressanteFiltroData : SMCPagerFilterData, ISMCMappable
    {
        public long? SeqNivelEnsino { get; set; }

        public long? SeqTipoVinculoAluno { get; set; }

        public bool? PossuiTipoIntercambio { get; set; }

        public CadastroOrientacao[] CadastroOrientacoesIngressante { get; set; }
    }
}