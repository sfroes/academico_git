using SMC.Academico.Common.Areas.CNC.Enums;
using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.CNC.ValueObjects
{
    public class SolicitacaoAnaliseEmissaoDocumentoConclusaoFormacaoConcluidaVO : ISMCMappable
    {
        public long SeqFormacaoEspecifica { get; set; }

        public List<string> DescricoesFormacaoEspecifica { get; set; }

        public SituacaoAlunoFormacao? SituacaoAlunoFormacao { get; set; }

        public DateTime Data { get; set; }

        public DateTime DataInicio { get; set; }

        public DateTime? DataFim { get; set; }

        public long? SeqAlunoFormacao { get; set; }

        public bool? GeraCarimbo { get; set; }

        public long? SeqEntidadeResponsavel { get; set; }

        public bool ExigeGrau { get; set; }

        public long SeqTipoFormacaoEspecifica { get; set; }

        public bool PermiteEmitirDocumentoConclusao { get; set; }

        public string TokenTipoFormacaoEspecifica { get; set; }

        public long? SeqFormacaoEspecificaSuperior { get; set; }

        public string DescricaoFormacaoEspecifica { get; set; }

        public string DescricaoTipoFormacaoEspecifica { get; set; }

        public DateTime? DataFormacao { get; set; }

        public string DescricaoDocumentoConclusao { get; set; }

        public long? SeqTitulacao { get; set; }

        public string DescricaoTitulacao { get; set; }

        public long NumeroRA { get; set; }

        public DateTime? DataColacaoGrau { get; set; }

        public DateTime? DataConclusao { get; set; }

        public int? NumeroVia { get; set; }

        public List<(long SeqFormacaoEspecifica, string DescricaoFormacaoEspecifica, string TokenTipoFormacaoEspecifica)> HierarquiaFormacao { get; set; }
    }
}