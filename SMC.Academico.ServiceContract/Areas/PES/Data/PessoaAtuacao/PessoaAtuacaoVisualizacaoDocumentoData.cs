using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.PES.Data
{
    public class PessoaAtuacaoVisualizacaoDocumentoData : ISMCMappable
    {
        public long SeqPessoaAtuacao { get; set; }

        public SituacaoDocumentacao SituacaoDocumentacao { get; set; }

        public List<PessoaAtuacaoTipoDocumentoData> TiposDocumento { get; set; }
    }
}