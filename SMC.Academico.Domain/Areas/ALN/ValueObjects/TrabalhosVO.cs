using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.ALN.ValueObjects
{
    public class TrabalhosVO : ISMCMappable
    {
        public TrabalhosVO()
        {
            Avaliacoes = new List<AvaliacaoTrabalhoVO>();
        }

        //Sequencial do aluno
        public long Seq { get; set; }

        public long SeqTrabalhoAcademico { get; set; }
        public long SeqConfiguracaoComponente { get; set; }

        public string Titulo_Componente { get; set; }

        public string TituloTrabalhoAcademico { get; set; }

        public string TituloIdiomaTrabalho { get; set; }

        public string Titulo { get { return !string.IsNullOrEmpty(TituloIdiomaTrabalho) ? TituloIdiomaTrabalho : TituloTrabalhoAcademico; } }

        public List<AvaliacaoTrabalhoVO> Avaliacoes { get; set; }
    }
}