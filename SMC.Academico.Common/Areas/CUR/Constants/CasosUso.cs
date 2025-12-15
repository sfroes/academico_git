 
using System;

namespace SMC.Academico.Common.Areas.CUR.Constants
{
    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_CUR_001_01_01.
	/// </summary>
    public static class UC_CUR_001_01_01
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo consultar os currículos e suas ofertas de curso, permitindo também a exclusão.
	    /// </summary>
        public const string PESQUISAR_CURRICULO = "UC_CUR_001_01_01.Pesquisar_Curriculo";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_CUR_001_01_02.
	/// </summary>
    public static class UC_CUR_001_01_02
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo cadastrar ou alterar os dados de um currículo.
	    /// </summary>
        public const string MANTER_CURRICULO = "UC_CUR_001_01_02.Manter_Curriculo";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_CUR_001_01_03.
	/// </summary>
    public static class UC_CUR_001_01_03
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo consultar os grupos curriculares de um currículo, permitindo também a exclusão dos grupos curriculares e dos componentes curriculares.
	    /// </summary>
        public const string PESQUISAR_GRUPO_CURRICULAR = "UC_CUR_001_01_03.Pesquisar_Grupo_Curricular";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_CUR_001_01_04.
	/// </summary>
    public static class UC_CUR_001_01_04
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo cadastrar ou alterar os dados de um grupo curricular.
	    /// </summary>
        public const string MANTER_GRUPO_CURRICULAR = "UC_CUR_001_01_04.Manter_Grupo_Curricular";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_CUR_001_01_05.
	/// </summary>
    public static class UC_CUR_001_01_05
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo consultar os grupos curriculares associados a uma oferta de curso, associar novos grupos,  permitindo também a exclusão de associações.
	    /// </summary>
        public const string PESQUISAR_ASSOCIACAO_GRUPO_CURRICULAR_OFERTA_CURSO = "UC_CUR_001_01_05.Pesquisar_Associacao_Grupo_Curricular_Oferta_Curso";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_CUR_001_01_06.
	/// </summary>
    public static class UC_CUR_001_01_06
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo associar grupos curriculares a uma oferta de curso.
	    /// </summary>
        public const string ASSOCIAR_GRUPO_CURRICULAR_OFERTA_CURSO = "UC_CUR_001_01_06.Associar_Grupo_Curricular_Oferta_Curso";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_CUR_001_01_07.
	/// </summary>
    public static class UC_CUR_001_01_07
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo associar componente curricular ao grupo selecionado.
	    /// </summary>
        public const string ASSOCIAR_COMPONENTE_CURRICULAR_GRUPO = "UC_CUR_001_01_07.Associar_Componente_Curricular_Grupo";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_CUR_001_02_01.
	/// </summary>
    public static class UC_CUR_001_02_01
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo consultar as divisões curriculares, permitindo também a exclusão das mesmas.
	    /// </summary>
        public const string PESQUISAR_DIVISAO_CURRICULAR = "UC_CUR_001_02_01.Pesquisar_Divisao_Curricular";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_CUR_001_02_02.
	/// </summary>
    public static class UC_CUR_001_02_02
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo cadastrar ou alterar os dados da divisão e dos itens de divisão curricular.
	    /// </summary>
        public const string MANTER_DIVISAO_CURRICULAR = "UC_CUR_001_02_02.Manter_Divisao_Curricular";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_CUR_001_03_01.
	/// </summary>
    public static class UC_CUR_001_03_01
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo manter o cadastro de tipo de divisão curricular, permitindo todas as funcionalidades de um CRUD básico (consulta, cadastro, alteração e exclusão)    É um cadastro dynamic.
	    /// </summary>
        public const string MANTER_TIPO_DIVISAO_CURRICULAR = "UC_CUR_001_03_01.Manter_Tipo_Divisao_Curricular";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_CUR_001_04_01.
	/// </summary>
    public static class UC_CUR_001_04_01
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo manter o cadastro de tipo de grupo curricular, permitindo todas as funcionalidades de um CRUD básico (consulta, cadastro, alteração e exclusão) e também a associação/desassociação de tipos de componente a um grupo.    É um cadastro dynamic.
	    /// </summary>
        public const string MANTER_TIPO_GRUPO_CURRICULAR = "UC_CUR_001_04_01.Manter_Tipo_Grupo_Curricular";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_CUR_001_05_01.
	/// </summary>
    public static class UC_CUR_001_05_01
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo consultar as matrizes curriculares de uma oferta de curso, permitindo também a exclusão de matrizes.
	    /// </summary>
        public const string PESQUISAR_MATRIZ_CURRICULAR = "UC_CUR_001_05_01.Pesquisar_Matriz_Curricular";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_CUR_001_05_02.
	/// </summary>
    public static class UC_CUR_001_05_02
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo cadastrar ou alterar os dados de uma matriz curricular.
	    /// </summary>
        public const string MANTER_MATRIZ_CURRICULAR = "UC_CUR_001_05_02.Manter_Matriz_Curricular";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_CUR_001_05_03.
	/// </summary>
    public static class UC_CUR_001_05_03
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo permitir  a inclusão e a alteração de situações de uma matriz curricular.
	    /// </summary>
        public const string MANTER_HISTORICO_SITUACAO_MATRIZ_CURRICULAR = "UC_CUR_001_05_03.Manter_Historico_Situacao_Matriz_Curricular";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_CUR_001_05_04.
	/// </summary>
    public static class UC_CUR_001_05_04
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo consultar as configurações de um grupo curricular de uma matriz, permitindo também a exclusão de configuração de um grupo.
	    /// </summary>
        public const string PESQUISAR_CONFIGURACAO_GRUPO_MATRIZ = "UC_CUR_001_05_04.Pesquisar_Configuracao_Grupo_Matriz";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_CUR_001_05_05.
	/// </summary>
    public static class UC_CUR_001_05_05
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo cadastrar ou alterar os dados de uma configuração de um grupo curricular de uma matriz.
	    /// </summary>
        public const string MANTER_CONFIGURACAO_GRUPO_MATRIZ = "UC_CUR_001_05_05.Manter_Configuracao_Grupo_Matriz";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_CUR_001_05_06.
	/// </summary>
    public static class UC_CUR_001_05_06
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo consultar as configurações de um componente curricular de uma matriz.
	    /// </summary>
        public const string PESQUISAR_CONFIGURACAO_COMPONENTE_MATRIZ = "UC_CUR_001_05_06.Pesquisar_Configuracao_Componente_Matriz";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_CUR_001_05_07.
	/// </summary>
    public static class UC_CUR_001_05_07
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo cadastrar ou alterar os dados de uma configuração de um componente curricular de uma matriz.
	    /// </summary>
        public const string CONFIGURAR_COMPONENTE_MATRIZ = "UC_CUR_001_05_07.Configurar_Componente_Matriz";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_CUR_001_05_08.
	/// </summary>
    public static class UC_CUR_001_05_08
    {

        /// <summary>
	    /// Permite a exclusão de assunto de componente na matriz curricular.
	    /// </summary>
        public const string EXCLUSAO_ASSUNTO_COMPONENTE_MATRIZ = "UC_CUR_001_05_08.Exclusao_Assunto_Componente_Matriz";

        /// <summary>
	    /// Listar os assuntos associadas a configuração de componente curricular na matriz
	    /// </summary>
        public const string PESQUISAR_ASSUNTO_COMPONENTE_MATRIZ = "UC_CUR_001_05_08.Pesquisar_Assunto_Componente_Matriz";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_CUR_001_05_09.
	/// </summary>
    public static class UC_CUR_001_05_09
    {

        /// <summary>
	    /// Associar os assuntos à configuração de componente curricular na matriz
	    /// </summary>
        public const string MANTER_ASSUNTO_COMPONENTE_MATRIZ = "UC_CUR_001_05_09.Manter_Assunto_Componente_Matriz";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_CUR_001_06_01.
	/// </summary>
    public static class UC_CUR_001_06_01
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo consultar as divisões de uma matriz curricular.
	    /// </summary>
        public const string CONSULTA_DIVISOES_MATRIZ_CURRICULAR = "UC_CUR_001_06_01.Consulta_Divisoes_Matriz_Curricular";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_CUR_001_07_01.
	/// </summary>
    public static class UC_CUR_001_07_01
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo pesquisar matrizes curriculares para geração de relatórios.
	    /// </summary>
        public const string PESQUISAR_MATRIZ_CURRICULAR = "UC_CUR_001_07_01.Pesquisar_Matriz_Curricular";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_CUR_001_07_02.
	/// </summary>
    public static class UC_CUR_001_07_02
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo gerar relatórios de matrizes curriculares.
	    /// </summary>
        public const string EXIBIR_RELATORIO_MATRIZ_CURRICULAR = "UC_CUR_001_07_02.Exibir_Relatorio_Matriz_Curricular";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_CUR_001_08_01.
	/// </summary>
    public static class UC_CUR_001_08_01
    {

        /// <summary>
	    /// Esse caso de uso tem como objetivo o cadastro de tipos de configuração de grupo curricular.
	    /// </summary>
        public const string MANTER_TIPO_CONFIGURACAO_GRUPO_CURRICULAR = "UC_CUR_001_08_01.Manter_Tipo_Configuracao_Grupo_Curricular";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_CUR_001_09_01.
	/// </summary>
    public static class UC_CUR_001_09_01
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo manter o cadastro de condição de obrigatoriedade, permitindo todas as funcionalidades de um CRUD básico (consulta, cadastro, alteração e exclusão)    É um cadastro dynamic.
	    /// </summary>
        public const string MANTER_CONDICAO_OBRIGATORIEDADE = "UC_CUR_001_09_01.Manter_Condicao_Obrigatoriedade";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_CUR_002_01_01.
	/// </summary>
    public static class UC_CUR_002_01_01
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo consultar os componentes curriculares, permitindo também a exclusão.
	    /// </summary>
        public const string PESQUISAR_COMPONENTE_CURRICULAR = "UC_CUR_002_01_01.Pesquisar_Componente_Curricular";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_CUR_002_01_02.
	/// </summary>
    public static class UC_CUR_002_01_02
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo cadastrar ou alterar os dados de um componente curricular.
	    /// </summary>
        public const string MANTER_COMPONENTE_CURRICULAR = "UC_CUR_002_01_02.Manter_Componente_Curricular";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_CUR_002_01_03.
	/// </summary>
    public static class UC_CUR_002_01_03
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo consultar as configurações de um componente curricular, permitindo também a exclusão.
	    /// </summary>
        public const string PESQUISAR_CONFIGURACAO_COMPONENTE_CURRICULAR = "UC_CUR_002_01_03.Pesquisar_Configuracao_Componente_Curricular";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_CUR_002_01_04.
	/// </summary>
    public static class UC_CUR_002_01_04
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo cadastrar ou alterar os dados de uma configuração do componente curricular.
	    /// </summary>
        public const string MANTER_CONFIGURACAO_COMPONENTE_CURRICULAR = "UC_CUR_002_01_04.Manter_Configuracao_Componente_Curricular";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_CUR_002_02_01.
	/// </summary>
    public static class UC_CUR_002_02_01
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo consultar os tipos de componente curricular e seus tipos de divisões, permitindo também a exclusão.
	    /// </summary>
        public const string PESQUISAR_TIPO_COMPONENTE_CURRICULAR = "UC_CUR_002_02_01.Pesquisar_Tipo_Componente_Curricular";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_CUR_002_02_02.
	/// </summary>
    public static class UC_CUR_002_02_02
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo cadastrar ou alterar os dados de um tipo de componente curricular.
	    /// </summary>
        public const string MANTER_TIPO_COMPONENTE_CURRICULAR = "UC_CUR_002_02_02.Manter_Tipo_Componente_Curricular";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_CUR_002_03_01.
	/// </summary>
    public static class UC_CUR_002_03_01
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo consultar os detalhes de um componente curricular.
	    /// </summary>
        public const string VISUALIZAR_DETALHES_COMPONENTE_CURRICULAR = "UC_CUR_002_03_01.Visualizar_Detalhes_Componente_Curricular";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_CUR_002_04_01.
	/// </summary>
    public static class UC_CUR_002_04_01
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo consultar os grupos de configuração de componente curricular, permitindo também a exclusão.
	    /// </summary>
        public const string PESQUISAR_GRUPO_CONFIGURACAO_COMPONENTE = "UC_CUR_002_04_01.Pesquisar_Grupo_Configuracao_Componente";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_CUR_002_04_02.
	/// </summary>
    public static class UC_CUR_002_04_02
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo cadastrar ou alterar os dados de um grupo de configuração de componente curricular.
	    /// </summary>
        public const string MANTER_GRUPO_CONFIGURACAO_COMPONENTE = "UC_CUR_002_04_02.Manter_Grupo_Configuracao_Componente";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_CUR_002_06_01.
	/// </summary>
    public static class UC_CUR_002_06_01
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo consultar os periódicos CAPES importados.
	    /// </summary>
        public const string PESQUISAR_PERIODICO_CAPES = "UC_CUR_002_06_01.Pesquisar_Periodico_Capes";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_CUR_002_06_02.
	/// </summary>
    public static class UC_CUR_002_06_02
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo importar os periódicos CAPES
	    /// </summary>
        public const string IMPORTAR_ARQUIVO_CLASSIFICACAO = "UC_CUR_002_06_02.Importar_Arquivo_Classificacao";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_CUR_002_06_03.
	/// </summary>
    public static class UC_CUR_002_06_03
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo a manutenção de periódicos CAPES.
	    /// </summary>
        public const string MANTER_PERIODICO_CAPES = "UC_CUR_002_06_03.Manter_Periodico_Capes";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_CUR_003_01_01.
	/// </summary>
    public static class UC_CUR_003_01_01
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo pesquisar os requisitos de um componente ou divisão, considerando uma matriz curricular.
	    /// </summary>
        public const string PESQUISAR_REQUISITO = "UC_CUR_003_01_01.Pesquisar_Requisito";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_CUR_003_01_02.
	/// </summary>
    public static class UC_CUR_003_01_02
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo criar e alterar os requisitos de um componente ou divisão.  
	    /// </summary>
        public const string MANTER_REQUISITO = "UC_CUR_003_01_02.Manter_Requisito";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_CUR_003_02_01.
	/// </summary>
    public static class UC_CUR_003_02_01
    {

        /// <summary>
	    /// Esse caso de uso tem como objetivo a pesquisa de dispensas, permitindo também sua exclusão.
	    /// </summary>
        public const string PESQUISAR_DISPENSA = "UC_CUR_003_02_01.Pesquisar_Dispensa";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_CUR_003_02_02.
	/// </summary>
    public static class UC_CUR_003_02_02
    {

        /// <summary>
	    /// Esse caso de uso tem como objetivo o cadastro (inclusão e alteração) de dispensas.
	    /// </summary>
        public const string MANTER_DISPENSA = "UC_CUR_003_02_02. Manter_Dispensa";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_CUR_003_02_03.
	/// </summary>
    public static class UC_CUR_003_02_03
    {

        /// <summary>
	    /// Esse caso de uso tem como objetivo a associação de matrizes como exceção de uma dispensa.
	    /// </summary>
        public const string ASSOCIAR_MATRIZ_DISPENSA_COMPONENTE = "UC_CUR_003_02_03.Associar_Matriz_Dispensa_Componente";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_CUR_004_01_01.
	/// </summary>
    public static class UC_CUR_004_01_01
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo pesquisar uma associação de tipo de componente curricular a um nível de ensino permitindo também a exclusão de associação.
	    /// </summary>
        public const string PESQUISAR_TIPO_COMPONENTE_INSTITUICAO_NIVEL_ENSINO = "UC_CUR_004_01_01.Pesquisar_Tipo_Componente_Instituicao_Nivel_Ensino";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_CUR_004_01_02.
	/// </summary>
    public static class UC_CUR_004_01_02
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo  cadastrar ou alterar uma associação de tipo de componente curricular a um nível de ensino.
	    /// </summary>
        public const string MANTER_TIPO_COMPONENTE_INSTITUICAO_NIVEL_ENSINO = "UC_CUR_004_01_02.Manter_Tipo_Componente_Instituicao_Nivel_Ensino";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_CUR_004_02_01.
	/// </summary>
    public static class UC_CUR_004_02_01
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo manter o cadastro dos tipos de divisões curriculares de um nível de ensino, permitindo todas as funcionalidades de um CRUD básico (consulta, cadastro e exclusão).
	    /// </summary>
        public const string MANTER_TIPO_DIVISAO_CURRICULAR_INSTITUICAO_NIVEL_ENSINO = "UC_CUR_004_02_01.Manter_Tipo_Divisao_Curricular_Instituicao_Nivel_Ensino";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_CUR_004_03_01.
	/// </summary>
    public static class UC_CUR_004_03_01
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo manter o cadastro dos tipos de grupos curriculares de um nível de ensino, permitindo todas as funcionalidades de um CRUD básico (consulta, cadastro e exclusão).  
	    /// </summary>
        public const string MANTER_TIPO_GRUPO_CURRICULAR_INSTITUICAO_NIVEL_ENSINO = "UC_CUR_004_03_01.Manter_Tipo_Grupo_Curricular_Instituicao_Nivel_Ensino";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_CUR_004_04_01.
	/// </summary>
    public static class UC_CUR_004_04_01
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo manter o cadastro de condições de obrigatoriedade por um nível de ensino, permitindo todas as funcionalidades de um CRUD básico (consulta, cadastro e exclusão).
	    /// </summary>
        public const string MANTER_CONDICAO_OBRIGATORIEDADE_INSTITUICAO_NIVEL_ENSINO = "UC_CUR_004_04_01.Manter_Condicao_Obrigatoriedade_Instituicao_Nivel_Ensino";

    }

}

