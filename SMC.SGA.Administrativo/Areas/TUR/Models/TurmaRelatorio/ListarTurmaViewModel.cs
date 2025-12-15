using SMC.Framework.Mapper;
using SMC.Framework.Model;

namespace SMC.SGA.Administrativo.Areas.TUR.Models
{
    public class ListarTurmaViewModel : ISMCMappable
    {
        public long? SeqCicloLetivo { get; set; }

        public SMCUploadFile ArquivoLogotipo { get; set; }

        public string Instituicao { get; set; }

        public string Titulo { get; set; }

        /*Turmas - TurmaCicloLetivoViewModel*/

        public long? SeqOfertaCurso { get; set; }

        public string OfertaCurso { get; set; }

        public string Localidade { get; set; }

        public string Turno { get; set; }

        public short? AlunosMatriculados { get; set; }

        /*TurmaConfiguracaoCicloLetivo*/

        public long? SeqTurmaConfiguracaoComponente { get; set; }

        /// public long? SeqOfertaCurso { get; set; }

        public long? SeqTurma { get; set; }

        public string EntidadeResponsavel { get; set; }

        public string Turma { get; set; }

        public string TipoTurma { get; set; }

        public short Vagas { get; set; }

        public string ConfiguracaoComponente { get; set; }

        public string ComponenteSubstituto { get; set; }

        /*DivisaoTurmaCicloLetivo*/

        public long? SeqDivisaoTurma { get; set; }

        /// public long? SeqTurma { get; set; }

        public string DivisaoComponente { get; set; }

        public string CodificacaoDivisao { get; set; }

        public string LocalidadeDivisao { get; set; }

        public short VagasDivisao { get; set; }

        public string InformacoesComplementares { get; set; }

        /*TopicoDivisaoTurmaCicloLetivo*/

        public long? SeqDivisaoTurmaOrganizacao { get; set; }

        /// public long? SeqDivisaoTurma { get; set; }

        public string TipoOrganizacaoComponente { get; set; }

        /*TopicoDivisaoTurma*/

        public long? SeqDivisaoTurmaColaboradorOrganizacao { get; set; }

        /// public long? SeqDivisaoTurmaOrganizacao { get; set; }

        public string Colaborador { get; set; }

        public short CargaHoraria { get; set; }

    }
}