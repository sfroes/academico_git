using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.CSO.Data
{
    public class GrauAcademicoData : ISMCMappable
    {
        public long Seq { get; set; }

        public string Descricao { get; set; }

        public string DescricaoXSD { get; set; }

        public bool Ativo { get; set; }

        [SMCMapProperty("NiveisEnsino.NiveisEnsino")]
        public IList<NivelEnsinoGrauAcademicoData> NiveisEnsino { get; set; }
    }
}