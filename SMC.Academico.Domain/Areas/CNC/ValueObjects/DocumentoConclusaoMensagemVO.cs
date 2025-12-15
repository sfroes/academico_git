using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.CNC.ValueObjects
{
    public class DocumentoConclusaoMensagemVO : ISMCMappable
    {
        public long Seq { get; set; }

        public string Descricao { get; set; }
    }
}
