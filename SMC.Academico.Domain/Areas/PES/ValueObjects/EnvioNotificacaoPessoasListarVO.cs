using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.PES.ValueObjects
{
    public class EnvioNotificacaoPessoasListarVO : ISMCMappable
    {
        public long Seq { get; set; }
        public long? SeqNotificacaoEmailDestinatario { get; set; }
        public long NumeroRegistroAcademico { get; set; }
        public string Nome { get; set; }
        public string SituacaoMatricula { get; set; }
        public string Vinculo { get; set; }
        public string DadosVinculo { get; set; }
        public string Turma { get; set; }
        public string Entidade { get; set; }
        public string Email { get; set; }
        public bool PermiteVisualizarNotificacao { get; set; }

    }
}