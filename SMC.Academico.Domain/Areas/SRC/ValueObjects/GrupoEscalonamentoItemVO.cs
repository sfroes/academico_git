using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.Framework;
using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.SRC.ValueObjects
{
    public class GrupoEscalonamentoItemVO : ISMCMappable, ISMCSeq
    {
        public long Seq { get; set; }

        public long SeqGrupoEscalonamento { get; set; }

        public long SeqProcessoEtapa { get; set; }

        public string DescricaoEtapa { get; set; }

        public SituacaoEtapa SituacaoEtapa { get; set; }

        public bool ExibeEscalonamentoDesabilitado { get; set; }

        public long SeqEscalonamento { get; set; }

        public long SeqServico { get; set; }

        public string TokenServico { get; set; }

        public long SeqProcesso { get; set; }

        public DateTime DataIncioEscalonamento { get; set; }

        public DateTime? DataFimEscalonamento { get; set; }

        public DateTime? DataEncerramentoEscalonamento { get; set; }

        public string DescricaoEscalonamento { get; set; }

        public IEnumerable<EscalonamentoEtapaItemVO> Escalonamentos { get; set; }

        public List<GrupoEscalonamentoItemParcelaVO> Parcelas { get; set; }

        public GrupoEscalonamentoVO GrupoEscalonamento { get; set; }

        public int QuantidadeParcelas { get; set; }

        public bool ExisteSolicitacoes { get; set; }

        public bool FinalizacaoEtapaAnterior { get; set; }

        public GrupoEscalonamentoParametros Legenda { get; set; }

        public bool ObrigatorioIdentificarParcela { get; set; }

        public short OrdemEtapa { get; set; }

        public bool CamposReadOnly { get; set; }

        public string MensagemInformativa { get; set; }

        public decimal? ValorPercentualBanco { get; set; }
        public bool HouveAlteracaoParcela { get; set; }
    }

    public class EscalonamentoEtapaItemVO : ISMCMappable, ISMCSeq
    {
        public long Seq { get; set; }

        public DateTime DataInicio { get; set; }

        public DateTime? DataFim { get; set; }

        public DateTime? DataEncerramento { get; set; }

        public string Descricao
        {
            get
            {
                var retorno = $"{this.DataInicio}";
                if (this.DataFim.HasValue)
                {
                    ///Valida se o escalonamento está encerrado
                    if (this.DataEncerramento.HasValue)
                    {
                        retorno += $" - {this.DataFim} - Encerrado";
                    }
                    else
                    {
                        retorno += $" - {this.DataFim}";
                    }
                }
                return retorno;
            }
        }
    }
}