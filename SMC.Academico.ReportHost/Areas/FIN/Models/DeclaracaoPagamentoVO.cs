using SMC.Framework.Mapper;
using System;

namespace SMC.Academico.ReportHost.Areas.MAT.Models
{
    public class DeclaracaoPagamentoVO : ISMCMappable
    {
        public string Bairro { get; set; }
        public string Cep { get; set; }
        public string Cidade { get; set; }
        public string CnpjEmpresa { get; set; }
        public int CodigoAlunoMatriculado { get; set; }
        public string CodigoAutenticacao { get; set; }
        public string CpfAluno { get; set; }
        public DateTime DataEmissao { get; set; }
        public DateTime DataPagamento { get; set; }
        public DateTime DataPagamentoMaxima { get; set; }
        public DateTime DataPagamentoMinimo { get; set; }
        public DateTime? DataVencimentoTitulo { get; set; }
        public string DescricaoEmpresa { get; set; }
        public string DescricaoInstituicao { get; set; }
        public string DescricaoServicoMatriculado { get; set; }
        public string Endereco { get; set; }
        public string Estado { get; set; }
        public string NomeAluno { get; set; }
        public decimal ValorChequeDevNBaixado { get; set; }
        public decimal ValorChequeDevolvido { get; set; }
        public decimal ValorOP { get; set; }
        public decimal ValorPagamento { get; set; }
    }
}