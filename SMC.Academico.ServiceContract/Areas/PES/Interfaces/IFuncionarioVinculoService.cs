using SMC.Academico.ServiceContract.Areas.PES.Data;
using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.PES.Interfaces
{
    public interface IFuncionarioVinculoService : ISMCService
    {
        /// <summary>
        /// Busca um vinculo de funcionario com suas depêndencias
        /// </summary>
        /// <param name="seq">Sequencial do funcionario</param>
        /// <returns>Dados do funcionario</returns>
        FuncionarioVinculoData BuscarFuncionarioVinculo(long seq);

        /// <summary>
        /// Busca vinculos dos funcionários
        /// </summary>
        /// <param name="filtros">Filtros para busca</param>
        /// <returns>Dados paginados dos vinculos do funcionario</returns>
        SMCPagerData<FuncionarioVinculoListaData> BuscarVinculosFuncionario(FuncionarioVinculoFiltroData filtros);

        /// <summary>
        /// Salvar dados do funcionario vinculo
        /// </summary>
        /// <param name="funcionarioVinculoVO">Dados as serem salvos</param>
        /// <returns>Sequencial do funcionario</returns>
        long SalvarFuncionarioVinculo(FuncionarioVinculoData funcionarioVinculoVO);

        /// <summary>
        /// Excluir o funcionário informado
        /// </summary>
        /// <param name="seq">Sequencial do funcionário a ser excluído</param>
        void ExcluirFuncionarioVinculo(long seq);

        /// <summary>
        /// Valida se o funcionario se o funcionario tem data coincidentes para o mesmo vinculo
        /// </summary>
        ///<param name="funcionarioVinculoVO">Dados funcionario vinculo</param>
        void ValidarDatasVinculo(FuncionarioVinculoData funcionarioVinculoData);

        /// <summary>
        /// Se o tipo de funcionário selecionado tiver o campo "Tipo Registro
        /// Profissional Obrigatório" configurado e o os dados dos de registro
        /// profissional do funcionário que está sendo cadastrado estiver vazio ou o
        /// tipo diferente do obrigatyório.: Emitir a mensagem de erro e abortar
        /// operação.
        /// </summary>
        /// <param name="seqTipoFuncionario">Sequencial do tipo de funcionário</param>
        /// <param name="tipoRegistroProfissionalFuncioario">Tipo registro profissional do funcionario</param>
        void ValidarRegistroProfissional(long seqTipoFuncionario, TipoRegistroProfissional? tipoRegistroProfissionalFuncioario);

        /// <summary>
        /// Se o tipo de vínculo do funcionário em questão estiver com o flag
        ///"Obrigatório vínculo único" igual a "Sim" e existir um funcionário com o
        ///mesmo tipo de vínculo configurado cuja vigência coincida com o período
        ///de vigência sendo incluído/alterado, abortar a operação e emitir a
        ///mensagem: "Operação não permitida. O tipo de vínculo de funcionário
        ///"Descrição do tipo de vínculo" pode ser associado apenas a um único
        ///funcionário num mesmo período de vigência
        /// </summary>
        /// <param name="seqTipoFuncionario"></param>
        /// <param name="dataInicio"></param>
        /// <param name="dataFim"></param>
        void ValidarObrigatorioVinculoUnico(long seq,long seqFuncionario, long seqTipoFuncionario, DateTime dataInicio, DateTime? dataFim = null);

        /// <summary>
        /// Busca as entidades que tem vinculo com o tipo de vinculo funcionario
        /// </summary>
        /// <param name="seqTipoFuncionario"></param>
        /// <returns></returns>
        List<SMCDatasourceItem> BuscarEntidadesPorVinculoFuncionario(long seqTipoFuncionario);

        /// <summary>
        /// Busca a entidade pelo seq
        /// </summary>
        /// <param name="seqEntidadeVinculo"></param>
        /// <returns></returns>
        SMCDatasourceItem BuscarEntidadesSeq(long seqEntidadeVinculo);
    }
}