using SMC.Academico.ServiceContract.Areas.FIN.Data.PessoaAtuacaoBeneficio;
using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.FIN.Data
{
    public class AdesaoContratoData : ISMCMappable
    {
        public string Nome { get; set; }

        public string RG { get; set; }

        public string CPF { get; set; }

        public string Passaporte { get; set; }

        public string ResponsavelFinanceiro { get; set; }

        public string EntidadeResponsavel { get; set; }

        public string OfertaCurso { get; set; }

        public string Modalidade { get; set; }

        public int? PrazoConclusao { get; set; }

        public DateTime Inicio { get; set; }

        public DateTime TerminoPrevisto { get; set; }

        public DateTime? DataAdesao { get; set; }

        public Guid? CodigoAdesao { get; set; }

        public string TermoAdesao { get; set; }

        public long SeqArquivoContrato { get; set; }

        public long? SeqArquivoTermoAdesao { get; set; }

        public bool AceitoTermo { get; set; }

        public string NomeContrato { get; set; }

        public string Servicos { get; set; }

        public DateTime DataInicioServico { get; set; }

        public DateTime DataFimServico { get; set; }

        public List<InformacaoFinanceiraAdesaoContratoData> InformacoesFinanceiras { get; set; }

        public List<PessoaAtuacaoBeneficioMatriculaData> InformacoesBeneficios { get; set; }

        public long SeqPessoaAtuacao { get; set; }
    }
}