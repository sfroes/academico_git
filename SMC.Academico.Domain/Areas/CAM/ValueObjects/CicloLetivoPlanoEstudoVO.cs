using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.CAM.ValueObjects
{
    public class CicloLetivoPlanoEstudoVO : ISMCMappable
    {
        public string OfertaMatriz { get; set; }

        public List<CicloLetivoPlanoEstudoTurmasVO> Turmas { get; set; }

        public List<CicloLetivoPlanoEstudoAtividadesVO> Atividades { get; set; }
    }
}