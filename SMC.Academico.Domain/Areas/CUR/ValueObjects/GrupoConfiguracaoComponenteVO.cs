using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.CUR.ValueObjects
{
    public class GrupoConfiguracaoComponenteVO : ISMCMappable
    {
        public long Seq { get; set; }

        public string Descricao { get; set; }

        public bool Ativo { get; set; }

        public TipoGrupoConfiguracaoComponente TipoGrupoConfiguracaoComponente { get; set; }

        public List<GrupoConfiguracaoComponenteItemVO> Itens { get; set; }
    }
}
