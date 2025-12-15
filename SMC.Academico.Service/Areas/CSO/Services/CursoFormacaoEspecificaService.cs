using SMC.Academico.Domain.Areas.CSO.DomainServices;
using SMC.Academico.Domain.Areas.CSO.Specifications;
using SMC.Academico.Domain.Areas.CSO.ValueObjects;
using SMC.Academico.ServiceContract.Areas.CSO.Data;
using SMC.Academico.ServiceContract.Areas.CSO.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.Service.Areas.CSO.Services
{
    public class CursoFormacaoEspecificaService : SMCServiceBase, ICursoFormacaoEspecificaService
    {
        #region [ DomainService ]

        private CursoFormacaoEspecificaDomainService CursoFormacaoEspecificaDomainService => Create<CursoFormacaoEspecificaDomainService>();

        #endregion [ DomainService ]

        /// <summary>
        /// Buscar a lista de formações específicas relacionadas ao curso para popular um Select
        /// </summary>
        /// <param name="seqCurso">Sequencial do curso</param>
        /// <returns>Lista de formação especifica</returns>
        public List<SMCDatasourceItem> BuscarCursoFormacaoEspecificaSelect(long seqCurso)
        {
            return CursoFormacaoEspecificaDomainService.BuscarCursoFormacaoEspecificaSelect(seqCurso);
        }

        public CursoFormacaoEspecificaData BuscarCursoFormacaoEspecifica(long seq)
        {
            var cursoFormacaoEspecifica = CursoFormacaoEspecificaDomainService.BuscarCursoFormacaoEspecifica(seq);
            var cursoFormacaoEspecificaData = cursoFormacaoEspecifica.Transform<CursoFormacaoEspecificaData>();
            // Arruma o nivel de ensino
            cursoFormacaoEspecificaData.SeqNivelEnsino = cursoFormacaoEspecifica?.Curso?.SeqNivelEnsino != null ? new List<long> { cursoFormacaoEspecifica.Curso.SeqNivelEnsino } : null;

            return cursoFormacaoEspecificaData;
        }

        /// <summary>
        /// Faz a busca dos CursoFormacaoEspecifica cadastrados para um curso
        /// </summary>
        /// <param name="seqCurso">Sequencial do curso</param>
        /// <returns>Retorna a lista de CursoFormacaoEspecifica cadastrados com suas superiores</returns>

        public List<CursoFormacaoEspecificaNodeData> BuscarCursoFormacoesEspecificas(CursoFormacaoEspecificaFiltroData filtro)
        {
            var nodes = CursoFormacaoEspecificaDomainService.BuscarCursoFormacoesEspecificas(filtro.Transform<CursoFormacaoEspecificaFilterSpecification>());
            var nodesRet = nodes.TransformList<CursoFormacaoEspecificaNodeData>();
            return nodesRet;
        }

        /// <summary>
        /// Buscar a lista de formações específicas das entidades superior ao curso para popular um Select
        /// </summary>
        /// <param name="seqCurso">Sequencial do curso</param>
        /// <returns>Lista de formação especifica</returns>
        public List<SMCDatasourceItem> BuscarTipoFormacaoEspecificaResponsavelSelect(long seqCurso)
        {
            return CursoFormacaoEspecificaDomainService.BuscarTipoFormacaoEspecificaResponsavelSelect(seqCurso);
        }

        /// <summary>
        /// Salvar a relação curso formação específica de acordo com o tipo de formação utilizado
        /// Não retorna o seq é uma gravação mestre detalhe
        /// <param name="formacao">Objeto com os dados da formação específica de acordo com o tipo</param>
        /// </summary>
        public long SalvarCursoFormacaoEspecifica(CursoFormacaoEspecificaData formacao)
        {
            var cursoFormacaoVO = formacao.Transform<CursoFormacaoEspecificaVO>();
            return CursoFormacaoEspecificaDomainService.SalvarCursoFormacaoEspecifica(cursoFormacaoVO);
        }

        /// <summary>
        /// Exclui uma formação específica de curso
        /// </summary>
        /// <param name="seq">Sequencial do curso formação específica</param>
        public void ExcluirCursoFormacaoEspecifica(long seq)
        {
            CursoFormacaoEspecificaDomainService.ExcluirCursoFormacaoEspecifica(seq);
        }

        /// <summary>
        /// Verificar se existe pelo menos uma Formação Especifica para o Curso Selecionado
        /// <param name="seqCurso">Sequencial do curso</param>
        /// <returns>True or false</returns>
        /// </summary>
        public bool VerificarExisteCursoFormacaoEspecifica(long seqCurso)
        {
            return CursoFormacaoEspecificaDomainService.VerificarExisteCursoFormacaoEspecifica(seqCurso);
        }

        public CursoFormacaoEspecificaData ConfigurarCursoFormacaoEspecifica(long seqCurso)
        {
            var cursoFormacaoEspecifica = CursoFormacaoEspecificaDomainService.ConfigurarCursoFormacaoEspecifica(seqCurso);
            var cursoFormacaoEspecificaData = cursoFormacaoEspecifica.Transform<CursoFormacaoEspecificaData>();
            // Arruma o nivel de ensino
            cursoFormacaoEspecificaData.SeqNivelEnsino = cursoFormacaoEspecifica?.Curso?.SeqNivelEnsino != null ? new List<long> { cursoFormacaoEspecifica.Curso.SeqNivelEnsino } : null;

            return cursoFormacaoEspecificaData;
        }

        /// <summary>
        /// Busca a descrição da formação específica segundo a regra RN_CSO_024 - Mascara - Formação Específica
        /// </summary>
        /// <param name="seqTipoFormacaoEspecifica">Sequencial do tipo da formação específica</param>
        /// <param name="seqGrauAcademico">Sequencial do grau da formação específica</param>
        /// <returns>Descrição da formação específica segundo a regra RN_CSO_024</returns>
        public string BuscarDescricaoFormacaoEspecifica(long? seqTipoFormacaoEspecifica, long? seqGrauAcademico)
        {
            return this.CursoFormacaoEspecificaDomainService.BuscarDescricaoFormacaoEspecifica(seqTipoFormacaoEspecifica, seqGrauAcademico);
        }

        /// <summary>
        /// Salva a replicação da formação específica do curso
        /// </summary>
        /// <param name="modelo">Dados para serem persistidos</param>
        public void SalvarReplicarCursoFormacaoEspecifica(ReplicarCursoFormacaoEspecificaData modelo)
        {
            this.CursoFormacaoEspecificaDomainService.SalvarReplicarCursoFormacaoEspecifica(modelo.Transform<ReplicarCursoFormacaoEspecificaVO>());
        }

        /// <summary>
        /// Verifica se a formação específica possui grau acadêmico
        /// </summary>
        /// <param name="seqCurso">Sequencial do curso</param>
        /// <param name="seqFormacaoEspecifica">Sequencial da formação específica</param>
        /// <returns>Retorna se a formacao especifica possui grau acadêmico</returns>
        public bool CursoFormacaoEspefificaPossuiGrau(long? seqCurso, long? seqFormacaoEspecifica)
        {
            return this.CursoFormacaoEspecificaDomainService.CursoFormacaoEspefificaPossuiGrau(seqCurso, seqFormacaoEspecifica);
        }
    }
}