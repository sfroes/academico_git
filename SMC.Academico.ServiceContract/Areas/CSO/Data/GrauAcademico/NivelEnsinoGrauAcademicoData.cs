using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.CSO.Data
{
    public class NivelEnsinoGrauAcademicoData : ISMCMappable
    {
        [SMCMapProperty("NiveisEnsino.Seq")]
        public long Seq { get; set; }

        [SMCMapProperty("NiveisEnsino.Descricao")]
        public string Descricao { get; set; }

    }

}
