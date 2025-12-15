using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.MAT.Data
{
    public class FormacoesEspecificasSolicitacaoMatriculaData : ISMCMappable
    {
        public string DescricaoTipoFormacaoEspecifica { get; set; }

        public string DescricoesFormacoesEspecificas { get; set; }
    }
}