using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.CUR.Data
{
    public class ComponenteCurricularEntidadeResponsavelData : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqComponenteCurricular { get; set; }

        public long SeqEntidade { get; set; }
                
        public string NomeEntidade { get; set; }
    }
}
