using SMC.Academico.Common.Areas.TUR.Enums;
using SMC.Framework.UI.Mvc;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.TUR.Models
{
    public class TurmaOrientacaoCabecalhoViewModel : SMCViewModelBase
    {
        public long Seq { get; set; }

        public string CodigoFormatado { get; set; }

        public string CicloLetivoInicio { get; set; }

        public string CicloLetivoFim { get; set; }

        public short Vagas { get; set; }   
        
        public string DescricaoTipoTurma { get; set; }

        public SituacaoTurma SituacaoTurmaAtual { get; set; }

        public List<TurmaCabecalhoResponsavelViewModel> Colaboradores { get; set; }

        public List<TurmaOrientacaoCabecalhoConfiguracoesViewModel> TurmaConfiguracoesCabecalho { get; set; }

        public List<TurmaCabecalhoDivisoesViewModel> TurmaDivisoesCabecalho { get; set; }
    }
}