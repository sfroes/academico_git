using SMC.Formularios.ServiceContract.Areas.FRM.Data;
using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.SRC.Data
{
    public class DadosFormularioSolicitacaoPadraoData : DadoFormularioData, ISMCMappable
    {
        public virtual long SeqSolicitacaoServico { get; set; }

        public virtual long SeqConfiguracaoEtapaPagina { get; set; }
    }
}