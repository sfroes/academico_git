using SMC.Framework;
using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.APR.Data
{
    public class AplicacaoAvaliacaoData : ISMCMappable, ISMCSeq
    {
        public long Seq { get; set; }

        public long SeqAvaliacao { get; set; }

        public long SeqOrigemAvaliacao { get; set; }

        public string Sigla { get; set; }

        public bool EntregaWeb { get; set; }

        public string Observacao { get; set; }

        public DateTime? DataCancelamento { get; set; }

        public string MotivoCancelamento { get; set; }

        public long? SeqEventoAgd { get; set; }

        public int? CodigoLocalSef { get; set; }

        public string Local { get; set; }
        
        public long SeqTipoEventoAgd { get; set; }

        public DateTime? DataInicioAplicacaoAvaliacao { get; set; }

        public DateTime? DataFimAplicacaoAvaliacao { get; set; }

        public short? QuantidadeMaximaPessoasGrupo { get; set; }
               
        public AvaliacaoData Avaliacao { get; set; }

        public OrigemAvaliacaoData OrigemAvaliacao { get; set; }

        public List<MembroBancaExaminadoraData> MembrosBancaExaminadora { get; set; }

        public long? SeqEventoAulaInicio { get; set; }

        public long? SeqEventoAulaFim { get; set; }

        public long? SeqAgendaTurma { get; set; }
    }
}
