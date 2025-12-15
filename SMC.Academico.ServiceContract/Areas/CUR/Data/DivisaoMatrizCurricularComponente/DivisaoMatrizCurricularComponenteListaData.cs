using SMC.Framework;
using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.CUR.Data
{
    public class DivisaoMatrizCurricularComponenteListaData : ISMCMappable, ISMCSeq
    {
        public long Seq { get; set; }
        public long SeqComponenteCurricular { get; set; }
        public long SeqGrupoCurricularComponente { get; set; }
        public string DescricaoComponente { get; set; }
        public string DescricaoConfiguracaoComponente { get; set; }
        public string DescricaoDivisao { get; set; }
        public bool ExigeAssuntoComponente { get; set; }
    }
}
