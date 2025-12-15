using SMC.Framework;
using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.CNC.Data
{
    public class GrupoRegistroData : ISMCMappable, ISMCSeq
    {
        public long Seq { get; set; }
        public long SeqInstituicaoEnsino { get; set; }
        public string Descricao { get; set; }
        public string Prefixo { get; set; }
        public long NumeroUltimoRegistro { get; set; }
        public string PrefixoNumeroUltimoRegistro { get; set; }

    }
}