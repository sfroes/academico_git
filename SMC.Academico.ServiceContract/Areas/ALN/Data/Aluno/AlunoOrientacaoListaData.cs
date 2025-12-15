using SMC.Academico.Common.Areas.ORT.Enums;
using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.ALN.Data
{
    public class AlunoOrientacaoListaData : ISMCMappable
    {
        public string NomeOrientador { get; set; }

        public string NomeSocialOrientador { get; set; }

        public TipoParticipacaoOrientacao TipoParticipacaoOrientacao { get; set; }
    }
}