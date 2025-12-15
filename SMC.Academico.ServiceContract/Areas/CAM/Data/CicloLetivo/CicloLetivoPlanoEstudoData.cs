using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.CAM.Data
{
    public class CicloLetivoPlanoEstudoData : ISMCMappable
    {
        public string OfertaMatriz { get; set; }

        public List<CicloLetivoPlanoEstudoTurmasData> Turmas { get; set; }

        public List<CicloLetivoPlanoEstudoAtividadeData> Atividades { get; set; }
    }
}