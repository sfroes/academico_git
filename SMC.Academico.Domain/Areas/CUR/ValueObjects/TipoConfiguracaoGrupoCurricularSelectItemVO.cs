using SMC.Framework;
using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.CUR.ValueObjects
{
    public class TipoConfiguracaoGrupoCurricularSelectItemVO: ISMCMappable, ISMCSeq
    {
        public long Seq { get; set; }

        public string Descricao { get; set; }

        public bool ExigeFormato { get; set; }
    }
}
