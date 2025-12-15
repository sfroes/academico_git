 
using System;

namespace SMC.Academico.Common.Areas.ALN.Constants
{
    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_ALN_001_01_01.
	/// </summary>
    public static class UC_ALN_001_01_01
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo pesquisar os alunos.
	    /// </summary>
        public const string PESQUISAR_ALUNO = "UC_ALN_001_01_01.Pesquisar_Aluno";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_ALN_001_01_02.
	/// </summary>
    public static class UC_ALN_001_01_02
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo alterar alunos.
	    /// </summary>
        public const string MANTER_ALUNO = "UC_ALN_001_01_02.Manter_Aluno";

        /// <summary>
	    /// Permite incluir/alterar o seguinte dado da pessoa-atuação:  - Nome Social    Permite alterar os seguintes dados da pessoa-atuação:  - CPF  - Data de Nascimento  - Nacionalidade  - País de Origem  - Filiação      Quando os campos estiverem desabilitados, exibir a seguinte mensagem em um ícone informativo:  <i> "Somente o Administrador Acadêmico do Sistema pode alterar este campo."</i>
	    /// </summary>
        public const string PERMITIR_ALTERAR_CPF = "UC_ALN_001_01_02.Permitir_Alterar_CPF";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_ALN_001_01_03.
	/// </summary>
    public static class UC_ALN_001_01_03
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo importar os alunos do SGA legado.
	    /// </summary>
        public const string IMPORTAR_ALUNO = "UC_ALN_001_01_03.Importar_Aluno";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_ALN_001_02_02.
	/// </summary>
    public static class UC_ALN_001_02_02
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo visualizar os dados de matrícula do Aluno.
	    /// </summary>
        public const string VISUALIZAR_DADOS_MATRICULA = "UC_ALN_001_02_02.Visualizar_Dados_Matricula";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_ALN_001_03_01.
	/// </summary>
    public static class UC_ALN_001_03_01
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo pesquisar os vínculos de uma pessoa-atuação, permitindo também a exclusão.
	    /// </summary>
        public const string PESQUISAR_VINCULO = "UC_ALN_001_03_01.Pesquisar_Vinculo";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_ALN_001_03_02.
	/// </summary>
    public static class UC_ALN_001_03_02
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo cadastrar ou alterar os vínculos de uma pessoa-atuação.
	    /// </summary>
        public const string MANTER_VINCULO = "UC_ALN_001_03_02.Manter_Vinculo";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_ALN_001_04_01.
	/// </summary>
    public static class UC_ALN_001_04_01
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo a associação de formação específia ao aluno.
	    /// </summary>
        public const string ASSOCIAR_FORMACAO_ESPECIFICA_ALUNO = "UC_ALN_001_04_01.Associar_Formacao_Especifica_Aluno";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_ALN_001_06_01.
	/// </summary>
    public static class UC_ALN_001_06_01
    {

        /// <summary>
	    /// Esse caso de uso tem como objetivo a emissão do relatório de identidade estudantil.
	    /// </summary>
        public const string EXIBIR_RELATORIO_IDENTIDADE_ESTUDANTIL = "UC_ALN_001_06_01.Exibir_Relatorio_Identidade_Estudantil";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_ALN_001_08_01.
	/// </summary>
    public static class UC_ALN_001_08_01
    {

        /// <summary>
	    /// Permite a emissão de declarações genéricas de alunos gerenciadas pelo Centro de Registros Acadêmicos (CRA).
	    /// </summary>
        public const string EMISSAO_DECLARACOES_GENERICAS_ALUNOS_CRA = "UC_ALN_001_08_01.Emissao_Declaracoes_Genericas_Alunos_CRA";

        /// <summary>
	    /// Permite a emissão de declarações genéricas de alunos gerenciadas pelos Programas de Pós-graduação.
	    /// </summary>
        public const string EMISSAO_DECLARACOES_GENERICAS_ALUNOS_PROPPG = "UC_ALN_001_08_01.Emissao_Declaracoes_Genericas_Alunos_PROPPg";

        /// <summary>
	    /// Permite a emissão de declarações genéricas de alunos gerenciadas pelas secretarias acadêmicas do Campus Coração Eucarístico
	    /// </summary>
        public const string EMISSAO_DECLARACOES_GENERICAS_ALUNOS_SECRETARIAS_COREU = "UC_ALN_001_08_01.Emissao_Declaracoes_Genericas_Alunos_Secretarias_Coreu";

        /// <summary>
	    /// Este caso de uso tem como objetivo a pesquisa de alunos para a emissão de relatórios.    Relatórios disponíveis:  <ul>   <li>Declaração de disciplinas cursadas</li>   <li>Declaração de matrícula</li>   <li>Declaração genérica</li>   <li>Identidade estudantil</li>   <li>Histórico escolar</li>   <li>Histórico escolar interno</li>   <li>Listagem de alunos</li>  </ul>
	    /// </summary>
        public const string PESQUISAR_ALUNO_RELATORIO = "UC_ALN_001_08_01.Pesquisar_Aluno_Relatorio";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_ALN_001_08_02.
	/// </summary>
    public static class UC_ALN_001_08_02
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo o preenchimento e conferência de informações das tags utilizadas no tipo de declaração genérica sendo emitido.
	    /// </summary>
        public const string INFORMAR_TAGS_DECLARACAO_GENERICA = "UC_ALN_001_08_02.Informar_Tags_Declaracao_Generica";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_ALN_001_09_01.
	/// </summary>
    public static class UC_ALN_001_09_01
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo cancelar um aluno por descumprimento de normas do curso.
	    /// </summary>
        public const string CANCELAR_MATRICULA = "UC_ALN_001_09_01.Cancelar_matricula";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_ALN_001_10_01.
	/// </summary>
    public static class UC_ALN_001_10_01
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo exibir lista de alunos.
	    /// </summary>
        public const string EXIBIR_RELATORIO_LISTAGEM_ASSINATURA = "UC_ALN_001_10_01.Exibir_Relatorio_Listagem_Assinatura";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_ALN_001_11_01.
	/// </summary>
    public static class UC_ALN_001_11_01
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo informar os filtros de pesquisa para o relatório de previsão de conclusão e orientação de alunos.
	    /// </summary>
        public const string PESQUISAR_PREVISAO_CONCLUSAO_ORIENTACAO_ALUNOS = "UC_ALN_001_11_01.Pesquisar_Previsao_Conclusao_Orientacao_Alunos";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_ALN_001_11_02.
	/// </summary>
    public static class UC_ALN_001_11_02
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo exibir relatório de alunos com a previsão de conclusão e orientação.
	    /// </summary>
        public const string EXIBIR_PREVISAO_CONCLUSAO_ORIENTACAO_ALUNOS = "UC_ALN_001_11_02.Exibir_Previsao_Conclusao_Orientacao_Alunos";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_ALN_001_12_01.
	/// </summary>
    public static class UC_ALN_001_12_01
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo a emissão do relatório de declaração genérica, de acordo com os parâmetros e valores de tags informados pelo usuário.
	    /// </summary>
        public const string EMITIR_RELATORIO_DECLARACAO_GENERICA = "UC_ALN_001_12_01.Emitir_Relatorio_Declaracao_Generica";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_ALN_002_01_01.
	/// </summary>
    public static class UC_ALN_002_01_01
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo pesquisar os ingressantes, permitindo também a exclusão.
	    /// </summary>
        public const string PESQUISAR_INGRESSANTE = "UC_ALN_002_01_01.Pesquisar_Ingressante";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_ALN_002_01_02.
	/// </summary>
    public static class UC_ALN_002_01_02
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo cadastrar ou alterar ingressantes.
	    /// </summary>
        public const string MANTER_INGRESSANTE = "UC_ALN_002_01_02.Manter_Ingressante";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_ALN_002_01_03.
	/// </summary>
    public static class UC_ALN_002_01_03
    {

        /// <summary>
	    /// Esse caso de uso tem como objetivo consultar o histórico de situações de um ingressante.
	    /// </summary>
        public const string CONSULTAR_SITUACOES_INGRESSANTE = "UC_ALN_002_01_03.Consultar_Situacoes_Ingressante";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_ALN_002_01_04.
	/// </summary>
    public static class UC_ALN_002_01_04
    {

        /// <summary>
	    /// Esse caso de uso tem como objetivo o cadastro de tickets para um ingressante.
	    /// </summary>
        public const string MANTER_TICKET = "UC_ALN_002_01_04.Manter_Ticket";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_ALN_002_03_01.
	/// </summary>
    public static class UC_ALN_002_03_01
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo pesquisar os ingressantes para associação de dados acadêmicos e financeiros em lote.
	    /// </summary>
        public const string PESQUISAR_ASSOCIACAO_DADOS_ACADEMICOS_INGRESSANTE_LOTE = "UC_ALN_002_03_01.Pesquisar_Associacao_Dados_Academicos_Ingressante_Lote";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_ALN_002_03_02.
	/// </summary>
    public static class UC_ALN_002_03_02
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo associar formação específica aos ingressantes selecionados.
	    /// </summary>
        public const string ASSOCIAR_FORMACAO_ESPECIFICA = "UC_ALN_002_03_02.Associar_Formacao_Especifica";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_ALN_002_03_03.
	/// </summary>
    public static class UC_ALN_002_03_03
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo associar orientador aos ingressantes selecionados.
	    /// </summary>
        public const string ASSOCIAR_ORIENTADOR = "UC_ALN_002_03_03.Associar_Orientador";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_ALN_002_08_01.
	/// </summary>
    public static class UC_ALN_002_08_01
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo associar condições de obrigatoriedade a um ingressante.
	    /// </summary>
        public const string ASSOCIAR_CONDICAO_OBRIGATORIEDADE = "UC_ALN_002_08_01.Associar_Condicao_Obrigatoriedade";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_ALN_003_01_01.
	/// </summary>
    public static class UC_ALN_003_01_01
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo pesquisar uma associação de vínculo a um nível de ensino permitindo também a exclusão de associação.
	    /// </summary>
        public const string PESQUISAR_VINCULO_INSTITUICAO_NIVEL = "UC_ALN_003_01_01.Pesquisar_Vinculo_Instituicao_Nivel";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_ALN_003_01_02.
	/// </summary>
    public static class UC_ALN_003_01_02
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo cadastrar ou alterar uma associação de vínculo a um nível de ensino.
	    /// </summary>
        public const string MANTER_VINCULO_INSTITUICAO_NIVEL = "UC_ALN_003_01_02.Manter_Vinculo_Instituicao_Nivel";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_ALN_003_01_03.
	/// </summary>
    public static class UC_ALN_003_01_03
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo a associação de tipos de orientação por tipo de vínculo do aluno.
	    /// </summary>
        public const string ASSOCIAR_TIPO_ORIENTACAO = "UC_ALN_003_01_03.Associar_Tipo_Orientacao";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_ALN_004_01_01.
	/// </summary>
    public static class UC_ALN_004_01_01
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo pesquisar as parcerias de intercâmbio, permitindo também a exclusão.
	    /// </summary>
        public const string PESQUISAR_PARCERIA_INTERCAMBIO = "UC_ALN_004_01_01.Pesquisar_Parceria_Intercambio";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_ALN_004_01_02.
	/// </summary>
    public static class UC_ALN_004_01_02
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo cadastrar ou alterar parcerias de intercâmbio.
	    /// </summary>
        public const string MANTER_PARCERIA_INTERCAMBIO = "UC_ALN_004_01_02.Manter_Parceria_Intercambio";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_ALN_004_01_03.
	/// </summary>
    public static class UC_ALN_004_01_03
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo pesquisar os termos de intercâmbio de uma parceria, permitindo também a exclusão.
	    /// </summary>
        public const string PESQUISAR_TERMO_INTERCAMBIO = "UC_ALN_004_01_03.Pesquisar_Termo_Intercambio";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_ALN_004_01_04.
	/// </summary>
    public static class UC_ALN_004_01_04
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo cadastrar ou alterar termos de intercâmbios de uma parceria.
	    /// </summary>
        public const string MANTER_TERMO_INTERCAMBIO = "UC_ALN_004_01_04.Manter_Termo_Intercambio";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_ALN_004_01_05.
	/// </summary>
    public static class UC_ALN_004_01_05
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo pesquisar as associações de benefícios a um termo de intercâmbio, permitindo também a exclusão.
	    /// </summary>
        public const string PESQUISAR_BENEFICIO_TERMO_INTERCAMBIO = "UC_ALN_004_01_05.Pesquisar_Beneficio_Termo_Intercambio";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_ALN_004_01_06.
	/// </summary>
    public static class UC_ALN_004_01_06
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo cadastrar ou alterar associações de benefícios a um termo de intercâmbio.
	    /// </summary>
        public const string ASSOCIAR_BENEFICIO_TERMO_INTERCAMBIO = "UC_ALN_004_01_06.Associar_Beneficio_Termo_Intercambio";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_ALN_004_02_01.
	/// </summary>
    public static class UC_ALN_004_02_01
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo manter o cadastro de tipo de termo, permitindo todas as funcionalidades de um CRUD básico (consulta, cadastro, alteração e exclusão)    É um cadastro dynamic.
	    /// </summary>
        public const string TIPO_TERMO_INTERCAMBIO = "UC_ALN_004_02_01.Tipo_Termo_Intercambio";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_ALN_004_03_01.
	/// </summary>
    public static class UC_ALN_004_03_01
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo pesquisar os alunos intercâmbistas.
	    /// </summary>
        public const string PESQUISAR_INTERCAMBIO = "UC_ALN_004_03_01.Pesquisar_Intercambio";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_ALN_004_03_02.
	/// </summary>
    public static class UC_ALN_004_03_02
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo alterar um período de intercambio do aluno.  
	    /// </summary>
        public const string MANTER_INTERCAMBIO = "UC_ALN_004_03_02.Manter_Intercambio";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_ALN_005_01_01.
	/// </summary>
    public static class UC_ALN_005_01_01
    {

        /// <summary>
	    /// Esse caso de uso tem como objetivo selecionar o vínculo do aluno.
	    /// </summary>
        public const string SELECIONAR_VINCULO = "UC_ALN_005_01_01.Selecionar_Vinculo";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_ALN_005_01_02.
	/// </summary>
    public static class UC_ALN_005_01_02
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo apresentar a página inicial (home) dos alunos.
	    /// </summary>
        public const string HOME_PORTAL_ALUNO = "UC_ALN_005_01_02.Home_Portal_Aluno";

        /// <summary>
	    /// Liberar visualização de banner, turmas e linha do tempo
	    /// </summary>
        public const string PERMITIR_VISUALIZAR_BANNER_TURMAS_LINHA_DO_TEMPO = "UC_ALN_005_01_02.Permitir_Visualizar_Banner_Turmas_Linha_do_Tempo";

        /// <summary>
	    /// Liberar visualização do link do BDP
	    /// </summary>
        public const string PERMITIR_VISUALIZAR_LINK_BDP = "UC_ALN_005_01_02.Permitir_Visualizar_Link_Bdp";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_ALN_005_01_03.
	/// </summary>
    public static class UC_ALN_005_01_03
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo visualizar as notas e frequencia de um aluno em uma turma.
	    /// </summary>
        public const string VISUALIZAR_NOTAS_FREQUENCIA = "UC_ALN_005_01_03.Visualizar_Notas_Frequencia";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_ALN_005_02_01.
	/// </summary>
    public static class UC_ALN_005_02_01
    {

        /// <summary>
	    /// Esse caso de uso tem como objetivo a pesquisa de soliciações de um aluno.
	    /// </summary>
        public const string PESQUISAR_SOLICITACAO_ALUNO = "UC_ALN_005_02_01.Pesquisar_Solicitacao_Aluno";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_ALN_005_02_02.
	/// </summary>
    public static class UC_ALN_005_02_02
    {

        /// <summary>
	    /// Esse caso de uso tem como objtetivo a seleção de um tipo de serviço para criar uma nova solicitação para o aluno.
	    /// </summary>
        public const string MANTER_SOLICITACAO_ALUNO = "UC_ALN_005_02_02.Manter_Solicitacao_Aluno";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_ALN_005_02_03.
	/// </summary>
    public static class UC_ALN_005_02_03
    {

        /// <summary>
	    /// Esse caso de uso tem como objetivo o cancelamento de uma solicitação do aluno.
	    /// </summary>
        public const string CANCELAR_SOLICITACAO_ALUNO = "UC_ALN_005_02_03.Cancelar_Solicitacao_Aluno";

    }

}

