using SMC.Academico.Common.Areas.TUR.Enums;
using SMC.Framework.UI.Mvc;
using System.Collections.Generic;

namespace SMC.SGA.Aluno.Models
{
    public class HomeTurmaViewModel : SMCViewModelBase
    {
        public long Seq { get; set; }

        public int Codigo { get; set; }

        public short Numero { get; set; }

        public string DescricaoTipoTurma { get; set; }

        public bool TurmaCompartilhada { get; set; }

        public string DescricaoConfiguracaoComponente { get; set; }

        public string DescricaoCicloLetivoInicio { get; set; }

        public string DescricaoCicloLetivoFim { get; set; }

        public long SeqCicloLetivoInicio { get; set; }

        public long SeqCicloLetivoFim { get; set; }

        public short? QuantidadeVagas { get; set; }

        public SituacaoTurma? SituacaoTurmaAtual { get; set; }

        public string SituacaoJustificativa { get; set; }
                
        public List<HomeTurmaDivisaoViewModel> TurmaDivisoes { get; set; }

        public bool? PermiteAvaliacaoParcial { get; set; }

        /// <summary>
        /// Comando só deve ser apresentao se o indicador de PermiteAvaliacaoParcial da origem avaliação da turma for igual a false
        /// </summary>
        public bool HideNotasFrequencia
        {
            get
            {
                bool retorno = (!PermiteAvaliacaoParcial.GetValueOrDefault()) ? false : true;

                return retorno;
            }
        }

        /// <summary>
        /// Comando só deve ser apresentao se o indicador de PermiteAvaliacaoParcial da origem avaliação da turma for igual true
        /// </summary>
        public bool HideAvaliacao
        {
            get
            {
                bool retorno = (PermiteAvaliacaoParcial.GetValueOrDefault()) ? false : true;

                return retorno;
            }
        }
    }
}