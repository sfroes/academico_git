using SMC.Academico.Common.Areas.ALN.Enums;
using SMC.Academico.Domain.Areas.MAT.ValueObjects;
using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.FIN.ValueObjects
{
    public class AdesaoContratoVO : ISMCMappable
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

        public long SeqPessoaAtuacao { get; set; }

        public string NomeContrato { get; set; }

        public string Servicos { get; set; }

        public DateTime DataInicioServico { get; set; }

        public DateTime DataFimServico { get; set; }

        public List<InformacaoFinanceiraTermoAcademicoVO> InformacoesFinanceiras { get; set; }

        public List<PessoaAtuacaoBeneficioMatriculaVO> InformacoesBeneficios { get; set; }

        public TipoAluno TipoAluno { get; set; }

        public long? NumeroRegistroAcademico { get; set; }

        public string DescricaoCicloLetivo { get; set; }
    }
}