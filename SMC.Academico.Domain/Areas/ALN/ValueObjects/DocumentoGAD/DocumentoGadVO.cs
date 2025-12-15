using SMC.Framework.Mapper;
using System;

namespace SMC.Academico.Domain.Areas.ALN.ValueObjects
{
    public class DocumentoGadVO : ISMCMappable
    {
        public long Seq { get; set; }
        public string Nome { get; set; }
        public DateTime DataInclusao { get; set; }
        public string UsuarioInclusao { get; set; }       
       
    }
}
