using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.ALN.Data
{
    public class AvaliacaoTrabalhoData : ISMCMappable
    {
        public long SeqConfiguracaoComponente { get; set; }

        public long SeqAplicacaoAvaliacao { get; set; }

        public long SeqTrabalhoAcademico { get; set; }

        public DateTime Data { get; set; }

        public string Resultado { get; set; }

        public bool EsconderComissao { get; set; }

        public List<SMCDatasourceItem> ComissaoExaminadora { get; set; }
    }
}