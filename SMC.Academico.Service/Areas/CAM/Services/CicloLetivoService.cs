using SMC.Academico.Domain.Areas.CAM.DomainServices;
using SMC.Academico.Domain.Areas.CAM.Models;
using SMC.Academico.Domain.Areas.CAM.Specifications;
using SMC.Academico.ServiceContract.Areas.CAM.Data;
using SMC.Academico.ServiceContract.Areas.CAM.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Service;
using SMC.Framework.Specification;
using System.Collections.Generic;

namespace SMC.Academico.Service.Areas.CAM.Services
{
    public class CicloLetivoService : SMCServiceBase, ICicloLetivoService
    {
        #region [ Serviços ]

        private CicloLetivoDomainService CicloLetivoDomainService
        {
            get { return this.Create<CicloLetivoDomainService>(); }
        }

        #endregion [ Serviços ]

        /// <summary>
        /// Grava um CicloLetivo aplicando suas validações e preenchendo o campo descrição
        /// </summary>
        /// <param name="cicloLetivo">Ciclo letivo a ser gravado</param>
        /// <returns>Sequencial do ciclo letivo gravado</returns>
        public long SalvarCicloLetivo(CicloLetivoData cicloLetivo)
        {
            var cicloLetivoDominio = cicloLetivo.Transform<CicloLetivo>();
            return this.CicloLetivoDomainService.SalvarCicloLetivo(cicloLetivoDominio);
        }

        /// <summary>
        /// Busca os ciclos letivos que atendam aos filtros informados
        /// </summary>
        /// <param name="filtro">Filtro dos Ciclos a Serem copiados</param>
        /// <returns>Lista paginada com os Ciclos Letivos que atendam aos filtros tendo o ano e o nº do cliclo subistituidos pelos do destino.</returns>
        public SMCPagerData<CicloLetivoData> BuscarCiclosLetivosCopia(CicloLetivoCopiaFiltroData filtro)
        {
            var spec = filtro.Transform<CicloLetivoFilterSpecification>();
            var ciclos = this.CicloLetivoDomainService
                .BuscarCiclosLetivosCopia(spec, filtro.AnoDestino)
                .Transform<SMCPagerData<CicloLetivoData>>();
            return ciclos;
        }

        /// <summary>
        /// Copia todos Ciclos Letivos informados substituindo o Ano e Número pelos valores informados
        /// </summary>
        /// <param name="filtro">Filtro para obter os Ciclos Letivos originais</param>
        public void CopiarCiclosLetivos(CicloLetivoCopiaFiltroData filtro)
        {
            var spec = filtro.Transform<CicloLetivoFilterSpecification>();
            this.CicloLetivoDomainService.CopiarCiclosLetivos(spec, filtro.AnoDestino);
        }

        /// <summary>
        /// Busca os Ciclos Letivos com Níveis de Ensino para o lookup
        /// </summary>
        /// <param name="filtro">Filtro dos Ciclos Letivos</param>
        /// <returns>Lista páginada dos ciclos letivos com Níveis de Ensino</returns>
        public SMCPagerData<CicloLetivoData> BuscarCiclosLetivosLookup(CicloLetivoFiltroData filtro)
        {
            // Ao pesquisar o ciclo letivo para o lookup, se o filtro estiver sendo realizado pelo 
            // Seq, mudar para Descrição, pois a descrição do ciclo letivo pode ser númerica e, nesse
            // caso, o componente do lookup está tratando indevidamente realizando o filtro pelo Seq.
            if (filtro.Seq.HasValue)
            {
                filtro.Descricao = filtro.Seq.ToString();
                filtro.Seq = null;
            }

            var spec = filtro.Transform<CicloLetivoFilterSpecification>();
            var ciclosLetivosDominio = this.CicloLetivoDomainService.BuscarCiclosLetivosLookup(spec);
            var ciclosLetivosData = ciclosLetivosDominio.Transform<SMCPagerData<CicloLetivoData>>();
            return ciclosLetivosData;
        }

        /// <summary>
        /// Busca os Ciclos Letivos com Níveis de Ensino para o lookup ao selecionar.
        /// </summary>
        /// <param name="filtro">Filtro dos Ciclos Letivos</param>
        /// <returns>Lista páginada dos ciclos letivos com Níveis de Ensino</returns>
        public List<CicloLetivoData> BuscarCiclosLetivosLookupSelect(long[] seqs)
        {
            var spec = new SMCContainsSpecification<CicloLetivo, long>(f => f.Seq, seqs);
            var ciclosLetivosDominio = this.CicloLetivoDomainService.BuscarCiclosLetivosLookup(spec);
            var ciclosLetivosData = ciclosLetivosDominio.TransformList<CicloLetivoData>();
            return ciclosLetivosData;
        }

        public CicloLetivoData BuscarCicloLetivo(long seq)
        {
            return CicloLetivoDomainService.SearchByKey(new SMCSeqSpecification<CicloLetivo>(seq)).Transform<CicloLetivoData>();
        }

        /// <summary>
        /// Busca todos os ciclos letivos por campanha e nível de ensino
        /// </summary>
        /// <param name="seqCampanha">Sequencial da campanha</param>
        /// <param name="seqNivelEnsino">Sequencial do nivel ensino</param>
        /// <returns>Dados dos ciclos letivos ordenados por descrição</returns>
        public List<SMCDatasourceItem> BuscarCiclosLetivosPorCampanhaNivelSelect(long seqCampanha, long seqNivelEnsino)
        {
            return CicloLetivoDomainService.BuscarCiclosLetivosPorCampanhaNivelSelect(seqCampanha, seqNivelEnsino);
        }

        /// <summary>
        /// Busca todos os ciclos letivos por campanha
        /// </summary>
        /// <param name="seqCampanha">Sequencial da campanha</param>
        /// <returns>Dados dos ciclos letivos ordenados por descrição</returns>
        public List<SMCDatasourceItem> BuscarCiclosLetivosPorCampanhaSelect(long seqCampanha)
        {
            return CicloLetivoDomainService.BuscarCiclosLetivosPorCampanhaSelect(seqCampanha);
        }

        /// <summary>
        /// Busca os ciclo letivos que o aluno possui vinculado às turmas cursadas/cursando
        /// </summary>
        /// <param name="seqAluno">Sequencial do aluno</param>
        /// <returns>Lista de ciclo letivos para seleção</returns>
        public List<SMCDatasourceItem> BuscarCiclosLetivosPorAluno(long seqAluno)
        {
            return CicloLetivoDomainService.BuscarCiclosLetivosPorAluno(seqAluno);
        }

        /// <summary>
        /// Recupera todos os ciclos letivos do aluno independente da situação
        /// </summary>
        /// <param name="seqAluno">Sequencial do aluno</param>
        /// <returns>Ciclos letivos vinculados ao aluno</returns>
        public List<SMCDatasourceItem> BuscarCiclosLetivosPorHistoricoAluno(long seqAluno)
        {
            return CicloLetivoDomainService.BuscarCiclosLetivosPorHistoricoAluno(seqAluno);
        }

        /// <summary>
        /// Busca o ciclo letivo com a formatação {Numero}º/{Ano}
        /// </summary>
        /// <param name="seqCicloLetivo"></param>
        /// <returns>Ciclo letivo formatado {Numero}º/{Ano}</returns>
        public string BuscarDescricaoFormatadaCicloLetivo(long seqCicloLetivo)
        {
            return CicloLetivoDomainService.BuscarDescricaoFormatadaCicloLetivo(seqCicloLetivo);
        }


        /// <summary>
        /// Busca uma quantidade de ciclos letivos do aluno, em que o mesmo possui situação no ciclo letivo
        /// </summary>
        /// <param name="seqAluno">Sequencial do aluno</param>
        /// <param name="quantidadeCiclosLetivos">Quantidade de ciclos letivos anteriores ao atual</param>
        /// <returns>Lista de ciclos letivos em que o aluno pussui situação</returns>
        public List<SMCDatasourceItem> BuscarCiclosLetivosAlunoComSituacaoSelect(long seqAluno, int? quantidadeCiclosLetivosAnteriores = null)
        {
            return this.CicloLetivoDomainService.BuscarCiclosLetivosAlunoComSituacaoSelect(seqAluno, quantidadeCiclosLetivosAnteriores);
        }
    }
}