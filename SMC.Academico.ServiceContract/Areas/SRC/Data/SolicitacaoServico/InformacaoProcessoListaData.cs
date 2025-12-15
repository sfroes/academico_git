using SMC.Framework.Mapper;
using SMC.Framework.Model;

namespace SMC.Academico.ServiceContract.Areas.SRC.Data
{
    public class InformacaoProcessoListaData : ISMCMappable
    {
        public long SeqProcesso { get; set; }

        public long? SeqGrupoEscalonamento { get; set; }

        public bool ExibirData { get; set; }

        public bool ExibirPrazo { get; set; }

        public SMCPagerData<InformacaoProcessoItemData> InformacoesProcesso { get; set; }
    }
}