using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Academico.Domain.Areas.PES.Models;
using SMC.Framework;
using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.PES.ValueObjects
{
    public class FuncionarioVO : InformacoesPessoaVO, ISMCMappable
    {
        public long SeqTipoFuncionario { get; set; }
        public long? SeqEntidadeVinculo { get; set; }
        public long SeqTipoEntidade { get; set; }

        [SMCMapProperty("DataInicio")]
        public DateTime DataInicioVinculo { get; set; }
        [SMCMapProperty("DataFim")]
        public DateTime? DataFimVinculo { get; set; }
    }
}

