using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.ServiceContract.Areas.CAM.Data
{
    public class ProcessoSeletivoData : ISMCMappable, ISMCLookupData
    {
        public long SeqCampanha { get; set; }

        public long Seq { get; set; }

        public string Descricao { get; set; }

        public List<long> SeqsNivelEnsino { get; set; }

        public long? SeqTipoProcessoSeletivo { get; set; }

        public long? SeqTipoVinculoAluno { get; set; }

        public long? SeqFormaIngresso { get; set; }

        public long? SeqProcessoGpi { get; set; }

        public bool? ReservaVaga { get; set; }

        public List<ProcessoSeletivoProcessoMatriculaData> ProcessosMatricula { get; set; }

        public List<ConvocacaoData> Convocacoes { get; set; }
    }
}
