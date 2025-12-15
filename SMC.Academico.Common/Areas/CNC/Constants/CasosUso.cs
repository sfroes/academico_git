 
using System;

namespace SMC.Academico.Common.Areas.CNC.Constants
{
    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_CNC_001_02_01.
	/// </summary>
    public static class UC_CNC_001_02_01
    {

        /// <summary>
	    /// Esse caso de uso tem como objetivo a exibição da integralização curricular de um aluno.
	    /// </summary>
        public const string EXIBIR_INTEGRALIZACAO_CURRICULAR = "UC_CNC_001_02_01.Exibir_Integralizacao_Curricular";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_CNC_002_01_01.
	/// </summary>
    public static class UC_CNC_002_01_01
    {

        /// <summary>
	    /// Este caso de  uso tem como objetivo a pesquisa dos documentos de conclusão, permitindo também sua exclusão.
	    /// </summary>
        public const string PESQUISAR_DOCUMENTO_CONCLUSAO = "UC_CNC_002_01_01.Pesquisar_Documento_Conclusao";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_CNC_002_01_02.
	/// </summary>
    public static class UC_CNC_002_01_02
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo a manutenção (inclusão/alteração) de documento de conclusão que referem-se a cadastro de histórico.     É considerado cadastro de histórico, os diplomas registrados pela UFMG enquanto a PUC não tinha autonomia para realizar a emissão/registro ou, quando eram registrados apenas por livros.    E esse cadastro é necessário quando alunos que formaram nessa época solicitam a emissão da 2º via do diploma e, o cadastro da 1º via precisa ser resgatado nos livros de registro da UFMG ou da própria puc.    [Importante] Documentos de conclusão que não referem-se a histórico deverão ser emitidos através de solicitação de serviço.
	    /// </summary>
        public const string MANTER_DOCUMENTO_CONCLUSAO_ANTIGO = "UC_CNC_002_01_02.Manter_Documento_Conclusao_Antigo";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_CNC_002_01_03.
	/// </summary>
    public static class UC_CNC_002_01_03
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo apenas consultar os dados de um documento de conclusão.
	    /// </summary>
        public const string CONSULTAR_DOCUMENTO_CONCLUSAO = "UC_CNC_002_01_03.Consultar_Documento_Conclusao";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_CNC_002_01_04.
	/// </summary>
    public static class UC_CNC_002_01_04
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo listar os apostilamentos associados ao documento de conclusão, permitindo também sua exclusão.
	    /// </summary>
        public const string PESQUISAR_APOSTILAMENTO = "UC_CNC_002_01_04.Pesquisar_Apostilamento";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_CNC_002_01_05.
	/// </summary>
    public static class UC_CNC_002_01_05
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo inserir/atualizar os dados de apostilamento do documento de conclusão.
	    /// </summary>
        public const string MANTER_APOSTILAMENTO = "UC_CNC_002_01_05.Manter_Apostilamento";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_CNC_002_01_06.
	/// </summary>
    public static class UC_CNC_002_01_06
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo inserir/atualizar os dados de apostilamento do documento de conclusão.
	    /// </summary>
        public const string CONSULTAR_DADOS_PESSOAIS = "UC_CNC_002_01_06.Consultar_Dados_Pessoais";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_CNC_002_01_07.
	/// </summary>
    public static class UC_CNC_002_01_07
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo permitir anular ou restaurar um diploma digital.  [Observação] Por enquanto temos somente esse tipo de documento que é digital
	    /// </summary>
        public const string MANTER_STATUS_DOCUMENTO_DIGITAL = "UC_CNC_002_01_07.Manter_Status_Documento_Digital";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_CNC_002_03_01.
	/// </summary>
    public static class UC_CNC_002_03_01
    {

        /// <summary>
	    /// Esse caso de uso tem como objetivo a pesquisa de diplomas digitais.
	    /// </summary>
        public const string PESQUISAR_DIPLOMA_DIGITAL = "UC_CNC_002_03_01.Pesquisar_Diploma_Digital";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_CNC_002_03_02.
	/// </summary>
    public static class UC_CNC_002_03_02
    {

        /// <summary>
	    /// Esse caso de uso tem como objetivo a consulta de diplomas digitais.
	    /// </summary>
        public const string CONSULTAR_DIPLOMA_DIGITAL = "UC_CNC_002_03_02.Consultar_Diploma_Digital";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_CNC_002_04_01.
	/// </summary>
    public static class UC_CNC_002_04_01
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo manter o cadastro de titulações, permitindo todas as funcionalidades de um CRUD básico (consulta, cadastro, alteração e exclusão)    É um cadastro dynamic.  
	    /// </summary>
        public const string MANTER_TITULACAO = "UC_CNC_002_04_01.Manter_Titulacao";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_CNC_004_01_01.
	/// </summary>
    public static class UC_CNC_004_01_01
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo manter o cadastro de tipos de documento acadêmico, permitindo todas as funcionalidades de um CRUD básico (consulta, cadastro, alteração e exclusão)    É um cadastro dynamic.
	    /// </summary>
        public const string MANTER_TIPO_DOCUMENTO_ACADEMICO = "UC_CNC_004_01_01.Manter_Tipo_Documento_Academico";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_CNC_004_03_01.
	/// </summary>
    public static class UC_CNC_004_03_01
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo manter o cadastro de tipos de apostilamento, permitindo todas as funcionalidades de um CRUD básico (consulta, cadastro, alteração e exclusão)    É um cadastro dynamic.
	    /// </summary>
        public const string MANTER_TIPO_APOSTILAMENTO = "UC_CNC_004_03_01.Manter_Tipo_Apostilamento";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_CNC_004_04_01.
	/// </summary>
    public static class UC_CNC_004_04_01
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo, consultar, cadastrar ou alterar a parametrização de um tipo de documento acadêmico por instituição e nível de ensino, permitindo todas as funcionalidades de um CRUD básico (consulta, cadastro, alteração e exclusão).    É um cadastro dynamic.
	    /// </summary>
        public const string MANTER_TIPO_DOCUMENTO_ACADEMICO_INSTITUICAO_NIVEL = "UC_CNC_004_04_01.Manter_Tipo_Documento_Academico_Instituicao_Nivel";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_CNC_004_05_01.
	/// </summary>
    public static class UC_CNC_004_05_01
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo manter o cadastro de situação de documento acadêmico, permitindo todas as funcionalidades de um CRUD básico (consulta, cadastro, alteração e exclusão)    É um cadastro dynamic.
	    /// </summary>
        public const string MANTER_SITUACAO_DOCUMENTO_ACADEMICO = "UC_CNC_004_05_01.Manter_Situacao_Documento_Academico";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_CNC_004_06_01.
	/// </summary>
    public static class UC_CNC_004_06_01
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo manter o cadastro de órgão de registro, permitindo todas as funcionalidades de um CRUD básico (consulta, cadastro, alteração e exclusão)    É um cadastro dynamic.
	    /// </summary>
        public const string MANTER_ORGAO_REGISTRO = "UC_CNC_004_06_01.Manter_Orgao_Registro";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_CNC_004_07_01.
	/// </summary>
    public static class UC_CNC_004_07_01
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo manter o cadastro de grupo de registro, permitindo todas as funcionalidades de um CRUD básico (consulta, cadastro, alteração e exclusão)    É um cadastro dynamic.
	    /// </summary>
        public const string MANTER_GRUPO_REGISTRO = "UC_CNC_004_07_01.Manter_Grupo_Registro";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_CNC_004_08_01.
	/// </summary>
    public static class UC_CNC_004_08_01
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo manter o cadastro de classificação de invalidade do documento, permitindo todas as funcionalidades de um CRUD básico (consulta, cadastro, alteração e exclusão). É um cadastro dynamic.
	    /// </summary>
        public const string MANTER_CLASSIFICACAO_INVALIDADE_DOCUMENTO = "UC_CNC_004_08_01.Manter_Classificacao_Invalidade_Documento";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_CNC_005_01_01.
	/// </summary>
    public static class UC_CNC_005_01_01
    {

        /// <summary>
	    /// Este caso de  uso tem como objetivo a pesquisa dos documentos acadêmicos, permitindo também seu cancelamento.
	    /// </summary>
        public const string PESQUISAR_DOCUMENTO_ACADEMICO_PESSOA_ATUACAO = "UC_CNC_005_01_01.Pesquisar_Documento_Academico_Pessoa_Atuacao";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_CNC_005_01_02.
	/// </summary>
    public static class UC_CNC_005_01_02
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo apenas consultar os dados de um documento de conclusão.
	    /// </summary>
        public const string CONSULTAR_DOCUMENTO_ACADEMICO_PESSOA_ATUACAO = "UC_CNC_005_01_02.Consultar_Documento_Academico_Pessoa_Atuacao";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_CNC_005_01_03.
	/// </summary>
    public static class UC_CNC_005_01_03
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo consultar os dados pessoais do aluno.
	    /// </summary>
        public const string CONSULTAR_DADOS_PESSOAIS_PESSOA_ATUACAO = "UC_CNC_005_01_03.Consultar_Dados_Pessoais_Pessoa_Atuacao";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_CNC_005_01_04.
	/// </summary>
    public static class UC_CNC_005_01_04
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo permitir cancelar a assinatura de um documento acadêmico.
	    /// </summary>
        public const string CANCELAR_DOCUMENTO_ACADEMICO_PESSOA_ATUACAO = "UC_CNC_005_01_04.Cancelar_Documento_Academico_Pessoa_Atuacao";

    }

}

