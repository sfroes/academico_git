using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.SRC.ValueObjects
{
    public class ServicoVO : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqInstituicaoEnsino { get; set; }

        public long SeqTipoServico { get; set; }

        [SMCMapProperty("TipoServico.Descricao")]
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

        public List<InstituicaoNivelServicoVO> InstituicaoNivelServicos { get; set; }

        public List<ServicoSituacaoSolicitarVO> SituacoesSolicitar { get; set; }

        public List<ServicoSituacaoAtenderVO> SituacoesAtender { get; set; }

        public List<ServicoSituacaoReabrirVO> SituacoesReabrir { get; set; }

        public List<JustificativaSolicitacaoServicoVO> Justificativas { get; set; }

        public List<RestricaoSolicitacaoSimultaneaVO> RestricoesSolicitacaoSimultanea { get; set; }

        public List<ServicoMotivoBloqueioParcelaVO> MotivosBloqueioParcela { get; set; }

        public List<ServicoTipoNotificacaoVO> TiposNotificacao { get; set; }

        public List<ServicoTaxaVO> Taxas { get; set; } 

        public List<ServicoParametroEmissaoTaxaVO> ParametrosEmissaoTaxa { get; set; }

        public List<ServicoTipoDocumentoVO> TiposDocumento { get; set; }
    }
}