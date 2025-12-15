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
    public class CursoOfertaService : SMCServiceBase, ICursoOfertaService
    {
        #region [ Service ]

        private CursoOfertaDomainService CursoOfertaDomainService
        {
            get { return this.Create<CursoOfertaDomainService>(); }
        }

        #endregion [ Service ]

        /// <summary>
        /// Busca cursos oferta de acordo com o sequencial
        /// </summary>
        /// <param name="seq">Sequencial de curso oferta</param>
        /// <returns>Objeto curso oferta</returns>
        public CursoOfertaData BuscarCursoOferta(long seq)
        {
            return CursoOfertaDomainService.BuscarCursoOferta(seq).Transform<CursoOfertaData>();
        }

        /// <summary>
        /// Busca os cursos oferta de acordo com os sequenciais
        /// </summary>
        /// <param name="seqs">Sequenciais</param>
        /// <returns>Cursos oferta</returns>
        public List<CursoOfertaData> BuscarCursosOferta(long[] seqs)
        {
            return CursoOfertaDomainService.BuscarCursosOferta(seqs).TransformList<CursoOfertaData>();
        }

        /// <summary>
        /// Buscar os cursos ofertas que foram cadastrados para os cursos de mesma hierarquia
        /// </summary>
        /// <param name="filtros">Filtros da listagem de cursos ofertas</param>
        /// <returns>SMCPagerData de cursos fertas</returns>
        public SMCPagerData<CursoOfertaData> BuscarCursoOfertasLookup(CursoOfertaFiltroData filtros)
        {
            var registros = CursoOfertaDomainService.BuscarCursoOfertasLookup(filtros.Transform<CursoOfertaFilterSpecification>());
            return registros.Transform<SMCPagerData<CursoOfertaData>>();
        }

        /// <summary>
        /// Buscar as ofertas de curso ativas para o curso informado
        /// </summary>
        /// <param name="seqCurso">Sequencial do curso</param>
        /// <returns>Select item com os valores de ofertas para o curso</returns>
        public List<SMCDatasourceItem> BuscarCursoOfertasAtivasSelect(long seqCurso)
        {
            return this.CursoOfertaDomainService.BuscarCursoOfertasAtivasSelect(seqCurso);
        }

        /// <summary>
        /// Verficia se já foram cadastrados tipos de curso para o nível de ensino do curso informado
        /// </summary>
        /// <param name="seqCurso">Sequencial do curso</param>
        /// <returns>Modelo de curso oferta caso os pré-requisitos sejam atendidos</returns>
        public CursoOfertaData VerificarDependenciasCursoOferta(long seqCurso)
        {
            return this.CursoOfertaDomainService.VerificarDependenciasCursoOferta(seqCurso).Transform<CursoOfertaData>();
        }

        /// <summary>
        /// Exclui uma oferta de curso
        /// </summary>
        /// <param name="seq">Sequencial da oferta de curso</param>
        public void ExcluirCursoOferta(long seq)
        {
            this.CursoOfertaDomainService.DeleteEntity(seq);
        }

        /// <summary>
        /// Recpera a máscara de nome de uma formação específica segundo a regra RN_CSO_023 - Mascara - Oferta de Curso
        /// </summary>
        /// <param name="seqFormacaoEspecifica">Sequencial da formação específica selecionada</param>
        /// <param name="seqCurso">Sequencial do curso selecionado</param>
        /// <returns>Máscara do curso oferta segundo a regra RN_CSO_023</returns>
        public string RecuperarMascaraCursoOferta(long seqFormacaoEspecifica, long seqCurso)
        {
            return this.CursoOfertaDomainService.RecuperarMascaraCursoOferta(seqFormacaoEspecifica, seqCurso);
        }

        /// <summary>
        /// Buscar as ofertas de curso para o processo informado
        /// </summary>
        /// <param name="seqProcesso">Sequencial do processo</param>
        /// <returns>Select item com os valores de ofertas para o processo</returns>
        public List<SMCDatasourceItem> BuscarCursosOfertasPorProcessoSelect(long seqProcesso)
        {
            return this.CursoOfertaDomainService.BuscarCursosOfertasPorProcessoSelect(seqProcesso);
        }

        /// <summary>
        /// Buscar as ofertas de curso para o aluno de acordo com o histórico escolar
        /// </summary>
        /// <param name="seqPessoaAtuacao">Sequencial da pessoa atuação aluno</param>
        /// <returns>Select item com os valores de ofertas que posssuem histórico escolar</returns>
        public List<SMCDatasourceItem> BuscarCursosOfertasPorAlunoHistoricoEscolarSelect(long seqPessoaAtuacao)
        {
            return this.CursoOfertaDomainService.BuscarCursosOfertasPorAlunoHistoricoEscolarSelect(seqPessoaAtuacao);
        }

        public long SalvarCursoOferta(CursoOfertaData modelo)
        {
            return this.CursoOfertaDomainService.SalvarCursoOferta(modelo.Transform<CursoOfertaVO>());
        }
    }
}