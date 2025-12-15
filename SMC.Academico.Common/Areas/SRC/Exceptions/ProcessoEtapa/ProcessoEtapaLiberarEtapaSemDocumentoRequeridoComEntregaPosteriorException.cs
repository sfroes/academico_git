using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class ProcessoEtapaLiberarEtapaSemDocumentoRequeridoComEntregaPosteriorException : SMCApplicationException
    {
        // "Não é possível liberar a etapa. A descrição do termo de responsabilidade de entrega da documentação foi preenchida, 
        // porém não existe documento requerido configurado para permitir entrega posterior."
        public ProcessoEtapaLiberarEtapaSemDocumentoRequeridoComEntregaPosteriorException()
        : base(ExceptionsResource.ERR_ProcessoEtapaLiberarEtapaSemDocumentoRequeridoComEntregaPosterior)
        {}
    }
}
