using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.CUR.ValueObjects
{
    public class TipoConfiguracaoGrupoCurricularVO : ISMCMappable
    {
        public long Seq { get; set; }

        public string Descricao { get; set; }

        public string Token { get; set; }

        public bool Raiz { get; set; }

        public bool ExigeFormato { get; set; }

        public bool SelfSubgrupo { get; set; }

        public List<TipoConfiguracaoGrupoCurricularFilhoVO> TiposConfiguracoesGrupoCurricularFilhos { get; set; }
    }
}
