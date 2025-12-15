using SMC.Academico.ServiceContract.Data;
using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.CSO.Data
{
    public class ProgramaPropostaData : ISMCMappable
    {
        public long Seq { get; set; }

        [SMCMapProperty("SeqEntidade")]
        public long SeqPrograma { get; set; }

        public long SeqDadoFormulario { get; set; }

        public long SeqCicloLetivo { get; set; }

        public SGADadoFormularioData DadoFormulario { get; set; }

        public List<long> SeqsNiveisEnsino { get; set; }
    }
}