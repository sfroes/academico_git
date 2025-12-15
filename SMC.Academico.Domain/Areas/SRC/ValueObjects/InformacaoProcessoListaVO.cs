using SMC.Framework.Mapper;
using SMC.Framework.Model;

namespace SMC.Academico.Domain.Areas.SRC.ValueObjects
{
    public class InformacaoProcessoListaVO : ISMCMappable
    {
        public long SeqProcesso { get; set; }

        public long? SeqGrupoEscalonamento { get; set; }

        public bool ExibirData { get; set; }

        public bool ExibirPrazo { get; set; }

        public SMCPagerData<InformacaoProcessoItemVO> InformacoesProcesso { get; set; }
    }
}