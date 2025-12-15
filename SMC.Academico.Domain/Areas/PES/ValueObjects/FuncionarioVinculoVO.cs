using SMC.Framework.Mapper;
using System;

namespace SMC.Academico.Domain.Areas.PES.ValueObjects
{
    public class FuncionarioVinculoVO : ISMCMappable
    {
        public long Seq { get; set; }
        public long SeqFuncionario { get; set; }
        public bool ObrigatorioVinculoUnico { get; set; }
        public string DescricaoTipoVinculo { get; set; }
        public long SeqTipoFuncionario { get; set; }
        public long? SeqEntidadeVinculo { get; set; }
        public long? SeqTipoEntidade { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime? DataFim { get; set; }
    }
}