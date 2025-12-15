using SMC.Academico.ServiceContract.Areas.ORG.Data;
using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.ReportHost.Areas.TUR.Models
{
    public class TurmaRelatorioFiltroVO : ISMCMappable
    {
        public long SeqCicloLetivo { get; set; }

        public long? SeqNivelEnsino { get; set; }

        #region [BI_CSO_001]

        /// <summary>
        /// Sequencial da entidade responsável com o nome esperado pelo lookup (para facilitar o depency)
        /// e mapeado para o nome adequando para os dtos
        /// </summary>
        public List<long> SeqsEntidadesResponsaveis { get; set; }

        public long? SeqCursoOferta { get; set; }

        #endregion [BI_CSO_001]
        /// <summary>
        /// O campo “Tipo de turma” deve ser filtrado de acordo com o valor do 
        /// campo “Nível de ensino”, de acordo com parametrização de 
        /// instituição-nível-tipo de turma.
        /// </summary>
        public long? SeqTipoTurma { get; set; }

        public long SeqInstituicaoEnsino { get; set; }
    }
}