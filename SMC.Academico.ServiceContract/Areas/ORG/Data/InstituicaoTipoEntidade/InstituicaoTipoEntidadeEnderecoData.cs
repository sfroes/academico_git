using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.Localidades.Common.Areas.LOC.Enums;

namespace SMC.Academico.ServiceContract.Areas.ORG.Data
{
    public class InstituicaoTipoEntidadeEnderecoData : ISMCMappable, ISMCSeq​​
    {
        public virtual long Seq { get; set; }

        public virtual long SeqInstituicaoTipoEntidade { get; set; }

        public virtual TipoEndereco TipoEndereco { get; set; }

        public virtual bool Obrigatorio { get; set; }
    }
}