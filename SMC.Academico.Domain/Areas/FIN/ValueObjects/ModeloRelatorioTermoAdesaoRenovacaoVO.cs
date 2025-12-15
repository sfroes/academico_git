namespace SMC.Academico.Domain.Areas.FIN.ValueObjects
{
    public class ModeloRelatorioTermoAdesaoRenovacaoVO
    {
        public string RA { get; set; }

        public string Nome { get; set; }

        public string RG { get; set; }

        public string CPF { get; set; }

        public string ResponsavelFinanceiro { get; set; }

        public string EntidadeResponsavel { get; set; }

        public string OfertaCurso { get; set; }

        public string Modalidade { get; set; }

        public string PrazoConclusao { get; set; }

        public string Inicio { get; set; }

        public string TerminoPrevisto { get; set; }

        public string Servicos { get; set; }

        public string InicioServicoAdicional { get; set; }

        public string FimServicoAdicional { get; set; }

        public string DescricaoServicoPadrao { get; set; }

        public string NumeroParcelas { get; set; }

        public string ValorParcela { get; set; }

        public string InicioParcela { get; set; }

        public string DiaVencimentoParcela { get; set; }


        public string NumeroParcelasAdicional { get; set; }

        public string ValorParcelaAdicional { get; set; }

        public string InicioParcelaAdicional { get; set; }

        public string DiaVencimentoParcelaAdicional { get; set; }

        public string TermoAdesao { get; set; }

        public string DataAdesao { get; set; }

        public string CodigoAdesao { get; set; }

        public string NomeContrato { get; set; }

        public string DescricaoCicloLetivo { get; set; }

        public int QuantidadeParcelasRestantes { get; set; }
    }
}