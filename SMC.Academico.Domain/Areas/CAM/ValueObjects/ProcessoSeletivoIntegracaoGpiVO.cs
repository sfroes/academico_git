using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.Domain.Areas.CAM.ValueObjects
{
    public class ProcessoSeletivoIntegracaoGpiVO : ISMCMappable
    {
        public long Seq { get; set; }
        public string Descricao { get; set; }
        public long SeqTipoProcessoSeletivo { get; set; }
        public long? SeqTipoVinculoAluno { get; set; }
        public long? SeqFormaIngresso { get; set; }
        public bool ReservaVaga { get; set; }
        public long? SeqProcessoGpi { get; set; }
        public List<long> SeqsNiveisEnsino { get; set; }
        public List<ProcessoSeletivoOfertaIntegracaoGpiVO> Ofertas { get; set; }
    }
}
