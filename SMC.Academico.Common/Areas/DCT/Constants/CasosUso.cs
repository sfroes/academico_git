 
using System;

namespace SMC.Academico.Common.Areas.DCT.Constants
{
    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_DCT_001_03_01.
	/// </summary>
    public static class UC_DCT_001_03_01
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo manter o cadastro de tipos de vínculos de um colaborador, permitindo todas as funcionalidades de um CRUD básico (consulta, cadastro, alteração e exclusão)    É um cadastro dynamic.
	    /// </summary>
        public const string MANTER_TIPO_VINCULO_COLABORADOR = "UC_DCT_001_03_01.Manter_Tipo_Vinculo Colaborador";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_DCT_001_04_01.
	/// </summary>
    public static class UC_DCT_001_04_01
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo manter o cadastro de categorias de instituições de ensino, permitindo todas as funcionalidades de um CRUD básico (consulta, cadastro, alteração e exclusão)    É um cadastro dynamic.
	    /// </summary>
        public const string MANTER_CATEGORIA_INSTITUICAO_ENSINO = "UC_DCT_001_04_01.Manter_Categoria_Instituicao_Ensino";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_DCT_001_05_01.
	/// </summary>
    public static class UC_DCT_001_05_01
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo manter o cadastro de instituições de ensino ou pesquisa externas, permitindo todas as funcionalidades de um CRUD básico (consulta, cadastro, alteração e exclusão)    É um cadastro dynamic.
	    /// </summary>
        public const string MANTER_INSTITUICAO_EXTERNA = "UC_DCT_001_05_01.Manter_Instituicao_Externa";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_DCT_001_06_01.
	/// </summary>
    public static class UC_DCT_001_06_01
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo consultar os colaboradores permitindo também a exclusão.
	    /// </summary>
        public const string PESQUISAR_COLABORADOR = "UC_DCT_001_06_01.Pesquisar_Colaborador";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_DCT_001_06_02.
	/// </summary>
    public static class UC_DCT_001_06_02
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo cadastrar ou alterar os dados de um professor/pesquisador.
	    /// </summary>
        public const string MANTER_COLABORADOR = "UC_DCT_001_06_02.Manter_Colaborador";

        /// <summary>
	    /// Permite alterar os dados pessoais e de contato do Colaborador.
	    /// </summary>
        public const string PERMITIR_ALTERAR_DADOS_COLABORADOR = "UC_DCT_001_06_02.Permitir_Alterar_Dados_Colaborador";

        /// <summary>
	    /// Usuários que tem permissão a este token poderá visualizar todos os tipos de vinculos parametrizados para a instituição logada e tipo da entidade selecionada.
	    /// </summary>
        public const string PERMITIR_SELECIONAR_TIPO_VINCULO = "UC_DCT_001_06_02.Permitir_Selecionar_Tipo_Vinculo";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_DCT_001_06_03.
	/// </summary>
    public static class UC_DCT_001_06_03
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo consultar os vínculos de um colaborador, permitindo também a exclusão.
	    /// </summary>
        public const string PESQUISAR_VINCULO_COLABORADOR = "UC_DCT_001_06_03.Pesquisar_Vinculo_Colaborador";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_DCT_001_06_04.
	/// </summary>
    public static class UC_DCT_001_06_04
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo cadastrar ou alterar os dados do vínculo de um colaborador.
	    /// </summary>
        public const string MANTER_VINCULO_COLABORADOR = "UC_DCT_001_06_04.Manter_Vinculo_Colaborador";

        /// <summary>
	    /// <font color="#000080">Permite alterar a data fim do vinculo dos docentes que estão com situação de demitido e afastado.
	    /// </summary>
        public const string PERMITIR_ALTERAR_DATA_FIM_VINCULO = "UC_DCT_001_06_04.PermitirAlterarDataFimVinculo";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_DCT_001_06_05.
	/// </summary>
    public static class UC_DCT_001_06_05
    {

        /// <summary>
	    /// Esse caso de uso tem como objetivo a associação de oferta de curso e tipo de atividade ao colaborador.
	    /// </summary>
        public const string ASSOCIAR_OFERTA_CURSO_TIPO_ATIVIDADE = "UC_DCT_001_06_05.Associar_Oferta_Curso_Tipo_Atividade";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_DCT_001_07_01.
	/// </summary>
    public static class UC_DCT_001_07_01
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo realizar o filtro para relatório de atualização de vinculo do colaborador.
	    /// </summary>
        public const string CENTRAL_RELATORIOS_COLABORADOR = "UC_DCT_001_07_01.Central_Relatorios_Colaborador";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_DCT_001_07_02.
	/// </summary>
    public static class UC_DCT_001_07_02
    {

        /// <summary>
	    /// <ul>   <li>Este caso de uso tem como objetivo exibir o log com as atualizações de vinculo do docente.</li>  </ul>
	    /// </summary>
        public const string EMITIR_RELATORIO_LOG_ATUALIZACAO_COLABORADOR = "UC_DCT_001_07_02.Emitir_Relatorio_Log_Atualizacao_Colaborador";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_DCT_001_07_03.
	/// </summary>
    public static class UC_DCT_001_07_03
    {

        /// <summary>
	    /// Emissão do certificado de cumprimento do estágio pós-doutoral dos colaboradores que possuem vínculo de pós-doutor.
	    /// </summary>
        public const string EMITIR_CERTIFICADO_POS_DOUTOR = "UC_DCT_001_07_03.Emitir_Certificado_Pos_Doutor";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_DCT_001_08_01.
	/// </summary>
    public static class UC_DCT_001_08_01
    {

        /// <summary>
	    /// Tem como objetivo pesquisar os componentes para os quais o professor/pesquisador esta apto a lecionar.
	    /// </summary>
        public const string PESQUISAR_COMPONENTE_APTO_LECIONAR = "UC_DCT_001_08_01.Pesquisar_Componente_Apto_Lecionar";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_DCT_001_08_02.
	/// </summary>
    public static class UC_DCT_001_08_02
    {

        /// <summary>
	    /// Permite associar os componentes que o professor/pesquisador esteja apto a lecionar.
	    /// </summary>
        public const string ASSOCIAR_COMPONENTE_APTO_LECIONAR = "UC_DCT_001_08_02.Associar_Componente_Apto_Lecionar";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_DCT_002_01_01.
	/// </summary>
    public static class UC_DCT_002_01_01
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo manter a associação dos tipos de vínculo de colaborador ao tipo de entidade da Instituição de Ensino logada, permitindo todas as funcionalidades de um CRUD básico (consulta, cadastro, alteração e exclusão).
	    /// </summary>
        public const string MANTER_TIPO_VINCULO_COLABORADOR_INSTITUICAO_TIPO_ENTIDADE = "UC_DCT_002_01_01.Manter_Tipo_Vinculo_Colaborador_Instituicao_Tipo_Entidade";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_DCT_002_02_01.
	/// </summary>
    public static class UC_DCT_002_02_01
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo manter o cadastro dos tipos de atividade de colaborador de um nível de ensino de uma instituição, permitindo todas as funcionalidades de um CRUD básico (consulta, cadastro, alteração e exclusão).
	    /// </summary>
        public const string MANTER_TIPO_ATIVIDADE_COLABORADOR_INSTITUICAO_NIVEL = "UC_DCT_002_02_01.Manter_Tipo_Atividade_Colaborador_Instituicao_Nivel";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_DCT_003_01_01.
	/// </summary>
    public static class UC_DCT_003_01_01
    {

        /// <summary>
	    /// Esse caso de uso tem como objetivo apresentar a página Home do Portal do Professor.
	    /// </summary>
        public const string HOME_PORTAL_PROFESSOR = "UC_DCT_003_01_01.Home_Portal_Professor";

    }

}

