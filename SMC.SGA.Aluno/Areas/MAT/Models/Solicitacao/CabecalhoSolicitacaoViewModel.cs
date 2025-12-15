using SMC.Framework;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System.Collections.Generic;

namespace SMC.SGA.Aluno.Areas.MAT.Models
{
    public class CabecalhoSolicitacaoViewModel : SMCViewModelBase
    {
        [SMCSize(SMCSize.Grid24_24)]
        public string DescricaoProcesso { get; set; }

        [SMCSize(SMCSize.Grid24_24)]
        public string DescricaoProcessoSeletivo { get; set; }

        //[SMCSize(SMCSize.Grid24_24)]
        //public string DescricaoCicloLetivo { get; set; }

        [SMCSize(SMCSize.Grid24_24)]
        public string DescricaoVinculo { get; set; }

        [SMCSize(SMCSize.Grid24_24)]
        public string DescricaoEntidadeResponsavel { get; set; }

        [SMCSize(SMCSize.Grid24_24)]
        public string DescricaoOferta { get; set; }

        [SMCSize(SMCSize.Grid24_24)]
        public string DescricaoLocalidade { get; set; }

        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid12_24, SMCSize.Grid12_24, SMCSize.Grid12_24)]
        public string DescricaoModalidade { get; set; }

        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid12_24, SMCSize.Grid12_24, SMCSize.Grid12_24)]
        public string DescricaoTurno { get; set; }

        [SMCSize(SMCSize.Grid24_24)]
        public string DescricaoCurso { get; set; }

        public List<FormacoesEspecificasSolicitacaoMatriculaViewModel> FormacoesEspecificas { get; set; }

        [SMCSize(SMCSize.Grid24_24)]
        public string NomeOrientador { get; set; }

        public bool ExigeCurso { get; set; }

        public bool ExibeFormacoesEspecificas { get; set; }

        [SMCSize(SMCSize.Grid24_24)]
        public string DescricaoTipoOrientacao { get; set; }

        public bool ExibeEntidadeResponsavelEVinculo { get; set; }

        [SMCHidden]
        public bool ExibeContatoSecretariaAcademica { get; set; }

        [SMCSize(SMCSize.Grid24_24)]
        [SMCConditionalDisplay(nameof(ContatoSecretariaAcademica), SMCConditionalOperation.NotEqual, "")]
        public string ContatoSecretariaAcademica { get; set; }

        [SMCHidden]
        public bool ExibeContatoCoordenacaoCurso { get; set; }

        [SMCSize(SMCSize.Grid24_24)]
        [SMCConditionalDisplay(nameof(ContatoCoordenacaoCurso), SMCConditionalOperation.NotEqual, "")]
        public string ContatoCoordenacaoCurso { get; set; }

        [SMCHidden]
        public bool ExibeContatoSetorMatricula { get; set; }

        [SMCSize(SMCSize.Grid24_24)]
        [SMCConditionalDisplay(nameof(ContatoSetorMatricula), SMCConditionalOperation.NotEqual, "")]
        public string ContatoSetorMatricula { get; set; }
    }
}