using SMC.Framework;
using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.CUR.Data
{
    public class CurriculoCursoOfertaGrupoListaData : ISMCMappable, ISMCSeq
    {
        public long Seq { get; set; }

        public string Descricao { get; set; }
    }
}
