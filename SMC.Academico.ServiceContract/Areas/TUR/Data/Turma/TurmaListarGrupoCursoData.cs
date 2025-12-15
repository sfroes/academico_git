using SMC.Academico.Common.Constants;
using SMC.Framework.Mapper;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace SMC.Academico.ServiceContract.Areas.TUR.Data
{
    public class TurmaListarGrupoCursoData : ISMCMappable
    {        
        public string DescricaoCursoLocalidadeTurno { get; set; }

        public List<TurmaListarData> Turmas { get; set; }
    }
}
