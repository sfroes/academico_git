using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.Academico.ServiceContract.Areas.MAT.Data;
using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.SRC.Data
{
    public class ProcessoEtapaData : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqProcesso { get; set; }

        public string DescricaoEtapa { get; set; }

        public long SeqEtapaSgf { get; set; }

        public string DescricaoEtapaSgf { get; set; }

        public DateTime? DataInicio { get; set; }

        public DateTime? DataFim { get; set; }

        public DateTime? DataEncerramento { get; set; }

        public SituacaoEtapa SituacaoEtapa { get; set; }

        public TipoPrazoEtapa? TipoPrazoEtapa { get; set; }

        public short? NumeroDiasPrazoEtapa { get; set; }

        public bool CentralAtendimento { get; set; }

        public string OrientacaoAtendimento { get; set; }
        public bool FinalizacaoEtapaAnterior { get; set; }

        public bool SolicitacaoEtapaAnteriorAtendida { get; set; }

        public bool ExibeItemMatriculaSolicitante { get; set; }

        public string Token { get; set; }

        public bool ExibeItemAposTerminoEtapa { get; set; }

        public List<EscalonamentoData> Escalonamentos { get; set; }

        [SMCMapProperty("Processo.Servico.TipoServico.ExigeEscalonamento")]
        public bool ExigeEscalonamento { get; set; }

        [SMCMapProperty("Processo.Servico.TipoServico.Token")]
        public string TokenTipoServico { get; set; }

        public short Ordem { get; set; }

        [SMCMapProperty("Processo.DataFim")]
        public DateTime? DataFimProcesso { get; set; }

        [SMCMapProperty("Processo.Servico.SeqTemplateProcessoSgf")]
        public long SeqTemplateProcessoSgf { get; set; }

        public bool EtapaCompartilhada { get; set; }

        public bool ControleVaga { get; set; }

        public bool CamposReadyOnly { get; set; }

        public bool ExibeSecaoTokenMatricula { get; set; }

        public List<SituacaoItemMatriculaData> SituacoesItemMatricula { get; set; }

        public List<ProcessoEtapaFiltroDadoData> FiltrosDados { get; set; }
    }
}