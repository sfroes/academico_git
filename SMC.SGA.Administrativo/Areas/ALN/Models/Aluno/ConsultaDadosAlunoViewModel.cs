using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.ALN.Models
{
    public class ConsultaDadosAlunoViewModel : SMCViewModelBase
    {
        #region Dados Aluno

        public long SeqAluno { get; set; }

        public long RA { get; set; }

        public string Nome { get; set; }

        public string Situacao { get; set; }

        public bool Falecido { get; set; }

        public bool VinculoAtivo { get; set; }

        public string Vinculo { get; set; }

        public string DadosVinculo { get; set; }

        public DateTime DataAdmissao { get; set; }

        public string Entidade { get; set; }

        #endregion Dados Aluno

        #region Cabeçalho

        public string CicloLetivoIngresso { get; set; }

        public string OfertaCurso { get; set; }

        public string Localidade { get; set; }

        public string Turno { get; set; }

        [SMCValueEmpty("-")]
        public string OfertaMatriz { get; set; }

        public string Ingressante { get; set; }

        public string IdIngressante { get; set; }

        [SMCValueEmpty("-")]
        public string DescricaoFormaIngresso { get; set; }

        [SMCValueEmpty("-")]
        public string NomeInstituicaoTransferenciaExterna { get; set; }

        [SMCValueEmpty("-")]
        public string CursoTransferenciaExterna { get; set; }

        #endregion Cabeçalho

        #region Ciclos Letivos

        public List<ConsultaDadosAlunoCiclosLetivosViewModel> CiclosLetivos { get; set; }

        #endregion Ciclos Letivos
    }
}