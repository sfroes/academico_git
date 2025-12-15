using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Academico.Common.Areas.ORT.Enums;
using SMC.Academico.ServiceContract.Areas.APR.Data;
using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.ServiceContract.Areas.ORT.Data
{
    public class AvaliacaoTrabalhoAcademicoAvaliacaoData : ISMCMappable
    {
        public long Seq { get; set; }

        public bool AlunoFormado { get; set; }

        public long SeqOrigemAvaliacao { get; set; }

        public long SeqTrabalhoAcademico { get; set; }

        public long? SeqArquivoAnexadoAtaDefesa { get; set; }

        public string GuidArquivoAnexadoAtaDefesa { get; set; }

        public DateTime? DataInicioAplicacaoAvaliacao { get; set; }

        public DateTime? Hora { get; set; }

        public SituacaoHistoricoEscolar SituacaoHistoricoEscolar { get; set; }

        public DateTime? DataCancelamento { get; set; }

        public string Local { get; set; }

        public string NotaConceito { get; set; }

        public virtual TipoAvaliacao TipoAvaliacao { get; set; }

        public List<MembroBancaExaminadoraData> MembrosBancaExaminadora { get; set; }
    }
}
