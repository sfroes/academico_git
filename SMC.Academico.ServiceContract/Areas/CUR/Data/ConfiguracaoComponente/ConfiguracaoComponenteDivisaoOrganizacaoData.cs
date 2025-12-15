using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.CUR.Data
{
    public class ConfiguracaoComponenteDivisaoOrganizacaoData : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqDivisaoComponente { get; set; }

        public long SeqComponenteCurricularOrganizacao { get; set; }

        [SMCMapProperty("ComponenteCurricularOrganizacao.Descricao")]
        public string DescricaoComponenteCurricularOrganizacao { get; set; }

        public short? CargaHoraria { get; set; }
    }
}