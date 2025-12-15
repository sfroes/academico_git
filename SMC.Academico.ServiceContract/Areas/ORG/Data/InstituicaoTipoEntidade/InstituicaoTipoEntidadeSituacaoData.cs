using SMC.Framework;
using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.ORG.Data
{
    public class InstituicaoTipoEntidadeSituacaoData : ISMCMappable, ISMCSeq​​
    {
        public virtual long Seq { get; set; }

        public virtual long SeqInstituicaoTipoEntidade { get; set; }

        public virtual long SeqSituacaoEntidade { get; set; }

        public virtual bool Ativo { get; set; }
    }
}