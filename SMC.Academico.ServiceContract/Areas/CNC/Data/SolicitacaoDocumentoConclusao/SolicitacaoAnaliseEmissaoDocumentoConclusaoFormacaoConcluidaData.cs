using SMC.Academico.Common.Areas.CNC.Enums;
using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.CNC.Data.SolicitacaoDocumentoConclusao
{
    public class SolicitacaoAnaliseEmissaoDocumentoConclusaoFormacaoConcluidaData : ISMCMappable
    {
        public long SeqFormacaoEspecifica { get; set; }

        public List<string> DescricoesFormacaoEspecifica { get; set; }

        public SituacaoAlunoFormacao? SituacaoAlunoFormacao { get; set; }

        public DateTime Data { get; set; }

        public string DescricaoDocumentoConclusao { get; set; }

        public long? SeqTitulacao { get; set; }

        public string DescricaoTitulacao { get; set; }

        public long NumeroRA { get; set; }

        public DateTime? DataColacaoGrau { get; set; }

        public DateTime? DataConclusao { get; set; }

        public int? NumeroVia { get; set; }
    }
}