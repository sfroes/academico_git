 
using System;

namespace SMC.Academico.Common.Areas.APR.Constants
{
    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_APR_001_01_01.
	/// </summary>
    public static class UC_APR_001_01_01
    {

        /// <summary>
	    /// Esse caso de uso tem como objetivo a pesquisa das avaliações/trabalhos cadastrados para uma origem de avaliação.
	    /// </summary>
        public const string PESQUISAR_AVALIACAO_TURMA = "UC_APR_001_01_01.Pesquisar_Avaliacao_Turma";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_APR_001_01_02.
	/// </summary>
    public static class UC_APR_001_01_02
    {

        /// <summary>
	    /// Esse caso de uso tem como objetivo a inclusão/alteração de uma avaliação/trabalho para uma origem de avaliação.
	    /// </summary>
        public const string MANTER_AVALIACAO_TURMA = "UC_APR_001_01_02.Manter_Avaliacao_Turma";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_APR_001_02_01.
	/// </summary>
    public static class UC_APR_001_02_01
    {

        /// <summary>
	    /// Esse caso de uso tem como objetivo o cadastro de avaliações gerais para um curso.
	    /// </summary>
        public const string PESQUISAR_AVALIACAO_CURSO = "UC_APR_001_02_01.Pesquisar_Avaliacao_Curso";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_APR_001_02_02.
	/// </summary>
    public static class UC_APR_001_02_02
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo a inclusão/alteração de avaliações gerais por curso.
	    /// </summary>
        public const string MANTER_AVALIACAO_CURSO = "UC_APR_001_02_02.Manter_Avaliacao_Curso";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_APR_001_03_01.
	/// </summary>
    public static class UC_APR_001_03_01
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo consultar as escalas de apuração, permitindo também a exclusão.
	    /// </summary>
        public const string PESQUISAR_ESCALA_APURACAO = "UC_APR_001_03_01.Pesquisar_Escala_Apuracao";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_APR_001_03_02.
	/// </summary>
    public static class UC_APR_001_03_02
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo cadastrar ou alterar os dados de uma escala de apuração e de seus itens.
	    /// </summary>
        public const string MANTER_ESCALA_APURACAO = "UC_APR_001_03_02.Manter_Escala_Apuracao";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_APR_001_04_01.
	/// </summary>
    public static class UC_APR_001_04_01
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo consultar os critérios de aprovação, permitindo também a exclusão.
	    /// </summary>
        public const string PESQUISAR_CRITERIO_APROVACAO = "UC_APR_001_04_01.Pesquisar_Criterio_Aprovacao";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_APR_001_04_02.
	/// </summary>
    public static class UC_APR_001_04_02
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo cadastrar ou alterar os dados de um critério de aprovação.
	    /// </summary>
        public const string MANTER_CRITERIO_APROVACAO = "UC_APR_001_04_02.Manter_Criterio_Aprovacao";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_APR_001_05_01.
	/// </summary>
    public static class UC_APR_001_05_01
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo o lançamento de notas em avaliações de uma origem avaliação.
	    /// </summary>
        public const string LANCAMENTO_NOTA = "UC_APR_001_05_01.Lancamento_Nota";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_APR_001_05_02.
	/// </summary>
    public static class UC_APR_001_05_02
    {

        /// <summary>
	    /// Tela chamada para incluir comentário no lançamento de nota por aluno
	    /// </summary>
        public const string COMENTARIO_APURACAO = "UC_APR_001_05_02.Comentario_Apuracao";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_APR_001_07_01.
	/// </summary>
    public static class UC_APR_001_07_01
    {

        /// <summary>
	    /// Esse caso de uso tem como objetivo a entrega de uma avaliação de forma online.
	    /// </summary>
        public const string ENTREGA_ONLINE = "UC_APR_001_07_01.Entrega_Online";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_APR_001_08_01.
	/// </summary>
    public static class UC_APR_001_08_01
    {

        /// <summary>
	    /// Esse caso de uso tem como objetivo a correção de entrega de avaliação realizada online.
	    /// </summary>
        public const string CORRECAO_ENTREGA_ONLINE = "UC_APR_001_08_01.Correcao_Entrega_Online";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_APR_001_09_01.
	/// </summary>
    public static class UC_APR_001_09_01
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo listar o histórico de alterações de situação da entrega online de uma avaliação feita por um ou mais alunos.
	    /// </summary>
        public const string HISTORICO_SITUACAO_ENTREGA_ONLINE = "UC_APR_001_09_01.Historico_Situacao_Entrega_Online";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_APR_001_11_01.
	/// </summary>
    public static class UC_APR_001_11_01
    {

        /// <summary>
	    /// Esse caso de uso tem como objetivo disponibilizar campos de filtro para gerar relatório de bancas examinadoras por período.    
	    /// </summary>
        public const string PESQUISAR_BANCAS_AGENDADAS_PERIODO = "UC_APR_001_11_01.Pesquisar_Bancas_Agendadas_Periodo";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_APR_001_11_02.
	/// </summary>
    public static class UC_APR_001_11_02
    {

        /// <summary>
	    /// Esse caso de uso tem como objetivo exibir o relatório de bancas examinadoras por período.    
	    /// </summary>
        public const string EXIBIR_RELATORIO_BANCAS_AGENDADAS_PERIODO = "UC_APR_001_11_02.Exibir_Relatorio_Bancas_Agendadas_Periodo";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_APR_001_12_01.
	/// </summary>
    public static class UC_APR_001_12_01
    {

        /// <summary>
	    /// Esse caso de uso tem como objetivo a consulta de notas e frequencia final de um aluno.
	    /// </summary>
        public const string CONSULTA_NOTA_FREQUENCIA = "UC_APR_001_12_01.Consulta_Nota_Frequencia";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_APR_001_13_01.
	/// </summary>
    public static class UC_APR_001_13_01
    {

        /// <summary>
	    /// Esse caso de uso tem como objetivo a consulta de avaliações agendadas para uma turma e suas divisões. Também permite a consulta de notas e faltas para a turma.
	    /// </summary>
        public const string CONSULTAR_AVALIACAO = "UC_APR_001_13_01.Consultar_Avaliacao";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_APR_001_15_01.
	/// </summary>
    public static class UC_APR_001_15_01
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo cadastrar ou alterar notas e frequências para alunos de uma turma.
	    /// </summary>
        public const string LANCAMENTO_NOTA_FREQUENCIA_FINAL = "UC_APR_001_15_01.Lancamento_Nota_Frequencia_Final";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_APR_001_17_01.
	/// </summary>
    public static class UC_APR_001_17_01
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo a geração de relatório de acompanhamento de notas e faltas para uma turma / divisão de turma.
	    /// </summary>
        public const string EXIBIR_RELATORIO_ACOMPANHAMENTO_NOTA = "UC_APR_001_17_01.Exibir_Relatorio_Acompanhamento_Nota";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_APR_002_02_01.
	/// </summary>
    public static class UC_APR_002_02_01
    {

        /// <summary>
	    /// Esse caso de uso tem como objetivo exibir o relatório de diário de turma.    
	    /// </summary>
        public const string EXIBIR_RELATORIO_DIARIO_TURMA = "UC_APR_002_02_01.Exibir_Relatorio_Diario_Turma";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_APR_002_03_01.
	/// </summary>
    public static class UC_APR_002_03_01
    {

        /// <summary>
	    /// Esse caso de uso tem como objetivo a emissão de relatório de histórico escolar.
	    /// </summary>
        public const string EXIBIR_RELATORIO_HISTORICO_ESCOLAR = "UC_APR_002_03_01.Exibir_Relatorio_Historico_Escolar";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_APR_002_05_01.
	/// </summary>
    public static class UC_APR_002_05_01
    {

        /// <summary>
	    /// Esse caso de uso tem como objetivo a emissão de relatório de histórico escolar interno (para conferencia).
	    /// </summary>
        public const string EXIBIR_RELATORIO_HISTORICO_ESCOLAR_INTERNO = "UC_APR_002_05_01.Exibir_Relatorio_Historico_Escolar_Interno";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_APR_002_07_01.
	/// </summary>
    public static class UC_APR_002_07_01
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo exibir o relatório de Declaração de Componentes Curriculares Cursados.
	    /// </summary>
        public const string EXIBIR_RELATORIO_DECLARACAO_DISCIPLINAS_CURSADAS = "UC_APR_002_07_01.Exibir_Relatorio_Declaracao_Disciplinas_Cursadas";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_APR_002_08_01.
	/// </summary>
    public static class UC_APR_002_08_01
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo consultar o histórico escolar de um aluno, permitindo também a exclusão da associação de componentes curricular.
	    /// </summary>
        public const string PESQUISAR_HISTORICO_ESCOLAR = "UC_APR_002_08_01.Pesquisar_Historico_Escolar";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_APR_002_08_02.
	/// </summary>
    public static class UC_APR_002_08_02
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo associar componentes curriculares a um histórico escolar.
	    /// </summary>
        public const string MANTER_HISTORICO_ESCOLAR = "UC_APR_002_08_02.Manter_Historico_Escolar";

        /// <summary>
	    /// Permitir inclusão e manutenção de todos os tipos de componentes
	    /// </summary>
        public const string PERMITIR_MANTER_TODOS_TIPOS_COMPONENTES = "UC_APR_002_08_02.Permitir_manter_todos_tipos_componentes";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_APR_003_01_01.
	/// </summary>
    public static class UC_APR_003_01_01
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo, consultar, cadastrar ou alterar a parametrização de uma escala de apuração por Instituição e nível de ensino, permitindo todas as funcionalidades de um CRUD básico (consulta, cadastro, alteração e exclusão).
	    /// </summary>
        public const string MANTER_ESCALA_APURACAO_INSTITUICAO_NIVEL = "UC_APR_003_01_01.Manter_Escala_Apuracao_Instituicao_Nivel";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_APR_003_02_01.
	/// </summary>
    public static class UC_APR_003_02_01
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo, consultar, cadastrar ou alterar a parametrização de um critério de aprovação por Instituição e nível de ensino, permitindo todas as funcionalidades de um CRUD básico (consulta, cadastro, alteração e exclusão).
	    /// </summary>
        public const string MANTER_CRITERIO_APROVACAO_INSTITUICAO_NIVEL = "UC_APR_003_02_01.Manter_Criterio_Aprovacao_Instituicao_Nivel";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_APR_003_03_01.
	/// </summary>
    public static class UC_APR_003_03_01
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo, consultar, cadastrar ou alterar a parametrização de um tipo de origem de avaliação por instituição e nível de ensino, permitindo todas as funcionalidades de um CRUD básico (consulta, cadastro, alteração e exclusão).
	    /// </summary>
        public const string MANTER_TIPO_ORIGEM_AVALIACAO_INSTITUICAO_NIVEL = "UC_APR_003_03_01.Manter_Tipo_Origem_Avaliacao_Instituicao_Nivel";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_APR_003_04_01.
	/// </summary>
    public static class UC_APR_003_04_01
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo manter a parametrização de tipos de Membro de Banca por Instituição Nível, permitindo todas as funcionalidades de um CRUD básico (consulta, cadastro, alteração e exclusão)    É um cadastro dynamic.
	    /// </summary>
        public const string MANTER_TIPO_MEMBRO_BANCA_INSTITUICAO_NIVEL = "UC_APR_003_04_01.Manter_Tipo_Membro_Banca_Instituicao_Nivel";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_APR_004_01_01.
	/// </summary>
    public static class UC_APR_004_01_01
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo consultar pastas e materiais (arquivos ou links), permitindo também sua alteração e exclusão.
	    /// </summary>
        public const string PESQUISAR_MATERIAL = "UC_APR_004_01_01.Pesquisar_Material";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_APR_004_01_02.
	/// </summary>
    public static class UC_APR_004_01_02
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo alterar os dados de um material (pasta, arquivo e link).
	    /// </summary>
        public const string MANTER_MATERIAL = "UC_APR_004_01_02.Manter_Material";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_APR_004_02_01.
	/// </summary>
    public static class UC_APR_004_02_01
    {

        /// <summary>
	    /// Esse caso de uso tem como objetivo a exibição dos materiais de uma origem permitindo sua visualização e download.
	    /// </summary>
        public const string DOWNLOAD_MATERIAL = "UC_APR_004_02_01.Download_Material";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_APR_006_01_01.
	/// </summary>
    public static class UC_APR_006_01_01
    {

        /// <summary>
	    /// Esse caso de uso tem como objetivo a pesquisa de aulas para lançamento de frequencia.
	    /// </summary>
        public const string PESQUISAR_LANCAMENTO_FREQUENCIA = "UC_APR_006_01_01.Pesquisar_Lancamento_Frequencia";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_APR_006_01_02.
	/// </summary>
    public static class UC_APR_006_01_02
    {

        /// <summary>
	    /// Esse caso de uso tem como objetivo a apuração de frequencia de uma aula.
	    /// </summary>
        public const string MANTER_LANCAMENTO_FREQUENCIA = "UC_APR_006_01_02.Manter_Lancamento_Frequencia";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_APR_006_02_01.
	/// </summary>
    public static class UC_APR_006_02_01
    {

        /// <summary>
	    /// Esse caso de uso tem como objetivo o lançamento de frequencia em horários da grade de uma turma.
	    /// </summary>
        public const string LANÇAMENTO_FREQUENCIA_GRADE = "UC_APR_006_02_01.Lançamento_Frequencia_GRADE";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_APR_006_03_01.
	/// </summary>
    public static class UC_APR_006_03_01
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo realizar a configuração do relatório de Lista de Frequência.
	    /// </summary>
        public const string PARAMETROS_LISTA_FREQUENCIA = "UC_APR_006_03_01.Parametros_Lista_Frequencia";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_APR_006_03_02.
	/// </summary>
    public static class UC_APR_006_03_02
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo exibir o relatório de Lista de Frequência.
	    /// </summary>
        public const string EXIBIR_LISTA_FREQUENCIA = "UC_APR_006_03_02.Exibir_Lista_Frequencia";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_APR_006_04_01.
	/// </summary>
    public static class UC_APR_006_04_01
    {

        /// <summary>
	    /// Permissão para alteração de aula executada ou não executaqda para não apurada     
	    /// </summary>
        public const string PERMITIRALTERARSITUACAOAULAAPURADA = "UC_APR_006_04_01.PERMITIR_ALTERAR_SITUACAO_AULA_APURADA";

        /// <summary>
	    /// Filtrar aulas de um professor ou turma em um intervalo de data para permitir alteração de situação das aulas selecionadas
	    /// </summary>
        public const string SITUACAO_AULA_EM_LOTE = "UC_APR_006_04_01.Situacao_Aula_Em_Lote";

    }

}

