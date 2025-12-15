 
using System;

namespace SMC.Academico.Common.Areas.GRD.Constants
{
    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_GRD_001_01_01.
	/// </summary>
    public static class UC_GRD_001_01_01
    {

        /// <summary>
	    /// Pesquisar os horários dos eventos de aula.
	    /// </summary>
        public const string PESQUISAR_EVENTO_AULA = "UC_GRD_001_01_01.Pesquisar_Evento_Aula";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_GRD_001_01_02.
	/// </summary>
    public static class UC_GRD_001_01_02
    {

        /// <summary>
	    /// Tem como objetivo incluir  dados referentes ao evento aula.
	    /// </summary>
        public const string INCLUIR_EVENTO_AULA = "UC_GRD_001_01_02.Incluir_Evento_Aula";

        /// <summary>
	    /// <font color="#000080">O usuário que possuir permissão neste token poderá alterar as datas de inicio e fim para geração dos eventos de aula , quando o parametro de distribuição de aula cadastrado para a divisão de turma for 'Semanal' ou 'Quinzenal'.
	    /// </summary>
        public const string PERMITE_ALTERAR_DATA_AGENDAMENTO_AULA = "UC_GRD_001_01_02.Permite_Alterar_Data_Agendamento_Aula";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_GRD_001_01_03.
	/// </summary>
    public static class UC_GRD_001_01_03
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo permitir a alteração das informações de horário e de associação do professor.
	    /// </summary>
        public const string ALTERAR_HORARIO = "UC_GRD_001_01_03.Alterar_Horario";

        /// <summary>
	    /// Visualizar detalhes da divisão de turma direcionada para grade.
	    /// </summary>
        public const string DETALHE_DIVISAO_TURMA_GRADE = "UC_GRD_001_01_03.Detalhe_Divisao_Turma_Grade";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_GRD_001_01_04.
	/// </summary>
    public static class UC_GRD_001_01_04
    {

        /// <summary>
	    /// Tem como objetivo excluir os eventos de aula.
	    /// </summary>
        public const string EXCLUIR_EVENTO_AULA = "UC_GRD_001_01_04.Excluir_Evento_Aula";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_GRD_001_01_05.
	/// </summary>
    public static class UC_GRD_001_01_05
    {

        /// <summary>
	    /// Permite a visualização  dos agendamentos que são simulados conforme parametros informados pelo usuário.
	    /// </summary>
        public const string VISUALIZAR_SIMULACAO_EVENTO_AULA = "UC_GRD_001_01_05.Visualizar_Simulacao_Evento_Aula";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_GRD_001_01_06.
	/// </summary>
    public static class UC_GRD_001_01_06
    {

        /// <summary>
	    /// Somente usuários que possuem permissão a este token poderão incluir, alterar ou excluir registros da grade quando a turma não estiver mais vigente.  Considera-se turma' não vigente' aquela cuja data atual  não se encontra dentro do periodo letivo da turma incluindo os limites do periodo.
	    /// </summary>
        public const string PERMITE_ALTERAR_GRADE_FORA_VIGENCIA_TURMA = "UC_GRD_001_01_06.Permite_Alterar_Grade_Fora_Vigencia_Turma";

        /// <summary>
	    /// Permite consultar os dados do agendamento.
	    /// </summary>
        public const string VISUALIZAR_DETALHES_EVENTO_AULA = "UC_GRD_001_01_06.Visualizar_Detalhes_Evento_Aula";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_GRD_001_01_07.
	/// </summary>
    public static class UC_GRD_001_01_07
    {

        /// <summary>
	    /// Permie a alteração do local associado ao horário da grade de aula.
	    /// </summary>
        public const string ALTERAR_LOCAL = "UC_GRD_001_01_07.Alterar_Local";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_GRD_001_01_08.
	/// </summary>
    public static class UC_GRD_001_01_08
    {

        /// <summary>
	    /// Permite alterar os colaboradores associados ao horários na grade de aula.
	    /// </summary>
        public const string ALTERAR_COLABORADOR = "UC_GRD_001_01_08.Alterar_Colaborador";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_GRD_001_01_09.
	/// </summary>
    public static class UC_GRD_001_01_09
    {

        /// <summary>
	    /// Permite associar colaborador substituto ao evento aula.
	    /// </summary>
        public const string ALTERAR_COLABORADOR_SUBSTITUTO = "UC_GRD_001_01_09.Alterar_Colaborador_substituto";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_GRD_001_02_01.
	/// </summary>
    public static class UC_GRD_001_02_01
    {

        /// <summary>
	    /// Esse caso de uso tem como objetivo permitir a alteração das configurações para geração do evento aula na grade por divisão de turma.
	    /// </summary>
        public const string ALTERAR_CONFIGURACAO_GRADE = "UC_GRD_001_02_01.Alterar_Configuracao_Grade";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_GRD_001_04_01.
	/// </summary>
    public static class UC_GRD_001_04_01
    {

        /// <summary>
	    /// Pesquisar as divisões de turma que compartilham no mesmo horário o professor e o local.
	    /// </summary>
        public const string PESQUISAR_GRADE_COMPARTILHADA = "UC_GRD_001_04_01.Pesquisar_Grade_Compartilhada";

    }

    /// <summary>
	/// Contém a lista de funcionalidades para o caso de uso UC_GRD_001_04_02.
	/// </summary>
    public static class UC_GRD_001_04_02
    {

        /// <summary>
	    /// Este caso de uso tem como objetivo cadastrar um compartilhamento de grade.
	    /// </summary>
        public const string MANTER_GRADE_COMPARTILHADA = "UC_GRD_001_04_02.Manter_Grade_Compartilhada";

    }

}

