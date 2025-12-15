using SMC.Academico.Common.Areas.CAM.Enums;
using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.CAM.Data
{
    public class InstituicaoTipoEventoListaData : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqTipoEventoAgd { get; set; }

        [SMCMapProperty("InstituicaoEnsino.Nome")]
        public string DescricaoInstituicaoEnsino { get; set; }

        public string DescricaoTipoEvento { get; set; }

        [SMCMapProperty("TipoAgenda.Descricao")]
        public string DescricaoTipoAgenda { get; set; }

        public AbrangenciaEvento AbrangenciaEvento { get; set; }

        public List<InstituicaoTipoEventoParametroData> Parametros { get; set; }
    }
}