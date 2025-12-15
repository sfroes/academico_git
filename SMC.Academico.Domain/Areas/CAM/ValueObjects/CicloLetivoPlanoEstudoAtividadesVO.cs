using SMC.Framework;
using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.CAM.ValueObjects
{
    public class CicloLetivoPlanoEstudoAtividadesVO : ISMCMappable
    {
        public long Seq { get; set; }

        public string Descricao { get; set; }
    }
}