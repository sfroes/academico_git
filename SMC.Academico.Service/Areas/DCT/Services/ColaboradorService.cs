using SMC.Academico.Common.Areas.DCT.Enums;
using SMC.Academico.Domain.Areas.DCT.DomainServices;
using SMC.Academico.Domain.Areas.DCT.Models;
using SMC.Academico.Domain.Areas.DCT.ValueObjects;
using SMC.Academico.ServiceContract.Areas.DCT.Data;
using SMC.Academico.ServiceContract.Areas.DCT.Interfaces;
using SMC.Framework;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Service.Areas.DCT.Services
{
    public class ColaboradorService : SMCServiceBase, IColaboradorService
    {
        #region [ DomainServices ]

        private ColaboradorDomainService ColaboradorDomainService
        {
            get { return this.Create<ColaboradorDomainService>(); }
        }

        #endregion [ DomainServices ]

        /// <summary>
        /// Busca colaboradores com seus dados pessoais
        /// </summary>
        /// <param name="filtros">Filtros para busca</param>
        /// <returns>Dados paginados dos colaboradores</returns>
        public SMCPagerData<ColaboradorListaData> BuscarColaboradores(ColaboradorFiltroData filtros)
        {
            return this.ColaboradorDomainService.BuscarColaboradores(filtros.Transform<ColaboradorFiltroVO>()).Transform<SMCPagerData<ColaboradorListaData>>();
        }


        /// <summary>
        /// Busca colaboradores com seus dados pessoais para o lookup de colaboradores
        /// </summary>
        /// <param name="seq">Sequencial do colaborador</param>
        /// <returns>Dados do colaborador</returns>
        public ColaboradorLookupData BuscarColaboradorLookup(long seq)
        {
            return this.ColaboradorDomainService.BuscarColaboradorLookup(seq).Transform<ColaboradorLookupData>();
        } 

        /// <summary>
        /// Buscar todos os tipos de vinculo baseado na turma.
        /// </summary>
        /// <param name="seqTurma">Sequencial da turma para filtrar apenas os vinculos relacionados a ela</param>
        /// <returns>Dados dos vínculos relacionados a Turma</returns>
        public List<SMCDatasourceItem> BuscarEntidadeVinculoColaboradorPorTurmaSelect(long seqTurma)
        {
            return this.ColaboradorDomainService.BuscarEntidadeVinculoColaboradorPorTurmaSelect(seqTurma);
        }
        /// <summary>
        /// Verifica se as quantidades de filiação do tipo de atuação colaborador estão configuradas na instituição
        /// </summary>
        /// <returns>Novo objeto de colaborador caso o tipo de vínculo esteja configurado</returns>
        /// <exception cref="InstituicaoTipoAtuacaoNaoConfiguradaException">Caso o tipo de atuação não esteja configurado na instituição</exception>
        public ColaboradorData BuscarConfiguracaoColaborador()
        {
            return this.ColaboradorDomainService.BuscarConfiguracaoColaborador().Transform<ColaboradorData>();
        }

        /// <summary>
        /// Recupera um colaborador com seus dados pessoais e de contato
        /// </summary>
        /// <param name="seq">Sequencial do colaborado</param>
        /// <returns>Dados do colaborador com suas dependências</returns>
        public ColaboradorData BuscarColaborador(long seq)
        {
            return this.ColaboradorDomainService.BuscarColaborador(seq).Transform<ColaboradorData>();
        }

        /// <summary>
        /// Grava um colaborador com seus dados pessoais e dados de contato
        /// </summary>
        /// <param name="colaborador">Dados do colaborador a ser gravado</param>
        /// <returns>Sequencial do colaborador</returns>
        public long SalvarColaborador(ColaboradorData colaborador)
        {
            var vo = colaborador.Transform<ColaboradorVO>();
            if (colaborador.Cursos.SMCAny())
            {
                // Mapeia o data complexo da entidade para o dto com lista simples do enum.
                foreach (var cursoData in colaborador.Cursos)
                {
                    var cursoVO = vo.Cursos.FirstOrDefault(f => f.SeqCursoOfertaLocalidade == cursoData.SeqCursoOfertaLocalidade);
                    cursoVO.Atividades = cursoData.TipoAtividadeColaborador.Select(f => new ColaboradorVinculoAtividade()
                    {
                        TipoAtividadeColaborador = f
                    }).ToList();
                }
            }
            return this.ColaboradorDomainService.SalvarColaborador(vo);
        }

        /// <summary>
        /// Recupera os colaboradores por um tipo de atividade
        /// </summary>
        /// <param name="tipoAtividadeColaborador">Tipo de atividade desejada</param>
        /// <returns>Colaboradores do tipo de atividade desejada</returns>
        public List<SMCDatasourceItem> BuscarColaboradoresPorTipoAtividadeSelect(TipoAtividadeColaborador tipoAtividadeColaborador)
        {
            return this.ColaboradorDomainService.BuscarColaboradoresPorTipoAtividadeSelect(tipoAtividadeColaborador);
        }

        /// <summary>
        /// Busca os colaboradores com vínculo ativo na entidade do ingressante e com tipo de atividade “Orientação”
        /// no curso-oferta-localidade do curso-oferta-localidade-turno do item da oferta de campanha do ingressante.
        /// </summary>
        /// <param name="seqIngressante">Seq do ingressante</param>
        /// <returns>Lista de colaboradores</returns>
        public List<SMCDatasourceItem> BuscarColaboradoresPorIngressanteSelect(long seqIngressante)
        {
            return this.ColaboradorDomainService.BuscarColaboradoresPorIngressanteSelect(seqIngressante);
        }

        /// <summary>
        /// Buscar colaboradores seguindo a RN_ORT_013 - Filtro Orientador do caso de uso Orientação
        /// </summary>
        /// <param name="colaboradorFiltroData">Dados de filtro</param>
        /// <returns>Sequencial e nome dos orientadores</returns>
        public List<SMCDatasourceItem> BuscarColaboradoresOrientacaoSelect(ColaboradorFiltroData colaboradorFiltroData)
        {
            return this.ColaboradorDomainService.BuscarColaboradoresOrientacaoSelect(colaboradorFiltroData.Transform<ColaboradorFiltroVO>());
        }

        /// <summary>
        /// Busca os colaboradoes que atendam aos filtros informados
        /// </summary>
        /// <param name="filtros">Dados dos filtros</param>
        /// <returns>Lista de colaboradores ordenados por nome</returns>
        public List<SMCDatasourceItem> BuscarColaboradoresSelect(ColaboradorFiltroData filtros)
        {
            return this.ColaboradorDomainService.BuscarColaboradoresSelect(filtros.Transform<ColaboradorFiltroVO>());
        }

        /// <summary>
        /// Recupera um professor com a instituição selecionada no portal
        /// </summary>
        /// <param name="seqUsuarioSAS">Sequencial do usuario SAS</param>
        /// <param name="seqInstituicaoEnsino">Sequencial da instituição de ensino</param>
        /// <returns>Dados do colaborador</returns>
        public ColaboradorData BuscarProfessorLogin(long seqUsuarioSAS, long seqInstituicaoEnsino)
        {
            return this.ColaboradorDomainService.BuscarProfessorLogin(seqUsuarioSAS, seqInstituicaoEnsino).Transform<ColaboradorData>();
        }

        /// <summary>
        /// Busca os Colaboradores que tenham a atividade do tipo Orientação
        /// </summary>
        /// <param name="colaboradorFiltro">Filtro realizado na tela</param>
        /// <returns>Lista de colaboradores Orientadores ordenados por nome</returns>
        public List<SMCDatasourceItem> BuscarColaboradoresOrientadores(ColaboradorOrientadorFiltroData filtros)
        {
            return this.ColaboradorDomainService.BuscarColaboradoresOrientadores(filtros.Transform<ColaboradorOrientadorFiltroVO>());
        }

        /// <summary>
        /// Buscar colaboradores para turma
        /// </summary>
        /// <param name="seqTurma">Sequencial da turma</param>
        /// <param name="tipoAtividadeColaborador">Atividade do colaborador</param>
        /// <returns>Lista de colaboradores em formato select</returns>
        public List<SMCDatasourceItem> BuscarColaboradoresPorTurmaSelect(long seqTurma, TipoAtividadeColaborador tipoAtividadeColaborador)
        {
            return this.ColaboradorDomainService.BuscarColaboradoresPorTurmaSelect(seqTurma, tipoAtividadeColaborador);
        }

        /// <summary>
        /// Buscar professores aptos a lecionar na grade com seu vinculo ativo mais longo
        /// </summary>
        /// <param name="colaboradorFiltroVO">Filtro do colaborador</param>
        /// <returns>Dados dos professores com seus vinculos</returns>
        public List<ColaboradorGradeData> BuscarColaboradoresAptoLecionarGrade(ColaboradorFiltroData colaboradorFiltroData)
        {
            return ColaboradorDomainService.BuscarColaboradoresAptoLecionarGrade(colaboradorFiltroData.Transform<ColaboradorFiltroVO>())
                                           .TransformList<ColaboradorGradeData>();
        }

    }
}