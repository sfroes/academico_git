using SMC.Framework;
using SMC.Framework.Mapper;

namespace SMC.SGA.Administrativo.Areas.CSO.Models
{
    public class CursoCabecalhoViewModel : ISMCSeq, ISMCMappable
    {
        public long Seq { get; set; }

        public string Nome { get; set; }

        public string Sigla { get; set; }

        public string Descricao { get { return $"{Seq.ToString("0000")} - {Nome}"; } }

        public bool Exibir { get; set; }
    }
}