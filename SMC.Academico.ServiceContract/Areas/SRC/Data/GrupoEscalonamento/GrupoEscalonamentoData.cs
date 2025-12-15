using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.Framework;
using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.SRC.Data
{
    public class GrupoEscalonamentoData : ISMCMappable, ISMCSeq
    {
        public long Seq { get; set; }

        public long SeqProcesso { get; set; }

        public string Descricao { get; set; }

        public bool Ativo { get; set; }

        public short? NumeroDivisaoParcelas { get; set; }

        public bool ObrigarNumeroDivisaoParcelas { get; set; }

        public bool ExibeNumeroDivisaoParcelasDesabilitado { get; set; }

        public bool TodasParcelasLiberadas { get; set; }

        public bool ProcessoEncerrado { get; set; }

        public bool ExibeMensagemSalvar { get; set; }

        public bool ExibirLegenda { get; set; }

        public bool ExibeMensagemInformativa { get; set; }

        public bool ExibeMensagemTokenDisciplinaIsolada { get; set; }

        public List<GrupoEscalonamentoItemData> Itens { get; set; }

        public long SeqSolicitacaoServico { get; set; }

        public bool CamposReadOnly { get; set; }

        public string MensagemInformativa { get; set; }

        public string MensagemTokenDisciplinaIsolada { get; set; }
    }
}