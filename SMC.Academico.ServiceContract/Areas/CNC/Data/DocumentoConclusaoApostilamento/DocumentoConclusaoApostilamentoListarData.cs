using SMC.Framework.Mapper;
using System;

namespace SMC.Academico.ServiceContract.Areas.CNC.Data
{
    public class DocumentoConclusaoApostilamentoListarData : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqDocumentoConclusao { get; set; }

        public string DescricaoTipoApostilamento { get; set; }

        public string Descricao { get; set; }

        public DateTime DataInclusao { get; set; }
    }
}
