using SMC.Framework;
using SMC.Framework.Mapper;
using System;

namespace SMC.Academico.ServiceContract.Areas.ORG.Data
{
    public class HierarquiaEntidadeListaData : ISMCMappable, ISMCSeq​​
    {
        public long Seq { get; set; }

        public string Descricao { get; set; }


        public Nullable<DateTime> DataInicioVigencia { get; set; }

        public Nullable<DateTime> DataFimVigencia { get; set; }

        [SMCMapProperty("TipoHierarquiaEntidade.Descricao")]
        public string DescricaoTipoHierarquiaEntidade { get; set; }


        public long SeqTipoHierarquiaEntidade { get; set; }
    }
}
