using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System.Collections.Generic;

namespace SMC.SGA.Aluno.Areas.APR.Models.Avaliacao
{
    public class ConsultaAvaliacoesTurmaViewModel : ISMCMappable
    {
        public long SeqTurma { get; set; }

        public string Descricao { get; set; }

        public int Falta { get; set; }

        [SMCDisplay]
        [SMCValueEmpty("-")]
        public string NotaTotal { get; set; }

        [SMCDisplay]
        [SMCValueEmpty("-")]
        public string NotaTotalReavaliacao { get; set; }

        public string Situacao { get; set; }

        public bool DiarioFechado { get; set; }

        public bool PossuiApuracaoFrequencia { get; set; }
        
        public List<DetalhesAvaliacaoViewModel> Avaliacoes { get; set; }

        public List<ConsultaAvaliacoesDivisaoTurmaViewModel> DivisoesTurma { get; set; }
    }
}