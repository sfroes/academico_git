using SMC.Academico.Common.Areas.DCT.Enums;
using SMC.Academico.Common.Areas.ORT.Enums;
using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.ALN.ValueObjects
{
    public class InstituicaoNivelTipoOrientacaoParticipacaoVO : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqInstituicaoNivelTipoOrientacao { get; set; }

        public TipoParticipacaoOrientacao TipoParticipacaoOrientacao { get; set; }

        /// <summary>
        /// Descrição do tipo de participação com indicação de obrigatoriedade
        /// </summary>
        public string DescricaoTipoParticipacaoOrientacao { get; set; }

        public bool ObrigatorioOrientacao { get; set; }

        public OrigemColaborador OrigemColaborador { get; set; }
    }
}