using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.SRC.ValueObjects
{
    public class DadosModalSolicitacaoAtualVO : ISMCMappable
    {
        public string Descricao { get; set; }

        public DateTime? DataSituacao { get; set; }

        public string UsuarioResponsavel { get; set; }

        public string Observacao { get; set; }

    }
}