using SMC.Framework;
using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.PES.Data
{
    public class FuncionarioVinculoListaData : ISMCSeq, ISMCMappable
    {
        public long Seq { get; set; }
        public long SeqFuncionario { get; set; }
        public long SeqTipoFuncionario { get; set; }
        public string DescricaoTipoFuncionario { get; set; }
        public string DescricaoEntidadeCadastrada { get; set; }
        public string TipoEntidadeCadastrada { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime? DataFim { get; set; }
    }
}