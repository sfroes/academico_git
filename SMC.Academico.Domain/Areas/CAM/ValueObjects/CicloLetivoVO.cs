using SMC.Framework;
using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.CAM.ValueObjects
{
    public class CicloLetivoVO : ISMCMappable, ISMCSeq
    {
        public long Seq { get; set; }

        public long SeqInstituicaoEnsino { get; set; }

        public long SeqRegimeLetivo { get; set; }

        public short Ano { get; set; }

        public short Numero { get; set; }

        public string Descricao { get; set; }

        public string DescricaoFormatada { get; set; }

        public string AnoNumeroCicloLetivo { get; set; }

        [SMCMapProperty("RegimeLetivo.Descricao")]
        public string DescricaoRegimeLetivo { get; set; }

        public List<CicloLetivoSituacaoVO> CiclosLetivosSituacoes { get; set; }

        public List<CicloLetivoPlanoEstudoVO> PlanosEstudos { get; set; }
    }
}