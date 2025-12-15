using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.ALN.ValueObjects
{
    public class AvaliacaoTrabalhoVO : ISMCMappable
    {
        public long SeqConfiguracaoComponente { get; set; }

        public long SeqAplicacaoAvaliacao { get; set; }

        public long SeqTrabalhoAcademico { get; set; }

        public DateTime Data { get; set; }

        public string Resultado { get; set; }

        public bool EsconderComissao { get; set; }

        public List<SMCDatasourceItem> ComissaoExaminadora { get; set; }

        public long SeqOrigemAvaliacao { get; set; }

        public bool? Aprovado { get; set; }
    }
}