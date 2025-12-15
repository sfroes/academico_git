using SMC.Academico.Common.Areas.CAM.Enums;
using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.CAM.Data
{
    public class InstituicaoTipoEventoData : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqInstituicaoEnsino { get; set; }

        public long SeqTipoAgenda { get; set; }

        public long SeqTipoEventoAgd { get; set; }

        public AbrangenciaEvento AbrangenciaEvento { get; set; }

        public bool ApenasUmaParametrizacao { get; set; }

        public bool DiaUtilEscolarDocente { get; set; }

        public bool DiaUtilAdministrativo { get; set; }

        public bool Ativo { get; set; }

        public string Token { get; set; }

        public List<InstituicaoTipoEventoParametroData> Parametros { get; set; }
    }
}