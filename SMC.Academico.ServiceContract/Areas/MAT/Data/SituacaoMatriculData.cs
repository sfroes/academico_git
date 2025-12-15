using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.MAT.Data
{
    public class SituacaoMatriculaData: ISMCMappable
    {
        public long Seq { get; set; }

        public string Descricao { get; set; }

        public string Token { get; set; }

    }
}