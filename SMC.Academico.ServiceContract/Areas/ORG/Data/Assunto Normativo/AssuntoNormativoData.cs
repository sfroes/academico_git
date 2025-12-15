using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.ORG.Data
{
    public class AssuntoNormativoData : ISMCMappable
    {

        public long Seq { get; set; }

        public string Descricao { get; set; }

        public string Token { get; set; }

        public bool HabilitaEmissaoDocumentoConclusao { get; set; }

        public bool Ativo { get; set; }
    }
}