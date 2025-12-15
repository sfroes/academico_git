using SMC.Framework;
using SMC.Framework.Mapper;
using System;

namespace SMC.Academico.ServiceContract.Areas.PES.Data
{
    public class FuncionarioVinculoData : ISMCSeq, ISMCMappable
    {
        public long Seq { get; set; }
        public long SeqFuncionario { get; set; }
        public long SeqTipoFuncionario { get; set; }
        public long? SeqEntidadeVinculo { get; set; }
        public long? SeqTipoEntidade { get; set; }
        public bool ExibirCamposTipoEntidadesEEntidades { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime? DataFim { get; set; }
    }
}