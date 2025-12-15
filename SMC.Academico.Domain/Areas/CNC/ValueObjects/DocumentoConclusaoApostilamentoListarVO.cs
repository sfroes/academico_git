using SMC.Framework.Mapper;
using System;

namespace SMC.Academico.Domain.Areas.CNC.ValueObjects
{
    public class DocumentoConclusaoApostilamentoListarVO : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqDocumentoConclusao { get; set; }

        public string DescricaoTipoApostilamento { get; set; }

        public string Descricao { get; set; }

        public DateTime DataInclusao { get; set; }
    }
}
