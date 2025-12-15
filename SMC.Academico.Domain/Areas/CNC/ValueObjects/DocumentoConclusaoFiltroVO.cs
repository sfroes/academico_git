using SMC.Academico.Common.Areas.CNC.Enums;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.CNC.ValueObjects
{
    public class DocumentoConclusaoFiltroVO : SMCPagerFilterData, ISMCMappable
    {
        public long? SeqPessoa { get; set; }

        public List<long?> SeqsEntidadesResponsaveis { get; set; }

        public long? SeqCursoOfertaLocalidade { get; set; }

        public long? SeqTipoDocumentoAcademico { get; set; }

        public long? SeqSituacaoDocumentoAcademico { get; set; }

        public long? SeqTipoServico { get; set; }

        public long? SeqSolicitacaoServico { get; set; }

        public TipoInvalidade? TipoInvalidade { get; set; }
    }
}
