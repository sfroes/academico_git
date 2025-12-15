using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.MAT.Data
{
    public class EfetivacaoComprovanteData : ISMCMappable
    {
        public List<EfetivacaoDadosData> DadosPessoais { get; set; }

        public List<EfetivacaoTurmaData> Turmas { get; set; }

        public List<EfetivacaoAtividadeData> Atividades { get; set; }

        public List<EfetivacaoParcelaData> Parcelas { get; set; }

    }
}
