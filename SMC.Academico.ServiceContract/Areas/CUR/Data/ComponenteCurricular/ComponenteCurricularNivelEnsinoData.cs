using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.CUR.Data
{
    public class ComponenteCurricularNivelEnsinoData : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqComponenteCurricular { get; set; }

        public long SeqNivelEnsino { get; set; }

        public bool Responsavel { get; set; }
                       
        public string NivelEnsinoDescricao { get; set; }
    }
}
