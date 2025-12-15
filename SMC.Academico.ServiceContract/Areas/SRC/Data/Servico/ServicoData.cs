using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.SRC.Data
{
    public class ServicoData : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqInstituicaoEnsino { get; set; }

        public long SeqTipoServico { get; set; }

        public string DescricaoTipoServico { get; set; }

        public long SeqTemplateProcessoSgf { get; set; }

        public string DescricaoTemplateSGF { get; set; }

        public string Descricao { get; set; }

        public string Token { get; set; }

        public string TokenAcessoAtendimento { get; set; }

        public string TokenPermissaoManutencaoProcesso { get; set; }

        public TipoAtuacao TipoAtuacao { get; set; }

        public OrigemSolicitacaoServico OrigemSolicitacaoServico { get; set; }

        public bool? ValidarSituacaoFutura { get; set; }

        public bool? ExigeJustificativaSolicitacao { get; set; }

        public PermiteReabrirSolicitacao PermiteReabrirSolicitacao { get; set; }

        public bool ExibirSecaoReabrir { get; set; }

        public TipoPrazoReabertura? TipoPrazoReabertura { get; set; }

        public short? NumeroDiasPrazoReabertura { get; set; }

        public AcaoLiberacaoTrabalho? AcaoLiberacaoTrabalho { get; set; }

        public IntegracaoFinanceira IntegracaoFinanceira { get; set; }

        public bool ObrigatorioIdentificarParcela { get; set; }

        public string OrientacaoDeferimento { get; set; }

        public bool ExibirCampoTipoTransacao { get; set; }

        public bool ExibirSecaoMotivosBloqueioParcelas { get; set; }

        public bool CamposReadyOnly { get; set; }

        public List<InstituicaoNivelServicoData> InstituicaoNivelServicos { get; set; }

        public List<ServicoSituacaoSolicitarData> SituacoesSolicitar { get; set; }

        public List<ServicoSituacaoAtenderData> SituacoesAtender { get; set; }

        public List<ServicoSituacaoReabrirData> SituacoesReabrir { get; set; }

        public List<JustificativaSolicitacaoServicoData> Justificativas { get; set; }
     
        public List<RestricaoSolicitacaoSimultaneaData> RestricoesSolicitacaoSimultanea { get; set; }

        public List<ServicoMotivoBloqueioParcelaData> MotivosBloqueioParcela { get; set; }

        public List<ServicoTipoNotificacaoData> TiposNotificacao { get; set; }

        public List<ServicoTaxaData> Taxas { get; set; }

        public List<ServicoParametroEmissaoTaxaData> ParametrosEmissaoTaxa { get; set; }

        public List<ServicoTiposDocumentoData> TiposDocumento { get; set; }
    }
}