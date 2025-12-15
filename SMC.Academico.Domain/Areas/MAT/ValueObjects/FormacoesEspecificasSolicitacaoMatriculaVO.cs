using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.MAT.ValueObjects
{
    public class FormacoesEspecificasSolicitacaoMatriculaVO : ISMCMappable
    {
        public string DescricaoTipoFormacaoEspecifica { get; set; }

        public string DescricoesFormacoesEspecificas { get; set; }
    }
}