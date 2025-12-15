using SMC.Academico.Common.Areas.ORT.Enums;
using SMC.Framework.DataAnnotations;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System.Collections.Generic;

namespace SMC.SGA.Aluno.Models
{
    public class HomePublicacaoBdpViewModel : SMCViewModelBase
    {
        public long Seq { get; set; }

        public long SeqInstituicaoNivel { get; set; }

        public string DescricaoTipoTrabalho { get; set; }

        public string DescricaoTituloTrabalho { get; set; }
        
        public SituacaoTrabalhoAcademico UltimaSituacaoTrabalho { get; set; }

        public string NomeReduzidaInstituicao { get; set; }

        #region Botões do Grid

        public bool DesabilitarBotaoNovo
        {
            get
            {
                if(UltimaSituacaoTrabalho == SituacaoTrabalhoAcademico.AguardandoCadastroAluno)
                {
                    return false;
                }

                return true;
            }
        }

        public bool DesabilitarBotaoEditar
        {
            get
            {
                if (UltimaSituacaoTrabalho == SituacaoTrabalhoAcademico.CadastradaAluno)
                {
                    return false;
                }

                return true;
            }
        }

        public bool DesabilitarBotaoVisualizar
        {
            get
            {
                if (UltimaSituacaoTrabalho != SituacaoTrabalhoAcademico.AguardandoCadastroAluno && UltimaSituacaoTrabalho != SituacaoTrabalhoAcademico.CadastradaAluno)
                {
                    return false;
                }

                return true;
            }
        }

        public bool DesabilitarBotaoImprimir
        {
            get
            {
                if (UltimaSituacaoTrabalho != SituacaoTrabalhoAcademico.AguardandoCadastroAluno && UltimaSituacaoTrabalho != SituacaoTrabalhoAcademico.CadastradaAluno)
                {
                    return false;
                }

                return true;
            }
        }

        public bool DesabilitarBotaoAutorizar
        {
            get
            {
                if (UltimaSituacaoTrabalho == SituacaoTrabalhoAcademico.CadastradaAluno)
                {
                    return false;
                }

                return true;
            }
        }

        #endregion
    }
}