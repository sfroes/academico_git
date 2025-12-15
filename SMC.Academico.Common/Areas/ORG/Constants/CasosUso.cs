 
using System;

namespace SMC.Academico.Common.Areas.ORG.Constants
{
    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_ORG_001_01_01.
	/// </summary>
    public static class UC_ORG_001_01_01
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo pesquisar mantenedoras, permitindo também a exclusão.
	    /// </summary>
        public const string PESQUISAR_MANTENEDORA = "UC_ORG_001_01_01.Pesquisar_Mantenedora";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_ORG_001_01_02.
	/// </summary>
    public static class UC_ORG_001_01_02
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo cadastrar ou alterar os dados de uma mantenedora.
	    /// </summary>
        public const string MANTER_MANTENEDORA = "UC_ORG_001_01_02.Manter_Mantenedora";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_ORG_001_02_01.
	/// </summary>
    public static class UC_ORG_001_02_01
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo consultar instituições de ensino, permitindo também a exclusão.
	    /// </summary>
        public const string PESQUISAR_INSTITUICAO_ENSINO = "UC_ORG_001_02_01.Pesquisar_Instituicao_Ensino";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_ORG_001_02_02.
	/// </summary>
    public static class UC_ORG_001_02_02
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo cadastrar ou alterar os dados de uma instituição de ensino.
	    /// </summary>
        public const string MANTER_INSTITUICAO_ENSINO = "UC_ORG_001_02_02.Manter_Instituicao_Ensino";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_ORG_001_03_01.
	/// </summary>
    public static class UC_ORG_001_03_01
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo consultar os níveis de ensino de forma hierárquica, permitindo também a exclusão e reordenação da hierarquia.
	    /// </summary>
        public const string PESQUISAR_NIVEL_ENSINO = "UC_ORG_001_03_01.Pesquisar_Nivel_Ensino";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_ORG_001_03_02.
	/// </summary>
    public static class UC_ORG_001_03_02
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo cadastrar ou alterar os dados de um nível de ensino.
	    /// </summary>
        public const string MANTER_NIVEL_ENSINO = "UC_ORG_001_03_02.Manter_Nivel_Ensino";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_ORG_001_04_01.
	/// </summary>
    public static class UC_ORG_001_04_01
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo manter o cadastro de tipo de entidade, permitindo todas as funcionalidades de um CRUD básico (consulta, cadastro, alteração e exclusão). É um cadastro dynamic.
	    /// </summary>
        public const string MANTER_TIPO_ENTIDADE = "UC_ORG_001_04_01.Manter_Tipo_Entidade";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_ORG_001_05_01.
	/// </summary>
    public static class UC_ORG_001_05_01
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo consultar tipos de hierarquia de entidades, permitindo também a exclusão.
	    /// </summary>
        public const string PESQUISAR_TIPO_HIERARQUIA_ENTIDADE = "UC_ORG_001_05_01.Pesquisar_Tipo_Hierarquia_Entidade";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_ORG_001_05_02.
	/// </summary>
    public static class UC_ORG_001_05_02
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo cadastrar ou alterar os dados de um tipo de hierarquia de entidade.
	    /// </summary>
        public const string MANTER_TIPO_HIERARQUIA_ENTIDADE = "UC_ORG_001_05_02.Manter_Tipo_Hierarquia_Entidade";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_ORG_001_05_03.
	/// </summary>
    public static class UC_ORG_001_05_03
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo consultar a hierarquia de tipos de entidade, bem como adicionar ou excluir itens (tipos de entidade).
	    /// </summary>
        public const string MONTAR_HIERARQUIA_TIPO_ENTIDADE = "UC_ORG_001_05_03.Montar_Hierarquia_Tipo_Entidade";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_ORG_001_05_04.
	/// </summary>
    public static class UC_ORG_001_05_04
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo possibilitar a seleção dos tipos de entidade, para associação na hierarquia de tipos de entidade.
	    /// </summary>
        public const string ASSOCIAR_TIPO_ENTIDADE = "UC_ORG_001_05_04.Associar_Tipo_Entidade";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_ORG_001_06_01.
	/// </summary>
    public static class UC_ORG_001_06_01
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo consultar entidades, permitindo também a exclusão.
	    /// </summary>
        public const string PESQUISAR_ENTIDADE = "UC_ORG_001_06_01.Pesquisar_Entidade";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_ORG_001_06_02.
	/// </summary>
    public static class UC_ORG_001_06_02
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo cadastrar ou alterar os dados de uma entidade.
	    /// </summary>
        public const string MANTER_ENTIDADE = "UC_ORG_001_06_02.Manter_Entidade";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_ORG_001_07_01.
	/// </summary>
    public static class UC_ORG_001_07_01
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo consultar as hierarquias de entidades, permitindo também a exclusão.
	    /// </summary>
        public const string PESQUISAR_HIERARQUIA_ENTIDADE = "UC_ORG_001_07_01.Pesquisar_Hierarquia_Entidade";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_ORG_001_07_02.
	/// </summary>
    public static class UC_ORG_001_07_02
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo cadastrar ou alterar os dados de uma hierarquia de entidades.
	    /// </summary>
        public const string MANTER_HIERARQUIA_ENTIDADE = "UC_ORG_001_07_02.Manter_Hierarquia_Entidade";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_ORG_001_07_03.
	/// </summary>
    public static class UC_ORG_001_07_03
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo consultar a árvore entidade de uma hierarquia de entidades, permitindo também a exclusão de itens da árvore.
	    /// </summary>
        public const string MONTAR_HIERARQUIA_ENTIDADE = "UC_ORG_001_07_03.Montar_Hierarquia_Entidade";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_ORG_001_07_04.
	/// </summary>
    public static class UC_ORG_001_07_04
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo cadastrar ou alterar os itens da árvore de uma hierarquia de entidades.
	    /// </summary>
        public const string ASSOCIAR_ENTIDADE = "UC_ORG_001_07_04.Associar_Entidade";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_ORG_001_08_01.
	/// </summary>
    public static class UC_ORG_001_08_01
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo cadastrar ou alterar os dados de Situação.
	    /// </summary>
        public const string MANTER_SITUACAO = "UC_ORG_001_08_01.Manter_Situacao";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_ORG_001_10_01.
	/// </summary>
    public static class UC_ORG_001_10_01
    {

        /// <summary>
	    /// Poderá ser associada na entidade mais de uma situação e, para cada situação deverá ser informada a Data de Início e a Data de Término.     O período cadastrado por situação determina quando a situação estará disponível no cadastro das respectivas entidades.    A Manutenção de Situação será acionada a partir do cadastro das entidades não externalizadas e das entidades externalizadas*.    *Entidades Externalizadas = Programa, Curso, Curso Unidade e Curso Oferta Localidade.
	    /// </summary>
        public const string MANTER_SITUACAO_ENTIDADE = "UC_ORG_001_10_01.Manter_Situacao_Entidade";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_ORG_001_11_01.
	/// </summary>
    public static class UC_ORG_001_11_01
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo consultar os tipos de agenda, permitindo também a exclusão.  
	    /// </summary>
        public const string PESQUISAR_TIPO_AGENDA = "UC_ORG_001_11_01.Pesquisar_Tipo_Agenda";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_ORG_001_11_02.
	/// </summary>
    public static class UC_ORG_001_11_02
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo cadastrar ou alterar os dados de um tipo de agenda.  
	    /// </summary>
        public const string MANTER_TIPO_AGENDA = "UC_ORG_001_11_02.Manter_Tipo_Agenda";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_ORG_001_12_01.
	/// </summary>
    public static class UC_ORG_001_12_01
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo consultar as imagens já associadas à entidade passada como parâmetro
	    /// </summary>
        public const string PESQUISAR_ASSOCIACAO_IMAGEM = "UC_ORG_001_12_01.Pesquisar_Associacao_Imagem";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_ORG_001_12_02.
	/// </summary>
    public static class UC_ORG_001_12_02
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo associar imagens ou alterar as imagens já associadas à entidade passada como parâmetro
	    /// </summary>
        public const string MANTER_ASSOCIACAO_IMAGEM = "UC_ORG_001_12_02.Manter_Associacao_Imagem";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_ORG_002_01_01.
	/// </summary>
    public static class UC_ORG_002_01_01
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo consultar níveis de ensino configurados para cada instituição de ensino e os parâmetros associados a cada instituição x nível de ensino.
	    /// </summary>
        public const string PESQUISAR_PARAMETROS_INSTITUICAO_NIVEL_ENSINO = "UC_ORG_002_01_01.Pesquisar_Parametros_Instituicao_Nivel_Ensino";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_ORG_002_01_02.
	/// </summary>
    public static class UC_ORG_002_01_02
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo manter o cadastro dos dados gerais de um nível de ensino, permitindo todas as funcionalidades de um CRUD básico (consulta, cadastro, alteração).
	    /// </summary>
        public const string MANTER_ASSOCIACAO_NIVEL_ENSINO = "UC_ORG_002_01_02.Manter_Associacao_Nivel_Ensino";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_ORG_002_03_01.
	/// </summary>
    public static class UC_ORG_002_03_01
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo consultar as associações dos tipos de entidade a instituição de ensino logada, permitindo também a exclusão de associações.
	    /// </summary>
        public const string PESQUISAR_PARAMETROS_INSTITUICAO_ENSINO = "UC_ORG_002_03_01.Pesquisar_Parametros_Instituicao_Ensino";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_ORG_002_03_02.
	/// </summary>
    public static class UC_ORG_002_03_02
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo associar um Tipo de Entidade à Instituição de Ensino logada.    <b>[Importante] </b>A única associação de tipo de entidade que não será considerada pelo sistema é a referente a "Instituição de Ensino", visto que para este Tipo de Entidade a parametrização é feita de forma fixa, conforme RN_ORG_003 - Cadastro - Instituição de Ensino.
	    /// </summary>
        public const string MANTER_ASSOCIACAO_TIPO_ENTIDADE = "UC_ORG_002_03_02.Manter_Associacao_Tipo_Entidade";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_ORG_002_05_01.
	/// </summary>
    public static class UC_ORG_002_05_01
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo consultar os parâmetros das localidades por instituição, permitindo também a exclusão.
	    /// </summary>
        public const string PESQUISAR_LOCALIDADE_INSTITUICAO = "UC_ORG_002_05_01.Pesquisar_Localidade_Instituicao";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_ORG_002_05_02.
	/// </summary>
    public static class UC_ORG_002_05_02
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo cadastrar ou alterar os parâmetros das localidades por instituição.
	    /// </summary>
        public const string MANTER_LOCALIDADE_INSTITUICAO_NIVEL = "UC_ORG_002_05_02.Manter_Localidade_Instituicao_Nivel";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_ORG_002_06_01.
	/// </summary>
    public static class UC_ORG_002_06_01
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo parametrizar os modelos word de relatório por instituição nível.    
	    /// </summary>
        public const string MANTER_MODELO_WORD_RELATORIO_INSTITUICAO_NIVEL = "UC_ORG_002_06_01.Manter_Modelo_Word_Relatorio_Instituicao_Nivel";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_ORG_002_07_01.
	/// </summary>
    public static class UC_ORG_002_07_01
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo manter a parametrização de calendários AGD por Instituição Nível, permitindo todas as funcionalidades de um CRUD básico (consulta, cadastro, alteração e exclusão)    É um cadastro dynamic.
	    /// </summary>
        public const string MANTER_CALENDARIO_AGDINSTITUICAO_NIVEL = "UC_ORG_002_07_01.Manter_Calendario_AGD_Instituicao_Nivel";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_ORG_002_08_01.
	/// </summary>
    public static class UC_ORG_002_08_01
    {

        /// <summary>
	    /// Esse caso de uso tem como objetivo manter os parâmetros gerais da instituição de ensino.
	    /// </summary>
        public const string MANTER_PARAMETROS_GERAIS_INSTITUICAO = "UC_ORG_002_08_01.Manter_Parametros_Gerais_Instituicao";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_ORG_002_09_01.
	/// </summary>
    public static class UC_ORG_002_09_01
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo pesquisar as configurações de notificação por entidade, permitindo também a exclusão.
	    /// </summary>
        public const string PESQUISAR_CONFIGURACAO_NOTIFICACAO_ENTIDADE = "UC_ORG_002_09_01.Pesquisar_Configuracao_Notificacao_Entidade";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_ORG_002_09_02.
	/// </summary>
    public static class UC_ORG_002_09_02
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo cadastrar ou alterar uma configuração de notificação por Entidade.
	    /// </summary>
        public const string MANTER_CONFIGURACAO_NOTIFICACAO_ENTIDADE = "UC_ORG_002_09_02.Manter_Configuracao_Notificacao_Entidade";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_ORG_002_10_01.
	/// </summary>
    public static class UC_ORG_002_10_01
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo parametrizar os modelos word de relatório por instituição de ensino.
	    /// </summary>
        public const string MANTER_PARAMETRO_MODELO_WORD_POR_INSTITUICAO = "UC_ORG_002_10_01.Manter_Parametro_Modelo_Word_Por_Instituicao";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_ORG_002_11_01.
	/// </summary>
    public static class UC_ORG_002_11_01
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo parametrizar os sistemas de origem do GAD por Instituição e Nível de Ensino.
	    /// </summary>
        public const string MANTER_SISTEMA_ORIGEM_GADINSTITUICAO_NIVEL = "UC_ORG_002_11_01.Manter_Sistema_Origem_GAD_Instituicao_Nivel";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_ORG_003_01_01.
	/// </summary>
    public static class UC_ORG_003_01_01
    {

        /// <summary>
	    /// Esse caso de uso tem como objetivo a pesquisa dos assuntos normativos, permitindo também a exclusão.
	    /// </summary>
        public const string PESQUISAR_ASSUNTO_NORMATIVO = "UC_ORG_003_01_01.Pesquisar_Assunto_Normativo";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_ORG_003_01_02.
	/// </summary>
    public static class UC_ORG_003_01_02
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo a manutenção (inclusão/alteração) dos assuntos normativos.
	    /// </summary>
        public const string MANTER_ASSUNTO_NORMATIVO = "UC_ORG_003_01_02.Manter_Assunto_Normativo";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_ORG_003_02_01.
	/// </summary>
    public static class UC_ORG_003_02_01
    {

        /// <summary>
	    /// Esse caso de uso tem como objetivo a pesquisa dos tipos de ato normativos, permitindo também a exclusão.
	    /// </summary>
        public const string PESQUISAR_TIPO_ATO_NORMATIVO = "UC_ORG_003_02_01.Pesquisar_Tipo_Ato_Normativo";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_ORG_003_02_02.
	/// </summary>
    public static class UC_ORG_003_02_02
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo a manutenção (inclusão/alteração) dos tipos de atos normativos.
	    /// </summary>
        public const string MANTER_TIPO_ATO_NORMATIVO = "UC_ORG_003_02_02.Manter_Tipo_Ato_Normativo";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_ORG_003_03_01.
	/// </summary>
    public static class UC_ORG_003_03_01
    {

        /// <summary>
	    /// Esse caso de uso tem como objetivo a pesquisa dos atos normativos, permitindo a inserção e exclusão.
	    /// </summary>
        public const string PESQUISAR_ATO_NORMATIVO = "UC_ORG_003_03_01.Pesquisar_Ato_Normativo";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_ORG_003_03_02.
	/// </summary>
    public static class UC_ORG_003_03_02
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo a manutenção (inclusão/alteração) dos atos normativos.
	    /// </summary>
        public const string MANTER_ATO_NORMATIVO = "UC_ORG_003_03_02.Manter_Ato_Normativo";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_ORG_003_03_03.
	/// </summary>
    public static class UC_ORG_003_03_03
    {

        /// <summary>
	    /// Esse caso de uso tem como objetivo a pesquisa das entidades associadas ao ato normativo, permitindo a inserção e exclusão.
	    /// </summary>
        public const string PESQUISAR_ASSOCIACAO_ENTIDADE = "UC_ORG_003_03_03.Pesquisar_Associacao_Entidade";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_ORG_003_03_04.
	/// </summary>
    public static class UC_ORG_003_03_04
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo manter a associação de entidade ao ato-normativo.
	    /// </summary>
        public const string MANTER_ASSOCIACAO_ENTIDADE = "UC_ORG_003_03_04.Manter_Associacao_Entidade";

    }

}

