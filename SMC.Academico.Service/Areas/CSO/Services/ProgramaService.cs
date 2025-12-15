using SMC.Academico.Common.Constants;
using SMC.Academico.Domain.Areas.CSO.DomainServices;
using SMC.Academico.Domain.Areas.CSO.Specifications;
using SMC.Academico.Domain.Areas.CSO.ValueObjects;
using SMC.Academico.Domain.Areas.ORG.DomainServices;
using SMC.Academico.ServiceContract.Areas.CSO.Data;
using SMC.Academico.ServiceContract.Areas.CSO.Interfaces;
using SMC.Academico.ServiceContract.Areas.ORG.Data;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.Service.Areas.CSO.Services
{
    public class ProgramaService : SMCServiceBase, IProgramaService
    {
        #region [ Serviços]

        private ProgramaDomainService ProgramaDomainService
        {
            get { return this.Create<ProgramaDomainService>(); }
        }

        private IEntidadeService EntidadeService
        {
            get { return this.Create<IEntidadeService>(); }
        }

        private InstituicaoTipoEntidadeDomainService InstituicaoTipoEntidadeDomainService
        {
            get { return this.Create<InstituicaoTipoEntidadeDomainService>(); }
        }

        #endregion [ Serviços]

        /// <summary>
        /// Busca as configurações de entidade para cadastro de um programa
        /// </summary>
        /// <returns>Dados da configuração de entidade</returns>
        public EntidadeData BuscaConfiguracoesEntidadePrograma()
        {
            return this.EntidadeService.BuscarConfiguracoesEntidadeComClassificacao(TOKEN_TIPO_ENTIDADE_EXTERNADA.PROGRAMA);
        }

        /// <summary>
        /// Busca os programas com suas formações específicas e cursos
        /// </summary>
        /// <param name="filtros">Filtros da listagem de programas</param>
        /// <returns>Programas com as descrições das formações específicas e cursos</returns>
        public SMCPagerData<ProgramaListaData> BuscarProgramas(ProgramaFiltroData filtros)
        {
            var specPrograma = filtros.Transform<ProgramaFilterSpecification>();
            var programasDominio = this.ProgramaDomainService.BuscarProgramas(specPrograma);
            var programasData = programasDominio.Transform<SMCPagerData<ProgramaListaData>>();
            return programasData;
        }

        /// <summary>
        /// Busca um programa com as confirgurações de entidade
        /// </summary>
        /// <param name="seq">Sequencia do programa a ser recuperado</param>
        /// <returns>Dados do Programa e configurações de enditade</returns>
        public ProgramaData BuscarPrograma(long seq)
        {
            return this.ProgramaDomainService
                .BuscarPrograma(seq)
                .Transform<ProgramaData>(BuscaConfiguracoesEntidadePrograma());
        }

        /// <summary>
        /// Buscar as situações do tipo de entidade programa para a instituição
        /// </summary>
        /// <returns>Lista de situações de um tipo de entidade programa na insituição</returns>
        public List<SMCDatasourceItem> BuscarSituacoesPrograma(bool listarInativos)
        {
            return this.InstituicaoTipoEntidadeDomainService.BuscarSituacoesTipoEntidadeDaInstituicaoSelect(TOKEN_TIPO_ENTIDADE_EXTERNADA.PROGRAMA, listarInativos);
        }

        /// <summary>
        /// Grava um programa e suas dependências. Idiomas e sua hierarquia(gerada no serviço)
        /// </summary>
        /// <param name="programa">Programa a ser gravado incluindo idiomas</param>
        /// <returns>Sequencia do programa gravado</returns>
        public long SalvarPrograma(ProgramaData programa)
        {
            return ProgramaDomainService.SalvarPrograma(programa.Transform<ProgramaVO>());
        }

        /// <summary>
        /// Recupera os programas de um grupo de programas
        /// </summary>
        /// <param name="seqEntidadeVinculo">Sequencial do grupo de programas</param>
        /// <returns>Sequenciais dos programas filhos do grupo de programas informado</returns>
        public List<long> BuscarSeqsProgramasGrupo(long seqEntidadeVinculo)
        {
            return ProgramaDomainService.BuscarSeqsProgramasGrupo(seqEntidadeVinculo);
        }

        /// <summary>
        /// Buscar programa do aluno
        /// </summary>
        /// <param name="seqAluno">Sequencial do aluno</param>
        /// <returns>Sequencial do progrma</returns>
        public long BuscarProgramaPorAluno(long seqAluno)
        {
            return this.ProgramaDomainService.BuscarProgramaPorAluno(seqAluno);
        }

        /// <summary>
        /// Busca os itens selecionados para confirmação durante a replicação da formação específica de programa
        /// </summary>
        /// <param name="model">Modelo com os itens selecionados</param>
        /// <returns>Itens para a exibição da TreeView de confirmação</returns>
        public List<ReplicaFormacaoEspecificaProgramaConfirmacaoData> BuscarItensSelecionadosReplicarFormacaoEspecificaPrograma(ReplicaFormacaoEspecificaProgramaData model)
        {
            return ProgramaDomainService.BuscarItensSelecionadosReplicarFormacaoEspecificaPrograma(model.Transform<ReplicaFormacaoEspecificaProgramaVO>()).TransformList<ReplicaFormacaoEspecificaProgramaConfirmacaoData>();
        }


        /// <summary>
        /// Salva a replicação da formação específica do programa
        /// </summary>
        /// <param name="modelo">Dados para serem persistidos</param>
        public void SalvarReplicarFormacaoEspecificaPrograma(ReplicaFormacaoEspecificaProgramaData model)
        {
            ProgramaDomainService.SalvarReplicarFormacaoEspecificaPrograma(model.Transform<ReplicaFormacaoEspecificaProgramaVO>());
        }
    }
}