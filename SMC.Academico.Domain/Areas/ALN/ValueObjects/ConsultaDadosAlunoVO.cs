using SMC.Academico.Domain.Areas.CAM.ValueObjects;
using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.ALN.ValueObjects
{
    public class ConsultaDadosAlunoVO : ISMCMappable
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

        public string OfertaMatriz { get; set; }

        public string Ingressante { get; set; }

        public string IdIngressante { get; set; }

        public string DescricaoFormaIngresso { get; set; }

        #endregion Cabeçalho

        #region Cliclo Letivo

        public List<CicloLetivoVO> CiclosLetivos { get; set; }

        #endregion Cliclo Letivo
    }
}