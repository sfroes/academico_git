 
using System;

namespace SMC.Academico.Common.Areas.CSO.Constants
{
    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_CSO_001_01_01.
	/// </summary>
    public static class UC_CSO_001_01_01
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo consultar os cursos de uma instituição de ensino, permitindo também a exclusão.
	    /// </summary>
        public const string PESQUISAR_CURSO = "UC_CSO_001_01_01.Pesquisar_Curso";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_CSO_001_01_02.
	/// </summary>
    public static class UC_CSO_001_01_02
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo cadastrar ou alterar os dados de um curso.
	    /// </summary>
        public const string MANTER_CURSO = "UC_CSO_001_01_02.Manter_Curso";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_CSO_001_01_03.
	/// </summary>
    public static class UC_CSO_001_01_03
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo cadastrar ou alterar os dados da oferta de curso de um curso.     
	    /// </summary>
        public const string MANTER_OFERTA_CURSO = "UC_CSO_001_01_03.Manter_Oferta_Curso";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_CSO_001_01_04.
	/// </summary>
    public static class UC_CSO_001_01_04
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo pesquisar as formações específicas de um curso.
	    /// </summary>
        public const string PESQUISAR_FORMACAO_ESPECIFICA_CURSO = "UC_CSO_001_01_04.Pesquisar_Formacao_Especifica_Curso";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_CSO_001_01_05.
	/// </summary>
    public static class UC_CSO_001_01_05
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo manter as formações específicas de um curso.
	    /// </summary>
        public const string MANTER_FORMACAO_ESPECIFICA_CURSO = "UC_CSO_001_01_05.Manter_Formacao_Especifica_Curso";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_CSO_001_01_06.
	/// </summary>
    public static class UC_CSO_001_01_06
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo replicar as formações específicas de um curso.
	    /// </summary>
        public const string REPLICAR_FORMACAO_ESPECIFICA_CURSO = "UC_CSO_001_01_06.Replicar_Formacao_Especifica_Curso";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_CSO_001_02_01.
	/// </summary>
    public static class UC_CSO_001_02_01
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo consultar as associações dos cursos às unidades, localidades e modalidades de uma instituição de ensino, permitindo também a exclusão desta associação.
	    /// </summary>
        public const string PESQUISAR_ASSOCIACAO_CURSO_UNIDADE = "UC_CSO_001_02_01.Pesquisar_Associacao_Curso_Unidade";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_CSO_001_02_02.
	/// </summary>
    public static class UC_CSO_001_02_02
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo cadastrar ou alterar os dados da associação curso unidade.
	    /// </summary>
        public const string MANTER_ASSOCIACAO_CURSO_UNIDADE = "UC_CSO_001_02_02.Manter_Associacao_Curso_Unidade";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_CSO_001_02_03.
	/// </summary>
    public static class UC_CSO_001_02_03
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo cadastrar ou alterar os dados da oferta de curso por localidade.
	    /// </summary>
        public const string MANTER_CURSO_OFERTA_LOCALIDADE = "UC_CSO_001_02_03.Manter_Curso_Oferta_Localidade";

        /// <summary>
	    /// Se usuário não tiver acesso a esse token restringir a alteração de todos os campos do cadastro de curso-oferta-localidade, ou seja, exibir os campos/comandos como "somente leitura". Exceto os campos:    <u>Aba Dados Gerais</u>  <ul>   <li>Código órgão regulador</li>   <li>Código habilitação</li>  </ul>    <u>Aba Dados de Contato</u>  <ul>   <li>Todos os campos de Endereço</li>   <li>Todos os campos de Endereço Eletrônico</li>   <li>Todos os campos de Telefones</li>  </ul>
	    /// </summary>
        public const string PERMITIR_ALTERACAO_CURSO_OFERTA_LOCALIDADE = "UC_CSO_001_02_03.Permitir_Alteracao_Curso_Oferta_Localidade";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_CSO_001_02_04.
	/// </summary>
    public static class UC_CSO_001_02_04
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo cadastrar ou alterar os dados da oferta de curso por localidade.
	    /// </summary>
        public const string ASSOCIAR_MATRIZ_CURSO_OFERTA_LOCALIDADE = "UC_CSO_001_02_04.Associar_Matriz_Curso_Oferta_Localidade";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_CSO_001_03_01.
	/// </summary>
    public static class UC_CSO_001_03_01
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo consultar os graus acadêmicos, permitindo também a exclusão.
	    /// </summary>
        public const string PESQUISAR_GRAU_ACADEMICO = "UC_CSO_001_03_01.Pesquisar_Grau_Academico";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_CSO_001_03_02.
	/// </summary>
    public static class UC_CSO_001_03_02
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo cadastrar ou alterar os dados de um grau acadêmico.
	    /// </summary>
        public const string MANTER_GRAU_ACADEMICO = "UC_CSO_001_03_02.Manter_Grau_Academico";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_CSO_001_04_01.
	/// </summary>
    public static class UC_CSO_001_04_01
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo consultar as hierarquias de classificação, permitindo também a exclusão.
	    /// </summary>
        public const string PESQUISAR_HIERARQUIA_CLASSIFICACAO = "UC_CSO_001_04_01.Pesquisar_Hierarquia_Classificacao";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_CSO_001_04_02.
	/// </summary>
    public static class UC_CSO_001_04_02
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo cadastrar ou alterar os dados de uma hierarquia de classificação.
	    /// </summary>
        public const string MANTER_HIERARQUIA_CLASSIFICACAO = "UC_CSO_001_04_02.Manter_Hierarquia_Classificacao";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_CSO_001_04_03.
	/// </summary>
    public static class UC_CSO_001_04_03
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo consultar uma hierarquia de classificações em formato de árvore, permitindo também a exclusão de itens da árvore.
	    /// </summary>
        public const string MONTAR_HIERARQUIA_CLASSIFICACAO = "UC_CSO_001_04_03.Montar_Hierarquia_Classificacao";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_CSO_001_04_04.
	/// </summary>
    public static class UC_CSO_001_04_04
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo cadastrar ou alterar os dados de uma classificação.
	    /// </summary>
        public const string MANTER_CLASSIFICACAO = "UC_CSO_001_04_04.Manter_Classificacao";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_CSO_001_05_01.
	/// </summary>
    public static class UC_CSO_001_05_01
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo manter o cadastro de modalidades, permitindo todas as funcionalidades de um CRUD básico (consulta, cadastro, alteração e exclusão)    É um cadastro dynamic.
	    /// </summary>
        public const string MANTER_MODALIDADE = "UC_CSO_001_05_01.Manter_Modalidade";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_CSO_001_06_01.
	/// </summary>
    public static class UC_CSO_001_06_01
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo consultar os tipos de hierarquia de classificação de cursos, permitindo também a exclusão.
	    /// </summary>
        public const string PESQUISAR_TIPO_HIERARQUIA_CLASSIFICACAO = "UC_CSO_001_06_01.Pesquisar_Tipo_Hierarquia_Classificacao";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_CSO_001_06_02.
	/// </summary>
    public static class UC_CSO_001_06_02
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo cadastrar ou alterar os dados de um tipo de hierarquia de classificação.
	    /// </summary>
        public const string MANTER_TIPO_HIERARQUIA_CLASSIFICACAO = "UC_CSO_001_06_02.Manter_Tipo_Hierarquia_Classificacao";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_CSO_001_06_03.
	/// </summary>
    public static class UC_CSO_001_06_03
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo consultar uma hierarquia de tipos de classificação em formato de árvore, permitindo também a exclusão de itens da árvore.
	    /// </summary>
        public const string MONTAR_HIERARQUIA_TIPO_CLASSIFICACAO = "UC_CSO_001_06_03.Montar_Hierarquia_Tipo_Classificacao";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_CSO_001_06_04.
	/// </summary>
    public static class UC_CSO_001_06_04
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo cadastrar ou alterar os dados de um tipo de classificação e associá-lo a uma hierarquia de tipos de classificação.
	    /// </summary>
        public const string MANTER_TIPO_CLASSIFICACAO = "UC_CSO_001_06_04.Manter_Tipo_Classificacao";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_CSO_001_08_01.
	/// </summary>
    public static class UC_CSO_001_08_01
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo manter o cadastro de tipo de formação, permitindo todas as funcionalidades de um CRUD básico (consulta, cadastro, alteração e exclusão). É um cadastro dynamic.    Estas informações serão utilizadas posteriormente para:  <ul>   <li>Parametrização dos Tipos de Formação por Instituição e Nível de Ensino, </li>   <li>Parametrização dos Tipos de Formação por Instituição e Tipo de Entidade, </li>   <li>Cadastro das Formações Específicas de um Curso. </li>  </ul>
	    /// </summary>
        public const string MANTER_TIPO_FORMACAO_ESPECIFICA = "UC_CSO_001_08_01.Manter_Tipo_Formacao_Especifica";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_CSO_001_09_01.
	/// </summary>
    public static class UC_CSO_001_09_01
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo manter o cadastro de turnos, permitindo todas as funcionalidades de um CRUD básico (consulta, cadastro, alteração e exclusão)    É um cadastro dynamic.
	    /// </summary>
        public const string MANTER_TURNO = "UC_CSO_001_09_01.Manter_Turno";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_CSO_001_11_01.
	/// </summary>
    public static class UC_CSO_001_11_01
    {

        /// <summary>
	    ///   Este caso de uso tem como objetivo manter o cadastro de Tipo de Oferta, permitindo todas as funcionalidades de um CRUD básico (consulta, cadastro, alteração e exclusão)    É um cadastro dynamic.
	    /// </summary>
        public const string MANTER_TIPO_OFERTA_CURSO = "UC_CSO_001_11_01.Manter_Tipo_Oferta_Curso";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_CSO_001_12_01.
	/// </summary>
    public static class UC_CSO_001_12_01
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo consultar as oferta temporais, permitindo também a sua exclusão.
	    /// </summary>
        public const string PESQUISAR_OFERTA_TEMPORAL = "UC_CSO_001_12_01.Pesquisar_Oferta_Temporal";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_CSO_001_12_02.
	/// </summary>
    public static class UC_CSO_001_12_02
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo cadastrar ou alterar ofertas temporais
	    /// </summary>
        public const string MANTER_OFERTA_TEMPORAL = "UC_CSO_001_12_02.Manter_Oferta_Temporal";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_CSO_002_01_01.
	/// </summary>
    public static class UC_CSO_002_01_01
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo consultar os programas do stricto sensu de uma instituição de ensino, permitindo também acionar demais funcionalidades relacionadas por exemplo: cadastro das formações específicas e proposta do programa.
	    /// </summary>
        public const string PESQUISAR_PROGRAMA = "UC_CSO_002_01_01.Pesquisar_Programa";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_CSO_002_01_02.
	/// </summary>
    public static class UC_CSO_002_01_02
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo cadastrar ou alterar os dados de um programa stricto sensu.
	    /// </summary>
        public const string MANTER_PROGRAMA = "UC_CSO_002_01_02.Manter_Programa";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_CSO_002_01_03.
	/// </summary>
    public static class UC_CSO_002_01_03
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo consultar as Formações Específicas de um programa stricto sensu, permitindo também a exclusão.
	    /// </summary>
        public const string PESQUISAR_FORMACAO_ESPECIFICA_PROGRAMA = "UC_CSO_002_01_03.Pesquisar_Formacao_Especifica_Programa";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_CSO_002_01_04.
	/// </summary>
    public static class UC_CSO_002_01_04
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo consultar as Formações Específicas de um programa stricto sensu, permitindo também a exclusão.
	    /// </summary>
        public const string MANTER_FORMACAO_ESPECIFICA_PROGRAMA = "UC_CSO_002_01_04.Manter_Formacao_Especifica_Programa";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_CSO_002_01_05.
	/// </summary>
    public static class UC_CSO_002_01_05
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo cadastrar ou alterar os dados da proposta de um programa stricto sensu.
	    /// </summary>
        public const string PESQUISAR_PROPOSTA = "UC_CSO_002_01_05.Pesquisar_Proposta";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_CSO_002_01_06.
	/// </summary>
    public static class UC_CSO_002_01_06
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo cadastrar ou alterar os dados da proposta de um programa stricto sensu.
	    /// </summary>
        public const string MANTER_PROPOSTA = "UC_CSO_002_01_06.Manter_Proposta";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_CSO_002_01_07.
	/// </summary>
    public static class UC_CSO_002_01_07
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo replicar as formações específicas de um programa.
	    /// </summary>
        public const string REPLICAR_FORMACAO_ESPECIFICA_PROGRAMA = "UC_CSO_002_01_07.Replicar_Formacao_Especifica_Programa";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_CSO_003_01_01.
	/// </summary>
    public static class UC_CSO_003_01_01
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo, consultar, cadastrar ou alterar a parametrização de uma hierarquia de classificação por instituição e nível de ensino, permitindo todas as funcionalidades de um CRUD básico (consulta, cadastro, alteração e exclusão).
	    /// </summary>
        public const string MANTER_HIERARQUIA_CLASSIFICACAO_INSTITUICAO_NIVEL = "UC_CSO_003_01_01.Manter_Hierarquia_Classificacao_Instituicao_Nivel";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_CSO_003_02_01.
	/// </summary>
    public static class UC_CSO_003_02_01
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo, consultar, cadastrar ou alterar a parametrização de uma modalidade por instituição e nível de ensino, permitindo todas as funcionalidades de um CRUD básico (consulta, cadastro, alteração e exclusão).
	    /// </summary>
        public const string MANTER_MODALIDADE_INSTITUICAO_NIVEL = "UC_CSO_003_02_01.Manter_Modalidade_Instituicao_Nivel";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_CSO_003_03_01.
	/// </summary>
    public static class UC_CSO_003_03_01
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo, consultar, cadastrar ou alterar a parametrização de um tipo de curso por instituição e nível de ensino, permitindo todas as funcionalidades de um CRUD básico (consulta, cadastro, alteração e exclusão).
	    /// </summary>
        public const string MANTER_TIPO_CURSO_INSTITUICAO_NIVEL = "UC_CSO_003_03_01.Manter_Tipo_Curso_Instituicao_Nivel";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_CSO_003_04_01.
	/// </summary>
    public static class UC_CSO_003_04_01
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo, consultar, cadastrar ou alterar a parametrização de um tipo de oferta de curso por instituição e nível de ensino, permitindo todas as funcionalidades de um CRUD básico (consulta, cadastro, alteração e exclusão).
	    /// </summary>
        public const string MANTER_TIPO_OFERTA_CURSO_INSTITUICAO_NIVEL = "UC_CSO_003_04_01.Manter_Tipo_Oferta_Curso_Instituicao_Nivel";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_CSO_003_05_01.
	/// </summary>
    public static class UC_CSO_003_05_01
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo, consultar, cadastrar ou alterar a parametrização de um tipo de formação específica por instituição e nível de ensino, permitindo todas as funcionalidades de um CRUD básico (consulta, cadastro, alteração e exclusão).
	    /// </summary>
        public const string MANTER_ASSOCIACAO_TIPO_FORMACAO_INSTITUICAO_NIVEL = "UC_CSO_003_05_01.Manter_Associacao_Tipo_Formacao_Instituicao_Nivel";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_CSO_003_06_01.
	/// </summary>
    public static class UC_CSO_003_06_01
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo, consultar, cadastrar ou alterar a parametrização de um turno por instituição e nível de ensino, permitindo todas as funcionalidades de um CRUD básico (consulta, cadastro, alteração e exclusão).
	    /// </summary>
        public const string MANTER_TURNO_INSTITUICAO_NIVEL = "UC_CSO_003_06_01.Manter_Turno_Instituicao_Nivel";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_CSO_003_07_01.
	/// </summary>
    public static class UC_CSO_003_07_01
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo, consultar, cadastrar ou alterar a parametrização de uma hierarquia de classificação por instituição e tipo de entidade, permitindo todas as funcionalidades de um CRUD básico (consulta, cadastro, alteração e exclusão).
	    /// </summary>
        public const string MANTER_HIERARQUIA_CLASSIFICACAO_INSTITUICAO_TIPO_ENTIDADE = "UC_CSO_003_07_01.Manter_Hierarquia_Classificacao_Instituicao_Tipo_Entidade";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_CSO_003_08_01.
	/// </summary>
    public static class UC_CSO_003_08_01
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo, consultar, cadastrar ou alterar a parametrização de um tipo de formação específica por instituição e tipo de entidade, permitindo todas as funcionalidades de um CRUD básico (consulta, cadastro, alteração e exclusão).
	    /// </summary>
        public const string MANTER_TIPO_FORMACAO_TIPO_ENTIDADE = "UC_CSO_003_08_01.Manter_Tipo_Formacao_Tipo_Entidade";

    }

}

