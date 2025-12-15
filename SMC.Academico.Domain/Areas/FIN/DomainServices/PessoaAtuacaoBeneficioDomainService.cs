using SMC.Academico.Common.Areas.ALN.Enums;
using SMC.Academico.Common.Areas.ALN.Includes;
using SMC.Academico.Common.Areas.FIN.Enums;
using SMC.Academico.Common.Areas.FIN.Exceptions;
using SMC.Academico.Common.Areas.FIN.Includes;
using SMC.Academico.Common.Areas.PES.Constants;
using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Academico.Common.Areas.PES.Includes;
using SMC.Academico.Common.Areas.SRC.Constants;
using SMC.Academico.Common.Constants;
using SMC.Academico.Domain.Areas.ALN.DomainServices;
using SMC.Academico.Domain.Areas.ALN.Models;
using SMC.Academico.Domain.Areas.ALN.Specifications;
using SMC.Academico.Domain.Areas.ALN.ValueObjects;
using SMC.Academico.Domain.Areas.CAM.DomainServices;
using SMC.Academico.Domain.Areas.CAM.ValueObjects;
using SMC.Academico.Domain.Areas.FIN.Models;
using SMC.Academico.Domain.Areas.FIN.Specifications;
using SMC.Academico.Domain.Areas.FIN.ValueObjects;
using SMC.Academico.Domain.Areas.MAT.DomainServices;
using SMC.Academico.Domain.Areas.MAT.Specifications;
using SMC.Academico.Domain.Areas.MAT.ValueObjects;
using SMC.Academico.Domain.Areas.ORG.DomainServices;
using SMC.Academico.Domain.Areas.PES.DomainServices;
using SMC.Academico.Domain.Areas.PES.Models;
using SMC.Academico.Domain.Areas.PES.Specifications;
using SMC.Academico.Domain.Areas.PES.ValueObjects;
using SMC.Academico.Domain.Areas.SRC.DomainServices;
using SMC.Academico.Domain.Areas.SRC.ValueObjects;
using SMC.Academico.Domain.DomainServices;
using SMC.Academico.Domain.Models;
using SMC.Academico.Domain.Specifications;
using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.Financeiro.Service.FIN;
using SMC.Financeiro.ServiceContract.Areas.GRA.Data;
using SMC.Financeiro.ServiceContract.BLT;
using SMC.Framework;
using SMC.Framework.Domain;
using SMC.Framework.Exceptions;
using SMC.Framework.Extensions;
using SMC.Framework.Jobs;
using SMC.Framework.Model;
using SMC.Framework.Repository;
using SMC.Framework.Security;
using SMC.Framework.Specification;
using SMC.Framework.UnitOfWork;
using SMC.Framework.Util;
using SMC.Notificacoes.Common.Areas.NTF.Enums;
using SMC.Notificacoes.ServiceContract.Areas.NTF.Data;
using SMC.Notificacoes.ServiceContract.Areas.NTF.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Areas.FIN.DomainServices
{
    public class PessoaAtuacaoBeneficioDomainService : AcademicoContextDomain<PessoaAtuacaoBeneficio>
    {
        #region [ Querys ]

        private const string QUERY_RELATORIO_ALUNO_BOLSISTA =
                @"select
                    pab.seq_pessoa_atuacao_beneficio as SeqPessoaAtuacaoBeneficio,
                    er.sgl_entidade as SiglaEntidadeResponsavel,
                    case
                        when dp.nom_social is not null then rtrim(dp.nom_social) + ' (' + rtrim(dp.nom_pessoa) + ')'
                        else rtrim(dp.nom_pessoa)
                    end as Nome,
                    dta.val_dominio as SeqTipoAtuacao,
                    dta.dsc_dominio as TipoAtuacao,
                    pa.seq_pessoa_atuacao as SeqPessoaAtuacao,
                    al.cod_aluno_migracao as CodigoAlunoMigracao,
                    b.seq_beneficio as SeqBeneficio,
                    b.seq_beneficio_financeiro as SeqBeneficioFinanceiro,
                    case
                        when cb.idt_dom_forma_deducao = 1 -- Percentual de bolsa
                        then rtrim(b.dsc_beneficio) + ' - ' + cast(pab.val_beneficio as varchar) + '%'
                        else b.dsc_beneficio
                    end as DescricaoBeneficio,
                    hv.dat_inicio_vigencia as DataInicioVigencia,
                    hv.dat_fim_vigencia as DataFimVigencia,
                    ds.dsc_dominio as SituacaoChancelaBeneficio,
                    ne.dsc_nivel_ensino as DescricaoNivelEnsino,
                    p.num_cpf as CPF
                from	fin.pessoa_atuacao_beneficio pab
                join	pes.pessoa_atuacao pa
                        on pab.seq_pessoa_atuacao = pa.seq_pessoa_atuacao
                join	pes.pessoa p
                        on pa.seq_pessoa = p.seq_pessoa
                join	pes.pessoa_dados_pessoais dp
                        on pa.seq_pessoa_dados_pessoais = dp.seq_pessoa_dados_pessoais
                join	dominio dta
                        on pa.idt_dom_tipo_atuacao = dta.val_dominio
                        and dta.nom_dominio = 'tipo_atuacao'
                left join	aln.aluno al
                            on pa.seq_pessoa_atuacao = al.seq_pessoa_atuacao
                left join	aln.aluno_historico ah
                            on al.seq_pessoa_atuacao = ah.seq_atuacao_aluno
                left join	aln.ingressante i
                            on pa.seq_pessoa_atuacao = i.seq_pessoa_atuacao
                full outer join   cam.campanha_ciclo_letivo ccl
							on i.seq_campanha_ciclo_letivo = ccl.seq_campanha_ciclo_letivo
                join	ORG.nivel_ensino ne
                        on ne.seq_nivel_ensino = ah.seq_nivel_ensino or ne.seq_nivel_ensino = i.seq_nivel_ensino
                join	org.entidade er
                        on ah.seq_entidade_vinculo = er.seq_entidade
                        or i.seq_entidade_responsavel = er.seq_entidade
                join	fin.beneficio b
                        on pab.seq_beneficio = b.seq_beneficio
                left join	fin.configuracao_beneficio cb
                            on pab.seq_configuracao_beneficio = cb.seq_configuracao_beneficio
                join	fin.beneficio_historico_situacao bh
                        on pab.seq_pessoa_atuacao_beneficio = bh.seq_pessoa_atuacao_beneficio
		                and bh.ind_atual = 1
                join	fin.beneficio_historico_vigencia hv
		                on pab.seq_pessoa_atuacao_beneficio = hv.seq_pessoa_atuacao_beneficio
		                and hv.ind_atual = 1
                join	dominio ds
                        on bh.idt_dom_situacao_chancela_beneficio = ds.val_dominio
                        and ds.nom_dominio = 'situacao_chancela_beneficio'
                where	p.seq_entidade_instituicao = @SEQ_ENTIDADE_INSTITUICAO
                and	(	@DAT_INICIO between hv.dat_inicio_vigencia and hv.dat_fim_vigencia
                or		@DAT_FIM between hv.dat_inicio_vigencia and hv.dat_fim_vigencia
                or		hv.dat_inicio_vigencia between @DAT_INICIO and @DAT_FIM
                or		hv.dat_fim_vigencia between @DAT_INICIO and @DAT_FIM
                or      @DAT_INICIO is null
                or      @DAT_FIM is null )
                and		pa.idt_dom_tipo_atuacao in (select cast(item as bigint) from DPD.dbo.fn_converte_lista_em_tabela(@LISTA_TIPO_ATUACAO, ','))
                and (	@LISTA_ENTIDADE_RESPONSAVEL is null or er.seq_entidade in (select CAST(item as bigint) from DPD.dbo.fn_converte_lista_em_tabela(@LISTA_ENTIDADE_RESPONSAVEL, ',')))
                and	(	@LISTA_BENEFICIO is null or b.seq_beneficio in (select CAST(item as bigint) from DPD.dbo.fn_converte_lista_em_tabela(@LISTA_BENEFICIO, ',')))
                and (	@SITUACAO_BENEFICIO = 0 or bh.idt_dom_situacao_chancela_beneficio = @SITUACAO_BENEFICIO)   
                and (	@SEQ_NIVEL_ENSINO = 0 or ne.seq_nivel_ensino = @SEQ_NIVEL_ENSINO)  
                and ((@SEQ_CICLO_LETIVO_INGRESSO = 0 or (ah.ind_atual = 1 and ah.seq_ciclo_letivo = @SEQ_CICLO_LETIVO_INGRESSO)) or (@SEQ_CICLO_LETIVO_INGRESSO = 0 or (ccl.seq_ciclo_letivo = @SEQ_CICLO_LETIVO_INGRESSO)))          
                order by
                    isnull(dp.nom_social, dp.nom_pessoa),
                    er.sgl_entidade";

        #endregion [ Querys ]

        #region Propriedade

        private const string INCLUSAO = "Inclusão";
        private const string ALTERAR = "Alteração";
        private const string ASSOCIACAO = "Associação";

        #endregion Propriedade

        #region [ DomainServices ]

        private ConfiguracaoEventoLetivoDomainService ConfiguracaoEventoLetivoDomainService => Create<ConfiguracaoEventoLetivoDomainService>();

        private CicloLetivoDomainService CicloLetivoDomainService => Create<CicloLetivoDomainService>();

        private PessoaAtuacaoDomainService PessoaAtuacaoDomainService => Create<PessoaAtuacaoDomainService>();

        private ConfiguracaoBeneficioDomainService ConfiguracaoBeneficioDomainService => Create<ConfiguracaoBeneficioDomainService>();

        private BeneficioDomainService BeneficioDomainService => Create<BeneficioDomainService>();

        private IngressanteDomainService IngressanteDomainService => Create<IngressanteDomainService>();

        private InstituicaoNivelBeneficioDomainService InstituicaoNivelBeneficioDomainService => Create<InstituicaoNivelBeneficioDomainService>();

        private BeneficioHistoticoValorAuxilioDomainService BeneficioHistoticoValorAuxilioDomainService => Create<BeneficioHistoticoValorAuxilioDomainService>();

        private PessoaAtuacaoBloqueioDomainService PessoaAtuacaoBloqueioDomainService => Create<PessoaAtuacaoBloqueioDomainService>();

        private AlunoDomainService AlunoDomainService => Create<AlunoDomainService>();

        private PessoaDomainService PessoaDomainService => Create<PessoaDomainService>();

        private BeneficioControleFinanceiroDomainService BeneficioControleFinanceiroDomainService => Create<BeneficioControleFinanceiroDomainService>();

        private EventoLetivoDomainService EventoLetivoDomainService => Create<EventoLetivoDomainService>();

        private SolicitacaoMatriculaDomainService SolicitacaoMatriculaDomainService => Create<SolicitacaoMatriculaDomainService>();

        private InstituicaoEnsinoDomainService InstituicaoEnsinoDomainService => Create<InstituicaoEnsinoDomainService>();

        private PlanoEstudoItemDomainService PlanoEstudoItemDomainService => Create<PlanoEstudoItemDomainService>();

        private AlunoHistoricoDomainService AlunoHistoricoDomainService => Create<AlunoHistoricoDomainService>();

        private AlunoHistoricoSituacaoDomainService AlunoHistoricoSituacaoDomainService => Create<AlunoHistoricoSituacaoDomainService>();

        private ContratoDomainService ContratoDomainService => Create<ContratoDomainService>();

        private InstituicaoNivelTipoVinculoAlunoDomainService InstituicaoNivelTipoVinculoAlunoDomainService => Create<InstituicaoNivelTipoVinculoAlunoDomainService>();

        private BeneficioHistoricoSituacaoDomainService BeneficioHistoricoSituacaoDomainService => Create<BeneficioHistoricoSituacaoDomainService>();

        private SolicitacaoServicoEnvioNotificacaoDomainService SolicitacaoServicoEnvioNotificacaoDomainService => Create<SolicitacaoServicoEnvioNotificacaoDomainService>();

        private BeneficioHistoricoVigenciaDomainService BeneficioHistoricoVigenciaDomainService => Create<BeneficioHistoricoVigenciaDomainService>();

        private MotivoAlteracaoBeneficioDomainService MotivoAlteracaoBeneficioDomainService => Create<MotivoAlteracaoBeneficioDomainService>();

        private EntidadeConfiguracaoNotificacaoDomainService EntidadeConfiguracaoNotificacaoDomainService => Create<EntidadeConfiguracaoNotificacaoDomainService>();

        private BeneficioEnvioNotificacaoDomainService BeneficioEnvioNotificacaoDomainService => Create<BeneficioEnvioNotificacaoDomainService>();

        private ArquivoAnexadoDomainService ArquivoAnexadoDomainService => Create<ArquivoAnexadoDomainService>();
        private TipoBeneficioDomainService TipoBeneficioDomainService => Create<TipoBeneficioDomainService>();

        #endregion [ DomainServices ]

        #region [ Services ]

        private IIntegracaoFinanceiroService IntegracaoFinanceiroService => Create<IIntegracaoFinanceiroService>();
        private INotificacaoService NotificacaoService => Create<INotificacaoService>();
        private IFinanceiroService FinanceiroService => Create<IFinanceiroService>();

        #endregion [ Services ]

        public SMCPagerData<PessoaAtuacaoBeneficioVO> BuscarPesssoasAtuacoesBeneficios(PessoaAtuacaoBeneficioFilterSpecification filtro)
        {
            int total = 0;
            var includes = IncludesPessoaAtuacaoBeneficio.PessoaAtuacao
                         | IncludesPessoaAtuacaoBeneficio.PessoaAtuacao_DadosPessoais
                         | IncludesPessoaAtuacaoBeneficio.PessoaAtuacao_Pessoa
                         | IncludesPessoaAtuacaoBeneficio.Beneficio
                         | IncludesPessoaAtuacaoBeneficio.ConfiguracaoBeneficio
                         | IncludesPessoaAtuacaoBeneficio.Beneficio_TipoBeneficio
                         | IncludesPessoaAtuacaoBeneficio.HistoricoVigencias;

            var result = this.SearchBySpecification(filtro, out total, includes).TransformList<PessoaAtuacaoBeneficioVO>();

            //Verifica se a pessoa atuação é aluno
            result.SMCForEach(f => f.Aluno = f.TipoAtuacao == TipoAtuacao.Aluno);

            //Buscar para todos os beneficios situação atual da chancela
            result.SMCForEach(f => f.SeqSituacaoChancelaBeneficioAtual = (int)this.BeneficioHistoricoSituacaoDomainService.BuscarHistoricoSituacaoChancelaBeneficioAtual(f.Seq));

            //Buscar Data fim da vigencia
            result.SMCForEach(f =>
            {
                //Valida uma falta em situação de ajuste de banco histórico de vigência
                if (f.HistoricoVigencias.SMCAny())
                {
                    f.DataInicioVigencia = f.HistoricoVigencias.FirstOrDefault(fd => fd.Atual).DataInicioVigencia;
                    f.DataFimVigencia = f.HistoricoVigencias.FirstOrDefault(fd => fd.Atual).DataFimVigencia;
                }
            }
            );

            result = this.OrdernarListaPessoaAtuacaoBeneficio(result);

            return new SMCPagerData<PessoaAtuacaoBeneficioVO>(result, total);
        }

        /// <summary>
        /// Os benefícios deverão ser ordenados:
        /// Primeiramente pela Situação e, na seguinte ordem: Aguardando Chancela, Deferido, Excluido · e Indeferido.
        /// - Em seguida pela data de início de vigência e, em ordem crescente.
        /// - E por último pela data de fim de vigência e, em ordem crescente.
        /// </summary>
        /// <param name="lista">Lista de Pesssoa Atuacao Beneficio</param>
        /// <returns>Lista ordenada conforme regras</returns>
        private List<PessoaAtuacaoBeneficioVO> OrdernarListaPessoaAtuacaoBeneficio(List<PessoaAtuacaoBeneficioVO> lista)
        {
            List<PessoaAtuacaoBeneficioVO> retorno = new List<PessoaAtuacaoBeneficioVO>();

            var listaNenhum = lista.Where(w => w.SeqSituacaoChancelaBeneficioAtual == (int)SituacaoChancelaBeneficio.Nenhum).OrderBy(o => o.DataInicioVigencia).ThenBy(t => t.DataFimVigencia).ToList();
            var listaAguardandoChancela = lista.Where(w => w.SeqSituacaoChancelaBeneficioAtual == (int)SituacaoChancelaBeneficio.AguardandoChancela).OrderBy(o => o.DataInicioVigencia).ThenBy(t => t.DataFimVigencia).ToList();
            var listaDeferido = lista.Where(w => w.SeqSituacaoChancelaBeneficioAtual == (int)SituacaoChancelaBeneficio.Deferido).OrderBy(o => o.DataInicioVigencia).ThenBy(t => t.DataFimVigencia).ToList();
            var listaIndeferido = lista.Where(w => w.SeqSituacaoChancelaBeneficioAtual == (int)SituacaoChancelaBeneficio.Indeferido).OrderBy(o => o.DataInicioVigencia).ThenBy(t => t.DataFimVigencia).ToList();
            var listaExcluido = lista.Where(w => w.SeqSituacaoChancelaBeneficioAtual == (int)SituacaoChancelaBeneficio.Excluido).OrderBy(o => o.DataInicioVigencia).ThenBy(t => t.DataFimVigencia).ToList();

            retorno.AddRange(listaNenhum);
            retorno.AddRange(listaAguardandoChancela);
            retorno.AddRange(listaDeferido);
            retorno.AddRange(listaIndeferido);
            retorno.AddRange(listaExcluido);

            return retorno;
        }

        public PessoaAtuacaoBeneficioVO BuscarPessoaAtuacaoCabecalho(long seqPessoaAtuacao)
        {
            var includes = IncludesPessoaAtuacao.DadosPessoais
                         | IncludesPessoaAtuacao.Pessoa;

            PessoaAtuacaoBeneficioVO pessoaAtuacao = PessoaAtuacaoDomainService.SearchByKey(new SMCSeqSpecification<PessoaAtuacao>(seqPessoaAtuacao), includes)
                                                                               .Transform<PessoaAtuacaoBeneficioVO>();

            pessoaAtuacao.CondicaoPagamento = BuscarDadosCondicaoPagamentoCabecalho(seqPessoaAtuacao, pessoaAtuacao.TipoAtuacao);

            return pessoaAtuacao;
        }

        public PessoaAtuacaoBeneficioVO BuscarPessoaAtuacaoDocumentoCabecalho(long seqPessoaAtuacao)
        {
            var includes = IncludesPessoaAtuacao.DadosPessoais
                         | IncludesPessoaAtuacao.Pessoa;

            PessoaAtuacaoBeneficioVO pessoaAtuacao = PessoaAtuacaoDomainService.SearchByKey(new SMCSeqSpecification<PessoaAtuacao>(seqPessoaAtuacao), includes)
                                                                               .Transform<PessoaAtuacaoBeneficioVO>();

            return pessoaAtuacao;
        }

        /// <summary>
        /// Salvar a pessoa atuação benefício
        /// </summary>
        /// <param name="pessoaAtuacaoBeneficioVO">Dados a serem salvos</param>
        /// <returns>Sequencial da pessoa atuação</returns>
        public long SalvarPessoaAtuacaoBeneficio(PessoaAtuacaoBeneficioVO pessoaAtuacaoBeneficioVO)
        {
            bool gerarTermoIngressante = false;
            long seqSolicitacaoMatriculaIngressante = 0;

            var pessoaAtuacaoBeneficio = pessoaAtuacaoBeneficioVO.Transform<PessoaAtuacaoBeneficio>();

            using (var unitOfWork = SMCUnitOfWork.Begin())
            {
                if (pessoaAtuacaoBeneficio.ArquivosAnexo.SMCAny())
                {
                    foreach (var arquivo in pessoaAtuacaoBeneficio.ArquivosAnexo)
                    {
                        var guid = new Guid(pessoaAtuacaoBeneficioVO.ArquivosAnexo.First(f => f.Seq == arquivo.Seq).ArquivoAnexado.GuidFile);
                        var specArquivo = new ArquivoAnexadoFilterSpecification() { UidArquivo = guid };
                        arquivo.SeqArquivoAnexado = arquivo.ArquivoAnexado.Seq = ArquivoAnexadoDomainService
                            .SearchProjectionBySpecification(specArquivo, p => p.Seq)
                            .FirstOrDefault();
                        EnsureFileIntegrity(arquivo, s => s.SeqArquivoAnexado, a => a.ArquivoAnexado);
                    }
                }
                var dadosOrigem = PessoaAtuacaoDomainService.RecuperaDadosOrigem(pessoaAtuacaoBeneficio.SeqPessoaAtuacao);

                var tipoAtuacaoPessoaAtuacao = PessoaAtuacaoDomainService.SearchProjectionByKey(new SMCSeqSpecification<PessoaAtuacao>(pessoaAtuacaoBeneficio.SeqPessoaAtuacao), x => x.TipoAtuacao);

                var beneficio = BeneficioDomainService.BuscarBeneficio(pessoaAtuacaoBeneficio.SeqBeneficio);

                //Função que ira validar as datas e intervalos entre configuração de beneficio e valor
                ValidacaoIntervaloDatasConfiguracaoBeneficioValor(pessoaAtuacaoBeneficioVO);

                if (pessoaAtuacaoBeneficio.Seq > 0)
                {
                    var pessoaAtuacaoBeneficioBanco = this.SearchByKey(new SMCSeqSpecification<PessoaAtuacaoBeneficio>(pessoaAtuacaoBeneficio.Seq), IncludesPessoaAtuacaoBeneficio.ControlesFinanceiros).Transform<PessoaAtuacaoBeneficioVO>();
                    pessoaAtuacaoBeneficioBanco.SeqSituacaoChancelaBeneficioAtual = (int)this.BeneficioHistoricoSituacaoDomainService.BuscarHistoricoSituacaoChancelaBeneficioAtual(pessoaAtuacaoBeneficioBanco.Seq);

                    if (pessoaAtuacaoBeneficioBanco.ControlesFinanceiros.SMCAny())
                    {
                        //Veririca na alteração se a mudança de situação financeira
                        if ((pessoaAtuacaoBeneficioBanco.SeqSituacaoChancelaBeneficioAtual == (int)SituacaoChancelaBeneficio.Deferido && pessoaAtuacaoBeneficioVO.SeqSituacaoChancelaBeneficioAtual != (int)SituacaoChancelaBeneficio.Excluido) ||
                            (pessoaAtuacaoBeneficioBanco.SeqSituacaoChancelaBeneficioAtual == (int)SituacaoChancelaBeneficio.Excluido && pessoaAtuacaoBeneficioVO.SeqSituacaoChancelaBeneficioAtual != (int)SituacaoChancelaBeneficio.Deferido))
                        {
                            throw new PessoaAtuacaoBeneficioContratoSituacaoFinanceiraException();
                        }

                        //NV20 Verifica se data fim é válida segundo os contratos já associados
                        if (pessoaAtuacaoBeneficioVO.DataFimVigencia.GetValueOrDefault() < pessoaAtuacaoBeneficio.ControlesFinanceiros.Max(m => m.DataFim))
                        {
                            throw new PessoaAtuacaoBeneficioDataFimContratoException();
                        }
                    }
                }

                ///Validar se percentuais e valores compreendem as regras de validação
                ///Percentual de bolsa, então o somatório do % da bolsa + % dos responsáveis não pode exceder 100%.
                ///Valor de bolsa ou Saldo final de bolsa, então o somatório do % dos responsáveis não pode exceder 100%
                decimal valorAcumuladoResponsaveis = 0;
                if (pessoaAtuacaoBeneficio.ResponsaveisFinanceiro != null)
                {
                    foreach (var item in pessoaAtuacaoBeneficio.ResponsaveisFinanceiro)
                    {
                        valorAcumuladoResponsaveis += item.ValorPercentual;
                    }
                }

                if (pessoaAtuacaoBeneficio.FormaDeducao == FormaDeducao.PercentualBolsa)
                {
                    if (valorAcumuladoResponsaveis + (decimal)pessoaAtuacaoBeneficio.ValorBeneficio > 100)
                    {
                        throw new PessoaAtuacaoBeneficioPercentualBolsaException(pessoaAtuacaoBeneficio.Seq == 0 ? INCLUSAO : ALTERAR);
                    }
                }
                else
                {
                    if (valorAcumuladoResponsaveis > 100)
                    {
                        throw new PessoaAtuacaoBeneficioValorSaldoFinalBolsaException(pessoaAtuacaoBeneficio.Seq == 0 ? INCLUSAO : ALTERAR);
                    }
                }

                ///[CONSISTÊNCIA DO NRO DE PARCELAS DE ACORDO COM A CONFIGURAÇÃO DO BENEFÍCIO]
                ///Se a pessoa-atuação for do tipo de atuação Ingressante e o benefício que está sendo associado
                ///possuir um número de parcelas padrão para a condição de pagamento, conforme instituição e nível
                ///de ensino do ingressante, verificar se a solicitação de matrícula do ingressante possui condição de
                ///pagamento preenchido.
                ///Se a opção for SIM atualizar a condição de pagamento da solicitação de matrícula do ingressante,
                ///conforme a condição de pagamento retornada pela rotina do financeiro definida na regra RN_MAT_076
                ///-Exibição dados financeiros condição pagamento. Caso a rotina do financeiro não retorne nenhuma
                ///condição de pagamento, abortar a operação e exibir a seguinte mensagem impeditiva: "Para associar
                ///esse benefício é necessário existir uma condição de pagamento válida no sistema financeiro com a
                ///mesma quantidade de parcelas exigida pelo benefício.
                if (tipoAtuacaoPessoaAtuacao == TipoAtuacao.Ingressante)
                {
                    // (Task 51995) Apenas fazer a validação das parcelas do ingressante se o benefício estiver Deferido
                    if (pessoaAtuacaoBeneficio.Seq > 0 && pessoaAtuacaoBeneficioVO.SeqSituacaoChancelaBeneficioAtual == (int)SituacaoChancelaBeneficio.Deferido)
                    {
                        if (this.ValidarNumeroParcelasParametrizadosConfiguracaoSaoDiferentes(pessoaAtuacaoBeneficio.SeqPessoaAtuacao, pessoaAtuacaoBeneficio.SeqBeneficio))
                        {
                            var SolicitacaoMatricula = this.SolicitacaoMatriculaDomainService.SearchBySpecification(new SolicitacaoMatriculaFilterSpecification() { SeqPessoaAtuacao = pessoaAtuacaoBeneficio.SeqPessoaAtuacao }).FirstOrDefault();

                            if (SolicitacaoMatricula.SeqCondicaoPagamentoGra != null)
                            {
                                var quantidadeParcelasBeneficio = this.InstituicaoNivelBeneficioDomainService.SearchProjectionBySpecification(new InstituicaoNivelBeneficioFilterSpecification() { SeqBeneficio = pessoaAtuacaoBeneficio.SeqBeneficio, SeqNivelEnsino = dadosOrigem.SeqNivelEnsino }, p => p.NumeroParcelasPadraoCondicaoPagamento).FirstOrDefault();
                                var condicaoPagamento = this.SolicitacaoMatriculaDomainService.BuscarCondicoesPagamentoAcademicoBaseadoNovoBeneficio(SolicitacaoMatricula.Seq, quantidadeParcelasBeneficio.GetValueOrDefault()).FirstOrDefault();

                                if (condicaoPagamento != null && condicaoPagamento.SeqCondicaoPagamento != null)
                                {
                                    SolicitacaoMatricula.SeqCondicaoPagamentoGra = condicaoPagamento.SeqCondicaoPagamento;
                                    SolicitacaoMatricula.SeqArquivoTermoAdesao = null;
                                    this.SolicitacaoMatriculaDomainService.SaveEntity(SolicitacaoMatricula);

                                    // Alterado para gerar o termo depois de salvar o benefício como deferido
                                    //this.ContratoDomainService.GerarTermoAdesaoContrato(SolicitacaoMatricula.Seq);
                                    gerarTermoIngressante = true;
                                    seqSolicitacaoMatriculaIngressante = SolicitacaoMatricula.Seq;
                                }
                                else
                                {
                                    throw new PessoaAtuacaoBeneficioCondicaoPagamentoIngressanteException();
                                }
                            }
                        }
                    }
                }

                ///Se a pessoa - atuação for do tipo Aluno e o benefício que está sendo associado possuir um número de
                ///parcelas padrão para a condição de pagamento, conforme instituição-nível de ensino do aluno,
                ///verificar se a condição de pagamento no GRA do aluno é compatível com o número de parcelas padrão
                ///parametrizado no Acadêmico.A identificação da condição de pagamento no GRA do Aluno é conforme
                ///o retorno da rotina do financeiro definida na regra: RN_FIN_014 - Pessoa Atuação x Benefício - Aciona
                ///rotina GRA-Identificação Condição de Pagamento do Aluno
                ///
                if (tipoAtuacaoPessoaAtuacao == TipoAtuacao.Aluno)
                {
                    if (this.ValidarNumeroParcelasParametrizadosConfiguracaoSaoDiferentes(pessoaAtuacaoBeneficio.SeqPessoaAtuacao, pessoaAtuacaoBeneficio.SeqBeneficio)
                        && pessoaAtuacaoBeneficioVO.SeqSituacaoChancelaBeneficioAtual == (int)SituacaoChancelaBeneficio.Deferido)
                    {
                        throw new PessoaAtuacaoBeneficioCondicaoPagamentoAlunoException();
                    }
                }

                /*[CONSISTÊNCIA BÁSICA DE RESTRIÇÃO DE COMBINAÇÃO DE BENEFÍCIO] - Nova regra Remover regra
                Se a pessoa-atuação já possui outros benefícios, verificar se pelo menos 1(um) desses demais
                benefícios possuem todas as seguintes características:
                - Forma de Dedução igual a Saldo Final de Parcela
                - Situação diferente de EXCLUIDO
                - Período de vigência coincide com algum intervalo do benefício que está sendo salvo
                Se identificado pelo menos 1(um) benefício com as características acima, ou se possuir o mesmo benefício
                em período coincidente, abortar a oepração e exibir a seguinte mensagem impeditiva:*/
                var listaPessoaAtuacaoBeneficios = this.BuscarPesssoasAtuacoesBeneficios(new PessoaAtuacaoBeneficioFilterSpecification() { SeqPessoaAtuacao = pessoaAtuacaoBeneficio.SeqPessoaAtuacao, Excluidos = false });
                if (listaPessoaAtuacaoBeneficios.Count() > 0)
                {
                    bool validacaoIrregular = false;

                    foreach (var item in listaPessoaAtuacaoBeneficios)
                    {
                        if (item.Seq != pessoaAtuacaoBeneficioVO.Seq)
                        {
                            if (item.FormaDeducao == FormaDeducao.SaldoFinalParcela)
                            {
                                if (this.BeneficioHistoricoSituacaoDomainService.BuscarHistoricoSituacaoChancelaBeneficioAtual(item.Seq) == SituacaoChancelaBeneficio.Deferido)
                                {
                                    if ((item.DataInicioVigencia >= pessoaAtuacaoBeneficioVO.DataInicioVigencia && item.DataInicioVigencia <= pessoaAtuacaoBeneficioVO.DataFimVigencia) ||
                                        (pessoaAtuacaoBeneficioVO.DataInicioVigencia >= item.DataInicioVigencia && pessoaAtuacaoBeneficioVO.DataInicioVigencia <= item.DataFimVigencia))
                                    {
                                        validacaoIrregular = true;
                                    }
                                }
                            }
                        }

                        if (validacaoIrregular)
                        {
                            var descricaoBeneficio = this.BeneficioDomainService.BuscarBeneficio((long)item.SeqBeneficio).Descricao;
                            throw new PessoaAtuacaoBeneficioRestricaoCombinacaoException((pessoaAtuacaoBeneficioVO.Seq == 0 ? INCLUSAO : ALTERAR), descricaoBeneficio);
                        }
                    }
                }

                //Depois das validações historico da situação do beneficio será salvo de forma individual desta forma exclui a lista de historico do objeto para evitar conflitos
                pessoaAtuacaoBeneficio.HistoricoSituacoes = null;

                if (pessoaAtuacaoBeneficioVO.SeqSituacaoChancelaBeneficioAtual == (int)SituacaoChancelaBeneficio.Deferido)
                {
                    this.SaveEntity(pessoaAtuacaoBeneficio);
                    this.SalvarBeneficioHistoricoSituacaoChancela(pessoaAtuacaoBeneficio.Seq, pessoaAtuacaoBeneficioVO.SeqSituacaoChancelaBeneficioAtual, pessoaAtuacaoBeneficioVO.Justificativa);
                    this.SalvarBeneficioHisitoricoVigencia(pessoaAtuacaoBeneficio.Seq, pessoaAtuacaoBeneficioVO.DataInicioVigencia, pessoaAtuacaoBeneficioVO.DataFimVigencia, pessoaAtuacaoBeneficioVO.Justificativa);

                    // Bug 52281 - O termo do ingressante estava sendo gerado antes de salvar em banco que o benefício está deferido, o que fazia com que o método
                    // de gerar termo não recuperasse corretamente o termo atualizado. Adicionado aqui para gerar o termo assim que ele for salvo como deferido
                    if (tipoAtuacaoPessoaAtuacao == TipoAtuacao.Ingressante && gerarTermoIngressante && seqSolicitacaoMatriculaIngressante != 0)
                    {
                        this.ContratoDomainService.GerarTermoAdesaoContrato(seqSolicitacaoMatriculaIngressante, true);
                    }

                    // TODO: Considerar o flag enviado da tela
                    try
                    {
                        this.AtualizarContratoPessoaAtuacaoBeneficio(pessoaAtuacaoBeneficio.Seq);
                    }
                    catch (PessoaAtuacaoBeneficioValidacaoChancelaException ex)
                    {
                        throw ex;
                    }
                    catch (Exception ex)
                    {
                        throw new PessoaAtuacaoBeneficioErroIncluirBeneficioException(ex.Message);
                    }

                    if (beneficio.IncluirDesbloqueioTemporario
                        && ((tipoAtuacaoPessoaAtuacao == TipoAtuacao.Ingressante && pessoaAtuacaoBeneficio.IncideParcelaMatricula)
                             || tipoAtuacaoPessoaAtuacao == TipoAtuacao.Aluno))
                    {
                        /*Verificar se a pessoa-atuação em questão possui bloqueio com token
                        'PARCELA_MATRICULA_PENDENTE ou 'PARCELA_PRE_MATRICULA_PENDENTE' com a situação
                        igual a "Bloqueado".Para cada bloqueio encontrado, verificar a solicitacao de servico correspondente e
                        Atributos marcados com* são de preenchimento obrigatório.
                        encontrar a data de inicio do PERIODO_LETIVO, do ciclo letivo da solicitação.Se a data de inicio do
                        PERIODO_LETIVO estiver dentro do período de vigência do benefício, atualizar os seguintes campos do
                        respectivo bloqueio:*/
                        var includePessoaAtuacaoBloqueio = IncludesPessoaAtuacaoBloqueio.SolicitacaoServico_ConfiguracaoProcesso_Processo_CicloLetivo;
                        var listaBloqueios = PessoaAtuacaoBloqueioDomainService
                                                                .SearchBySpecification(new PessoaAtuacaoBloqueioFilterSpecification()
                                                                {
                                                                    SeqPessoaAtuacao = pessoaAtuacaoBeneficio.SeqPessoaAtuacao,
                                                                    BloqueioMatricula = true
                                                                }, includePessoaAtuacaoBloqueio).Where(x => x.SituacaoBloqueio == SituacaoBloqueio.Bloqueado).ToList();

                        foreach (var item in listaBloqueios)
                        {
                            //Buscar a data de inicio do evento letivo
                            DatasEventoLetivoVO datasEventoLetivo = new DatasEventoLetivoVO();
                            if (tipoAtuacaoPessoaAtuacao == TipoAtuacao.Aluno)
                            {
                                var includesPlanoEstudoItem = IncludesPlanoEstudoItem.PlanoEstudo_AlunoHistoricoCicloLetivo_AlunoHistorico_CursoOfertaLocalidadeTurno_CursoOfertaLocalidade;
                                var planoEstudoItem = this.PlanoEstudoItemDomainService.SearchBySpecification(new PlanoEstudoItemFilterSpecification() { SeqAluno = pessoaAtuacaoBeneficio.SeqPessoaAtuacao, PlanoEstudoAtual = true }, includesPlanoEstudoItem).FirstOrDefault();

                                datasEventoLetivo = ConfiguracaoEventoLetivoDomainService.BuscarDatasEventoLetivo(
                                                    item.SolicitacaoServico.ConfiguracaoProcesso.Processo.CicloLetivo.Seq,
                                                    dadosOrigem.SeqCursoOfertaLocalidadeTurno,
                                                    planoEstudoItem.PlanoEstudo.AlunoHistoricoCicloLetivo.TipoAluno,
                                                    TOKEN_TIPO_EVENTO.PERIODO_CICLO_LETIVO);
                            }
                            else if (tipoAtuacaoPessoaAtuacao == TipoAtuacao.Ingressante)
                            {
                                datasEventoLetivo = ConfiguracaoEventoLetivoDomainService.BuscarDatasEventoLetivo(
                                                    item.SolicitacaoServico.ConfiguracaoProcesso.Processo.CicloLetivo.Seq,
                                                    dadosOrigem.SeqCursoOfertaLocalidadeTurno,
                                                    TipoAluno.Calouro,
                                                    TOKEN_TIPO_EVENTO.PERIODO_CICLO_LETIVO);
                            }

                            //RN_FIN_023 - Pessoa Atuação x Benefício - Incluir desbloqueio temporário na parcela de matrícula pendente
                            var permiteDesbloqueioIngressante = tipoAtuacaoPessoaAtuacao == TipoAtuacao.Ingressante && beneficio.IncluirDesbloqueioTemporario && pessoaAtuacaoBeneficio.IncideParcelaMatricula;

                            //Para aluno:
                            //Verifico se a data inicio do evento letivo e vigente pelo beneficio
                            //Uma vez que a data da parcela de matricula sempre ocorre no inicio do ciclo letivo
                            //Desta forma a consistência é feita a verificação se a parcela de matricula é vigente no beneficio
                            var permiteDesbloqueioAluno = tipoAtuacaoPessoaAtuacao == TipoAtuacao.Aluno && beneficio.IncluirDesbloqueioTemporario && datasEventoLetivo.DataInicio >= pessoaAtuacaoBeneficioVO.DataInicioVigencia && datasEventoLetivo.DataInicio <= pessoaAtuacaoBeneficioVO.DataFimVigencia;

                            if (permiteDesbloqueioIngressante || permiteDesbloqueioAluno)
                            {
                                item.SituacaoBloqueio = SituacaoBloqueio.Desbloqueado;
                                item.TipoDesbloqueio = TipoDesbloqueio.Temporario;
                                item.UsuarioDesbloqueioTemporario = beneficio.Seq + " - " + beneficio.Descricao;
                                item.DataAlteracao = DateTime.Now;
                                item.CadastroIntegracao = true;
                                item.JustificativaDesbloqueio = beneficio.Seq + " - " + beneficio.Descricao;
                                item.DataDesbloqueioTemporario = DateTime.Now;

                                PessoaAtuacaoBloqueioDomainService.SaveEntity(item);
                            }
                        }
                    }
                }
                else ///Caso não atenda as RN ele salva sem chamar o financeiro
				{
                    this.SaveEntity(pessoaAtuacaoBeneficio);
                    this.SalvarBeneficioHistoricoSituacaoChancela(pessoaAtuacaoBeneficio.Seq, pessoaAtuacaoBeneficioVO.SeqSituacaoChancelaBeneficioAtual, pessoaAtuacaoBeneficioVO.Justificativa);
                    this.SalvarBeneficioHisitoricoVigencia(pessoaAtuacaoBeneficio.Seq, pessoaAtuacaoBeneficioVO.DataInicioVigencia, pessoaAtuacaoBeneficioVO.DataFimVigencia, pessoaAtuacaoBeneficioVO.Justificativa);
                }
                //throw new Exception("Abortado para testes");
                ///Rollback caso alguma das funções provoquem erro
                unitOfWork.Commit();
            }
            return pessoaAtuacaoBeneficio.Seq;
        }

        /// <summary>
        /// Salvar historico situação da pessoa atuacao beneficio
        /// </summary>
        /// <param name="seqPessoaAtuacaBeneficio">Sequencial pessoa atuação beneficio</param>
        /// <param name="SeqSituacaoChancelaBeneficioAtual">Seqeuncial do Enum da situação a ser salvo</param>
        /// <param name="justificativa">Texto a ser salvo na observação</param>
        public void SalvarBeneficioHistoricoSituacaoChancela(long seqPessoaAtuacaBeneficio, int SeqSituacaoChancelaBeneficioAtual, string justificativa)
        {
            //Verifica a atual e diferente da atual que esta registrada para evitar duplicidade no momento da alteração.
            if ((int)this.BeneficioHistoricoSituacaoDomainService.BuscarHistoricoSituacaoChancelaBeneficioAtual(seqPessoaAtuacaBeneficio) != SeqSituacaoChancelaBeneficioAtual)
            {
                BeneficioHistoricoSituacao historico = new BeneficioHistoricoSituacao();

                historico.DataInicioSituacao = DateTime.Now;
                historico.Observacao = justificativa;
                historico.SeqPessoaAtuacaoBeneficio = seqPessoaAtuacaBeneficio;
                historico.SituacaoChancelaBeneficio = SMCEnumHelper.GetEnum<SituacaoChancelaBeneficio>(SeqSituacaoChancelaBeneficioAtual.ToString());
                historico.Atual = true;

                this.BeneficioHistoricoSituacaoDomainService.SaveEntity(historico);
            }
        }

        /// <summary>
        /// Salvar uma vigência nova
        /// </summary>
        /// <param name="seqPessoaAtuacaBeneficio">Sequencial da pessoa atuação</param>
        /// <param name="dataInicioVigencia">Data inicio da vigência</param>
        /// <param name="dataFimVigencia">Data fim da vigência</param>
        /// <param name="justificativa">Justificativa da alteração da vigência</param>
        /// <param name="seqMotivoAlteracaoBeneficio">Sequencial do motivo de alteração do benefício</param>
        public void SalvarBeneficioHisitoricoVigencia(long seqPessoaAtuacaBeneficio, DateTime dataInicioVigencia, DateTime? dataFimVigencia, string justificativa, long? seqMotivoAlteracaoBeneficio = null)
        {
            var vigenciaAtual = this.BeneficioHistoricoVigenciaDomainService.SearchProjectionBySpecification(new BeneficioHistoricoVigenciaFilterSpecification() { SeqPessoaAtuacaoBeneficio = seqPessoaAtuacaBeneficio, Atual = true },
                                                                                                             p => new BeneficioHistoricoVigenciaVO
                                                                                                             {
                                                                                                                 Atual = p.Atual,
                                                                                                                 DataInicioVigencia = p.DataInicioVigencia,
                                                                                                                 DataFimVigencia = p.DataFimVigencia
                                                                                                             }).FirstOrDefault();
            if (vigenciaAtual != null)
            {
                if (vigenciaAtual.DataInicioVigencia != dataInicioVigencia || vigenciaAtual.DataFimVigencia != dataFimVigencia)
                {
                    BeneficioHistoricoVigencia beneficioHistoricoVigencia = new BeneficioHistoricoVigencia();

                    beneficioHistoricoVigencia.SeqPessoaAtuacaoBeneficio = seqPessoaAtuacaBeneficio;
                    beneficioHistoricoVigencia.DataInicioVigencia = dataInicioVigencia;
                    beneficioHistoricoVigencia.DataFimVigencia = dataFimVigencia;
                    beneficioHistoricoVigencia.SeqMotivoAlteracaoBeneficio = seqMotivoAlteracaoBeneficio;
                    beneficioHistoricoVigencia.Observacao = justificativa;
                    beneficioHistoricoVigencia.Atual = true;

                    this.BeneficioHistoricoVigenciaDomainService.SaveEntity(beneficioHistoricoVigencia);
                }
            }
            else
            {
                BeneficioHistoricoVigencia beneficioHistoricoVigencia = new BeneficioHistoricoVigencia();

                beneficioHistoricoVigencia.SeqPessoaAtuacaoBeneficio = seqPessoaAtuacaBeneficio;
                beneficioHistoricoVigencia.DataInicioVigencia = dataInicioVigencia;
                beneficioHistoricoVigencia.DataFimVigencia = dataFimVigencia;
                beneficioHistoricoVigencia.Atual = true;
                beneficioHistoricoVigencia.SeqMotivoAlteracaoBeneficio = seqMotivoAlteracaoBeneficio;
                beneficioHistoricoVigencia.Observacao = justificativa;

                this.BeneficioHistoricoVigenciaDomainService.SaveEntity(beneficioHistoricoVigencia);
            }
        }

        public void ValidacaoIntervaloDatasConfiguracaoBeneficioValor(PessoaAtuacaoBeneficioVO pessoaAtuacaoBeneficio)
        {
            var dadosOrigem = PessoaAtuacaoDomainService.RecuperaDadosOrigem(pessoaAtuacaoBeneficio.SeqPessoaAtuacao);

            var tipoAtuacaoPessoaAtuacao = PessoaAtuacaoDomainService.SearchProjectionByKey(new SMCSeqSpecification<PessoaAtuacao>(pessoaAtuacaoBeneficio.SeqPessoaAtuacao), x => x.TipoAtuacao);

            var beneficio = BeneficioDomainService.BuscarBeneficio((long)pessoaAtuacaoBeneficio.SeqBeneficio);

            long nivelEnsino = 0;

            //Incializacao padrão
            SituacaoIngressante situacaoIngressante = SituacaoIngressante.Nenhum;
            var situacaoAluno = new SituacaoAlunoVO();

            if (tipoAtuacaoPessoaAtuacao == TipoAtuacao.Ingressante)
            {
                nivelEnsino = IngressanteDomainService.SearchProjectionByKey(new SMCSeqSpecification<Ingressante>(pessoaAtuacaoBeneficio.SeqPessoaAtuacao),
                                                                              x => x.SeqNivelEnsino);
                situacaoIngressante = (SituacaoIngressante)IngressanteDomainService.SearchProjectionByKey(new IngressanteFilterSpecification { Seq = pessoaAtuacaoBeneficio.SeqPessoaAtuacao },
                                                                      p => (SituacaoIngressante?)p.HistoricosSituacao.OrderByDescending(o => o.Seq).FirstOrDefault().SituacaoIngressante);
            }
            else if (tipoAtuacaoPessoaAtuacao == TipoAtuacao.Aluno)
            {
                nivelEnsino = this.AlunoHistoricoDomainService.SearchProjectionBySpecification(new AlunoHistoricoFilterSpecification() { SeqAluno = pessoaAtuacaoBeneficio.SeqPessoaAtuacao, Atual = true }, p => p.SeqNivelEnsino).FirstOrDefault();
                situacaoAluno = this.AlunoHistoricoSituacaoDomainService.BuscarSituacaoAtualAluno(pessoaAtuacaoBeneficio.SeqPessoaAtuacao);
            }

            ///[CONSISTÊNCIA DA SITUAÇÃO ATUAL DO INGRESSANTE E DO ALUNO AO DEFERIR O BENEFÍCIO]
            ///Se a situação do benefício for igual a Deferido e, a situação atual do Ingressante for igual a
            ///Matriculado, Desistente ou Cancelado, abortar a operação e exibir a seguinte mensagem impeditiva:
            ///"<Alteração> não permitida. A situação atual do ingressante não permite o deferimento de benefício."
            ///Senão, se a situação do benefício for igual a Deferido e, a situação atual do Aluno não possui vínculo ativo,
            ///abortar a operação e exibir a seguinte mensagem impeditiva:
            ///"<Alteração> não permitida. A situação atual do aluno não permite o deferimento de benefício."
            if (pessoaAtuacaoBeneficio.SeqSituacaoChancelaBeneficioAtual == (int)SituacaoChancelaBeneficio.Deferido)
            {
                if (tipoAtuacaoPessoaAtuacao == TipoAtuacao.Ingressante)
                {
                    if (situacaoIngressante == SituacaoIngressante.Matriculado || situacaoIngressante == SituacaoIngressante.Desistente || situacaoIngressante == SituacaoIngressante.Cancelado)
                    {
                        throw new PessoaAtuacaoBeneficioDeferirBeneficioException((pessoaAtuacaoBeneficio.Seq == 0 ? INCLUSAO : ALTERAR), TipoAtuacao.Ingressante.SMCGetDescription().ToLower());
                    }
                }
                else if (tipoAtuacaoPessoaAtuacao == TipoAtuacao.Aluno && pessoaAtuacaoBeneficio.Seq == 0)
                {
                    if (!situacaoAluno.VinculoAlunoAtivo.GetValueOrDefault())
                    {
                        throw new PessoaAtuacaoBeneficioDeferirBeneficioException((pessoaAtuacaoBeneficio.Seq == 0 ? INCLUSAO : ALTERAR), TipoAtuacao.Aluno.SMCGetDescription().ToLower());
                    }
                }
            }

            ///[CONSISTÊNCIA DATA DE INÍCIO DEVE SER MENOR QUE A DATA FIM] - Nova regra
            ///Se a data início for maior que a data fim, abortar a operação e exibir a seguinte mensagem impeditiva:
            ///"Associação não permitida. A data de início deve ser menor que a data fim.
            if (pessoaAtuacaoBeneficio.DataInicioVigencia > pessoaAtuacaoBeneficio.DataFimVigencia)
            {
                throw new PessoaAtuacaoBeneficioDataInicioDataFimBeneficioException();
            }

            ///[CONSISTÊNCIA DATA DE INÍCIO(primeiro dia) e DATA FIM(último dia)] NV13
            ///Se a data início não for o primeiro dia do mês, abortar a operação e exibir a seguinte mensagem
            ///impeditiva:
            if (pessoaAtuacaoBeneficio.DataInicioVigencia.Day != 1)
            {
                throw new PessoaAtuacaoBeneficioDataInicioException(pessoaAtuacaoBeneficio.Seq == 0 ? INCLUSAO : ALTERAR);
            }

            ///Se a data fim não for o último dia do mês, abortar a operação e exibir a seguinte mensagem impeditiva:
            ///"Inclusão/Alteração não permitida. A data fim deve ser sempre o último dia do mês."
            if (pessoaAtuacaoBeneficio.DataFimVigencia != null
                && ((DateTime)pessoaAtuacaoBeneficio.DataFimVigencia).AddDays(1).Month == ((DateTime)pessoaAtuacaoBeneficio.DataFimVigencia).Month)
            {
                throw new PessoaAtuacaoBeneficioDataFimException(pessoaAtuacaoBeneficio.Seq == 0 ? INCLUSAO : ALTERAR);
            }

            ///[CONSISTÊNCIA DA DATA DE INÍCIO - INGRESSANTE(Data de Admissão) E ALUNO(Ciclo letivo atual ou futuro)]
            ///Se, Ingressante e, Incide Parcela Matrícula igual a Não e, não há configuração associada ao benefício:
            ///Início Vigência = deverá aceitar somente datas que seja o primeiro dia do mês / ano posterior ou igual
            ///a data de admissão do ingressante.Caso contrário, abortar a operação e exibir mensagem impeditiva:
            ///"Data de início não permitida. O benefício não incide na parcela de matrícula, nesse caso a data de início
            ///de vigência deverá ser maior ou IGUAL AO mês/ano da admissão do ingressante. Lembrando que o dia deve ser
            ///o primeiro dia do mês.”.
            ///
            /// Se, Ingressante e, Incide Parcela Matrícula igual a Não e, há configuração associada ao benefício:
            /// Início Vigência = deverá aceitar somente datas que seja o primeiro dia do mês / ano posterior a
            /// data de admissão do ingressante.Caso contrário, abortar a operação e exibir mensagem impeditiva:
            /// "Data de início não permitida. O benefício não incide na parcela de matrícula, nesse caso a data de
            /// início de vigência deverá ser maior que o mês/ano da admissão do ingressante. Lembrando que o dia
            /// deve ser o primeiro dia do mês.”
            ///
            ///Senão, se Ingressante e Incide Parcela Matrícula igual a Sim:
            ///Início Vigência = deverá aceitar somente datas que seja o primeiro dia do mês/ano da data de
            ///admissão do ingressante.Caso contrato, abortar a operação e exibir a seguinte mensagem
            ///impeditiva: "Data de início não permitida. O benefício incide na parcela de matrícula, nesse caso a
            ///data de início de vigência deverá ser o mês/ano da admissão do ingressante.Lembrando que o dia deve ser o primeiro dia do mês.”
            if (tipoAtuacaoPessoaAtuacao == TipoAtuacao.Ingressante)
            {
                var configuracaoBeneficioDtaAdmissao = this.ConfiguracaoBeneficioDomainService.SearchBySpecification(new ConfiguracaoBeneficioFilterSpecification() { SeqBeneficio = beneficio.Seq }).ToList();
                var dataAdmissao = this.BuscarDataAdmissaoIngressante(pessoaAtuacaoBeneficio.SeqPessoaAtuacao);

                if (!pessoaAtuacaoBeneficio.IncideParcelaMatricula && !configuracaoBeneficioDtaAdmissao.SMCAny())
                {
                    ///Gera uma data com o primeiro dia do mes e ano data de admissão
                    var dataAjustada = new DateTime(dataAdmissao.Year, dataAdmissao.Month, 1);

                    if (pessoaAtuacaoBeneficio.DataInicioVigencia <= dataAjustada)
                    {
                        throw new PessoaAtuacaoBeneficioDataInicioNaoIncideParcelaSemConfiguracaoException();
                    }
                }

                if (!pessoaAtuacaoBeneficio.IncideParcelaMatricula && configuracaoBeneficioDtaAdmissao.SMCAny())
                {
                    ///Gera uma data com o ultimo dia do mes e ano data de admissão desta forma a data de incio de vigencia do beneficio tem que ser maior que esta data
                    ///tendo em vista que a data inicio da vigencia sempre começa no dia primeiro
                    var dataAjustada = new DateTime(dataAdmissao.Year, dataAdmissao.Month, DateTime.DaysInMonth(dataAdmissao.Year, dataAdmissao.Month));

                    if (pessoaAtuacaoBeneficio.DataInicioVigencia < dataAjustada)
                    {
                        throw new PessoaAtuacaoBeneficioDataInicioNaoIncideParcelaComConfiguracaoException();
                    }
                }

                if (pessoaAtuacaoBeneficio.IncideParcelaMatricula)
                {
                    if (pessoaAtuacaoBeneficio.DataInicioVigencia != dataAdmissao)
                    {
                        throw new PessoaAtuacaoBeneficioDataInicioSimIncideParcelaException();
                    }
                }
            }

            if (tipoAtuacaoPessoaAtuacao == TipoAtuacao.Aluno)
            {
                var configuracaoBeneficioDtaAdmissao = this.ConfiguracaoBeneficioDomainService.SearchBySpecification(new ConfiguracaoBeneficioFilterSpecification() { SeqBeneficio = beneficio.Seq }).ToList();
                var dataAdmissao = this.BuscarDataAdmissaoAluno(pessoaAtuacaoBeneficio.SeqPessoaAtuacao);

                ///Gera uma data com o primeiro dia do mes e ano data de admissão
                var dataVigencia = new DateTime(dataAdmissao.Year, dataAdmissao.Month, 1);

                if (pessoaAtuacaoBeneficio.DataInicioVigencia < dataVigencia)
                {
                    throw new PessoaAtuacaoBeneficioDataAdmissaoAluno();
                }

            }

            if (beneficio.DeducaoValorParcelaTitular)
            {
                var spec = new InstituicaoNivelBeneficioFilterSpecification() { SeqNivelEnsino = nivelEnsino, SeqBeneficio = pessoaAtuacaoBeneficio.SeqBeneficio };
                spec.SetOrderByDescending(s => s.ConfiguracoesBeneficio.FirstOrDefault().DataInicioValidade);

                var configuracaoBeneficio = InstituicaoNivelBeneficioDomainService.SearchProjectionByKey(spec, x => x.ConfiguracoesBeneficio);

                if (configuracaoBeneficio.Count() > 0)
                {
                    var specHistoricoValorAuxilio = new BeneficioHistoricoValorAuxilioFilterSpecification { SeqBeneficio = pessoaAtuacaoBeneficio.SeqBeneficio, SeqNivelEnsino = nivelEnsino };
                    var historicoValorAuxilio = BeneficioHistoticoValorAuxilioDomainService.BuscarDadosValoresAuxilio(specHistoricoValorAuxilio);
                    int compreendeMaisIntervalos = 0;

                    ConfiguracaoBeneficio dataIniConfig = new ConfiguracaoBeneficio() { DataInicioValidade = new DateTime(), DataFimValidade = new DateTime() };
                    ConfiguracaoBeneficio dataFimConfig = new ConfiguracaoBeneficio() { DataInicioValidade = new DateTime(), DataFimValidade = new DateTime() };

                    if (historicoValorAuxilio.Count() > 0)
                    {
                        //Caso exista configuração
                        foreach (var configuracao in configuracaoBeneficio.OrderByDescending(x => x.DataInicioValidade))
                        {
                            //Verifica se a data inicio esta contido em um periodo e a data fim em outro
                            if (pessoaAtuacaoBeneficio.DataInicioVigencia < configuracao.DataInicioValidade &&
                               (pessoaAtuacaoBeneficio.DataInicioVigencia < configuracao.DataFimValidade || configuracao.DataFimValidade == null))
                            {
                                if (pessoaAtuacaoBeneficio.DataFimVigencia >= configuracao.DataInicioValidade &&
                                   (pessoaAtuacaoBeneficio.DataFimVigencia <= configuracao.DataFimValidade || configuracao.DataFimValidade == null))
                                {
                                    dataFimConfig.DataInicioValidade = configuracao.DataInicioValidade;
                                    dataFimConfig.DataFimValidade = configuracao.DataFimValidade;
                                }
                                compreendeMaisIntervalos++;
                                continue;
                            }//Verifica se a data inicio compreende e a data fim no mesmo intervalo
                            else if (pessoaAtuacaoBeneficio.DataInicioVigencia >= configuracao.DataInicioValidade &&
                                                         pessoaAtuacaoBeneficio.DataInicioVigencia <= configuracao.DataFimValidade || configuracao.DataFimValidade == null)
                            {
                                dataIniConfig.DataInicioValidade = configuracao.DataInicioValidade;
                                dataIniConfig.DataFimValidade = configuracao.DataFimValidade;

                                if (pessoaAtuacaoBeneficio.DataFimVigencia >= configuracao.DataInicioValidade &&
                                    (pessoaAtuacaoBeneficio.DataFimVigencia <= configuracao.DataFimValidade || configuracao.DataFimValidade == null))
                                {
                                    dataFimConfig.DataInicioValidade = configuracao.DataInicioValidade;
                                    dataFimConfig.DataFimValidade = configuracao.DataFimValidade;
                                }
                            }
                        }

                        if (compreendeMaisIntervalos > 1 ||
                          ((dataIniConfig.DataInicioValidade != dataFimConfig.DataInicioValidade && dataIniConfig.DataFimValidade != dataFimConfig.DataFimValidade) &&
                            dataIniConfig.DataInicioValidade != new DateTime() && dataFimConfig.DataInicioValidade != new DateTime()))
                        {
                            throw new PessoaAtuacaoBeneficioAtingeDuasConfiguracoesException();
                        }

                        //Verificar os intervalos entre os valores de historico ///
                        var atualConfiguracaoBenefico = ConfiguracaoBeneficioDomainService.SearchByKey(new SMCSeqSpecification<ConfiguracaoBeneficio>((int)pessoaAtuacaoBeneficio.SeqConfiguracaoBeneficio));

                        /*Se datas de vigencia estiverem fora da configuração atual
                        DataFimVigencia pessoaBeneficio não será nula pois ela é obrigatória*/
                        if (!((VereficarDataIntervalo(pessoaAtuacaoBeneficio.DataInicioVigencia, atualConfiguracaoBenefico.DataInicioValidade, atualConfiguracaoBenefico.DataFimValidade))
                             && (VereficarDataIntervalo((DateTime)pessoaAtuacaoBeneficio.DataFimVigencia, atualConfiguracaoBenefico.DataInicioValidade, atualConfiguracaoBenefico.DataFimValidade))))
                        {
                            bool verificarDataInicio = (pessoaAtuacaoBeneficio.DataInicioVigencia < atualConfiguracaoBenefico.DataInicioValidade || pessoaAtuacaoBeneficio.DataInicioVigencia > atualConfiguracaoBenefico.DataFimValidade);
                            bool verificarDataFim = (pessoaAtuacaoBeneficio.DataFimVigencia < atualConfiguracaoBenefico.DataInicioValidade ||
                                                    (pessoaAtuacaoBeneficio.DataFimVigencia > atualConfiguracaoBenefico.DataFimValidade || atualConfiguracaoBenefico.DataFimValidade != null));

                            List<BeneficioHistoricoValorAuxilioVO> listaHistoricosDataInicio = new List<BeneficioHistoricoValorAuxilioVO>();
                            List<BeneficioHistoricoValorAuxilioVO> listaHistoricosDataFim = new List<BeneficioHistoricoValorAuxilioVO>();

                            if (verificarDataInicio)
                            {
                                //DataInicioVigencia esta em algum intervalo
                                foreach (var item in historicoValorAuxilio.OrderByDescending(x => x.DataInicioValidade))
                                {
                                    if (VereficarDataIntervalo(pessoaAtuacaoBeneficio.DataInicioVigencia, item.DataInicioValidade, item.DataFimValidade))
                                    {
                                        listaHistoricosDataInicio.Add(item);
                                    }
                                }

                                if (listaHistoricosDataInicio.Count == 0)
                                {
                                    throw new PessoaAtuacaoBeneficioNaoExisteIntervaloException();
                                }

                                listaHistoricosDataInicio = new List<BeneficioHistoricoValorAuxilioVO>();

                                foreach (var item in historicoValorAuxilio.OrderByDescending(x => x.DataInicioValidade))
                                {
                                    if (item.DataInicioValidade < atualConfiguracaoBenefico.DataInicioValidade)
                                    {
                                        if ((pessoaAtuacaoBeneficio.DataInicioVigencia < item.DataFimValidade || item.DataFimValidade == null)
                                             && pessoaAtuacaoBeneficio.DataInicioVigencia < item.DataInicioValidade)
                                        {
                                            listaHistoricosDataInicio.Add(item);
                                            continue;
                                        }

                                        if (VereficarDataIntervalo(pessoaAtuacaoBeneficio.DataInicioVigencia, item.DataInicioValidade, item.DataFimValidade))
                                        {
                                            listaHistoricosDataInicio.Add(item);
                                            break;
                                        }
                                    }
                                }

                                for (int x = 0; listaHistoricosDataInicio.OrderByDescending(y => y.DataInicioValidade).Count() > x; x++)
                                {
                                    if (listaHistoricosDataInicio.Count() > 1)
                                    {
                                        if (listaHistoricosDataInicio.Count() >= (x + 2))
                                        {
                                            //Verifica se a data da lista atual é igual a proxima data da lista caso não seja existe um buraco entre elas
                                            var proximaDataLista = DateTime.Parse("01/" + listaHistoricosDataInicio[x + 1].DataFimValidade?.AddMonths(1).Month.ToString() + "/" + listaHistoricosDataInicio[x + 1].DataFimValidade?.AddMonths(1).Year.ToString());
                                            if (proximaDataLista != listaHistoricosDataInicio[x].DataInicioValidade)
                                            {
                                                throw new PessoaAtuacaoBeneficioIntervalosVaziosEntreDatasException();
                                            }
                                        }
                                    }
                                }
                            }

                            if (verificarDataFim)
                            {
                                //DataFimVigencia esta em algum intervalo
                                foreach (var item in historicoValorAuxilio.OrderBy(x => x.DataInicioValidade))
                                {
                                    if (VereficarDataIntervalo((DateTime)pessoaAtuacaoBeneficio.DataFimVigencia, item.DataInicioValidade, item.DataFimValidade))
                                    {
                                        listaHistoricosDataFim.Add(item);
                                    }
                                }

                                if (listaHistoricosDataFim.Count == 0)
                                {
                                    throw new PessoaAtuacaoBeneficioNaoExisteIntervaloException();
                                }

                                listaHistoricosDataFim = new List<BeneficioHistoricoValorAuxilioVO>();

                                foreach (var item in historicoValorAuxilio.OrderBy(x => x.DataInicioValidade))
                                {
                                    if (item.DataInicioValidade > atualConfiguracaoBenefico.DataFimValidade || item.DataFimValidade == null)
                                    {
                                        if ((pessoaAtuacaoBeneficio.DataFimVigencia > item.DataFimValidade || item.DataFimValidade == null)
                                             && pessoaAtuacaoBeneficio.DataFimVigencia > item.DataInicioValidade)
                                        {
                                            listaHistoricosDataFim.Add(item);
                                            continue;
                                        }

                                        if (VereficarDataIntervalo((DateTime)pessoaAtuacaoBeneficio.DataFimVigencia, item.DataInicioValidade, item.DataFimValidade))
                                        {
                                            listaHistoricosDataFim.Add(item);
                                            break;
                                        }
                                    }
                                }

                                for (int x = 0; listaHistoricosDataFim.OrderBy(y => y.DataInicioValidade).Count() > x; x++)
                                {
                                    if (listaHistoricosDataFim.Count() > 1)
                                    {
                                        if (listaHistoricosDataFim.Count() >= (x + 2))
                                        {
                                            DateTime proximaDataLista;

                                            //Verifica se é nulo para assim se embasar para saber se ira pegar pela data pegar para montar a nova data
                                            if (listaHistoricosDataFim[x + 1].DataFimValidade != null)
                                            {
                                                //Verifica se a data da lista atual é igual a proxima data da lista caso não seja existe um buraco entre elas
                                                proximaDataLista = DateTime.Parse("01/" + listaHistoricosDataFim[x + 1].DataFimValidade?.AddMonths(-1).Month.ToString() + "/" + listaHistoricosDataFim[x + 1].DataFimValidade?.AddMonths(-1).Year.ToString());
                                            }
                                            else
                                            {
                                                proximaDataLista = DateTime.Parse("01/" + listaHistoricosDataFim[x + 1].DataInicioValidade.AddMonths(-1).Month.ToString() + "/" + listaHistoricosDataFim[x + 1].DataInicioValidade.AddMonths(-1).Year.ToString());
                                            }

                                            if (proximaDataLista != listaHistoricosDataFim[x].DataInicioValidade)
                                            {
                                                throw new PessoaAtuacaoBeneficioIntervalosVaziosEntreDatasException();
                                            }
                                        }
                                    }
                                }
                            }

                            //Validar se data do valor auxilio esta no intervalo com a data da configuração
                            if (verificarDataInicio)
                            {
                                var mesDataFim = (listaHistoricosDataInicio.OrderByDescending(x => x.DataInicioValidade).FirstOrDefault().DataFimValidade != null
                                                  ? listaHistoricosDataInicio.OrderByDescending(x => x.DataInicioValidade).FirstOrDefault().DataFimValidade?.AddMonths(-1) : null);

                                if (mesDataFim != null)
                                {
                                    if (mesDataFim < atualConfiguracaoBenefico.DataInicioValidade.AddMonths(-2)) ;
                                    {
                                        throw new PessoaAtuacaoBeneficioIntervalosVaziosEntreDatasException();
                                    }
                                }
                            }

                            //Validar se data do valor auxilio esta no intervalo com a data da configuração
                            if (verificarDataFim)
                            {
                                var mesDataInicio = listaHistoricosDataFim.OrderBy(x => x.DataInicioValidade).FirstOrDefault().DataInicioValidade.AddMonths(1);

                                if (atualConfiguracaoBenefico.DataFimValidade != null)
                                {
                                    if ((mesDataInicio) > atualConfiguracaoBenefico.DataFimValidade?.AddMonths(2))
                                    {
                                        throw new PessoaAtuacaoBeneficioIntervalosVaziosEntreDatasException();
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        //Valida caso não exista valor auxilio se a data do usuario está comprendida na configuração
                        bool validaConfiguracao = false;
                        foreach (var item in configuracaoBeneficio)
                        {
                            if ((pessoaAtuacaoBeneficio.DataInicioVigencia >= item.DataInicioValidade && (pessoaAtuacaoBeneficio.DataFimVigencia <= item.DataFimValidade || item.DataFimValidade == null)) &&
                                 (pessoaAtuacaoBeneficio.DataFimVigencia >= item.DataInicioValidade && (pessoaAtuacaoBeneficio.DataFimVigencia <= item.DataFimValidade || item.DataFimValidade == null)))
                            {
                                validaConfiguracao = true;
                            }
                        }

                        if (!validaConfiguracao)
                        {
                            throw new PessoaAtuacaoBeneficioForaPeriodoSelecionadoException();
                        }
                    }
                }
                else
                {
                    throw new PessoaBeneficioAtuacaoDeducaoValorException();
                }
            }
            else
            {
                var specHistoricoValorAuxilio = new BeneficioHistoricoValorAuxilioFilterSpecification { SeqBeneficio = pessoaAtuacaoBeneficio.SeqBeneficio, SeqNivelEnsino = nivelEnsino };
                var historicoValorAuxilio = BeneficioHistoticoValorAuxilioDomainService.BuscarDadosValoresAuxilio(specHistoricoValorAuxilio);

                if (historicoValorAuxilio.Count() > 0)
                {
                    List<BeneficioHistoricoValorAuxilioVO> listaHistoricosDataInicio = new List<BeneficioHistoricoValorAuxilioVO>();
                    List<BeneficioHistoricoValorAuxilioVO> listaHistoricosDataFim = new List<BeneficioHistoricoValorAuxilioVO>();

                    //Verificar se a data inicio esta em alguma configuração
                    foreach (var item in historicoValorAuxilio.OrderBy(x => x.DataInicioValidade))
                    {
                        if (VereficarDataIntervalo(pessoaAtuacaoBeneficio.DataInicioVigencia, item.DataInicioValidade, item.DataFimValidade))
                        {
                            listaHistoricosDataInicio.Add(item);
                        }
                    }

                    //Valida um valor beneficio da data inicio
                    if (listaHistoricosDataInicio.Count == 0)
                    {
                        throw new PessoaAtuacaoBeneficioNaoExisteIntervaloException();
                    }

                    listaHistoricosDataInicio = new List<BeneficioHistoricoValorAuxilioVO>();

                    //Verifica se as data do beneficio fazem parte do inicio da vigencia
                    foreach (var item in historicoValorAuxilio.OrderBy(x => x.DataInicioValidade))
                    {
                        if (VereficarDataIntervalo(pessoaAtuacaoBeneficio.DataInicioVigencia, item.DataInicioValidade, item.DataFimValidade))
                        {
                            listaHistoricosDataInicio.Add(item);
                            break;
                        }
                    }

                    //Verifica se a data fim esta em alugm beneficio
                    foreach (var item in historicoValorAuxilio.OrderBy(x => x.DataInicioValidade))
                    {
                        if (VereficarDataIntervalo((DateTime)pessoaAtuacaoBeneficio.DataFimVigencia, item.DataInicioValidade, item.DataFimValidade))
                        {
                            listaHistoricosDataFim.Add(item);
                        }
                    }

                    //Valida a um valor data fim beneficio
                    if (listaHistoricosDataFim.Count == 0)
                    {
                        throw new PessoaAtuacaoBeneficioNaoExisteIntervaloException();
                    }

                    //Verifica se os limites se encontram no mesmo valor auxilio caso não ele ira conferir o intervalo
                    if (listaHistoricosDataInicio.FirstOrDefault().DataInicioValidade != listaHistoricosDataFim.FirstOrDefault().DataInicioValidade)
                    {
                        listaHistoricosDataFim = new List<BeneficioHistoricoValorAuxilioVO>();

                        //Verifica se as data do beneficio fazem parte do fim da vigencia
                        foreach (var item in historicoValorAuxilio.OrderBy(x => x.DataInicioValidade))
                        {
                            if (VereficarDataIntervalo((DateTime)pessoaAtuacaoBeneficio.DataFimVigencia, item.DataInicioValidade, item.DataFimValidade))
                            {
                                if (item.DataInicioValidade != pessoaAtuacaoBeneficio.DataInicioVigencia)
                                {
                                    listaHistoricosDataFim.Add(item);
                                    break;
                                }
                            }

                            if (item.DataInicioValidade != pessoaAtuacaoBeneficio.DataInicioVigencia && item.DataInicioValidade > listaHistoricosDataInicio.FirstOrDefault().DataInicioValidade)
                            {
                                listaHistoricosDataFim.Add(item);
                            }
                        }

                        listaHistoricosDataInicio.AddRange(listaHistoricosDataFim);

                        //Valida se existe algum intervalo entre os valores auxilio
                        for (int x = 0; listaHistoricosDataInicio.OrderByDescending(y => y.DataInicioValidade).Count() > x; x++)
                        {
                            if (listaHistoricosDataInicio.Count() > 1)
                            {
                                if (listaHistoricosDataInicio.Count() >= (x + 2))
                                {
                                    //Verifica se a data da lista atual é igual a proxima data da lista caso não seja existe um buraco entre elas
                                    var proximaDataLista = DateTime.Parse("01/" + listaHistoricosDataInicio[x + 1].DataFimValidade?.AddMonths(-1).Month.ToString() + "/" + listaHistoricosDataInicio[x + 1].DataFimValidade?.AddMonths(-1).Year.ToString());
                                    if (proximaDataLista != listaHistoricosDataInicio[x].DataInicioValidade)
                                    {
                                        throw new PessoaAtuacaoBeneficioIntervalosVaziosEntreDatasException();
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    throw new PessoaAtuacaoBeneficioNaoDeducaoException();
                }
            }
        }

        /// <summary>
        /// Validar se determinada data esta dentro do das datas do periodo letivo
        /// </summary>
        /// <param name="seqCicloLetivo">Sequencial do ciclo letivo</param>
        /// <param name="seqCursoOfertaLocalidadeTurno">Sequencial do curso oferta localidade turno</param>
        /// <param name="data">Data a se validada</param>
        /// <returns>True ou false se a data esta dentro do periodo letivo</returns>
        private bool ValidarDataPeriodoLetivoPorCiclo(long seqCicloLetivo, long seqCursoOfertaLocalidadeTurno, DateTime data)
        {
            var retorno = true;

            var dataPeriodoLetivo = this.ConfiguracaoEventoLetivoDomainService.BuscarDatasEventoLetivo(seqCicloLetivo, seqCursoOfertaLocalidadeTurno, null, TOKEN_TIPO_EVENTO.PERIODO_CICLO_LETIVO);

            if (data < dataPeriodoLetivo.DataInicio || data > dataPeriodoLetivo.DataFim)
            {
                retorno = false;
            }

            return retorno;
        }

        public List<SMCDatasourceItem> BuscarsBeneficiosSelect(long seqPessoaAtuacao)
        {
            var ingressante = PessoaAtuacaoDomainService.BuscarInstituicaoNivelEnsinoESequenciaisPorPessoaAtuacao(seqPessoaAtuacao);

            var beneficios = BeneficioDomainService.BuscarBeneficioPorNivelEnsinoSelect(ingressante.SeqNivelEnsino);

            List<SMCDatasourceItem> retorno = new List<SMCDatasourceItem>();

            foreach (var item in beneficios)
            {
                SMCDatasourceItem novoItem = new SMCDatasourceItem();

                novoItem.Seq = item.Seq;

                novoItem.Descricao = item.Descricao;

                retorno.Add(novoItem);
            }

            return retorno;
        }

        public List<SMCDatasourceItem> BuscarConfiguracoesBeneficiosSelect(long seqBeneficio, long seqPessoaAtuacao)
        {
            var nivelEnsino = IngressanteDomainService.SearchProjectionByKey(new IngressanteFilterSpecification() { Seq = seqPessoaAtuacao }, x => x.NivelEnsino.Seq);
            if (nivelEnsino == 0)
            {
                nivelEnsino = AlunoDomainService.SearchProjectionByKey(new SMCSeqSpecification<Aluno>(seqPessoaAtuacao), p => p.Historicos.FirstOrDefault(f => f.Atual).SeqNivelEnsino);
            }

            var spec = new InstituicaoNivelBeneficioFilterSpecification() { SeqNivelEnsino = nivelEnsino, SeqBeneficio = seqBeneficio };
            spec.SetOrderByDescending(s => s.ConfiguracoesBeneficio.FirstOrDefault().DataInicioValidade);

            var configuracoesBeneficio = InstituicaoNivelBeneficioDomainService.SearchProjectionByKey(spec, x => x.ConfiguracoesBeneficio);

            List<SMCDatasourceItem> retorno = new List<SMCDatasourceItem>();

            if (configuracoesBeneficio != null)
            {
                foreach (var item in configuracoesBeneficio)
                {
                    SMCDatasourceItem novoItem = new SMCDatasourceItem();

                    novoItem.Seq = item.Seq;

                    if (item.DataFimValidade != null)
                    {
                        novoItem.Descricao = item.DataInicioValidade.ToString("dd/MM/yyyy") + " - " + item.DataFimValidade?.ToString("dd/MM/yyyy");
                    }
                    else
                    {
                        novoItem.Descricao = item.DataInicioValidade.ToString("dd/MM/yyyy") + " - Data fim não informada";
                    }

                    retorno.Add(novoItem);
                }
            }

            return retorno;
        }

        public TipoDeducao BuscarTipoDeducao(long seqConfiguracaoBeneficio)
        {
            var retorno = ConfiguracaoBeneficioDomainService.SearchByKey(new SMCSeqSpecification<ConfiguracaoBeneficio>(seqConfiguracaoBeneficio));

            return retorno.TipoDeducao;
        }

        public FormaDeducao BuscarFormaDeducao(long seqConfiguracaoBeneficio)
        {
            var retorno = ConfiguracaoBeneficioDomainService.SearchByKey(new SMCSeqSpecification<ConfiguracaoBeneficio>(seqConfiguracaoBeneficio));

            return retorno.FormaDeducao ?? FormaDeducao.Nenhum;
        }

        public decimal BuscarValorConfiguracaoBeneficio(long seqConfiguracaoBeneficio)
        {
            var retorno = ConfiguracaoBeneficioDomainService.SearchByKey(new SMCSeqSpecification<ConfiguracaoBeneficio>(seqConfiguracaoBeneficio));

            return retorno.ValorDeducao ?? 0;
        }

        public AssociarResponsavelFinanceiro BuscarIdAssociarResponsavelFinanceiro(long seqBeneficio)
        {
            var retorno = BeneficioDomainService.SearchByKey(new SMCSeqSpecification<Beneficio>(seqBeneficio));

            return retorno.AssociarResponsavelFinanceiro;
        }

        public bool BuscarIdDeducaoValorParcelaTitular(long seqBeneficio)
        {
            var retorno = BeneficioDomainService.SearchByKey(new SMCSeqSpecification<Beneficio>(seqBeneficio));

            return retorno.DeducaoValorParcelaTitular;
        }

        /// <summary>
        /// Buscar dados para altera pessoa atuação benefício
        /// </summary>
        /// <param name="seq">Sequencial da pessoa atuação benefício</param>
        /// <returns>Dados da pessoa atuação benefício</returns>
        public PessoaAtuacaoBeneficioVO AlterarPessoaAtuacaoBeneficio(long seq)
        {
            var includes = IncludesPessoaAtuacaoBeneficio.PessoaAtuacao
             | IncludesPessoaAtuacaoBeneficio.PessoaAtuacao_DadosPessoais
             | IncludesPessoaAtuacaoBeneficio.PessoaAtuacao_Pessoa
             | IncludesPessoaAtuacaoBeneficio.Beneficio
             | IncludesPessoaAtuacaoBeneficio.ConfiguracaoBeneficio
             | IncludesPessoaAtuacaoBeneficio.ResponsaveisFinanceiro
             | IncludesPessoaAtuacaoBeneficio.ResponsaveisFinanceiro_PessoaJuridica
             | IncludesPessoaAtuacaoBeneficio.ControlesFinanceiros
             | IncludesPessoaAtuacaoBeneficio.HistoricoSituacoes
             | IncludesPessoaAtuacaoBeneficio.ArquivosAnexo_ArquivoAnexado
             | IncludesPessoaAtuacaoBeneficio.HistoricoVigencias;

            var pessoaAtuacaoBeneficio = this.SearchByKey(new SMCSeqSpecification<PessoaAtuacaoBeneficio>(seq), includes);




            var pessoaAtuacaoBeneficioVO = pessoaAtuacaoBeneficio.Transform<PessoaAtuacaoBeneficioVO>();

            foreach (var itemAnexo in pessoaAtuacaoBeneficioVO.ArquivosAnexo)
            {
                if (itemAnexo.ArquivoAnexado != null)
                {
                    var arquivoAnexadoOrigem = pessoaAtuacaoBeneficio.ArquivosAnexo.FirstOrDefault(a => a.SeqArquivoAnexado == itemAnexo.SeqArquivoAnexado).ArquivoAnexado;
                    itemAnexo.ArquivoAnexado.GuidFile = arquivoAnexadoOrigem.UidArquivo.ToString();
                }
            }

            var ingressanteSituacao = IngressanteDomainService.SearchProjectionByKey(new IngressanteFilterSpecification { Seq = pessoaAtuacaoBeneficio.SeqPessoaAtuacao },
                                                                            p => p.HistoricosSituacao.OrderByDescending(o => o.Seq).FirstOrDefault().SituacaoIngressante);
            /* NV24
               No Histórico de Situação, todos os campos deverão ser exibidos como somente leitura.E as situações deverão ser
               exibidas em ordem decrescente pela data de início(mais atual listado primeiramente). */
            pessoaAtuacaoBeneficioVO.HistoricoSituacoes = pessoaAtuacaoBeneficioVO.HistoricoSituacoes.OrderByDescending(o => o.DataInicioSituacao).ToList();

            //Buscar Data fim da vigencia
            pessoaAtuacaoBeneficioVO.DataInicioVigencia = pessoaAtuacaoBeneficioVO.HistoricoVigencias.FirstOrDefault(f => f.Atual).DataInicioVigencia;
            pessoaAtuacaoBeneficioVO.DataFimVigencia = pessoaAtuacaoBeneficioVO.HistoricoVigencias.FirstOrDefault(f => f.Atual).DataFimVigencia;

            pessoaAtuacaoBeneficioVO.IncideParcelaMatriculaBanco = pessoaAtuacaoBeneficio.IncideParcelaMatricula;

            if (pessoaAtuacaoBeneficio.IncideParcelaMatricula && pessoaAtuacaoBeneficio.PessoaAtuacao.TipoAtuacao == TipoAtuacao.Ingressante)
            {
                pessoaAtuacaoBeneficioVO.DataInicioVigencia = this.BuscarDataAdmissaoIngressante(pessoaAtuacaoBeneficio.SeqPessoaAtuacao);
            }

            pessoaAtuacaoBeneficioVO.IdAssociarResponsavelFinanceiro = (int)BuscarIdAssociarResponsavelFinanceiro(pessoaAtuacaoBeneficio.SeqBeneficio);
            pessoaAtuacaoBeneficioVO.IdDeducaoValorParcelaTitular = BuscarIdDeducaoValorParcelaTitular(pessoaAtuacaoBeneficio.SeqBeneficio);
            pessoaAtuacaoBeneficioVO.IdFormaDeducao = (pessoaAtuacaoBeneficio.SeqConfiguracaoBeneficio != null ? (int)BuscarFormaDeducao((long)pessoaAtuacaoBeneficio.SeqConfiguracaoBeneficio) : 0);
            pessoaAtuacaoBeneficioVO.IdTipoDeducao = (pessoaAtuacaoBeneficio.SeqConfiguracaoBeneficio != null ? (int)BuscarTipoDeducao((long)pessoaAtuacaoBeneficio.SeqConfiguracaoBeneficio) : 0);
            pessoaAtuacaoBeneficioVO.IdIncideParcelaMatricula = (BuscarConfiguracoesBeneficiosSelect(pessoaAtuacaoBeneficio.SeqBeneficio, pessoaAtuacaoBeneficio.SeqPessoaAtuacao).Count() > 0) ? true : false;
            pessoaAtuacaoBeneficioVO.ExisteConfiguracaoBeneficio = pessoaAtuacaoBeneficioVO.IdIncideParcelaMatricula.GetValueOrDefault();

            //Veririficar se existe pessoa atuação
            var existeResponsaveis = BeneficioDomainService.BuscarBeneficio(pessoaAtuacaoBeneficio.SeqBeneficio);
            pessoaAtuacaoBeneficioVO.IdExisteResponsaveisFinanceiros = existeResponsaveis.ResponsaveisFinanceiros.Count() > 0 ? true : false;

            pessoaAtuacaoBeneficioVO.Aluno = ingressanteSituacao == SituacaoIngressante.Nenhum;

            /*NV19 - Exibir os contratos financeiros associados ao benefício que não possuem exclusão lógica.
            Os contratos deverão ser organizados em ordem decrescente, considerando a data de início.*/
            pessoaAtuacaoBeneficioVO.ControlesFinanceiros = pessoaAtuacaoBeneficioVO.ControlesFinanceiros.OrderByDescending(o => o.DataInicio).Where(w => w.DataExclusao == null).ToList();

            //Buscar a situação mais atual do historico
            pessoaAtuacaoBeneficioVO.SeqSituacaoChancelaBeneficioAtual = (int)this.BeneficioHistoricoSituacaoDomainService.BuscarHistoricoSituacaoChancelaBeneficioAtual(seq);

            pessoaAtuacaoBeneficioVO.SituacoesChancelaBeneficio = this.RetornarSituacaoChancelaBeneficioSelect(false, this.BeneficioHistoricoSituacaoDomainService.BuscarHistoricoSituacaoChancelaBeneficioAtual(seq));

            //Busca a justificativa atual
            pessoaAtuacaoBeneficioVO.Justificativa = pessoaAtuacaoBeneficio.HistoricoSituacoes.FirstOrDefault(f => f.Atual).Observacao;

            if (pessoaAtuacaoBeneficio.Beneficio != null)
            {
                var tipoBeneficio = TipoBeneficioDomainService.SearchByKey(new SMCSeqSpecification<TipoBeneficio>(pessoaAtuacaoBeneficio.Beneficio.SeqTipoBeneficio));
                if (tipoBeneficio != null)
                    pessoaAtuacaoBeneficioVO.ParametroSetorBolsa = tipoBeneficio.ChancelaSetorBolsas;
            }

            return pessoaAtuacaoBeneficioVO;
        }

        /// <summary>
        /// Consuta de dados da pessoa atuação benefício
        /// </summary>
        /// <param name="seq">Sequencial pessoa atuação benefício</param>
        /// <returns>Dados para exibir dados</returns>
        public PessoaAtuacaoBeneficioVO ConsultarPessoaAtuacaoBeneficio(long seq)
        {
            var pessoaAtuacaoBeneficio = this.SearchProjectionByKey(new SMCSeqSpecification<PessoaAtuacaoBeneficio>(seq), p => new
            {
                //Propriedade primitivas
                Seq = p.Seq,
                SeqPessoaAtuacao = p.SeqPessoaAtuacao,
                Beneficio = p.Beneficio,
                ConfiguracaoBeneficio = p.ConfiguracaoBeneficio,
                FormaDeducao = p.FormaDeducao,
                ValorBeneficio = p.ValorBeneficio,
                TipoAtuacao = p.PessoaAtuacao.TipoAtuacao,
                IncideParcelaMatricula = p.IncideParcelaMatricula,
                Nome = p.PessoaAtuacao.DadosPessoais.Nome,
                NomeSocial = p.PessoaAtuacao.DadosPessoais.NomeSocial,
                DeducaoValorParcelaTitular = p.Beneficio.DeducaoValorParcelaTitular,
                ExibeValoresTermoAdesao = p.ExibeValoresTermoAdesao,
                //Propriedades de navegação
                HistoricoSituacoes = p.HistoricoSituacoes,
                HistoricoVigencias = p.HistoricoVigencias.Select(s => new BeneficioHistoricoVigenciaVO
                {
                    Seq = s.Seq,
                    SeqPessoaAtuacaoBeneficio = s.SeqPessoaAtuacaoBeneficio,
                    SeqMotivoAlteracaoBeneficio = s.SeqMotivoAlteracaoBeneficio,
                    DataInicioVigencia = s.DataInicioVigencia,
                    DataFimVigencia = s.DataFimVigencia,
                    Observacao = s.Observacao,
                    DescricaoMotivoAlteracaoBeneficio = s.MotivoAlteracaoBeneficio.Descricao,
                    Atual = s.Atual,
                    DataInclusao = s.DataInclusao,
                    UsuarioInclusao = s.UsuarioInclusao
                }).OrderByDescending(d => d.DataInclusao).ToList(),
                ResponsaveisFinanceiro = p.ResponsaveisFinanceiro.Select(s => new PessoaAtuacaoBeneficioResponsavelFinanceiroVO
                {
                    ValorPercentual = s.ValorPercentual,
                    TipoResponsavelFinanceiro = s.TipoResponsavelFinanceiro,
                    Seq = s.Seq,
                    SeqPessoaJuridica = s.SeqPessoaJuridica,
                    SeqPessoaAtuacaoBeneficio = s.SeqPessoaAtuacaoBeneficio,
                    RazaoSocial = s.PessoaJuridica.RazaoSocial
                }),
                ControlesFinanceiros = p.ControlesFinanceiros,
                ArquivosAnexo = p.ArquivosAnexo.Select(s => new PessoaAtuacaoBeneficioAnexoVO
                {
                    Seq = s.Seq,
                    SeqArquivoAnexado = s.SeqArquivoAnexado,
                    UidArquivoAnexado = s.ArquivoAnexado.UidArquivo,
                    SeqPessoaAtuacaoBeneficio = s.SeqPessoaAtuacaoBeneficio,
                    ArquivoAnexado = new SMCUploadFile
                    {
                        GuidFile = s.ArquivoAnexado.UidArquivo.ToString(),
                        Name = s.ArquivoAnexado.Nome,
                        Size = s.ArquivoAnexado.Tamanho,
                        Type = s.ArquivoAnexado.Tipo
                    },
                    Observacao = s.Observacao,
                    DataInclusao = s.DataInclusao
                }).ToList()
            });

            PessoaAtuacaoBeneficioVO pessoaAtuacaoBeneficioVO = new PessoaAtuacaoBeneficioVO();

            pessoaAtuacaoBeneficioVO.Seq = pessoaAtuacaoBeneficio.Seq;
            pessoaAtuacaoBeneficioVO.SeqPessoaAtuacao = pessoaAtuacaoBeneficio.SeqPessoaAtuacao;
            pessoaAtuacaoBeneficioVO.SeqBeneficio = pessoaAtuacaoBeneficio.Beneficio.Seq;
            pessoaAtuacaoBeneficioVO.DescricaoBeneficio = pessoaAtuacaoBeneficio.Beneficio.Descricao;

            if (pessoaAtuacaoBeneficio.ConfiguracaoBeneficio != null)
            {
                pessoaAtuacaoBeneficioVO.DescricaoConfiguracaoBeneficio = pessoaAtuacaoBeneficio.ConfiguracaoBeneficio.DataInicioValidade.SMCDataAbreviada() + " - " + (pessoaAtuacaoBeneficio.ConfiguracaoBeneficio.DataFimValidade.HasValue ? pessoaAtuacaoBeneficio.ConfiguracaoBeneficio.DataFimValidade.SMCDataAbreviada() : "Data fim não informada");
                pessoaAtuacaoBeneficioVO.DescricaoTipoDeducao = pessoaAtuacaoBeneficio.ConfiguracaoBeneficio.TipoDeducao.SMCGetDescription();
                pessoaAtuacaoBeneficioVO.SeqConfiguracaoBeneficio = pessoaAtuacaoBeneficio.ConfiguracaoBeneficio.Seq;
            }

            pessoaAtuacaoBeneficioVO.DescricaoFormaDeducao = pessoaAtuacaoBeneficio.FormaDeducao.SMCGetDescription();
            pessoaAtuacaoBeneficioVO.DescricaoSituacaoBeneficio = pessoaAtuacaoBeneficio.HistoricoSituacoes.FirstOrDefault(f => f.Atual).SituacaoChancelaBeneficio.SMCGetDescription();
            pessoaAtuacaoBeneficioVO.ValorBeneficio = pessoaAtuacaoBeneficio.ValorBeneficio;
            pessoaAtuacaoBeneficioVO.DataInicioVigencia = pessoaAtuacaoBeneficio.HistoricoVigencias.FirstOrDefault(f => f.Atual).DataInicioVigencia;
            pessoaAtuacaoBeneficioVO.DataFimVigencia = pessoaAtuacaoBeneficio.HistoricoVigencias.FirstOrDefault(f => f.Atual).DataFimVigencia;
            pessoaAtuacaoBeneficioVO.Aluno = pessoaAtuacaoBeneficio.TipoAtuacao == TipoAtuacao.Aluno;
            pessoaAtuacaoBeneficioVO.IncideParcelaMatricula = pessoaAtuacaoBeneficio.IncideParcelaMatricula;
            pessoaAtuacaoBeneficioVO.IdDeducaoValorParcelaTitular = pessoaAtuacaoBeneficio.DeducaoValorParcelaTitular;
            pessoaAtuacaoBeneficioVO.HistoricoSituacoes = pessoaAtuacaoBeneficio.HistoricoSituacoes.TransformList<BeneficioHistoricoSituacaoVO>().OrderByDescending(o => o.DataInicioSituacao).ToList();
            pessoaAtuacaoBeneficioVO.ResponsaveisFinanceiro = pessoaAtuacaoBeneficio.ResponsaveisFinanceiro.TransformList<PessoaAtuacaoBeneficioResponsavelFinanceiroVO>();
            pessoaAtuacaoBeneficioVO.ControlesFinanceiros = pessoaAtuacaoBeneficio.ControlesFinanceiros.TransformList<BeneficioControleFinanceiroVO>().OrderByDescending(o => o.DataInicio).ToList();
            pessoaAtuacaoBeneficioVO.HistoricoVigencias = pessoaAtuacaoBeneficio.HistoricoVigencias;
            pessoaAtuacaoBeneficioVO.ArquivosAnexo = pessoaAtuacaoBeneficio.ArquivosAnexo;

            pessoaAtuacaoBeneficioVO.IdAssociarResponsavelFinanceiro = (int)BuscarIdAssociarResponsavelFinanceiro(pessoaAtuacaoBeneficio.Beneficio.Seq);

            //Buscar a situação mais atual do historico
            pessoaAtuacaoBeneficioVO.SeqSituacaoChancelaBeneficioAtual = (int)this.BeneficioHistoricoSituacaoDomainService.BuscarHistoricoSituacaoChancelaBeneficioAtual(seq);

            pessoaAtuacaoBeneficioVO.Nome = pessoaAtuacaoBeneficio.Nome;
            pessoaAtuacaoBeneficioVO.NomeSocial = pessoaAtuacaoBeneficio.NomeSocial;

            if (pessoaAtuacaoBeneficio.TipoAtuacao == TipoAtuacao.Aluno)
            {
                pessoaAtuacaoBeneficioVO.AlunoAtivo = AlunoHistoricoSituacaoDomainService.BuscarSituacaoAtualAluno(pessoaAtuacaoBeneficioVO.SeqPessoaAtuacao).VinculoAlunoAtivo.GetValueOrDefault();
            }
            else
            {
                pessoaAtuacaoBeneficioVO.SituacaoIngressante = IngressanteDomainService.SearchProjectionByKey(new IngressanteFilterSpecification { Seq = pessoaAtuacaoBeneficioVO.SeqPessoaAtuacao },
                                                                         p => (SituacaoIngressante?)p.HistoricosSituacao.OrderByDescending(o => o.Seq).FirstOrDefault().SituacaoIngressante);
            }

            pessoaAtuacaoBeneficioVO.ExibeValoresTermoAdesao = pessoaAtuacaoBeneficio.ExibeValoresTermoAdesao;

            return pessoaAtuacaoBeneficioVO;
        }

        /// <summary>
        /// Buscar a data que o igressante foi adimitido
        /// </summary>
        /// <param name="seqPessoaAtuacao">Sequencial da pessoa atuação</param>
        /// <returns>Data de admissão do ingressante</returns>
        public DateTime BuscarDataAdmissaoIngressante(long seqPessoaAtuacao)
        {
            var result = IngressanteDomainService.SearchByKey(new SMCSeqSpecification<Ingressante>(seqPessoaAtuacao));
            if (result == null)
            {
                result = AlunoDomainService.SearchProjectionByKey(new SMCSeqSpecification<Aluno>(seqPessoaAtuacao), p => p.Historicos.FirstOrDefault(f => f.Atual).Ingressante);
            }

            return new DateTime(result.DataAdmissao.Year, result.DataAdmissao.Month, 1);
        }

        /// <summary>
        /// Realiza as validações da regra RN_FIN_002 Consistência associação benefício (apenas associação)
        /// </summary>
        /// <param name="seqPessoaAtuacao">Sequencial da pessoa atuação</param>
        public PessoaAtuacaoBeneficioVO ValidarAssociacaoBeneficio(long seqPessoaAtuacao)
        {
            //RN_FIN_002 - Pessoa Atuação x Benefício - Consistência para habilitação dos comandos Novo e Alterar
            var ingressante = IngressanteDomainService.SearchProjectionByKey(new IngressanteFilterSpecification { Seq = seqPessoaAtuacao },
                                                                          p => (SituacaoIngressante?)p.HistoricosSituacao.OrderByDescending(o => o.Seq).FirstOrDefault().SituacaoIngressante);
            if (ingressante.HasValue)
            {
                /*
                Se a pessoa - atuação em questão for um Ingressante e, a situação do ingressante for igual a
                "Matriculado", "Desistente" ou "Cancelado".O botão Novo benefício deverá ser desabilitado com a
                seguinte mensagem informativa:
                */
                if (ingressante == SituacaoIngressante.Matriculado || ingressante == SituacaoIngressante.Desistente || ingressante == SituacaoIngressante.Cancelado)
                {
                    throw new PessoaAtuacaoBeneficioSituacaoException(ASSOCIACAO);
                }
            }
            //else //Remoção da regra conforme bug 38540
            //{
            //    /*
            //    Senão, se a pessoa - atuação for um Aluno e, a situação atual do aluno não possui vínculo ativo(ex:
            //    cancelado). O botão Novo benefício deverá ser desabilitado com a seguinte mensagem informativa:
            //    */
            //    var situacaoAluno = AlunoDomainService.BuscarSituacaoAtual(seqPessoaAtuacao);
            //    if (!situacaoAluno.VinculoAlunoAtivo.GetValueOrDefault())
            //    {
            //        throw new PessoaAtuacaoBeneficioAlunoSituacaoException(ASSOCIACAO);
            //    }
            //}

            var retorno = new PessoaAtuacaoBeneficioVO
            {
                SeqPessoaAtuacao = seqPessoaAtuacao,
                Aluno = AlunoDomainService.ValidarAtuacaoAluno(seqPessoaAtuacao),
                ControlesFinanceiros = new List<BeneficioControleFinanceiroVO>(),
                SituacoesChancelaBeneficio = this.RetornarSituacaoChancelaBeneficioSelect(true),
                SeqSituacaoChancelaBeneficioAtual = (int)SituacaoChancelaBeneficio.AguardandoChancela,
                IdAssociarResponsavelFinanceiro = (int)AssociarResponsavelFinanceiro.NaoPermite, //Inicializa de forma que o Masterdetail de responsaveis financeiros não sejam exibidos logo ao abrir a tela                
                ExibeValoresTermoAdesao = true
            };

            return retorno;
        }

        /// <summary>
        /// Busca simples da pessoa atuação e suas colections
        /// </summary>
        /// <param name="seqPessoaAtuacaoBeneficio">Sequencial pessoa atuação beneficio</param>
        /// <returns>Dados da pessoa atuação beneficio</returns>
        public PessoaAtuacaoBeneficio BuscarPessoaAtuacaoBeneficio(long seqPessoaAtuacaoBeneficio)
        {
            var includes = IncludesPessoaAtuacaoBeneficio.PessoaAtuacao
                         | IncludesPessoaAtuacaoBeneficio.PessoaAtuacao_DadosPessoais
                         | IncludesPessoaAtuacaoBeneficio.PessoaAtuacao_Pessoa
                         | IncludesPessoaAtuacaoBeneficio.Beneficio
                         | IncludesPessoaAtuacaoBeneficio.ConfiguracaoBeneficio
                         | IncludesPessoaAtuacaoBeneficio.ResponsaveisFinanceiro
                         | IncludesPessoaAtuacaoBeneficio.ResponsaveisFinanceiro_PessoaJuridica
                         | IncludesPessoaAtuacaoBeneficio.HistoricoSituacoes;

            var pessoaAtuacaoBeneficio = this.SearchByKey(new SMCSeqSpecification<PessoaAtuacaoBeneficio>(seqPessoaAtuacaoBeneficio), includes);

            if (seqPessoaAtuacaoBeneficio == 0)
            {
                pessoaAtuacaoBeneficio = new PessoaAtuacaoBeneficio();
            }

            return pessoaAtuacaoBeneficio;
        }

        /// <summary>
        /// Exlcuí beneficio de forma lógica
        /// </summary>
        /// <param name="pessoaAtuacaoBeneficioVO">Parametros beneficio</param>
        /// <param name="excluirSomenteContrato">Excluir somente os contratos, podendo ou não alterar a situação do beneficio</param>
        public void ExcluirPessoaAtuacaoBeneficio(PessoaAtuacaoBeneficioVO pessoaAtuacaoBeneficioVO, bool excluirSomenteContrato = false)
        {
            using (var unitOfWork = SMCUnitOfWork.Begin())
            {
                var dadosBeneficio = SearchProjectionByKey(new SMCSeqSpecification<PessoaAtuacaoBeneficio>(pessoaAtuacaoBeneficioVO.Seq), x => new
                {
                    SeqPessoaAtuacao = x.SeqPessoaAtuacao,
                    DataInicioVigencia = x.HistoricoVigencias.FirstOrDefault(h => h.Atual).DataInicioVigencia,
                    DataFimVigencia = x.HistoricoVigencias.FirstOrDefault(h => h.Atual).DataFimVigencia,
                    SeqBeneficioFinanceiro = x.Beneficio.SeqBeneficioFinanceiro,
                    PossuiContrato = x.ControlesFinanceiros.Any(a => a.DataExclusao == null), ///deverão ser considerados os contratos financeiros que não possuem data de exclusão.,
                    TipoAtuacao = x.PessoaAtuacao.TipoAtuacao,
                    SeqPessoa = x.PessoaAtuacao.SeqPessoa,
                    CodigoAlunoMigracao = (x.PessoaAtuacao as Aluno).CodigoAlunoMigracao,
                    IncluirDesbloqueioTemporario = x.Beneficio.IncluirDesbloqueioTemporario,
                    IncideParcelaMatricula = x.IncideParcelaMatricula
                });

                /*NV02 - Ao excluir um benefício associado à pessoa-atuação, verificar se existe contrato financeiro (desconsiderar os contratos
                que possuem exclusão lógica). Caso exista, acionar a regra de negócio: RN_FIN_018 - Pessoa Atuação x Benefício -
                Aciona rotina GRA para Exclusao do benefício da pessoa - atuacao*/
                if (dadosBeneficio.PossuiContrato)
                {
                    //Recupera os dados de origem da pessoa atuação, considerando a simulação em caso de não possuir curso
                    var dadosOrigem = PessoaAtuacaoDomainService.RecuperaDadosOrigem(dadosBeneficio.SeqPessoaAtuacao);

                    /*RN_FIN_018 - PessoaAtuação x Benefício - Aciona rotina GRA para Exclusao do benefício da pessoa-atuacao
                    A rotina abaixo deverá ser acionada para realizar a exclusão lógica dos contratos de benefício no GRA.
                    [O próprio GRA irá realizar a exclusão lógica dos registros da tabela FIN.beneficio_controle_financeiro
                    do acadêmico]
                    Acionar a rotina ST_EXCLUI_CONTRATO_BENEFICIO_ACADEMICO do GRA e enviar os seguintes
                    parâmetros:*/
                    ContratoBeneficioParametroData parametros = new ContratoBeneficioParametroData();

                    parametros.SeqBeneficio = dadosBeneficio.SeqBeneficioFinanceiro;

                    if (dadosBeneficio.TipoAtuacao == TipoAtuacao.Aluno)
                    {
                        parametros.DataInicioBeneficio = dadosBeneficio.DataInicioVigencia;
                        parametros.DataFimBeneficio = dadosBeneficio.DataFimVigencia;
                        parametros.CodigoAluno = dadosBeneficio.CodigoAlunoMigracao;
                        parametros.SeqIngressante = null;
                    }
                    else if (dadosBeneficio.TipoAtuacao == TipoAtuacao.Ingressante)
                    {
                        // Recupera as datas de inicio e fim do contrato conforme o escalonamento do ingressante
                        var dadosEscalonamento = SolicitacaoMatriculaDomainService.SearchProjectionByKey(new SolicitacaoMatriculaFilterSpecification()
                        {
                            SeqPessoaAtuacao = dadosBeneficio.SeqPessoaAtuacao,
                            TokensTiposServico = new[] { TOKEN_TIPO_SERVICO.MATRICULA_INGRESSANTE }
                        }, p => new
                        {
                            DataInicio = p.GrupoEscalonamento.Itens.Min(m => m.Escalonamento.DataInicio),
                            DataFim = p.GrupoEscalonamento.Itens.Max(m => m.Escalonamento.DataFim),
                        });
                        parametros.DataInicioBeneficio = dadosEscalonamento.DataInicio;
                        parametros.DataFimBeneficio = dadosEscalonamento.DataFim;
                        parametros.CodigoAluno = null;
                        parametros.SeqIngressante = dadosBeneficio.SeqPessoaAtuacao;
                    }

                    parametros.SeqOrigem = (int)dadosOrigem.SeqOrigem;
                    parametros.SeqServicoOrigem = dadosOrigem.CodigoServicoOrigem;
                    parametros.DataExclusao = DateTime.Now;
                    parametros.UsuarioOperacao = SMCContext.User.SMCGetNome();

                    if (dadosBeneficio.IncluirDesbloqueioTemporario
                        && (dadosBeneficio.IncideParcelaMatricula && dadosBeneficio.TipoAtuacao == TipoAtuacao.Ingressante)
                        || dadosBeneficio.TipoAtuacao == TipoAtuacao.Aluno
                        || (dadosBeneficio.IncluirDesbloqueioTemporario
                        && !dadosBeneficio.IncideParcelaMatricula
                        && dadosBeneficio.TipoAtuacao == TipoAtuacao.Ingressante))
                    {
                        var includePessoaAtuacaoBloqueio = IncludesPessoaAtuacaoBloqueio.SolicitacaoServico_ConfiguracaoProcesso_Processo_CicloLetivo;
                        var listaDesbloqueios = PessoaAtuacaoBloqueioDomainService
                                                                .SearchBySpecification(new PessoaAtuacaoBloqueioFilterSpecification()
                                                                {
                                                                    SeqPessoaAtuacao = dadosBeneficio.SeqPessoaAtuacao,
                                                                    SituacaoBloqueio = SituacaoBloqueio.Desbloqueado,
                                                                    TipoDesbloqueio = TipoDesbloqueio.Temporario,
                                                                    BloqueioMatricula = true
                                                                }, includePessoaAtuacaoBloqueio).ToList();

                        foreach (var item in listaDesbloqueios)
                        {
                            DatasEventoLetivoVO datasEventoLetivo = new DatasEventoLetivoVO();
                            if (dadosBeneficio.TipoAtuacao == TipoAtuacao.Aluno)
                            {
                                var includesPlanoEstudoItem = IncludesPlanoEstudoItem.PlanoEstudo_AlunoHistoricoCicloLetivo_AlunoHistorico_CursoOfertaLocalidadeTurno_CursoOfertaLocalidade;
                                var planoEstudoItem = this.PlanoEstudoItemDomainService.SearchBySpecification(new PlanoEstudoItemFilterSpecification() { SeqAluno = dadosBeneficio.SeqPessoaAtuacao, PlanoEstudoAtual = true }, includesPlanoEstudoItem).FirstOrDefault();

                                datasEventoLetivo = ConfiguracaoEventoLetivoDomainService.BuscarDatasEventoLetivo(
                                                    item.SolicitacaoServico.ConfiguracaoProcesso.Processo.CicloLetivo.Seq,
                                                    dadosOrigem.SeqCursoOfertaLocalidadeTurno,
                                                    planoEstudoItem.PlanoEstudo.AlunoHistoricoCicloLetivo.TipoAluno,
                                                    TOKEN_TIPO_EVENTO.PERIODO_CICLO_LETIVO);
                            }
                            else if (dadosBeneficio.TipoAtuacao == TipoAtuacao.Ingressante)
                            {
                                datasEventoLetivo = ConfiguracaoEventoLetivoDomainService.BuscarDatasEventoLetivo(
                                                    item.SolicitacaoServico.ConfiguracaoProcesso.Processo.CicloLetivo.Seq,
                                                    dadosOrigem.SeqCursoOfertaLocalidadeTurno,
                                                    TipoAluno.Calouro,
                                                    TOKEN_TIPO_EVENTO.PERIODO_CICLO_LETIVO);
                            }

                            if (dadosBeneficio.TipoAtuacao == TipoAtuacao.Ingressante)
                            {
                                item.SituacaoBloqueio = SituacaoBloqueio.Bloqueado;
                                item.TipoDesbloqueio = TipoDesbloqueio.Nenhum;
                                item.JustificativaDesbloqueio = string.Empty;
                                item.DataDesbloqueioTemporario = null;
                                item.UsuarioDesbloqueioTemporario = null;
                            }

                            if (dadosBeneficio.TipoAtuacao == TipoAtuacao.Aluno
                                && (datasEventoLetivo.DataInicio >= dadosBeneficio.DataInicioVigencia
                                || datasEventoLetivo.DataInicio <= dadosBeneficio.DataFimVigencia))
                            {
                                item.SituacaoBloqueio = SituacaoBloqueio.Bloqueado;
                                item.TipoDesbloqueio = TipoDesbloqueio.Nenhum;
                                item.JustificativaDesbloqueio = string.Empty;
                                item.DataDesbloqueioTemporario = null;
                                item.UsuarioDesbloqueioTemporario = null;
                            }
                            PessoaAtuacaoBloqueioDomainService.SaveEntity(item);
                        }
                    }

                    string result = this.IntegracaoFinanceiroService.ExcluiContratoBeneficio(parametros);

                    if (!string.IsNullOrEmpty(result))
                    {
                        throw new SMCApplicationException(result);
                    }

                    if (!excluirSomenteContrato)
                    {
                        this.SalvarBeneficioHistoricoSituacaoChancela(pessoaAtuacaoBeneficioVO.Seq, (int)SituacaoChancelaBeneficio.Excluido, pessoaAtuacaoBeneficioVO.Justificativa);
                    }
                }
                else
                {
                    if (!excluirSomenteContrato)
                    {
                        this.SalvarBeneficioHistoricoSituacaoChancela(pessoaAtuacaoBeneficioVO.Seq, (int)SituacaoChancelaBeneficio.Excluido, pessoaAtuacaoBeneficioVO.Justificativa);
                    }
                }

                unitOfWork.Commit();
            }
        }

        /// <summary>
        /// Verifica se datas estão em intervalo valido
        /// </summary>
        /// <param name="atual">Data corrente</param>
        /// <param name="dataInicio">Data inicio do benefcio</param>
        /// <param name="dataFim">Data fim do beneficio</param>
        /// <returns>Se itervalo é valido</returns>
        private bool VereficarDataIntervalo(DateTime? atual, DateTime dataInicio, DateTime? dataFim)
        {
            bool retorno = false;

            retorno = (atual >= dataInicio && (atual <= dataFim || dataFim == null));

            return retorno;
        }

        /// <summary>
        /// Atualização dos contatos da Pessoa Atuação Beneficio
        /// </summary>
        /// <param name="seqPessoaAtuacaoBeneficio">Sequencial da pessoa atuação beneficio</param>
        /// <param name="isPortal">Valida se a chamada foi do portal por padrão false</param>
        public void AtualizarContratoPessoaAtuacaoBeneficio(long seqPessoaAtuacaoBeneficio, bool isPortal = false)
        {
            ///[COMO A ROTINA DEVE SER ACIONADA DURANTE O DEFERIMENTO NO ADMINISTRATIVO]
            ///Os parâmetros da rotina deverão ser enviados, conforme RN_FIN_007 - Pessoa Atuação x Benefício -
            ///Parâmetros rotina GRA – Inclui contrato benefício acadêmico.
            var dadosBeneficio = SearchProjectionByKey(new SMCSeqSpecification<PessoaAtuacaoBeneficio>(seqPessoaAtuacaoBeneficio), x => new
            {
                SeqPessoaAtuacao = x.SeqPessoaAtuacao,
                DataInicioVigencia = x.HistoricoVigencias.FirstOrDefault(h => h.Atual).DataInicioVigencia,
                DataFimVigencia = x.HistoricoVigencias.FirstOrDefault(h => h.Atual).DataFimVigencia,
                SeqBeneficioFinanceiro = x.Beneficio.SeqBeneficioFinanceiro,
                RecebeCobranca = x.Beneficio.RecebeCobranca,
                JustificativaNaoRecebeCobranca = x.Beneficio.JustificativaNaoRecebeCobranca,
                PossuiContrato = x.ControlesFinanceiros.Any(a => a.DataExclusao == null), ///deverão ser considerados os contratos financeiros que não possuem data de exclusão.,
                FormaDeducao = x.ConfiguracaoBeneficio.FormaDeducao,
                ResponsaveisFinanceiro = x.ResponsaveisFinanceiro.Select(s => new
                {
                    s.Seq,
                    s.SeqPessoaAtuacaoBeneficio,
                    s.SeqPessoaJuridica,
                    s.TipoResponsavelFinanceiro,
                    s.ValorPercentual,
                    s.PessoaJuridica.Cnpj
                }),
                TipoAtuacao = x.PessoaAtuacao.TipoAtuacao,
                ValorBeneficio = x.ValorBeneficio,
                SeqPessoa = x.PessoaAtuacao.SeqPessoa,
                SeqInstituicaoEnsino = x.PessoaAtuacao.Pessoa.SeqInstituicaoEnsino,
                CodigoAlunoMigracao = (x.PessoaAtuacao as Aluno).CodigoAlunoMigracao,
                DataPrevisaoConclusaoPessoaAtuacao = (DateTime?)((x.PessoaAtuacao as Ingressante).DataPrevisaoConclusao ?? (x.PessoaAtuacao as Aluno).Historicos.FirstOrDefault(h => h.Atual).PrevisoesConclusao.OrderByDescending(p => p.Seq).FirstOrDefault().DataPrevisaoConclusao),
                IncideParcela = x.IncideParcelaMatricula
            });

            ///Se Ingressante e não existe[contrato financeiro] * associado ao benefício e, campo Incide na parcela
            ///de matrícula é igual a Sim.
            ///Senão, se Aluno e não existe [contrato financeiro]* associado ao benefício e há [benefício financeiro]*.
            if (!dadosBeneficio.PossuiContrato
                && ((dadosBeneficio.TipoAtuacao == TipoAtuacao.Ingressante && dadosBeneficio.IncideParcela && dadosBeneficio.SeqBeneficioFinanceiro != null)
                     || (dadosBeneficio.TipoAtuacao == TipoAtuacao.Aluno && dadosBeneficio.SeqBeneficioFinanceiro != null)))
            {
                // Recupera os dados de origem da pessoa atuação, considerando a simulação em caso de não possuir curso
                var dadosOrigem = PessoaAtuacaoDomainService.RecuperaDadosOrigem(dadosBeneficio.SeqPessoaAtuacao);

                //Verfica se a chamada foi feito pelo portal do ingressante pois se a chamda foi feita pelo portal as validações de combinação serão feitas antes de chamar a função
                if (!isPortal)
                {
                    //Validar chancela do beneficio conforme RN_FIN_015 - Pessoa Atuação x Benefício - Aciona rotina GRA para consistência de restrição de combinação de benefício
                    var respostaChancelaBeneficio = ValidarChancelaBeneficiosAdministrativo(dadosBeneficio.SeqPessoaAtuacao, dadosOrigem);
                    if (!string.IsNullOrEmpty(respostaChancelaBeneficio))
                    {
                        throw new PessoaAtuacaoBeneficioValidacaoChancelaException(respostaChancelaBeneficio);
                    }
                }

                // Cria o objeto para chamada ao GRA
                var objBenef = new ContratoBeneficioParametroData();

                //-Sequencial do benefício: sequencial do benefício no financeiro associado ao benefício que está associado ao ingressante em questão;
                objBenef.SeqBeneficio = dadosBeneficio.SeqBeneficioFinanceiro;

                // -Forma de dedução: domínio da forma de dedução da configuração de benefício associado ao ingressante em questão;
                objBenef.FormaDeducao = (short?)dadosBeneficio.FormaDeducao;

                // -Valor de dedução: valor de benefício associado ao ingressante em questão;
                objBenef.ValorDeducao = dadosBeneficio.ValorBeneficio;

                // -Data de Inicio do benefício associado ao ingressante em questão;
                objBenef.DataInicioBeneficio = dadosBeneficio.DataInicioVigencia;

                // Caso seja true, não deve ser informado o período de início e fim do benefício pois o GRA vai calcular internamente
                if (dadosBeneficio.TipoAtuacao == TipoAtuacao.Aluno)
                {
                    // -Data de Fim do benefício associado ao ingressante em questão;
                    objBenef.DataFimBeneficio = dadosBeneficio.DataFimVigencia;

                    objBenef.ConsiderarPeriodoMatriculaIngressante = false;

                    // TODO: Verificar campo
                    objBenef.CodigoAluno = dadosBeneficio.CodigoAlunoMigracao;

                    objBenef.SeqIngressante = null;
                }
                else if (dadosBeneficio.TipoAtuacao == TipoAtuacao.Ingressante)
                {
                    objBenef.ConsiderarPeriodoMatriculaIngressante = true;
                    objBenef.CodigoAluno = null;

                    // Seq_ingressante: sequencial da pessoa-atuação do ingressante
                    objBenef.SeqIngressante = dadosBeneficio.SeqPessoaAtuacao;

                    // Recupera as datas de inicio e fim do contrato conforme o escalonamento do ingressante
                    var dadosEscalonamento = SolicitacaoMatriculaDomainService.SearchProjectionByKey(new SolicitacaoMatriculaFilterSpecification()
                    {
                        SeqPessoa = dadosBeneficio.SeqPessoa,
                        TokensTiposServico = new[] { TOKEN_TIPO_SERVICO.MATRICULA_INGRESSANTE }
                    }, p => new
                    {
                        DataInicio = p.GrupoEscalonamento.Itens.OrderBy(i => i.Escalonamento.DataInicio).Select(i => i.Escalonamento.DataInicio).FirstOrDefault(),
                        DataFim = p.GrupoEscalonamento.Itens.OrderByDescending(i => i.Escalonamento.DataFim).Select(i => i.Escalonamento.DataFim).FirstOrDefault(),
                    });
                    objBenef.DataFimBeneficio = dadosEscalonamento.DataFim;
                }

                objBenef.SeqOrigem = (int)dadosOrigem.SeqOrigem;
                objBenef.SeqServicoOrigem = dadosOrigem.CodigoServicoOrigem;

                objBenef.RecebeCobranca = dadosBeneficio.RecebeCobranca;
                objBenef.ObservacaoCobranca = dadosBeneficio.JustificativaNaoRecebeCobranca;

                /*Lista de titular:
                Se o tipo de responsável financeiro for "Responsável Financeiro/Titular", preencher o Cod_pessoa_CAD e o percentual; Nesse caso, o campo CNPJ deverá ser NULO.
                Se o tipo de responsável financeiro for "Convênio//Parceiro", preencher o CNPJ e o percentual; Nesse caso, o campo Cod_pessoa_CAD deverá ser nulo*/
                objBenef.Titulares = new List<TitularesBeneficioData>();
                foreach (var item in dadosBeneficio.ResponsaveisFinanceiro)
                {
                    if (item.TipoResponsavelFinanceiro == TipoResponsavelFinanceiro.ResponsavelFinanceiroTitular)
                    {
                        objBenef.Titulares.Add(new TitularesBeneficioData()
                        {
                            CodigoPessoaCAD = PessoaDomainService.BuscarCodigoDePessoaNosDadosMestres(item.SeqPessoaJuridica, TipoPessoa.Juridica, null),
                            PercentualResponsavel = item.ValorPercentual,
                            CNPJ = null
                        });
                    }
                    else
                    {
                        objBenef.Titulares.Add(new TitularesBeneficioData()
                        {
                            CodigoPessoaCAD = null,
                            PercentualResponsavel = item.ValorPercentual,
                            CNPJ = item.Cnpj
                        });
                    }
                }

                //objBenef.Titulares = dadosBeneficio.ResponsaveisFinanceiro.Select(t => new TitularesBeneficioData
                //{
                //    CodigoPessoaCAD = PessoaDomainService.BuscarCodigoDePessoaNosDadosMestres(t.SeqPessoaJuridica, TipoPessoa.Juridica, null),
                //    PercentualResponsavel = t.ValorPercentual,
                //}).ToList();

                var ciclos = ConfiguracaoEventoLetivoDomainService.BuscarDatasEventosLetivoPorPeriodo(objBenef.DataInicioBeneficio.GetValueOrDefault(), objBenef.DataFimBeneficio, dadosOrigem.SeqCursoOfertaLocalidadeTurno, dadosOrigem.TipoAtuacao == TipoAtuacao.Ingressante ? TipoAluno.Calouro : TipoAluno.Veterano);
                objBenef.CiclosLetivos = ciclos.Select(c => new CicloLetivoData
                {
                    Ano = c.Ano,
                    DataFim = c.DataFim,
                    DataInicio = c.DataInicio,
                    Semestre = c.Numero
                }).ToList();

                ///Inclui o contrato benefício e recupera o sequencial dos contratos
                ///Os contratos financeiros retornados pela integração deverão ser associados ao benefício de acordo
                ///com RN_FIN_011 - Pessoa Atuação x Benefício - Inserção / Atualização dos Contratos Financeiros
                var contratos = IntegracaoFinanceiroService.IncluirContratoBeneficio(objBenef);
                if (contratos != null)
                {
                    foreach (var contrato in contratos)
                    {
                        if (contrato.SeqContratoBeneficio.HasValue)
                        {
                            var seqCicloLetivo = CicloLetivoDomainService.BuscarCicloLetivoPorAnoNumero(contrato.Ano, contrato.Numero, dadosBeneficio.SeqInstituicaoEnsino);

                            var beneficioControleFinanceiro = new BeneficioControleFinanceiro()
                            {
                                SeqPessoaAtuacaoBeneficio = seqPessoaAtuacaoBeneficio,
                                SeqContratoBeneficioFinanceiro = contrato.SeqContratoBeneficio.GetValueOrDefault(),
                                SeqCicloLetivo = seqCicloLetivo,
                                DataInicio = contrato.DataInicioValidade,
                                DataFim = contrato.DataFimValidade,
                                DataExclusao = null
                            };
                            BeneficioControleFinanceiroDomainService.SaveEntity(beneficioControleFinanceiro);
                        }
                    }
                }
            }

            ///Contemplação do beneficio em relação a conclusão de curso
            bool contemplaConclusaoCurso = this.BeneficioContemplaPrevisaoConclusaoCurso(dadosBeneficio.SeqPessoaAtuacao, dadosBeneficio.DataInicioVigencia, dadosBeneficio.DataFimVigencia, dadosBeneficio.DataPrevisaoConclusaoPessoaAtuacao);
            ///Se Ingressante e, o campo Incide na parcela de matrícula é igual a Não e, o benefício está
            ///parametrizado para não receber cobrança e, o período de vigência do benefício está[contemplado até
            ///o final de previsão de conclusão do curso]*.
            ///
            ///Senão se Aluno e, não há [benefício financeiro]* e, o benefício está parametrizado para não receber
            ///cobrança e, o período de vigência do benefício está[contemplado até o final de previsão de
            ///conclusão do curso]*.
            if (contemplaConclusaoCurso && ((dadosBeneficio.TipoAtuacao == TipoAtuacao.Ingressante && !dadosBeneficio.SeqBeneficioFinanceiro.HasValue && !dadosBeneficio.RecebeCobranca)
                                         || (dadosBeneficio.TipoAtuacao == TipoAtuacao.Aluno && !dadosBeneficio.SeqBeneficioFinanceiro.HasValue && !dadosBeneficio.RecebeCobranca)))
            {
                ///Recupera os dados de origem da pessoa atuação, considerando a simulação em caso de não possuir curso
                var dadosOrigem = PessoaAtuacaoDomainService.RecuperaDadosOrigem(dadosBeneficio.SeqPessoaAtuacao);

                ///Cria o objeto para chamada ao GRA
                var objBenef = new ContratoBeneficioParametroData();

                objBenef.DataInicioBeneficio = dadosBeneficio.DataInicioVigencia;

                if (dadosBeneficio.TipoAtuacao == TipoAtuacao.Aluno)
                {
                    objBenef.ConsiderarPeriodoMatriculaIngressante = false;

                    // -Data de Fim do benefício associado ao ingressante em questão;
                    objBenef.DataFimBeneficio = dadosBeneficio.DataFimVigencia;

                    // TODO: Verificar campo
                    objBenef.CodigoAluno = dadosBeneficio.CodigoAlunoMigracao;

                    objBenef.SeqIngressante = null;
                }
                else if (dadosBeneficio.TipoAtuacao == TipoAtuacao.Ingressante)
                {
                    objBenef.ConsiderarPeriodoMatriculaIngressante = true;

                    // Recupera as datas de inicio e fim do contrato conforme o escalonamento do ingressante
                    var dadosEscalonamento = SolicitacaoMatriculaDomainService.SearchProjectionByKey(new SolicitacaoMatriculaFilterSpecification()
                    {
                        SeqPessoa = dadosBeneficio.SeqPessoa,
                        TokensTiposServico = new[] { TOKEN_TIPO_SERVICO.MATRICULA_INGRESSANTE }
                    }, p => new
                    {
                        DataInicio = p.GrupoEscalonamento.Itens.OrderBy(i => i.Escalonamento.DataInicio).Select(i => i.Escalonamento.DataInicio).FirstOrDefault(),
                        DataFim = p.GrupoEscalonamento.Itens.OrderByDescending(i => i.Escalonamento.DataFim).Select(i => i.Escalonamento.DataFim).FirstOrDefault(),
                    });
                    objBenef.DataFimBeneficio = dadosEscalonamento.DataFim;

                    objBenef.CodigoAluno = null;

                    // Seq_ingressante: sequencial da pessoa-atuação do ingressante
                    objBenef.SeqIngressante = dadosBeneficio.SeqPessoaAtuacao;
                }

                objBenef.SeqOrigem = (int)dadosOrigem.SeqOrigem;
                objBenef.SeqServicoOrigem = dadosOrigem.CodigoServicoOrigem;

                objBenef.ObservacaoCobranca = dadosBeneficio.JustificativaNaoRecebeCobranca;

                if (dadosBeneficio.SeqBeneficioFinanceiro == null && !dadosBeneficio.RecebeCobranca)
                {
                    objBenef.SeqBeneficio = null;
                    objBenef.FormaDeducao = null;
                    objBenef.ValorDeducao = null;
                    objBenef.Titulares = null;
                    objBenef.RecebeCobranca = false;
                    objBenef.CiclosLetivos = null;
                }

                var retorno = IntegracaoFinanceiroService.IncluirContratoBeneficioIndividual(objBenef);
            }
        }

        /// <summary>
        ///[Contemplado até o final de previsão de conclusão do curso]* = é considerado que o benefício está
        ///contemplado até o final de previsão de conclusão do curso, quando a data de início do benefício for
        ///referente ao mesmo mês/ano da data de admissão da pessoa-atuação e, a data fim do benefício for
        ///referente ao mesmo mês/ano da previsão de conclusão de curso da pessoa-atuação.
        /// </summary>
        /// <param name="seqPessoaAtuacao">Sequencial da pessoa atuação</param>
        /// <param name="dtInicioBeneficio">Data inicio beneficio</param>
        /// <param name="dtFimBeneficio">Data final do beneficio</param>
        /// <param name="dtPrevisaoConclusao">Data previsão conclusão de curso da pessoa atuação</param>
        /// <returns></returns>
        public bool BeneficioContemplaPrevisaoConclusaoCurso(long seqPessoaAtuacao, DateTime dtInicioBeneficio, DateTime? dtFimBeneficio, DateTime? dtPrevisaoConclusao)
        {
            bool retorno = false;

            var dadosPessoaAtuacao = this.PessoaAtuacaoDomainService.SearchProjectionByKey(new SMCSeqSpecification<PessoaAtuacao>(seqPessoaAtuacao), p => new
            {
                DataAdmissao = (DateTime?)(p as Ingressante).DataAdmissao ?? (p as Aluno).Historicos.FirstOrDefault(f => f.Atual).DataAdmissao
            });

            ///Garantir que a data de admissao sempre estará no primeiro dia do mês
            var dataAdmissaoAjustada = new DateTime(dadosPessoaAtuacao.DataAdmissao.Year, dadosPessoaAtuacao.DataAdmissao.Month, 1);

            if (dtPrevisaoConclusao != null
               && (dtInicioBeneficio == dataAdmissaoAjustada && dtFimBeneficio == dtPrevisaoConclusao.Value.Date))
            {
                retorno = true;
            }

            return retorno;
        }

        /// <summary>
        /// Realiza a consulta de beneficios com data atual no período de vigência de acordo com a
        /// pessoa atuação na matrícula e renovação.
        /// Busca apenas benefícios que não estejam cancelados (data de cancelamento nula)
        /// </summary>
        /// <param name="seqPessoaAtuacao">Sequencial de pessoa atuação</param>
        /// <param name="situacao">Situação dos benefícios</param>
        /// <returns>Lista de benefícios vigêntes para a pessoa atuação</returns>
        public List<PessoaAtuacaoBeneficioMatriculaVO> BuscarPesssoasAtuacoesBeneficiosMatricula(long seqPessoaAtuacao, SituacaoChancelaBeneficio? situacao = null, DateTime? dataReferencia = null)
        {
            // Monta o specification
            var spec = new PessoaAtuacaoBeneficioFilterSpecification()
            {
                SeqPessoaAtuacao = seqPessoaAtuacao,
                DataReferenciaVigencia = dataReferencia.HasValue ? dataReferencia : DateTime.Now,
                RegistroAtivo = true,
                SituacaoChancelaBeneficio = situacao
            };

            // Busca os benefícios
            var result = this.SearchProjectionBySpecification(spec, p => new PessoaAtuacaoBeneficioMatriculaVO()
            {
                SeqPessoaAtuacaoBeneficio = p.Seq,
                SeqBeneficio = p.SeqBeneficio,
                DescricaoBeneficio = p.Beneficio.Descricao,
                DataInicioVigencia = p.HistoricoVigencias.FirstOrDefault(h => h.Atual).DataInicioVigencia,
                DataFimVigencia = p.HistoricoVigencias.FirstOrDefault(h => h.Atual).DataFimVigencia,
                TipoBeneficio = p.Beneficio.TipoBeneficio.Descricao,
                ValorBeneficio = p.ValorBeneficio,
                FormaDeducao = p.ConfiguracaoBeneficio.FormaDeducao,
                OrdenacaoBolsa = p.ConfiguracaoBeneficio.FormaDeducao == FormaDeducao.PercentualBolsa,
                ExibeValoresTermoAdesao = p.ExibeValoresTermoAdesao
            }).OrderBy(o => o.OrdenacaoBolsa).ThenBy(t => t.DescricaoBeneficio).ToList();

            // Retorna
            return result;
        }

        /// <summary>
        /// Realiza a consulta de beneficios com data atual no período de vigência de acordo com a
        /// pessoa atuação na matrícula e renovação.
        /// Busca apenas benefícios que não estejam cancelados (data de cancelamento nula)
        /// </summary>
        /// <param name="seqPessoaAtuacao">Sequencial de pessoa atuação</param>
        /// <param name="situacao">Situação dos benefícios</param>
        /// <returns>Lista de benefícios vigêntes para a pessoa atuação</returns>
        public List<PessoaAtuacaoBeneficioMatriculaVO> BuscarPesssoasAtuacoesBeneficiosIntegralizacao(long seqPessoaAtuacao, SituacaoChancelaBeneficio? situacao = null)
        {
            // Monta o specification
            var spec = new PessoaAtuacaoBeneficioFilterSpecification()
            {
                SeqPessoaAtuacao = seqPessoaAtuacao,
                RegistroAtivo = true,
                SituacaoChancelaBeneficio = situacao
            };

            // Busca os benefícios
            var result = this.SearchProjectionBySpecification(spec, p => new PessoaAtuacaoBeneficioMatriculaVO()
            {
                SeqPessoaAtuacaoBeneficio = p.Seq,
                SeqBeneficio = p.SeqBeneficio,
                DescricaoBeneficio = p.Beneficio.Descricao,
                DataInicioVigencia = p.HistoricoVigencias.FirstOrDefault(h => h.Atual).DataInicioVigencia,
                DataFimVigencia = p.HistoricoVigencias.FirstOrDefault(h => h.Atual).DataFimVigencia,
                TipoBeneficio = p.Beneficio.TipoBeneficio.Descricao,
                ValorBeneficio = p.ValorBeneficio,
                FormaDeducao = p.ConfiguracaoBeneficio.FormaDeducao,
                OrdenacaoBolsa = p.ConfiguracaoBeneficio.FormaDeducao == FormaDeducao.PercentualBolsa,
                ExibeValoresTermoAdesao = p.ExibeValoresTermoAdesao
            }).OrderBy(o => o.OrdenacaoBolsa).ThenBy(t => t.DescricaoBeneficio).ToList();

            // Retorna
            return result;
        }

        #region [ Relatório Bolsistas ]

        public List<RelatorioBolsistasVO> BuscarDadosRelatorioBolsistas(RelatorioBolsistasFiltroVO filtro)
        {
            // Executa a query e retorna a lista
            var result = this.RawQuery<RelatorioBolsistasVO>(QUERY_RELATORIO_ALUNO_BOLSISTA, new SMCFuncParameter[]
            {
                new SMCFuncParameter("SEQ_ENTIDADE_INSTITUICAO", filtro.SeqInstituicaoLogada),
                new SMCFuncParameter("LISTA_TIPO_ATUACAO", string.Join(",", filtro.SeqsTipoAtuacao)),
                new SMCFuncParameter("LISTA_ENTIDADE_RESPONSAVEL", filtro.SeqsEntidadesResponsaveis.SMCAny() ? string.Join(",", filtro.SeqsEntidadesResponsaveis) : null),
                new SMCFuncParameter("LISTA_BENEFICIO", filtro.SeqsBeneficios.SMCAny() ? string.Join(",", filtro.SeqsBeneficios) : null),
                new SMCFuncParameter("SITUACAO_BENEFICIO", filtro.SituacaoBeneficio != SituacaoChancelaBeneficio.Nenhum ? (short)filtro.SituacaoBeneficio : 0),
                new SMCFuncParameter("DAT_INICIO", filtro.DataInicioReferencia.HasValue ? filtro.DataInicioReferencia.Value.ToString("yyyy-MM-dd") : null),
                new SMCFuncParameter("DAT_FIM", filtro.DataFimReferencia.HasValue ? filtro.DataFimReferencia.Value.ToString("yyyy-MM-dd") : null),
                new SMCFuncParameter("SEQ_CICLO_LETIVO_INGRESSO", filtro.SeqCicloLetivoIngresso.HasValue ? filtro.SeqCicloLetivoIngresso.Value : 0),
                new SMCFuncParameter("SEQ_NIVEL_ENSINO", filtro.SeqNivelEnsino.HasValue ? filtro.SeqNivelEnsino.Value : 0)
            });

            result?.Where(e => !string.IsNullOrEmpty(e.CPF)).ToList().ForEach(e =>
            {
                e.CPF = SMCMask.ApplyMaskCPF(e.CPF);
            });

            //Chamadas de serviços financeiros
            IntegracaoFinanceiraBolsistas(filtro, result);

            ConcatenarResponsaveisFinanceiros(ref result);

            return result;
        }

        #region [ Métodos privados Relatório Bolsistas ]

        /// <summary>
        /// Chamadas de serviços financeiros
        /// </summary>
        /// <param name="filtro"></param>
        /// <param name="result"></param>
        private void IntegracaoFinanceiraBolsistas(RelatorioBolsistasFiltroVO filtro, List<RelatorioBolsistasVO> result)
        {
            /* Chamadas serviços financeiros
                   1) procedure: GRA..ST_RETORNA_CONTRATO_BENEFICIO_ACADEMICO = serviço: IntegracaoFinanceiroService.BuscarContratoBeneficio
                   2) procedure: GRA..ST_RETORNA_PARCELAS_ABERTAS_ACADEMICO = serviço: IntegracaoFinanceiroService.BuscarParcelasEmAberto (recebe como parâmetro um ParcelasEmAbertoFiltroData)
                   Para passagem dos parâmetros para os serviços, utilizar o método PessoaAtuacaoDomainService.RecuperaDadosOrigem.
                   Foi realizada alteração na query para retornar para o tipo de atuação aluno o CodigoAlunoMigracao, que é necessário para a chamada das rotinas financeiras (script anexo).*/

            // Validação para chamada de serviços financeiros de cada aluno
            if (result.SMCAny() && (filtro.ExibirParcelasEmAberto || filtro.ExibirReferenciaContrato))
            {
                // Verifico se o chekBox de Referência financeira e Parcelas em Aberto estão marcados
                if (filtro.ExibirParcelasEmAberto && filtro.ExibirReferenciaContrato)
                {
                    IntegrarReferenciaEParcelas(result);
                }
                // Verifico se apenas o checkbox de exibir parcelas foi marcado
                else if (filtro.ExibirParcelasEmAberto && !filtro.ExibirReferenciaContrato)
                {
                    IntegrarParcelas(result);
                }
                // Senão, apenas o checkBox de referência financeira está marcado
                else
                {
                    IntegrarReferencia(result);
                }
            }
        }

        private void IntegrarReferencia(List<RelatorioBolsistasVO> result)
        {
            /*NV10 A coluna "Referência Financeira" somente deverá ser exibida se o filtro "Exibir referência do contrato no
               sistema financeiro" estiver selecionado em UC_FIN_001_06_01 - Pesquisar Alunos com Benefício
               Utilizar a rotina do GRA abaixo para retornar as referencias financeiras de cada benefício a ser exibido no relatório:
               GRA..ST_RETORNA_CONTRATO_BENEFICIO_ACADEMICO
               Parametros:
               seq_pessoa_atuacao = se o tipo de atuação for aluno deverá ser enviado o cod_aluno_migracao.
               · Se o tipo de atuação for Ingressante deverá ser enviado o seq_pessoa_atuacao.
               · Cod_servico_origem = se o tipo de atuação for aluno deverá ser enviado o cod_curso_oferta do aluno no SGP.
               Se o tipo de atuação for Ingressante deverá ser enviado o seq_curso_oferta_localidade. Caso o ingressante não
               possua curso-oferta-localidade, simular qual seria o seu respectivo curso, conforme o nível de ensino e entidade
               responsável que ele possui e, tipo de oferta de curso cujo token seja “INTERNO”. Buscar apenas ofertas de curso
               ativas.
               · seq_beneficio = enviar o seq_benefício_financeiro correspondente
               · Dat_inicio_benefício = passar nulo
               · Dat_inicio_benefício = passar nulo
               OBS. Esse objeto retorna uma linha para cada referência encontrada*/
            foreach (var item in result)
            {
                var referenciafiltro = new ContratoBeneficioFiltroData();

                var dadosOrigem = PessoaAtuacaoDomainService.RecuperaDadosOrigem(item.SeqPessoaAtuacao);

                referenciafiltro.SeqBeneficio = item.SeqBeneficioFinanceiro;
                referenciafiltro.CodigoServicoOrigem = dadosOrigem.CodigoServicoOrigem;

                if (item.SeqTipoAtuacao == (short)TipoAtuacao.Aluno)
                {
                    referenciafiltro.SeqPessoaAtuacao = item.CodigoAlunoMigracao.GetValueOrDefault();
                }
                else if (item.SeqTipoAtuacao == (short)TipoAtuacao.Ingressante)
                {
                    referenciafiltro.SeqPessoaAtuacao = item.SeqPessoaAtuacao;
                }

                item.ReferenciaFinanceira = BuscarReferenciaFinanceira(referenciafiltro);
            }
        }

        private string BuscarReferenciaFinanceira(ContratoBeneficioFiltroData referenciafiltro)
        {
            string referencia = string.Empty;
            try
            {
                var referencias = IntegracaoFinanceiroService.BuscarContratoBeneficio(referenciafiltro)?.ToArray();

                if (referencias == null || !referencias.Any()) { return referencia; }

                referencia = ConcatenarRegistros(referencias, 2);
            }
            catch (Exception)
            {
                //ex.Message
                referencia = string.Empty;
            }
            return referencia;
        }

        /// <summary>
        /// Método que concatena um vetor de strings conforme a quantidade de registros por linha, antes de uma quebra de linha (HTML <br/>)
        /// </summary>
        /// <param name="referencias">Registros em texto para concatenação</param>
        /// <param name="registrosLinha">Número de registros antes da quebra de linha, no relatório</param>
        /// <returns></returns>
        private string ConcatenarRegistros(string[] referencias, int registrosLinha)
        {
            string referencia = string.Empty;
            string quebraDeLinhaHTML = "<br/>";

            for (int i = 0; i < referencias.Length; i++)
            {
                //Verifico se é o último registro e retorno a string completa e concatenada.
                if (i == referencias.Length - 1) { return referencia += $"{referencias[i]}"; }

                //Verifico se é Modulo de registrosLinha (de registrosLinha em registrosLinha é adicionado uma quebra de linha)
                if ((i + 1) % registrosLinha == 0)
                {
                    referencia += $"{referencias[i]}{quebraDeLinhaHTML}";
                }
                else
                {
                    //Concatenação com Divisor de registros
                    referencia += $"{referencias[i]} | ";
                }
            }
            return referencia;
        }

        private void IntegrarParcelas(List<RelatorioBolsistasVO> result)
        {
            foreach (var item in result)
            {
                var parcelasFiltro = new ParcelasEmAbertoFiltroData();

                var dadosOrigem = PessoaAtuacaoDomainService.RecuperaDadosOrigem(item.SeqPessoaAtuacao);

                parcelasFiltro.CodigoServicoOrigem = dadosOrigem.CodigoServicoOrigem;

                if (item.SeqTipoAtuacao == (short)TipoAtuacao.Aluno)
                {
                    parcelasFiltro.SeqOrigem = 1;
                    parcelasFiltro.SeqPessoaAtuacao = item.CodigoAlunoMigracao.GetValueOrDefault();
                }
                else if (item.SeqTipoAtuacao == (short)TipoAtuacao.Ingressante)
                {
                    parcelasFiltro.SeqOrigem = (int)dadosOrigem.SeqOrigem;
                    parcelasFiltro.SeqPessoaAtuacao = item.SeqPessoaAtuacao;
                }

                /*GRA..ST_RETORNA_PARCELAS_ABERTAS_ACADEMICO
                    Parametros:
                    · seq_origem = se o tipo de atuação for aluno deverá ser enviado o valor fixo "1". Se o tipo de atuação for
                    Ingressante deverá ser enviado o campo seq_origem_financeira definido no curso-oferta-localidade. Caso o
                    ingressante não possua curso-oferta-localidade, simular qual seria o seu respectivo curso, conforme o nível de
                    ensino e entidade responsável que ele possui e, tipo de oferta de curso cujo token seja “INTERNO”. Buscar
                    apenas ofertas de curso ativas.
                    · seq_pessoa_atuacao = se o tipo de atuação for aluno deverá ser enviado o cod_aluno_migracao. Se o tipo de
                    atuação for Ingressante deverá ser enviado o seq_pessoa_atuacao.
                    · Cod_servico_origem = se o tipo de atuação for aluno deverá ser enviado o cod_curso_oferta do aluno no SGP.
                    Se o tipo de atuação for Ingressante deverá ser enviado o seq_curso_oferta_localidade. Caso o ingressante não
                    possua curso-oferta-localidade, simular qual seria o seu respectivo curso, conforme o nível de ensino e entidade
                    responsável que ele possui e, tipo de oferta de curso cujo token seja “INTERNO”. Buscar apenas ofertas de curso
                    ativas.
                    OBS. Esse objeto retorna uma lista concatenada das parcelas em aberto do aluno/ingressante.*/

                item.ParcelasAbertas = BuscarParcelasEmAberto(parcelasFiltro);
            }
        }

        private void IntegrarReferenciaEParcelas(List<RelatorioBolsistasVO> result)
        {
            foreach (var item in result)
            {
                var referenciafiltro = new ContratoBeneficioFiltroData();

                var parcelasFiltro = new ParcelasEmAbertoFiltroData();

                var dadosOrigem = PessoaAtuacaoDomainService.RecuperaDadosOrigem(item.SeqPessoaAtuacao);

                referenciafiltro.SeqBeneficio = item.SeqBeneficioFinanceiro;
                referenciafiltro.CodigoServicoOrigem = dadosOrigem.CodigoServicoOrigem;
                parcelasFiltro.CodigoServicoOrigem = dadosOrigem.CodigoServicoOrigem;

                if (item.SeqTipoAtuacao == (short)TipoAtuacao.Aluno)
                {
                    referenciafiltro.SeqPessoaAtuacao = item.CodigoAlunoMigracao.GetValueOrDefault();
                    parcelasFiltro.SeqOrigem = 1;
                    parcelasFiltro.SeqPessoaAtuacao = item.CodigoAlunoMigracao.GetValueOrDefault();
                }
                else if (item.SeqTipoAtuacao == (short)TipoAtuacao.Ingressante)
                {
                    referenciafiltro.SeqPessoaAtuacao = item.SeqPessoaAtuacao;
                    parcelasFiltro.SeqOrigem = (int)dadosOrigem.SeqOrigem;
                    parcelasFiltro.SeqPessoaAtuacao = item.SeqPessoaAtuacao;
                }

                /*NV10 A coluna "Referência Financeira" somente deverá ser exibida se o filtro "Exibir referência do contrato no
                   sistema financeiro" estiver selecionado em UC_FIN_001_06_01 - Pesquisar Alunos com Benefício
                   Utilizar a rotina do GRA abaixo para retornar as referencias financeiras de cada benefício a ser exibido no relatório:
                   GRA..ST_RETORNA_CONTRATO_BENEFICIO_ACADEMICO
                   Parametros:
                   seq_pessoa_atuacao = se o tipo de atuação for aluno deverá ser enviado o cod_aluno_migracao.
                   · Se o tipo de atuação for Ingressante deverá ser enviado o seq_pessoa_atuacao.
                   · Cod_servico_origem = se o tipo de atuação for aluno deverá ser enviado o cod_curso_oferta do aluno no SGP.
                   Se o tipo de atuação for Ingressante deverá ser enviado o seq_curso_oferta_localidade. Caso o ingressante não
                   possua curso-oferta-localidade, simular qual seria o seu respectivo curso, conforme o nível de ensino e entidade
                   responsável que ele possui e, tipo de oferta de curso cujo token seja “INTERNO”. Buscar apenas ofertas de curso
                   ativas.
                   · seq_beneficio = enviar o seq_benefício_financeiro correspondente
                   · Dat_inicio_benefício = passar nulo
                   · Dat_inicio_benefício = passar nulo
                   OBS. Esse objeto retorna uma linha para cada referência encontrada*/
                item.ReferenciaFinanceira = BuscarReferenciaFinanceira(referenciafiltro);

                /*GRA..ST_RETORNA_PARCELAS_ABERTAS_ACADEMICO
                    Parametros:
                    · seq_origem = se o tipo de atuação for aluno deverá ser enviado o valor fixo "1". Se o tipo de atuação for
                    Ingressante deverá ser enviado o campo seq_origem_financeira definido no curso-oferta-localidade. Caso o
                    ingressante não possua curso-oferta-localidade, simular qual seria o seu respectivo curso, conforme o nível de
                    ensino e entidade responsável que ele possui e, tipo de oferta de curso cujo token seja “INTERNO”. Buscar
                    apenas ofertas de curso ativas.
                    · seq_pessoa_atuacao = se o tipo de atuação for aluno deverá ser enviado o cod_aluno_migracao. Se o tipo de
                    atuação for Ingressante deverá ser enviado o seq_pessoa_atuacao.
                    · Cod_servico_origem = se o tipo de atuação for aluno deverá ser enviado o cod_curso_oferta do aluno no SGP.
                    Se o tipo de atuação for Ingressante deverá ser enviado o seq_curso_oferta_localidade. Caso o ingressante não
                    possua curso-oferta-localidade, simular qual seria o seu respectivo curso, conforme o nível de ensino e entidade
                    responsável que ele possui e, tipo de oferta de curso cujo token seja “INTERNO”. Buscar apenas ofertas de curso
                    ativas.
                    OBS. Esse objeto retorna uma lista concatenada das parcelas em aberto do aluno/ingressante.*/

                item.ParcelasAbertas = BuscarParcelasEmAberto(parcelasFiltro);
            }
        }

        private string BuscarParcelasEmAberto(ParcelasEmAbertoFiltroData parcelasFiltro)
        {
            string ParcelasAbertas = string.Empty;
            try
            {
                ParcelasAbertas = IntegracaoFinanceiroService.BuscarParcelasEmAberto(parcelasFiltro);
            }
            catch (Exception)
            {
                ///ex.Message;
                ParcelasAbertas = string.Empty;
            }

            return ParcelasAbertas;
        }

        #endregion [ Métodos privados Relatório Bolsistas ]

        /// <summary>
        /// Concatena os nomes fantasia dos reponsáveis pelo benefício ao beneficio
        /// </summary>
        /// <param name="result">Lista com as descrições de benefícios atualizada</param>
        private void ConcatenarResponsaveisFinanceiros(ref List<RelatorioBolsistasVO> result)
        {
            if (!result.SMCAny())
            {
                return;
            }
            var specBeneficio = new PessoaAtuacaoBeneficioFilterSpecification()
            {
                Seqs = result.Select(s => s.SeqPessoaAtuacaoBeneficio).ToArray()
            };
            var responsaveisFinanceiros = SearchProjectionBySpecification(specBeneficio, p => new
            {
                p.Seq,
                NomesFantasia = p.ResponsaveisFinanceiro
                    .Where(w => !string.IsNullOrEmpty(w.PessoaJuridica.NomeFantasia))
                    .OrderBy(o => o.PessoaJuridica.NomeFantasia)
                    .Select(s => s.PessoaJuridica.NomeFantasia)
                    .ToList()
            }).Where(w => w.NomesFantasia.Count > 0).ToArray();

            foreach (var responsaveisBeneficio in responsaveisFinanceiros)
            {
                var beneficio = result.FirstOrDefault(f => f.SeqPessoaAtuacaoBeneficio == responsaveisBeneficio.Seq);
                if (beneficio == null)
                {
                    continue;
                }
                var responsaveis = string.Join(", ", responsaveisBeneficio.NomesFantasia);
                beneficio.DescricaoBeneficio = $"{beneficio.DescricaoBeneficio} - {responsaveis}";
            }
        }

        #endregion [ Relatório Bolsistas ]

        /// <summary>
        /// Valida se a quantidade de parcelas parametrizadas na configuração de beneficio esta de acordo com a pessoa atuação, sendo aluno ou ingressante
        /// </summary>
        /// <param name="seqPessoaAtuacao">Sequencial pessoa atuação</param>
        /// <param name="seqBeneficio">Sequencial do beneficio</param>
        /// <returns></returns>
        public bool ValidarNumeroParcelasParametrizadosConfiguracaoSaoDiferentes(long seqPessoaAtuacao, long seqBeneficio)
        {
            var pessoaAtuacao = this.PessoaAtuacaoDomainService.SearchProjectionByKey(new SMCSeqSpecification<PessoaAtuacao>(seqPessoaAtuacao), p => new
            {
                SeqNivelEnsino = (long?)(p as Ingressante).SeqNivelEnsino ?? (p as Aluno).Historicos.FirstOrDefault(f => f.Atual).SeqNivelEnsino,
                SeqTipoVinculo = (long?)(p as Ingressante).SeqTipoVinculoAluno ?? (p as Aluno).SeqTipoVinculoAluno,
                CodigoMigracaoAluno = (long?)(p as Aluno).CodigoAlunoMigracao ?? null,
                SeqInstituicaoEnsino = p.Pessoa.SeqInstituicaoEnsino
            });

            var exigeCurso = this.InstituicaoNivelTipoVinculoAlunoDomainService.BuscarInstituicaoNivelTipoVinculoAlunoPorNivelTipo(pessoaAtuacao.SeqInstituicaoEnsino, pessoaAtuacao.SeqNivelEnsino, pessoaAtuacao.SeqTipoVinculo, null).ExigeCurso;
            var numeroParcelasBeneficio = this.InstituicaoNivelBeneficioDomainService.SearchProjectionBySpecification(new InstituicaoNivelBeneficioFilterSpecification() { SeqBeneficio = seqBeneficio, SeqNivelEnsino = pessoaAtuacao.SeqNivelEnsino }, p => p.NumeroParcelasPadraoCondicaoPagamento).FirstOrDefault();
            var tipoAtuacaoPessoaAtuacao = PessoaAtuacaoDomainService.SearchProjectionByKey(new SMCSeqSpecification<PessoaAtuacao>(seqPessoaAtuacao), p => p.TipoAtuacao);

            if (numeroParcelasBeneficio.HasValue && exigeCurso)
            {
                if (tipoAtuacaoPessoaAtuacao == TipoAtuacao.Ingressante)
                {
                    var SolicitacaoMatricula = this.SolicitacaoMatriculaDomainService.SearchBySpecification(new SolicitacaoMatriculaFilterSpecification() { SeqPessoaAtuacao = seqPessoaAtuacao }).FirstOrDefault();

                    if (SolicitacaoMatricula.SeqCondicaoPagamentoGra != null)
                    {
                        var condicaoPagamento = this.SolicitacaoMatriculaDomainService.BuscarCondicaoPagamentoAcademico(SolicitacaoMatricula.Seq);

                        if (condicaoPagamento != null && condicaoPagamento.QuantidadeParcelas != numeroParcelasBeneficio)
                        {
                            return true;
                        }
                    }
                }

                var dadosOrigem = PessoaAtuacaoDomainService.RecuperaDadosOrigem(seqPessoaAtuacao);

                if (tipoAtuacaoPessoaAtuacao == TipoAtuacao.Aluno)
                {
                    var codicaoPagamento = this.IntegracaoFinanceiroService.BuscarCondicaoPagamentoAluno(new CondicaoPagamentoAlunoFiltroData()
                    {
                        CodigoServicoOrigem = dadosOrigem.CodigoServicoOrigem,
                        SeqOrigem = (int)dadosOrigem.SeqOrigem,
                        SeqPessoaAtuacao = (long)pessoaAtuacao.CodigoMigracaoAluno
                    });

                    if (numeroParcelasBeneficio != codicaoPagamento.QuantidadeParcelas)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Retorana a lista Situacao chancela do beneficio conforme regras de navegação e a situação atual
        /// </summary>
        /// <param name="ehNovoBeneficio">Informa se é um beneficio novo</param>
        /// <param name="situacaoAtual">Situacao do Beneficio Atual</param>
        /// <returns>Retorna lista conforme regra de navegação</returns>
        private List<SMCDatasourceItem> RetornarSituacaoChancelaBeneficioSelect(bool ehNovoBeneficio, SituacaoChancelaBeneficio? situacaoAtual = null)
        {
            /*O campo Situação deverá ser preenchido com a situação atual do benefício.
            Se refere - se um novo benefício, exibir somente a situação:
            · Aguardando Chancela.
            Se a situação atual do benefício for igual a Aguardando Chancela, exibir somente as situações:
            · Deferido
            · Indeferido.
            Senão, se a Situação atual for igual a Indeferido, exibir somente a situação:
            · Aguardando Chancela.
            Senão, se a Situação atual for igual a Cancelado ou Deferido, não exibir outra situação.*/
            List<SMCDatasourceItem> retorno = new List<SMCDatasourceItem>();

            //O campo Situação deverá ser preenchido com a situação atual do benefício. Se refere-se um novo benefício, exibir somente a situação: - Aguardando Chancela.
            if (ehNovoBeneficio)
            {
                retorno.Add(new SMCDatasourceItem()
                {
                    Seq = (int)SituacaoChancelaBeneficio.AguardandoChancela,
                    Descricao = SMCEnumHelper.GetDescription(SituacaoChancelaBeneficio.AguardandoChancela)
                });
            }
            else
            {
                if (situacaoAtual == SituacaoChancelaBeneficio.AguardandoChancela)
                {
                    retorno.Add(new SMCDatasourceItem()
                    {
                        Seq = (int)SituacaoChancelaBeneficio.AguardandoChancela,
                        Descricao = SMCEnumHelper.GetDescription(SituacaoChancelaBeneficio.AguardandoChancela)
                    });

                    retorno.Add(new SMCDatasourceItem()
                    {
                        Seq = (int)SituacaoChancelaBeneficio.Deferido,
                        Descricao = SMCEnumHelper.GetDescription(SituacaoChancelaBeneficio.Deferido)
                    });

                    retorno.Add(new SMCDatasourceItem()
                    {
                        Seq = (int)SituacaoChancelaBeneficio.Indeferido,
                        Descricao = SMCEnumHelper.GetDescription(SituacaoChancelaBeneficio.Indeferido)
                    });
                }

                if (situacaoAtual == SituacaoChancelaBeneficio.Indeferido)
                {
                    retorno.Add(new SMCDatasourceItem()
                    {
                        Seq = (int)SituacaoChancelaBeneficio.AguardandoChancela,
                        Descricao = SMCEnumHelper.GetDescription(SituacaoChancelaBeneficio.AguardandoChancela)
                    });

                    retorno.Add(new SMCDatasourceItem()
                    {
                        Seq = (int)SituacaoChancelaBeneficio.Indeferido,
                        Descricao = SMCEnumHelper.GetDescription(SituacaoChancelaBeneficio.Indeferido)
                    });
                }
            }

            return retorno;
        }

        /// <summary>
        /// Valida todos os benficios conforme regra:
        /// RN_FIN_015 - Pessoa Atuação x Benefício - Aciona rotina GRA para consistência de restrição de combinação de benefício
        /// </summary>
        /// <param name="seqPessoaAtuacaoBeneficio">Sequencial da pessoa atuação beneficio</param>
        /// <returns>Lista com o motivo das restrições</returns>
        private List<ChancelaBeneficioData> ValidarChancelaBeneficios(long seqPessoaAtuacao, PessoaAtuacaoDadosOrigemVO dadosOrigem)
        {
            var retorno = string.Empty;

            var spec = new PessoaAtuacaoBeneficioFilterSpecification() { SeqPessoaAtuacao = seqPessoaAtuacao };

            var listaBeneficios = this.SearchProjectionBySpecification(spec, p => new
            {
                TipoAtuacao = p.PessoaAtuacao.TipoAtuacao,
                SeqBeneficioFinanceiro = p.Beneficio.SeqBeneficioFinanceiro,
                DataFimVigencia = p.HistoricoVigencias.FirstOrDefault(h => h.Atual).DataFimVigencia,
                DataInicioVigencia = p.HistoricoVigencias.FirstOrDefault(h => h.Atual).DataInicioVigencia,
                IncideParcela = p.IncideParcelaMatricula,
                SituacaoChancelaBeneficio = p.HistoricoSituacoes.OrderByDescending(o => o.DataInicioSituacao).FirstOrDefault().SituacaoChancelaBeneficio,
                Percentual = p.ValorBeneficio,
                CodigoAlunoMigracao = (p.PessoaAtuacao as Aluno).CodigoAlunoMigracao,
                FormaDeducao = p.FormaDeducao,
                //descomentar em caso de implementação da regra - Alterar implementação UC_MAT_003_13 
                //RecebeCobranca = p.Beneficio.RecebeCobranca
            }).ToList();

            List<ChancelaBeneficioData> result = new List<ChancelaBeneficioData>();

            if (listaBeneficios.Any())
            {
                ChancelaBeneficioParametroData parametros = new ChancelaBeneficioParametroData();
                parametros.SeqOrigem = dadosOrigem.SeqOrigem;
                parametros.CodServicoOrigem = dadosOrigem.CodigoServicoOrigem;
                parametros.UsuarioResponsavel = SMCContext.User.SMCGetNome();
                /*
                 Regra Procedure
                 Se for ingressante irei passar como CodAluno = null e o SeqIngressante
                 Se for aluno passará CodAluno = CodigoAlunoMigracao e SeqIngessante = null
                */
                if (listaBeneficios.FirstOrDefault().TipoAtuacao == TipoAtuacao.Ingressante)
                {
                    parametros.CodigoAluno = null;
                    parametros.SeqIngressante = seqPessoaAtuacao;
                }
                else
                {
                    parametros.SeqIngressante = null;
                    parametros.CodigoAluno = parametros.CodigoAluno = listaBeneficios.FirstOrDefault().CodigoAlunoMigracao;//Selecionado o primeiro pois todos os beneficios são do mesmo aluno;
                }

                parametros.Beneficios = new List<BeneficioData>();


                /*-Se a rotina for acionada pelo Administrativo, será composta pelo benefício que está sendo salvo e, se
                houver, mais os demais benefícios associados a pessoa - atuação e que há[benefício financeiro] *, esteja
                [ativo] * e deferido.
                - Se a rotina for acionada pelo Portal Ingressante, será composta por todos os benefícios associados a
                pessoa - atuação e que há[benefício financeiro] *, esteja[ativo] *, deferido e incide na parcela de
                matrícula igual a Sim.*/
                foreach (var item in listaBeneficios)
                {
                    bool beneficioAtivo = item.DataFimVigencia > DateTime.Now || item.DataFimVigencia == null;
                    if (beneficioAtivo &&
                        (item.TipoAtuacao == TipoAtuacao.Ingressante && item.IncideParcela && item.SeqBeneficioFinanceiro != null && item.SituacaoChancelaBeneficio == SituacaoChancelaBeneficio.Deferido)
                         || (item.TipoAtuacao == TipoAtuacao.Aluno && item.SeqBeneficioFinanceiro != null && item.SituacaoChancelaBeneficio == SituacaoChancelaBeneficio.Deferido))
                    {
                        BeneficioData beneficio = new BeneficioData();
                        beneficio.DataFim = (DateTime)item.DataFimVigencia;
                        beneficio.DataInicio = item.DataInicioVigencia;
                        //Somente irá ser passado o valor se for percentual caso contrario irá ser passado nenhum valor 0
                        beneficio.Percentual = (item.FormaDeducao == FormaDeducao.PercentualBolsa) ? (decimal)item.Percentual : 0;
                        beneficio.SeqBeneficioFinanceiro = (int)item.SeqBeneficioFinanceiro;

                        parametros.Beneficios.Add(beneficio);
                    }

                    //Bruno - COMENTADO APÓS CONVERSA COM A RAPHAELA. A IMPLEMENTAÇÃO NO GRA EM 08/04/24, AINDA NÃO ESTAVA ADAPTADA PARA RECEBER DADOS DE INGRESSANTE 
                    // Caso seja necessária a implementação, deve-se carregar na projection acima a propriedade "GerarCobranca"

                    //TSK - Alterar implementação UC_MAT_003_13
                    //if (item.TipoAtuacao == TipoAtuacao.Ingressante)
                    //{
                    //    var ingressante = IngressanteDomainService.SearchByKey(new SMCSeqSpecification<Ingressante>(seqPessoaAtuacao));

                    //    var beneficioContempladoFinalPrevisao = item.DataInicioVigencia >= ingressante.DataAdmissao && item.DataFimVigencia <= ingressante.DataPrevisaoConclusao;
                    //    if (beneficioContempladoFinalPrevisao && (!item.IncideParcela && !item.RecebeCobranca))
                    //    {
                    //        BeneficioData beneficio = new BeneficioData();
                    //        beneficio.DataFim = (DateTime)item.DataFimVigencia;
                    //        beneficio.DataInicio = item.DataInicioVigencia;
                    //        //Somente irá ser passado o valor se for percentual caso contrario irá ser passado nenhum valor 0
                    //        beneficio.Percentual = (item.FormaDeducao == FormaDeducao.PercentualBolsa) ? (decimal)item.Percentual : 0;
                    //        beneficio.SeqBeneficioFinanceiro = (int)item.SeqBeneficioFinanceiro;

                    //        parametros.Beneficios.Add(beneficio);
                    //    }
                    //}
                }

                result = this.IntegracaoFinanceiroService.ValidaChacelaBeneficio(parametros);
            }

            return result;
        }

        /// <summary>
        /// Valida todos os benficios conforme regra Administrativo:
        /// RN_FIN_015 - Pessoa Atuação x Benefício - Aciona rotina GRA para consistência de restrição de combinação de benefício
        /// </summary>
        /// <param name="seqPessoaAtuacaoBeneficio">Sequencial da pessoa atuação beneficio</param>
        /// <param name="dadosOrigem">Dados de origem coforme funcionalidade</param>
        /// <returns>Motivos das restrições</returns>
        public string ValidarChancelaBeneficiosAdministrativo(long seqPessoaAtuacao, PessoaAtuacaoDadosOrigemVO dadosOrigem)
        {
            var retorno = string.Empty;

            var result = this.ValidarChancelaBeneficios(seqPessoaAtuacao, dadosOrigem);

            /*O retorno dessa rotina será uma lista e, composta pelos mesmos benefícios enviados como parâmetro.
            O campo deferido é um inteiro composto pelo números:
            caso 0:
                significa que não foi deferido e contém um motivo
            caso 1:
                significa que foi deferido e não contém motivo
            caso 2:
                significa que o aluno/ingressante não foi encontrado no GRA, a cabe a regra de negocio o seu tratamento*/
            foreach (var item in result)
            {
                if (!string.IsNullOrEmpty(item.DescricaoMotivoIndeferimento))
                {
                    retorno += $"<br /> - {item.DescricaoMotivoIndeferimento}";
                }
                else
                {
                    /*Associação de benefício para Aluno-- > nesse caso o deferimento do benefício deverá ser bloqueado com a seguinte mensagem informativa:
                    Deferimento do benefício não permitido pelo sistema financeiro.Motivo: Aluno não encontrado no GRA.
                    Caso ingressante não faz nada*/
                    if (item.Deferido == 2 && dadosOrigem.TipoAtuacao == TipoAtuacao.Aluno)
                    {
                        retorno = "<br />Aluno não encontrado no GRA.";
                    }
                }
            }

            return retorno;
        }

        /// <summary>
        /// Valida todos os benficios conforme regra Portal:
        /// RN_FIN_015 - Pessoa Atuação x Benefício - Aciona rotina GRA para consistência de restrição de combinação de benefício
        /// </summary>
        /// <param name="seqPessoaAtuacaoBeneficio">Sequencial da pessoa atuação beneficio</param>
        /// <param name="dadosOrigem">Dados de origem coforme funcionalidade</param>
        /// <returns>Lista de pessoa atuacao beneficio</returns>
        public List<long> ValidarChancelaBeneficiosPortal(long seqPessoaAtuacao, PessoaAtuacaoDadosOrigemVO dadosOrigem)
        {
            List<long> retorno = new List<long>();

            var result = this.ValidarChancelaBeneficios(seqPessoaAtuacao, dadosOrigem);

            var spec = new PessoaAtuacaoBeneficioFilterSpecification() { SeqPessoaAtuacao = seqPessoaAtuacao };
            var listaBeneficios = this.SearchProjectionBySpecification(spec, p => new
            {
                SeqPessoaAtuacaoBeneficio = p.Seq,
                SeqBeneficioFinanceiro = p.Beneficio.SeqBeneficioFinanceiro,
                FormaDeducao = p.FormaDeducao
            }).ToList();

            /*Os benefícios que não possuem mensagem de erro, deverão acionar a rotina de inclusão
            ST_INCLUI_CONTRATO_BENEFICIO_ACADEMICO, conforme RN_FIN_007 - Pessoa Atuação x
            Benefício - Parâmetros rotina GRA – Inclui contrato benefício acadêmico, na seguinte ordem:
            · Primeiramente os benefícios que a forma de dedução é igual a Percentual de Bolsa
            · Em seguida, os benefícios que a forma de dedução é igual a Saldo Final de Parcela.
            · E por último os benefícios que a forma de dedução é igual a Valor.*/
            var beneficiosPercentual = listaBeneficios.Where(w => w.FormaDeducao == FormaDeducao.PercentualBolsa);
            var beneficiosSaldoFinal = listaBeneficios.Where(w => w.FormaDeducao == FormaDeducao.SaldoFinalParcela);
            var beneficiosValorBolsa = listaBeneficios.Where(w => w.FormaDeducao == FormaDeducao.ValorBolsa);
            string mensagemErro = "Durante a adesão no portal do ingressante foi identificado restrição de combinação de benefício, conforme retorno do sistema financeiro. Motivo: {0}.";

            /*Os benefícios que possuem mensagem de erro, não deverão acionar a rotina de inclusão
            ST_INCLUI_CONTRATO_BENEFICIO_ACADEMICO.E os seguintes procedimentos deverão ser
            realizados para cada benefício retornado com erro:
            Inserir no histórico a situação atual igual a Aguardando Chancela, a data de · início igual a data
            corrente(hoje) e a observação deverá ser preenchida no seguinte formato: "Durante a adesão no
            portal do ingressante foi identificado restrição de combinação de benefício., conforme retorno do
            sistema financeiro. Motivo: [Descrição da mensagem de erro do respectivo benefício].
            · Enviar a notificação RN_FIN_016 - Pessoa Atuação x Benefício - Envia Notificação sobre alteração
            da situação para Aguardando Chancela*/
            foreach (var item in result)
            {
                if (beneficiosPercentual.SMCAny(a => a.SeqBeneficioFinanceiro == item.SeqBeneficioFinanceiro))
                {
                    if (item.Deferido == 1)
                    {
                        retorno.Add(beneficiosPercentual.FirstOrDefault(f => f.SeqBeneficioFinanceiro == item.SeqBeneficioFinanceiro).SeqPessoaAtuacaoBeneficio);
                    }
                    else
                    {
                        var seqPessoAtuacaoBeneficio = beneficiosPercentual.FirstOrDefault(f => f.SeqBeneficioFinanceiro == item.SeqBeneficioFinanceiro).SeqPessoaAtuacaoBeneficio;
                        this.SalvarBeneficioHistoricoSituacaoChancela(seqPessoAtuacaoBeneficio, (int)SituacaoChancelaBeneficio.AguardandoChancela, string.Format(mensagemErro, item.DescricaoMotivoIndeferimento));
                        this.EnviarNotificaoBeneficoAguardandoChancela(seqPessoAtuacaoBeneficio);
                    }
                }
            }
            foreach (var item in result)
            {
                if (beneficiosSaldoFinal.SMCAny(a => a.SeqBeneficioFinanceiro == item.SeqBeneficioFinanceiro))
                {
                    if (item.Deferido == 1)
                    {
                        retorno.Add(beneficiosSaldoFinal.FirstOrDefault(f => f.SeqBeneficioFinanceiro == item.SeqBeneficioFinanceiro).SeqPessoaAtuacaoBeneficio);
                    }
                    else
                    {
                        var seqPessoAtuacaoBeneficio = beneficiosSaldoFinal.FirstOrDefault(f => f.SeqBeneficioFinanceiro == item.SeqBeneficioFinanceiro).SeqPessoaAtuacaoBeneficio;
                        this.SalvarBeneficioHistoricoSituacaoChancela(seqPessoAtuacaoBeneficio, (int)SituacaoChancelaBeneficio.AguardandoChancela, string.Format(mensagemErro, item.DescricaoMotivoIndeferimento));
                        this.EnviarNotificaoBeneficoAguardandoChancela(seqPessoAtuacaoBeneficio);
                    }
                }
            }
            foreach (var item in result)
            {
                if (beneficiosValorBolsa.SMCAny(a => a.SeqBeneficioFinanceiro == item.SeqBeneficioFinanceiro))
                {
                    if (item.Deferido == 1)
                    {
                        retorno.Add(beneficiosValorBolsa.FirstOrDefault(f => f.SeqBeneficioFinanceiro == item.SeqBeneficioFinanceiro).SeqPessoaAtuacaoBeneficio);
                    }
                    else
                    {
                        var seqPessoAtuacaoBeneficio = beneficiosValorBolsa.FirstOrDefault(f => f.SeqBeneficioFinanceiro == item.SeqBeneficioFinanceiro).SeqPessoaAtuacaoBeneficio;
                        this.SalvarBeneficioHistoricoSituacaoChancela(seqPessoAtuacaoBeneficio, (int)SituacaoChancelaBeneficio.AguardandoChancela, string.Format(mensagemErro, item.DescricaoMotivoIndeferimento));
                        this.EnviarNotificaoBeneficoAguardandoChancela(seqPessoAtuacaoBeneficio);
                    }
                }
            }

            return retorno;
        }

        /// <summary>
        /// Enviar notificação para pessoa atuação quando beneficio aguardando chancela
        /// </summary>
        /// <param name="seqPessoaAtuacaoBeneficio">Sequêncial pessoa atuação beneficio</param>
        public void EnviarNotificaoBeneficoAguardandoChancela(long seqPessoaAtuacaoBeneficio)
        {
            var pessoaAtuacaoBeneficio = this.SearchProjectionByKey(new SMCSeqSpecification<PessoaAtuacaoBeneficio>(seqPessoaAtuacaoBeneficio), p => new
            {
                SeqPessoAtuacao = p.SeqPessoaAtuacao,
                DescricaoBeneficio = p.Beneficio.Descricao
            });

            var spec = new SolicitacaoMatriculaFilterSpecification() { SeqPessoaAtuacao = pessoaAtuacaoBeneficio.SeqPessoAtuacao };

            var solicitacao = this.SolicitacaoMatriculaDomainService.SearchProjectionBySpecification(spec, p => new
            {
                NomeSolicitante = string.IsNullOrEmpty(p.PessoaAtuacao.DadosPessoais.NomeSocial) ? p.PessoaAtuacao.DadosPessoais.Nome : p.PessoaAtuacao.DadosPessoais.NomeSocial,
                DescricaoProcesso = p.ConfiguracaoProcesso.Processo.Descricao,
                SeqSolicitacao = p.Seq
            }).FirstOrDefault();

            Dictionary<string, string> dadosMerge = new Dictionary<string, string>();
            dadosMerge.Add("{{COD_PESSOA_ATUACAO}}", pessoaAtuacaoBeneficio.SeqPessoAtuacao.ToString());
            dadosMerge.Add("{{NOM_PESSOA}}", solicitacao.NomeSolicitante);
            dadosMerge.Add("{{DAT_CORRENTE}}", DateTime.Now.SMCDataAbreviada());
            dadosMerge.Add("{{DSC_PROCESSO}}", solicitacao.DescricaoProcesso);
            dadosMerge.Add("{{DSC_BENEFICIO}}", pessoaAtuacaoBeneficio.DescricaoBeneficio);

            // Chama o método para enviar a notificação para a solicitação.
            var parametros = new EnvioNotificacaoSolicitacaoServicoVO()
            {
                SeqSolicitacaoServico = solicitacao.SeqSolicitacao,
                TokenNotificacao = TOKEN_TIPO_NOTIFICACAO.BENEFICIO_AGUARDANDO_CHANCELA,
                DadosMerge = dadosMerge,
                EnvioSolicitante = false,
                ConfiguracaoPrimeiraEtapa = false
            };
            this.SolicitacaoServicoEnvioNotificacaoDomainService.EnviarNotificacaoSolicitacaoServico(parametros);
        }

        /// <summary>
        /// Enviar notificação para pessoa atuação quando ocorrer alteração de vigência
        /// </summary>
        /// <param name="pessoaAtuacaoBeneficioVO">Dados da pessoa atuação</param>
        /// <param name="dataFimAnterior">Data fim anterior</param>
        /// <param name="dataInicioAnterior">Data inicio anterior</param>
        private void EnviarNotificaoAlteracaoVigencia(PessoaAtuacaoBeneficioVO pessoaAtuacaoBeneficioVO, DateTime? dataInicioAnterior, DateTime? dataFimAnterior)
        {
            var pessoaAtuacaoBeneficio = this.SearchProjectionByKey(new SMCSeqSpecification<PessoaAtuacaoBeneficio>(pessoaAtuacaoBeneficioVO.Seq), p => new
            {
                SeqPessoAtuacao = p.SeqPessoaAtuacao,
                DescricaoBeneficio = p.Beneficio.Descricao,
                TipoAtuacao = p.PessoaAtuacao.TipoAtuacao,
                Nome = string.IsNullOrEmpty(p.PessoaAtuacao.DadosPessoais.NomeSocial) ? p.PessoaAtuacao.DadosPessoais.Nome : p.PessoaAtuacao.DadosPessoais.NomeSocial,
            });

            var motivoAlteracao = this.MotivoAlteracaoBeneficioDomainService.SearchProjectionByKey(new SMCSeqSpecification<MotivoAlteracaoBeneficio>(pessoaAtuacaoBeneficioVO.SeqMotivoAlteracaoBeneficio), p => new
            {
                p.Descricao
            });

            Dictionary<string, string> dadosMerge = new Dictionary<string, string>();
            dadosMerge.Add("{{NOM_PESSOA}}", pessoaAtuacaoBeneficio.Nome);
            dadosMerge.Add("{{TIPO_ATUACAO}}", pessoaAtuacaoBeneficio.TipoAtuacao.SMCGetDescription());
            dadosMerge.Add("{{ID}}", pessoaAtuacaoBeneficio.SeqPessoAtuacao.ToString());
            dadosMerge.Add("{{DSC_BENEFICIO}}", pessoaAtuacaoBeneficio.DescricaoBeneficio);
            dadosMerge.Add("{{DAT_INICIO_VIGENCIA_ANTERIOR}}", dataInicioAnterior.SMCDataAbreviada());
            dadosMerge.Add("{{DAT_FIM_VIGENCIA_ANTERIOR}}", dataFimAnterior.SMCDataAbreviada());
            dadosMerge.Add("{{DAT_INICIO_VIGENCIA_NOVA}}", pessoaAtuacaoBeneficioVO.DataInicioVigencia.SMCDataAbreviada());
            dadosMerge.Add("{{DAT_FIM_VIGENCIA_NOVA}}", pessoaAtuacaoBeneficioVO.DataFimVigencia.SMCDataAbreviada());
            dadosMerge.Add("{{DSC_MOTIVO_ALTERACAO}}", motivoAlteracao.Descricao);

            var parametros = new EnvioNotificacaoPessoaAtuacaoBeneficioVO()
            {
                SeqPessoaAtuacaoBeneficio = pessoaAtuacaoBeneficioVO.Seq,
                DadosMerge = dadosMerge,
                TokenTipoNotificacao = TOKEN_TIPO_NOTIFICACAO.ALTERACAO_VIGENCIA_BENEFICIO
            };

            this.EnviarNotificacaoPessoaAtuacaoBenefcio(parametros);
        }

        /// <summary>
        /// Salvar a alteração de vigência da pessoa atuação beneficio
        /// </summary>
        /// <param name="pessoaAtuacaoBeneficioVO">Dados a serem salvos</param>
        /// <returns>Sequêncial pessoa atuação beneficio</returns>
        public long SalvarAlterarVigenciaBeneficio(PessoaAtuacaoBeneficioVO pessoaAtuacaoBeneficioVO)
        {
            using (var unitOfWork = SMCUnitOfWork.Begin())
            {
                PessoaAtuacaoBeneficio pessoaAtuacaoBeneficioBD = new PessoaAtuacaoBeneficio();
                var includes = IncludesPessoaAtuacaoBeneficio.HistoricoVigencias | IncludesPessoaAtuacaoBeneficio.ArquivosAnexo;
                pessoaAtuacaoBeneficioBD = this.SearchByKey(new SMCSeqSpecification<PessoaAtuacaoBeneficio>(pessoaAtuacaoBeneficioVO.Seq), includes);

                var arquivosConvertidos = pessoaAtuacaoBeneficioVO.ArquivosAnexo.TransformList<PessoaAtuacaoBeneficioAnexo>();

                var arquivoAlterado =  (pessoaAtuacaoBeneficioBD.ArquivosAnexo != null && pessoaAtuacaoBeneficioBD.ArquivosAnexo.Count() > 0 && pessoaAtuacaoBeneficioBD.ArquivosAnexo.Count() > pessoaAtuacaoBeneficioVO.ArquivosAnexo.Count()
                    || pessoaAtuacaoBeneficioVO.ArquivosAnexo.Any(x => x.ArquivoAnexado.State == SMCUploadFileState.Changed));

                if (arquivosConvertidos.SMCAny())
                {
                    foreach (var arquivo in arquivosConvertidos)
                    {
                        var guid = new Guid(pessoaAtuacaoBeneficioVO.ArquivosAnexo.First(f => f.Seq == arquivo.Seq).ArquivoAnexado.GuidFile);
                        var specArquivo = new ArquivoAnexadoFilterSpecification() { UidArquivo = guid };
                        arquivo.SeqArquivoAnexado = arquivo.ArquivoAnexado.Seq = ArquivoAnexadoDomainService
                            .SearchProjectionBySpecification(specArquivo, p => p.Seq)
                            .FirstOrDefault();
                        EnsureFileIntegrity(arquivo, s => s.SeqArquivoAnexado, a => a.ArquivoAnexado);
                    }
                }
                pessoaAtuacaoBeneficioBD.ArquivosAnexo = arquivosConvertidos;
                this.SaveEntity(pessoaAtuacaoBeneficioBD);


                /*Se não houver alteração em nenhum arquivo ao alterar a vigencia e as datas de inicio e fim se manterem as mesmas, exibir a mensagem:
                 "Alteração não permitida. Nenhuma informação do benefício foi alterada."*/
                if (!arquivoAlterado && pessoaAtuacaoBeneficioBD.HistoricoVigencias.FirstOrDefault(f => f.Atual).DataInicioVigencia == pessoaAtuacaoBeneficioVO.DataInicioVigencia
                    && pessoaAtuacaoBeneficioBD.HistoricoVigencias.FirstOrDefault(f => f.Atual).DataFimVigencia == pessoaAtuacaoBeneficioVO.DataFimVigencia)
                {
                    throw new PessoaAtuacaoBeneficioNenhumaAlteracaoBeneficioException();
                }

                //Buscar situação atução do beneficio
                pessoaAtuacaoBeneficioVO.SeqSituacaoChancelaBeneficioAtual = (int)this.BeneficioHistoricoSituacaoDomainService.BuscarHistoricoSituacaoChancelaBeneficioAtual(pessoaAtuacaoBeneficioVO.Seq);

                this.ValidacaoIntervaloDatasConfiguracaoBeneficioValor(pessoaAtuacaoBeneficioVO);

                /*Se o tipo de atuação for igual a Ingressante ou Aluno e, existe[contrato financeiro] * associado ao
                benefício, deverá ser avaliado primeiramente no GRA se há restrição de combinação do benefício que
                está sendo salvo com demais benefícios, conforme a regra: RN_FIN_015 - Pessoa Atuação x Benefício
                - Aciona rotina GRA para consistência de restrição de combinação de benefício
                Se pelo menos 1(um) benefício da lista retornada teve a concessão indeferida, · deverá ser
                abortada a operação e exibida a seguinte mensagem impeditiva: "Deferimento não permitido,
                devido à restrição de combinação de benefício.Motivo: [Concatenar os motivos de erros retornados
                pela rotina de consistência de restrição do GRA]".
                · Senão, se a rotina não retornou concessão indeferida, e o tipo de atuação for*/
                var dadosBeneficio = SearchProjectionByKey(new SMCSeqSpecification<PessoaAtuacaoBeneficio>(pessoaAtuacaoBeneficioVO.Seq), x => new
                {
                    SeqPessoaAtuacao = x.SeqPessoaAtuacao,
                    DataInicioVigencia = x.HistoricoVigencias.FirstOrDefault(h => h.Atual).DataInicioVigencia,
                    DataFimVigencia = x.HistoricoVigencias.FirstOrDefault(h => h.Atual).DataFimVigencia,
                    SeqBeneficioFinanceiro = x.Beneficio.SeqBeneficioFinanceiro,
                    RecebeCobranca = x.Beneficio.RecebeCobranca,
                    JustificativaNaoRecebeCobranca = x.Beneficio.JustificativaNaoRecebeCobranca,
                    PossuiContrato = x.ControlesFinanceiros.Any(a => a.DataExclusao == null), ///deverão ser considerados os contratos financeiros que não possuem data de exclusão.,
                    FormaDeducao = x.ConfiguracaoBeneficio.FormaDeducao,
                    ResponsaveisFinanceiro = x.ResponsaveisFinanceiro,
                    TipoAtuacao = x.PessoaAtuacao.TipoAtuacao,
                    ValorBeneficio = x.ValorBeneficio,
                    SeqPessoa = x.PessoaAtuacao.SeqPessoa,
                    SeqInstituicaoEnsino = x.PessoaAtuacao.Pessoa.SeqInstituicaoEnsino,
                    CodigoAlunoMigracao = (x.PessoaAtuacao as Aluno).CodigoAlunoMigracao,
                    DataPrevisaoConclusaoPessoaAtuacao = (DateTime?)((x.PessoaAtuacao as Ingressante).DataPrevisaoConclusao ?? (x.PessoaAtuacao as Aluno).Historicos.FirstOrDefault(h => h.Atual).PrevisoesConclusao.OrderByDescending(p => p.Seq).FirstOrDefault().DataPrevisaoConclusao),
                    IncideParcela = x.IncideParcelaMatricula,
                    SeqBeneficio = x.SeqBeneficio,
                    IncluirDesbloqueioTemporario = x.Beneficio.IncluirDesbloqueioTemporario,
                    DescricaoBeneficio = x.Beneficio.Descricao
                });

                if (dadosBeneficio.PossuiContrato)
                {
                    //Recupera os dados de origem da pessoa atuação, considerando a simulação em caso de não possuir curso
                    var dadosOrigem = PessoaAtuacaoDomainService.RecuperaDadosOrigem(dadosBeneficio.SeqPessoaAtuacao);

                    //Validar chancela do beneficio conforme RN_FIN_015 - Pessoa Atuação x Benefício - Aciona rotina GRA para consistência de restrição de combinação de benefício
                    //var respostaChancelaBeneficio = ValidarChancelaBeneficiosAdministrativo(dadosBeneficio.SeqPessoaAtuacao, dadosOrigem);
                    //if (!string.IsNullOrEmpty(respostaChancelaBeneficio))
                    //{
                    //    throw new PessoaAtuacaoBeneficioValidacaoChancelaException(respostaChancelaBeneficio);
                    //}

                    /*INGRESSANTE: verificar se houve alteração do campo "Incide Parcela Matrícula":
                    *Se não houve alteração, nenhuma ação deverá ser realização;
                    *Se houve alteração e, o campo possuia o valor NÃO e passou para o valor SIM, significa que não
                    existia contrato no sistema financeiro para o ingressante e agora deverá existir. Nesse caso, deverá ser
                    adiconada a rotina para inclusão do benefício no sistema financeiro....
                    * Se houve alteração e, o campo possuia o valor SIM e passou para o valor NÃO, E existe contrato
                    financeiro associado ao benefício, significa que exisitia contrato no sistema financeiro para o
                    ingressante e este não deve existir mais.Nesse caso, deverá ser acionada a rotina para exclusão do
                    contrato do benefício no sistema financeir, conforme RN_FIN_018 -Pessoa Atuação x Benefício -
                    Aciona rotina GRA para Exclusao do benefício da pessoa - atuacao*/
                    if (dadosBeneficio.TipoAtuacao == TipoAtuacao.Ingressante)
                    {
                        /*Ao salvar a "Alteração de vigência do benefício" com sucesso, o sistema deverá
                        incluir um novo registro no histórico de vigências do benefício conforme os dados
                        informados pelo usuário na interface.*/
                        this.SalvarBeneficioHisitoricoVigencia(pessoaAtuacaoBeneficioVO.Seq, pessoaAtuacaoBeneficioVO.DataInicioVigencia, pessoaAtuacaoBeneficioVO.DataFimVigencia, pessoaAtuacaoBeneficioVO.Justificativa, pessoaAtuacaoBeneficioVO.SeqMotivoAlteracaoBeneficio);

                        if (dadosBeneficio.IncideParcela != pessoaAtuacaoBeneficioVO.IncideParcelaMatricula)
                        {
                            var pessoaAtuacaoBeneficio = this.SearchByKey(new SMCSeqSpecification<PessoaAtuacaoBeneficio>(pessoaAtuacaoBeneficioVO.Seq));
                            pessoaAtuacaoBeneficio.IncideParcelaMatricula = pessoaAtuacaoBeneficioVO.IncideParcelaMatricula;
                            this.SaveEntity(pessoaAtuacaoBeneficio);

                            //Significa que ele era não e passou para sim
                            if (pessoaAtuacaoBeneficioVO.IncideParcelaMatricula == true)
                            {
                                try
                                {
                                    this.AtualizarContratoPessoaAtuacaoBeneficio(pessoaAtuacaoBeneficio.Seq);
                                }
                                catch (Exception ex)
                                {
                                    throw new PessoaAtuacaoBeneficioErroIncluirBeneficioException(ex.Message);
                                }
                            }
                            else
                            {
                                //Exclui os contratos sem alterar o historico de situação
                                this.ExcluirPessoaAtuacaoBeneficio(pessoaAtuacaoBeneficioVO, true);
                            }
                        }
                    }
                    else
                    {
                        /*ALUNO: deverá ser acionada a rotina de alteração do benefício no GRA
                        ST_ALTERA_CONTRATO_BENEFICIO_ACADEMICO para o benefício que está sendo alterado de
                        acordo com RN_FIN_021 -Pessoa Atuação x Benefício - Aciona rotina para alterar contrato benefício
                        no GRA*/
                        try
                        {
                            this.AlterarVigenciaContratoPessoaAtuacaoBeneficio(pessoaAtuacaoBeneficioVO);

                            /*Ao salvar a "Alteração de vigência do benefício" com sucesso, o sistema deverá
                            incluir um novo registro no histórico de vigências do benefício conforme os dados
                            informados pelo usuário na interface.*/
                            this.SalvarBeneficioHisitoricoVigencia(pessoaAtuacaoBeneficioVO.Seq, pessoaAtuacaoBeneficioVO.DataInicioVigencia, pessoaAtuacaoBeneficioVO.DataFimVigencia, pessoaAtuacaoBeneficioVO.Justificativa, pessoaAtuacaoBeneficioVO.SeqMotivoAlteracaoBeneficio);
                        }
                        catch (Exception ex)
                        {
                            throw new PessoaAtuacaoBeneficioErroIncluirBeneficioException(ex.Message);
                        }
                    }

                    //[VERIFICA SE É NECESSÁRIO BLOQUEAR OU DESBLOQUEAR A PARCELA DE MATRÍCULA PESSOA ATUAÇÃO]
                    if (dadosBeneficio.IncluirDesbloqueioTemporario
                       && ((dadosBeneficio.TipoAtuacao == TipoAtuacao.Ingressante && pessoaAtuacaoBeneficioVO.IncideParcelaMatricula)
                            || dadosBeneficio.TipoAtuacao == TipoAtuacao.Aluno))
                    {
                        /*Verificar se a pessoa-atuação em questão possui bloqueio com token
                        'PARCELA_MATRICULA_PENDENTE ou 'PARCELA_PRE_MATRICULA_PENDENTE' com a situação
                        igual a "Bloqueado".Para cada bloqueio encontrado, verificar a solicitacao de servico correspondente e
                        Atributos marcados com* são de preenchimento obrigatório.
                        encontrar a data de inicio do PERIODO_LETIVO, do ciclo letivo da solicitação.Se a data de inicio do
                        PERIODO_LETIVO estiver dentro do período de vigência do benefício, atualizar os seguintes campos do
                        respectivo bloqueio:*/
                        var tokensBloqueio = new List<string> { TOKEN_MOTIVO_BLOQUEIO.PARCELA_MATRICULA_PENDENTE, TOKEN_MOTIVO_BLOQUEIO.PARCELA_PRE_MATRICULA_PENDENTE };

                        var includePessoaAtuacaoBloqueio = IncludesPessoaAtuacaoBloqueio.SolicitacaoServico_ConfiguracaoProcesso_Processo_CicloLetivo;
                        var listaBloqueios = PessoaAtuacaoBloqueioDomainService
                                                                .SearchBySpecification(new PessoaAtuacaoBloqueioFilterSpecification()
                                                                {
                                                                    SeqPessoaAtuacao = dadosBeneficio.SeqPessoaAtuacao,
                                                                    BloqueioMatricula = true,
                                                                    BloqueadoOuDesbloqueadoTemporariamente = true
                                                                }, includePessoaAtuacaoBloqueio).ToList();

                        foreach (var item in listaBloqueios)
                        {
                            //Buscar a data de inicio do evento letivo
                            DatasEventoLetivoVO datasEventoLetivo = new DatasEventoLetivoVO();
                            if (dadosBeneficio.TipoAtuacao == TipoAtuacao.Aluno)
                            {
                                var includesPlanoEstudoItem = IncludesPlanoEstudoItem.PlanoEstudo_AlunoHistoricoCicloLetivo_AlunoHistorico_CursoOfertaLocalidadeTurno_CursoOfertaLocalidade;
                                var planoEstudoItem = this.PlanoEstudoItemDomainService.SearchBySpecification(new PlanoEstudoItemFilterSpecification() { SeqAluno = dadosBeneficio.SeqPessoaAtuacao, PlanoEstudoAtual = true }, includesPlanoEstudoItem).FirstOrDefault();

                                datasEventoLetivo = ConfiguracaoEventoLetivoDomainService.BuscarDatasEventoLetivo(
                                                    item.SolicitacaoServico.ConfiguracaoProcesso.Processo.CicloLetivo.Seq,
                                                    dadosOrigem.SeqCursoOfertaLocalidadeTurno,
                                                    planoEstudoItem.PlanoEstudo.AlunoHistoricoCicloLetivo.TipoAluno,
                                                    TOKEN_TIPO_EVENTO.PERIODO_CICLO_LETIVO);
                            }
                            else if (dadosBeneficio.TipoAtuacao == TipoAtuacao.Ingressante)
                            {
                                datasEventoLetivo = ConfiguracaoEventoLetivoDomainService.BuscarDatasEventoLetivo(
                                                    item.SolicitacaoServico.ConfiguracaoProcesso.Processo.CicloLetivo.Seq,
                                                    dadosOrigem.SeqCursoOfertaLocalidadeTurno,
                                                    TipoAluno.Calouro,
                                                    TOKEN_TIPO_EVENTO.PERIODO_CICLO_LETIVO);
                            }

                            if (item.SituacaoBloqueio == SituacaoBloqueio.Bloqueado)
                            {
                                /*Verifico se a data inicio do evento letivo e vigente pelo beneficio
                                Uma vez que a data da parcela de matricula sempre ocorre no inicio do ciclo letivo
                                Desta forma a consistência é feita a verificação se a parcela de matricula é vigente no beneficio.*/
                                if (datasEventoLetivo.DataInicio >= pessoaAtuacaoBeneficioVO.DataInicioVigencia && datasEventoLetivo.DataInicio <= pessoaAtuacaoBeneficioVO.DataFimVigencia)
                                {
                                    item.SituacaoBloqueio = SituacaoBloqueio.Desbloqueado;
                                    item.TipoDesbloqueio = TipoDesbloqueio.Temporario;
                                    item.UsuarioDesbloqueioTemporario = dadosBeneficio.SeqBeneficio + " - " + dadosBeneficio.DescricaoBeneficio;
                                    item.DataAlteracao = DateTime.Now;
                                    item.CadastroIntegracao = true;
                                    item.JustificativaDesbloqueio = dadosBeneficio.SeqBeneficio + " - " + dadosBeneficio.DescricaoBeneficio;
                                    item.DataDesbloqueioTemporario = DateTime.Now;
                                }
                            }
                            else
                            {
                                if (datasEventoLetivo.DataInicio < pessoaAtuacaoBeneficioVO.DataInicioVigencia || datasEventoLetivo.DataInicio > pessoaAtuacaoBeneficioVO.DataFimVigencia)
                                {
                                    item.SituacaoBloqueio = SituacaoBloqueio.Bloqueado;
                                    item.TipoDesbloqueio = null;
                                    item.UsuarioDesbloqueioTemporario = null;
                                    item.JustificativaDesbloqueio = null;
                                    item.DataDesbloqueioTemporario = null;
                                }
                            }

                            PessoaAtuacaoBloqueioDomainService.SaveEntity(item);
                        }
                    }
                }
                else
                    this.SalvarBeneficioHisitoricoVigencia(pessoaAtuacaoBeneficioVO.Seq, pessoaAtuacaoBeneficioVO.DataInicioVigencia, pessoaAtuacaoBeneficioVO.DataFimVigencia, pessoaAtuacaoBeneficioVO.Justificativa, pessoaAtuacaoBeneficioVO.SeqMotivoAlteracaoBeneficio);

                /*Enviar notificação para os usuários responsáveis conforme RN_FIN_022 -
                Pessoa Atuação x Benefício - Envia Notificação sobre alteração da vigência do benefício*/
                this.EnviarNotificaoAlteracaoVigencia(pessoaAtuacaoBeneficioVO, dadosBeneficio.DataInicioVigencia, dadosBeneficio.DataFimVigencia);
                //throw new Exception("Roolback");
                //Comit para salvar transação
                unitOfWork.Commit();

                return long.MaxValue;
            }
        }

        /// <summary>
        /// Alterar vigências dos contrtos financeiros do beneficio associado
        /// </summary>
        /// <param name="seqPessoaAtuacaoBeneficio"></param>
        /// <param name="isPortal"></param>
        private void AlterarVigenciaContratoPessoaAtuacaoBeneficio(PessoaAtuacaoBeneficioVO pessoaAtuacaoBeneficioVO)
        {
            //[COMO A ROTINA DEVE SER ACIONADA DURANTE O DEFERIMENTO NO ADMINISTRATIVO]
            //Os parâmetros da rotina deverão ser enviados, conforme RN_FIN_021 - Pessoa Atuação x Benefício - Aciona rotina para alterar contrato benefício no GRA
            var dadosBeneficio = SearchProjectionByKey(new SMCSeqSpecification<PessoaAtuacaoBeneficio>(pessoaAtuacaoBeneficioVO.Seq), x => new
            {
                SeqPessoaAtuacao = x.SeqPessoaAtuacao,
                DataInicioVigencia = x.HistoricoVigencias.FirstOrDefault(h => h.Atual).DataInicioVigencia,
                DataFimVigencia = x.HistoricoVigencias.FirstOrDefault(h => h.Atual).DataFimVigencia,
                SeqBeneficioFinanceiro = x.Beneficio.SeqBeneficioFinanceiro,
                RecebeCobranca = x.Beneficio.RecebeCobranca,
                JustificativaNaoRecebeCobranca = x.Beneficio.JustificativaNaoRecebeCobranca,
                FormaDeducao = x.ConfiguracaoBeneficio.FormaDeducao,
                ResponsaveisFinanceiro = x.ResponsaveisFinanceiro.Select(s => new
                {
                    s.Seq,
                    s.SeqPessoaAtuacaoBeneficio,
                    s.SeqPessoaJuridica,
                    s.TipoResponsavelFinanceiro,
                    s.ValorPercentual,
                    s.PessoaJuridica.Cnpj
                }),
                ValorBeneficio = x.ValorBeneficio,
                SeqInstituicaoEnsino = x.PessoaAtuacao.Pessoa.SeqInstituicaoEnsino,
                CodigoAlunoMigracao = (x.PessoaAtuacao as Aluno).CodigoAlunoMigracao,
            });

            // Recupera os dados de origem da pessoa atuação, considerando a simulação em caso de não possuir curso
            var dadosOrigem = PessoaAtuacaoDomainService.RecuperaDadosOrigem(dadosBeneficio.SeqPessoaAtuacao);

            /*[*anterior] = refere-se aos dados do último registro antes da alteração do usuário
            [*atual] = refere-se aos dados que acabaram de ser alterados pelo usuário
            Parametros do para alterar vigencia*/
            var objBenef = new AlterarVigenciaContratoBeneficioParametrosData();

            //Sequencial do benefício: sequencial do benefício no financeiro
            objBenef.SeqBeneficio = dadosBeneficio.SeqBeneficioFinanceiro;

            //Forma de dedução: domínio da forma de dedução da configuração de benefício selecionada
            objBenef.FormaDeducao = (short?)dadosBeneficio.FormaDeducao;

            //Valor de dedução: valor de dedução do benefício
            objBenef.ValorDeducao = dadosBeneficio.ValorBeneficio;

            //Data início benefício anterior: data de início do benefício [*anterior]
            objBenef.DataInicioBeneficioAnterior = dadosBeneficio.DataInicioVigencia;

            //Data fim benefício anterior: Aluno: data de início do benefício[*anterior]
            objBenef.DataFimBeneficioAnterior = dadosBeneficio.DataFimVigencia;

            //Data início benefício nova: Aluno: data de início do benefício[*atual]
            objBenef.DataInicioBeneficioNova = pessoaAtuacaoBeneficioVO.DataInicioVigencia;

            //Data fim benefício nova: Aluno: data fim do benefício[*atual]
            objBenef.DataFimBeneficioNova = pessoaAtuacaoBeneficioVO.DataFimVigencia;

            //Sequencial do ingressante: Aluno = nulo.
            objBenef.SeqIngressante = null;

            //Código do aluno: Aluno = código do legado(migração)
            objBenef.CodigoAluno = dadosBeneficio.CodigoAlunoMigracao;

            //Considerar periodo de matricula: Aluno = false
            objBenef.ConsiderarPeriodoMatriculaIngressante = false;

            //Sequencial da origem: o sequencial deverá ser enviado de acordo com RN_FIN_012 - Pessoa Atuação x Benefício - Identificação do parâmetro: Sequencial Origem.
            objBenef.SeqOrigem = (int)dadosOrigem.SeqOrigem;

            //Código do serviço origem: o código deverá ser enviado de acordo com RN_FIN_013 - Pessoa Atuação x Benefício - Identificação do parâmetro: Código Serviço Origem.
            objBenef.SeqServicoOrigem = dadosOrigem.CodigoServicoOrigem;

            /*Lista de titular:
            Se o tipo de responsável financeiro for "Responsável Financeiro/Titular", preencher o Cod_pessoa_CAD e o percentual; Nesse caso, o campo CNPJ deverá ser NULO.
            Se o tipo de responsável financeiro for "Convênio//Parceiro", preencher o CNPJ e o percentual; Nesse caso, o campo Cod_pessoa_CAD deverá ser nulo*/
            objBenef.Titulares = new List<TitularesBeneficioData>();
            foreach (var item in dadosBeneficio.ResponsaveisFinanceiro)
            {
                if (item.TipoResponsavelFinanceiro == TipoResponsavelFinanceiro.ResponsavelFinanceiroTitular)
                {
                    objBenef.Titulares.Add(new TitularesBeneficioData()
                    {
                        CodigoPessoaCAD = PessoaDomainService.BuscarCodigoDePessoaNosDadosMestres(item.SeqPessoaJuridica, TipoPessoa.Juridica, null),
                        PercentualResponsavel = item.ValorPercentual,
                        CNPJ = null
                    });
                }
                else
                {
                    objBenef.Titulares.Add(new TitularesBeneficioData()
                    {
                        CodigoPessoaCAD = null,
                        PercentualResponsavel = item.ValorPercentual,
                        CNPJ = item.Cnpj
                    });
                }
            }

            //objBenef.Titulares = dadosBeneficio.ResponsaveisFinanceiro.Select(t => new TitularesBeneficioData
            //{
            //    CodigoPessoaCAD = PessoaDomainService.BuscarCodigoDePessoaNosDadosMestres(t.SeqPessoaJuridica, TipoPessoa.Juridica, null),
            //    PercentualResponsavel = t.ValorPercentual,
            //}).ToList();

            //Recebe cobrança: buscar valor do campo na configuração associada à associação do benefício na pessoa-atuação.
            objBenef.RecebeCobranca = dadosBeneficio.RecebeCobranca;

            //Justificativa não recebe cobrança: buscar valor do campo na configuração associada à associação do benefício na pessoa-atuação.
            objBenef.ObservacaoCobranca = dadosBeneficio.JustificativaNaoRecebeCobranca;

            var ciclos = ConfiguracaoEventoLetivoDomainService.BuscarDatasEventosLetivoPorPeriodo(objBenef.DataInicioBeneficioNova.GetValueOrDefault(), objBenef.DataFimBeneficioNova, dadosOrigem.SeqCursoOfertaLocalidadeTurno, dadosOrigem.TipoAtuacao == TipoAtuacao.Ingressante ? TipoAluno.Calouro : TipoAluno.Veterano);
            objBenef.CiclosLetivos = ciclos.Select(c => new CicloLetivoData
            {
                Ano = c.Ano,
                DataFim = c.DataFim,
                DataInicio = c.DataInicio,
                Semestre = c.Numero
            }).ToList();

            /*Se a rotina ST_ALTERA_CONTRATO_BENEFICIO_ACADEMICO realizou a integração com sucesso:
            Se houver contratos financeiros retornados na rotina, os mesmos deverão · ser associados ao
            benefício de acordo com RN_FIN_011 -Pessoa Atuação x Benefício - Inserção / Atualização dos
            Contratos Financeiros*/
            var contratos = IntegracaoFinanceiroService.AlterarVigenciaContratoBeneficio(objBenef);
            if (contratos != null)
            {
                foreach (var contrato in contratos)
                {
                    if (contrato.SeqContratoBeneficio.HasValue)
                    {
                        var seqCicloLetivo = CicloLetivoDomainService.BuscarCicloLetivoPorAnoNumero(contrato.Ano, contrato.Numero, dadosBeneficio.SeqInstituicaoEnsino);

                        var beneficioControleFinanceiro = new BeneficioControleFinanceiro()
                        {
                            SeqPessoaAtuacaoBeneficio = pessoaAtuacaoBeneficioVO.Seq,
                            SeqContratoBeneficioFinanceiro = contrato.SeqContratoBeneficio.GetValueOrDefault(),
                            SeqCicloLetivo = seqCicloLetivo,
                            DataInicio = contrato.DataInicioValidade,
                            DataFim = contrato.DataFimValidade,
                            DataExclusao = null
                        };
                        BeneficioControleFinanceiroDomainService.SaveEntity(beneficioControleFinanceiro);
                    }
                }
            }


        }

        /// <summary>
        /// Listar tipo de responsável financeiro select baseado no beneficio
        /// </summary>
        /// <param name="seqBeneficio">Sequencial do beneficio</param>
        /// <returns>Lista de tipos de responsaveis financeiros select</returns>
        public List<SMCDatasourceItem> BuscarTipoResponsavelFinanceiroSelect(long seqBeneficio)
        {
            var beneficio = this.BeneficioDomainService.BuscarBeneficio(seqBeneficio);

            List<SMCDatasourceItem> listaTipoResponsavelFinanceiro = new List<SMCDatasourceItem>();
            if (beneficio != null && beneficio.TipoResponsavelFinanceiro != null)
            {
                //Preenche todos os dados do datasourse
                if (beneficio.TipoResponsavelFinanceiro == TipoResponsavelFinanceiro.Todos)
                {
                    foreach (var item in Enum.GetValues(typeof(TipoResponsavelFinanceiro)))
                    {
                        //Verifica se o item é o nenhum ou todos e desta forma não retorna-lo
                        if (Convert.ToUInt32(item) != (int)TipoResponsavelFinanceiro.Nenhum && Convert.ToUInt32(item) != (int)TipoResponsavelFinanceiro.Todos)
                        {
                            listaTipoResponsavelFinanceiro.Add(new SMCDatasourceItem()
                            {
                                Seq = Convert.ToUInt32(item),
                                Descricao = SMCEnumHelper.GetDescription(SMCEnumHelper.GetEnum<TipoResponsavelFinanceiro>(item.ToString()))
                            });
                        }
                    }
                }
                else
                {
                    listaTipoResponsavelFinanceiro.Add(new SMCDatasourceItem()
                    {
                        Seq = (int)beneficio.TipoResponsavelFinanceiro,
                        Descricao = beneficio.TipoResponsavelFinanceiro.SMCGetDescription()
                    });
                }
            }

            return listaTipoResponsavelFinanceiro;
        }

        /// <summary>
        /// Enviar Notificação de uma pessoa atuação beneficio
        /// </summary>
        /// <param name="parametros">Parametros para envio da notificação</param>
        public void EnviarNotificacaoPessoaAtuacaoBenefcio(EnvioNotificacaoPessoaAtuacaoBeneficioVO parametros)
        {
            //Procurar a entidade vínculo da pessoa atuação benefício
            long? seqEntidadeVinculo = 0;
            var pessoaAtuacao = this.SearchProjectionByKey(new SMCSeqSpecification<PessoaAtuacaoBeneficio>(parametros.SeqPessoaAtuacaoBeneficio), p => new
            {
                p.SeqPessoaAtuacao,
                p.PessoaAtuacao.TipoAtuacao
            });

            if (pessoaAtuacao.TipoAtuacao == TipoAtuacao.Aluno)
            {
                seqEntidadeVinculo = this.AlunoDomainService.SearchProjectionByKey(new SMCSeqSpecification<Aluno>(pessoaAtuacao.SeqPessoaAtuacao),
                                                                                   p => p.Historicos.FirstOrDefault(f => f.Atual).SeqEntidadeVinculo);
            }
            else if (pessoaAtuacao.TipoAtuacao == TipoAtuacao.Ingressante)
            {
                seqEntidadeVinculo = this.IngressanteDomainService.SearchProjectionByKey(new SMCSeqSpecification<Ingressante>(pessoaAtuacao.SeqPessoaAtuacao),
                                                                                          p => p.SeqEntidadeResponsavel);
            }

            // Se encontrou a configuração para envio, prossegue com o envio
            if (seqEntidadeVinculo.Value > 0)
            {
                // Buscar dados da entidade de configuração notificação
                long seqConfiguracaoTipoNotificacao = EntidadeConfiguracaoNotificacaoDomainService.BuscarSeqConfiguracaoNotificacaoAtivo(seqEntidadeVinculo.Value, parametros.TokenTipoNotificacao);
                if (seqConfiguracaoTipoNotificacao > 0)
                {
                    // Monta o Data para o serviço de notificação
                    NotificacaoEmailData data = new NotificacaoEmailData()
                    {
                        SeqConfiguracaoNotificacao = seqConfiguracaoTipoNotificacao,
                        DadosMerge = parametros.DadosMerge,
                        DataPrevistaEnvio = DateTime.Now,
                        PrioridadeEnvio = PrioridadeEnvio.QuandoPossivel,
                    };

                    // Chama o serviço de envio de notificação
                    long seqNotificacaoEnviada = this.NotificacaoService.SalvarNotificacao(data);

                    // Buscar a lista de sequencial da notificação-email-destinatário enviada
                    var envioDestinatario = this.NotificacaoService.BuscaNotificacaoEmailDestinatario(seqNotificacaoEnviada);
                    if (envioDestinatario.Count == 0)
                        throw new PessoaAtuacaoBeneficioEnvioNotificacaoException(parametros.TokenTipoNotificacao);

                    foreach (var item in envioDestinatario)
                    {
                        // Salva a referencia da notificação enviada
                        BeneficioEnvioNotificacao envio = new BeneficioEnvioNotificacao()
                        {
                            SeqPessoaAtuacaoBeneficio = parametros.SeqPessoaAtuacaoBeneficio,
                            SeqNotificacaoEmailDestinatario = item.Seq
                        };
                        this.BeneficioEnvioNotificacaoDomainService.InsertEntity(envio);
                    }
                }
            }
        }

        /// <summary>
        /// Atualiza as datas de término dos benefícos com conessão até o final do curso
        /// </summary>
        /// <param name="filtro">Fitlro com o sequencial do historico de agendamento para o feedack</param>
        public void AtualizarTerminoBeneficio(ISMCWebJobFilterModel filtro)
        {
            var servico = Create<Jobs.AtualizacaoTerminoBeneficioWebJob>();
            servico.Execute(filtro);
        }

        /// <summary>
        /// Buscar dados das notificações da pessoa atuação benefício
        /// </summary>
        /// <param name="seqPessoaAtuacaoBeneficio">Sequencial pessoa atuação beneficio</param>
        /// <returns>Lista de todas as notificações da pessoa atuação benefício</returns>
        public PessoaAtuacaoBeneficioVO BuscarNotificacoesPessoaAtuacaoBeneficio(long seqPessoaAtuacaoBeneficio)
        {
            BeneficioEnvioNotificacaoFilterSpecification spec = new BeneficioEnvioNotificacaoFilterSpecification() { SeqPessoaAtuacaoBeneficio = seqPessoaAtuacaoBeneficio };
            List<long> seqsNotificacaoEmailDestinatario = BeneficioEnvioNotificacaoDomainService.SearchProjectionBySpecification(spec, p => p.SeqNotificacaoEmailDestinatario).ToList();

            NotificacaoEmailDestinatarioFiltroData filtro = new NotificacaoEmailDestinatarioFiltroData() { SeqsNotificacoesEmailDestinatario = seqsNotificacaoEmailDestinatario };

            var notificacoes = this.NotificacaoService.ConsultaNotificacoes(filtro).OrderByDescending(o => o.DataProcessamento).ToList();

            PessoaAtuacaoBeneficioVO pessoaAtuacaoBeneficioVO = new PessoaAtuacaoBeneficioVO() { Notificacoes = notificacoes.TransformList<BeneficioEnvioNotificacaoVO>() };

            return pessoaAtuacaoBeneficioVO;
        }

        /// <summary>
        /// Buscar as condições de pagamento para a Pessoa atuação
        /// </summary>
        /// <param name="seqPessoaAtuacao">Sequencial da Pessoa Atuação</param>
        /// <returns>Condicção de pagamento da pessoa atuação</returns>
        private string BuscarDadosCondicaoPagamentoCabecalho(long seqPessoaAtuacao, TipoAtuacao tipoAtuacao)
        {
            string retorno = string.Empty;

            if (tipoAtuacao == TipoAtuacao.Aluno)
            {
                /*2.Se o tipo de atuação for aluno,
                2.1.Buscar a quantidade de parcelas de acordo com vw_parcela_final_matricula_ACADEMICO, passando como
                parâmetro o código migração do aluno em questão.*/
                var codigoAlunoMigracao = new int[] { this.AlunoDomainService.SearchProjectionByKey(seqPessoaAtuacao, p => p.CodigoAlunoMigracao.Value) };
                retorno = FinanceiroService.BuscarDataVencimentoBeneficios(codigoAlunoMigracao).FirstOrDefault()?.NumeroParcelas.ToString();
            }
            else
            {
                /*1.Se tipo de atuação for ingressante,
                1.1.Buscar a quantidade de parcelas da condição de pagamento, de acordo com RN_MAT_076 - Exibição dados
                financeiros condição pagamento, associada à solicitação do ingressante em questão.Somente se a solicitação de
                matrícula possuir arquivo do termo de adesão.*/
                CondicaoPagamentoAcademicoVO condicaoPagamento = new CondicaoPagamentoAcademicoVO();
                var dadosIngressante = this.IngressanteDomainService.SearchProjectionByKey(seqPessoaAtuacao, p => new
                {
                    p.SolicitacoesServico.FirstOrDefault().Seq,
                    p.SolicitacoesServico.FirstOrDefault().GrupoEscalonamento.Itens.OrderByDescending(o => o.Escalonamento.ProcessoEtapa.Ordem).FirstOrDefault().Escalonamento.ProcessoEtapa.Ordem
                });

                if (dadosIngressante.Ordem > 1)
                {
                    condicaoPagamento = SolicitacaoMatriculaDomainService.BuscarCondicaoPagamentoAcademicoCabecalho(dadosIngressante.Seq);
                }

                retorno = condicaoPagamento?.QuantidadeParcelas.ToString();
            }

            return retorno;
        }

        public DateTime BuscarDataAdmissaoAluno(long seqPessoaAtuacao)
        {
            var spec = new AlunoHistoricoFilterSpecification { Atual = true, SeqAluno = seqPessoaAtuacao };

            var historico = AlunoHistoricoDomainService.SearchByKey(spec);

            return new DateTime(historico.DataAdmissao.Year, historico.DataAdmissao.Month, 1);
        }

    }
}