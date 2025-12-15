using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.Framework;
using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.SRC.Data
{
    public class GrupoEscalonamentoListaData : ISMCMappable, ISMCSeq
    {
        public long Seq { get; set; }

        public string Descricao { get; set; }

        public bool Ativo { get; set; }

        public long SeqProcesso { get; set; }

        public string DescricaoProcesso { get; set; }

        public short? NumeroDivisaoParcelas { get; set; }

        public bool ProcessoNoPeriodoVigencia { get; set; }

        public bool ProcessoExpirado { get; set; }

        public bool ProcessoEncerrado { get; set; }

        public bool PossuiParcelas { get; set; }
        public bool PossuiIntegracaoFinanceira { get; set; }

        public bool ProcessoFuturo { get; set; }

        public List<GrupoEscalonamentoListaItemData> Itens { get; set; }

        public bool TodasEtapasEncerradas { get; set; }

        public bool NaoPermiteAssociarSolicitacaoEtapasExpiradas { get; set; }

        public int QuantidadeSolicitacoes { get; set; }

        public PermiteReabrirSolicitacao PermiteReabrirSolicitacao { get; set; }

        public bool? PermitirNotificacao { get; set; }

        public bool PossuiParcelasVencimentoMenorEscalonamento { get; set; }

        public bool HabilitaBtnComPermissaoManutencaoProcesso { get; set; }
    }
}