using SMC.Academico.Common.Enums;
using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.Framework;
using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.ORG.Data
{
    public class InstituicaoTipoEntidadeEnderecoEletronicoData : ISMCMappable, ISMCSeq​​
    {
        public virtual long Seq { get; set; }

        public virtual long SeqInstituicaoTipoEntidade { get; set; }

        public virtual TipoEnderecoEletronico TipoEnderecoEletronico { get; set; }

        public virtual bool Obrigatorio { get; set; }
        
        public CategoriaEnderecoEletronico? CategoriaEnderecoEletronico { get; set; }
    }
}