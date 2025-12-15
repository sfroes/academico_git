using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.SRC.Data
{
    public class GrupoEscalonamentoItemData : ISMCMappable, ISMCSeq
    {
        public long Seq { get; set; }

        public long SeqGrupoEscalonamento { get; set; }

        public long SeqProcessoEtapa { get; set; }

        public string DescricaoEtapa { get; set; }

        public SituacaoEtapa SituacaoEtapa { get; set; }

        public bool ExibeEscalonamentoDesabilitado { get; set; }

        public long SeqEscalonamento { get; set; }

        public decimal? ValorPercentualBanco { get; set; }

        public long SeqServico { get; set; }

        public string TokenServico { get; set; }

        public long SeqProcesso { get; set; }

        public string DescricaoEscalonamento { get; set; }

        public List<SMCDatasourceItem> Escalonamentos { get; set; }

        public List<GrupoEscalonamentoItemParcelaData> Parcelas { get; set; }

        public int QuantidadeParcelas { get; set; }

        public bool ExisteSolicitacoes { get; set; }

        public bool FinalizacaoEtapaAnterior { get; set; }

        public GrupoEscalonamentoParametros Legenda { get; set; }

        public bool ObrigatorioIdentificarParcela { get; set; }

        public bool CamposReadOnly { get; set; }

        public string MensagemInformativa { get; set; }
        public bool HouveAlteracaoParcela { get; set; }
    }
}