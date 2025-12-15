using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System;

namespace SMC.SGA.Administrativo.Areas.FIN.Models
{
    public class RelatorioBolsistasViewModel : SMCViewModelBase, ISMCMappable
    {
        /*NV12 Trazer todas as pessoas-atuações com seus benefícios conforme filtros realizado em: 
         * UC_FIN_001_06_01 - Pesquisar Alunos com Benefício
          Deverão ser exibidos todos os benefícios cujo período de vigência esteja contemplado 
          parcial ou integralmente no no período filtrado.
          Ex.: se eu colocar no filtro o período 01/03/2019 à 29/02/2020, os benefícios com as 
          vigências abaixo seriam exibidos:

          · Benefício 1: 01/03/2018 a 29/02/2019
          · Benefício 2: 01/05/2019 a 31/12/2019
          · Benefício 3: 01/12/2019 a 31/12/2021
          · Benefício 4: 01/03/2018 a 31/12/2021 */

        [SMCKey]
        public long SeqPessoaAtuacaoBeneficio { get; set; }

        /// <summary>
        /// Exibir a sigla da entidade responsável.
        /// </summary>
        public string SiglaEntidadeResponsavel { get; set; }


        /// <summary>
        /// NV04 Os registros deverão ser listados em ordem alfabética pela 
        /// sigla da entidade responsável, em seguida pelo nome da pessoa-atuação.
        /// </summary>
        public string Nome { get; set; }

        /// <summary>
        /// NV05 Exibir a descrição da atuação da pessoa-atuação em questão.
        /// </summary>
        public string TipoAtuacao { get; set; }

        public long SeqPessoaAtuacao { get; set; }

        /// <summary>
        /// NV06 Exibir a descrição do benefício da pessoa-atuação.
        /// Quando o benefício da pessoa-atuação possuir configuração de benefício associada, 
        /// verificar qual é a forma de dedução.
        /// Se a forma de dedução for igual a "Percentual de bolsa", concatenar o percentual do benefício associado à pessoa
        /// atuação (campo val_benefício), conforme exemplo abaixo:
        /// · Bolsa CAPES - Modalidade I
        /// · Bolsa assistencial - 50%
        /// </summary>
        public string DescricaoBeneficio { get; set; }

        /// <summary>
        /// NV07 Exibir a data de início de vigência do benefício associado à pessoa-atuação
        /// </summary>
        public DateTime? DataInicioVigencia { get; set; }

        /// <summary>
        /// NV08 Exibir a data fim da vigência do benefício associado à pessoa-atuação
        /// </summary>
        public DateTime? DataFimVigencia { get; set; }

        /// <summary>
        /// NV09 Exibir a descrição da situação da chancela do benefício
        /// </summary>
        public string SituacaoChancelaBeneficio { get; set; }

        /// <summary>
        /// NV10 Essa coluna somente deverá ser exibida se o filtro "Exibir referência do contrato no sistema financeiro" estiver
        /// selecionado em UC_FIN_001_06_01 - Pesquisar Alunos com Benefício
        /// Exibir uma linha para cada descrição retornada na função do GRA:
        /// <Aguardando Julianne> Função a ser chamada para cada aluno a ser exibido no relatório
        /// A função do GRA irá retornar uma tabela,sendo uma linha para cada contrato do aluno no período filtrado
        /// </summary>
        public string ReferenciaFinanceira { get; set; }

        /// <summary>
        /// Essa coluna somente deverá ser exibida se o filtro "Exibir parcelas em aberto" estiver selecionado em
        /// UC_FIN_001_06_01 - Pesquisar Alunos com Benefício
        /// Exibir a lista de parcelas em aberto do aluno que é retornada na função do GRA:
        /// <Aguardando Julianne> Função a ser chamada para cada aluno a ser exibido no relatório
        /// </summary>
        public string ParcelasAbertas { get; set; }

		public string CPF { get; set; }

		public string DescricaoNivelEnsino { get; set; }

        public int? CodigoAlunoMigracao { get; set; }

    }
}