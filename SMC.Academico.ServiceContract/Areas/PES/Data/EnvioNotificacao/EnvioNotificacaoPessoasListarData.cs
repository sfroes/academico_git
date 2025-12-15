using SMC.Framework;
using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.PES.Data
{
    public class EnvioNotificacaoPessoasListarData : ISMCMappable, ISMCSeq
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