using SMC.Academico.Common.Areas.ALN.Enums;
using SMC.Academico.Common.Areas.FIN.Enums;
using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.FIN.ValueObjects
{
    public class PessoaAtuacaoBeneficioVO : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqPessoaAtuacao { get; set; }

        public long? SeqBeneficio { get; set; }

        public long? SeqConfiguracaoBeneficio { get; set; }

        public int? CodigoAlunoMigracao { get; set; }

        public DateTime DataInicioVigencia { get; set; }

        public DateTime? DataFimVigencia { get; set; }

        [SMCMapProperty("PessoaAtuacao.ConfiguracaoBeneficio.TipoDeducao")]
        public string TipoDeducao { get; set; }

        public FormaDeducao FormaDeducao { get; set; }

        public decimal? ValorBeneficio { get; set; }

        public int SeqSituacaoChancelaBeneficioAtual { get; set; }

        public int? SeqContratoBeneficioFinanceiro { get; set; }

        public bool IncideParcelaMatricula { get; set; }

        public bool IncideParcelaMatriculaBanco { get; set; }

        public List<PessoaAtuacaoBeneficioResponsavelFinanceiroVO> ResponsaveisFinanceiro { get; set; }

        public List<BeneficioControleFinanceiroVO> ControlesFinanceiros { get; set; }

        public List<BeneficioHistoricoSituacaoVO> HistoricoSituacoes { get; set; }

        public ConfiguracaoBeneficioVO ConfiguracaoBeneficio { get; set; }

        public List<PessoaAtuacaoBeneficioAnexoVO> ArquivosAnexo { get; set; }

        public List<BeneficioHistoricoVigenciaVO> HistoricoVigencias { get; set; }

        public List<BeneficioEnvioNotificacaoVO> Notificacoes { get; set; }

        public string Justificativa { get; set; }

        public long SeqMotivoAlteracaoBeneficio { get; set; }
        public bool ParametroSetorBolsa { get; set; }

        #region DataSource

        public List<SMCDatasourceItem> SituacoesChancelaBeneficio { get; set; }

        #endregion

        #region Dados Cabeçalho

        [SMCMapProperty("DadosPessoais.Nome")]
        public string Nome { get; set; }

        [SMCMapProperty("DadosPessoais.NomeSocial")]
        public string NomeSocial { get; set; }

        [SMCMapProperty("Pessoa.Cpf")]
        public string Cpf { get; set; }

        [SMCMapProperty("Pessoa.Passaporte")]
        public string Passaporte { get; set; }

        [SMCMapProperty("Descricao")]
        public string DadosVinculo { get; set; }

        public string CondicaoPagamento { get; set; }

        #endregion Dados Cabeçalho

        #region Descricoes

        [SMCMapProperty("Beneficio.Descricao")]
        public string DescricaoBeneficio { get; set; }

        public string DescricaoConfiguracaoBeneficio { get; set; }

        public string DescricaoSituacaoBeneficio { get; set; }

        public string DescricaoFormaDeducao { get; set; }

        public string DescricaoTipoDeducao { get; set; }

        #endregion

        #region Hidens

        public int? IdTipoDeducao { get; set; }

        public int? IdFormaDeducao { get; set; }

        public int? IdAssociarResponsavelFinanceiro { get; set; }

        public bool? IdExisteResponsaveisFinanceiros { get; set; }

        public bool? IdDeducaoValorParcelaTitular { get; set; }

        public bool? IdIncideParcelaMatricula { get; set; }

        public bool Aluno { get; set; }

        public bool AlunoAtivo { get; set; }

        public SituacaoIngressante? SituacaoIngressante { get; set; }

        [SMCMapProperty("PessoaAtuacao.TipoAtuacao")]
        public TipoAtuacao TipoAtuacao { get; set; }

        public bool ExisteConfiguracaoBeneficio { get; set; }

        public bool ExibeValoresTermoAdesao { get; set; }

        #endregion Hidens
    }
}