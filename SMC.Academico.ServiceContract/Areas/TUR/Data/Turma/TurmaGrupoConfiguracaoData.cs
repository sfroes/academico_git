using SMC.Framework;
using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.TUR.Data
{
    public class TurmaGrupoConfiguracaoData : ISMCMappable, ISMCSeq
    {
        public long Seq { get; set; }

        public long SeqConfiguracaoComponente { get; set; }

        public string Descricao { get; set; }

        public long SeqComponenteCurricular { get; set; }

        public bool Selecionado { get; set; }

        public bool Principal { get; set; }
        
    }
}
