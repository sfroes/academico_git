using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.CNC.ValueObjects
{
    public class EntregaDocumentoDigitalFiliacaoPaginaVO : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqPessoa { get; set; }

        public TipoParentesco TipoParentesco { get; set; }

        public string Nome { get; set; }
    }
}
