using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.SRC.Data
{
    public class InstituicaoNivelServicoData : ISMCMappable
    {       
        public long Seq { get; set; }
     
        public long SeqInstituicaoNivelTipoVinculoAluno { get; set; }
       
        public long SeqServico { get; set; }
       
        public long SeqNivelEnsino { get; set; }
       
        public long SeqTipoVinculoAluno { get; set; }

        public string DescricaoTipoVinculoAluno { get; set; }

        public int? SeqTipoTransacaoFinanceira { get; set; }
    }
}
