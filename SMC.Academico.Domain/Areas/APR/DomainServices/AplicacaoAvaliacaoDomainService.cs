using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Academico.Common.Areas.APR.Exceptions.AplicacaoAvaliacao;
using SMC.Academico.Common.Areas.APR.Includes;
using SMC.Academico.Common.Areas.MAT.Constants;
using SMC.Academico.Common.Areas.ORT.Exceptions;
using SMC.Academico.Common.Constants;
using SMC.Academico.Domain.Areas.ALN.DomainServices;
using SMC.Academico.Domain.Areas.ALN.Models;
using SMC.Academico.Domain.Areas.ALN.Specifications;
using SMC.Academico.Domain.Areas.APR.Models;
using SMC.Academico.Domain.Areas.APR.Specifications;
using SMC.Academico.Domain.Areas.APR.ValueObjects;
using SMC.Academico.Domain.Areas.CAM.DomainServices;
using SMC.Academico.Domain.Areas.DCT.DomainServices;
using SMC.Academico.Domain.Areas.DCT.Specifications;
using SMC.Academico.Domain.Areas.DCT.ValueObjects;
using SMC.Academico.Domain.Areas.ORG.DomainServices;
using SMC.Academico.Domain.Areas.ORT.DomainServices;
using SMC.Academico.Domain.Areas.ORT.Specifications;
using SMC.Academico.Domain.Areas.ORT.ValueObjects;
using SMC.Academico.Domain.Areas.PES.DomainServices;
using SMC.Framework;
using SMC.Framework.Domain;
using SMC.Framework.Exceptions;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Specification;
using SMC.Framework.UnitOfWork;
using SMC.Notificacoes.Common.Areas.NTF.Enums;
using SMC.Notificacoes.ServiceContract.Areas.NTF.Data;
using SMC.Notificacoes.ServiceContract.Areas.NTF.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Transactions;

namespace SMC.Academico.Domain.Areas.APR.DomainServices
{
    public class AplicacaoAvaliacaoDomainService : AcademicoContextDomain<AplicacaoAvaliacao>
    {
        #region [ Query ]

        private string _bancasAgendadasQuery = @"declare
	                            @SEQ_TIPO_EVENTO bigint,
	                            @DAT_INICIO datetime,
	                            @DAT_FIM datetime,
	                            @IND_SITUACAO_BANCA smallint, -- CRIAR ENUM 1 (Canceladas), 2 (Não canceladas), 3 (Sem ata anexada) e 4 (Todas)
	                            @IND_LANCAMENTO_NOTA smallint

                            set @SEQ_TIPO_EVENTO = {2}
                            set	@DAT_INICIO = '{3}'
                            set @DAT_FIM = '{4}'
                            set @IND_SITUACAO_BANCA = {5}
                            set @IND_LANCAMENTO_NOTA = {6}

                            select
	                            aa.seq_aplicacao_avaliacao as SeqAplicacaoAvaliacao,
	                            co.seq_entidade_curso as SeqEntidadeCurso,
	                            ecol.nom_entidade as NomeCursoLocalidade,
	                            t.dsc_turno as DescricaoTurno,
	                            case
		                            when pdp.nom_social is not null then RTRIM(pdp.nom_social) + ' (' + rtrim(pdp.nom_pessoa) + ')'
		                            else pdp.nom_pessoa
	                            end as NomeAluno,
	                            tt.dsc_tipo_trabalho as DescricaoTipoTrabalho,
	                            te.seq_tipo_evento as SeqTipoEvento,
	                            te.dsc_tipo_evento as DescricaoTipoEvento,
	                            aa.dsc_local as DescricaoLocal,
	                            aa.dat_inicio_aplicacao_avaliacao as DataInicioAplicacaoAvaliacao,

                                case
		                            when pbdpi.dsc_titulo is not null then pbdpi.dsc_titulo
		                            else ta.dsc_titulo
	                            end as DescricaoTitulo,

	                            -- ta.dsc_titulo as DescricaoTitulo,
	                            ta.ind_potencial_patente as PotencialPatente,
	                            ta.ind_potencial_registro_software as PotencialRegistroSoftware,
	                            ta.ind_potencial_negocio as PotencialNegocio,
	                            eai.dsc_escala_apuracao_item as DescricaoEscalaApuracaoItem,
	                            aa.dat_cancelamento as DataCancelamento,
	                            aa.dsc_motivo_cancelamento as DescricaoCancelamento,
                                apu.num_nota as Nota,
                                egp.nom_entidade as NomeEntidadeResponsavel,
                                cc.dsc_componente_curricular as DescricaoComponenteCurricular
                            from	APR.avaliacao a
                            join	APR.aplicacao_avaliacao aa
		                            on a.seq_avaliacao = aa.seq_avaliacao
                            join	APR.origem_avaliacao oa
		                            on aa.seq_origem_avaliacao = oa.seq_origem_avaliacao
                            join	ORT.trabalho_academico_divisao_componente tadc
		                            on oa.seq_origem_avaliacao = tadc.seq_origem_avaliacao

                            join    CUR.divisao_componente dc
                                    on tadc.seq_divisao_componente = dc.seq_divisao_componente
                            join    CUR.configuracao_componente cfc
                                    on dc.seq_configuracao_componente = cfc.seq_configuracao_componente
                            join    CUR.componente_curricular cc
                                    on cfc.seq_componente_curricular = cc.seq_componente_curricular

                            join	ORT.trabalho_academico ta
		                            on tadc.seq_trabalho_academico = ta.seq_trabalho_academico
                            join	ORT.trabalho_academico_autoria taa
		                            on ta.seq_trabalho_academico = taa.seq_trabalho_academico
                            join	ALN.aluno al
		                            on taa.seq_atuacao_aluno = al.seq_pessoa_atuacao
                            join	ALN.aluno_historico ah
		                            on al.seq_pessoa_atuacao = ah.seq_atuacao_aluno
		                            and ah.ind_atual = 1
                            join	CSO.curso_oferta_localidade_turno colt
		                            on ah.seq_curso_oferta_localidade_turno = colt.seq_curso_oferta_localidade_turno
                            join	CSO.curso_oferta_localidade col
		                            on colt.seq_entidade_curso_oferta_localidade = col.seq_entidade
                            join	ORG.entidade ecol
		                            on col.seq_entidade = ecol.seq_entidade
                            join	CSO.curso_oferta co
		                            on col.seq_curso_oferta = co.seq_curso_oferta
                            join	CSO.curso cu
		                            on co.seq_entidade_curso = cu.seq_entidade
                            join	ORG.hierarquia_entidade_item hei
		                            on cu.seq_entidade = hei.seq_entidade
                            join	ORG.hierarquia_entidade he
		                            on hei.seq_hierarquia_entidade = he.seq_hierarquia_entidade
                            join	ORG.tipo_hierarquia_entidade the
		                            on he.seq_tipo_hierarquia_entidade = the.seq_tipo_hierarquia_entidade
		                            and the.idt_dom_tipo_visao = 1	-- Visão organizacional
                            join	ORG.hierarquia_entidade_item heip
		                            on hei.seq_hierarquia_entidade_item_superior = heip.seq_hierarquia_entidade_item
                            join	ORG.hierarquia_entidade_item heigp
		                            on heip.seq_hierarquia_entidade_item_superior = heigp.seq_hierarquia_entidade_item
                            join    ORG.entidade egp
                                    on heigp.seq_entidade = egp.seq_entidade
                            join	CSO.turno t
		                            on colt.seq_turno = t.seq_turno
                            join	PES.pessoa_atuacao pa
		                            on al.seq_pessoa_atuacao = pa.seq_pessoa_atuacao
                            join	PES.pessoa_dados_pessoais pdp
		                            on pa.seq_pessoa_dados_pessoais = pdp.seq_pessoa_dados_pessoais
                            join	ORT.tipo_trabalho tt
		                            on ta.seq_tipo_trabalho = tt.seq_tipo_trabalho
                            join	CALENDARIO..tipo_evento te
		                            on aa.seq_tipo_evento_agd = te.seq_tipo_evento
                            left join	APR.apuracao_avaliacao apu
			                            on aa.seq_aplicacao_avaliacao = apu.seq_aplicacao_avaliacao
			                            and ah.seq_aluno_historico = apu.seq_aluno_historico
                            left join	APR.escala_apuracao_item eai
			                            on apu.seq_escala_apuracao_item = eai.seq_escala_apuracao_item

                            left join	ORT.publicacao_bdp pbdp
			                            on pbdp.seq_trabalho_academico = ta.seq_trabalho_academico
                            left join	ORT.publicacao_bdp_idioma pbdpi
			                            on pbdp.seq_publicacao_bdp = pbdpi.seq_publicacao_bdp
                                        and pbdpi.ind_idioma_trabalho = 1

                            where	a.idt_dom_tipo_avaliacao = 1 -- Banca
                            and		oa.idt_dom_tipo_origem_avaliacao = 3 -- Trabalho acadêmico
                            and		heigp.seq_entidade in ({0})
                            and	(	'0' = '{1}'
                            or		cu.seq_nivel_ensino in ({1}))
                            and	(	@SEQ_TIPO_EVENTO is null
                            or		aa.seq_tipo_evento_agd = @SEQ_TIPO_EVENTO)
                            and		aa.dat_inicio_aplicacao_avaliacao between @DAT_INICIO and @DAT_FIM
                            and (	(@IND_SITUACAO_BANCA = 1 and aa.dat_cancelamento is not null) -- canceladas
                            or		(@IND_SITUACAO_BANCA = 2 and aa.dat_cancelamento is null) -- não canceladas
                            or		(@IND_SITUACAO_BANCA = 3 and apu.seq_arquivo_anexado_ata_defesa is null) -- sem ata
                            or		@IND_SITUACAO_BANCA = 4) -- todas
                            and	(	(@IND_LANCAMENTO_NOTA = 1 and apu.seq_apuracao_avaliacao is not null) --  and apu.seq_arquivo_anexado_ata_defesa is not null (Task 38162)
                            or		(@IND_LANCAMENTO_NOTA = 0 and apu.seq_apuracao_avaliacao is null)
                            or      @IND_LANCAMENTO_NOTA = 3)";

        private string _membrosBancaQuery = @"-- para cada registro de banca, executar a query para buscar os membros de banca
                                            -- parametro que varia na query é o seq_aplicacao_avaliacao
                                            declare @SEQ_APLICACAO_AVALIACAO bigint
                                            set @SEQ_APLICACAO_AVALIACAO = {0}

                                            select
                                                m.seq_atuacao_colaborador as SeqColaborador,
                                                m.seq_instituicao_externa as SeqInstituicaoExterna,
	                                            case when pdp.nom_pessoa is not null then pdp.nom_pessoa else m.nom_colaborador end as Nome,
	                                            m.idt_dom_tipo_membro_banca TipoMembroBanca,
                                                case when ie.nom_instituicao_externa is not null then ie.nom_instituicao_externa else m.nom_instituicao_externa end as Instituicao,
                                                m.dsc_complemento_instituicao as ComplementoInstituicao
                                            from	APR.membro_banca_examinadora m
                                            left join	DCT.colaborador c
		                                            on m.seq_atuacao_colaborador = c.seq_pessoa_atuacao
                                            left join	PES.pessoa_atuacao pa
		                                            on c.seq_pessoa_atuacao = pa.seq_pessoa_atuacao
                                            left join	PES.pessoa_dados_pessoais pdp
		                                            on pa.seq_pessoa_dados_pessoais = pdp.seq_pessoa_dados_pessoais
                                            left join	DCT.instituicao_externa ie
		                                            on m.seq_instituicao_externa = ie.seq_instituicao_externa
                                            where	m.seq_aplicacao_avaliacao = @SEQ_APLICACAO_AVALIACAO
                                            and (   m.ind_participou is null or m.ind_participou = 1)
                                            order by Nome
                                            ";

        private string _areaConhecimentoQuery = @"
                                        -- para cada registro de banca, executar a query para buscar as áreas de conhecimento do curso
                                        -- parametro que varia na query é o seq_entidade_curso
                                        declare @SEQ_ENTIDADE_CURSO bigint
                                        set @SEQ_ENTIDADE_CURSO = {0}

                                        select	c.dsc_classificacao as AreaConhecimentoDescricao
                                        from	CSO.entidade_classificacao ec
                                        join	CSO.classificacao c
		                                        on ec.seq_classificacao = c.seq_classificacao
                                        join	CSO.hierarquia_classificacao h
		                                        on c.seq_hierarquia_classificacao = h.seq_hierarquia_classificacao
                                        join	CSO.tipo_hierarquia_classificacao th
		                                        on h.seq_tipo_hierarquia_classificacao = th.seq_tipo_hierarquia_classificacao
                                        where	ec.seq_entidade = @SEQ_ENTIDADE_CURSO
                                        and		th.dsc_token = 'HIERARQUIA_CAPES'";

        #endregion [ Query ]

        #region [ Domain Services ]

        private HistoricoEscolarColaboradorDomainService HistoricoEscolarColaboradorDomainService => Create<HistoricoEscolarColaboradorDomainService>();

        private ConfiguracaoEventoLetivoDomainService ConfiguracaoEventoLetivoDomainService => Create<ConfiguracaoEventoLetivoDomainService>();

        private PessoaAtuacaoDomainService PessoaAtuacaoDomainService => Create<PessoaAtuacaoDomainService>();

        private HistoricoEscolarDomainService HistoricoEscolarDomainService => Create<HistoricoEscolarDomainService>();

        private InstituicaoExternaDomainService InstituicaoExternaDomainService => Create<InstituicaoExternaDomainService>();

        private ApuracaoAvaliacaoDomainService ApuracaoAvaliacaoDomainService => Create<ApuracaoAvaliacaoDomainService>();

        private InstituicaoNivelTipoTrabalhoDomainService InstituicaoNivelTipoTrabalhoDomainService => Create<InstituicaoNivelTipoTrabalhoDomainService>();

        private AlunoHistoricoSituacaoDomainService AlunoHistoricoSituacaoDomainService => Create<AlunoHistoricoSituacaoDomainService>();

        private OrigemAvaliacaoDomainService OrigemAvaliacaoDomainService => Create<OrigemAvaliacaoDomainService>();

        private AvaliacaoDomainService AvaliacaoDomainService => Create<AvaliacaoDomainService>();

        private AlunoDomainService AlunoDomainService => Create<AlunoDomainService>();

        private TrabalhoAcademicoDomainService TrabalhoAcademicoDomainService => Create<TrabalhoAcademicoDomainService>();

        private InstituicaoNivelTipoMembroBancaDomainService InstituicaoNivelTipoMembroBancaDomainService => Create<InstituicaoNivelTipoMembroBancaDomainService>();

        private PlanoEstudoItemDomainService PlanoEstudoItemDomainService => Create<PlanoEstudoItemDomainService>();

        private EntidadeConfiguracaoNotificacaoDomainService EntidadeConfiguracaoNotificacaoDomainService => Create<EntidadeConfiguracaoNotificacaoDomainService>();

        private INotificacaoService NotificacaoService => this.Create<INotificacaoService>();

        private ColaboradorDomainService ColaboradorDomainService => this.Create<ColaboradorDomainService>();

        #endregion [ Domain Services ]

        public bool ExibirMensagemAprovacaoPublicacaoBibliotecaObrigatoria(LancamentoNotaBancaExaminadoraVO lancamentoNotaBancaExaminadoraVO)
        {
            if (CriterioAprovacaoAprovado(lancamentoNotaBancaExaminadoraVO))
            {
                // Recupera o tipo de trabalho
                var tipoTrabalho = TrabalhoAcademicoDomainService.SearchProjectionByKey(lancamentoNotaBancaExaminadoraVO.SeqTrabalhoAcademico.Value, x => x.SeqTipoTrabalho);

                // Recupera a configuração
                var publicacaoBibliotecaObrigatoria = InstituicaoNivelTipoTrabalhoDomainService.SearchProjectionByKey(new InstituicaoNivelTipoTrabalhoFilterSpecification
                {
                    SeqInstituicaoEnsino = lancamentoNotaBancaExaminadoraVO.SeqInstituicaoEnsino,
                    SeqNivelEnsino = lancamentoNotaBancaExaminadoraVO.SeqNivelEnsino,
                    SeqTipoTrabalho = tipoTrabalho,
                }, x => x.PublicacaoBibliotecaObrigatoria);

                return publicacaoBibliotecaObrigatoria;
            }
            return false;
        }

        public List<BancasAgendadasVO> BuscarBancasAgendadasPorPeriodo(BancasAgendadasFiltroVO filtro)
        {

            filtro.DataInicio = filtro.DataInicio.AddHours(00).AddMinutes(00).AddSeconds(00);
            if (filtro.DataFim.HasValue)
                filtro.DataFim = filtro.DataFim.Value.AddHours(23).AddMinutes(59).AddSeconds(59);

            var auxDatInicio = filtro.DataInicio.ToString("yyyy-MM-dd HH:mm:ss");
            var auxDatFim = !filtro.DataFim.HasValue ? DateTime.MaxValue.ToString("yyyy-MM-dd HH:mm:ss") : filtro.DataFim.GetValueOrDefault().ToString("yyyy-MM-dd HH:mm:ss");

            /*Consistência para permitir pesquisar quando se passa como filtro a data fim igual a data início
            ou seja, permitir listar bancas agendadas em um dia específico*/
            if (filtro.DataFim.HasValue && auxDatInicio == auxDatFim)
            {
                auxDatInicio = filtro.DataInicio.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.CurrentCulture);
                auxDatFim = filtro.DataFim.Value.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.CurrentCulture);
            }


            var query = string.Format(_bancasAgendadasQuery, new object[] {
                string.Join(",", filtro.SeqEntidadesResponsaveis),
                !filtro.SeqNiveisEnsino.SMCAny() ? "0" : string.Join(",",filtro.SeqNiveisEnsino),
                !filtro.SeqTipoEvento.HasValue ? "null" : filtro.SeqTipoEvento.ToString(),
                auxDatInicio,
                auxDatFim,
                !filtro.SituacaoBanca.HasValue ? 4 : Convert.ToInt16(filtro.SituacaoBanca),
                !filtro.ExibirBancasComNota.HasValue ?  3 : (filtro.ExibirBancasComNota.Value ? 1 : 0)
            });

            var bancasAgendadasPeriodo = this.RawQuery<BancasAgendadasVO>(query).ToList();

            foreach (var bancaAgendada in bancasAgendadasPeriodo)
            {
                //Preenche lista de membros da banca
                var membrosBanca = this.RawQuery<MembroBancaExaminadoraVO>(string.Format(_membrosBancaQuery, bancaAgendada.SeqAplicacaoAvaliacao));
                bancaAgendada.MembrosBancaExaminadora = membrosBanca;

                //Preenche lista de area de conhecimento
                var areaConhecimento = this.RawQuery<BancasAgendadasAreaConhecimentoVO>(string.Format(_areaConhecimentoQuery, bancaAgendada.SeqEntidadeCurso));
                bancaAgendada.AreasConhecimento = areaConhecimento;

                bancaAgendada.OrdenacaoBancasAgendadasRelatorio = filtro.Ordenacao;

                var sb = new StringBuilder();
                sb.Append(bancaAgendada.PotencialNegocio == true ? "Negócio;" : "");
                sb.Append(bancaAgendada.PotencialPatente == true ? "Patente;" : "");
                sb.Append(bancaAgendada.PotencialRegistroSoftware == true ? "Registro de software" : "");

                bancaAgendada.PotencialPropriedadeIntelectual = sb.ToString().TrimEnd(';');

            }

            // Aplica o filtro por membro de banca
            if (filtro.SeqColaborador.HasValue)
                bancasAgendadasPeriodo = bancasAgendadasPeriodo.Where(b => b.MembrosBancaExaminadora.Any(m => m.SeqColaborador == filtro.SeqColaborador && (filtro.TipoMembroBanca == TipoMembroBanca.Nenhum || m.TipoMembroBanca == filtro.TipoMembroBanca))).ToList();

            return bancasAgendadasPeriodo;
        }

        public AvaliacaoTrabalhoAcademicoBancaExaminadoraVO BuscarAplicacaoAvaliacaoTrabalhoAcademicoBancaExaminadora(long seqAplicacaoAvaliacao, long seqTrabalhoAcademico)
        {
            var result = this.SearchProjectionByKey(new SMCSeqSpecification<AplicacaoAvaliacao>(seqAplicacaoAvaliacao),
                    x => new AvaliacaoTrabalhoAcademicoBancaExaminadoraVO
                    {
                        Seq = x.Seq,
                        SeqAvaliacao = x.SeqAvaliacao,
                        SeqTrabalhoAcademico = x.OrigemAvaliacao.DivisoesComponente.Select(s => s.SeqTrabalhoAcademico).FirstOrDefault(),
                        SeqOrigemAvaliacao = x.SeqOrigemAvaliacao,
                        SeqTipoEventoAgd = x.SeqTipoEventoAgd,
                        DataInicioAplicacaoAvaliacao = x.DataInicioAplicacaoAvaliacao,
                        Local = x.Local,
                        DataCancelamento = x.DataCancelamento,
                        MotivoCancelamento = x.MotivoCancelamento,
                        NotaConceito = (x.ApuracoesAvaliacao.FirstOrDefault().Nota.HasValue) ? x.ApuracoesAvaliacao.FirstOrDefault().Nota.ToString() : x.ApuracoesAvaliacao.FirstOrDefault().EscalaApuracaoItem.Descricao,
                        SeqNivelEnsino = x.OrigemAvaliacao.DivisoesComponente.FirstOrDefault().TrabalhoAcademico.SeqNivelEnsino,
                        Sigla = x.Sigla,
                        MembrosBancaExaminadora = x.MembrosBancaExaminadora.Select(m => new MembroBancaExaminadoraVO()
                        {
                            Seq = m.Seq,
                            SeqAplicacaoAvaliacao = m.SeqAplicacaoAvaliacao,
                            SeqColaborador = m.SeqColaborador,
                            NomeColaborador = m.NomeColaborador,
                            NomeInstituicaoExterna = m.NomeInstituicaoExterna,
                            Nome = m.Colaborador.DadosPessoais.Nome,
                            TipoMembroBanca = m.TipoMembroBanca,
                            SeqInstituicaoExterna = m.SeqInstituicaoExterna,
                            Instituicao = m.InstituicaoExterna.Nome,
                            Participou = m.Participou,
                            Sexo = m.Colaborador.DadosPessoais.Sexo,
                            ComplementoInstituicao = m.ComplementoInstituicao,
                        }).ToList()
                    });

            //Recupera instituições externas e tipos dos membros da banca
            result.MembrosBancaExaminadora.ForEach(m =>
            {
                m.InstituicoesExternas = InstituicaoExternaDomainService.BuscarInstituicaoExternaPorColaboradorSelect(new InstituicaoExternaFiltroVO()
                {
                    SeqColaborador = m.SeqColaborador,
                    Ativo = true
                });

                m.TiposMembroBanca = InstituicaoNivelTipoMembroBancaDomainService.BuscarTiposMembroBancaSelect(new InstituicaoNivelTipoMembroBancaFilterSpecification()
                {
                    SeqNivelEnsino = result.SeqNivelEnsino
                });
            });

            // Recupera os Seqs dos alunos
            var seqsAlunos = TrabalhoAcademicoDomainService.SearchProjectionByKey(seqTrabalhoAcademico, x => x.Autores.Select(a => a.SeqAluno)).ToList();

            // Verifica algum dos alunos está formado
            if (seqsAlunos != null && seqsAlunos.Any())
            {
                foreach (var seqAluno in seqsAlunos)
                {
                    var situacaoAluno = AlunoHistoricoSituacaoDomainService.BuscarSituacaoAtualAluno(seqAluno);
                    if (situacaoAluno?.TokenSituacaoMatricula == TOKENS_SITUACAO_MATRICULA.FORMADO)
                    {
                        result.AlunoFormado = true;
                        break;
                    }
                }
            }

            return result;
        }

        public AvaliacaoTrabalhoAcademicoBancaExaminadoraVO BuscarDetalhesCancelamentoAplicacaoAvaliacaoBancaExaminadora(long seqAplicacaoAvaliacao)
        {
            var result = this.SearchProjectionByKey(new SMCSeqSpecification<AplicacaoAvaliacao>(seqAplicacaoAvaliacao),
                    x => new AvaliacaoTrabalhoAcademicoBancaExaminadoraVO
                    {
                        Seq = x.Seq,
                        SeqAvaliacao = x.SeqAvaliacao,
                        DataCancelamento = x.DataCancelamento,
                        MotivoCancelamento = x.MotivoCancelamento
                    });
            return result;
        }

        public LancamentoNotaBancaExaminadoraVO BuscarLancamentoNotaBancaExaminadoraInsert(long seqAplicacaoAvaliacao)
        {
            var result = this.SearchProjectionByKey(new SMCSeqSpecification<AplicacaoAvaliacao>(seqAplicacaoAvaliacao),
                x => new LancamentoNotaBancaExaminadoraVO
                {
                    Seq = x.Seq,
                    SeqTrabalhoAcademico = x.OrigemAvaliacao.DivisoesComponente.Select(s => s.SeqTrabalhoAcademico).FirstOrDefault(),
                    SeqOrigemAvaliacao = x.SeqOrigemAvaliacao,
                    SeqComponenteCurricular = x.OrigemAvaliacao.DivisoesComponente.Select(s => s.DivisaoComponente.ConfiguracaoComponente).FirstOrDefault().SeqComponenteCurricular,
                    SeqNivelEnsino = x.OrigemAvaliacao.DivisoesComponente.FirstOrDefault().TrabalhoAcademico.SeqNivelEnsino,
                    SeqEscalaApuracao = x.OrigemAvaliacao.SeqEscalaApuracao,
                    SeqEscalaApuracaoItem = x.ApuracoesAvaliacao.FirstOrDefault().SeqEscalaApuracaoItem,
                    Nota = x.ApuracoesAvaliacao.FirstOrDefault().Nota,
                    NumeroDefesa = x.ApuracoesAvaliacao.FirstOrDefault().NumeroDefesa,
                    ArquivoAnexadoAtaDefesa = x.ApuracoesAvaliacao.FirstOrDefault().SeqArquivoAnexadoAtaDefesa.HasValue ? new SMCUploadFile
                    {
                        FileData = x.ApuracoesAvaliacao.FirstOrDefault().ArquivoAnexadoAtaDefesa.Conteudo,
                        GuidFile = x.ApuracoesAvaliacao.FirstOrDefault().ArquivoAnexadoAtaDefesa.UidArquivo.ToString(),
                        Name = x.ApuracoesAvaliacao.FirstOrDefault().ArquivoAnexadoAtaDefesa.Nome,
                        Size = x.ApuracoesAvaliacao.FirstOrDefault().ArquivoAnexadoAtaDefesa.Tamanho,
                        Type = x.ApuracoesAvaliacao.FirstOrDefault().ArquivoAnexadoAtaDefesa.Tipo
                    } : null,
                    //ApuracaoNota = x.OrigemAvaliacao.DivisoesComponente.Select(s => s.DivisaoComponente.ConfiguracaoComponente).FirstOrDefault().DivisoesMatrizCurricularComponente.FirstOrDefault().CriterioAprovacao.ApuracaoNota,
                    ApuracaoNota = x.OrigemAvaliacao.CriterioAprovacao.ApuracaoNota,
                    NotaMaxima = x.OrigemAvaliacao.CriterioAprovacao.NotaMaxima,
                    EscalaApuracaoItens = x.OrigemAvaliacao.CriterioAprovacao.EscalaApuracao.Itens.Select(s => new SMCDatasourceItem { Seq = s.Seq, Descricao = s.Descricao }).ToList(),
                    MembrosBancaExaminadora = x.MembrosBancaExaminadora.Select(m => new MembroBancaExaminadoraVO()
                    {
                        Seq = m.Seq,
                        SeqAplicacaoAvaliacao = m.SeqAplicacaoAvaliacao,
                        SeqColaborador = m.SeqColaborador,
                        Nome = m.Colaborador.DadosPessoais.Nome,
                        NomeColaborador = m.NomeColaborador,
                        TipoMembroBanca = m.TipoMembroBanca,
                        SeqInstituicaoExterna = m.SeqInstituicaoExterna,
                        Instituicao = m.InstituicaoExterna.Nome,
                        NomeInstituicaoExterna = m.NomeInstituicaoExterna,
                        Participou = m.Participou,
                        Sexo = m.Colaborador.DadosPessoais.Sexo,
                        ComplementoInstituicao = m.ComplementoInstituicao,
                    }).ToList(),
                    SeqTipoTrabalho = x.OrigemAvaliacao.DivisoesComponente.FirstOrDefault().TrabalhoAcademico.SeqTipoTrabalho
                });

            //Recupera instituições externas e tipos dos membros da banca
            result.MembrosBancaExaminadora.ForEach(m =>
            {
                m.InstituicoesExternas = InstituicaoExternaDomainService.BuscarInstituicaoExternaPorColaboradorSelect(new InstituicaoExternaFiltroVO()
                {
                    SeqColaborador = m.SeqColaborador,
                    Ativo = true
                });

                m.TiposMembroBanca = InstituicaoNivelTipoMembroBancaDomainService.BuscarTiposMembroBancaSelect(new InstituicaoNivelTipoMembroBancaFilterSpecification()
                {
                    SeqNivelEnsino = result.SeqNivelEnsino
                });
            });

            // Recupera a configuração
            result.PublicacaoBibliotecaObrigatoria = InstituicaoNivelTipoTrabalhoDomainService.SearchProjectionByKey(new InstituicaoNivelTipoTrabalhoFilterSpecification
            {
                SeqInstituicaoEnsino = result.SeqInstituicaoEnsino,
                SeqNivelEnsino = result.SeqNivelEnsino,
                SeqTipoTrabalho = result.SeqTipoTrabalho,
            }, x => x.PublicacaoBibliotecaObrigatoria);

            //if (!result.SeqTrabalhoAcademico.HasValue)
            //{
            //    throw new SMCApplicationException("Trabalho acadêmico não vinculado à aplicação avaliação");
            //}

            //var seqMatrizCurricular = TrabalhoAcademicoDomainService.SearchProjectionByKey(new SMCSeqSpecification<TrabalhoAcademico>(result.SeqTrabalhoAcademico.Value), p =>
            //    p.Autores.FirstOrDefault().Aluno
            //        .Historicos.FirstOrDefault(f => f.Atual)
            //        .HistoricosCicloLetivo.OrderByDescending(o => o.CicloLetivo.Ano).ThenByDescending(o => o.CicloLetivo.Numero).FirstOrDefault()
            //        .PlanosEstudo.FirstOrDefault(f => f.Atual)
            //        .SeqMatrizCurricularOferta);

            //if (!seqMatrizCurricular.HasValue)
            //{
            //    throw new SMCApplicationException("Aluno sem matriz");
            //}
            //if (!result.SeqComponenteCurricular.HasValue)
            //{
            //    throw new SMCApplicationException("Componente curricular não vinculado à aplicação avaliação");
            //}

            //var configComponente = DivisaoMatrizCurricularComponenteDomainService.SearchProjectionByKey(new DivisaoMatrizCurricularComponenteFilterSpecification()
            //{
            //    SeqComponenteCurricular = result.SeqComponenteCurricular,
            //    SeqMatrizCurricular = seqMatrizCurricular
            //}, p => new
            //{
            //    ApuracaoNota = (bool?)p.CriterioAprovacao.ApuracaoNota,
            //    NotaMaxima = p.CriterioAprovacao.NotaMaxima,
            //    EscalaApuracaoItens = p.CriterioAprovacao.EscalaApuracao.Itens.Select(s => new SMCDatasourceItem { Seq = s.Seq, Descricao = s.Descricao }).ToList()
            //});

            //if (configComponente == null)
            //{
            //    throw new SMCApplicationException("Componente curricular não vinculado à matriz curricular do aluno");
            //}

            //result.ApuracaoNota = configComponente.ApuracaoNota;
            //result.NotaMaxima = configComponente.NotaMaxima;
            //result.EscalaApuracaoItens = configComponente.EscalaApuracaoItens;

            return result;
        }

        /// <summary>
        /// Verificar se situação de aprovação é igual "Aprovado", de acordo com o
        /// critério associado ao componente da avaliação em questão e a nota lançada.
        /// </summary>
        /// <param name="seqAplicacaoAvaliacao"></param>
        /// <param name="nota"></param>
        /// <returns>Caso aprovado retorna true, reprovado false</returns>
        private bool SituacaoAprovacaoNota(long seqAplicacaoAvaliacao, decimal nota)
        {
            if (seqAplicacaoAvaliacao <= 0 || nota <= 0) { return false; }

            var criterio = this.SearchProjectionByKey(new SMCSeqSpecification<AplicacaoAvaliacao>(seqAplicacaoAvaliacao),
                    x => x.OrigemAvaliacao.CriterioAprovacao);

            if (criterio.NotaMaxima.HasValue)
                return (nota / criterio.NotaMaxima.Value) * 100 >= criterio.PercentualNotaAprovado;
            else
                return (nota / 100) * 100 >= criterio.PercentualNotaAprovado;
        }

        /// <summary>
        ///  Verificar se situação de aprovação é igual "Aprovado", de acordo com o
        /// critério associado ao componente da avaliação em questão e conceito lançado.
        /// </summary>
        /// <param name="seqAplicacaoAvaliacao"></param>
        /// <param name="seqEscalaApuracaoItem"></param>
        /// <returns>Caso aprovado retorna true, reprovado false</returns>
        private bool SituacaoAprovacaoConceito(long seqAplicacaoAvaliacao, long seqEscalaApuracaoItem)
        {
            if (seqAplicacaoAvaliacao <= 0 || seqEscalaApuracaoItem <= 0) { return false; }

            var escalaApuracaoItem = this.SearchProjectionByKey(new SMCSeqSpecification<AplicacaoAvaliacao>(seqAplicacaoAvaliacao),

                x => x.OrigemAvaliacao.CriterioAprovacao.EscalaApuracao.Itens);

            var item = escalaApuracaoItem.FirstOrDefault(e => e.Seq == seqEscalaApuracaoItem);

            if (item == null) { return false; }

            return item.Aprovado;
        }

        /// <summary>
        /// Verificar se situação de aprovação é igual "Aprovado", de acordo com o
        /// critério associado ao componente da avaliação em questão e a nota ou conceito lançado.
        /// </summary>
        public bool CriterioAprovacaoAprovado(LancamentoNotaBancaExaminadoraVO lancamentoNotaBancaExaminadoraVO)
        {
            bool criterioAprovacaoAprovado = false;

            if (lancamentoNotaBancaExaminadoraVO.ApuracaoNota.GetValueOrDefault())
            {
                criterioAprovacaoAprovado = SituacaoAprovacaoNota(lancamentoNotaBancaExaminadoraVO.SeqAplicacaoAvaliacao.Value, lancamentoNotaBancaExaminadoraVO.Nota.Value);
            }
            else
            {
                criterioAprovacaoAprovado = SituacaoAprovacaoConceito(lancamentoNotaBancaExaminadoraVO.SeqAplicacaoAvaliacao.Value, lancamentoNotaBancaExaminadoraVO.SeqEscalaApuracaoItem.Value);
            }

            return criterioAprovacaoAprovado;
        }

        public bool ApuracaoNota(long seqAplicacaoAvaliacao)
        {
            var result = this.SearchProjectionByKey(new SMCSeqSpecification<AplicacaoAvaliacao>(seqAplicacaoAvaliacao),

                    x => x.OrigemAvaliacao.CriterioAprovacao.ApuracaoNota);

            return result;
        }

        public AlunoHistorico BuscarHistoricoAtualAluno(long seqAplicacaoAvaliacao)
        {
            var alunoHistorico = this.SearchProjectionByKey(new SMCSeqSpecification<AplicacaoAvaliacao>(seqAplicacaoAvaliacao),

                   x => x.OrigemAvaliacao.HistoricosEscolares.FirstOrDefault().AlunoHistorico);

            return alunoHistorico;
        }

        #region [ Salvar Aplicacao Avaliacao do trabalho acadêmico ]

        public long SalvarAplicacaoAvaliacaoTrabalhoAcademicoBancaExaminadora(AvaliacaoTrabalhoAcademicoBancaExaminadoraVO avaliacaoBancaExaminadoraVO)
        {
            var apuracaoEfetuada = false;
            var aplicacaoAvaliacao = avaliacaoBancaExaminadoraVO.Transform<AplicacaoAvaliacao>();


            ValidarInclusao(avaliacaoBancaExaminadoraVO, aplicacaoAvaliacao);

            if (aplicacaoAvaliacao.Seq > 0)
            {
                var aplicacaoAvaliacaoBanco = this.SearchProjectionByKey(avaliacaoBancaExaminadoraVO.Seq.GetValueOrDefault(), x => new
                {
                    SeqEscalaApuracaoItem = x.ApuracoesAvaliacao.FirstOrDefault().SeqEscalaApuracaoItem,
                    Nota = x.ApuracoesAvaliacao.FirstOrDefault().Nota,
                });
                apuracaoEfetuada = aplicacaoAvaliacaoBanco != null && (aplicacaoAvaliacaoBanco.Nota.HasValue || aplicacaoAvaliacaoBanco.SeqEscalaApuracaoItem.HasValue);

                // Task 36436: SE esta já tiver sido apurada, verificar se existe membro na banca.... se não houver não permitir a ação e emitir a mensagem de erro
                if (apuracaoEfetuada && (aplicacaoAvaliacao.MembrosBancaExaminadora == null || !aplicacaoAvaliacao.MembrosBancaExaminadora.Any()))
                    throw new AplicacaoAvaliacaoMembrosBancaFaltantesException();
            }

            aplicacaoAvaliacao.OrigemAvaliacao = BuscarOrigemAvaliacao(aplicacaoAvaliacao.SeqOrigemAvaliacao);
            avaliacaoBancaExaminadoraVO.SeqTrabalhoAcademico = BuscarSeqTrabalhoAcademico(aplicacaoAvaliacao.OrigemAvaliacao);

            /*Favor implemetar as alterações listadas abaixo na edição de banca examinadora:
            Deverá ser realizado SOMENTE quando a apuração do resultado já tiver sido realizada para a avaliação em questão.

            Na inclusão de um novo membro o atributo ind_participou deverá ser setado com o valor "1".
            */
            aplicacaoAvaliacao.MembrosBancaExaminadora?.SMCForEach(x => x.Participou = (x.Seq == 0) ? (apuracaoEfetuada ? (bool?)true : null) : x.Participou);

            if (aplicacaoAvaliacao.Seq == 0)
            {
                aplicacaoAvaliacao.Avaliacao = GerarNovaAvaliacao();

                aplicacaoAvaliacao.Avaliacao.SeqInstituicaoEnsino = avaliacaoBancaExaminadoraVO.SeqInstituicaoEnsino;

                aplicacaoAvaliacao.Sigla = Guid.NewGuid().ToString();

                aplicacaoAvaliacao.EntregaWeb = false;

                aplicacaoAvaliacao.SeqEventoAgd = null;

                aplicacaoAvaliacao.Avaliacao.Descricao = MascaraDescricaoAvaliacao(avaliacaoBancaExaminadoraVO.SeqTrabalhoAcademico.Value, aplicacaoAvaliacao.OrigemAvaliacao);

                aplicacaoAvaliacao.Avaliacao.AplicacoesAvaliacao.Add(aplicacaoAvaliacao);

                AvaliacaoDomainService.SaveEntity(aplicacaoAvaliacao.Avaliacao);

                aplicacaoAvaliacao.Sigla = $"B{aplicacaoAvaliacao.Seq}";

                AvaliacaoDomainService.UpdateFields(aplicacaoAvaliacao, a => a.Sigla);
            }
            else
            {
                // Salva a aplicação avaliaçãqo
                SaveEntity(aplicacaoAvaliacao);

                // Caso tenha sido feita a apuração:
                // Atualiza os colaboradores no histórico
                // Atualiza o Ciclo Letivo de acordo com a data de defesa
                if (apuracaoEfetuada)
                {
                    var dadosHistorico = HistoricoEscolarDomainService.SearchProjectionByKey(new HistoricoEscolarFilterSpecification
                    {
                        SeqOrigemAvaliacao = aplicacaoAvaliacao.SeqOrigemAvaliacao
                    }, x => new
                    {
                        Seq = x.Seq,
                        SeqCicloLetivo = x.SeqCicloLetivo,
                        DataExame = x.DataExame,
                        Colaboradores = x.Colaboradores
                    });

                    if (dadosHistorico != null)
                    {
                        // Remover os que não são mais membros
                        dadosHistorico.Colaboradores?.Where(c => aplicacaoAvaliacao.MembrosBancaExaminadora == null || !aplicacaoAvaliacao.MembrosBancaExaminadora.Any(m => m.Participou.GetValueOrDefault() && m.SeqColaborador == c.SeqColaborador)).ToList().ForEach(c =>
                        {
                            HistoricoEscolarColaboradorDomainService.DeleteEntity(c.Seq);
                        });

                        // Adiciona os que não existem
                        aplicacaoAvaliacao.MembrosBancaExaminadora?.Where(m => m.Participou.GetValueOrDefault() && !dadosHistorico.Colaboradores.Any(c => c.SeqColaborador == m.SeqColaborador)).ToList().ForEach(m =>
                        {
                            HistoricoEscolarColaboradorDomainService.InsertEntity(new HistoricoEscolarColaborador
                            {
                                DescricaoComplementoInstituicao = m.ComplementoInstituicao,
                                SeqHistoricoEscolar = dadosHistorico.Seq,
                                NomeColaborador = m.NomeColaborador,
                                NomeInstituicaoExterna = m.NomeInstituicaoExterna,
                                SeqColaborador = m.SeqColaborador,
                                SeqInstituicaoExterna = m.SeqInstituicaoExterna,
                                TipoMembroBanca = m.TipoMembroBanca
                            });
                        });

                        // Altera os que já existem
                        dadosHistorico.Colaboradores?.Where(c => aplicacaoAvaliacao.MembrosBancaExaminadora != null && aplicacaoAvaliacao.MembrosBancaExaminadora.Any(m => m.SeqColaborador == c.SeqColaborador)).ToList().ForEach(c =>
                        {
                            var dadosMembro = aplicacaoAvaliacao.MembrosBancaExaminadora.FirstOrDefault(m => m.SeqColaborador == c.SeqColaborador);
                            c.DescricaoComplementoInstituicao = dadosMembro.ComplementoInstituicao;
                            c.NomeColaborador = dadosMembro.NomeColaborador;
                            c.NomeInstituicaoExterna = dadosMembro.NomeInstituicaoExterna;
                            c.SeqColaborador = dadosMembro.SeqColaborador;
                            c.SeqInstituicaoExterna = dadosMembro.SeqInstituicaoExterna;
                            c.TipoMembroBanca = dadosMembro.TipoMembroBanca;

                            HistoricoEscolarColaboradorDomainService.UpdateEntity(c);
                        });

                        //Retorna o seq do aluno para buscar os dados de origem
                        var seqAluno = TrabalhoAcademicoDomainService.SearchProjectionByKey(avaliacaoBancaExaminadoraVO.SeqTrabalhoAcademico.GetValueOrDefault(), t => t.Autores.FirstOrDefault().SeqAluno);

                        //Recupera os dados de origem do aluno
                        var dadosOrigem = PessoaAtuacaoDomainService.RecuperaDadosOrigem(seqAluno, false);

                        //Recupera o sequencial do ciclo letivo baseado na data de aplicação da avaliação
                        var seqCicloLetivo = ConfiguracaoEventoLetivoDomainService.BuscarSeqCicloLetivo(avaliacaoBancaExaminadoraVO.DataInicioAplicacaoAvaliacao.GetValueOrDefault(), dadosOrigem.SeqCursoOfertaLocalidadeTurno, null, TOKEN_TIPO_EVENTO.PERIODO_CICLO_LETIVO);

                        //Caso o ciclo letivo referente a data de avaliação seja diferente do gravado anteriormente, atualiza o ciclo letivo no histórico escolar
                        if (seqCicloLetivo != dadosHistorico.SeqCicloLetivo)
                        {
                            var historicoEscolar = HistoricoEscolarDomainService.SearchByKey(dadosHistorico.Seq);
                            historicoEscolar.SeqCicloLetivo = seqCicloLetivo;
                            HistoricoEscolarDomainService.UpdateFields<HistoricoEscolar>(historicoEscolar, h => h.SeqCicloLetivo);
                        }
                    }
                }
            }

            var orientadores = avaliacaoBancaExaminadoraVO.MembrosBancaExaminadora.Where(w => w.TipoMembroBanca == TipoMembroBanca.Orientador).Select(s => s.SeqColaborador).ToList();

            EnviarNotificacaoAvaliacaoTrabalhoAcademicoBancaExaminadora(avaliacaoBancaExaminadoraVO.SeqTrabalhoAcademico, orientadores, aplicacaoAvaliacao.DataInicioAplicacaoAvaliacao, aplicacaoAvaliacao.Local);

            return avaliacaoBancaExaminadoraVO.SeqTrabalhoAcademico.Value;

        }

        private void EnviarNotificacaoAvaliacaoTrabalhoAcademicoBancaExaminadora(long? seqtrabalhoAcademico, List<long?> orientadores, DateTime dataAvalicao, string localAvaliacao)
        {
            if (seqtrabalhoAcademico.HasValue)
            {
                var trabalhoAcademico = TrabalhoAcademicoDomainService.BuscarTrabalhoAcademicoModeloNotificacao(seqtrabalhoAcademico.Value);

                if (trabalhoAcademico.PotencialNegocio.HasValue && trabalhoAcademico.PotencialNegocio.Value || trabalhoAcademico.PotencialPatente.HasValue && trabalhoAcademico.PotencialPatente.Value || trabalhoAcademico.PotencialRegistroSoftware.HasValue && trabalhoAcademico.PotencialRegistroSoftware.Value)
                {
                    // Recupera o sequencial da configuração da notificação
                    var seqConfiguracaoNotificacao = EntidadeConfiguracaoNotificacaoDomainService.BuscarSeqConfiguracaoNotificacaoAtivo(trabalhoAcademico.SeqEntidade, TOKEN_TIPO_NOTIFICACAO.POTENCIAL_REGISTRO_TRABALHO);

                    if (seqConfiguracaoNotificacao == 0)
                        throw new ConfiguracaoNotificacaoNaoExisteException();

                    /*
                      Caso pelo menos um dos campos ind_potencial_patente, ind_potencial_registro_software e 
                        ind_potencial_negocio, registrados no trabalho, possuir um valor positivo:
                        -Buscar a configuração da notificação cujo tipo de notificação tenha o token 
                        'POTENCIAL_REGISTRO_TRABALHO' e esteja associada a entidade da instituição de ensino do 
                        trabalho em questão. Se houver configuração cadastrada:
                        1. Alterar as tags no texto da mensagem de acordo com a especificação abaixo:
                        • {{DSC_TIPO_TRABALHO}} -> descrição do tipo do trabalho;
                        • {{NOM_PESSOA}} -> nome do autor do trabalho;
                        • {{PROGRAMA}} -> nome da entidade do grupo de programa relacionado ao curso do aluno.
                        • {{POTENCIAL_REGISTRO}} ->listar as descrições dos potenciais de registro de trabalho que possuem valor positivo conforme abaixo:
                            -Se ind_potencial_negocio for positivo exibir a descrição 'Negócio'
                            -Se ind_potencial_registro_software for positivo exibir a descrição 'Registro de software'
                            -Se ind_potencial_patente for positivo exibir a descrição 'Patente'
                        Caso exista mais de um potencial com valor positivo listar todos de forma concatenada e separá-los por virgula.
                        • {{TITULO}} -> Título do trabalho. 
                        • {{AREA}} -> Listar a descrição da formação especifica atual do aluno cujo token seja AREA_CONCENTRACAO. Se não informado exibir '-'.
                        • {{ORIENTADOR}} -> Listar o nome do membro da banca associada a aplicação da avaliação cujo tipo de membro seja igual a 4 -Orientador. Se não informado exibir '-'.
                        • {{DAT_DEFESA}} -> data de início da aplicação da avaliação. Exibir no formato "dd/mm/yyyy hh:mm";
                        • {{LOCAL_DEFESA}} -> descrição do local da avaliação.
                     */
                    if (seqConfiguracaoNotificacao != 0)
                    {
                        string nomeOrientadores = string.Empty;
                        if (orientadores.Any())
                        {
                            var spec = new ColaboradorFilterSpecification() { Seqs = orientadores.Select(s => s.Value).ToArray() };
                            var colaboradorOrientador = ColaboradorDomainService.SearchBySpecification(spec, s => s.DadosPessoais).ToList();

                            nomeOrientadores = string.Join(", ", colaboradorOrientador.Select(s => s.DadosPessoais.Nome));
                        }
                        else
                            nomeOrientadores = "-";

                        var potenciais = new List<string>();

                        if (trabalhoAcademico.PotencialNegocio.HasValue && trabalhoAcademico.PotencialNegocio.Value)
                            potenciais.Add("Negócio");
                        if (trabalhoAcademico.PotencialRegistroSoftware.HasValue && trabalhoAcademico.PotencialRegistroSoftware.Value)
                            potenciais.Add("Registro de software");
                        if (trabalhoAcademico.PotencialPatente.HasValue && trabalhoAcademico.PotencialPatente.Value)
                            potenciais.Add("Patente");

                        string potencialRegistro = string.Join(", ", potenciais);

                        var dadosMerge = new Dictionary<string, string>
                        {
                            { "{{DSC_TIPO_TRABALHO}}", trabalhoAcademico.DescricaoTipoTrabalho },
                            { "{{NOM_PESSOA}}", trabalhoAcademico.NomeAutor },
                            { "{{PROGRAMA}}", trabalhoAcademico.Autores.FirstOrDefault().DescricaoEntidadeResponsavel },
                            { "{{POTENCIAL_REGISTRO}}", potencialRegistro },
                            { "{{TITULO}}", trabalhoAcademico.Titulo },
                            { "{{AREA}}", trabalhoAcademico.Autores.FirstOrDefault().DescricaoFormacaoEspecifica },
                            { "{{ORIENTADOR}}", nomeOrientadores },
                            { "{{DAT_DEFESA}}", dataAvalicao.ToString("dd/MM/yyyy HH:mm") },
                            { "{{LOCAL_DEFESA}}", localAvaliacao }
                        };

                        var data = new NotificacaoEmailData()
                        {
                            SeqConfiguracaoNotificacao = seqConfiguracaoNotificacao,
                            DadosMerge = dadosMerge,
                            DataPrevistaEnvio = DateTime.Now,
                            PrioridadeEnvio = PrioridadeEnvio.QuandoPossivel,
                        };

                        using (var transacao = SMCUnitOfWork.Begin())
                        {
                            try
                            {
                                long seqNotificacaoEnviada = this.NotificacaoService.SalvarNotificacao(data);
                                transacao.Commit();
                            }
                            catch (Exception ex)
                            {
                                throw ex;
                            }

                        }
                    }
                }
            }
        }

        /// <summary>
        /// Valida a Inserção da Avaliação, conforme  UC_ORT_002_02_04.Manter_Agendamento_Banca_Examinadora
        ///  Na inclusão, Criar uma avaliação considerando os valores dos atributos:
        ///  O tipo de avaliação = 'Banca'
        ///  · Avaliação geral = 'Não'
        ///  · Replica nota = 'Não'
        ///  · Valor = 1
        ///  · A descrição da avaliação a máscara descrita pela regra
        ///  · RN_APR_016 - Máscara descrição avaliação banca examinadora
        ///  A sigla a concatenação da letra B, com o número relativo aonúmero de avaliações
        ///  já existente para este componente.
        ///  Ex.: B1, B2, etc.
        ///  A origem de avaliação a mesma associada ao componente curricular em questão.
        ///  Entrega WEB = 'Não'·
        ///  Evento AGD = null
        /// </summary>
        /// <param name="AplicacaoAvaliacao"></param>
        private void ValidarInclusao(AvaliacaoTrabalhoAcademicoBancaExaminadoraVO avaliacaoBancaExaminadoraVO, AplicacaoAvaliacao aplicacaoAvaliacaoBanca)
        {
            // Busca os dados do trabalho academico
            var specTrabalhoAcademico = new TrabalhoAcademicoFilterSpecification() { Seq = avaliacaoBancaExaminadoraVO.SeqTrabalhoAcademico };
            var trabalho = this.TrabalhoAcademicoDomainService.SearchProjectionByKey(specTrabalhoAcademico, x => new
            {
                SeqsAlunos = x.Autores.Select(a => a.Aluno.Seq).ToList(),
                DataDepositoSecretaria = x.DataDepositoSecretaria
            });

            // Se a data da avaliação não foi informada, erro
            if (!avaliacaoBancaExaminadoraVO.DataInicioAplicacaoAvaliacao.HasValue)
                throw new SMCApplicationException("Data de aplicação da avaliação é obrigatória.");

            // Verifica se a data da avaliação (banca) é posterior a data de deposito do trabalho na secretaria
            if (trabalho.DataDepositoSecretaria.HasValue && avaliacaoBancaExaminadoraVO.DataInicioAplicacaoAvaliacao < trabalho.DataDepositoSecretaria)
                throw new SMCApplicationException("Data de aplicação da avaliação deve ser maior que a data de depósito.");

            // Para cada aluno do trabalho
            foreach (var seqAluno in trabalho.SeqsAlunos)
            {
                // Dados de origem do aluno
                var dadosOrigem = PessoaAtuacaoDomainService.RecuperaDadosOrigem(seqAluno, false);

                // Busca a situação do aluno o ciclo letivo da data da banca
                var cicloLetivo = ConfiguracaoEventoLetivoDomainService.BuscarCicloLetivo(avaliacaoBancaExaminadoraVO.DataInicioAplicacaoAvaliacao.Value, dadosOrigem.SeqCursoOfertaLocalidadeTurno, null, TOKEN_TIPO_EVENTO.PERIODO_CICLO_LETIVO);
                var situacaoNoCiclo = AlunoHistoricoSituacaoDomainService.BuscarSituacaoAtualAlunoNoCicloLetivo(seqAluno, cicloLetivo.SeqCicloLetivo);
                var tokenSituacaoNoCiclo = situacaoNoCiclo?.TokenSituacaoMatricula;

                // Busca também a situação na data da banca
                var situacaoNaData = AlunoHistoricoSituacaoDomainService.BuscarSituacaoAlunoNaData(seqAluno, avaliacaoBancaExaminadoraVO.DataInicioAplicacaoAvaliacao.Value);
                var tokenSituacaoNaData = situacaoNaData?.TokenSituacaoMatricula;

                /* Na inclusão/alteração: 
                 * Se a data de avaliação informada for menor que a data atual, verificar se o aluno está com a situação 
                 * da matrícula igual a "Matriculado", "Matriculado em mobilidade" ou "Provável Formando" no ciclo letivo 
                 * da data de defesa informada OU na data da defesa informada.
                 * 
                 * Se a data de avaliação informada for a atual ou maior que a atual, verificar se a situação do aluno na 
                 * data da avaliação informada é igual a "Matriculado", "Matriculado em mobilidade" ou "Provável Formando".
                 * 
                 * Caso isso não aconteça, emitir a mensagem abaixo:
                 * "Operacao não permitida. O aluno deverá estar com a situação de matriculado, matriculado em mobilidade 
                 * ou provável formando no ciclo letivo referente a data da defesa."
                */
                // Verifica se o aluno está nas situações na data da banca. Se tiver, ok.
                if (avaliacaoBancaExaminadoraVO.DataInicioAplicacaoAvaliacao.Value < DateTime.Today &&
                    tokenSituacaoNaData != TOKENS_SITUACAO_MATRICULA.MATRICULADO &&
                    tokenSituacaoNaData != TOKENS_SITUACAO_MATRICULA.MATRICULADO_MOBILIDADE &&
                    tokenSituacaoNaData != TOKENS_SITUACAO_MATRICULA.PROVAVEL_FORMANDO &&
                    tokenSituacaoNoCiclo != TOKENS_SITUACAO_MATRICULA.MATRICULADO &&
                    tokenSituacaoNoCiclo != TOKENS_SITUACAO_MATRICULA.MATRICULADO_MOBILIDADE &&
                    tokenSituacaoNoCiclo != TOKENS_SITUACAO_MATRICULA.PROVAVEL_FORMANDO)
                {
                    throw new TrabalhoAcademicoAlunoComSituacaoNaoPermitidaDataDefesaException();
                }
                else if (avaliacaoBancaExaminadoraVO.DataInicioAplicacaoAvaliacao.Value >= DateTime.Today &&
                    tokenSituacaoNaData != TOKENS_SITUACAO_MATRICULA.MATRICULADO &&
                    tokenSituacaoNaData != TOKENS_SITUACAO_MATRICULA.MATRICULADO_MOBILIDADE &&
                    tokenSituacaoNaData != TOKENS_SITUACAO_MATRICULA.PROVAVEL_FORMANDO)
                {
                    throw new TrabalhoAcademicoAlunoComSituacaoNaoPermitidaDataDefesaException();
                }
            }
        }

        private OrigemAvaliacao BuscarOrigemAvaliacao(long seqOrigemAvaliacao)
        {
            var include = IncludesOrigemAvaliacao.AplicacoesAvaliacao
                         | IncludesOrigemAvaliacao.DivisoesComponente
                         | IncludesOrigemAvaliacao.DivisoesComponente_DivisaoComponente
                         | IncludesOrigemAvaliacao.DivisoesComponente_DivisaoComponente_ConfiguracaoComponente;

            return OrigemAvaliacaoDomainService.SearchByKey(new SMCSeqSpecification<OrigemAvaliacao>(seqOrigemAvaliacao), include);
        }

        /// <summary>
        /// RN_APR_016 Máscara descrição avaliação banca examinadora
        /// A máscara será a concatenação dos campos: "Componente Curricular" + " - " + "Nome do(s) Aluno(s)".
        /// OBS.: Quando houver mais de um aluno, o nome deverá ser separado pelo caracter "/".
        /// Ex.:
        /// Defesa de Tese - Ana Paula Faria
        /// Exame de Qualificação de Doutorado = Elisa Kalume Andrade
        /// Defesa de TCC - Karem Queiroz / Luiz Ricardo Silva
        /// </summary>
        /// <param name="avaliacao"></param>
        private string MascaraDescricaoAvaliacao(long seqTrabalhoAcademico, OrigemAvaliacao origemAvaliacao)
        {
            var autores = BuscarAutoresTrabalhoAcademico(seqTrabalhoAcademico);

            var descricaoComponente = BuscarDescricaoComponente(origemAvaliacao);

            string nomesAutores = string.Join(" / ", autores.Select(a => a.NomeAutor).ToArray());

            string descricao = $"{descricaoComponente} - {nomesAutores}";

            return descricao;
        }

        private string BuscarDescricaoComponente(OrigemAvaliacao origemAvaliacao)
        {
            string descricaoComponente = origemAvaliacao.DivisoesComponente.Select(s => s.DivisaoComponente.ConfiguracaoComponente).FirstOrDefault().Descricao;

            return descricaoComponente;
        }

        private List<TrabalhoAcademicoAutoriaVO> BuscarAutoresTrabalhoAcademico(long seqTrabalhoAcademico)
        {
            return TrabalhoAcademicoDomainService.AlterarTrabalhoAcademico(seqTrabalhoAcademico)?.Autores;
        }

        private long BuscarSeqTrabalhoAcademico(OrigemAvaliacao origemAvaliacao)
        {
            return Convert.ToInt64(origemAvaliacao?.DivisoesComponente?.Select(s => s.SeqTrabalhoAcademico).FirstOrDefault());
        }

        /// <summary>
        /// Criar nova avaliação atribuindo valores fixos aos atributos
        /// </summary>
        /// <returns></returns>
        private Avaliacao GerarNovaAvaliacao()
        {
            return new Avaliacao()
            {
                TipoAvaliacao = Common.Areas.APR.Enums.TipoAvaliacao.Banca,
                AvaliacaoGeral = false,
                ReplicaNota = false,
                Valor = 1,
                AplicacoesAvaliacao = new List<AplicacaoAvaliacao>()
            };
        }

        #endregion [ Salvar Aplicacao Avaliacao do trabalho acadêmico ]

        #region [ Exclusão Aplicação Avaliação do tabalho acadêmico ]

        public void ExcluirAplicacaoAvaliacaoTrabalhoAcademicoBancaExaminadora(long seqAplicacaoAvaliacao)
        {
            if (seqAplicacaoAvaliacao == 0) { throw new LancamentoNotaBancaExaminadoraAvaliacaoExclusaoException(); }

            var apuracao = this.SearchProjectionByKey(seqAplicacaoAvaliacao, x => new
            {
                Apuracoes = x.ApuracoesAvaliacao.Select(a => a.Seq),
                SeqAvaliacao = x.SeqAvaliacao,
                SituacaoHistoricoEscolar = (SituacaoHistoricoEscolar?)x.OrigemAvaliacao.HistoricosEscolares.FirstOrDefault().SituacaoHistoricoEscolar,
                HistoricosEscolares = x.OrigemAvaliacao.HistoricosEscolares.Select(h => new
                {
                    SeqHistoricoEscolar = h.Seq,
                    SituacaoHistorico = h.SituacaoHistoricoEscolar
                }),
                OutrasAvaliacoes = x.OrigemAvaliacao.AplicacoesAvaliacao.Where(a => a.Seq != seqAplicacaoAvaliacao && !a.DataCancelamento.HasValue && (a.ApuracoesAvaliacao.FirstOrDefault().Nota.HasValue || a.ApuracoesAvaliacao.FirstOrDefault().SeqEscalaApuracaoItem.HasValue)).Select(a => new { a.Seq, a.ApuracoesAvaliacao.FirstOrDefault().Nota, a.ApuracoesAvaliacao.FirstOrDefault().SeqEscalaApuracaoItem })
            });

            if (apuracao != null)
            {
                // Remove as apurações caso existam
                if (apuracao.Apuracoes != null)
                    foreach (var apuracaoAvaliacao in apuracao.Apuracoes)
                        ApuracaoAvaliacaoDomainService.DeleteEntity(apuracaoAvaliacao);

                // Remove os históricos caso não exista mais nenhuma outra avaliação
                if (apuracao.HistoricosEscolares != null)
                    foreach (var historico in apuracao.HistoricosEscolares)
                    {
                        if (!apuracao.OutrasAvaliacoes.Any())
                            HistoricoEscolarDomainService.DeleteEntity(historico.SeqHistoricoEscolar);
                        else // Atualiza o histórico
                            HistoricoEscolarDomainService.UpdateFields(new HistoricoEscolar
                            {
                                Seq = historico.SeqHistoricoEscolar,
                                SituacaoHistoricoEscolar = apuracao.SituacaoHistoricoEscolar.Value,
                                Nota = apuracao.OutrasAvaliacoes.OrderByDescending(o => o.Seq).FirstOrDefault().Nota,
                                SeqEscalaApuracaoItem = apuracao.OutrasAvaliacoes.OrderByDescending(o => o.Seq).FirstOrDefault().SeqEscalaApuracaoItem
                            }, h => h.SituacaoHistoricoEscolar, h => h.Nota, h => h.SeqEscalaApuracaoItem);
                    }

                // Remove aa avaliacao caso exista
                if (apuracao.SeqAvaliacao > 0)
                    AvaliacaoDomainService.DeleteEntity(apuracao.SeqAvaliacao);
            }

            // Caso ainda não tenha sido excluido por relacionamento, garante a exclusão
            var existe = this.Count(new SMCSeqSpecification<AplicacaoAvaliacao>(seqAplicacaoAvaliacao)) > 0;
            if (existe)
                this.DeleteEntity(seqAplicacaoAvaliacao);
        }

        #endregion [ Exclusão Aplicação Avaliação do tabalho acadêmico ]

        /// <summary>
        /// Buscar a proxima silga da avaliação
        /// </summary>
        /// <param name="filtro">Parametros de pesquisa</param>
        /// <returns>Sigla formatada</returns>
        public string BuscarProximaSiglaAvaliacao(AplicacaoAvaliacaoFilterSpecification filtro)
        {
            string retorno = string.Empty;
            filtro.SetOrderByDescending(o => o.DataInclusao);
            string ultimaSigla = SearchProjectionBySpecification(filtro, p => p.Sigla).FirstOrDefault();
            int ultimoNumero = 0;
            if (!string.IsNullOrEmpty(ultimaSigla))
            {
                ultimoNumero = Convert.ToInt32(ultimaSigla.Substring(1));
            }
            switch (filtro.TipoAvaliacao)
            {
                case TipoAvaliacao.Prova:
                    retorno = $"P{ultimoNumero + 1}";
                    break;

                case TipoAvaliacao.Reavaliacao:
                    retorno = $"R{ultimoNumero + 1}";
                    break;

                case TipoAvaliacao.Trabalho:
                    retorno = $"T{ultimoNumero + 1}";
                    break;

                default:
                    retorno = $"G{ultimoNumero + 1}";
                    break;
            }

            return retorno;
        }

        /// <summary>
        /// Buscar todas as aplicações avaliacoes
        /// </summary>
        /// <param name="seqOrigemAvaliacao">Parametros de pesquisa</param>
        /// <returns>Lista de aplicações avaliações</returns>
        public List<AplicacaoAvaliacao> BuscarAplicacoesAvaliacoes(AplicacaoAvaliacaoFilterSpecification filtro)
        {
            IncludesAplicacaoAvaliacao include = IncludesAplicacaoAvaliacao.Avaliacao | IncludesAplicacaoAvaliacao.ApuracoesAvaliacao;
            List<AplicacaoAvaliacao> retorno = SearchBySpecification(filtro, include).ToList();

            return retorno;
        }

        /// <summary>
        /// Busca a quantidade de avaliações aplicadas nas divisões de uma turma
        /// </summary>
        /// <param name="seqAlunoHistorico">Sequencial do aluno histórico</param>
        /// <param name="seqOrigemAvaliacao">Sequencial da origem de avaliação da turma do aluno</param>
        /// <returns>Quandidade de avaliações aplicadadas para as divisoes da origem informada</returns>
        public int BuscarQuantidadeAvaliacoesAlunoPorOrigemTurma(long seqAlunoHistorico, long seqOrigemAvaliacao)
        {
            var specAluno = new PlanoEstudoItemFilterSpecification()
            {
                PlanoEstudoAtual = true,
                SeqAlunoHistorico = seqAlunoHistorico,
                SeqOrigemAvaliacaoTurma = seqOrigemAvaliacao
            };
            var seqsOrigemAvaliacaoAlunoDivisoesAluno = PlanoEstudoItemDomainService
                .SearchProjectionBySpecification(specAluno, p => p.DivisaoTurma.SeqOrigemAvaliacao)
                .ToList();
            var spec = new AplicacaoAvaliacaoFilterSpecification() { SeqsOrigemAvaliacao = seqsOrigemAvaliacaoAlunoDivisoesAluno };
            return Count(spec);
        }
    }
}