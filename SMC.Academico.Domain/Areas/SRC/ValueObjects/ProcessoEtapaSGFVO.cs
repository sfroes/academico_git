using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.SRC.ValueObjects
{
    public class ProcessoEtapaSGFVO : ISMCMappable
    {       
        public long Seq { get; set; }
      
        public string Descricao { get; set; }        

        public bool Obrigatorio { get; set; }
      
        public bool? AssociarEtapa { get; set; }
    }
}
