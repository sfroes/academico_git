using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.ALN.Data
{
    public class TrabalhosData : ISMCMappable
    {
        public TrabalhosData()
        {
            Avaliacoes = new List<AvaliacaoTrabalhoData>();
        }

        //Sequencial do aluno
        public long Seq { get; set; }

        public long SeqTrabalhoAcademico { get; set; }

        public long SeqConfiguracaoComponente { get; set; }

        public string Titulo_Componente { get; set; }

        public string Titulo { get; set; }

        [SMCMapForceFromTo]
        public List<AvaliacaoTrabalhoData> Avaliacoes { get; set; }
    }
}