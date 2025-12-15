using SMC.Academico.Common.Areas.ORT.Enums;
using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.ALN.ValueObjects
{
    public class AlunoOrientacaoListaVO : ISMCMappable
    {
        public string NomeOrientador { get; set; }

        public string NomeSocialOrientador { get; set; }

        public string NomeFormatado => string.IsNullOrEmpty(NomeSocialOrientador) ? NomeOrientador : $"{NomeSocialOrientador} ({NomeOrientador})";

        public TipoParticipacaoOrientacao TipoParticipacaoOrientacao { get; set; }
    }
}