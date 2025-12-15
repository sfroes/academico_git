 
using System;

namespace SMC.Academico.Common.Areas.FIN.Constants
{
    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_FIN_001_01_01.
	/// </summary>
    public static class UC_FIN_001_01_01
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo manter o cadastro de tipo de benefício, permitindo todas as funcionalidades de um CRUD básico (consulta, cadastro, alteração e exclusão)    É um cadastro dynamic.    
	    /// </summary>
        public const string MANTER_TIPO_BENEFICIO = "UC_FIN_001_01_01.Manter_Tipo_Beneficio";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_FIN_001_02_01.
	/// </summary>
    public static class UC_FIN_001_02_01
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo pesquisar os benefícios, permitindo também a exclusão.    
	    /// </summary>
        public const string PESQUISAR_BENEFICIO = "UC_FIN_001_02_01.Pesquisar_Beneficio";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_FIN_001_02_02.
	/// </summary>
    public static class UC_FIN_001_02_02
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo cadastrar ou alterar os dados de um benefício.  
	    /// </summary>
        public const string MANTER_BENEFICIO = "UC_FIN_001_02_02.Manter_Beneficio";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_FIN_001_03_01.
	/// </summary>
    public static class UC_FIN_001_03_01
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo pesquisar os benefícios associados a uma pessoa atuação, permitindo também a exclusão.
	    /// </summary>
        public const string PESQUISAR_ASSOCIACAO_BENEFICIO_PESSOA_ATUACAO = "UC_FIN_001_03_01.Pesquisar_Associacao_Beneficio_Pessoa_Atuacao";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_FIN_001_03_02.
	/// </summary>
    public static class UC_FIN_001_03_02
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo associar benefícios a uma pessoa atuação.
	    /// </summary>
        public const string ASSOCIAR_BENEFICIO_PESSOA_ATUACAO = "UC_FIN_001_03_02.Associar_Beneficio_Pessoa_Atuacao";

        /// <summary>
	    /// Este token permite:  - Habilitar o campo SOMENTE quando o benefício for de um tipo de benefício configurado com o campo "Chancela setor bolsas" igual a "Não".
	    /// </summary>
        public const string CHANCELA_SETOR_FINANCEIRO = "UC_FIN_001_03_02.Chancela_Setor_Financeiro";

        /// <summary>
	    /// Este token permite:  - Habilitar o campo SOMENTE quando o benefício for de um tipo de benefício configurado com o campo "Chancela setor bolsas" igual a "Sim".
	    /// </summary>
        public const string PERMITE_ALTERAR_CHANCELA_BENEFICIO = "UC_FIN_001_03_02.Permite_Alterar_Chancela_Beneficio";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_FIN_001_03_03.
	/// </summary>
    public static class UC_FIN_001_03_03
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo consultar todos os dados de uma benefício que está associado à uma pessoa-atuação.
	    /// </summary>
        public const string CONSULTAR_BENEFÍCIO_PESSOA_ATUACAO = "UC_FIN_001_03_03.Consultar_Benefício_Pessoa_atuacao";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_FIN_001_03_04.
	/// </summary>
    public static class UC_FIN_001_03_04
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo alterar a vigência de um benefício
	    /// </summary>
        public const string ALTERAR_VIGENCIA_BENEFÍCIO = "UC_FIN_001_03_04.Alterar_Vigencia_Benefício";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_FIN_001_05_01.
	/// </summary>
    public static class UC_FIN_001_05_01
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo pesquisar as pessoas jurídicas, permitindo também a exclusão.    
	    /// </summary>
        public const string PESQUISAR_PESSOA_JURIDICA = "UC_FIN_001_05_01.Pesquisar_Pessoa_Juridica";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_FIN_001_05_02.
	/// </summary>
    public static class UC_FIN_001_05_02
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo cadastrar ou alterar os dados de uma pessoa jurídica.  
	    /// </summary>
        public const string MANTER_PESSOA_JURIDICA = "UC_FIN_001_05_02.Manter_Pessoa_Juridica";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_FIN_001_06_01.
	/// </summary>
    public static class UC_FIN_001_06_01
    {

        /// <summary>
	    /// Este token tem como obejtivo exibir os campos "Exibir parcelas em aberto" e "Exibir referência do contrato no sistema financeiro", no relatório de alunos bolsistas.
	    /// </summary>
        public const string EXIBICAO_FILTROS_INFORMACOES_FINANCEIRAS = "UC_FIN_001_06_01.Exibicao_filtros_informacoes_financeiras";

        /// <summary>
	    /// Este caso de uso tem como objetivo realizar o filtro para relatório de bolsistas.
	    /// </summary>
        public const string PESQUISAR_ALUNOS_RELATORIO_BENEFÍCIO = "UC_FIN_001_06_01.Pesquisar_Alunos_Relatorio_Benefício";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_FIN_001_06_02.
	/// </summary>
    public static class UC_FIN_001_06_02
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo exibir o relatório de alunos bolsistas.
	    /// </summary>
        public const string EXIBIR_RELATORIO_ALUNOS_BOLSISTAS = "UC_FIN_001_06_02.Exibir_Relatorio_Alunos_Bolsistas";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_FIN_001_07_01.
	/// </summary>
    public static class UC_FIN_001_07_01
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo manter o cadastro dos motivos de alterações do benefício, permitindo todas as funcionalidades de um CRUD básico (consulta, cadastro, alteração e exclusão)    É um cadastro dynamic.
	    /// </summary>
        public const string MANTER_MOTIVO_ALTERACAO_BENEFICIO = "UC_FIN_001_07_01.Manter_Motivo_Alteracao_Beneficio";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_FIN_002_02_01.
	/// </summary>
    public static class UC_FIN_002_02_01
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo pesquisar os contratos, permitindo também a exclusão.
	    /// </summary>
        public const string PESQUISAR_CONTRATO = "UC_FIN_002_02_01.Pesquisar_Contrato";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_FIN_002_02_02.
	/// </summary>
    public static class UC_FIN_002_02_02
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo cadastrar ou alterar contratos.
	    /// </summary>
        public const string MANTER_CONTRATO = "UC_FIN_002_02_02.Manter_Contrato";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_FIN_002_02_03.
	/// </summary>
    public static class UC_FIN_002_02_03
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo pesquisar os termos de adesão, permitindo também a exclusão.
	    /// </summary>
        public const string PESQUISAR_TERMO_ADESAO = "UC_FIN_002_02_03.Pesquisar_Termo_Adesao";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_FIN_002_02_04.
	/// </summary>
    public static class UC_FIN_002_02_04
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo cadastrar ou alterar termos de adesão.
	    /// </summary>
        public const string MANTER_TERMO_ADESAO = "UC_FIN_002_02_04.Manter_Termo_Adesao";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_FIN_003_01_01.
	/// </summary>
    public static class UC_FIN_003_01_01
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo pesquisar as parametrizações de Benefício por Instituição Nível, permitindo também a exclusão.
	    /// </summary>
        public const string PESQUISAR_BENEFICIO_INSTITUICAO_NIVEL = "UC_FIN_003_01_01.Pesquisar_Beneficio_Instituicao_Nivel";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_FIN_003_01_02.
	/// </summary>
    public static class UC_FIN_003_01_02
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo cadastrar ou alterar as parâmetrizações de benefícios por Instituição Nível.
	    /// </summary>
        public const string MANTER_BENEFICIO_INSTITUICAO_NIVEL = "UC_FIN_003_01_02.Manter_Beneficio_Instituicao_Nivel";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_FIN_003_01_03.
	/// </summary>
    public static class UC_FIN_003_01_03
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo pesquisar os valores auxílio, permitindo também a exclusão.  
	    /// </summary>
        public const string PESQUISAR_VALOR_AUXILIO_BENEFICIO = "UC_FIN_003_01_03.Pesquisar_Valor_Auxilio_Beneficio";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_FIN_003_01_04.
	/// </summary>
    public static class UC_FIN_003_01_04
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo cadastrar ou alterar valores auxílio.
	    /// </summary>
        public const string MANTER_VALOR_AUXILIO_BENEFICIO = "UC_FIN_003_01_04.Manter_Valor_Auxilio_Beneficio";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_FIN_003_01_05.
	/// </summary>
    public static class UC_FIN_003_01_05
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo pesquisar as configuraçãoes de benefício, permitindo também a exclusão.
	    /// </summary>
        public const string PESQUISAR_CONFIGURACAO_BENEFICIO = "UC_FIN_003_01_05.Pesquisar_Configuracao_Beneficio";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_FIN_003_01_06.
	/// </summary>
    public static class UC_FIN_003_01_06
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo cadastrar ou alterar configurações de benefício.
	    /// </summary>
        public const string MANTER_CONFIGURACAO_BENEFICIO = "UC_FIN_003_01_06.Manter_Configuracao_Beneficio";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_FIN_004_01_01.
	/// </summary>
    public static class UC_FIN_004_01_01
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo possibilitar a emissão de boletos em aberto.
	    /// </summary>
        public const string EMITIR_BOLETO_ABERTO = "UC_FIN_004_01_01.Emitir_Boleto_Aberto";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_FIN_004_02_01.
	/// </summary>
    public static class UC_FIN_004_02_01
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo possibilitar o pagamento de parcelas em aberto com cartão de crédito ou débito.
	    /// </summary>
        public const string NEGOCIACAO_PARCELAS_CARTAO = "UC_FIN_004_02_01.Negociacao_Parcelas_Cartao";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_FIN_004_03_01.
	/// </summary>
    public static class UC_FIN_004_03_01
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo a visualização dos contratos de adesão à benefícios de agência de fomento.
	    /// </summary>
        public const string TERMO_CONCESSAO_BOLSA = "UC_FIN_004_03_01.Termo_Concessao_Bolsa";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_FIN_004_03_02.
	/// </summary>
    public static class UC_FIN_004_03_02
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo possibilitar a adesão de benefícios de agência de fomento.
	    /// </summary>
        public const string ADERIR_TERMO_CONCESSAO_BOLSA = "UC_FIN_004_03_02.Aderir_Termo_Concessao_Bolsa";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_FIN_004_04_01.
	/// </summary>
    public static class UC_FIN_004_04_01
    {

        /// <summary>
	    /// Esse caso de uso tem como objetivo a informação de parâmetros para emissão da declaração de pagamento no período.
	    /// </summary>
        public const string PESQUISAR_DECLARACAO_PAGAMENTO = "UC_FIN_004_04_01.Pesquisar_Declaracao_Pagamento";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_FIN_004_04_02.
	/// </summary>
    public static class UC_FIN_004_04_02
    {

        /// <summary>
	    /// Esse caso de uso tem como objetivo exibir o relatório da declaração de pagamento no período.
	    /// </summary>
        public const string EXIBIR_DECLARACAO_PAGAMENTO = "UC_FIN_004_04_02.Exibir_Declaracao_Pagamento";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_FIN_004_05_01.
	/// </summary>
    public static class UC_FIN_004_05_01
    {

        /// <summary>
	    /// Esse caso de uso tem como objetivo a informação de parâmetros para emissão da declaração de quitação anual de débitos no período.
	    /// </summary>
        public const string PESQUISAR_DECLARACAO_QUITACAO_ANUAL = "UC_FIN_004_05_01.Pesquisar_Declaracao_Quitacao_Anual";

    }

}

