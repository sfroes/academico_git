using SMC.Framework.Mapper;
using System;

namespace SMC.Academico.ServiceContract.Areas.CAM.Data
{
    public class CampanhaCopiaEtapaProcessoGPIItemData : ISMCMappable
    {
        public bool Checked { get; set; }

        public long Seq { get; set; }

        public long SeqProcessoSeletivo { get; set; }

        public string Token { get; set; }

        public string Descricao { get; set; }

        public DateTime DataInicioEtapa { get; set; }

        public DateTime DataFimEtapa { get; set; }

        public bool CopiarConfiguracaoEtapa { get; set; }
    }
}