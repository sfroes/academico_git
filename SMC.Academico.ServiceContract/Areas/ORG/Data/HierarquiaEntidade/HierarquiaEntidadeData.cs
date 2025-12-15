using SMC.Framework;
using SMC.Framework.Mapper;
using System;

namespace SMC.Academico.ServiceContract.Areas.ORG.Data
{
    public class HierarquiaEntidadeData : ISMCMappable, ISMCSeq​​
    {
        public long Seq { get; set; }

        public long SeqInstituicaoEnsino { get; set; }

        public long SeqTipoHierarquiaEntidade { get; set; }

        public string Descricao { get; set; }


        public Nullable<DateTime> DataInicioVigencia { get; set; }

        public Nullable<DateTime> DataFimVigencia { get; set; }

        public bool SomenteAtivas { get; set; }
    }
}
