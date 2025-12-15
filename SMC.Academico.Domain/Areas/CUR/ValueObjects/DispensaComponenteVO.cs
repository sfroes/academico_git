using SMC.Framework;
using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.CUR.ValueObjects
{
    public class DispensaComponenteVO : ISMCMappable, ISMCSeq
    {
        public long Seq { get; set; }

        public string Codigo { get; set; }

        public string Descricao { get; set; }

        public string DescricaoCompleta { get { return $"{Codigo} - {Descricao}"; } }
    }
}
