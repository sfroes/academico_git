using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.ORT.Data
{
    public class AvaliacaoTrabalhoAcademicoCabecalhoData : ISMCMappable
    {
        public string DescricaoTipoTrabalho { get; set; }

        public string Titulo { get; set; }

        public List<AutorData> Autores { get; set; }

        public string DescricaoComponenteCurricular { get; set; }

        public SituacaoHistoricoEscolar? SituacaoHistoricoEscolar { get; set; }
    }
}
