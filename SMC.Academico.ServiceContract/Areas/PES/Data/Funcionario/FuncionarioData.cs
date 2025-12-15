using SMC.Academico.ServiceContract.Areas.PES.Data;
using System;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.PES.Data
{
    public class FuncionarioData : PessoaAtuacaoData
    {
        public long SeqTipoFuncionario { get; set; }
        public long? SeqEntidadeVinculo { get; set; }
        public long SeqTipoEntidade { get; set; }
        public DateTime DataInicioVinculo { get; set; }
        public DateTime? DataFimVinculo { get; set; }
    }
}