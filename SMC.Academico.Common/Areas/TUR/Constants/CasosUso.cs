 
using System;

namespace SMC.Academico.Common.Areas.TUR.Constants
{
    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_TUR_001_01_01.
	/// </summary>
    public static class UC_TUR_001_01_01
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo pesquisar as turmas, permitindo também a alteração e exclusão.
	    /// </summary>
        public const string PESQUISAR_TURMA = "UC_TUR_001_01_01.Pesquisar_Turma";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_TUR_001_01_02.
	/// </summary>
    public static class UC_TUR_001_01_02
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo cadastrar uma turma, seja uma nova turma, um turma proveniente de desdobramento ou uma cópia de turma.
	    /// </summary>
        public const string MANTER_TURMA = "UC_TUR_001_01_02.Manter_Turma";

        /// <summary>
	    /// <font color="#000080">O usuário que possuir permissão neste token poderá alterar o perído letivo da turma.
	    /// </summary>
        public const string PERMITE_ALTERAR_PERIODO_LETIVO_TURMA = "UC_TUR_001_01_02.Permite_Alterar_Periodo_Letivo_Turma";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_TUR_001_01_03.
	/// </summary>
    public static class UC_TUR_001_01_03
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo reabrir o diário de uma turma.
	    /// </summary>
        public const string REABRIR_DIARIO = "UC_TUR_001_01_03.Reabrir_Diario";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_TUR_001_01_04.
	/// </summary>
    public static class UC_TUR_001_01_04
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo cancelar uma turma.
	    /// </summary>
        public const string CANCELAR_TURMA = "UC_TUR_001_01_04.Cancelar_Turma";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_TUR_001_02_01.
	/// </summary>
    public static class UC_TUR_001_02_01
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo pesquisar as associações de professores à turma, permitindo também a exclusão.
	    /// </summary>
        public const string PESQUISAR_ASSOCIACAO_PROFESSOR_TURMA = "UC_TUR_001_02_01.Pesquisar_Associacao_Professor_Turma";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_TUR_001_02_02.
	/// </summary>
    public static class UC_TUR_001_02_02
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo associar ou alterar associação de um ou mais colaboradores à divisões de turma.
	    /// </summary>
        public const string ASSOCIAR_PROFESSOR_DIVISAO_TURMA = "UC_TUR_001_02_02.Associar_Professor_Divisao_Turma";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_TUR_001_02_03.
	/// </summary>
    public static class UC_TUR_001_02_03
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo associar ou alterar associação de um ou mais colaboradores como responsável de uma turma.
	    /// </summary>
        public const string ASSOCIAR_PROFESSOR_RESPOSAVEL_TURMA = "UC_TUR_001_02_03.Associar_Professor_Resposavel_Turma";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_TUR_001_03_01.
	/// </summary>
    public static class UC_TUR_001_03_01
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo manter o cadastro de tipo de turma, permitindo todas as funcionalidades de um CRUD básico (consulta, cadastro, alteração e exclusão)    É um cadastro dynamic.
	    /// </summary>
        public const string MANTER_TIPO_TURMA = "UC_TUR_001_03_01.Manter_Tipo_Turma";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_TUR_001_04_01.
	/// </summary>
    public static class UC_TUR_001_04_01
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo realizar o filtro para relatório de turmas.
	    /// </summary>
        public const string PESQUISAR_TURMA = "UC_TUR_001_04_01.Pesquisar_Turma";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_TUR_001_04_02.
	/// </summary>
    public static class UC_TUR_001_04_02
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo exibir o relatório de turmas por ciclo letivo.
	    /// </summary>
        public const string EXIBIR_RELATORIO_TURMAS_CICLO_LETIVO = "UC_TUR_001_04_02.Exibir_Relatorio_Turmas_Ciclo_Letivo";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_TUR_001_05_01.
	/// </summary>
    public static class UC_TUR_001_05_01
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo visualizar os detalhes do compartilhamento de turma.
	    /// </summary>
        public const string VISUALIZAR_DETALHES_COMPARTILHAMENTO_TURMA = "UC_TUR_001_05_01.Visualizar_Detalhes_Compartilhamento_Turma";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_TUR_001_06_01.
	/// </summary>
    public static class UC_TUR_001_06_01
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo visualizar os detalhes de divisão de turma.
	    /// </summary>
        public const string VISUALIZAR_DETALHES_DIVISAO_TURMA = "UC_TUR_001_06_01.Visualizar_Detalhes_Divisao_Turma";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_TUR_001_07_01.
	/// </summary>
    public static class UC_TUR_001_07_01
    {

        /// <summary>
	    /// Esse caso de uso tem como objetivo listar as orientações de uma turma.
	    /// </summary>
        public const string PESQUISAR_ORIENTACAO_TURMA = "UC_TUR_001_07_01.Pesquisar_Orientacao_Turma";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_TUR_001_07_02.
	/// </summary>
    public static class UC_TUR_001_07_02
    {

        /// <summary>
	    /// Esse caso de uso tem como objetivo manter a orientação de uma turma.
	    /// </summary>
        public const string MANTER_ORIENTACAO_TURMA = "UC_TUR_001_07_02.Manter_Orientacao_Turma";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_TUR_001_08_01.
	/// </summary>
    public static class UC_TUR_001_08_01
    {

        /// <summary>
	    /// Esse caso de uso tem como objetivo manter as configurações de turma ou divisão de turma  
	    /// </summary>
        public const string ALTERAR_CONFIGURACAO_TURMA = "UC_TUR_001_08_01.Alterar_Configuracao_Turma";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_TUR_002_01_01.
	/// </summary>
    public static class UC_TUR_002_01_01
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo manter o cadastro de parâmetro de tipo de turma por Instituição e Nível de Ensino, permitindo todas as funcionalidades de um CRUD básico (consulta, cadastro, alteração e exclusão)    É um cadastro dynamic.
	    /// </summary>
        public const string MANTER_TIPO_TURMA_INSTITUICAOO_NIVEL = "UC_TUR_002_01_01.Manter_Tipo_Turma_Instituicaoo_Nivel";

    }

}

