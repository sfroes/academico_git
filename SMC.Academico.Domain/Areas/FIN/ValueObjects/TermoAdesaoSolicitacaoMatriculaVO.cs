using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.Domain.Areas.FIN.ValueObjects
{
    public class TermoAdesaoSolicitacaoMatriculaVO : ISMCMappable
    {
        public bool TipoOfertaExigeCurso { get; set; }

        public long? SeqCurso { get; set; }

        public long SeqInstituicaoEnsino { get; set; }

        public long SeqNivelEnsino { get; set; }

        public long SeqTipoVinculoAluno { get; set; }

        public long? SeqServico { get; set; }
    }
}
