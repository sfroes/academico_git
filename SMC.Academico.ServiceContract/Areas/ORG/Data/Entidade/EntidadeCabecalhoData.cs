using SMC.Framework;
using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.ORG.Data
{
    /// <summary>
    /// DTO para os dados de cabeçalho de entidade
    /// </summary>
    public class EntidadeCabecalhoData : ISMCMappable, ISMCSeq​​
    {
        public long Seq { get; set; }

        public string Nome { get; set; }

        public string Sigla { get; set; }
    }
}