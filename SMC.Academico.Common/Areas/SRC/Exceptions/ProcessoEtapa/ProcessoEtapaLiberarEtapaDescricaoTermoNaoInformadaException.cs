using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    // "Não é possível liberar a etapa. Existe documento requerido configurado para permitir entrega posterior, 
    // porém a descrição do termo de responsabilidade de entrega não foi informada."
    public class ProcessoEtapaLiberarEtapaDescricaoTermoNaoInformadaException : SMCApplicationException
    {
        public ProcessoEtapaLiberarEtapaDescricaoTermoNaoInformadaException()
        : base(ExceptionsResource.ERR_ProcessoEtapaLiberarEtapaDescricaoTermoNaoInformada)
        {}
    }
}
