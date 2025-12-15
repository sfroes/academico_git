using SMC.Academico.Domain.Areas.PES.DomainServices;
using SMC.Academico.Domain.Areas.PES.Specifications;
using SMC.Academico.Domain.Areas.PES.ValueObjects;
using SMC.Academico.ServiceContract.Areas.PES.Data;
using SMC.Academico.ServiceContract.Areas.PES.Interfaces;
using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System;
using System.Collections.Generic;

namespace SMC.Academico.Service.Areas.PES.Services
{
    public class FuncionarioVinculoService : SMCServiceBase, IFuncionarioVinculoService
    {
        #region DomainService

        private FuncionarioVinculoDomainService FuncionarioVinculoDomainService => Create<FuncionarioVinculoDomainService>();

        #endregion DomainService

        /// <summary>
        /// Busca um vinculo de funcionario com suas depêndencias
        /// </summary>
        /// <param name="seq">Sequencial do funcionario</param>
        /// <returns>Dados do funcionario</returns>
        public FuncionarioVinculoData BuscarFuncionarioVinculo(long seq)
        {
            var retorno = FuncionarioVinculoDomainService.BuscarFuncionarioVinculo(seq).Transform<FuncionarioVinculoData>();
            if (retorno.SeqEntidadeVinculo != null)
            {
                retorno.ExibirCamposTipoEntidadesEEntidades = true;
            }
            return retorno;
        }

        /// <summary>
        /// Busca vinculos dos funcionários
        /// </summary>
        /// <param name="filtros">Filtros para busca</param>
        /// <returns>Dados paginados dos vinculos do funcionario</returns>
        public SMCPagerData<FuncionarioVinculoListaData> BuscarVinculosFuncionario(FuncionarioVinculoFiltroData filtros)
        {
            return FuncionarioVinculoDomainService.BuscarVinculosFuncionario(filtros.Transform<FuncionarioVinculoFilterSpecification>()).Transform<SMCPagerData<FuncionarioVinculoListaData>>();
        }

        /// <summary>
        /// Salvar dados do funcionario vinculo
        /// </summary>
        /// <param name="funcionarioVinculoVO">Dados as serem salvos</param>
        /// <returns>Sequencial do funcionario</returns>
        public long SalvarFuncionarioVinculo(FuncionarioVinculoData funcionarioVinculoVO)
        {
            return FuncionarioVinculoDomainService.SalvarFuncionarioVinculo(funcionarioVinculoVO.Transform<FuncionarioVinculoVO>());
        }

        /// <summary>
        /// Excluir o funcionario informado
        /// </summary>
        /// <param name="seq">Sequencial do funcionário a ser excluído</param>
        public void ExcluirFuncionarioVinculo(long seq)
        {
            FuncionarioVinculoDomainService.ExcluirFuncionarioVinculo(seq);
        }

        /// <summary>
        /// Valida se o funcionario se o funcionario tem data coincidentes para o mesmo vinculo
        /// </summary>
        ///<param name="funcionarioVinculoVO">Dados funcionario vinculo</param>
        public void ValidarDatasVinculo(FuncionarioVinculoData funcionarioVinculoData)
        {
            FuncionarioVinculoDomainService.ValidarDatasVinculo(funcionarioVinculoData.Transform<FuncionarioVinculoVO>());
        }

        /// <summary>
        /// Se o tipo de funcionário selecionado tiver o campo "Tipo Registro
        /// Profissional Obrigatório" configurado e o os dados dos de registro
        /// profissional do funcionário que está sendo cadastrado estiver vazio ou o
        /// tipo diferente do obrigatyório.: Emitir a mensagem de erro e abortar
        /// operação.
        /// </summary>
        /// <param name="seqTipoFuncionario">Sequencial do tipo de funcionário</param>
        /// <param name="tipoRegistroProfissionalFuncioario">Tipo registro profissional do funcionario</param>
        public void ValidarRegistroProfissional(long seqTipoFuncionario, TipoRegistroProfissional? tipoRegistroProfissionalFuncioario)
        {
            FuncionarioVinculoDomainService.ValidarRegistroProfissional(seqTipoFuncionario, tipoRegistroProfissionalFuncioario);
        }

        public void ValidarObrigatorioVinculoUnico(long seq,long seqFuncionario, long seqTipoFuncionario, DateTime dataInicio, DateTime? dataFim = null)
        {
            FuncionarioVinculoDomainService.ValidarObrigatorioVinculoUnico(seq,seqFuncionario,seqTipoFuncionario, dataInicio, dataFim);
        }

        public List<SMCDatasourceItem> BuscarEntidadesPorVinculoFuncionario(long seqTipoFuncionario)
        {
            return FuncionarioVinculoDomainService.BuscarEntidadesPorVinculoFuncionario(seqTipoFuncionario);
        }
        public SMCDatasourceItem BuscarEntidadesSeq(long seqEntidadeVinculo)
        {
            return FuncionarioVinculoDomainService.BuscarEntidadesSeq(seqEntidadeVinculo);
        }
    }
}