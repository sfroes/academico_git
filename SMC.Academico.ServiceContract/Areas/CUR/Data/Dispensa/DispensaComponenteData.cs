using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.CUR.Data
{
    public class DispensaComponenteData : ISMCMappable
    {
        public long Seq { get; set; }

        public string Codigo { get; set; }

        public string Descricao { get; set; }

        public string DescricaoCompleta { get { return $"{Codigo} - {Descricao}"; } }
    }
}
