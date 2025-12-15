using SMC.Academico.Domain.Areas.SRC.Models;
using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.SRC.ValueObjects
{
    public class DadosFormularioSolicitacaoVO : ISMCMappable
    {
        public virtual long Seq { get; set; }

        public virtual long SeqSolicitacaoServico { get; set; }

        public virtual long SeqFormulario { get; set; }

        public virtual long SeqVisao { get; set; }

        public virtual long SeqConfiguracaoEtapaPagina { get; set; }

        public virtual List<SolicitacaoDadoFormularioCampo> DadosCampos { get; set; }
    }
}