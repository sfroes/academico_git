using SMC.Academico.Common.Areas.CSO.Includes;
using SMC.Academico.Domain.Areas.CSO.DomainServices;
using SMC.Academico.Domain.Areas.CSO.Models;
using SMC.Academico.Domain.Areas.CSO.ValueObjects;
using SMC.Academico.ServiceContract.Areas.CSO.Data;
using SMC.Academico.ServiceContract.Areas.CSO.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Service;
using SMC.Framework.Specification;
using System.Collections.Generic;

namespace SMC.Academico.Service.Areas.CSO.Services
{
    public class FormacaoEspecificaService : SMCServiceBase, IFormacaoEspecificaService
    {
        #region [ DomainService ]

        private FormacaoEspecificaDomainService FormacaoEspecificaDomainService
        {
            get { return this.Create<FormacaoEspecificaDomainService>(); }
        }

        #endregion [ DomainService ]

        /// <summary>
        /// Buscar as formações específicas de uma entidade
        /// </summary>
        /// <param name="seqEntidadeResponsavel">Sequencia da entidade que têm as formações específicas</param>
        /// <returns>Array com dados dos nós das formações específicas com marcação das folhas</returns>
        public FormacaoEspecificaNodeData[] BuscarFormacoesEspecificas(long seqEntidadeResponsavel)
        {
            var formacaoVO = FormacaoEspecificaDomainService.BuscarFormacoesEspecificas(new FormacaoEspecificaFiltroVO() { SeqCurso = null, SeqsEntidadesResponsaveis = new long[] { seqEntidadeResponsavel } });
            return formacaoVO.TransformToArray<FormacaoEspecificaNodeData>();
        }

        /// <summary>
        /// Buscar as formações específicas de uma entidade para o lookup
        /// </summary>
        /// <param name="formacaoEspecificaFiltro">Sequencia da entidade que têm as formações específicas</param>
        /// <returns>Array com dados dos nós das formações específicas com marcação das folhas</returns>
        public FormacaoEspecificaNodeData[] BuscarFormacoesEspecificasLookup(FormacaoEspecificaFiltroData formacaoEspecificaFiltro)
        {
            var filtroVO = formacaoEspecificaFiltro.Transform<FormacaoEspecificaFiltroVO>();

            var formacaoVO = FormacaoEspecificaDomainService.BuscarFormacoesEspecificas(filtroVO);
            return formacaoVO.TransformToArray<FormacaoEspecificaNodeData>();
        }

        /// <summary>
        /// Buscar as formações epecíficas selecionadas numa entidade
        /// </summary>
        /// <param name="seqFormacoesEspecificas">seq das formações específias seleconadas</param>
        /// <returns>Array com hierarquias das formações específicas informadas</returns>
        public FormacaoEspecificaHierarquiaData[] BuscarFormacoesEspecificasGridLookup(long[] seqFormacoesEspecificas)
        {
            var formacaoVO = FormacaoEspecificaDomainService.BuscarFormacoesEspecificasHierarquia(seqFormacoesEspecificas);
            return formacaoVO.TransformToArray<FormacaoEspecificaHierarquiaData>();
        }

        /// <summary>
        /// Recupera as formações específicas do tipo "linha de pesquisa" associadas aos programas filhos de um grupo de programas
        /// </summary>
        /// <param name="seqGrupoPrograma">Sequencial do grupo de progrmas</param>
        /// <returns>Dados das formações específias que atendam aos critérios</returns>
        public List<SMCDatasourceItem> BuscarLinhasDePesquisaGrupoPrograma(long seqGrupoPrograma)
        {
            return this.FormacaoEspecificaDomainService.BuscarLinhasDePesquisaGrupoPrograma(seqGrupoPrograma);
        }

        /// <summary>
        /// Recupera uma formação específica pelo sequencial
        /// </summary>
        /// <param name="seq">Sequencial da formação específica</param>
        /// <returns>Dados da formação específica</returns>
        public FormacaoEspecificaListaData BuscarFormacaoEspecifica(long seq)
        {
            return this.FormacaoEspecificaDomainService.SearchProjectionByKey(new SMCSeqSpecification<FormacaoEspecifica>(seq), f=> new FormacaoEspecificaListaData() 
            {
                Seq = f.Seq,
                Descricao = f.Descricao,
                SeqTipoFormacaoEspecifica = f.SeqTipoFormacaoEspecifica,
                DescricaoTipoFormacaoEspecifica = f.TipoFormacaoEspecifica.Descricao
            });
        }

        /// <summary>
        /// Verifica se a formação específica exige grau acadêmico, baseado no tipo formação específica
        /// </summary>
        /// <param name="filtro">Filtro a ser cosiderado</param>
        /// <returns>Retorna se a formacao especifica exige grau acadêmico</returns>
        public bool FormacaoEspefificaExigeGrau(long seqFormacaoEspecifica)
        {
            return this.FormacaoEspecificaDomainService.FormacaoEspefificaExigeGrau(seqFormacaoEspecifica);
        }

        /// <summary>
        /// Verifica se o lookup de formação específica pode ou não apareceer no filtro da tela de
        /// consulta de condidatos de campanha
        /// </summary>
        /// <param name="seqTipoOferta">Seq do tipo de oferta selecionado</param>
        /// <param name="seqsEntidadesResponsaveis">Seqs das entidades responsáveis selecionados </param>
        /// <param name="seqCursoOferta">Seq do curso oferta selecionado</param>
        /// <returns>permissão para exibir o lookup de formação específica</returns>
        public bool BloquearCampoFormacaoEspecifica(long? seqTipoOferta, long? seqCursoOferta, List<long> seqsEntidadesResponsaveis)
        {
            return this.FormacaoEspecificaDomainService.BloquearCampoFormacaoEspecifica(seqTipoOferta, seqCursoOferta, seqsEntidadesResponsaveis);
        }

        /// <summary>
        /// Salva a uma formação específica
        /// </summary>
        /// <param name="model">Dados da formação específica para serem gravados</param>
        /// <returns>Sequencial da formação específica gravada</returns>
        public long SalvarFormacaoEspecifica(FormacaoEspecificaData model)
        {
            return FormacaoEspecificaDomainService.SalvarFormacaoEspecifica(model.Transform<FormacaoEspecificaVO>());
        }

        /// <summary>
        /// Buscar as formações específicas pelo sequencial do documento de conclusao
        /// </summary>
        /// <param name="seqDocumentoConclusao">Sequencial do documento de conclusao</param>
        /// <returns>Lista de formações específicas</returns>
        public List<FormacaoEspecificaData> BuscarFormacoesEspecificasPorDocumentoConclusao(long seqDocumentoConclusao)
        {
            return FormacaoEspecificaDomainService.BuscarFormacoesEspecificasPorDocumentoConclusao(seqDocumentoConclusao).TransformList<FormacaoEspecificaData>();
        }

        /// <summary>
        /// Verifica se a formação específica permite titulação, baseado no tipo formação específica
        /// </summary>
        /// <param name="seqFormacaoEspecifica">Sequencial formação especifica</param>
        /// <returns>Retorna se a formacao especifica permite titulação</returns>
        public bool FormacaoEspefificaExibeTitulacao(long seqFormacaoEspecifica)
        {
            return this.FormacaoEspecificaDomainService.FormacaoEspefificaExibeTitulacao(seqFormacaoEspecifica);
        }
    }
}