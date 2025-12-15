 
using System;

namespace SMC.Academico.Common.Areas.ORT.Constants
{
    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_ORT_001_01_01.
	/// </summary>
    public static class UC_ORT_001_01_01
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo manter o cadastro de tipos de orientação, permitindo todas as funcionalidades de um CRUD básico (consulta, cadastro, alteração e exclusão)    É um cadastro dynamic.
	    /// </summary>
        public const string MANTER_TIPO_ORIENTACAO = "UC_ORT_001_01_01.Manter_Tipo_Orientacao";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_ORT_001_02_01.
	/// </summary>
    public static class UC_ORT_001_02_01
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo consultar as orientações, permitindo também a exclusão.
	    /// </summary>
        public const string PESQUISAR_ORIENTACAO = "UC_ORT_001_02_01.Pesquisar_Orientacao";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_ORT_001_02_02.
	/// </summary>
    public static class UC_ORT_001_02_02
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo cadastrar ou alterar os dados de uma orientação.
	    /// </summary>
        public const string MANTER_ORIENTACAO = "UC_ORT_001_02_02.Manter_Orientacao";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_ORT_001_03_01.
	/// </summary>
    public static class UC_ORT_001_03_01
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo pesquisar orientações.
	    /// </summary>
        public const string PESQUISAR_ORIENTADORES = "UC_ORT_001_03_01.Pesquisar_Orientadores";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_ORT_001_03_02.
	/// </summary>
    public static class UC_ORT_001_03_02
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo exibir relatório de orientadores e seus orientandos.
	    /// </summary>
        public const string EXIBIR_RELATÓRIO_ORIENTADORES = "UC_ORT_001_03_02.Exibir_Relatório_Orientadores";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_ORT_002_01_01.
	/// </summary>
    public static class UC_ORT_002_01_01
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo manter o cadastro de tipos de trabalho, permitindo todas as funcionalidades de um CRUD básico (consulta, cadastro, alteração e exclusão)    É um cadastro dynamic.
	    /// </summary>
        public const string MANTER_TIPO_TRABALHO = "UC_ORT_002_01_01.Manter_Tipo_Trabalho";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_ORT_002_02_01.
	/// </summary>
    public static class UC_ORT_002_02_01
    {

        /// <summary>
	    /// O usuário que possuir permissão neste token poderá incluir um novo trabalho acadêmico.
	    /// </summary>
        public const string PERMITIR_INCLUIR_TRABALHO_ACADEMICO = "UC_ORT_002_02_01.Permitir_Incluir_Trabalho_Academico";

        /// <summary>
	    /// Este caso de uso tem como objetivo consultar os trabalhos acadêmicos cadastrados, permitindo a exclusão.
	    /// </summary>
        public const string PESQUISAR_TRABALHO_ACADEMICO = "UC_ORT_002_02_01.Pesquisar_Trabalho_Academico";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_ORT_002_02_02.
	/// </summary>
    public static class UC_ORT_002_02_02
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo cadastrar ou alterar os dados de um trabalho acadêmico.
	    /// </summary>
        public const string MANTER_TRABALHO_ACADEMICO = "UC_ORT_002_02_02.Manter_Trabalho_Academico";

        /// <summary>
	    /// O usuário que possuir permissão neste token poderá alterar todos os campos referente as respostas do questionário de  potencial de registro.
	    /// </summary>
        public const string PERMITE_ALTERAR_RESPOSTA_POTENCIAL_REGISTRO = "UC_ORT_002_02_02.Permite Alterar Resposta Potencial Registro";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_ORT_002_02_03.
	/// </summary>
    public static class UC_ORT_002_02_03
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo consultar as avaliações dos componentes curriculares de um trabalho acadêmico.
	    /// </summary>
        public const string PESQUISAR_AVALIACAO_TRABALHO_ACADEMICO = "UC_ORT_002_02_03.Pesquisar_Avaliacao_Trabalho_Academico";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_ORT_002_02_04.
	/// </summary>
    public static class UC_ORT_002_02_04
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo cadastrar ou alterar o agendamento de banca examinado para o componente curricular de um trabalho acadêmico.
	    /// </summary>
        public const string MANTER_AGENDAMENTO_BANCA_EXAMINADORA = "UC_ORT_002_02_04.Manter_Agendamento_Banca_Examinadora";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_ORT_002_02_05.
	/// </summary>
    public static class UC_ORT_002_02_05
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo lançar nota para a banca examinadora dos componentes curriculares de um trabalho acadêmico.
	    /// </summary>
        public const string LANCAR_NOTA_BANCA_EXAMINADORA = "UC_ORT_002_02_05.Lancar_Nota_Banca_Examinadora";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_ORT_002_02_06.
	/// </summary>
    public static class UC_ORT_002_02_06
    {

        /// <summary>
	    /// Esse caso de uso tem como objetivo exibir o relatório de comprovante de entrega do trabalho na secretaria.
	    /// </summary>
        public const string EMITIR_COMPROVANTE_ENTREGA = "UC_ORT_002_02_06.Emitir_Comprovante_Entrega";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_ORT_002_02_07.
	/// </summary>
    public static class UC_ORT_002_02_07
    {

        /// <summary>
	    /// Esse caso de uso tem como objetivo exibir a ata de defesa gerada pelo sistema.
	    /// </summary>
        public const string EMITIR_ATA_DEFESA = "UC_ORT_002_02_07.Emitir_Ata_Defesa";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_ORT_002_02_08.
	/// </summary>
    public static class UC_ORT_002_02_08
    {

        /// <summary>
	    /// Caso de uso para pemitir anexar, assinar digitalmente e emitir atas de defesa.
	    /// </summary>
        public const string MANTER_ATA_DE_DEFESA = "UC_ORT_002_02_08.Manter_Ata_de_Defesa";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_ORT_002_02_09.
	/// </summary>
    public static class UC_ORT_002_02_09
    {

        /// <summary>
	    /// Esse caso de uso tem como objetivo incluir autorização para novo depósito para alunos que tiveram um trabalho defendido e cuja avaliação final foi  'REPROVADO'.
	    /// </summary>
        public const string MANTER_AUTORIZACAO_NOVO_DEPOSITO = "UC_ORT_002_02_09.Manter_Autorizacao_Novo_Deposito";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_ORT_003_01_01.
	/// </summary>
    public static class UC_ORT_003_01_01
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo cadastrar, alterar e consultar os dados do trabalho de um aluno para publicação na biblioteca.
	    /// </summary>
        public const string MANTER_PUBLICACAO_TRABALHO_BIBLIOTECA = "UC_ORT_003_01_01.Manter_Publicacao_Trabalho_Biblioteca";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_ORT_003_01_02.
	/// </summary>
    public static class UC_ORT_003_01_02
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo registrar a autorização para publicação do trabalho na biblioteca.
	    /// </summary>
        public const string AUTORIZAR_PUBLICACAO_TRABALHO_BIBLIOTECA = "UC_ORT_003_01_02.Autorizar_Publicacao_Trabalho_Biblioteca";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_ORT_003_01_03.
	/// </summary>
    public static class UC_ORT_003_01_03
    {

        /// <summary>
	    /// Esse caso de uso tem como objetivo exibir o relatório de autorização da publicação do trabalho.
	    /// </summary>
        public const string EXIBIR_RELATORIO_AUTORIZACAO_PUBLICACAO = "UC_ORT_003_01_03.Exibir_Relatorio_Autorizacao_Publicacao";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_ORT_003_02_01.
	/// </summary>
    public static class UC_ORT_003_02_01
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo consultar os trabalhos que terão publicação no BDP.
	    /// </summary>
        public const string PESQUISAR_TRABALHO_ACADEMICO_BDP = "UC_ORT_003_02_01.Pesquisar_Trabalho_Academico_BDP";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_ORT_003_02_02.
	/// </summary>
    public static class UC_ORT_003_02_02
    {

        /// <summary>
	    /// Habilita o comando "Liberar para Conferência da Biblioteca".
	    /// </summary>
        public const string LIBERAR_PARA_BIBLIOTECA = "UC_ORT_003_02_02.Liberar_Para_Biblioteca";

        /// <summary>
	    /// Habilita o comando "Liberar para Consulta"e permite a alteração dos arquivos do trabalho após publicação
	    /// </summary>
        public const string LIBERAR_PARA_CONSULTA = "UC_ORT_003_02_02.Liberar_Para_Consulta";

        /// <summary>
	    /// Este caso de uso tem como objetivo  alterar os dados de um trabalho acadêmico publicado no BDP.
	    /// </summary>
        public const string MANTER_TRABALHO_ACADEMICO_BDP = "UC_ORT_003_02_02.Manter_Trabalho_Academico_BDP";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_ORT_003_02_03.
	/// </summary>
    public static class UC_ORT_003_02_03
    {

        /// <summary>
	    /// Esse caso de uso tem como objetivo exibir o relatório de autorização da publicação do trabalho.
	    /// </summary>
        public const string EXIBIR_RELATORIO_AUTORIZACAO_PUBLICACAO = "UC_ORT_003_02_03.Exibir_Relatorio_Autorizacao_Publicacao";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_ORT_003_02_04.
	/// </summary>
    public static class UC_ORT_003_02_04
    {

        /// <summary>
	    /// Esse caso de uso tem como objetivo exibir as  informações para  emissão da ficha catalográfica da biblioteca para anexo ao trabalho acadêmico.
	    /// </summary>
        public const string GERAR_FICHA_CATALOGRAFICA = "UC_ORT_003_02_04.Gerar_Ficha_Catalografica";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_ORT_003_02_05.
	/// </summary>
    public static class UC_ORT_003_02_05
    {

        /// <summary>
	    /// Esse caso de uso tem como objetivo emitir a ficha catalográfica da biblioteca para anexo ao trabalho acadêmico.
	    /// </summary>
        public const string EMITIR_FICHA_CATALOGRAFICA = "UC_ORT_003_02_05.Emitir_Ficha_Catalografica";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_ORT_004_02_01.
	/// </summary>
    public static class UC_ORT_004_02_01
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo manter a parametrização de tipos de trabalho por Instituição Nível, permitindo todas as funcionalidades de um CRUD básico (consulta, cadastro, alteração e exclusão)    É um cadastro dynamic.
	    /// </summary>
        public const string MANTER_TIPO_TRABALHO_INSTITUICAO_NIVEL = "UC_ORT_004_02_01.Manter_Tipo_Trabalho_Instituicao_Nivel";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_ORT_004_03_01.
	/// </summary>
    public static class UC_ORT_004_03_01
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo pesquisar a parametrização da numeração de trabalho por Entidade X Curso Oferta Localidade.
	    /// </summary>
        public const string PESQUISAR_NUMERACAO_TRABALHO = "UC_ORT_004_03_01.Pesquisar_Numeracao_Trabalho";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_ORT_004_03_02.
	/// </summary>
    public static class UC_ORT_004_03_02
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo manter a parametrização da numeração de trabalho por Entidade X Curso Oferta Localidade.
	    /// </summary>
        public const string MANTER_NUMERACAO_TRABALHO = "UC_ORT_004_03_02.Manter_Numeracao_Trabalho";

    }

}

