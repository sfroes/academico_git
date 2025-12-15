using SMC.Framework;
using SMC.Framework.Mapper;
using System;

namespace SMC.Academico.Domain.Areas.PES.ValueObjects
{
    public class FuncionarioVinculoListaVO : ISMCSeq, ISMCMappable
    {
        public long Seq { get; set; }
        public long SeqFuncionario { get; set; }
        public long SeqTipoFuncionario { get; set; }
        public string DescricaoTipoFuncionario { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime? DataFim { get; set; }
        public string DescricaoEntidadeCadastrada { get; set; }
        public string TipoEntidadeCadastrada { get; set; }
    }
}