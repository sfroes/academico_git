using SMC.Academico.Common.Areas.ORT.Enums;
using SMC.Framework.Mapper;

namespace SMC.Academico.ReportHost.Areas.ALN.Models
{
    public class AlunoOrientacaoVO : ISMCMappable
    {
        public string NomeOrientador { get; set; }

        public TipoParticipacaoOrientacao TipoParticipacaoOrientacao { get; set; }
    }
}