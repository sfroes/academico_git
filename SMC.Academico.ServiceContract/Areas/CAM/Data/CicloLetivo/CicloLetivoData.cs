using SMC.Framework;
using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.CAM.Data
{
    public class CicloLetivoData : ISMCMappable, ISMCSeq
    {
        public long Seq { get; set; }

        public long SeqInstituicaoEnsino { get; set; }

        public long SeqRegimeLetivo { get; set; }

        public short Ano { get; set; }

        public short Numero { get; set; }

        public string Descricao { get; set; }

        public string DescricaoFormatada { get; set; }

        [SMCMapProperty("RegimeLetivo.Descricao")]
        public string DescricaoRegimeLetivo { get; set; }

        public string AnoNumeroCicloLetivo { get; set; }

        public List<CicloLetivoNivelEnsinoData> NiveisEnsino { get; set; }

        public List<CicloLetivoSituacaoData> CiclosLetivosSituacoes { get; set; }

        public List<CicloLetivoPlanoEstudoData> PlanosEstudos { get; set; }
    }
}