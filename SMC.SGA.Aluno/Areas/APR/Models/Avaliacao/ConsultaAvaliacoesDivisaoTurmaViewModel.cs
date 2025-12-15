using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System.Collections.Generic;

namespace SMC.SGA.Aluno.Areas.APR.Models.Avaliacao
{
    public class ConsultaAvaliacoesDivisaoTurmaViewModel : ISMCMappable
    {
        public long SeqDivisaoTurma { get; set; }

        public string Descricao { get; set; }
        
        public string Nota { get; set; }

        public List<DetalhesAvaliacaoViewModel> Avaliacoes { get; set; }
    }
}