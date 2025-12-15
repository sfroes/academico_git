using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.CNC.Data
{
    public class DocumentoConclusaoMensagemData : ISMCMappable
    {
        public long Seq { get; set; }

        public string Descricao { get; set; }
    }
}
