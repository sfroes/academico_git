using SMC.Academico.Common.Areas.ORG.Enums;
using SMC.Framework;
using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.ORG.Data
{
    public class SituacaoEntidadeData : ISMCMappable, ISMCSeq​​
    {
        public virtual long Seq { get; set; }

        public virtual string Descricao { get; set; }

        public virtual CategoriaAtividade CategoriaAtividade { get; set; }
    }
}
