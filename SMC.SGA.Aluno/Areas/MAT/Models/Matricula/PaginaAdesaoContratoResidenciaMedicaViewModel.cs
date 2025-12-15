using SMC.Academico.Common.Areas.ALN.Constants;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.SGA.Aluno.Areas.MAT.Views.Matricula.App_LocalResources;
using System;
using System.Collections.Generic;

namespace SMC.SGA.Aluno.Areas.MAT.Models.Matricula
{
    public class PaginaAdesaoContratoResidenciaMedicaViewModel : MatriculaPaginaViewModelBase
    {
        public override string Token => MatriculaTokens.ADESAO_CONTRATO_LATO_SENSU;

        public override string ChaveTextoBotaoProximo => "Botao_Confirmar";

        public string Nome { get; set; }

        public string RG { get; set; }

        [SMCCpf]
        public string CPF { get; set; }

        public string Passaporte { get; set; }

        public string EntidadeResponsavel { get; set; }

        public string OfertaCurso { get; set; }

        public string Modalidade { get; set; }

        public int? PrazoConclusao { get; set; }

        public string PrazoConclusaoMeses { get { return $"{PrazoConclusao} {UIResource.Texto_Meses}"; } }

        public DateTime Inicio { get; set; }

        public DateTime TerminoPrevisto { get; set; }

        public DateTime? DataAdesao { get; set; }

        public Guid? CodigoAdesao { get; set; }

        public string TermoAdesao { get; set; }

        public long SeqArquivoContrato { get; set; }

        public long? SeqArquivoTermoAdesao { get; set; }

        [SMCSize(Framework.SMCSize.Grid24_24)]
        [SMCRequired]
        public bool AceitoTermo { get; set; }

        public string NomeContrato { get; set; }

        public string Servicos { get; set; }

        public DateTime DataInicioServico { get; set; }

        public DateTime DataFimServico { get; set; }

        public List<AdesaoContratoBeneficiosViewModel> InformacoesBeneficios { get; set; }
    }
}