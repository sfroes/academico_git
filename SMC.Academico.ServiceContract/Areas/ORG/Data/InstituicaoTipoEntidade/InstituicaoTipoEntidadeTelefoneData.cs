using SMC.Academico.Common.Enums;
using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.Framework;
using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.ORG.Data
{
    public class InstituicaoTipoEntidadeTelefoneData : ISMCMappable, ISMCSeq​​
    {
        public long Seq { get; set; }

        public long SeqInstituicaoTipoEntidade { get; set; }

        public TipoTelefone TipoTelefone { get; set; }

        public bool Obrigatorio { get; set; }

        public CategoriaTelefone? CategoriaTelefone { get; set; }
    }
}