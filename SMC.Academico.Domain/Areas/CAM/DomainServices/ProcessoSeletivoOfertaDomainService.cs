using SMC.Academico.Common.Areas.CAM.Exceptions;
using SMC.Academico.Domain.Areas.CAM.Models;
using SMC.Academico.Domain.Areas.CAM.Specifications;
using SMC.Academico.Domain.Areas.CAM.ValueObjects;
using SMC.Framework.Domain;
using SMC.Inscricoes.ServiceContract.Areas.INS.Interfaces;
using System;
using System.Linq;
using SMC.Inscricoes.Common;
using SMC.Academico.Common.Constants;
using System.Collections.Generic;
using SMC.Academico.Domain.Areas.SRC.DomainServices;
using SMC.Academico.Common.Areas.CAM.Includes;
using SMC.Framework.UnitOfWork;
using SMC.Framework;
using SMC.Academico.Domain.Areas.TUR.DomainServices;
using SMC.Academico.Domain.Areas.ALN.Specifications;
using SMC.Academico.Domain.Areas.ALN.DomainServices;
using SMC.Framework.Specification;
using SMC.Inscricoes.ServiceContract.Areas.INS.Data;

namespace SMC.Academico.Domain.Areas.CAM.DomainServices
{
    public class ProcessoSeletivoOfertaDomainService : AcademicoContextDomain<ProcessoSeletivoOferta>
    {
        #region DomainServices
        private ProcessoDomainService ProcessoDomainService => Create<ProcessoDomainService>();

        private ProcessoSeletivoDomainService ProcessoSeletivoDomainService => Create<ProcessoSeletivoDomainService>();

        private CampanhaOfertaDomainService CampanhaOfertaDomainService => Create<CampanhaOfertaDomainService>();

        private TipoProcessoSeletivoDomainService TipoProcessoSeletivoDomainService => Create<TipoProcessoSeletivoDomainService>();

        private InstituicaoNivelTipoProcessoSeletivoDomainService InstituicaoNivelTipoProcessoSeletivoDomainService => Create<InstituicaoNivelTipoProcessoSeletivoDomainService>();

        private ConvocacaoDomainService ConvocacaoDomainService => Create<ConvocacaoDomainService>();

        private DivisaoTurmaDomainService DivisaoTurmaDomainService => Create<DivisaoTurmaDomainService>();

        private ConvocacaoOfertaDomainService ConvocacaoOfertaDomainService => Create<ConvocacaoOfertaDomainService>();

        private IngressanteDomainService IngressanteDomainService => Create<IngressanteDomainService>();

        private CampanhaDomainService CampanhaDomainService => Create<CampanhaDomainService>();


        #endregion

        #region Services
        private IEtapaProcessoService EtapaProcessoService => Create<IEtapaProcessoService>();

        private IOfertaService OfertaService => Create<IOfertaService>();

        private IHierarquiaOfertaService HierarquiaOfertaService => Create<IHierarquiaOfertaService>();

        public IProcessoService ProcessoService => Create<IProcessoService>();

        #endregion

        /// <summary>
        /// Buscar o processo seletivo oferta para verificação de quantidades
        /// </summary>
        /// <param name="seqProcessoSeletivo">Sequencial do processo seletivo</param>
        /// <param name="seqDivisaoTurma">Sequencial da divisão da turma</param>
        /// <returns>Processo seletivo oferta com as quantidades</returns>
        public ProcessoSeletivoOfertaVO BuscarProcessoSeletivoOfertaQuantidades(long seqProcessoSeletivo, long seqDivisaoTurma)
        {
            ProcessoSeletivoOfertaFilterSpecification spec = new ProcessoSeletivoOfertaFilterSpecification();
            spec.SeqProcessoSeletivo = seqProcessoSeletivo;
            spec.SeqDivisaoTurma = seqDivisaoTurma;
            var processoSeletivoOferta = this.SearchProjectionBySpecification(spec,
                                                   p => new ProcessoSeletivoOfertaVO()
                                                   {
                                                       Seq = p.Seq,
                                                       QuantidadeVagas = p.QuantidadeVagas,
                                                       QuantidadeVagasOcupadas = p.QuantidadeVagasOcupadas,
                                                       ProcessoReservaVaga = p.ProcessoSeletivo.ReservaVaga,
                                                   }).FirstOrDefault();

            return processoSeletivoOferta;
        }

        /// <summary>
        /// Atualizar os dados quantidade de vagas oculpadas 
        /// </summary>
        /// <param name="seq">Sequencial do processo seletivo oferta</param>
        /// <param name="quantidadeVagasOcupadas">Quantidade de vagas oculpadas</param>
        public void AtualizarQuantidadeVagasOcupadas(long seq, short quantidadeVagasOcupadas)
        {
            var processoSeletivoOferta = new ProcessoSeletivoOferta()
            {
                Seq = seq,
                QuantidadeVagasOcupadas = quantidadeVagasOcupadas
            };

            UpdateFields(processoSeletivoOferta, f => f.QuantidadeVagasOcupadas);
        }

        public void SalvarOfertaProcessoSeletivo(ProcessoSeletivoOfertaVO processoSeletivoOfertaVO)
        {
            var includeProcessoSeletivo = IncludesProcessoSeletivo.Ofertas | IncludesProcessoSeletivo.ProcessosMatricula | IncludesProcessoSeletivo.TipoProcessoSeletivo;

            var processoSeletivo = ProcessoSeletivoDomainService.SearchByKey(processoSeletivoOfertaVO.SeqProcessoSeletivo, includeProcessoSeletivo);

            /// RN_CAM_035 - Consistência de associação de oferta do processo seletivo 
            ValidarConsistenciaAssociacaoOfertaProcessoSeletivo_RN_CAM_035(processoSeletivoOfertaVO, processoSeletivo);

            using (var unitOfWork = SMCUnitOfWork.Begin())
            {
                ///RN_CAM_033 - Associação de oferta do processo seletivo 
                ValidarAssociacaoOfertaProcessoSeletivo_RN_CAM_033(processoSeletivoOfertaVO, processoSeletivo);

                ///RN_CAM_036 - Exclusão de oferta do processo seletivo

                // Persistir o Processo seletivo e suas ofertas
                ProcessoSeletivoDomainService.SaveEntity(processoSeletivo);

                unitOfWork.Commit();
            }
        }

        private void ValidarAssociacaoOfertaProcessoSeletivo_RN_CAM_033(ProcessoSeletivoOfertaVO processoSeletivoOfertaVO, ProcessoSeletivo processoSeletivo)
        {
            /*RN_CAM_033 Associação de oferta do processo seletivo
            1. Para cada oferta selecionada, criar um registro de oferta associado ao processo seletivo, com a 
            referência da oferta da campanha, a quantidade de vagas "0" e a quantidade de vagas ocupadas "0".*/
            foreach (var seqCampanhaOferta in processoSeletivoOfertaVO.Ofertas)
            {
                var processoSeletivoOferta = new ProcessoSeletivoOferta()
                {
                    SeqProcessoSeletivo = processoSeletivo.Seq,
                    SeqCampanhaOferta = seqCampanhaOferta,
                    QuantidadeVagas = 0,
                    QuantidadeVagasOcupadas = 0
                };

                SaveEntity(processoSeletivoOferta);
            }

            /*2. Se existir um processo de inscrição do GPI associado ao processo seletivo, executar a regra 
            RN_CAM_076 - Criação da oferta no GPI, para cada oferta selecionada.*/
            if (processoSeletivo.SeqProcessoGpi.HasValue && processoSeletivoOfertaVO.Ofertas.Count > 0)
            {
                //Busca os processos seletivos com suas respectivas ofertas
                var dadoProcessoSeletivoSelecionado = ProcessoSeletivoDomainService.BuscarProcessosSeletivosIntegracaoGpi(new long[] { processoSeletivo.Seq }, processoSeletivoOfertaVO.Ofertas.ToArray()).FirstOrDefault();

                //Prepara os dados para serem enviados para o GPI
                var itensOfertasHierarquiasOfertas = CampanhaDomainService.PrepararItensHierarquiaOfertaIntegracaoGpi(processoSeletivo.SeqProcessoGpi.GetValueOrDefault(),dadoProcessoSeletivoSelecionado.Ofertas);

                //Recebe os dados de retorno do GPI (Sequenciais origem e os novos que foram gerados)
                var hierarquiasOfertasGpi = HierarquiaOfertaService.AdicionarItemHierarquiaOferta(processoSeletivo.SeqProcessoGpi.Value, itensOfertasHierarquiasOfertas);

                //Specification para recuperação dos processos seletivos
                var specProcessoSeletivoOferta = new ProcessoSeletivoOfertaFilterSpecification() { Seqs = hierarquiasOfertasGpi.Keys.ToArray() };

                //Recupera os processos seletivos ofertas para atualizar o SeqHierarquiaOFertaGpi que foi gerado
                var processosSeletivosOfertas = SearchBySpecification(specProcessoSeletivoOferta).ToList();

                //Atualiza o SeqHierarquiaOfertaGpi em cada processo seletivo oferta
                foreach (var hierarquiaOfertaGpi in hierarquiasOfertasGpi)
                {
                    var processoSeletivoOferta = processosSeletivosOfertas.FirstOrDefault(p => p.Seq == hierarquiaOfertaGpi.Key);
                    processoSeletivoOferta.SeqHierarquiaOfertaGpi = hierarquiaOfertaGpi.Value;
                    UpdateFields<ProcessoSeletivoOferta>(processoSeletivoOferta, x => x.SeqHierarquiaOfertaGpi);
                }
                
            }

            /*3. Incluir a(s) nova(s) oferta(s) na(s) convocação(ões) selecionada(s) pelo usuário, caso ele tenha 
            selecionado alguma. executando as regras: 
            RN_CAM_041 - Consistência de associação de oferta da convocação 
            RN_CAM_042 - Associação de oferta à convocação */
            ConsistenciaEAssociacaoOfertaConvocacao_RN_CAM_041_E_RN_CAM_042(processoSeletivoOfertaVO);

        }

        /// <summary>
        /// RN_CAM_035 - Consistência de associação de oferta do processo seletivo 
        /// </summary>
        /// <param name="processoSeletivoOfertaVO"></param>
        /// <param name="processoSeletivo"></param>
        private void ValidarConsistenciaAssociacaoOfertaProcessoSeletivo_RN_CAM_035(ProcessoSeletivoOfertaVO processoSeletivoOfertaVO, ProcessoSeletivo processoSeletivo)
        {
            foreach (var oferta in processoSeletivoOfertaVO.Ofertas)
            {

                // 1. Verifica por ofertas duplicadas
                /*1. Não permitir associar uma mesma oferta mais de uma vez ao processo seletivo. 
               * Em caso de violação, abortar a operação e emitir a mensagem de erro:
               *
               *“Associação não permitida.A oferta<descrição da oferta duplicada> já foi associada ao processo seletivo.".*/
                var ofertaDuplicada = processoSeletivo.Ofertas.FirstOrDefault(o => o.SeqCampanhaOferta == oferta);

                if (ofertaDuplicada != null)
                {
                    var descOferta = CampanhaOfertaDomainService.SearchProjectionByKey(oferta, o => o.Descricao);
                    throw new ProcessoSeletivoOfertaDuplicadaException(descOferta);
                }
                // Inclui a nova oferta da campanha na oferta do processo seletivo 
                processoSeletivo.Ofertas.Add(new ProcessoSeletivoOferta()
                {
                    SeqCampanhaOferta = oferta
                });
            }

            var ingressoDireto = TipoProcessoSeletivoDomainService.SearchProjectionByKey(processoSeletivo.SeqTipoProcessoSeletivo, x => x.Token)
                        == TOKEN_TIPO_PROCESSO_SELETIVO.DISCIPLINA_ISOLADA;

            /*2.Se o tipo de processo do processo seletivo em questão for de ingresso direto e o tipo de oferta de qualquer 
               *uma das ofertas associadas exigir curso - oferta - localidade - turno, para cada processo de matrícula associado
               *ao processo seletivo, verificar se para cada oferta que está sendo associada, existe uma configuração para o
               *processo de matrícula com o curso - oferta - localidade - turno da oferta associada e todos os vínculos das
               *formas de ingresso do tipo de processo seletivo. 
               * Se não existir, verificar se existe uma configuração para o processo de matrícula com o nível de ensino do 
               * curso - oferta - localidade - turno da oferta associada e todos os vínculos das formas de ingresso do tipo de 
               * processo seletivo.Se não existir, abortar a operação e emitir a mensagem de erro:
               *
               *"Associação não permitida. Existem ofertas cujo nível de ensino ou a oferta de curso por localidade e turno não estão 
               * configurados para o(s) processo(s) de matrícula e o(s) vínculo(s) da(s) forma(s) de ingresso do tipo de processo seletivo
               * do processo seletivo em questão.”*/

            // Para cada oferta associada.
            for (int i = 0; i < processoSeletivo.Ofertas.Count; i++)
            {
                var oferta = processoSeletivo.Ofertas[i];

                // Executa apenas quando estiver associando e se for ingresso direto.
                if (oferta.Seq == 0 && ingressoDireto)
                {
                    // No teste da campanha oferta 629, não existia curso oferta localidade turno associado e da erro na linha abaixo.
                    // A regra está correta?
                    var dados = CampanhaOfertaDomainService.SearchProjectionByKey(oferta.SeqCampanhaOferta, x => new
                    {
                        ExigeCursoOfertaLocalidadeTurno = x.TipoOferta.ExigeCursoOfertaLocalidadeTurno,
                        SeqCursoOfertaLocalidadeTurno = x.Itens.FirstOrDefault().SeqCursoOfertaLocalidadeTurno,
                        SeqNivelEnsino = (long?)x.Itens.FirstOrDefault().CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.CursoOferta.Curso.SeqNivelEnsino
                    });
                    if (dados.ExigeCursoOfertaLocalidadeTurno)
                    {
                        foreach (var processoMatricula in processoSeletivo.ProcessosMatricula)
                        {
                            var tiposVinculos = InstituicaoNivelTipoProcessoSeletivoDomainService.SearchProjectionBySpecification(new InstituicaoNivelTipoProcessoSeletivoFilterSpecification(processoSeletivo.SeqTipoProcessoSeletivo),
                                                                        x => x.InstituicaoNivelFormaIngresso.FormaIngresso.SeqTipoVinculoAluno).ToList();
                            if (ProcessoDomainService.Count(new ProcessoMatriculaPorCursoSpecification(
                                                                    processoMatricula.Seq,
                                                                    dados.SeqCursoOfertaLocalidadeTurno.Value,
                                                                    tiposVinculos)) == 0)
                            {
                                if (ProcessoDomainService.Count(new ProcessoMatriculaPorNivelEnsinoSpecification(
                                                           processoMatricula.Seq,
                                                           new List<long> { dados.SeqNivelEnsino.Value },
                                                           tiposVinculos)) == 0)
                                {
                                    throw new ProcessoSeletivoOfertaNivelEnsinoOfertaCursoException();
                                }
                            }
                        }
                    }
                }
            }

            //3. Verifica se possui processo GPI e se este possui estapa de inscrição.
            //Se houver processo do GPI associado ao processo seletivo da oferta em questão, verificar se ele possui etapa de inscrição 
            //cadastrada. Se não possuir, abortar a operação e emitir a mensagem de erro:
            //"Associação não permitida. O processo do GPI associado ao processo seletivo desta oferta não possui etapa de inscrição cadastrada."
            ProcessoSeletivoDomainService.VerificarEtapaInscricaoProcessoGPI(processoSeletivo.SeqProcessoGpi);

        }

        /// <summary>
        /// RN_CAM_041 - Consistência de associação de oferta da convocação
        /// </summary>
        /// <param name="processoSeletivoOfertaVO"></param>
        /// <param name="processoSeletivo"></param>
        private void ValidarConsistenciaAssociacaoOfertaConvocacao(ProcessoSeletivoOfertaVO processoSeletivoOfertaVO, ProcessoSeletivo processoSeletivo)
        {
            // 1. - Validar Duplicidade
            // Percorro as convocações
            foreach (var seqConvocacao in processoSeletivoOfertaVO.Convocacoes)
            {
                //Percorro as ofertas da convocação, para veficar se existe ofertas duplicadas
                var ofertaDuplicada = ConvocacaoDomainService.SearchProjectionByKey(seqConvocacao, x => x.Ofertas.FirstOrDefault(o => processoSeletivoOfertaVO
                                        .Ofertas.Contains(o.ProcessoSeletivoOferta.SeqCampanhaOferta))
                                        .ProcessoSeletivoOferta.CampanhaOferta.Descricao);

                if (!string.IsNullOrEmpty(ofertaDuplicada))
                {
                    throw new ProcessoSeletivoOfertaConvocacaoDuplicadaException(ofertaDuplicada);
                }
            }

            // 2. Validar configuração do processo seletivo da convocação
            if (processoSeletivo.TipoProcessoSeletivo != null && !processoSeletivo.TipoProcessoSeletivo.IngressoDireto)
            {
                // Monto a especificação da Campanha Oferta
                var specCampanhaOferta = new CampanhaOfertaFilterSpecification()
                {
                    Seqs = processoSeletivoOfertaVO.Ofertas.ToArray()
                };

                // Verifico se alguma oferta Exige Curso Oferta Localidade Turno
                var exigeCursoOfertaLocalidadeTurno = CampanhaOfertaDomainService.SearchProjectionBySpecification(specCampanhaOferta, x => x.TipoOferta.ExigeCursoOfertaLocalidadeTurno == true).FirstOrDefault();
                if (exigeCursoOfertaLocalidadeTurno)
                {
                    // Percorro as convocações
                    foreach (var seqConvocacao in processoSeletivoOfertaVO.Convocacoes)
                    {
                        // recupero a convocação e suas ofertas
                        var seqCicloLetivoConvocacaoOferta = ConvocacaoDomainService.SearchProjectionByKey(seqConvocacao, x => x.CampanhaCicloLetivo.SeqCicloLetivo);

                        // Percorro as ofertas, selecionadas pelo usuário
                        foreach (var seqCampanhaOferta in processoSeletivoOfertaVO.Ofertas)
                        {
                            var campanhaOfertaCurso = CampanhaOfertaDomainService.SearchProjectionByKey(seqCampanhaOferta, x => x.Itens.Select(i => i.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.CursoOferta.Curso).FirstOrDefault());

                            var processoSeletivoOferta = processoSeletivo.Ofertas.FirstOrDefault(o => o.SeqCampanhaOferta == seqCampanhaOferta);

                            // Busco o seqProcesso associado com o ciclo letivo da convocação
                            var seqProcesso = ProcessoSeletivoDomainService.SearchProjectionByKey(processoSeletivoOferta.SeqProcessoSeletivo, x =>
                                                            x.ProcessosMatricula.FirstOrDefault(p => p.Processo.SeqCicloLetivo == seqCicloLetivoConvocacaoOferta).SeqProcesso);

                            bool existeVinculoProcesso = false;

                            if (seqProcesso > 0 && campanhaOfertaCurso != null)
                            {
                                // Busco o curso oferta localidade associado à configuração processo
                                var campanhaOfertaProcessoCurso = ProcessoDomainService.SearchProjectionByKey(seqProcesso, x =>
                                                                    x.Configuracoes.Select(p => p.Cursos.FirstOrDefault(c => c.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade
                                                                     .CursoOferta.Curso.Seq == campanhaOfertaCurso.Seq)).FirstOrDefault());

                                if (campanhaOfertaProcessoCurso != null)
                                {
                                    //busco o tipo vinculo da configuração do processo, comparando com o vínculo do processo seletivo
                                    var tipoVinculoAlunoProcesso = ProcessoDomainService.SearchProjectionByKey(seqProcesso, x => x.Configuracoes.Select(p =>
                                                                     p.TiposVinculoAluno.FirstOrDefault(v => v.SeqTipoVinculoAluno == processoSeletivo.SeqTipoProcessoSeletivo)
                                                                      ).FirstOrDefault());

                                    existeVinculoProcesso = (tipoVinculoAlunoProcesso != null);
                                }
                                else
                                {
                                    // Se não Existir 
                                    // Busco o nível de ensino do curso oferteta localidade turno da oferta
                                    var seqCampanhaOfertaCursoNivelEnsino = CampanhaOfertaDomainService.SearchProjectionByKey(seqCampanhaOferta, x => x.Itens.Select(i => i.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.CursoOferta.Curso.SeqNivelEnsino).FirstOrDefault());

                                    // Busco o nível de ensino do curso oferta localidade Turno associado à configuração processo
                                    var configuracaoProcessoCursoNivelEnsino = ProcessoDomainService.SearchProjectionByKey(seqProcesso, x =>
                                                                               x.Configuracoes.Select(p => p.NiveisEnsino.FirstOrDefault(c => c.SeqNivelEnsino == seqCampanhaOfertaCursoNivelEnsino)).FirstOrDefault());

                                    //busco o tipo vinculo da configuração do processo, comparando com o vínculo do processo seletivo
                                    var tipoVinculoAlunoProcesso = ProcessoDomainService.SearchProjectionByKey(seqProcesso, x => x.Configuracoes.Select(p =>
                                                                     p.TiposVinculoAluno.FirstOrDefault(v => v.SeqTipoVinculoAluno == processoSeletivo.SeqTipoProcessoSeletivo)
                                                                      ).FirstOrDefault());

                                    existeVinculoProcesso = (configuracaoProcessoCursoNivelEnsino != null && tipoVinculoAlunoProcesso != null);
                                }
                            }
                            if (!existeVinculoProcesso)
                            {
                                throw new ProcessoSeletivoOfertaVinculoConvocacaoException();
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Método que recupera uma lista de convocações, conforme o processo seletivo
        /// </summary>
        /// <param name="seqProcessoSeletivo"></param>
        /// <returns>Lista de convocações do processo seletivo da oferta</returns>
        public List<ConvocacaoVO> BuscarConvocacoesProcessoSeletivo(long seqProcessoSeletivo)
        {
            ProcessoSeletivoOfertaFilterSpecification spec = new ProcessoSeletivoOfertaFilterSpecification();
            spec.SeqProcessoSeletivo = seqProcessoSeletivo;

            var convocacoes = this.SearchProjectionByKey(spec,
                                                       p => p.ProcessoSeletivo.Convocacoes.Select(c => new ConvocacaoVO()
                                                       {
                                                           Seq = c.Seq,
                                                           SeqProcessoSeletivo = c.SeqProcessoSeletivo,
                                                           Descricao = c.Descricao
                                                       }
                                                       ).ToList()
                                                   );
            return convocacoes = convocacoes.SMCAny() ? convocacoes : new List<ConvocacaoVO>();
        }

        #region [ RN_CAM_036 Exclusão de oferta do processo seletivo ]

        /// <summary>
        /// RN_CAM_036 Exclusão de oferta do processo seletivo
        /// 1. Verificar se existe ingressante associado à oferta que está sendo excluída para o processo seletivo em questão. Em caso afirmativo, abortar a operação e emitir a mensagem de erro:
        /// "Exclusão não permitida. Existe ingressante associado à oferta <descrição da oferta da campanha>", para o processo seletivo<descrição do processo seletivo>.
        /// 2. Ao excluir ofertas do processo seletivo, caso elas estejam associadas a alguma convocação do processo, exibir a mensagem de confirmação:
        /// "Deseja excluir a(s) oferta(s) da(s) convocação(ões) deste processo seletivo?"
        /// 2.1. Em caso afirmativo, excluir as ofertas de todas as convocações do processo seletivo em que elas se encontram.
        /// 2.2. Em caso negativo, abortar a operação.
        /// 3. Se houver processo de inscrição do GPI associado ao processo seletivo do SGA, executar 
        /// a regra RN_CAM_076 - Exclusão da oferta do GPI para cada oferta selecionada.
        /// 4. Se o processo estiver configurado para fazer reserva de vagas, para cada oferta do tipo TURMA excluída, remover da 
        /// quantidade de vagas ocupadas da divisão da turma, a quantidade de vagas da oferta excluída.
        /// 5. Excluir a oferta do processo seletivo do SGA.
        /// </summary>
        /// <param name="seqProcessoSeletivoOferta"></param>
        public void Excluir(long seqProcessoSeletivoOferta)
        {
            // Início da transação
            using (var unitOfWork = SMCUnitOfWork.Begin())
            {
                var spec = new ProcessoSeletivoOfertaFilterSpecification() { Seq = seqProcessoSeletivoOferta };

                var ofertaProcessoSeletivo = SearchByKey(spec);

                /* 1. Verificar se existe ingressante associado à oferta que está sendo excluída para o processo seletivo 
                em questão.*/
                ValidarIngressanteOfertaExclusao(ofertaProcessoSeletivo.Seq, ofertaProcessoSeletivo.SeqProcessoSeletivo);

                /*3. Se houver processo de inscrição do GPI associado ao processo seletivo do SGA, executar 
                a regra RN_CAM_077 Exclusão da oferta do GPI para cada oferta selecionada.*/
                ExclusaoOfertaGPI_RN_CAM_077(spec);

                /* 4. Se o processo estiver configurado para fazer reserva de vagas, para cada oferta do tipo TURMA excluída, 
                 * remover da quantidade de vagas ocupadas da divisão da turma, a quantidade de vagas da oferta excluída.*/
                RemoverQuantidadeVagasDivisaoTurma(ofertaProcessoSeletivo, spec);

                /* 5.Excluir a oferta do processo seletivo do SGA.*/
                DeleteEntity(ofertaProcessoSeletivo);

                // Confirmo todas as alterações
                unitOfWork.Commit();
            }
        }

        /// <summary>
        /// RN_CAM_036 Exclusão de oferta do processo seletivo - Item 1
        /// 1. Verificar se existe ingressante associado à oferta que está sendo excluída para o processo seletivo 
        /// em questão. Em caso afirmativo, abortar a operação e emitir a mensagem de erro:
        /// "Exclusão não permitida. Existe ingressante associado à oferta <descrição da oferta da campanha>", 
        /// para o processo seletivo<descrição do processo seletivo>. 
        /// </summary>
        /// <param name="spec"></param>
        private void ValidarIngressanteOfertaExclusao(long seqProcessoSeletivoOferta, long seqProcessoSeletivo)
        {
            var dadosProcessoSeletivoOferta = this.SearchProjectionByKey(new SMCSeqSpecification<ProcessoSeletivoOferta>(seqProcessoSeletivoOferta), p => new
            {
                PossuiIngressantes = p.CampanhaOferta.Ingressantes.Count(i => i.Ingressante.SeqProcessoSeletivo == seqProcessoSeletivo) > 0,
                DescricaoCampanhaOferta = p.CampanhaOferta.Descricao,
                DescricaoProcessoSeletivo = p.ProcessoSeletivo.Descricao
            });

            if (dadosProcessoSeletivoOferta.PossuiIngressantes)
                throw new ProcessoSeletivoOfertaVinculoIngressanteExcluirException(dadosProcessoSeletivoOferta.DescricaoCampanhaOferta, dadosProcessoSeletivoOferta.DescricaoProcessoSeletivo);
        }

        /// <summary>
        /// RN_CAM_036 Exclusão de oferta do processo seletivo - Item 3
        /// 3. Se houver processo de inscrição do GPI associado ao processo seletivo do SGA, executar
        /// a regra RN_CAM_077 Exclusão da oferta do GPI para cada oferta selecionada.
        /// </summary>
        /// <param name="spec"></param>
        private void ExclusaoOfertaGPI_RN_CAM_077(ProcessoSeletivoOfertaFilterSpecification spec)
        {
            var processoInscricaoGPI = SearchProjectionByKey(spec, s => s.ProcessoSeletivo.SeqProcessoGpi);
            if (processoInscricaoGPI.HasValue)
            {
                var seqHierarquiaOfertaGPI = SearchProjectionByKey(spec, s => s.SeqHierarquiaOfertaGpi);

                /*RN_CAM_077 Exclusão da oferta do GPI                
                Excluir a oferta do GPI e seu respectivo item de hierarquia, referenciado na oferta do processo 
                seletivo do SGA, da hierarquia de ofertas do processo do GPI, executando os serviços de exclusão 
                de oferta e de hierarquia da oferta do GPI.
                Caso não reste na hierarquia de ofertas do processo do GPI mais nenhum filho associado ao pai do 
                item excluído, remover o seu pai. Se não restar mais nenhum item associado ao pai do pai do item 
                excluído, remover o pai do seu pai e assim sucessivamente, até excluir a raiz do galho da árvore 
                do item excluído, se for o caso.*/
                if (seqHierarquiaOfertaGPI.HasValue)
                {
                    HierarquiaOfertaService.ExcluirHierarquiaOferta(seqHierarquiaOfertaGPI.Value);
                }
            }
        }

        /// <summary>
        /// RN_CAM_036 Exclusão de oferta do processo seletivo - Item 4
        /// 4. Se o processo estiver configurado para fazer reserva de vagas, para cada oferta do tipo TURMA excluída, 
        /// remover da quantidade de vagas ocupadas da divisão da turma, a quantidade de vagas da oferta excluída.
        /// </summary>
        /// <param name="ofertaProcessoSeletivo"></param>
        /// <param name="spec"></param>
        private void RemoverQuantidadeVagasDivisaoTurma(ProcessoSeletivoOferta ofertaProcessoSeletivo, ProcessoSeletivoOfertaFilterSpecification spec)
        {
            var processoSeletivo = SearchProjectionByKey(spec, s => s.ProcessoSeletivo);

            if (processoSeletivo != null && processoSeletivo.ReservaVaga)
            {
                var divisaoTurma = SearchProjectionByKey(spec, s => s.CampanhaOferta.Itens.Select(x => x.Turma.DivisoesTurma.FirstOrDefault()).FirstOrDefault());

                if (divisaoTurma != null && ofertaProcessoSeletivo != null)
                {
                    //Valido propriedade null
                    divisaoTurma.QuantidadeVagasOcupadas = divisaoTurma.QuantidadeVagasOcupadas.HasValue ? divisaoTurma.QuantidadeVagasOcupadas : 0;

                    //  remover da quantidade de vagas ocupadas da divisão da turma, a quantidade de vagas da oferta excluída.
                    divisaoTurma.QuantidadeVagasOcupadas -= ofertaProcessoSeletivo.QuantidadeVagas;

                    // Validação quantidade negativa
                    divisaoTurma.QuantidadeVagasOcupadas = divisaoTurma.QuantidadeVagasOcupadas < 0 ? 0 : divisaoTurma.QuantidadeVagasOcupadas;

                    // Salvo as alterações
                    DivisaoTurmaDomainService.SaveEntity(divisaoTurma);
                }
            }
        }

        #endregion [ RN_CAM_036 Exclusão de oferta do processo seletivo ]

        #region [ RN_CAM_037 Atualização das vagas das ofertas do processo seletivo ]

        /// <summary>
        /// RN_CAM_037 Atualização das vagas das ofertas do processo seletivo
        /// 1. Para as convocações selecionadas, atualizar a diferença de vagas de cada oferta alterada, na respectiva oferta da convocação, se existir.
        /// 2. Verificar se o novo valor de cada oferta é maior que a quantidade de vagas configurada para esta mesma oferta na campanha.Em caso afirmativo, abortar a operação e emitir a mensagem de erro:
        /// "Alteração não permitida. Existe oferta cuja quantidade de vagas é maior que as vagas configuradas na oferta da campanha."
        /// 3. Se o processo seletivo das ofertas em questão estiver configurado para trabalhar com reserva de vaga:
        /// 3.1. Verificar se para todas as ofertas do tipo TURMA que tiverem sua quantidade de vagas aumentada existe vaga suficiente na respectiva turma.A disponibilidade de vagas na turma deve ser verificada da seguinte forma (novo valor de vaga - valor de vaga anterior) <= (quantidade de vagas da divisão da turma - quantidade de vagas ocupadas). Caso alguma turma não tenha vagas suficientes, abortar a operação e emitir a mensagem de erro:
        /// "Não é permitido aumentar a quantidade de vagas das ofertas listadas abaixo, pois suas turmas não possuem vagas suficientes: <lista de ofertas cujas turmas não possuem vagas suficientes>"
        /// 3.2. Verificar se para todas as ofertas do tipo TURMA que tiverem sua quantidade de vagas diminuída o novo valor não vai ficar menor que a quantidade de vagas ocupadas na própria oferta do processo seletivo.Caso o valor de vagas de alguma oferta fique menor que as vagas ocupadas, abortar a operação e emitir a mensagem de erro:
        /// "Não é permitido diminuir as vagas das ofertas listadas abaixo, pois já estão ocupadas: <lista de ofertas cujo novo valor de vaga ficou menor que a quantidade de vagas ocupadas>
        /// 3.3. Para as ofertas do tipo TURMA, atualizar a diferença do novo valor de vaga para o valor anterior, na quantidade de vagas ocupadas da divisão da turma de cada oferta alterada.      
        /// </summary>
        public void AtualizarVagasOfertasProcessoSeletivo(VagasProcessoSeletivoOfertaVO ofertaLista)
        {
            // Início da transação
            using (var unitOfWork = SMCUnitOfWork.Begin())
            {
                // Efetiva a Atualização das vagas, alteradas das ofertas do Processo seletivo
                PersistirVagasOfertaProcessoSeletivo(ofertaLista);

                /* 1. Para as convocações selecionadas, atualizar a diferença de vagas de cada oferta alterada, 
                 * na respectiva oferta da convocação, se existir.*/
                PersistirDiferencaVagasOfertaConvocacao(ofertaLista);

                /* 2. Verificar se o novo valor de cada oferta é maior que a quantidade de 
                 * vagas configurada para esta mesma oferta na campanha.*/
                ValidarVagasOfertaCampanha(ofertaLista.ProcessoSeletivoOfertas);

                /*3. Se o processo seletivo das ofertas em questão estiver configurado para trabalhar com reserva de vaga:*/
                ValidarProcessoSeletivoReservaVaga(ofertaLista.SeqProcessoSeletivo, ofertaLista.ProcessoSeletivoOfertas);

                /// RN_CAM_041 - Consistência de associação de oferta da convocação
                /// RN_CAM_042 - Associação de oferta à convocação
                Validacoes_RN_CAM_041_E_RN_CAM_042(ofertaLista);

                unitOfWork.Commit();
            }
        }

        /// RN_CAM_041 - Consistência de associação de oferta da convocação
        /// RN_CAM_042 - Associação de oferta à convocação
        private void Validacoes_RN_CAM_041_E_RN_CAM_042(VagasProcessoSeletivoOfertaVO ofertaLista)
        {
            var processoSeletivoOfertaVO = new ProcessoSeletivoOfertaVO()
            {
                Convocacoes = ofertaLista.SeqsConvocacoes?.ToList(),
                Ofertas = ofertaLista.ProcessoSeletivoOfertas.Select(o => o.SeqCampanhaOferta).ToList(),
                SeqProcessoSeletivo = ofertaLista.SeqProcessoSeletivo
            };

            ConsistenciaEAssociacaoOfertaConvocacao_RN_CAM_041_E_RN_CAM_042(processoSeletivoOfertaVO);
        }

        /// <summary>
        /// Método que aplica as regras RN_CAM_041 - Consistência de associação de oferta da convocação
        /// e RN_CAM_042 - Associação de oferta à convocação;
        /// </summary>
        /// <param name="processoSeletivoOfertaVO"></param>
        private void ConsistenciaEAssociacaoOfertaConvocacao_RN_CAM_041_E_RN_CAM_042(ProcessoSeletivoOfertaVO processoSeletivoOfertaVO)
        {
            var includeProcessoSeletivo = IncludesProcessoSeletivo.Ofertas | IncludesProcessoSeletivo.ProcessosMatricula | IncludesProcessoSeletivo.TipoProcessoSeletivo;

            var processoSeletivo = ProcessoSeletivoDomainService.SearchByKey(processoSeletivoOfertaVO.SeqProcessoSeletivo, includeProcessoSeletivo);

            // 4. Validar Associação da(s) convocação(ões)
            if (processoSeletivoOfertaVO.Convocacoes != null && processoSeletivoOfertaVO.Convocacoes.Any())
            {
                // RN_CAM_041 - Consistência de associação de oferta da convocação
                ValidarConsistenciaAssociacaoOfertaConvocacao(processoSeletivoOfertaVO, processoSeletivo);

                //RN_CAM_042 - Associação de oferta à convocação
                PersistirOfertaNaConvocacao_RN_CAM_042(processoSeletivoOfertaVO, processoSeletivo);
            }
        }

        /// <summary>
        /// RN_CAM_042 - Associação de oferta à convocação
        /// Para cada oferta selecionada, criar um registro de oferta associada à convocação, com a referência 
        /// da oferta do processo seletivo e a quantidade de vagas "0".
        /// </summary>
        /// <param name="processoSeletivoOfertaVO"></param>
        /// <param name="processoSeletivo"></param>
        private void PersistirOfertaNaConvocacao_RN_CAM_042(ProcessoSeletivoOfertaVO processoSeletivoOfertaVO, ProcessoSeletivo processoSeletivo)
        {
            var include = IncludesConvocacao.Ofertas | IncludesConvocacao.CampanhaCicloLetivo;

            // Percorro as convocações
            foreach (var seqConvocacao in processoSeletivoOfertaVO.Convocacoes)
            {
                // recupero a convocação e suas ofertas
                var convocacao = ConvocacaoDomainService.SearchByKey(seqConvocacao, include);
                convocacao.Ofertas = convocacao.Ofertas ?? new List<ConvocacaoOferta>();

                // Percorro as ofertas, para vinculá-las a oferta da convocação
                foreach (var seqCampanhaOferta in processoSeletivoOfertaVO.Ofertas)
                {
                    // Criação da Oferta da convocação
                    convocacao.Ofertas.Add(new ConvocacaoOferta()
                    {
                        QuantidadeVagas = 0,
                        // busco a oferta do processo seletivo, selecionada pelo usuário, e vinculo ela a oferta da convocação
                        SeqProcessoSeletivoOferta = processoSeletivo.Ofertas.FirstOrDefault(p => p.SeqCampanhaOferta == seqCampanhaOferta).Seq,
                        SeqConvocacao = seqConvocacao
                    });
                }
                ConvocacaoDomainService.SaveEntity(convocacao);
            }
        }

        /// <summary>
        /// Efetiva a Atualização das vagas, alteradas das ofertas do Processo seletivo
        /// </summary>
        /// <param name="ofertaLista"></param>
        private void PersistirVagasOfertaProcessoSeletivo(VagasProcessoSeletivoOfertaVO ofertaLista)
        {
            var spec = new ProcessoSeletivoOfertaFilterSpecification()
            {
                Seqs = ofertaLista.ProcessoSeletivoOfertas.Select(p => p.Seq).ToArray(),
                SeqProcessoSeletivo = ofertaLista.SeqProcessoSeletivo
            };

            // Busco as ofertas da base, para alteração
            var ofertas = SearchBySpecification(spec).ToList();

            foreach (var oferta in ofertas)
            {
                // Busco a oferta Alterada pelo usuário
                var ofertaAlterada = ofertaLista.ProcessoSeletivoOfertas.FirstOrDefault(x => oferta.Seq == x.Seq);

                if (ofertaAlterada != null)
                {
                    // RN_CAM_018 - Atualização das vagas das ofertas da campanha
                    // verificar se a diferença de vagas é maior que a quantidade de vagas da respectiva oferta do processo seletivo
                    CampanhaOfertaDomainService.ValidarReducaoVagasOfertaProcessoSeletivo(oferta, ofertaAlterada.VagasDiferenca);

                    //Atualização da quantidade de vagas
                    oferta.QuantidadeVagas += (short)ofertaAlterada.VagasDiferenca;

                    SaveEntity(oferta);
                }
            }
        }

        /// <summary>
        /// RN_CAM_037 Atualização das vagas das ofertas do processo seletivo
        /// 3. Se o processo seletivo das ofertas em questão estiver configurado para trabalhar com reserva de vaga:
        /// 3.1. Verificar se para todas as ofertas do tipo TURMA que tiverem sua quantidade de vagas aumentada existe vaga suficiente na respectiva turma.A disponibilidade de vagas na turma deve ser verificada da seguinte forma (novo valor de vaga - valor de vaga anterior) <= (quantidade de vagas da divisão da turma - quantidade de vagas ocupadas). Caso alguma turma não tenha vagas suficientes, abortar a operação e emitir a mensagem de erro:
        /// "Não é permitido aumentar a quantidade de vagas das ofertas listadas abaixo, pois suas turmas não possuem vagas suficientes: <lista de ofertas cujas turmas não possuem vagas suficientes>"
        /// 3.2. Verificar se para todas as ofertas do tipo TURMA que tiverem sua quantidade de vagas diminuída o novo valor não vai ficar menor que a quantidade de vagas ocupadas na própria oferta do processo seletivo.Caso o valor de vagas de alguma oferta fique menor que as vagas ocupadas, abortar a operação e emitir a mensagem de erro:
        /// "Não é permitido diminuir as vagas das ofertas listadas abaixo, pois já estão ocupadas: <lista de ofertas cujo novo valor de vaga ficou menor que a quantidade de vagas ocupadas>
        /// 3.3. Para as ofertas do tipo TURMA, atualizar a diferença do novo valor de vaga para o valor anterior, na quantidade de vagas ocupadas da divisão da turma de cada oferta alterada.      
        /// </summary>
        /// <param name="seqProcessoSeletivo"></param>
        /// <param name="processoSeletivoOfertas"></param>
        private void ValidarProcessoSeletivoReservaVaga(long seqProcessoSeletivo, List<ProcessoSeletivoOfertaListaVO> processoSeletivoOfertas)
        {
            var spec = new ProcessoSeletivoFilterSpecification() { Seq = seqProcessoSeletivo };

            var ofertasProcessoSeletivoTurma = processoSeletivoOfertas.Where(t => t.TipoOfertaToken == TOKEN_TIPO_OFERTA.TURMA);

            var reservaVaga = ProcessoSeletivoDomainService.SearchProjectionByKey(spec, x => x.ReservaVaga);

            if (!reservaVaga || !ofertasProcessoSeletivoTurma.SMCAny()) { return; }

            var ofertasProcessoSeletivoBase = ProcessoSeletivoDomainService.SearchProjectionByKey(spec, x => x.Ofertas);

            var seqsProcessoSeletivoOfertasAlteradas = processoSeletivoOfertas.Select(x => x.Seq).ToArray();


            List<CampanhaOferta> OfertasTurma = new List<CampanhaOferta>();

            /*3. Se o processo seletivo das ofertas em questão estiver configurado para trabalhar com reserva de vaga:*/
            List<ProcessoSeletivoOfertaListaVO> OfertasTurmaVagasInsuficientes = new List<ProcessoSeletivoOfertaListaVO>();
            List<ProcessoSeletivoOfertaListaVO> OfertasTurmaVagasOcupadasInsuficientes = new List<ProcessoSeletivoOfertaListaVO>();

            foreach (var ofertaTurma in ofertasProcessoSeletivoTurma)
            {
                // Buscar a divisão da turma
                var specOferta = new ProcessoSeletivoOfertaFilterSpecification() { Seq = ofertaTurma.Seq };
                var divisaoTurma = SearchProjectionByKey(specOferta, x => x.CampanhaOferta.Itens.Select(s => s.Turma.DivisoesTurma.FirstOrDefault())).FirstOrDefault();

                /*3.1. Verificar se para todas as ofertas do tipo TURMA que tiverem sua quantidade de 
                 * vagas aumentada existe vaga suficiente na respectiva turma. */
                //Oferta com vaga aumentada
                if (ofertaTurma.VagasDiferenca > 0)
                {
                    bool possuiVagas = false;

                    if (divisaoTurma != null)
                    {
                        int divisaoTurmaVagas = divisaoTurma.QuantidadeVagas
                            , divisaoTurmaVagasOcupadas = (int)(divisaoTurma.QuantidadeVagasOcupadas ?? 0);

                        // A disponibilidade de vagas na turma deve ser verificada da seguinte forma:
                        // (novo valor de vaga - valor de vaga anterior) <= (quantidade de vagas da divisão da turma - quantidade de vagas ocupadas).
                        possuiVagas = (ofertaTurma.Vagas - ofertaTurma.VagasBase) <= (divisaoTurmaVagas - divisaoTurmaVagasOcupadas);
                    }

                    if (!possuiVagas)
                    {
                        /*Caso alguma turma não tenha vagas suficientes, abortar a operação e emitir a mensagem de erro:
                         * "Não é permitido aumentar a quantidade de vagas das ofertas listadas abaixo, pois suas turmas não possuem vagas suficientes:
                         * <lista de ofertas cujas turmas não possuem vagas suficientes>" */
                        OfertasTurmaVagasInsuficientes.Add(ofertaTurma);
                    }
                }
                //Oferta com quantidade de vagas diminuída
                else if (ofertaTurma.VagasDiferenca < 0)
                {
                    /*3.2. Verificar se para todas as ofertas do tipo TURMA que tiverem sua quantidade de vagas diminuída 
                     * o novo valor não vai ficar menor que a quantidade de vagas ocupadas na própria oferta do processo 
                     * seletivo.*/
                    bool qtdVagasInsuficiente = ofertaTurma.Vagas < ofertaTurma.Ocupadas;

                    /* Caso o valor de vagas de alguma oferta fique menor que as vagas ocupadas, abortar
                    * a operação e emitir a mensagem de erro:
                    "Não é permitido diminuir as vagas das ofertas listadas abaixo, pois já estão ocupadas:
                    <lista de ofertas cujo novo valor de vaga ficou menor que a quantidade de vagas ocupadas>*/
                    if (qtdVagasInsuficiente)
                    {
                        OfertasTurmaVagasOcupadasInsuficientes.Add(ofertaTurma);
                    }
                }

                /*3.3. Para as ofertas do tipo TURMA, atualizar a diferença do novo valor de vaga para o valor anterior, 
                 * na quantidade de vagas ocupadas da divisão da turma de cada oferta alterada.*/
                if (divisaoTurma != null)
                {
                    divisaoTurma.QuantidadeVagasOcupadas += (short)ofertaTurma.VagasDiferenca;

                    DivisaoTurmaDomainService.SaveEntity(divisaoTurma);
                }
            }

            /* Caso alguma turma não tenha vagas suficientes, abortar a operação e emitir a mensagem de erro:
             *
             * "Não é permitido aumentar a quantidade de vagas das ofertas listadas abaixo, pois suas turmas
             *  não possuem vagas suficientes: <lista de ofertas cujas turmas não possuem vagas suficientes>"*/
            if (OfertasTurmaVagasInsuficientes.Count() > 0)
            {
                var ofertas = string.Join(", ", OfertasTurmaVagasInsuficientes.Select(o => o.Oferta).ToArray());
                throw new OfertaVagaTurmaInsuficienteException(ofertas);
            }

            /* Caso o valor de vagas de alguma oferta fique menor que as vagas ocupadas, abortar a operação e
             * emitir a mensagem de erro:
              "Não é permitido diminuir as vagas das ofertas listadas abaixo, pois já estão ocupadas:
               <lista de ofertas cujo novo valor de vaga ficou menor que a quantidade de vagas ocupadas>*/
            if (OfertasTurmaVagasOcupadasInsuficientes.Count() > 0)
            {
                var ofertas = string.Join(", ", OfertasTurmaVagasOcupadasInsuficientes.Select(o => o.Oferta).ToArray());
                throw new OfertaVagaTurmaOcupadaInsuficienteException(ofertas);
            }
        }

        /// <summary>
        /// RN_CAM_037 Atualização das vagas das ofertas do processo seletivo
        /// 2. Verificar se o novo valor de cada oferta é maior que a quantidade de vagas configurada para esta mesma oferta na campanha.
        /// </summary>
        /// <param name="processoSeletivoOfertas"></param>
        private void ValidarVagasOfertaCampanha(List<ProcessoSeletivoOfertaListaVO> processoSeletivoOfertas)
        {
            if (!processoSeletivoOfertas.SMCAny()) { return; }

            var spec = new CampanhaOfertaFilterSpecification() { Seqs = processoSeletivoOfertas.Select(x => x.SeqCampanhaOferta).ToArray() };

            // Busco as ofertas da campanha relacionadas com as ofertas do processo seletivo
            var CampanhaOfertas = CampanhaOfertaDomainService.SearchBySpecification(spec).ToList();

            if (!CampanhaOfertas.SMCAny()) { return; }

            foreach (var ofertaProcessoSeletivo in processoSeletivoOfertas)
            {
                // Faço a validação de cada oferta
                var ofertaCampanhaVagaMenor = CampanhaOfertas.FirstOrDefault(ofertaCampanha => ofertaCampanha.Seq == ofertaProcessoSeletivo.SeqCampanhaOferta
                                                                            && ofertaProcessoSeletivo.Vagas > ofertaCampanha.QuantidadeVagas);

                /*Em caso afirmativo, abortar a operação e emitir a mensagem de erro:
                 * "Alteração não permitida. Existe oferta cuja quantidade de vagas é maior que as vagas configuradas na oferta da campanha."*/
                if (ofertaCampanhaVagaMenor != null) { throw new OfertaVagaMaiorProcessoSeletivoException(); }
            }
        }

        /// <summary>
        /// RN_CAM_037 Atualização das vagas das ofertas do processo seletivo
        /// 1. Para as convocações selecionadas, atualizar a diferença de vagas de cada oferta alterada, 
        /// na respectiva oferta da convocação, se existir
        /// </summary>
        /// <param name="ofertaLista"></param>
        private void PersistirDiferencaVagasOfertaConvocacao(VagasProcessoSeletivoOfertaVO ofertaLista)
        {
            if (!ofertaLista.SeqsConvocacoes.SMCAny()) { return; }

            var spec = new ConvocacaoOfertaFilterSpecification()
            {
                SeqsConvocacao = ofertaLista.SeqsConvocacoes,
                SeqProcessoSeletivo = ofertaLista.SeqProcessoSeletivo,
                SeqsProcessoSeletivoOferta = ofertaLista.ProcessoSeletivoOfertas.Select(o => o.Seq).ToArray()

            };

            // Busco as ofertas das convocações, selecionadas pelo usuário e que são vinculadas as ofertas do processo seletivo.
            var convocacoesOfertas = ConvocacaoOfertaDomainService.SearchBySpecification(spec).ToList();

            // Percorro as ofertas das convocações.
            foreach (var ofertaConvocacao in convocacoesOfertas)
            {
                // Busco a oferta do processo seletivo, correspondente a oferta da convocação
                var ofertaProcessoSeletivoAlterada = ofertaLista.ProcessoSeletivoOfertas.FirstOrDefault(x => x.Seq == ofertaConvocacao.SeqProcessoSeletivoOferta);

                // RN_CAM_037 - Atualização das vagas das ofertas do processo seletivo
                // 1.1. Caso esteja diminuindo a quantidade de vagas, verificar se a diferença de vagas é maior que a quantidade de vagas da respectiva oferta da convocação.
                CampanhaOfertaDomainService.ValidarReducaoVagasOfertaConvocacao(ofertaConvocacao, ofertaProcessoSeletivoAlterada.VagasDiferenca);

                // Faço a atualização da diferença de vagas
                ofertaConvocacao.QuantidadeVagas += (short)ofertaProcessoSeletivoAlterada.VagasDiferenca;

                ConvocacaoOfertaDomainService.SaveEntity(ofertaConvocacao);
            }
        }

        #endregion [ RN_CAM_037 Atualização das vagas das ofertas do processo seletivo ]

        #region [ RN_CAM_068 - Copiar vagas da campanha ]

        /// <summary>
        /// RN_CAM_068 - Copiar vagas da campanha
        /// 1. Exibir a mensagem de confirmação:
        /// "Deseja copiar para as ofertas abaixo a mesma quantidade de vagas cadastrada na campanha?
        /// <Lista tabelada das ofertas selecionadas, com a respectiva vaga na campanha> "
        /// 1.1. Caso o usuário informe "Sim", copiar para cada oferta selecionada do processo seletivo, a quantidade total de vagas da respectiva oferta na campanha, 
        /// executando a regra RN_CAM_037 - Atualização das vagas das ofertas do processo seletivo.
        /// 1.2 Caso o usuário informe "Não", abortar a operação.
        /// </summary>
        /// <param name="ofertaLista"></param>
        public void CopiarVagasCampanha_RN_CAM_068(VagasProcessoSeletivoOfertaVO ofertaLista)
        {
            var spec = new CampanhaOfertaFilterSpecification() { Seqs = ofertaLista.ProcessoSeletivoOfertas.Select(x => x.SeqCampanhaOferta).ToArray() };

            // Busco as Respectivas ofertas da campanha
            var ofertasCampanha = CampanhaOfertaDomainService.SearchBySpecification(spec);

            foreach (var ofertaProcessoSeletivo in ofertaLista.ProcessoSeletivoOfertas)
            {
                var ofertaCampanha = ofertasCampanha.FirstOrDefault(x => x.Seq == ofertaProcessoSeletivo.SeqCampanhaOferta);

                // Copiar para cada oferta selecionada do processo seletivo, a quantidade total de vagas da respectiva oferta na campanha
                ofertaProcessoSeletivo.Vagas = ofertaCampanha.QuantidadeVagas;

                // Atualizo a diferença de vagas, para executar a regra RN_CAM_037
                ofertaProcessoSeletivo.VagasDiferenca = ofertaProcessoSeletivo.Vagas - ofertaProcessoSeletivo.VagasBase.Value;
            }
            // Executar a regra RN_CAM_037 - Atualização das vagas das ofertas do processo seletivo.
            AtualizarVagasOfertasProcessoSeletivo(ofertaLista);
        }

        #endregion [ RN_CAM_068 - Copiar vagas da campanha ]

        #region [ RN_CAM_069 - Usar vagas disponíveis na campanha ]

        /// <summary>
        /// RN_CAM_069 - Usar vagas disponíveis na campanha
        /// 1. Exibir a mensagem de confirmação:
        /// "Deseja gravar nas ofertas abaixo a quantidade de vagas disponíveis na campanha?
        /// <Lista tabelada das ofertas selecionadas, com a respectiva quantidade de vagas disponíveis na campanha>"
        /// 1.1. Caso o usuário informe "Sim", gravar em cada oferta selecionada no processo seletivo, 
        /// a quantidade total de vagas de sua respectiva oferta na campanha, subtraída da quantidade de ingressantes nesta 
        /// oferta para o processo seletivo em questão, cuja situação é diferente de "Desistente" e "Cancelado (Prouni)", 
        /// executando a regra RN_CAM_037 - Atualização das vagas das ofertas do processo seletivo.
        /// 1.2 Caso o usuário informe "Não", abortar a operação.
        /// </summary>
        /// <param name="ofertaLista"></param>
        public void UsarVagasDisponiveis_RN_CAM_069(VagasProcessoSeletivoOfertaVO ofertaLista)
        {
            var filtro = new CampanhaOfertaFiltroTelaVO() { SeqsCampanhaOfertas = ofertaLista.ProcessoSeletivoOfertas.Select(x => x.SeqCampanhaOferta).ToArray() };

            // Busco as Respectivas ofertas da campanha, Aplicando a regra RN_CAM_069 - Usar vagas disponíveis na campanha
            var ofertasCampanha = CampanhaOfertaDomainService.BuscarCampanhaOfertas(filtro);

            foreach (var ofertaProcessoSeletivo in ofertaLista.ProcessoSeletivoOfertas)
            {
                var ofertaCampanha = ofertasCampanha.FirstOrDefault(x => x.Seq == ofertaProcessoSeletivo.SeqCampanhaOferta);

                // quantidade total de vagas de sua respectiva oferta na campanha, subtraída da quantidade de ingressantes nesta 
                // oferta para o processo seletivo em questão, cuja situação é diferente de "Desistente" e "Cancelado (Prouni)
                ofertaProcessoSeletivo.Vagas = ofertaCampanha.Disponiveis;

                // Atualizo a diferença de vagas, para executar a regra RN_CAM_037
                ofertaProcessoSeletivo.VagasDiferenca = ofertaProcessoSeletivo.Vagas - ofertaProcessoSeletivo.VagasBase.Value;
            }
            // Executar a regra RN_CAM_037 - Atualização das vagas das ofertas do processo seletivo.
            AtualizarVagasOfertasProcessoSeletivo(ofertaLista);
        }

        #endregion [ RN_CAM_069 - Usar vagas disponíveis na campanha ]

        #region [ Novo Processo Seletivo ]

        public ProcessoSeletivoVO NovoProcessosSeletivo(long seqCampanha)
        {
            //var campanha = CampanhaDomainService.SearchByKey(seqCampanha);

            return new ProcessoSeletivoVO() { SeqCampanha = seqCampanha, };
        }

        #endregion [ Novo Processo Seletivo ]

    }
}