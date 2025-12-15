using Newtonsoft.Json;
using SMC.Academico.Common.Areas.ALN.Constants;
using SMC.Academico.Common.Areas.ALN.Enums;
using SMC.Academico.Common.Areas.ALN.Exceptions;
using SMC.Academico.Common.Areas.ALN.Includes;
using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Academico.Common.Areas.CAM.Exceptions.CilcoLetivo;
using SMC.Academico.Common.Areas.CNC.Constants;
using SMC.Academico.Common.Areas.CNC.Exceptions;
using SMC.Academico.Common.Areas.CNC.Exceptions.SolicitacaoDocumentoConclusao;
using SMC.Academico.Common.Areas.CSO.Includes;
using SMC.Academico.Common.Areas.FIN.Constants;
using SMC.Academico.Common.Areas.FIN.Enums;
using SMC.Academico.Common.Areas.FIN.Exceptions;
using SMC.Academico.Common.Areas.MAT.Constants;
using SMC.Academico.Common.Areas.MAT.Exceptions;
using SMC.Academico.Common.Areas.ORG.Enums;
using SMC.Academico.Common.Areas.ORT.Constants;
using SMC.Academico.Common.Areas.PES.Constants;
using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Academico.Common.Areas.PES.Exceptions;
using SMC.Academico.Common.Areas.Shared.Helpers;
using SMC.Academico.Common.Areas.SRC.Constants;
using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.Academico.Common.Areas.SRC.Exceptions;
using SMC.Academico.Common.Constants;
using SMC.Academico.Domain.Areas.ALN.Models;
using SMC.Academico.Domain.Areas.ALN.Resources;
using SMC.Academico.Domain.Areas.ALN.Specifications;
using SMC.Academico.Domain.Areas.ALN.ValueObjects;
using SMC.Academico.Domain.Areas.APR.DomainServices;
using SMC.Academico.Domain.Areas.CAM.DomainServices;
using SMC.Academico.Domain.Areas.CAM.ValueObjects;
using SMC.Academico.Domain.Areas.CNC.DomainServices;
using SMC.Academico.Domain.Areas.CNC.ValueObjects;
using SMC.Academico.Domain.Areas.CSO.DomainServices;
using SMC.Academico.Domain.Areas.CSO.Models;
using SMC.Academico.Domain.Areas.CSO.Specifications;
using SMC.Academico.Domain.Areas.CUR.DomainServices;
using SMC.Academico.Domain.Areas.CUR.Specifications;
using SMC.Academico.Domain.Areas.FIN.DomainServices;
using SMC.Academico.Domain.Areas.FIN.Models;
using SMC.Academico.Domain.Areas.MAT.DomainServices;
using SMC.Academico.Domain.Areas.MAT.Specifications;
using SMC.Academico.Domain.Areas.MAT.ValueObjects;
using SMC.Academico.Domain.Areas.ORG.DomainServices;
using SMC.Academico.Domain.Areas.ORG.Models;
using SMC.Academico.Domain.Areas.ORG.Specifications;
using SMC.Academico.Domain.Areas.ORT.DomainServices;
using SMC.Academico.Domain.Areas.PES.DomainServices;
using SMC.Academico.Domain.Areas.PES.Models;
using SMC.Academico.Domain.Areas.PES.Specifications;
using SMC.Academico.Domain.Areas.PES.ValueObjects;
using SMC.Academico.Domain.Areas.SRC.DomainServices;
using SMC.Academico.Domain.Areas.SRC.Specifications;
using SMC.Academico.Domain.Areas.SRC.ValueObjects;
using SMC.Academico.Domain.Areas.TUR.DomainServices;
using SMC.Academico.Domain.Areas.TUR.Models;
using SMC.Academico.Domain.DomainServices;
using SMC.Academico.Domain.Repositories;
using SMC.Academico.FilesCollection;
using SMC.AssinaturaDigital.ServiceContract.Areas.DOC.Interfaces;
using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.EstruturaOrganizacional.ServiceContract.Areas.ESO.Interfaces;
using SMC.Financeiro.Common.Areas.GRA.Enums;
using SMC.Financeiro.Service.FIN;
using SMC.Financeiro.ServiceContract.Areas.BNK.Data;
using SMC.Financeiro.ServiceContract.Areas.GRA.Data;
using SMC.Financeiro.ServiceContract.BLT;
using SMC.Financeiro.ServiceContract.BLT.Data;
using SMC.Formularios.Common.Areas.TMP.Enums;
using SMC.Framework;
using SMC.Framework.Domain;
using SMC.Framework.Exceptions;
using SMC.Framework.Extensions;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.Repository;
using SMC.Framework.Rest;
using SMC.Framework.Security;
using SMC.Framework.Service;
using SMC.Framework.Specification;
using SMC.Framework.UnitOfWork;
using SMC.Framework.Util;
using SMC.IntegracaoAcademico.ServiceContract.Areas.IAC.Data;
using SMC.IntegracaoAcademico.ServiceContract.Areas.IAC.Interfaces;
using SMC.Localidades.Common.Areas.LOC.Enums;
using SMC.Localidades.ServiceContract.Areas.LOC.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using static iTextSharp.text.pdf.AcroFields;

namespace SMC.Academico.Domain.Areas.ALN.DomainServices
{
    public class AlunoDomainService : AcademicoContextDomain<Aluno>
    {
        #region [ Querys ]

        private const string QUERY_RELATORIO_ALUNO_POR_SITUACAO =
            @"
			declare
				@SEQ_CICLO_LETIVO bigint,
                @SEQ_CICLO_LETIVO_INGRESSO bigint,
				@SEQS_ENTIDADES_RESPONSAVEIS varchar(255),
				@SEQ_CURSO_OFERTA_LOCALIDADE bigint,
				@SEQ_TURNO bigint,
				@TIPOS_SITUACAO_MATRICULA varchar(255),
				@SITUACOES_MATRICULA varchar(255),
				@TIPOS_VINCULO varchar(255)

			set	@SEQ_CICLO_LETIVO = {0}
            set	@SEQ_CICLO_LETIVO_INGRESSO = {1}
			set @SEQS_ENTIDADES_RESPONSAVEIS = {2}
			set @SEQ_CURSO_OFERTA_LOCALIDADE = {3}
			set @SEQ_TURNO = {4}
			set @TIPOS_SITUACAO_MATRICULA = {5}
			set @SITUACOES_MATRICULA = {6}
			set @TIPOS_VINCULO = {7}

			select	distinct
                pa.seq_pessoa as SeqPessoa,
				pa.seq_pessoa_atuacao as SeqPessoaAtuacao,
				case
					when d.nom_social is not null then rtrim(d.nom_social) + ' (' + rtrim(d.nom_pessoa) + ')'
					else rtrim(d.nom_pessoa)
				end as Nome,
				rtrim(ta.dsc_dominio) + ' - ' + rtrim(tal.dsc_dominio) as DescricaoTipoAtuacao,
				ts.dsc_tipo_situacao_matricula as DescricaoTipoSituacaoMatricula,
				s.dsc_situacao_matricula as DescricaoSituacaoMatricula,
				tv.dsc_tipo_vinculo_aluno as DescricaoVinculo,
				case
					when ecol.nom_entidade is not null then rtrim(ecol.nom_entidade) + ' - ' + rtrim(tu.dsc_turno)
					else '-'
				end as DescricaoCursoOferta,
                ah.seq_entidade_vinculo as SeqEntidadeResponsavel
			from	pes.pessoa_atuacao pa
			join	aln.aluno a
					on pa.seq_pessoa_atuacao = a.seq_pessoa_atuacao
			join	aln.aluno_historico ah
					on a.seq_pessoa_atuacao = ah.seq_atuacao_aluno
					and ah.ind_atual = 1
					and ah.seq_entidade_vinculo in (select cast(item as bigint) from DPD.dbo.fn_converte_lista_em_tabela(@SEQS_ENTIDADES_RESPONSAVEIS, ','))
                    and (@SEQ_CICLO_LETIVO_INGRESSO is null or ah.seq_ciclo_letivo = @SEQ_CICLO_LETIVO_INGRESSO)
			left join	cso.curso_oferta_localidade_turno colt
						on ah.seq_curso_oferta_localidade_turno = colt.seq_curso_oferta_localidade_turno
			left join	cso.curso_oferta_localidade col
						on colt.seq_entidade_curso_oferta_localidade = col.seq_entidade
			left join	org.entidade ecol
						on col.seq_entidade = ecol.seq_entidade
			left join	cso.turno tu
						on colt.seq_turno = tu.seq_turno
			join	aln.aluno_historico_ciclo_letivo ahc
					on ah.seq_aluno_historico = ahc.seq_aluno_historico
					and ahc.seq_ciclo_letivo = @SEQ_CICLO_LETIVO
			join	aln.aluno_historico_situacao ahs
					on ahc.seq_aluno_historico_ciclo_letivo = ahs.seq_aluno_historico_ciclo_letivo
					and ahs.dat_exclusao is null
					and ahs.dat_inicio_situacao = (	select	max(dat_inicio_situacao)
													from	aln.aluno_historico_situacao
													where	seq_aluno_historico_ciclo_letivo = ahs.seq_aluno_historico_ciclo_letivo
													and		dat_exclusao is null
													and		dat_inicio_situacao <= GETDATE())
			join	mat.situacao_matricula s
					on ahs.seq_situacao_matricula = s.seq_situacao_matricula
			join	mat.tipo_situacao_matricula ts
					on s.seq_tipo_situacao_matricula = ts.seq_tipo_situacao_matricula
			join	pes.pessoa_dados_pessoais d
					on pa.seq_pessoa_dados_pessoais = d.seq_pessoa_dados_pessoais
			join	dominio ta
					on pa.idt_dom_tipo_atuacao = ta.val_dominio
					and ta.nom_dominio = 'tipo_atuacao'
			join	dominio tal
					on ahc.idt_dom_tipo_aluno = tal.val_dominio
					and tal.nom_dominio = 'tipo_aluno'
			join	aln.tipo_vinculo_aluno tv
					on a.seq_tipo_vinculo_aluno = tv.seq_tipo_vinculo_aluno
			where	(@SEQ_CURSO_OFERTA_LOCALIDADE is null or col.seq_entidade = @SEQ_CURSO_OFERTA_LOCALIDADE)
			and		(@SEQ_TURNO is null or tu.seq_turno = @SEQ_TURNO)
			and		(@TIPOS_SITUACAO_MATRICULA is null or ts.seq_tipo_situacao_matricula in (select cast(item as bigint) from DPD.dbo.fn_converte_lista_em_tabela(@TIPOS_SITUACAO_MATRICULA, ',')))
			and		(@SITUACOES_MATRICULA is null or s.seq_situacao_matricula in (select cast(item as bigint) from DPD.dbo.fn_converte_lista_em_tabela(@SITUACOES_MATRICULA, ',')))
			and		(@TIPOS_VINCULO is null or tv.seq_tipo_vinculo_aluno in (select cast(item as bigint) from DPD.dbo.fn_converte_lista_em_tabela(@TIPOS_VINCULO, ',')))
			order by
				case
					when d.nom_social is not null then rtrim(d.nom_social) + ' (' + rtrim(d.nom_pessoa) + ')'
					else rtrim(d.nom_pessoa)
				end
			";

        private const string QUERY_RELATORIO_ALUNO_POR_COMPONENTE_TURMA =
            @"
            select
            	1 as NumeroAgrupador,
            	-- turma
            	t.seq_turma as SeqTurma,
            	t.cod_turma as CodigoTurma,
            	t.num_turma as NumeroTurma,
            	tc.dsc_turma_configuracao_componente as DescricaoTurmaConfiguracaoComponente,
            	-- ciclo letivo
            	cl.num_ciclo_letivo as NumeroCicloLetivo,
            	cl.ano_ciclo_letivo as AnoCicloLetivo,
            	-- alunos
            	a.seq_pessoa_atuacao as SeqPessoaAtuacao,
            	upper(isnull(pdp.nom_social, pdp.nom_pessoa)) as NomePessoaAtuacao,
                dom.dsc_dominio as DescricaoTipoAtuacao,
            	tva.dsc_tipo_vinculo_aluno as DescricaoTipoVinculoAluno,
            	null as NumeroProtocolo,
            	co.dsc_curso_oferta as DescricaoCursoOferta
            from	tur.turma t
            join	cam.ciclo_letivo cl
            		on t.seq_ciclo_letivo_inicio = cl.seq_ciclo_letivo
            join	tur.turma_configuracao_componente tc
            		on t.seq_turma = tc.seq_turma
            join	TUR.divisao_turma dt
            		on t.seq_turma = dt.seq_turma
            join	ALN.plano_estudo_item pei
            		on dt.seq_divisao_turma = pei.seq_divisao_turma
            		and tc.seq_configuracao_componente = pei.seq_configuracao_componente
            join	ALN.plano_estudo pe
            		on pei.seq_plano_estudo = pe.seq_plano_estudo
            		and pe.ind_atual = 1
            join	ALN.aluno_historico_ciclo_letivo ahcl
            		on pe.seq_aluno_historico_ciclo_letivo = ahcl.seq_aluno_historico_ciclo_letivo
            		and ahcl.dat_exclusao is null
            join	ALN.aluno_historico ah
            		on ahcl.seq_aluno_historico = ah.seq_aluno_historico
            		and ah.ind_atual = 1
            join	ALN.aluno a
            		on ah.seq_atuacao_aluno = a.seq_pessoa_atuacao
            join	PES.pessoa_atuacao pa
            		on a.seq_pessoa_atuacao = pa.seq_pessoa_atuacao
            join	PES.pessoa_dados_pessoais pdp
            		on pa.seq_pessoa_dados_pessoais = pdp.seq_pessoa_dados_pessoais
            join	aln.tipo_vinculo_aluno tva
            		on a.seq_tipo_vinculo_aluno = tva.seq_tipo_vinculo_aluno
            join    dominio dom on dom.val_dominio = pa.idt_dom_tipo_atuacao and dom.nom_dominio = 'tipo_atuacao'
            left join	cso.curso_oferta_localidade_turno colt
            			on ah.seq_curso_oferta_localidade_turno = colt.seq_curso_oferta_localidade_turno
            left join	cso.curso_oferta_localidade col
            			on colt.seq_entidade_curso_oferta_localidade = col.seq_entidade
            left join	cso.curso_oferta co
            			on col.seq_curso_oferta = co.seq_curso_oferta
            where	t.seq_turma = @SEQ_TURMA

            UNION

            -- solicitações não finalizadas que possuem a turma
            select
            	2 as NumeroAgrupador,
            	-- turma
            	t.seq_turma as SeqTurma,
            	t.cod_turma as CodigoTurma,
            	t.num_turma as NumeroTurma,
            	tc.dsc_turma_configuracao_componente as DescricaoTurmaConfiguracaoComponente,
            	-- ciclo letivo
            	cl.num_ciclo_letivo as NumeroCicloLetivo,
            	cl.ano_ciclo_letivo as AnoCicloLetivo,
            	-- alunos
            	pa.seq_pessoa_atuacao as SeqPessoaAtuacao,
            	upper(isnull(pdp.nom_social, pdp.nom_pessoa)) as NomePessoaAtuacao,
                dom.dsc_dominio as DescricaoTipoAtuacao,
            	isnull(tvaal.dsc_tipo_vinculo_aluno, tvai.dsc_tipo_vinculo_aluno) as DescricaoTipoVinculoAluno,
            	ss.num_protocolo as NumeroProtocolo,
            	co.dsc_curso_oferta as DescricaoCursoOferta
            from	tur.turma t
            join	cam.ciclo_letivo cl
            		on t.seq_ciclo_letivo_inicio = cl.seq_ciclo_letivo
            join	tur.turma_configuracao_componente tc
            		on t.seq_turma = tc.seq_turma
            join	TUR.divisao_turma dt
            		on t.seq_turma = dt.seq_turma
            join	mat.solicitacao_matricula_item smi
            		on dt.seq_divisao_turma = smi.seq_divisao_turma
            		and tc.seq_configuracao_componente = smi.seq_configuracao_componente
            join	mat.solicitacao_matricula_item_historico_situacao ih
            		on smi.seq_solicitacao_matricula_item = ih.seq_solicitacao_matricula_item
            		and ih.dat_inclusao = (	select	max(ih2.dat_inclusao)
            								from	mat.solicitacao_matricula_item_historico_situacao ih2
            								where	ih2.seq_solicitacao_matricula_item = ih.seq_solicitacao_matricula_item)
            join	mat.situacao_item_matricula sim
            		on ih.seq_situacao_item_matricula = sim.seq_situacao_item_matricula
            		and ( sim.ind_situacao_inicial = 1
            		or	( sim.ind_situacao_final = 1 and sim.idt_dom_classificacao_situacao_final = 1)) -- Finalizado com sucesso
            join	mat.solicitacao_matricula sm
            		on smi.seq_solicitacao_matricula = sm.seq_solicitacao_servico
            join	src.solicitacao_servico ss
            		on sm.seq_solicitacao_servico = ss.seq_solicitacao_servico
            join	src.configuracao_processo cp
            		on ss.seq_configuracao_processo = cp.seq_configuracao_processo
            join	src.processo pr
            		on cp.seq_processo = pr.seq_processo
            join	src.servico sr
            		on pr.seq_servico = sr.seq_servico
            		and sr.dsc_token in (	'SOLICITACAO_MATRICULA_INGRESSANTE_STRICTO_SENSU',
            								'SOLICITACAO_MATRICULA_INGRESSANTE_STRICTO_SENSU_DISCIPLINA_ISOLADA',
            								'SOLICITACAO_RENOVACAO_MATRICULA_STRICTO_SENSU',
            								'MATRICULA_REABERTURA')
            join	src.solicitacao_servico_etapa sse
            		on ss.seq_solicitacao_servico = sse.seq_solicitacao_servico
            join	src.solicitacao_historico_situacao hs
            		on sse.seq_solicitacao_servico_etapa = hs.seq_solicitacao_servico_etapa
            		and hs.dat_exclusao is null
            		and hs.dat_inclusao = (	select	max(hs2.dat_inclusao)
            								from	src.solicitacao_historico_situacao hs2
            								join	src.solicitacao_servico_etapa sse2
            										on hs2.seq_solicitacao_servico_etapa = sse2.seq_solicitacao_servico_etapa
            										and sse2.seq_solicitacao_servico = ss.seq_solicitacao_servico
            								where	hs2.dat_exclusao is null)
            		and hs.idt_dom_categoria_situacao in (1,2) -- novo ou em andamento
            join	PES.pessoa_atuacao pa
            		on ss.seq_pessoa_atuacao = pa.seq_pessoa_atuacao
            join	PES.pessoa_dados_pessoais pdp
            		on pa.seq_pessoa_dados_pessoais = pdp.seq_pessoa_dados_pessoais
            join    dominio dom on dom.val_dominio = pa.idt_dom_tipo_atuacao and dom.nom_dominio = 'tipo_atuacao'
            left join	aln.aluno al
            			on pa.seq_pessoa_atuacao = al.seq_pessoa_atuacao
            left join	aln.tipo_vinculo_aluno tvaal
            			on al.seq_tipo_vinculo_aluno = tvaal.seq_tipo_vinculo_aluno
            left join	ALN.aluno_historico ah
            			on al.seq_pessoa_atuacao = ah.seq_atuacao_aluno
            			and ah.ind_atual = 1
            left join	aln.ingressante i
            			on pa.seq_pessoa_atuacao = i.seq_pessoa_atuacao
            left join	aln.tipo_vinculo_aluno tvai
            			on i.seq_tipo_vinculo_aluno = tvai.seq_tipo_vinculo_aluno
            left join	aln.ingressante_oferta iof
            			on i.seq_pessoa_atuacao = iof.seq_atuacao_ingressante
            left join	cam.campanha_oferta_item coi
            			on iof.seq_campanha_oferta_item = coi.seq_campanha_oferta_item
            			and coi.seq_curso_oferta_localidade_turno is not null
            left join	cso.curso_oferta_localidade_turno colt
            			on ah.seq_curso_oferta_localidade_turno = colt.seq_curso_oferta_localidade_turno
            			or coi.seq_curso_oferta_localidade_turno = colt.seq_curso_oferta_localidade_turno
            left join	cso.curso_oferta_localidade col
            			on colt.seq_entidade_curso_oferta_localidade = col.seq_entidade
            left join	cso.curso_oferta co
            			on col.seq_curso_oferta = co.seq_curso_oferta
            where	t.seq_turma = @SEQ_TURMA
            order by
            	NumeroAgrupador,
            	SeqTurma,
            	upper(isnull(pdp.nom_social, pdp.nom_pessoa))";

        private const string QUERY_RELATORIO_ALUNO_POR_COMPONENTE_ATIVIDADE_ACADEMICA =
            @"
            -- alunos do curso-oferta-localidade (e turno caso informado) que possuem o componente (atividade)
            -- no plano de estudos do ciclo letivo informado
            select
                1 as NumeroAgrupador,
                -- atividade
                co.seq_configuracao_componente as SeqConfiguracaoComponente,
                co.dsc_configuracao_componente as DescricaoConfiguracaoComponente,
                co.dsc_complementar as DescricaoComplementar,
                -- ciclo letivo
                cl.num_ciclo_letivo as NumeroCicloLetivo,
                cl.ano_ciclo_letivo as AnoCicloLetivo,
                -- alunos
                a.seq_pessoa_atuacao as SeqPessoaAtuacao,
                upper(isnull(pdp.nom_social, pdp.nom_pessoa)) as NomePessoaAtuacao,
                dom.dsc_dominio as DescricaoTipoAtuacao,
                tva.dsc_tipo_vinculo_aluno as DescricaoTipoVinculoAluno,
                null as NumeroProtocolo,
            	cof.dsc_curso_oferta as DescricaoCursoOferta
            from	aln.plano_estudo_item pei
            join	aln.plano_estudo pe
                    on pei.seq_plano_estudo = pe.seq_plano_estudo
                    and pe.ind_atual = 1
            join	aln.aluno_historico_ciclo_letivo ahcl
                    on pe.seq_aluno_historico_ciclo_letivo = ahcl.seq_aluno_historico_ciclo_letivo
                    and ahcl.dat_exclusao is null
            join	cam.ciclo_letivo cl
                    on ahcl.seq_ciclo_letivo = cl.seq_ciclo_letivo
            join	cur.configuracao_componente co
                    on pei.seq_configuracao_componente = co.seq_configuracao_componente
            join	ALN.aluno_historico ah
                    on ahcl.seq_aluno_historico = ah.seq_aluno_historico
                    and ah.ind_atual = 1
            join	ALN.aluno a
                    on ah.seq_atuacao_aluno = a.seq_pessoa_atuacao
            join	PES.pessoa_atuacao pa
                    on a.seq_pessoa_atuacao = pa.seq_pessoa_atuacao
            join    dominio dom on dom.val_dominio = pa.idt_dom_tipo_atuacao and dom.nom_dominio = 'tipo_atuacao'
            join	PES.pessoa_dados_pessoais pdp
                    on pa.seq_pessoa_dados_pessoais = pdp.seq_pessoa_dados_pessoais
            join	aln.tipo_vinculo_aluno tva
                    on a.seq_tipo_vinculo_aluno = tva.seq_tipo_vinculo_aluno
            join	cso.curso_oferta_localidade_turno colt
                    on ah.seq_curso_oferta_localidade_turno = colt.seq_curso_oferta_localidade_turno
            join	cso.curso_oferta_localidade col
            		on colt.seq_entidade_curso_oferta_localidade = col.seq_entidade
            join	cso.curso_oferta cof
            		on col.seq_curso_oferta = cof.seq_curso_oferta
            where	ahcl.seq_ciclo_letivo = @SEQ_CICLO_LETIVO
            and		co.seq_configuracao_componente = @SEQ_CONFIGURACAO_COMPONENTE
            and		colt.seq_entidade_curso_oferta_localidade = @SEQ_CURSO_OFERTA_LOCALIDADE
            and		colt.seq_turno = isnull(@SEQ_TURNO, colt.seq_turno)
            and		pei.seq_configuracao_componente is not null
            and		pei.seq_divisao_turma is null

            UNION

            -- solicitações não finalizadas alunos do curso-oferta-localidade (e turno caso informado)
            -- que possuem o componente (atividade)
            select
                2 as NumeroAgrupador,
                -- atividade
                co.seq_configuracao_componente as SeqConfiguracaoComponente,
                co.dsc_configuracao_componente as DescricaoConfiguracaoComponente,
                co.dsc_complementar as DescricaoComplementar,
                -- ciclo letivo
                cl.num_ciclo_letivo as NumeroCicloLetivo,
                cl.ano_ciclo_letivo as AnoCicloLetivo,
                -- alunos
                pa.seq_pessoa_atuacao as SeqPessoaAtuacao,
                upper(isnull(pdp.nom_social, pdp.nom_pessoa)) as NomePessoaAtuacao,
                dom.dsc_dominio as DescricaoTipoAtuacao,
                isnull(tvaal.dsc_tipo_vinculo_aluno, tvai.dsc_tipo_vinculo_aluno) as DescricaoTipoVinculoAluno,
                ss.num_protocolo as NumeroProtocolo,
            	cof.dsc_curso_oferta as DescricaoCursoOferta
            from	mat.solicitacao_matricula_item smi
            join	mat.solicitacao_matricula_item_historico_situacao ih
                    on smi.seq_solicitacao_matricula_item = ih.seq_solicitacao_matricula_item
                    and ih.dat_inclusao = (	select	max(ih2.dat_inclusao)
                        					from	mat.solicitacao_matricula_item_historico_situacao ih2
                        					where	ih2.seq_solicitacao_matricula_item = ih.seq_solicitacao_matricula_item)
            join	mat.situacao_item_matricula sim
                    on ih.seq_situacao_item_matricula = sim.seq_situacao_item_matricula
                    and ( sim.ind_situacao_inicial = 1
                    or	( sim.ind_situacao_final = 1 and sim.idt_dom_classificacao_situacao_final = 1)) -- Finalizado com sucesso
            join	mat.solicitacao_matricula sm
                    on smi.seq_solicitacao_matricula = sm.seq_solicitacao_servico
            join	src.solicitacao_servico ss
                    on sm.seq_solicitacao_servico = ss.seq_solicitacao_servico
            join	src.configuracao_processo cp
                    on ss.seq_configuracao_processo = cp.seq_configuracao_processo
            join	src.processo pr
                    on cp.seq_processo = pr.seq_processo
            join	cam.ciclo_letivo cl
                    on pr.seq_ciclo_letivo = cl.seq_ciclo_letivo
            join	src.servico sr
                    on pr.seq_servico = sr.seq_servico
                    and sr.dsc_token in (	'SOLICITACAO_MATRICULA_INGRESSANTE_STRICTO_SENSU',
                        					'SOLICITACAO_MATRICULA_INGRESSANTE_STRICTO_SENSU_DISCIPLINA_ISOLADA',
                        					'SOLICITACAO_RENOVACAO_MATRICULA_STRICTO_SENSU',
                        					'MATRICULA_REABERTURA')
            join	src.solicitacao_servico_etapa sse
                    on ss.seq_solicitacao_servico = sse.seq_solicitacao_servico
            join	src.solicitacao_historico_situacao hs
                    on sse.seq_solicitacao_servico_etapa = hs.seq_solicitacao_servico_etapa
                    and hs.dat_exclusao is null
                    and hs.dat_inclusao = (	select	max(hs2.dat_inclusao)
                        					from	src.solicitacao_historico_situacao hs2
                        					join	src.solicitacao_servico_etapa sse2
                        							on hs2.seq_solicitacao_servico_etapa = sse2.seq_solicitacao_servico_etapa
                        							and sse2.seq_solicitacao_servico = ss.seq_solicitacao_servico
                        					where	hs2.dat_exclusao is null)
                    and hs.idt_dom_categoria_situacao in (1,2) -- novo ou em andamento
            join	PES.pessoa_atuacao pa
                    on ss.seq_pessoa_atuacao = pa.seq_pessoa_atuacao
            join    dominio dom on dom.val_dominio = pa.idt_dom_tipo_atuacao and dom.nom_dominio = 'tipo_atuacao'
            join	PES.pessoa_dados_pessoais pdp
                    on pa.seq_pessoa_dados_pessoais = pdp.seq_pessoa_dados_pessoais
            left join	aln.aluno al
                        on pa.seq_pessoa_atuacao = al.seq_pessoa_atuacao
            left join	aln.tipo_vinculo_aluno tvaal
                        on al.seq_tipo_vinculo_aluno = tvaal.seq_tipo_vinculo_aluno
            left join	ALN.aluno_historico ah
            			on al.seq_pessoa_atuacao = ah.seq_atuacao_aluno
            			and ah.ind_atual = 1
            left join	aln.ingressante i
                        on pa.seq_pessoa_atuacao = i.seq_pessoa_atuacao
            left join	aln.tipo_vinculo_aluno tvai
                        on i.seq_tipo_vinculo_aluno = tvai.seq_tipo_vinculo_aluno
            left join	aln.ingressante_oferta iof
            			on i.seq_pessoa_atuacao = iof.seq_atuacao_ingressante
            left join	cam.campanha_oferta_item coi
            			on iof.seq_campanha_oferta_item = coi.seq_campanha_oferta_item
            			and coi.seq_curso_oferta_localidade_turno is not null
            left join	cso.curso_oferta_localidade_turno colt
            			on ah.seq_curso_oferta_localidade_turno = colt.seq_curso_oferta_localidade_turno
            			or coi.seq_curso_oferta_localidade_turno = colt.seq_curso_oferta_localidade_turno
            left join	cso.curso_oferta_localidade col
            			on colt.seq_entidade_curso_oferta_localidade = col.seq_entidade
            left join	cso.curso_oferta cof
            			on col.seq_curso_oferta = cof.seq_curso_oferta
            join	cur.configuracao_componente co
                    on smi.seq_configuracao_componente = co.seq_configuracao_componente
            where	pr.seq_ciclo_letivo = @SEQ_CICLO_LETIVO
            and		co.seq_configuracao_componente = @SEQ_CONFIGURACAO_COMPONENTE
            and		smi.seq_configuracao_componente is not null
            and		smi.seq_divisao_turma is null
            and		colt.seq_entidade_curso_oferta_localidade = @SEQ_CURSO_OFERTA_LOCALIDADE
            and		colt.seq_turno = isnull(@SEQ_TURNO, colt.seq_turno)
            order by
                NumeroAgrupador,
                upper(isnull(pdp.nom_social, pdp.nom_pessoa))";

        private const string QUERY_RELATORIO_INGRESSANTE_POR_SITUACAO =
          @"
			declare
				@SEQ_CICLO_LETIVO bigint,
				@SEQS_ENTIDADES_RESPONSAVEIS varchar(255),
				@SEQ_CURSO_OFERTA_LOCALIDADE bigint,
				@SEQ_TURNO bigint,
				@SITUACOES_INGRESSANTE varchar(255),
				@TIPOS_VINCULO varchar(255)

			set	@SEQ_CICLO_LETIVO = {0}
			set @SEQS_ENTIDADES_RESPONSAVEIS = {1}
			set @SEQ_CURSO_OFERTA_LOCALIDADE = {2}
			set @SEQ_TURNO = {3}
			set @SITUACOES_INGRESSANTE = {4}
			set @TIPOS_VINCULO = {5}

			select	distinct
                pa.seq_pessoa as SeqPessoa,
				pa.seq_pessoa_atuacao as SeqPessoaAtuacao,
				case
					when d.nom_social is not null then rtrim(d.nom_social) + ' (' + rtrim(d.nom_pessoa) + ')'
					else rtrim(d.nom_pessoa)
				end as Nome,
				rtrim(ta.dsc_dominio) as DescricaoTipoAtuacao,
				'-' as DescricaoTipoSituacaoMatricula,
				ds.dsc_dominio as DescricaoSituacaoMatricula,
				tv.dsc_tipo_vinculo_aluno as DescricaoVinculo,
				case
					when ecol.nom_entidade is not null then rtrim(ecol.nom_entidade) + ' - ' + rtrim(tu.dsc_turno)
					else '-'
				end as DescricaoCursoOferta,
                i.seq_entidade_responsavel as SeqEntidadeResponsavel
			from	pes.pessoa_atuacao pa
			join	aln.ingressante i
					on pa.seq_pessoa_atuacao = i.seq_pessoa_atuacao
					and i.seq_entidade_responsavel in (select cast(item as bigint) from DPD.dbo.fn_converte_lista_em_tabela(@SEQS_ENTIDADES_RESPONSAVEIS, ','))
			join	pes.pessoa_dados_pessoais d
					on pa.seq_pessoa_dados_pessoais = d.seq_pessoa_dados_pessoais
			join	dominio ta
					on pa.idt_dom_tipo_atuacao = ta.val_dominio
					and ta.nom_dominio = 'tipo_atuacao'
			join	aln.ingressante_historico_situacao hs
					on i.seq_pessoa_atuacao = hs.seq_atuacao_ingressante
					and hs.dat_inclusao = (	select	max(dat_inclusao)
											from	aln.ingressante_historico_situacao
											where	seq_atuacao_ingressante = hs.seq_atuacao_ingressante)
			join	dominio ds
					on hs.idt_dom_situacao_ingressante = ds.val_dominio
					and ds.nom_dominio = 'situacao_ingressante'
			join	cam.campanha_ciclo_letivo ccl
					on i.seq_campanha_ciclo_letivo = ccl.seq_campanha_ciclo_letivo
					and ccl.seq_ciclo_letivo = @SEQ_CICLO_LETIVO
			join	aln.tipo_vinculo_aluno tv
					on i.seq_tipo_vinculo_aluno = tv.seq_tipo_vinculo_aluno
			join	aln.ingressante_oferta iof
					on iof.seq_atuacao_ingressante = i.seq_pessoa_atuacao
			join	cam.campanha_oferta_item coi
					on iof.seq_campanha_oferta_item = coi.seq_campanha_oferta_item
			left join	cso.curso_oferta_localidade_turno colt
						on coi.seq_curso_oferta_localidade_turno = colt.seq_curso_oferta_localidade_turno
			left join	cso.curso_oferta_localidade col
						on colt.seq_entidade_curso_oferta_localidade = col.seq_entidade
			left join	org.entidade ecol
						on col.seq_entidade = ecol.seq_entidade
			left join	cso.turno tu
						on colt.seq_turno = tu.seq_turno
			where	(@SEQ_CURSO_OFERTA_LOCALIDADE is null or col.seq_entidade = @SEQ_CURSO_OFERTA_LOCALIDADE)
			and		(@SEQ_TURNO is null or tu.seq_turno = @SEQ_TURNO)
			and		(@SITUACOES_INGRESSANTE is null or ds.val_dominio in (select cast(item as bigint) from DPD.dbo.fn_converte_lista_em_tabela(@SITUACOES_INGRESSANTE, ',')))
			and		(@TIPOS_VINCULO is null or tv.seq_tipo_vinculo_aluno in (select cast(item as bigint) from DPD.dbo.fn_converte_lista_em_tabela(@TIPOS_VINCULO, ',')))
			order by
				case
					when d.nom_social is not null then rtrim(d.nom_social) + ' (' + rtrim(d.nom_pessoa) + ')'
					else rtrim(d.nom_pessoa)
				end";

        private const string QUERY_DECLARACAO_MATRICULA_ALUNO =
            @"
			select distinct
                p.seq_entidade_instituicao as SeqInstituicaoEnsino,
				a.seq_pessoa_atuacao as SeqPessoaAtuacao,
				pdp.nom_pessoa as NomeAluno,
				eie.nom_entidade as NomeInstituicaoEnsino,
				ev.nom_entidade as NomeEntidade,
				ec.nom_entidade as NomeCurso,
				cl.num_ciclo_letivo as NumeroCicloLetivo,
				cl.ano_ciclo_letivo as AnoCicloLetivo,
				rl.dsc_regime_letivo as DescricaoRegimeLetivo,
				co.dsc_componente_curricular as DescricaoComponenteCurricular,
				co.qtd_carga_horaria as QuantidadeCargaHoraria,
				co.qtd_credito as QuantidadeCreditos,
	            substring ((	select	distinct
						            ', ' +	case
									            when peic.seq_orientacao is null then rtrim(pdpcol.nom_pessoa)
									            else rtrim(doc.nom_pessoa)
								            end
					            from	ALN.plano_estudo_item peic
					            join	TUR.divisao_turma dtc
							            on peic.seq_divisao_turma = dtc.seq_divisao_turma
					            join	TUR.divisao_turma_colaborador dtcc
							            on dtc.seq_divisao_turma = dtcc.seq_divisao_turma
					            join	DCT.colaborador colab
							            on dtcc.seq_atuacao_colaborador = colab.seq_pessoa_atuacao
					            join	PES.pessoa_atuacao pacol
							            on colab.seq_pessoa_atuacao = pacol.seq_pessoa_atuacao
					            join	PES.pessoa_dados_pessoais pdpcol
							            on pacol.seq_pessoa_dados_pessoais = pdpcol.seq_pessoa_dados_pessoais
					            left join	ORT.orientacao_colaborador oc
								            on peic.seq_orientacao = oc.seq_orientacao
					            left join	DCT.colaborador coc
								            on oc.seq_atuacao_colaborador = coc.seq_pessoa_atuacao
					            left join	PES.pessoa_atuacao paoc
								            on coc.seq_pessoa_atuacao = paoc.seq_pessoa_atuacao
					            left join	PES.pessoa_dados_pessoais doc
								            on paoc.seq_pessoa_dados_pessoais = doc.seq_pessoa_dados_pessoais
					            where	dtc.seq_turma = dt.seq_turma
					            and		peic.seq_plano_estudo = pei.seq_plano_estudo
					            order by
						            ', ' +	case
									            when peic.seq_orientacao is null then rtrim(pdpcol.nom_pessoa)
									            else rtrim(doc.nom_pessoa)
								            end
					            FOR XML PATH('')), 3, 1000) as NomeProfessor,
				convert(varchar(10), ah.dat_admissao, 103) as DataAdmissao,
				convert(varchar(10), pv.dat_previsao_conclusao, 103) as DataPrevisaoConclusao,
				convert(bit, case when tdc.idt_dom_tipo_gestao_divisao_componente = 3 then 1 else 0 end) as Turma,
				tcc.dsc_turma_configuracao_componente as DescricaoTurmaConfiguracaoComponente,
				co.seq_tipo_componente_curricular as SeqTipoComponenteCurricular
			from	ALN.aluno a
			join	PES.pessoa_atuacao pa
					on a.seq_pessoa_atuacao = pa.seq_pessoa_atuacao
			join	PES.pessoa p
					on pa.seq_pessoa = p.seq_pessoa
			join	PES.pessoa_dados_pessoais pdp
					on pa.seq_pessoa_dados_pessoais = pdp.seq_pessoa_dados_pessoais
			join	ORG.entidade eie
					on p.seq_entidade_instituicao = eie.seq_entidade
			join	ALN.aluno_historico ah
					on a.seq_pessoa_atuacao = ah.seq_atuacao_aluno
					and ah.ind_atual = 1
			join	ALN.aluno_historico_previsao_conclusao pv
					on ah.seq_aluno_historico = pv.seq_aluno_historico
					and pv.dat_inclusao = (	select	max(dat_inclusao)
											from	ALN.aluno_historico_previsao_conclusao
											where	seq_aluno_historico = pv.seq_aluno_historico)
			join	ORG.entidade ev
					on ah.seq_entidade_vinculo = ev.seq_entidade
			left join	CSO.curso_oferta_localidade_turno colf
						on ah.seq_curso_oferta_localidade_turno = colf.seq_curso_oferta_localidade_turno
			left join	CSO.curso_oferta_localidade col
						on colf.seq_entidade_curso_oferta_localidade = col.seq_entidade
			left join	CSO.curso_unidade cu
						on col.seq_entidade_curso_unidade = cu.seq_entidade
			left join	ORG.entidade ec
						on cu.seq_entidade_curso = ec.seq_entidade
			join	ALN.aluno_historico_ciclo_letivo ahcl
					on ah.seq_aluno_historico = ahcl.seq_aluno_historico
					and ahcl.dat_exclusao is null
					and ahcl.seq_ciclo_letivo = @SEQ_CICLO_LETIVO
			join	CAM.ciclo_letivo cl
					on ahcl.seq_ciclo_letivo = cl.seq_ciclo_letivo
			join	CAM.regime_letivo rl
					on cl.seq_regime_letivo = rl.seq_regime_letivo
			join	ALN.aluno_historico_situacao ahs
					on ahcl.seq_aluno_historico_ciclo_letivo = ahs.seq_aluno_historico_ciclo_letivo
					and ahs.dat_exclusao is null
					and ahs.dat_inicio_situacao = (	select	MAX(dat_inicio_situacao)
													from	ALN.aluno_historico_situacao
													where	seq_aluno_historico_ciclo_letivo = ahcl.seq_aluno_historico_ciclo_letivo
													and		dat_exclusao is null
													and		dat_inicio_situacao <= GETDATE())
			join	MAT.situacao_matricula sm
					on ahs.seq_situacao_matricula = sm.seq_situacao_matricula
			join    MAT.tipo_situacao_matricula tsm
					on sm.seq_tipo_situacao_matricula = tsm.seq_tipo_situacao_matricula
					and tsm.dsc_token in ('MATRICULADO', 'FORMADO')
			join	ALN.plano_estudo pe
					on ahcl.seq_aluno_historico_ciclo_letivo = pe.seq_aluno_historico_ciclo_letivo
					and pe.ind_atual = 1
			join	ALN.plano_estudo_item pei
					on pe.seq_plano_estudo = pei.seq_plano_estudo
			left join	TUR.divisao_turma dt
						on pei.seq_divisao_turma = dt.seq_divisao_turma
			left join	CUR.divisao_componente dc
						on dt.seq_divisao_componente = dc.seq_divisao_componente
			left join	CUR.tipo_divisao_componente tdc
						on dc.seq_tipo_divisao_componente = tdc.seq_tipo_divisao_componente
			join	CUR.configuracao_componente cc
					on pei.seq_configuracao_componente = cc.seq_configuracao_componente
			left join	TUR.turma_configuracao_componente tcc
						on cc.seq_configuracao_componente = tcc.seq_configuracao_componente
						and tcc.seq_turma = dt.seq_turma
			join	CUR.componente_curricular co
					on cc.seq_componente_curricular = co.seq_componente_curricular
			where	a.seq_pessoa_atuacao in (select CONVERT(bigint, item) from dpd.dbo.fn_converte_lista_em_tabela(@SEQS_ALUNO, ','))
			order by
				a.seq_pessoa_atuacao,
				co.dsc_componente_curricular";

        private const string QUERY_RELATORIO_PREVISAO_CONCLUSAO =
            @"
              select 
	                al.seq_pessoa_atuacao as Seq,
	                e.seq_entidade as SeqEntidade,
	                e.nom_entidade as DescricaoEntidade,
	                case 
		                when dp.nom_social is null
		                then dp.nom_pessoa
                        else dp.nom_social + '(' + dp.nom_pessoa + ')'
	                end as Nome,
	                isnull(dp.nom_social, dp.nom_pessoa) as NomeSocialOuNome, 
	                ah.seq_nivel_ensino as SeqNivelEnsino,
	                ne.dsc_nivel_ensino as DescricaoNivel,
	                al.seq_tipo_vinculo_aluno as SeqTipoVinculoAluno,
	                tva.dsc_tipo_vinculo_aluno as DescricaoVinculo,
	                termo.seq_tipo_termo_intercambio as SeqTipoTermoIntercambio,
	                termo.dsc_tipo_termo_intercambio as DescricaoTipoTermoIntercambio,
	                cl.seq_ciclo_letivo as SeqCicloLetivoSituacao,
	                cl.dsc_ciclo_letivo as DescricaoCicloLetivoSituacao,
	                sm.dsc_situacao_matricula as DescricaoSituacaoMatricula,
	                ah.dat_admissao as DataAdmissao,
	                pc.dat_previsao_conclusao as DataPrevisaoConclusao, 
	                pc.dat_limite_conclusao as DataLimiteConclusao,
	                ori.seq_atuacao_colaborador as SeqColaborador, 
	                ori.nom_pessoa as NomeColaborador,
	                ori.dsc_dominio as TipoParticipacaoOrientacao, 
	                ori.dat_inicio_orientacao as DataInicioOrientacao, 
	                ori.dat_fim_orientacao as DataFimOrientacao
                from	ALN.aluno al
                join	ALN.aluno_historico ah
		                on al.seq_pessoa_atuacao = ah.seq_atuacao_aluno
		                and ah.ind_atual = 1
                join	ALN.aluno_historico_previsao_conclusao pc
		                on ah.seq_aluno_historico = pc.seq_aluno_historico
		                and pc.dat_inclusao = (	select	max(dat_inclusao)
								                from	ALN.aluno_historico_previsao_conclusao pc1
								                where	pc.seq_aluno_historico = pc1.seq_aluno_historico)
                join	org.entidade e
		                on ah.seq_entidade_vinculo = e.seq_entidade
                join	PES.pessoa_atuacao pa
		                on al.seq_pessoa_atuacao = pa.seq_pessoa_atuacao
                join	PES.pessoa pes
		                on pa.seq_pessoa = pes.seq_pessoa
                join	PES.pessoa_dados_pessoais dp
		                on pa.seq_pessoa_dados_pessoais = dp.seq_pessoa_dados_pessoais
                join	ORG.nivel_ensino ne
		                on ah.seq_nivel_ensino = ne.seq_nivel_ensino
                join	ALN.tipo_vinculo_aluno tva
		                on al.seq_tipo_vinculo_aluno = tva.seq_tipo_vinculo_aluno
                left join (	select distinct
				                opa.seq_pessoa_atuacao, 
				                oc.seq_atuacao_colaborador,
				                dpc.nom_pessoa,
				                tpo.dsc_dominio, 
				                oc.dat_inicio_orientacao, 
				                oc.dat_fim_orientacao
			                from	ORT.orientacao_pessoa_atuacao opa
			                join	ORT.orientacao o
					                on opa.seq_orientacao = o.seq_orientacao
			                join	ORT.tipo_orientacao tor
					                on  o.seq_tipo_orientacao = tor.seq_tipo_orientacao
					                and tor.ind_trabalho_conclusao_curso = 1
			                left join	ORT.orientacao_colaborador oc
						                on o.seq_orientacao = oc.seq_orientacao
			                left join	DCT.colaborador c
						                on oc.seq_atuacao_colaborador = c.seq_pessoa_atuacao
			                left join	PES.pessoa_atuacao pac
						                on c.seq_pessoa_atuacao = pac.seq_pessoa_atuacao
			                left join	PES.pessoa_dados_pessoais dpc
						                on pac.seq_pessoa_dados_pessoais = dpc.seq_pessoa_dados_pessoais
			                left join	dominio tpo
						                on oc.idt_dom_tipo_participacao_orientacao = tpo.val_dominio
						                and tpo.nom_dominio = 'tipo_participacao_orientacao'
			                ) ori
			                on al.seq_pessoa_atuacao = ori.seq_pessoa_atuacao
                join	ALN.aluno_historico_situacao ahs
		                on ahs.seq_aluno_historico_ciclo_letivo in (select	seq_aluno_historico_ciclo_letivo
													                from	ALN.aluno_historico_ciclo_letivo ahcl1
													                where	ahcl1.seq_aluno_historico = ah.seq_aluno_historico)
		                and ahs.dat_exclusao is null
		                and ahs.dat_inicio_situacao < GETDATE()
		                and ahs.dat_inicio_situacao = (	select	MAX(dat_inicio_situacao)
										                from	ALN.aluno_historico_situacao ahs1
										                join	ALN.aluno_historico_ciclo_letivo ahcl2
												                on ahs1.seq_aluno_historico_ciclo_letivo = ahcl2.seq_aluno_historico_ciclo_letivo
												                and ahcl2.seq_aluno_historico = ah.seq_aluno_historico
										                where	ahs1.dat_exclusao is null
										                and		ahs1.dat_inicio_situacao < GETDATE())	
                join	ALN.aluno_historico_ciclo_letivo ahcl
		                on ahs.seq_aluno_historico_ciclo_letivo = ahcl.seq_aluno_historico_ciclo_letivo
                join	CAM.ciclo_letivo cl
		                on ahcl.seq_ciclo_letivo = cl.seq_ciclo_letivo
                join	MAT.situacao_matricula sm
		                on ahs.seq_situacao_matricula = sm.seq_situacao_matricula
                left join (	select	distinct
				                pati.seq_pessoa_atuacao,
				                intva.seq_tipo_vinculo_aluno,
				                ini.seq_nivel_ensino,
				                ini.seq_entidade_instituicao,
				                tti.seq_tipo_termo_intercambio, 
				                tti.dsc_tipo_termo_intercambio
			                from	ALN.pessoa_atuacao_termo_intercambio pati
			                join	ALN.termo_intercambio ti
					                on pati.seq_termo_intercambio = ti.seq_termo_intercambio
			                join	ALN.parceria_intercambio_tipo_termo pitt
					                on ti.seq_parceria_intercambio_tipo_termo = pitt.seq_parceria_intercambio_tipo_termo
			                join	ALN.tipo_termo_intercambio tti
					                on pitt.seq_tipo_termo_intercambio = tti.seq_tipo_termo_intercambio
			                join	ALN.instituicao_nivel_tipo_termo_intercambio intti
					                on tti.seq_tipo_termo_intercambio = intti.seq_tipo_termo_intercambio
			                join	ALN.instituicao_nivel_tipo_vinculo_aluno intva
					                on intti.seq_instituicao_nivel_tipo_vinculo_aluno = intva.seq_instituicao_nivel_tipo_vinculo_aluno
			                join	ORG.instituicao_nivel ini
					                on intva.seq_instituicao_nivel = ini.seq_instituicao_nivel
			                where	intti.ind_concede_formacao = 1
			                or		intva.ind_exige_parceria_intercambio_ingresso = 1
			                ) termo
			                on al.seq_pessoa_atuacao = termo.seq_pessoa_atuacao
			                and al.seq_tipo_vinculo_aluno = termo.seq_tipo_vinculo_aluno
			                and ah.seq_nivel_ensino = termo.seq_nivel_ensino
			                and pes.seq_entidade_instituicao = termo.seq_entidade_instituicao

                where 1 = 1";

        #endregion [ Querys ]

        #region [ DomainService ]

        private AlunoHistoricoDomainService AlunoHistoricoDomainService => Create<AlunoHistoricoDomainService>();

        private ConfiguracaoEventoLetivoDomainService ConfiguracaoEventoLetivoDomainService => Create<ConfiguracaoEventoLetivoDomainService>();

        private DivisaoTurmaDomainService DivisaoTurmaDomainService => Create<DivisaoTurmaDomainService>();

        private FormacaoEspecificaDomainService FormacaoEspecificaDomainService => Create<FormacaoEspecificaDomainService>();

        private HistoricoEscolarDomainService HistoricoEscolarDomainService => Create<HistoricoEscolarDomainService>();

        private IngressanteDomainService IngressanteDomainService => Create<IngressanteDomainService>();

        private InstituicaoNivelDomainService InstituicaoNivelDomainService => Create<InstituicaoNivelDomainService>();

        private InstituicaoNivelTipoComponenteCurricularDomainService InstituicaoNivelTipoComponenteCurricularDomainService => Create<InstituicaoNivelTipoComponenteCurricularDomainService>();

        private InstituicaoNivelTipoVinculoAlunoDomainService InstituicaoNivelTipoVinculoAlunoDomainService => Create<InstituicaoNivelTipoVinculoAlunoDomainService>();

        private MensagemPessoaAtuacaoDomainService MensagemPessoaAtuacaoDomainService => Create<MensagemPessoaAtuacaoDomainService>();

        private PessoaAtuacaoDomainService PessoaAtuacaoDomainService => Create<PessoaAtuacaoDomainService>();

        private PessoaDadosPessoaisDomainService PessoaDadosPessoaisDomainService => Create<PessoaDadosPessoaisDomainService>();

        private PessoaDomainService PessoaDomainService => Create<PessoaDomainService>();

        private PlanoEstudoItemDomainService PlanoEstudoItemDomainService => Create<PlanoEstudoItemDomainService>();

        private TermoIntercambioDomainService TermoIntercambioDomainService => Create<TermoIntercambioDomainService>();

        private TrabalhoAcademicoDomainService TrabalhoAcademicoDomainService => Create<TrabalhoAcademicoDomainService>();

        private InstituicaoNivelTipoTermoIntercambioDomainService InstituicaoNivelTipoTermoIntercambioDomainService => Create<InstituicaoNivelTipoTermoIntercambioDomainService>();

        private CicloLetivoDomainService CicloLetivoDomainService => Create<CicloLetivoDomainService>();

        private SolicitacaoServicoDomainService SolicitacaoServicoDomainService => Create<SolicitacaoServicoDomainService>();

        private PlanoEstudoDomainService PlanoEstudoDomainService => Create<PlanoEstudoDomainService>();

        private AlunoHistoricoSituacaoDomainService AlunoHistoricoSituacaoDomainService => Create<AlunoHistoricoSituacaoDomainService>();

        private SolicitacaoServicoEnvioNotificacaoDomainService SolicitacaoServicoEnvioNotificacaoDomainService => Create<SolicitacaoServicoEnvioNotificacaoDomainService>();

        private PessoaAtuacaoBeneficioDomainService PessoaAtuacaoBeneficioDomainService => Create<PessoaAtuacaoBeneficioDomainService>();

        private SituacaoMatriculaDomainService SituacaoMatriculaDomainService => Create<SituacaoMatriculaDomainService>();

        private ArquivoAnexadoDomainService ArquivoAnexadoDomainService => Create<ArquivoAnexadoDomainService>();

        private BeneficioHistoricoVigenciaDomainService BeneficioHistoricoVigenciaDomainService => Create<BeneficioHistoricoVigenciaDomainService>();

        private MotivoAlteracaoBeneficioDomainService MotivoAlteracaoBeneficioDomainService => Create<MotivoAlteracaoBeneficioDomainService>();

        private AlunoHistoricoCicloLetivoDomainService AlunoHistoricoCicloLetivoDomainService => Create<AlunoHistoricoCicloLetivoDomainService>();

        private ViewAlunoDomainService ViewAlunoDomainService => Create<ViewAlunoDomainService>();

        private CursoOfertaLocalidadeDomainService CursoOfertaLocalidadeDomainService => Create<CursoOfertaLocalidadeDomainService>();

        private CursoOfertaLocalidadeTurnoDomainService CursoOfertaLocalidadeTurnoDomainService => Create<CursoOfertaLocalidadeTurnoDomainService>();

        private InstituicaoNivelTipoDocumentoAcademicoDomainService InstituicaoNivelTipoDocumentoAcademicoDomainService => Create<InstituicaoNivelTipoDocumentoAcademicoDomainService>();

        private DocumentoConclusaoDomainService DocumentoConclusaoDomainService => Create<DocumentoConclusaoDomainService>();
        private DeclaracaoGenericaDomainService DeclaracaoGenericaDomainService => Create<DeclaracaoGenericaDomainService>();

        private DocumentoAcademicoHistoricoSituacaoDomainService DocumentoAcademicoHistoricoSituacaoDomainService => Create<DocumentoAcademicoHistoricoSituacaoDomainService>();
        private PessoaAtuacaoTermoIntercambioDomainService PessoaAtuacaoTermoIntercambioDomainService => Create<PessoaAtuacaoTermoIntercambioDomainService>();
        private ParceriaIntercambioTipoTermoDomainService ParceriaIntercambioTipoTermoDomainService => Create<ParceriaIntercambioTipoTermoDomainService>();
        private TipoTermoIntercambioDomainService TipoTermoIntercambioDomainService => Create<TipoTermoIntercambioDomainService>();
        private NivelEnsinoDomainService NivelEnsinoDomainService => Create<NivelEnsinoDomainService>();
        private TipoFuncionarioDomainService TipoFuncionarioDomainService => Create<TipoFuncionarioDomainService>();
        private FuncionarioVinculoDomainService FuncionarioVinculoDomainService => Create<FuncionarioVinculoDomainService>();
        private HierarquiaEntidadeDomainService HierarquiaEntidadeDomainService => Create<HierarquiaEntidadeDomainService>();

        #endregion [ DomainService ]

        #region [ Service ]

        private IIntegracaoFinanceiroService IntegracaoFinanceiroService => this.Create<IIntegracaoFinanceiroService>();

        private IIntegracaoAcademicoService IntegracaoAcademicoService => this.Create<IIntegracaoAcademicoService>();

        private ILocalidadeService LocalidadeService => Create<ILocalidadeService>();

        private IFinanceiroService FinanceiroService => Create<IFinanceiroService>();

        public IEstruturaOrganizacionalService EstruturaOrganizacionalService => Create<IEstruturaOrganizacionalService>();

        private ISMCReportMergeService SMCReportMergeService => this.Create<ISMCReportMergeService>();

        private IConfiguracaoDocumentoService ConfiguracaoDocumentoService => this.Create<IConfiguracaoDocumentoService>();

        #endregion [ Service ]

        #region [ Repositories ]

        private IAcademicoRepository AcademicoRepository => this.Create<IAcademicoRepository>();

        #endregion

        #region [ Constants ]

        private string[] IgnoredProps
        {
            get => new string[]
            {
                nameof(Aluno.DataAlteracao),
                nameof(Aluno.UsuarioAlteracao),
                nameof(Aluno.DataInclusao),
                nameof(Aluno.UsuarioInclusao),
            };
        }

        #endregion [ Constants ]

        #region [ Apis ]

        public SMCApiClient APIDocumentoGAD => SMCApiClient.Create("DocumentoGAD");

        #endregion [ Apis ]

        /// <summary>
        /// Busca os alunos com as depêndencias apresentadas na consulta detalhada
        /// </summary>
        /// <param name="seq">Sequencial do aluno</param>
        /// <param name="desativarFiltroDados">Desabilita o filtro de dados de hierarquia_entidade_organizacional para atender o caso de entidades compartilhadas da tela de consulta de solicitações de serviço </param>
        /// <returns>Dados do aluno</returns>
        public AlunoListaVO BuscarAlunoVisualizacaoDados(long seq, bool desativarFiltroDados = false)
        {
            var seqCicloLetivoAtual = this.BuscarCicloLetivoAtual(seq, desativarFiltroDados);

            if (desativarFiltroDados)
                this.DisableFilter(FILTER.HIERARQUIA_ENTIDADE_ORGANIZACIONAL);

            var aluno = this.SearchProjectionByKey(new SMCSeqSpecification<Aluno>(seq), p => new
            {
                p.Seq,
                p.Historicos.FirstOrDefault(f => f.Atual).SeqNivelEnsino,
                p.SeqTipoVinculoAluno,
                SeqTipoTermoIntercambio = (long?)p.TermosIntercambio.OrderByDescending(o => o.Seq).FirstOrDefault().TermoIntercambio.ParceriaIntercambioTipoTermo.SeqTipoTermoIntercambio,
                p.NumeroRegistroAcademico,
                p.DadosPessoais.Nome,
                p.DadosPessoais.NomeSocial,
                p.SeqPessoa,
                p.Pessoa.Cpf,
                p.Pessoa.NumeroPassaporte,
                p.Pessoa.DataNascimento,
                p.Pessoa.Falecido,
                DescricaoEntidadeResponsavel = p.Historicos.FirstOrDefault(f => f.Atual).EntidadeVinculo.Nome,
                DescricaoNivelEnsino = p.Historicos.FirstOrDefault(f => f.Atual).NivelEnsino.Descricao,
                DescricaoVinculo = p.TipoVinculoAluno.Descricao,
                DescricaoFormaIngresso = p.Historicos.FirstOrDefault(f => f.Atual).FormaIngresso.Descricao,
                DadosVinculo = p.Descricao,
                DescricaoCursoOferta = p.Historicos.FirstOrDefault(f => f.Atual).CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.CursoOferta.Descricao,
                DescricaoLocalidade = p.Historicos.FirstOrDefault(f => f.Atual).CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.HierarquiasEntidades.FirstOrDefault().ItemSuperior.Entidade.Nome,
                DescricaoTurno = p.Historicos.FirstOrDefault(f => f.Atual).CursoOfertaLocalidadeTurno.Turno.Descricao,
                p.Historicos.FirstOrDefault(f => f.Atual).DataAdmissao,
                DataPrevisaoConclusao = (DateTime?)p.Historicos.FirstOrDefault(f => f.Atual).PrevisoesConclusao.OrderByDescending(o => o.DataInclusao).FirstOrDefault().DataPrevisaoConclusao,
                DataLimiteConclusao = (DateTime?)p.Historicos.FirstOrDefault(f => f.Atual).PrevisoesConclusao.OrderByDescending(o => o.DataInclusao).FirstOrDefault().DataLimiteConclusao,
                DescricaoFormacaoEspecifica = p.Historicos.FirstOrDefault(f => f.Atual).Formacoes.FirstOrDefault(f => !f.DataFim.HasValue).FormacaoEspecifica.Descricao,
                DescricaoTipoTermoIntercambio = p.TermosIntercambio.OrderByDescending(o => o.Seq).FirstOrDefault().TermoIntercambio.ParceriaIntercambioTipoTermo.TipoTermoIntercambio.Descricao,
                DescricaoInstituicaoExterna = p.TermosIntercambio.OrderByDescending(o => o.Seq).FirstOrDefault().TermoIntercambio.ParceriaIntercambioInstituicaoExterna.InstituicaoExterna.Nome,
                DataInicioTermoIntercambio = (DateTime?)p.TermosIntercambio.Min(mt => mt.TermoIntercambio.Vigencias.Min(m => m.DataInicio)),
                DataFimTermoIntercambio = (DateTime?)p.TermosIntercambio.Max(mt => mt.TermoIntercambio.Vigencias.Max(m => m.DataFim)),
                OrientacoesPessoaAtuacao = p.OrientacoesPessoaAtuacao.OrderByDescending(o => o.DataInclusao)
                    .Where(w => w.Orientacao.OrientacoesColaborador.Any(a => (!a.DataInicioOrientacao.HasValue || a.DataInicioOrientacao <= DateTime.Today)
                                                                          && (!a.DataFimOrientacao.HasValue || a.DataFimOrientacao >= DateTime.Today)))
                    .Select(s => new
                    {
                        TipoOrientacao = new
                        {
                            s.Orientacao.TipoOrientacao.Seq,
                            s.Orientacao.TipoOrientacao.Descricao
                        },
                        OrientacoesColaborador = s.Orientacao.OrientacoesColaborador
                            .Where(w => (!w.DataInicioOrientacao.HasValue || w.DataInicioOrientacao <= DateTime.Today)
                                     && (!w.DataFimOrientacao.HasValue || w.DataFimOrientacao >= DateTime.Today))
                            .Select(soc => new
                            {
                                NomeOrientador = soc.Colaborador.DadosPessoais.Nome,
                                NomeSocialOrientador = soc.Colaborador.DadosPessoais.NomeSocial,
                                soc.TipoParticipacaoOrientacao
                            })
                    }),
                SeqsEntidadesSuperioresCurso = p.Historicos.FirstOrDefault(f => f.Atual).CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.CursoOferta.Curso.HierarquiasEntidades.Select(h => h.ItemSuperior.SeqEntidade),
                FormacoesEspecificas = p.Historicos.FirstOrDefault(f => f.Atual).Formacoes.Where(f => !f.DataFim.HasValue).Select(s => (long)s.SeqFormacaoEspecifica),
                NomeInstituicaoTransferenciaExterna = p.InstituicaoTransferenciaExterna.Nome,
                p.CursoTransferenciaExterna,
                p.CodigoAlunoMigracao
            });
            var alunoVO = SMCMapperHelper.Create<AlunoListaVO>(aluno);
            var situacaoAtual = AlunoHistoricoSituacaoDomainService.BuscarSituacaoAtualAluno(seq);
            alunoVO.DescricaoSituacaoMatricula = $"{situacaoAtual.DescricaoCicloLetivo} - {situacaoAtual.Descricao}";

            var configuracaoVinculo = this.InstituicaoNivelTipoVinculoAlunoDomainService
                .BuscarConfiguracaoVinculo(aluno.SeqNivelEnsino, aluno.SeqTipoVinculoAluno);

            // RN_ALN_029 - Descrição Vínculo Tipo Termo Intercâmbio
            alunoVO.DescricaoVinculo = RecuperarVinculoAluno(configuracaoVinculo,
                                                             aluno.SeqTipoTermoIntercambio,
                                                             aluno.DescricaoVinculo,
                                                             aluno.DescricaoTipoTermoIntercambio);

            var specFormacoes = new FormacaoEspecificaFilterSpecification() { SeqEntidades = aluno.SeqsEntidadesSuperioresCurso.ToList() };
            var formacoesEntidadesResponsaveisCurso = this.FormacaoEspecificaDomainService
                .SearchBySpecification(specFormacoes, IncludesFormacaoEspecifica.TipoFormacaoEspecifica)
                .ToList();

            alunoVO.FormacoesEspecificas = new List<string>();
            foreach (var formacaoIngressante in aluno.FormacoesEspecificas)
            {
                var hierarquiaFormacaoIngressante = this.FormacaoEspecificaDomainService.GerarHierarquiaFormacaoEspecifica(formacaoIngressante, formacoesEntidadesResponsaveisCurso);
                hierarquiaFormacaoIngressante.SMCForEach(x => alunoVO.FormacoesEspecificas.Add($"[{x.DescricaoTipoFormacaoEspecifica}] {x.Descricao}"));
            }

            // RN_PES_023 - Nome e Nome Social - Visão Administrativo
            if (!string.IsNullOrEmpty(aluno.NomeSocial))
                alunoVO.Nome = $"{aluno.NomeSocial} ({aluno.Nome})";

            var seqTipoOrientacaoExigido = configuracaoVinculo.TiposOrientacao?.FirstOrDefault(f => f.CadastroOrientacaoAluno == CadastroOrientacao.Exige)?.SeqTipoOrientacao;
            if (seqTipoOrientacaoExigido.HasValue)
            {
                var orientacao = aluno.OrientacoesPessoaAtuacao.FirstOrDefault(f => f.TipoOrientacao.Seq == seqTipoOrientacaoExigido);
                if (orientacao != null)
                {
                    alunoVO.TipoOrientacao = orientacao.TipoOrientacao.Descricao;
                    alunoVO.Orientacoes = orientacao.OrientacoesColaborador.Select(s => new AlunoOrientacaoListaVO()
                    {
                        NomeOrientador = s.NomeOrientador,
                        NomeSocialOrientador = s.NomeSocialOrientador,
                        TipoParticipacaoOrientacao = s.TipoParticipacaoOrientacao
                    })
                    .OrderBy(o => o.NomeFormatado);
                }
            }

            alunoVO.ExigeParceriaIntercambioIngresso = configuracaoVinculo.ExigeParceriaIntercambioIngresso;
            alunoVO.ExigePeriodoIntercambioTermo = configuracaoVinculo.TiposTermoIntercambio
                ?.FirstOrDefault(f => f.SeqTipoTermoIntercambio == alunoVO.SeqTipoTermoIntercambio)
                ?.ExigePeriodoIntercambioTermo
                ?? false;

            try
            {
                alunoVO.CodigoPessoaCAD = this.PessoaDomainService.BuscarCodigoDePessoaNosDadosMestres(aluno.SeqPessoa, TipoPessoa.Fisica, null);
            }
            catch (CodigoPessoaDadosMestreException)
            {
                throw new CodigoPessoaNaoEncontradoDadosMestreException();
            }

            if (desativarFiltroDados)
                this.EnableFilter(FILTER.HIERARQUIA_ENTIDADE_ORGANIZACIONAL);

            return alunoVO;
        }

        public (long SeqInstituicao, long SeqNivelEnsino, long SeqTipoVinculoAluno) BuscarInstituicaoNivelEnsinoESequenciaisPorAluno(long seqPessoaAtuacao)
        {
            var dadosOrigem = PessoaAtuacaoDomainService.RecuperaDadosOrigem(seqPessoaAtuacao);

            //Recupera os sequenciais do ingressante para recuperar os parâmetros de acordo com o vínculo
            var sequenciais = this.SearchProjectionByKey(new SMCSeqSpecification<Aluno>(seqPessoaAtuacao),
                                                         p => new
                                                         {
                                                             SeqInstituicaoEnsino = p.Pessoa.SeqInstituicaoEnsino,
                                                             SeqTipoVinculoAluno = p.SeqTipoVinculoAluno
                                                         });

            return (sequenciais.SeqInstituicaoEnsino, dadosOrigem.SeqNivelEnsino, sequenciais.SeqTipoVinculoAluno);
        }

        /// <summary>
        /// Recupera os dados para chamar o sistema antigo e renderizar as páginas no portal do aluno
        /// </summary>
        /// <param name="seq"></param>
        /// <returns></returns>
        public (long? CodigoAlunoMigracao, long? SeqOrigem) BuscarDadosIntegracaoSGAAntigo(long seq)
        {
            var dadosOrigem = PessoaAtuacaoDomainService.RecuperaDadosOrigem(seq);
            var codigoMigracao = this.SearchProjectionByKey(new SMCSeqSpecification<Aluno>(seq), x => x.CodigoAlunoMigracao);
            return (codigoMigracao, dadosOrigem.SeqOrigem);
        }

        public (long SeqIngressante, long SeqSolicitacaoMatricula) BuscarDadosIngressanteAluno(long seqAluno)
        {
            var dados = SearchProjectionByKey(new SMCSeqSpecification<Aluno>(seqAluno), x => new
            {
                SeqIngressante = x.Historicos.FirstOrDefault(h => h.Atual).SeqIngressante,
                SeqSolicitacaoMatricula = x.Historicos.FirstOrDefault(h => h.Atual).SeqSolicitacaoServico,
            });

            return (dados.SeqIngressante.GetValueOrDefault(), dados.SeqSolicitacaoMatricula.GetValueOrDefault());
        }

        public List<SMCDatasourceItem> BuscarAlunosSelect(AlunoFilterSpecification filtros, bool exibirVinculo, bool carregarVinculoAtivo)
        {
            var registros = this.SearchProjectionBySpecification(filtros, x => new
            {
                Seq = x.Seq,
                DescricaoVinculo = x.TipoVinculoAluno.TipoVinculoAlunoFinanceiro == TipoVinculoAlunoFinanceiro.CursoRegular ? x.Descricao : x.Descricao + " - " + x.TipoVinculoAluno.Descricao,
                DescricaoNome = x.DadosPessoais.NomeSocial ?? x.DadosPessoais.Nome
            }).Select(s => new SMCDatasourceItem()
            {
                Seq = s.Seq,
                Descricao = exibirVinculo ? s.DescricaoVinculo : s.DescricaoNome,
            })
            .ToList();

            // Percorre os alunos e verifica se o vínculo está ativo
            if (carregarVinculoAtivo && registros != null)
            {
                foreach (var item in registros)
                {
                    item.DataAttributes = new List<SMCKeyValuePair>();
                    item.DataAttributes.Add(new SMCKeyValuePair { Key = "ativo", Value = AlunoHistoricoSituacaoDomainService.BuscarSituacaoAtualAluno(item.Seq).VinculoAlunoAtivo.GetValueOrDefault() ? "ativo" : string.Empty });
                }
            }

            return registros;
        }

        /// <summary>
        /// Grava alterações dos dados pessoais de um aluno
        /// </summary>
        /// <param name="aluno">Dados pessoais do aluno a ser atualizado</param>
        /// <returns>Sequencial do aluno atualizado</returns>
        /// <exception cref="AtuacaoSemTelefoneException">Caso não seja informado nenhum telefone</exception>
        public long SalvarAluno(AlunoVO alunoVO)
        {
            PessoaAtuacaoDomainService.RestaurarCamposReadonlyCpf(ref alunoVO);
            PessoaDomainService.FormatarNomesPessoaVo(ref alunoVO);
            IngressanteDomainService.ValidarContatosAtuacao(alunoVO.Enderecos, alunoVO.EnderecosEletronicos);
            if (alunoVO.Telefones.SMCCount() == 0)
            {
                throw new AtuacaoSemTelefoneException();
            }

            var aluno = alunoVO.Transform<Aluno>();
            var alunoBanco = SearchByKey(new SMCSeqSpecification<Aluno>(aluno.Seq), IncludesAluno.DadosPessoais);

            // Replicação dos seqs
            aluno.DadosPessoais.Seq = aluno.SeqPessoaDadosPessoais;
            aluno.DadosPessoais.SeqPessoa = aluno.SeqPessoa;
            aluno.Pessoa.Seq = aluno.SeqPessoa;
            aluno.Enderecos.SMCForEach(f => f.SeqPessoaAtuacao = aluno.Seq);
            // Campo não representado nas outras camadas, valor
            aluno.TipoAtuacao = TipoAtuacao.Aluno;
            // Remoção da formatação de cpf
            aluno.Pessoa.Cpf = aluno.Pessoa.Cpf.SMCRemoveNonDigits();
            aluno.Descricao = alunoBanco.Descricao;
            // Se o arquivo do logotipo não foi alterado, atualiza com conteúdo com o que está no banco
            this.EnsureFileIntegrity(aluno.DadosPessoais, x => x.SeqArquivoFoto, x => x.ArquivoFoto);

            this.PessoaDomainService.ValidarQuantidadesFiliacao(aluno.Pessoa, TipoAtuacao.Aluno);
            this.PessoaDomainService.ValidarCamposPassaporte(aluno.Pessoa);

            var dadosPessoaisAlterados = !SMCReflectionHelper.CompareExistingPrimitivePropertyValues(alunoBanco.DadosPessoais, aluno.DadosPessoais, false, IgnoredProps)
                                       || aluno.DadosPessoais.ArquivoFoto?.State == SMCUploadFileState.Changed
                                       || aluno.DadosPessoais.SeqArquivoFoto.HasValue != alunoBanco.DadosPessoais.SeqArquivoFoto.HasValue;

            using (ISMCUnitOfWork transacao = SMCUnitOfWork.Begin())
            {
                if (dadosPessoaisAlterados)
                {
                    // Inclui os novos dados pessoais no histórico da pessoa
                    aluno.DadosPessoais.Seq = 0;
                    //FIX: Para não gravar um arquivo
                    if (aluno.DadosPessoais.ArquivoFoto?.State == SMCUploadFileState.Unchanged)
                        aluno.DadosPessoais.ArquivoFoto = null;
                    this.PessoaDadosPessoaisDomainService.SaveEntity(aluno.DadosPessoais);

                    // Atualiza a referência de dados pessoais do aluno
                    aluno.SeqPessoaDadosPessoais = aluno.DadosPessoais.Seq;
                }

                // Atualiza os dados de pessoa
                this.PessoaDomainService.SaveEntity(aluno.Pessoa);

                // Remove as referências de segundo nível dos contatos para evitar conflitos com o *diff
                aluno.Enderecos.SMCForEach(f => f.PessoaEndereco = null);
                aluno.Telefones.SMCForEach(f => f.Telefone = null);
                aluno.EnderecosEletronicos.SMCForEach(f => f.EnderecoEletronico = null);
                // Atualiza os dados de contato
                SaveEntity(aluno);

                transacao.Commit();
            }

            return aluno.Seq;
        }

        /// <summary>
        /// Busca um aluno com seus dados pessoais
        /// </summary>
        /// <param name="seq">Sequencial do aluno</param>
        /// <returns>Dados do aluno</returns>
        public AlunoVO BuscarAluno(long seq)
        {
            PessoaDomainService.ValidarTipoAtuacaoConfiguradoNaInstituicao(TipoAtuacao.Aluno);

            var includesAluno = IncludesAluno.DadosPessoais_ArquivoFoto
                                    | IncludesAluno.Enderecos_PessoaEndereco_Endereco
                                    | IncludesAluno.EnderecosEletronicos_EnderecoEletronico
                                    | IncludesAluno.Pessoa_Filiacao
                                    | IncludesAluno.Telefones_Telefone;

            var aluno = this.SearchByKey(new SMCSeqSpecification<Aluno>(seq), includesAluno);
            var alunoVO = aluno.Transform<AlunoVO>();

            if (aluno.DadosPessoais.SeqArquivoFoto.HasValue)
                alunoVO.ArquivoFoto.GuidFile = aluno.DadosPessoais.ArquivoFoto.UidArquivo.ToString();

            if (alunoVO.Enderecos.SMCCount() > 0)
            {
                // Preenche as descrições dos países
                var paises = this.LocalidadeService.BuscarPaisesValidosCorreios();
                alunoVO.Enderecos.SMCForEach(f => f.DescPais = paises.SingleOrDefault(s => s.Codigo == f.CodigoPais)?.Nome);
            }

            PessoaAtuacaoDomainService.AplicarValidacaoPermiteAlterarCpf(ref alunoVO);

            return alunoVO;
        }

        /// <summary>
        /// Busca um aluno com seus dados pessoais para mobile
        /// </summary>
        /// <param name="seq">Sequencial do aluno</param>
        /// <returns>Dados do aluno</returns>
        public AlunoVO BuscarAlunoMobile(long seq)
        {
            var includesAluno = IncludesAluno.DadosPessoais_ArquivoFoto
                                | IncludesAluno.EnderecosEletronicos_EnderecoEletronico
                                | IncludesAluno.Pessoa;

            var aluno = this.SearchByKey(new SMCSeqSpecification<Aluno>(seq), includesAluno);
            var alunoVO = aluno.Transform<AlunoVO>();

            return alunoVO;
        }

        public RelatorioDisciplinasCursadasVO RelatorioDisciplinasCursadas(RelatorioDisciplinasCursadasFiltroVO filtro)
        {
            var dadosRelatorio = new RelatorioDisciplinasCursadasVO();

            if (filtro.SelectedValues != null && filtro.SelectedValues.Count > 0)
            {
                var specAluno = new SMCContainsSpecification<Aluno, long>(a => a.Seq, filtro.SelectedValues.ToArray());
                dadosRelatorio.Alunos = SearchProjectionBySpecification(specAluno, a => new ItemRelatorioDisciplinasCursadasVO()
                {
                    SeqAluno = a.Seq,
                    SeqHistoricoEscolar = a.Historicos.FirstOrDefault(h => h.Atual).Seq,
                    Aluno = a.DadosPessoais.Nome.Trim(),
                    CursoOferta = a.Historicos.FirstOrDefault(h => h.Atual).CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.CursoOferta.Descricao,
                    Instituicao = a.Historicos.FirstOrDefault(h => h.Atual).EntidadeVinculo.InstituicaoEnsino.Nome,
                    NivelEnsino = a.Historicos.FirstOrDefault(h => h.Atual).NivelEnsino.Descricao,
                    TipoVinculo = a.Historicos.FirstOrDefault(h => h.Atual).FormaIngresso.TipoVinculoAluno.Descricao,
                    Programa = a.Historicos.FirstOrDefault(h => h.Atual).EntidadeVinculo.Nome,
                    SeqInstituicaoEnsino = a.Pessoa.SeqInstituicaoEnsino
                }).ToList();

                //Buscando os demais dados para o relatório.
                foreach (var aluno in dadosRelatorio.Alunos)
                {
                    //Disciplinas cursadas
                    BuscarDisciplinasCursadasAluno(aluno, filtro.ExibirNaDeclaracao, filtro.ExibirEmentasComponentesCurriculares);
                }
            }
            else
            {
                throw new Exception("Favor selecionar ao menos um aluno para gerar o relatório");
            }

            return dadosRelatorio;
        }

        /// <summary>
        /// Buscar dados da matricula do aluno
        /// </summary>
        /// <param name="seq">Sequencial do aluno</param>
        public ConsultaDadosAlunoVO BuscarDadosMatriculaAluno(long seq)
        {
            //Buscar Ciclo Letivo Atual
            long seqCicloLetivoAtual = BuscarCicloLetivoAtual(seq);

            ///Dados cabecalho
            ConsultaDadosAlunoVO retorno = AlunoHistoricoDomainService.SearchProjectionByKey(new AlunoHistoricoFilterSpecification() { SeqAluno = seq, Atual = true },
                                            p => new ConsultaDadosAlunoVO
                                            {
                                                ///Dados Aluno
                                                SeqAluno = seq,
                                                RA = p.Aluno.NumeroRegistroAcademico,
                                                Nome = string.IsNullOrEmpty(p.Aluno.DadosPessoais.NomeSocial) ? p.Aluno.DadosPessoais.Nome : p.Aluno.DadosPessoais.NomeSocial + " (" + p.Aluno.DadosPessoais.Nome + ")",
                                                VinculoAtivo = p.Aluno.Ativo,
                                                Falecido = p.Aluno.Pessoa.Falecido,
                                                DataAdmissao = p.DataAdmissao,
                                                DadosVinculo = p.NivelEnsino.Descricao + " - " + p.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.CursoOferta.Descricao + " - " + p.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.HierarquiasEntidades.FirstOrDefault().ItemSuperior.Entidade.Nome + " - " + p.CursoOfertaLocalidadeTurno.Turno.Descricao,
                                                Entidade = p.EntidadeVinculo.Nome,
                                                Vinculo = p.Aluno.TipoVinculoAluno.Descricao,
                                                ///Dados Matricula
                                                CicloLetivoIngresso = p.CicloLetivo.Descricao,
                                                OfertaCurso = p.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.CursoOferta.Descricao,
                                                Localidade = p.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.HierarquiasEntidades.FirstOrDefault().ItemSuperior.Entidade.Nome,
                                                Turno = p.CursoOfertaLocalidadeTurno.Turno.Descricao,
                                                OfertaMatriz = p.HistoricosCicloLetivo.FirstOrDefault(w => w.SeqCicloLetivo == seqCicloLetivoAtual).PlanosEstudo.FirstOrDefault(pa => pa.Atual).MatrizCurricularOferta.MatrizCurricular.Descricao,
                                                //Ingressante = (!p.SeqIngressante.HasValue ? "-" : (p.SeqIngressante.ToString() + " - " + p.Ingressante.FormaIngresso.Descricao)),
                                                IdIngressante = !p.SeqIngressante.HasValue ? "-" : p.SeqIngressante.ToString(),
                                                DescricaoFormaIngresso = p.FormaIngresso.Descricao,
                                                ///Lista de ciclos
                                                CiclosLetivos = p.HistoricosCicloLetivo.Select(s => new CicloLetivoVO
                                                {
                                                    Descricao = s.CicloLetivo.Descricao,
                                                    Seq = s.CicloLetivo.Seq,
                                                    AnoNumeroCicloLetivo = s.CicloLetivo.AnoNumeroCicloLetivo,
                                                    CiclosLetivosSituacoes = s.AlunoHistoricoSituacao.Select(sa => new CicloLetivoSituacaoVO
                                                    {
                                                        DataInicio = sa.DataInicioSituacao,
                                                        Situacao = sa.SituacaoMatricula.Descricao,
                                                        NumeroProtocolo = sa.SolicitacaoServico.NumeroProtocolo,
                                                        Observacao = sa.Observacao,
                                                        DataInclusao = sa.DataInclusao,
                                                        UsuarioInclusao = sa.UsuarioInclusao,
                                                        NumeroProtocoloExclusao = sa.SolicitacaoServicoExclusao.NumeroProtocolo,
                                                        ObservacaoExclusao = sa.ObservacaoExclusao,
                                                        DataExclusao = sa.DataExclusao,
                                                        ExisteDataExclusao = sa.DataExclusao.HasValue,
                                                        UsuarioExclusao = sa.UsuarioExclusao,
                                                        TokenSituacao = sa.SituacaoMatricula.Token,
                                                        FlagVerDadosIntercambio = (sa.SituacaoMatricula.Token == TOKENS_SITUACAO_MATRICULA.MATRICULADO_MOBILIDADE),
                                                        SeqCicloLetivoSituacao = sa.Seq,
                                                        SeqSolicitacaoServico = sa.SeqSolicitacaoServico,
                                                        SeqSolicitacaoServicoExclusao = sa.SeqSolicitacaoServicoExclusao,
                                                        SeqPeriodoIntercambio = sa.SeqPeriodoIntercambio ?? 0,
                                                        SeqArquivoAnexado = sa.SeqArquivoAnexado,
                                                        ArquivoAnexado = sa.SeqArquivoAnexado.HasValue ? new SMCUploadFile
                                                        {
                                                            FileData = sa.ArquivoAnexado.Conteudo,
                                                            GuidFile = sa.ArquivoAnexado.UidArquivo.ToString(),
                                                            Name = sa.ArquivoAnexado.Nome,
                                                            Size = sa.ArquivoAnexado.Tamanho,
                                                            Type = sa.ArquivoAnexado.Tipo
                                                        } : null
                                                    }).OrderBy(o => o.DataInicio).ToList(),
                                                    PlanosEstudos = s.PlanosEstudo.Where(w => w.Atual).Select(sp => new CicloLetivoPlanoEstudoVO
                                                    {
                                                        OfertaMatriz = sp.MatrizCurricularOferta.MatrizCurricular.Descricao
                                                    }).ToList()
                                                }).ToList()
                                            });

            /* - Exibir a descrição da matriz curricular da oferta de matriz associada ao plano de estudo mais atual do aluno, de acordo com o ciclo em questão.
			 * - Caso não exista plano de estudo para o ciclo em questão, exibir o plano do ciclo anterior e mais próximo ao ciclo em questão que possua plano de estudo.*/
            if (string.IsNullOrWhiteSpace(retorno.OfertaMatriz))
                retorno.OfertaMatriz = AlunoHistoricoDomainService.SearchProjectionByKey(new AlunoHistoricoFilterSpecification() { SeqAluno = seq, Atual = true }, p => p.HistoricosCicloLetivo.Where(w => w.PlanosEstudo.Any()).OrderByDescending(o => o.DataInclusao).FirstOrDefault(h => !h.DataExclusao.HasValue).PlanosEstudo.OrderByDescending(o => o.DataInclusao).FirstOrDefault(pl => pl.Atual).MatrizCurricularOferta.MatrizCurricular.Descricao);

            retorno.Situacao = AlunoHistoricoSituacaoDomainService.BuscarSituacaoAtualAluno(seq).Descricao;

            ///Formatar dados da inclusão e exclusão da situação e validar linha de destaque
            foreach (var item in retorno.CiclosLetivos)
            {
                var linhaDestaque = item.CiclosLetivosSituacoes.OrderByDescending(o => o.DataInicio).FirstOrDefault(f => f.DataInicio <= DateTime.Now && !f.DataExclusao.HasValue);
                foreach (var situacao in item.CiclosLetivosSituacoes)
                {
                    situacao.Inclusao = $"{situacao.UsuarioInclusao} em {situacao.DataInclusao}";

                    ///Verifica se existe usuario de exclusão
                    if (!string.IsNullOrEmpty(situacao.UsuarioExclusao))
                    {
                        situacao.Exclusao = $"{situacao.UsuarioExclusao} em {situacao.DataExclusao}";
                        situacao.ExisteDataExclusao = true;
                    }

                    if (linhaDestaque != null && situacao.SeqCicloLetivoSituacao == linhaDestaque.SeqCicloLetivoSituacao)
                    {
                        situacao.EmDestaque = true;
                    }
                }

                var sitDestaque = item.CiclosLetivosSituacoes.FirstOrDefault(f => f.EmDestaque);
                if (sitDestaque != null)
                    item.DescricaoFormatada = $"{item.Descricao} - {sitDestaque?.Situacao}";
                else
                    item.DescricaoFormatada = item.Descricao;
            }

            var dadosAluno = AlunoHistoricoDomainService.SearchProjectionBySpecification(new AlunoHistoricoFilterSpecification() { SeqAluno = seq, Atual = true },
                p => new
                {
                    //seqIngressante = p.Ingressante.Seq,
                    //seqSolicitacaoMatricula = (long)p.HistoricosCicloLetivo.OrderByDescending(o => o.Seq).FirstOrDefault().PlanosEstudo.FirstOrDefault().SeqSolicitacaoServico,
                    seqNivelEnsino = p.Aluno.Historicos.Where(w => w.Atual == true).FirstOrDefault().NivelEnsino.Seq,
                    seqTipoVinculoAluno = p.Aluno.TipoVinculoAluno.Seq,
                    seqTipoTermoIntercambio = (p.Aluno.TermosIntercambio.Count > 0) ? p.Aluno.TermosIntercambio.OrderByDescending(o => o.Seq).FirstOrDefault().TermoIntercambio.ParceriaIntercambioTipoTermo.SeqTipoTermoIntercambio : 0,
                    DescricaoTipoIntercambio = (p.Aluno.TermosIntercambio.Count > 0) ? p.Aluno.TermosIntercambio.OrderByDescending(o => o.Seq).FirstOrDefault().TermoIntercambio.ParceriaIntercambioTipoTermo.TipoTermoIntercambio.Descricao : string.Empty,
                }).FirstOrDefault();

            var configuracaoVinculo = this.InstituicaoNivelTipoVinculoAlunoDomainService.BuscarConfiguracaoVinculo(dadosAluno.seqNivelEnsino, dadosAluno.seqTipoVinculoAluno);
            retorno.Vinculo = RecuperarVinculoAluno(configuracaoVinculo, dadosAluno.seqTipoTermoIntercambio, retorno.Vinculo, dadosAluno.DescricaoTipoIntercambio);

            var listaTurmas = PlanoEstudoItemDomainService.BuscarTurmasItensPlanoEstudo(seq);

            var listaAtividades = PlanoEstudoItemDomainService.BuscarSolicitacaoMatriculaAtividadesItens(seq, null);

            var tipoIntercambioConcedeFormacao = this.InstituicaoNivelTipoTermoIntercambioDomainService.SearchBySpecification(
                                                      new InstituicaoNivelTipoTermoIntercambioFilterSpecification()
                                                      {
                                                          SeqTipoTermoIntercambio = dadosAluno.seqTipoTermoIntercambio,
                                                          SeqInstituicaoNivelTipoVinculoAluno = configuracaoVinculo.Seq
                                                      }).ToList();

            //Percorre os ciclos letivos
            foreach (var cicloLetivo in retorno.CiclosLetivos)
            {
                //Cria o contador apenas para compor a treeview corretamente, para que não haja conflito de ID´s
                var contadorSeqCurso = listaTurmas.Count() > 0 ? listaTurmas.Max(t => t.Seq) : 0;

                //Percorre os cursos para criação dos nós pais da treeview
                listaTurmas.GroupBy(t => t.DescricaoCursoLocalidadeTurno).OrderBy(w => w.Key).SMCForEach(f =>
                  {
                      contadorSeqCurso++;

                      //Recupera as turmas do ciclo letivo
                      var turmasCiclo = f.Where(w => w.DescricaoCicloLetivoInicio == cicloLetivo.Descricao).ToList();

                      //Percorre as turmas
                      foreach (var turmaCiclo in turmasCiclo)
                      {
                          //Percorre os planos de estudo
                          foreach (var planoEstudo in cicloLetivo.PlanosEstudos)
                          {
                              //Instancia a lista de turmas caso não tenha sido instanciada
                              if (planoEstudo.Turmas == null)
                                  planoEstudo.Turmas = new List<CicloLetivoPlanoEstudoTurmasVO>();

                              //Adiciona o nó pai da treeview, caso ainda não exista na lista
                              if (!planoEstudo.Turmas.Any(t => t.Seq == contadorSeqCurso))
                              {
                                  planoEstudo.Turmas.Add(new CicloLetivoPlanoEstudoTurmasVO
                                  {
                                      Descricao = f.Key,
                                      Seq = contadorSeqCurso,
                                      SeqPai = null,
                                  });
                              }

                              //Adiciona a turma
                              planoEstudo.Turmas.Add(new CicloLetivoPlanoEstudoTurmasVO
                              {
                                  Descricao = turmaCiclo.TurmaFormatado,
                                  Seq = turmaCiclo.Seq,
                                  SeqPai = contadorSeqCurso,
                              });

                              ///Adiciona as divisoes das turmas
                              foreach (var divisoes in turmaCiclo.TurmaMatriculaDivisoes)
                              {
                                  planoEstudo.Turmas.Add(new CicloLetivoPlanoEstudoTurmasVO
                                  {
                                      Descricao = divisoes.DivisaoTurmaRelatorioDescricao,
                                      Seq = divisoes.SeqDivisaoTurma.Value * -1, ///Transforma o seq em negativo para não correr o risco de se ter seq pai duplicado
                                      SeqPai = turmaCiclo.Seq,
                                      ExibirMenu = true
                                  });
                              }
                          }
                      }
                  });

                var atividadesCiclo = listaAtividades.Where(w => w.CicloLetivo == cicloLetivo.Descricao).ToList();
                foreach (var atividade in atividadesCiclo)
                {
                    foreach (var planoEstudo in cicloLetivo.PlanosEstudos)
                    {
                        if (planoEstudo.Atividades == null)
                        {
                            planoEstudo.Atividades = new List<CicloLetivoPlanoEstudoAtividadesVO>();
                        }

                        ///Preenche as atividades
                        if (planoEstudo.Atividades.Count == 0)
                        {
                            foreach (var atividades in atividadesCiclo)
                            {
                                planoEstudo.Atividades.Add(new CicloLetivoPlanoEstudoAtividadesVO
                                {
                                    Descricao = atividades.DescricaoFormatada,
                                    Seq = atividades.Seq,
                                });
                            }
                        }
                    }
                }

                // NV16
                ///Exibir agrupamento quando o aluno estiver na situação MATRICULADO_MOBILIDADE.Exibir dados de
                ///intercâmbio referentes aos períodos de intercâmbio da situação em questão.
                ///Exibir agrupamento quando o aluno possui um tipo de termo que não concede formação e estiver na situação
                ///MATRICULADO.
                if (tipoIntercambioConcedeFormacao.SMCAny())
                {
                    foreach (var cicloSituacoes in cicloLetivo.CiclosLetivosSituacoes)
                    {
                        if (cicloSituacoes.TokenSituacao == TOKENS_SITUACAO_MATRICULA.MATRICULADO_MOBILIDADE)
                        {
                            cicloSituacoes.FlagVerDadosIntercambio = true;
                        }
                        //Task 36110: Retirar a seguinte consistência da exibição do "olhinho" que exibe informações de intercâmbio, na situação de matrícula:
                        //            Exibir agrupamento quando o aluno possui um tipo de termo que não concede formação e estiver na situação MATRICULADO.
                        //else if (!tipoIntercambioConcedeFormacao.FirstOrDefault().ConcedeFormacao && cicloSituacoes.TokenSituacao == TOKEN_CICLO_LETIVO_SITUACAO.MATRICULADO)
                        //{
                        //    cicloSituacoes.FlagVerDadosIntercambio = true;
                        //}
                        else
                        {
                            cicloSituacoes.FlagVerDadosIntercambio = false;
                        }
                    }
                }
            }

            ///Ordenação
            retorno.CiclosLetivos = retorno.CiclosLetivos.OrderByDescending(o => o.AnoNumeroCicloLetivo).ToList();

            ///Assegura se por ventura existe alguma turma repetida por algum erro de massa de dados
            foreach (var ciclos in retorno.CiclosLetivos)
            {
                foreach (var item in ciclos.PlanosEstudos)
                {
                    if (item.Turmas.SMCAny())
                    {
                        item.Turmas = item.Turmas.SMCDistinct(d => d.Seq).ToList();
                    }
                }
            }

            return retorno;
        }

        /// <summary>
        /// Busca os alunos para emissão da identidade estudantil pelos seqs informados
        /// </summary>
        /// <param name="filtro">Seqs dos alunos para pesquisa</param>
        /// <returns>Lista de alunos para emissão da identidade estudantil</returns>
        public List<IdentidadeEstudantilVO> BuscarAlunosIdentidadeEstudantil(List<long> seqsAlunos)
        {
            var specAluno = new SMCContainsSpecification<Aluno, long>(a => a.Seq, seqsAlunos.ToArray());

            var alunos = this.SearchProjectionBySpecification(specAluno,
                a => new IdentidadeEstudantilVO()
                {
                    SeqAluno = a.Seq,
                    NumeroRegistroAcademico = a.NumeroRegistroAcademico,
                    SeqPessoa = a.SeqPessoa,
                    SeqCicloLetivo = a.Historicos.FirstOrDefault(f => f.Atual).HistoricosCicloLetivo.OrderByDescending(o => o.CicloLetivo.Ano).OrderByDescending(o => o.CicloLetivo.Numero).FirstOrDefault(f => !f.DataExclusao.HasValue).SeqCicloLetivo,
                    SeqPrograma = a.Historicos.FirstOrDefault(f => f.Atual).CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.CursoUnidade.HierarquiasEntidades.FirstOrDefault().ItemSuperior.SeqEntidade,
                    Nome = a.DadosPessoais.Nome,
                    CodigoAlunoMigracao = a.CodigoAlunoMigracao,
                    TipoVinculoAlunoFinanceiro = a.TipoVinculoAluno.TipoVinculoAlunoFinanceiro,
                    DescricaoCurso = a.Historicos.FirstOrDefault(f => f.Atual).CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.CursoOferta.Curso.Nome,
                    DescricaoUnidade = a.Historicos.FirstOrDefault(f => f.Atual).CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.CursoUnidade.HierarquiasEntidades.FirstOrDefault().ItemSuperior.Entidade.Nome,
                    NumeroVia = 1,
                    DataValidade = a.Historicos.FirstOrDefault(f => f.Atual).PrevisoesConclusao.OrderByDescending(p => p.Seq).FirstOrDefault().DataPrevisaoConclusao,
                    Frente = true,
                    PlanoEstudoItens = a.Historicos.FirstOrDefault(f => f.Atual).HistoricosCicloLetivo.OrderByDescending(o => o.CicloLetivo.Ano).OrderByDescending(o => o.CicloLetivo.Numero).FirstOrDefault(f => !f.DataExclusao.HasValue).PlanosEstudo.FirstOrDefault(f => f.Atual).Itens.Select(sp => new PlanoEstudoItemVO()
                    {
                        SeqDivisaoTurma = sp.SeqDivisaoTurma,
                        SeqConfiguracaoComponente = sp.SeqConfiguracaoComponente
                    }).ToList(),
                    SeqInstituicaoEnsino = a.Pessoa.SeqInstituicaoEnsino,
                    DataPrevistaConclusaoIngressante = a.Historicos.FirstOrDefault(f => f.Atual).Ingressante.DataPrevisaoConclusao,
                    DataAdmissao = a.Historicos.FirstOrDefault(f => f.Atual).DataAdmissao
                }).ToList();

            // Recupera as matrizes principais das disciplinas isoladas associadas
            var specDivisao = new SMCContainsSpecification<DivisaoTurma, long>(p => p.Seq, alunos.Where(w => string.IsNullOrEmpty(w.DescricaoCurso)).SelectMany(sm => sm.PlanoEstudoItens.Select(s => s.SeqDivisaoTurma.GetValueOrDefault())).ToArray());
            var divisoesTurma = DivisaoTurmaDomainService.SearchProjectionBySpecification(specDivisao, p => new
            {
                p.Seq,
                DescricaoEntidadeResponsavel = p.Turma.ConfiguracoesComponente.FirstOrDefault(f => f.Principal).RestricoesTurmaMatriz.FirstOrDefault(f => f.OfertaMatrizPrincipal).MatrizCurricularOferta.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.CursoOferta.Curso.HierarquiasEntidades.FirstOrDefault().ItemSuperior.Entidade.Nome
            }).ToList();

            foreach (var aluno in alunos)
            {
                // Recupera os códigos de pessoa nos dados mestres
                aluno.Codigo = $"01{PessoaDomainService.BuscarCodigoDePessoaNosDadosMestres(aluno.SeqPessoa, TipoPessoa.Fisica, null).ToString("d8")}{aluno.NumeroVia.ToString("d2")}";
                aluno.RegistroDV = $"{aluno.CodigoAlunoMigracao}-{CalcularCodigoMigracaoDV(aluno.CodigoAlunoMigracao.GetValueOrDefault())}";

                // Caso seja um aluno de disciplina isolada, calcula a data de validade como sendo a maior data de termino das disciplinas associadas
                if (string.IsNullOrEmpty(aluno.DescricaoCurso))
                {
                    aluno.DescricaoTipoEntidadeResponsavel = MessagesResource.Label_Programa;
                    var datasTermino = new List<DateTime>();
                    foreach (var planoItem in aluno.PlanoEstudoItens)
                    {
                        var divisaoTurma = divisoesTurma.FirstOrDefault(f => f.Seq == planoItem.SeqDivisaoTurma);
                        aluno.DescricaoEntidadeResponsavel = divisaoTurma.DescricaoEntidadeResponsavel;
                    }
                    aluno.DataValidade = aluno.DataPrevistaConclusaoIngressante;
                }
                else
                {
                    aluno.DescricaoTipoEntidadeResponsavel = MessagesResource.Label_Curso;
                    aluno.SeqPrograma = null;
                    aluno.DescricaoEntidadeResponsavel = aluno.DescricaoCurso;
                }
            }

            var identidadesVencidas = alunos
                .Where(w => (w.DataValidade.GetValueOrDefault()) < DateTime.Now)
                .OrderBy(o => o.Nome)
                .Select(s => $"\r\n{s.NumeroRegistroAcademico} - {s.Nome}")
                .ToList();

            if (identidadesVencidas.SMCAny())
            {
                throw new IdentidadeAcademicaVencidaException(identidadesVencidas);
            }

            return alunos;
        }

        /// <summary>
        /// Busca os alunos para emissão da identidade estudantil pelos seqs informados
        /// </summary>
        /// <param name="filtro">Seqs dos alunos para pesquisa</param>
        /// <returns>Lista de alunos para emissão da identidade estudantil</returns>
        public List<RelatorioIdentidadeEstudantilVO> BuscarAlunosIdentidadeEstudantilPaginados(List<long> seqsAlunos)
        {
            return PessoaAtuacaoDomainService.PaginarIdentidadesEstudantis(BuscarAlunosIdentidadeEstudantil(seqsAlunos));
        }

        /// <summary>
        /// Busca os dados relativos a matricula dos alunos passados no parametro. Obs.: utilizado para gerar
        /// relatório de declaração de matrícula
        /// </summary>
        /// <param name="seqsPessoaAtuacao"></param>
        /// <returns></returns>
        public List<ItemDeclaracaoMatriculaVO> BuscarItemsDeclaracaoMatricula(long[] seqsPessoaAtuacao, long seqCicloLetivo)
        {
            // Consulta os componentes dos alunos selecionados
            var componentes = this.RawQuery<ItemDeclaracaoMatriculaVO>(QUERY_DECLARACAO_MATRICULA_ALUNO,
                                                                       new SMCFuncParameter("SEQS_ALUNO", string.Join(",", seqsPessoaAtuacao)),
                                                                       new SMCFuncParameter("SEQ_CICLO_LETIVO", seqCicloLetivo));

            // Recupera os tipos de orientação obrigatórios para curso regular ou intercâmbio
            var specConfigVinculo = new InstituicaoNivelTipoVinculoAlunoFilterSpecification()
            {
                TiposVinculoAlunoFinanceiro = new[] { TipoVinculoAlunoFinanceiro.CursoRegular, TipoVinculoAlunoFinanceiro.Intercambio }
            };
            var configVinculos = InstituicaoNivelTipoVinculoAlunoDomainService.SearchProjectionBySpecification(specConfigVinculo, p => new
            {
                p.SeqTipoVinculoAluno,
                p.InstituicaoNivel.SeqNivelEnsino,
                p.TipoVinculoAluno.TipoVinculoAlunoFinanceiro,
                SeqTipoTermoIntercambio = (long?)p.TiposTermoIntercambio.FirstOrDefault().SeqTipoTermoIntercambio,
                OrientacoesExigidas = p.TiposOrientacao
                    .Where(w => w.CadastroOrientacaoAluno == CadastroOrientacao.Exige)
                    .Select(s => new
                    {
                        SeqTipoTermoIntercambio = (long?)s.InstituicaoNivelTipoTermoIntercambio.SeqTipoTermoIntercambio,
                        s.SeqTipoOrientacao
                    })
            }).ToList();

            // Recupera as configurações dos tipos de componentes para formatar as cargas horárias
            var specConfigTipoComponente = new InstituicaoNivelTipoComponenteCurricularFilterSpecification()
            {
                SeqsTiposComponentesCurriculares = componentes.Select(s => s.SeqTipoComponenteCurricular).Distinct().ToList()
            };
            var configTiposComponente = InstituicaoNivelTipoComponenteCurricularDomainService.SearchProjectionBySpecification(specConfigTipoComponente, p => new
            {
                p.InstituicaoNivel.SeqNivelEnsino,
                p.SeqTipoComponenteCurricular,
                p.FormatoCargaHoraria
            }).ToList();

            // Ajusta os componentes por aluno
            var componentesAluno = componentes.GroupBy(g => g.SeqPessoaAtuacao);
            foreach (var grupoAluno in componentesAluno)
            {
                // Consulta para obter o "Ciclo Letivo Atual", "Nível de Ensino" e "Tipo de Vínculo"
                var dadosOrigem = PessoaAtuacaoDomainService.RecuperaDadosOrigem(grupoAluno.Key.Value);
                var datasCicloParametro = ConfiguracaoEventoLetivoDomainService.BuscarDatasEventoLetivo(seqCicloLetivo, grupoAluno.Key.Value, TOKEN_TIPO_EVENTO.PERIODO_CICLO_LETIVO);
                var cicloLetivoAtual = datasCicloParametro.DataInicio <= DateTime.Today && datasCicloParametro.DataFim >= DateTime.Today;

                string orientadores = null;
                // Recupera as orientações do aluno
                var configVinculo = configVinculos.FirstOrDefault(f => f.SeqTipoVinculoAluno == dadosOrigem.SeqTipoVinculoAluno && f.SeqNivelEnsino == dadosOrigem.SeqNivelEnsino);
                if (configVinculo != null)
                {
                    var orientacoesAluno = SearchProjectionByKey(new SMCSeqSpecification<Aluno>(grupoAluno.Key.Value), p =>
                        p.OrientacoesPessoaAtuacao.Select(s => new
                        {
                            s.Orientacao.SeqTipoTermoIntercambio,
                            s.Orientacao.SeqTipoOrientacao,
                            Colaboradores = s.Orientacao.OrientacoesColaborador.Select(sc => new
                            {
                                sc.Colaborador.DadosPessoais.Nome,
                                sc.DataInicioOrientacao,
                                sc.DataFimOrientacao
                            })
                        })
                    );

                    // Recupera a primeira orientação obrigatória para o aluno
                    long? seqTipoIntercambio = configVinculo.TipoVinculoAlunoFinanceiro == TipoVinculoAlunoFinanceiro.Intercambio ? configVinculo.SeqTipoTermoIntercambio : null;
                    long seqTipoOrientacao = configVinculo.OrientacoesExigidas.FirstOrDefault(f => f.SeqTipoTermoIntercambio == seqTipoIntercambio)?.SeqTipoOrientacao ?? 0;
                    var orientacao = orientacoesAluno.FirstOrDefault(f => f.SeqTipoOrientacao == seqTipoOrientacao
                                                                       && f.SeqTipoTermoIntercambio == seqTipoIntercambio);
                    if (orientacao != null)
                    {
                        if (!cicloLetivoAtual)
                        {
                            // Caso o ciclo selecionado na interface seja diferente do ciclo atual*, o campo <<orientador>> deve ser substituído pelo nome dos colaboradores ativos,
                            // pelo menos por um período do ciclo, na orientação do tipo exigido para a instituição-nível-vínculo da pessoa-atuação,
                            // de acordo com o ciclo informado na interface. Listar em ordem alfabética, concatenados e separados com vírgula.

                            var datasEventoLetivo = ConfiguracaoEventoLetivoDomainService.BuscarDatasEventoLetivo(seqCicloLetivo, dadosOrigem.SeqCursoOfertaLocalidadeTurno, null, TOKEN_TIPO_EVENTO.PERIODO_CICLO_LETIVO);
                            var orientadoresAtivos = orientacao.Colaboradores.Where(w => w.DataInicioOrientacao.HasValue
                                                                                      && (w.DataInicioOrientacao <= datasEventoLetivo.DataFim &&
                                                                                            (!w.DataFimOrientacao.HasValue || w.DataFimOrientacao >= datasEventoLetivo.DataInicio)));

                            orientadores = string.Join(", ", orientadoresAtivos.OrderBy(o => o.Nome).Select(s => s.Nome));
                        }
                        else
                        {
                            // Caso o ciclo informado na interface seja o atual*, o campo <<orientador>> deve ser substituído pelo nome dos colaboradores ativos
                            // na orientação do tipo exigido para a instituição-nível-vínculo da pessoa-atuação, de acordo com o ciclo informado na interface.
                            // Listar em ordem alfabética, concatenados e separados com vírgula.

                            var orientadoresAtivos = orientacao.Colaboradores.Where(w => w.DataInicioOrientacao.HasValue
                                                                                      && (w.DataInicioOrientacao <= DateTime.Today &&
                                                                                            (!w.DataFimOrientacao.HasValue || w.DataFimOrientacao >= DateTime.Today)));

                            orientadores = string.Join(", ", orientadoresAtivos.OrderBy(o => o.Nome).Select(s => s.Nome));
                        }
                    }
                }

                var contemTurmas = grupoAluno.Any(a => a.Turma);
                var contemAtividades = grupoAluno.Any(a => !a.Turma);
                // Formata os componentes com organização diferente de turma
                foreach (var componente in grupoAluno)
                {
                    if (datasCicloParametro != null)
                    {
                        componente.DataFimCicloLetivo = datasCicloParametro.DataFim.ToString("dd/MM/yyyy");
                        componente.DataInicioCicloLetivo = datasCicloParametro.DataInicio.ToString("dd/MM/yyyy");
                    }

                    if (!componente.Turma)
                    {
                        var formatoCargaHoraria = configTiposComponente
                            .FirstOrDefault(f => f.SeqNivelEnsino == dadosOrigem.SeqNivelEnsino && f.SeqTipoComponenteCurricular == componente.SeqTipoComponenteCurricular)
                            ?.FormatoCargaHoraria?.SMCGetDescription();
                        var partes = new List<string>()
                        {
                            componente.DescricaoComponenteCurricular
                        };
                        if (componente.QuantidadeCargaHoraria.HasValue)
                        {
                            partes.Add($"{componente.QuantidadeCargaHoraria} {formatoCargaHoraria}");
                        }
                        if (componente.QuantidadeCreditos.HasValue)
                        {
                            partes.Add($"{componente.QuantidadeCreditos} {(componente.QuantidadeCreditos == 1 ? MessagesResource.Label_Credito : MessagesResource.Label_Creditos)}");
                        }
                        componente.DescricaoComponenteCurricular = string.Join(" - ", partes);
                    }
                    // Replica os dados utilizados no dataset principal para todos os registros
                    componente.CicloLetivoAtual = cicloLetivoAtual;
                    componente.Orientadores = orientadores;
                    componente.ContemTurmas = contemTurmas;
                    componente.ContemAtividades = contemAtividades;
                    componente.CicloLetivoFuturo = datasCicloParametro.DataInicio > DateTime.Now;
                }
            }

            return componentes;
        }

        /// <summary>
        /// Buscar a matriz curricular do ultimo plano de estudos do aluno, ou seja,
        /// a matriz considerada a atual para o aluno.
        /// </summary>
        /// <param name="seqAluno">Sequencial do aluno</param>
        /// <param name="desabilitarFiltro">Desabilita o filtro de HIERARQUIA_ENTIDADE_ORGANIZACIONAL</param>
        /// <returns>Sequencial da matriz curricular do ultimo plano do aluno, ou NULL</returns>
        public (long? SeqMatrizCurricularOferta, long? SeqMatrizCurricular, long? SeqCurriculoCursoOferta) BuscarDadosMatrizCurricularAlunoUltimoPlano(long seqAluno, bool desabilitarFiltro = false)
        {
            if (desabilitarFiltro)
            {
                this.DisableFilter(FILTER.HIERARQUIA_ENTIDADE_ORGANIZACIONAL);
            }

            var specAluno = new SMCSeqSpecification<Aluno>(seqAluno);

            var dadosMatriz = SearchProjectionByKey(specAluno,
                p => p.Historicos.Where(w => w.Atual).FirstOrDefault()
                      .HistoricosCicloLetivo.Where(w => w.PlanosEstudo.Any(pl => pl.Atual)).OrderByDescending(o => o.DataInclusao).FirstOrDefault(h => !h.DataExclusao.HasValue)
                      .PlanosEstudo.OrderByDescending(o => o.DataInclusao).Select(h => new
                      {
                          Seq = h.Seq,
                          Atual = h.Atual,
                          SeqMatrizCurricularOferta = h.SeqMatrizCurricularOferta,
                          SeqMatrizCurricular = (long?)h.MatrizCurricularOferta.SeqMatrizCurricular,
                          SeqCurriculoCursoOferta = (long?)h.MatrizCurricularOferta.MatrizCurricular.SeqCurriculoCursoOferta,
                      }).FirstOrDefault(pl => pl.Atual));

            if (desabilitarFiltro)
            {
                this.EnableFilter(FILTER.HIERARQUIA_ENTIDADE_ORGANIZACIONAL);
            }

            if (dadosMatriz != null)
                return (dadosMatriz.SeqMatrizCurricularOferta, dadosMatriz.SeqMatrizCurricular, dadosMatriz.SeqCurriculoCursoOferta);
            else
                return (null, null, null);
        }

        #region [ Relatório de Histórico Escolar ]

        /// <summary>
        /// Emisão de relatório de histórico escolar.
        /// </summary>
        /// <param name="seqsAlunos">Lista de sequenciais dos alunos (PessoaAtuacao).</param>
        /// <returns>Data com dados dos alunos informados para montar os relatórios de cada aluno.</returns>
        public RelatorioHistoricoEscolarVO RelatorioHistoricoEscolar(List<long> seqsAlunos, bool? compCurriculaSemCreditos, bool? exibirMediaNotas)
        {
            RelatorioHistoricoEscolarVO dadosRelatorio = new RelatorioHistoricoEscolarVO();

            if (seqsAlunos != null && seqsAlunos.Count > 0)
            {
                // Buscando os alunos.
                var specAluno = new SMCContainsSpecification<Aluno, long>(a => a.Seq, seqsAlunos.ToArray());
                dadosRelatorio.Alunos = SearchProjectionBySpecification(specAluno,
                                    a => new ItemRelatorioHistoricoEscolarVO()
                                    {
                                        Seq = a.Seq,
                                        // Unidade e curso.
                                        Unidade = a.Historicos.Where(b => b.Atual).FirstOrDefault().EntidadeVinculo.Nome,
                                        Curso = a.Historicos.Where(b => b.Atual).FirstOrDefault().CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.CursoOferta.Curso.Nome,
                                        // Dados principais
                                        Nome = a.DadosPessoais.Nome,
                                        Pai = (a.Pessoa.Filiacao.Where(b => b.TipoParentesco == TipoParentesco.Pai).ToList().Count > 0) ? a.Pessoa.Filiacao.Where(b => b.TipoParentesco == TipoParentesco.Pai).FirstOrDefault().Nome : string.Empty,
                                        Mae = (a.Pessoa.Filiacao.Where(b => b.TipoParentesco == TipoParentesco.Mae).ToList().Count > 0) ? a.Pessoa.Filiacao.Where(b => b.TipoParentesco == TipoParentesco.Mae).FirstOrDefault().Nome : string.Empty,
                                        DataNascimento = a.Pessoa.DataNascimento,
                                        CodigoPaisNacionalidade = a.Pessoa.CodigoPaisNacionalidade,
                                        CodigoCidadeNaturalidade = a.DadosPessoais.CodigoCidadeNaturalidade,
                                        UfNaturalidade = a.DadosPessoais.UfNaturalidade,
                                        Nacionalidade = a.Pessoa.TipoNacionalidade,
                                        RG = a.DadosPessoais.NumeroIdentidade,
                                        Exp_RG = a.DadosPessoais.OrgaoEmissorIdentidade,
                                        UF_RG = a.DadosPessoais.UfIdentidade,
                                        CPF = a.Pessoa.Cpf,
                                        Passaporte = a.Pessoa.NumeroPassaporte,
                                        // Admissão
                                        FormaAdmissao = a.Historicos.Where(b => b.Atual).FirstOrDefault().FormaIngresso.Descricao,
                                        Nivel = a.Historicos.Where(b => b.Atual).FirstOrDefault().NivelEnsino.Descricao,
                                        DataAdmissao = a.Historicos.Where(b => b.Atual).FirstOrDefault().DataAdmissao,
                                        SeqAlunoHistoricoAtual = a.Historicos.Where(b => b.Atual).FirstOrDefault().Seq,
                                        SeqInstituicaoEnsino = a.Pessoa.SeqInstituicaoEnsino,
                                    }).ToList();

                // Buscando a naturalidade e nacionalidade de cada aluno.
                Naturalidade(dadosRelatorio.Alunos);

                // Buscando os demais dados para o relatório.
                foreach (var aluno in dadosRelatorio.Alunos)
                {
                    // Busca a situação do aluno no ciclo letivo atual
                    var situacaoAtual = AlunoHistoricoSituacaoDomainService.BuscarSituacaoAtualAluno(aluno.Seq);

                    // Esconder o campo de assinatura do relatorio caso a situacao atual seja diferente de FORMADO
                    aluno.EsconderAssinatura = situacaoAtual.TokenSituacaoMatricula != TOKENS_SITUACAO_MATRICULA.FORMADO;

                    // Trabalhos acadêmicos (bancas)
                    aluno.TrabalhosAcademicos = TrabalhoAcademicoDomainService.BuscarTrabalhosAcademicosAluno(aluno.Seq);
                    aluno.EsconderTrabalhosAcademicos = (aluno.TrabalhosAcademicos == null || aluno.TrabalhosAcademicos.Count() == 0);


                    if (aluno.TrabalhosAcademicos != null && aluno.TrabalhosAcademicos.Any())
                    {
                        foreach (var trabalho in aluno.TrabalhosAcademicos)
                        {
                            foreach (var avaliacao in trabalho.Avaliacoes)
                            {
                                var resultadoConvertido = Convert.ToDecimal(avaliacao.Resultado.Replace(".", ","));
                                var resultado = HistoricoEscolarDomainService.ArredondarNota(resultadoConvertido);
                                avaliacao.Resultado = $"{resultado}";
                            }
                        }
                    }

                    // Dados de admissão
                    DadosAdmissao(aluno);

                    // Informações de intercâmbio
                    DadosTipoMobilidade(aluno);

                    // Componentes
                    ComponentesConcluidosAproveitamentoCreditos(aluno, false, compCurriculaSemCreditos, false, true, true);

                    // Observações
                    DadosObservacoes(aluno, exibirMediaNotas);
                }
            }
            else
            {
                throw new Exception("Favor selecionar ao menos um aluno para gerar o relatório");
            }

            return dadosRelatorio;
        }

        private void BuscarDisciplinasCursadasAluno(ItemRelatorioDisciplinasCursadasVO aluno, TipoDeclaracaoDisciplinaCursada? exibirNaDeclaracao, bool? exibirEmentasComponentesCurriculares)
        {
            string chamadaProcedure = $"exec ACADEMICO.APR.st_rel_declaracao_disciplinas_cursadas {aluno.SeqHistoricoEscolar}, {exibirNaDeclaracao == TipoDeclaracaoDisciplinaCursada.SomenteDisciplina}, {exibirEmentasComponentesCurriculares}";
            List<DisciplinaCursadaVO> lista = RawQuery<DisciplinaCursadaVO>(chamadaProcedure);

            aluno.ComponentesCurriculares = new List<ItemRelatorioDisciplinasCursadasComponenteCurricularVO>();
            aluno.Ementas = new List<ItemRelatorioDisciplinasCursadasEmentaVO>();

            if (lista != null && lista.Count > 0)
            {
                lista.ForEach(l =>
                {
                    var componenteCurricular = l.Transform<ItemRelatorioDisciplinasCursadasComponenteCurricularVO>();
                    componenteCurricular.SeqAluno = aluno.SeqAluno;
                    componenteCurricular.Nota = string.IsNullOrEmpty(l.Nota) ? (decimal?)null : Convert.ToDecimal(l.Nota.Replace(".", ","));
                    componenteCurricular.PercentualFrequencia = string.IsNullOrEmpty(l.PercentualFrequencia) ? (decimal?)null : Convert.ToDecimal(l.PercentualFrequencia.Replace(".", ","));
                    componenteCurricular.DescricaoComponenteCurricularAssunto = componenteCurricular?.DescricaoComponenteCurricularAssunto?.Trim();
                    componenteCurricular.DescricaoComponenteCurricular = componenteCurricular?.DescricaoComponenteCurricular?.Trim();

                    // Verifica o arredondamento do % de frequencia
                    TipoArredondamento tipo = l.TipoArredondamento.HasValue ? (TipoArredondamento)l.TipoArredondamento : TipoArredondamento.Nenhum;
                    componenteCurricular.PercentualFrequencia = HistoricoEscolarDomainService.ArredondarPercentualFrequencia(componenteCurricular.PercentualFrequencia, tipo);

                    // Verificar o arredondamento da nota
                    componenteCurricular.Nota = HistoricoEscolarDomainService.ArredondarNota(componenteCurricular.Nota);

                    aluno.ComponentesCurriculares.Add(componenteCurricular);

                    var ementa = l.Transform<ItemRelatorioDisciplinasCursadasEmentaVO>();

                    //So adicionar a ementa quando existir
                    if (!string.IsNullOrEmpty(ementa.Ementa))
                    {
                        ementa.SeqAluno = aluno.SeqAluno;
                        aluno.Ementas.Add(ementa);
                    }

                    aluno.ExibirEmentasComponentesCurriculares = exibirEmentasComponentesCurriculares.GetValueOrDefault() && aluno.Ementas.Count > 0;
                });
            }
        }

        /// <summary>
        /// Buscar as mensagens com a token HISTORICO_ESCOLAR_OBSERVACAO
        /// </summary>
        /// <param name="aluno"></param>
        private void DadosObservacoes(ItemRelatorioHistoricoEscolarVO aluno, bool? exibirMediaNotas)
        {
            // Para o aluno em questão, buscar mensagens cujo token seja HISTORICO_ESCOLAR_OBS_ADMISSAO
            var mensagens = MensagemPessoaAtuacaoDomainService.ListarMensagens(new MensagemFiltroVO()
            {
                SeqPessoaAtuacao = aluno.Seq,
                TokenTipoMensagem = TOKEN_TIPO_MENSAGEM.HISTORICO_ESCOLAR_OBSERVACAO,
                MensagensValidas = true
            });
            if (mensagens != null && mensagens.Count() > 0)
            {
                aluno.Observacoes = mensagens.Select(a => new SMCDatasourceItem { Seq = aluno.Seq, Descricao = a.DescricaoMensagem }).ToList();
            }

            if (exibirMediaNotas.HasValue && exibirMediaNotas.Value)
            {
                decimal somatorioNotas = (decimal)0.00;
                int quantidadeComponentes = 0;
                decimal nota = (decimal)0.00;

                //foreach (var aproveitamentoCredito in aluno.AproveitamentoCreditos)
                //{
                //    if (string.IsNullOrEmpty(aproveitamentoCredito.Nota))
                //        continue;

                //    if (double.TryParse(aproveitamentoCredito.Nota, out nota))
                //    {
                //        somatorioNotas += nota;
                //        quantidadeComponentes++;
                //    }
                //}

                foreach (var componenteConcluido in aluno.ComponentesConcluidos)
                {
                    if (string.IsNullOrEmpty(componenteConcluido.Nota))
                        continue;

                    if (decimal.TryParse(componenteConcluido.Nota, out nota))
                    {
                        somatorioNotas += nota;
                        quantidadeComponentes++;
                    }
                }

                foreach (var componenteSemApuracao in aluno.ComponentesSemApuracao)
                {
                    if (string.IsNullOrEmpty(componenteSemApuracao.Nota))
                        continue;

                    if (decimal.TryParse(componenteSemApuracao.Nota, out nota))
                    {
                        somatorioNotas += nota;
                        quantidadeComponentes++;
                    }
                }

                //Retirado pois o componente de exame esta na lista de ComponentesConcluidos (Linha 1950) essa inclusão esta no método ComponentesConcluidosAproveitamentoCreditos (Linha 2172)
                //foreach (var componenteExame in aluno.ComponentesExame)
                //{
                //    if (string.IsNullOrEmpty(componenteExame.Nota))
                //        continue;

                //    if (decimal.TryParse(componenteExame.Nota, out nota))
                //    {
                //        somatorioNotas += nota;
                //        quantidadeComponentes++;
                //    }
                //}

                if (quantidadeComponentes > 0)
                {
                    decimal media = somatorioNotas / quantidadeComponentes;

                    aluno.Observacoes.Insert(0, new SMCDatasourceItem() { Seq = aluno.Seq, Descricao = $"Média de notas: {string.Format("{0:0.00}", media).Replace(".", ",")}" });
                }
            }
        }

        /// <summary>
        /// Buscar todos registros de termo de intercâmbio do aluno com o período de intercâmbio.
        /// Exibir a descrição do tipo do termo no lugar do label.
        /// Repetir todo o agrupamento de dados para cada mobilidade cadastrada para o aluno em questão.
        /// </summary>
        /// <param name="aluno"></param>
        private void DadosTipoMobilidade(ItemRelatorioHistoricoEscolarVO aluno)
        {
            var lista = TermoIntercambioDomainService.BuscarTipoMobilidadeHistoricoEscolar(aluno.Seq);
            if (lista != null && lista.Count() > 0)
            {
                aluno.TiposMobilidade = lista;
                aluno.EsconderTiposMobilidade = false;
            }
            else
            {
                aluno.EsconderTiposMobilidade = true;
            }
        }

        private void DadosAdmissao(ItemRelatorioHistoricoEscolarVO aluno)
        {
            // Histório atual do aluno.
            var ah = AlunoHistoricoDomainService.SearchProjectionByKey(aluno.SeqAlunoHistoricoAtual, a => new
            {
                HistoricosCicloLetivo = a.HistoricosCicloLetivo.Select(h => new
                {
                    AnoCicloLetivo = a.CicloLetivo.Ano,
                    NumeroCicloLetivo = a.CicloLetivo.Numero,
                    Excluido = a.DataExclusao.HasValue,
                    AlunoHistoricoSituacao = h.AlunoHistoricoSituacao.Select(ahs => new
                    {
                        DataInicioSituacao = ahs.DataInicioSituacao,
                        Excluido = ahs.DataExclusao.HasValue,
                        SituacaoMatricula = ahs.SituacaoMatricula != null ? new
                        {
                            TipoSituacaoMatricula = new
                            {
                                Token = ahs.SituacaoMatricula.TipoSituacaoMatricula.Token,
                            }
                        } : null
                    }),
                }),
                Formacoes = a.Formacoes.Select(f => new
                {
                    DataInicio = f.DataInicio,
                    DataFim = f.DataFim,
                    FormacaoEspecifica = f.FormacaoEspecifica != null ? new
                    {
                        FormacaoEspecificaSuperior = f.FormacaoEspecifica.FormacaoEspecificaSuperior != null ? new
                        {
                            DescricaoTipoFormacaoEspecifica = f.FormacaoEspecifica.FormacaoEspecificaSuperior.TipoFormacaoEspecifica.Descricao,
                            Descricao = f.FormacaoEspecifica.FormacaoEspecificaSuperior.Descricao
                        } : null,
                        DescricaoTipoFormacaoEspecifica = f.FormacaoEspecifica.TipoFormacaoEspecifica.Descricao,
                        Descricao = f.FormacaoEspecifica.Descricao
                    } : null
                })
            });

            // Exibir a data da situação atual do aluno-histórico-situação caso a tipo da situação seja cancelado ou transferido
            string tokenTipoSituacao = ah.HistoricosCicloLetivo.OrderByDescending(o => o.AnoCicloLetivo)
                .ThenByDescending(o => o.NumeroCicloLetivo).FirstOrDefault(c => !c.Excluido)
                                                          .AlunoHistoricoSituacao.OrderByDescending(o => o.DataInicioSituacao)
                                                          .FirstOrDefault(s => s.DataInicioSituacao <= DateTime.Today && !s.Excluido)
                                                          .SituacaoMatricula.TipoSituacaoMatricula.Token;

            if (ah != null && !string.IsNullOrEmpty(tokenTipoSituacao) && (tokenTipoSituacao.Equals(TOKENS_TIPO_SITUACAO_MATRICULA.CANCELADO) || tokenTipoSituacao.Equals(TOKENS_TIPO_SITUACAO_MATRICULA.TRANSFERIDO)))
            {
                aluno.DataCancelamento = ah.HistoricosCicloLetivo.OrderByDescending(o => o.AnoCicloLetivo)
                                            .ThenByDescending(o => o.NumeroCicloLetivo)
                                            .FirstOrDefault(c => !c.Excluido)
                                           .AlunoHistoricoSituacao.OrderByDescending(o => o.DataInicioSituacao)
                                           .FirstOrDefault(s => s.DataInicioSituacao <= DateTime.Today && !s.Excluido)
                                           .DataInicioSituacao;
            }

            // Exibir todas as formações específicas do aluno-historico atual com situações diferentes de "Cancelado" e "Não optado"
            // e as respectivas formações superiores.
            List<SMCDatasourceItem> formacoes = new List<SMCDatasourceItem>();
            foreach (var formacao in ah.Formacoes.Where(x => DateTime.Now > x.DataInicio && (!x.DataFim.HasValue || DateTime.Now < x.DataFim.Value)))
            {
                if (formacao.FormacaoEspecifica.FormacaoEspecificaSuperior != null)
                {
                    formacoes.Add(new SMCDatasourceItem { Seq = aluno.Seq, Descricao = RemoveEspacosEnter(formacao.FormacaoEspecifica.FormacaoEspecificaSuperior.DescricaoTipoFormacaoEspecifica + ": " + formacao.FormacaoEspecifica.FormacaoEspecificaSuperior.Descricao) });
                }
                formacoes.Add(new SMCDatasourceItem { Seq = aluno.Seq, Descricao = RemoveEspacosEnter(formacao.FormacaoEspecifica.DescricaoTipoFormacaoEspecifica + ": " + formacao.FormacaoEspecifica.Descricao) });
            }
            aluno.FormacaoEspecifica = formacoes;

            // Para o aluno em questão, buscar mensagens cujo token seja HISTORICO_ESCOLAR_OBS_ADMISSAO
            var mensagens = MensagemPessoaAtuacaoDomainService.ListarMensagens(new MensagemFiltroVO()
            {
                SeqPessoaAtuacao = aluno.Seq,
                TokenTipoMensagem = TOKEN_TIPO_MENSAGEM.HISTORICO_ESCOLAR_OBS_ADMISSAO,
                MensagensValidas = true
            });
            if (mensagens != null && mensagens.Count() > 0)
            {
                aluno.AdmissaoObservacao = mensagens.Select(a => new SMCDatasourceItem { Seq = aluno.Seq, Descricao = a.DescricaoMensagem }).ToList();
            }
            else
            {
                aluno.EsconderAdmissaoObservacao = true;
            }
        }

        //Obtendo a nacionalidade e naturalidade de cada aluno.
        private void Naturalidade(List<ItemRelatorioHistoricoEscolarVO> lista)
        {
            var listaNaturalidades = lista.Select(b => new Tuple<int?, string>(b.CodigoCidadeNaturalidade, b.UfNaturalidade))
                                          .ToList()
                                          .Distinct();
            Dictionary<Tuple<int?, string>, string> dicNaturalidade = new Dictionary<Tuple<int?, string>, string>();
            foreach (var naturalidade in listaNaturalidades)
            {
                if (naturalidade.Item1.GetValueOrDefault() > 0 && !string.IsNullOrEmpty(naturalidade.Item2))
                {
                    var cidade = LocalidadeService.BuscarCidade(naturalidade.Item1.Value, naturalidade.Item2);
                    dicNaturalidade.Add(naturalidade, cidade != null ? cidade.Nome.Trim() : string.Empty);
                }
            }

            foreach (var aluno in lista)
            {
                if (dicNaturalidade.ContainsKey(new Tuple<int?, string>(aluno.CodigoCidadeNaturalidade, aluno.UfNaturalidade)))
                {
                    var naturalidade = dicNaturalidade[new Tuple<int?, string>(aluno.CodigoCidadeNaturalidade, aluno.UfNaturalidade)];

                    if (string.IsNullOrEmpty(naturalidade))
                    {
                        aluno.Naturalidade = string.Empty;
                    }
                    else
                    {
                        aluno.Naturalidade = dicNaturalidade[new Tuple<int?, string>(aluno.CodigoCidadeNaturalidade, aluno.UfNaturalidade)] + ", " + aluno.UfNaturalidade;
                    }
                }
                else
                {
                    aluno.Naturalidade = string.Empty;
                }
            }
        }

        /// <summary>
        /// Componentes concluídos:
        /// Listar os componentes curriculares com a situação "Aprovado" do histórico escolar e os componentes e
        /// grupos curriculares(itens a serem dispensados) cujas situações estejam "Aprovadas" nas solicitações de dispensa
        /// com situação "Concluída".
        /// Não exibir os componentes cujo grupo curricular, do currículo do aluno em questão, se houver,
        /// foi parametrizado para não ser exibido no documento de histórico escolar.
        ///
        /// Aproveitamento de créditos:
        /// Buscar nas solicitações de dispensa com situação concluída, itens cuja situação seja "Aprovada".
        /// </summary>
        /// <param name="aluno">Objeto aluno com o SeqAlunoHistoricoAtual</param>
        /// <param name="exibirReprovados">Exibe os componentes reprovados para histórico interno</param>
        /// <param name="compCurriculaSemCreditos">Exibe os componentes curriculares sem créditos</param>
        /// <param name="compCursadosSemHistorico">Exibe os componentes curriculares sem lançamento no histórico</param>
        /// <param name="compExame">Exibe os componentes curriculares de exame</param>
        private void ComponentesConcluidosAproveitamentoCreditos(ItemRelatorioHistoricoEscolarVO aluno, bool? exibirReprovados, bool? compCurriculaSemCreditos, bool? compCursadosSemHistorico, bool? compExame, bool exibirExameJuntoComComponentesConcluido = false)
        {
            var lista = HistoricoEscolarDomainService.ComponentesCriteriosHistoricoEscolar(aluno.SeqAlunoHistoricoAtual, exibirReprovados.GetValueOrDefault(), compCurriculaSemCreditos.GetValueOrDefault(), compCursadosSemHistorico.GetValueOrDefault(), compExame.GetValueOrDefault());

            if (lista != null && lista.Count > 0)
            {
                lista.SMCForEach(a => a.Seq = aluno.Seq);
                aluno.AproveitamentoCreditos = lista.Where(a => a.TipoConclusao == TipoConclusao.AproveitamentoCredito).ToList();
                aluno.ComponentesConcluidos = lista.Where(a => a.TipoConclusao == TipoConclusao.ComponenteConcluido).ToList();
                aluno.ComponentesSemApuracao = lista.Where(a => a.TipoConclusao == TipoConclusao.CursadoSemHistoricoEscolar).ToList();
                aluno.ComponentesExame = lista.Where(a => a.TipoConclusao == TipoConclusao.Exame).ToList();

                foreach (var item in lista)
                {
                    if (!string.IsNullOrEmpty(item.Nota) && item.Nota != "Dispensado")
                    {
                        var nota = HistoricoEscolarDomainService.ArredondarNota(Convert.ToDecimal(item.Nota));
                        item.Nota = $"{nota}";
                    }

                    if (!string.IsNullOrEmpty(item.PercentualFrequencia))
                    {
                        if (item.TipoArredondamento.HasValue && item.TipoArredondamento.Value == TipoArredondamento.ArredondarParaTeto)
                        {
                            var percentualFrequencia = HistoricoEscolarDomainService.ArredondarPercentualFrequencia(Convert.ToDecimal(item.PercentualFrequencia), item.TipoArredondamento.Value);
                            item.PercentualFrequencia = $"{percentualFrequencia}%";
                        }
                        else
                        {
                            item.PercentualFrequencia = $"{RemoverCasasDecimais(item.PercentualFrequencia)}%";
                        }
                    }

                }
            }

            if (exibirExameJuntoComComponentesConcluido)
            {
                aluno.ComponentesConcluidos.AddRange(aluno.ComponentesExame);
                aluno.ComponentesConcluidos = aluno.ComponentesConcluidos.OrderBy(o => o.Ano).ThenBy(t => t.Semestre).ThenBy(tb => tb.DescricaoComponente).ToList();
            }
        }

        public static string RemoverCasasDecimais(string valor)
        {
            if (string.IsNullOrWhiteSpace(valor))
                return valor;

            var separadores = new[] { ',', '.' };

            var index = valor.IndexOfAny(separadores);

            if (index >= 0)
                return valor.Substring(0, index);

            return valor;
        }

        #endregion [ Relatório de Histórico Escolar ]

        #region [ Relatório de Histórico Escolar Interno ]

        /// <summary>
        /// Emisão de relatório de histórico escolar interno.
        /// </summary>
        /// <param name="seqsAlunos">Lista de sequenciais dos alunos (PessoaAtuacao).</param>
        /// <returns>Data com dados dos alunos informados para montar os relatórios de cada aluno.</returns>
        public RelatorioHistoricoEscolarVO RelatorioHistoricoEscolarInterno(List<long> seqsAlunos)
        {
            RelatorioHistoricoEscolarVO dadosRelatorio = new RelatorioHistoricoEscolarVO();

            if (seqsAlunos != null && seqsAlunos.Count > 0)
            {
                //Buscando os alunos.
                var specAluno = new SMCContainsSpecification<Aluno, long>(a => a.Seq, seqsAlunos.ToArray());
                dadosRelatorio.Alunos = SearchProjectionBySpecification(specAluno,
                                    a => new ItemRelatorioHistoricoEscolarVO()
                                    {
                                        Seq = a.Seq,
                                        SeqAlunoHistoricoAtual = a.Historicos.Where(b => b.Atual).FirstOrDefault().Seq,
                                        SeqNivelEnsino = a.Historicos.Where(b => b.Atual).FirstOrDefault().SeqNivelEnsino,
                                        DescricaoPessoaAtuacao = a.Descricao,
                                        DescricaoTipoVinculoAluno = a.TipoVinculoAluno.Descricao,

                                        //Dados principais
                                        Nome = a.DadosPessoais.Nome,
                                        NumeroRegistroAcademico = a.NumeroRegistroAcademico,
                                    }).ToList();

                //Buscando os demais dados para o relatório.
                foreach (var aluno in dadosRelatorio.Alunos)
                {
                    aluno.ExibirCredito = InstituicaoNivelDomainService.BuscarInstituicaoNivelPorNivelEnsino(aluno.SeqNivelEnsino).PermiteCreditoComponenteCurricular;
                    ComponentesConcluidosAproveitamentoCreditos(aluno, true, true, true, true, true);

                    TotaisComponentesConcluidos(aluno);
                }

            }
            else
            {
                throw new Exception("Favor selecionar ao menos um aluno para gerar o relatório");
            }

            return dadosRelatorio;
        }


        /// <summary>
        /// Calculas os totalizadores de carga horária, créditos e notas
        /// </summary>
        /// <param name="aluno">objeto aluno com os componentes curriculares concluídos</param>
        private void TotaisComponentesConcluidos(ItemRelatorioHistoricoEscolarVO aluno)
        {
            var aprovados = new TotaisComponentesCreditosVO() { Seq = aluno.Seq, Situacao = SituacaoHistoricoEscolar.Aprovado.SMCGetDescription() };

            //Totalizador Aprovados         
            var alunoAprovadoConcluido = aluno.ComponentesConcluidos.Where(w => w.SituacaoHistoricoEscolar == SituacaoHistoricoEscolar.Aprovado).ToList();
            var alunoAprovadoExame = aluno.ComponentesExame.Where(w => w.SituacaoHistoricoEscolar == SituacaoHistoricoEscolar.Aprovado).ToList();

            var alunoAprovado = alunoAprovadoConcluido.Union(alunoAprovadoExame).ToList();

            var somaNotaAprovado = alunoAprovado.Where(w => w.Nota != null).Sum(s => Convert.ToDecimal(s.Nota));
            var qtdAlunoNota = alunoAprovado.Where(w => w.Nota != null).Count() == 0 ? 1 : alunoAprovado.Where(w => w.Nota != null).Count();

            if (alunoAprovado != null && alunoAprovado.Count > 0)
            {
                aprovados.QuantidadeComponente = alunoAprovado.Count();
                aprovados.TotalCargaHoraria = alunoAprovado.Sum(s => s.CargaHoraria).GetValueOrDefault();
                aprovados.TotalCredito = alunoAprovado.Sum(s => s.Credito).GetValueOrDefault();
                aprovados.MediaNota = somaNotaAprovado / qtdAlunoNota;
            }

            //Totalizador Aprovados com crédito maior que zero
            var alunoAprovadoConcluidoCreditoMaiorZero = aluno.ComponentesConcluidos.Where(w => w.SituacaoHistoricoEscolar == SituacaoHistoricoEscolar.Aprovado && w.Credito != null && w.Credito > 0).ToList();
            var alunoAprovadoExameCreditoMaiorZero = aluno.ComponentesExame.Where(w => w.SituacaoHistoricoEscolar == SituacaoHistoricoEscolar.Aprovado && w.Credito != null && w.Credito > 0).ToList();

            var alunoAprovadoCreditoMaiorZero = alunoAprovadoConcluidoCreditoMaiorZero.Union(alunoAprovadoExameCreditoMaiorZero).ToList();

            var somaNotaAprovadoCreditoMaiorZero = alunoAprovadoCreditoMaiorZero.Where(w => w.Nota != null && w.Credito != null && w.Credito > 0).Sum(s => Convert.ToDecimal(s.Nota));
            var qtdAlunoNotaCreditoMaiorZero = alunoAprovadoCreditoMaiorZero.Where(w => w.Nota != null && w.Credito != null && w.Credito > 0).Count() == 0 ? 1 : alunoAprovadoCreditoMaiorZero.Where(w => w.Nota != null && w.Credito != null && w.Credito > 0).Count();

            if (alunoAprovadoCreditoMaiorZero != null && alunoAprovadoCreditoMaiorZero.Count > 0)
            {
                aprovados.QuantidadeComponenteCreditoMaiorZero = alunoAprovadoCreditoMaiorZero.Count();
                aprovados.TotalCargaHorariaCreditoMaiorZero = alunoAprovadoCreditoMaiorZero.Sum(s => s.CargaHoraria).GetValueOrDefault();
                aprovados.TotalCreditoCreditoMaiorZero = alunoAprovadoCreditoMaiorZero.Sum(s => s.Credito).GetValueOrDefault();
                aprovados.MediaNotaCreditoMaiorZero = somaNotaAprovadoCreditoMaiorZero / qtdAlunoNotaCreditoMaiorZero;
            }

            aluno.TotaisComponentesConcluidos.Add(aprovados);


            var dispensados = new TotaisComponentesCreditosVO() { Seq = aluno.Seq, Situacao = SituacaoHistoricoEscolar.Dispensado.SMCGetDescription() };

            //Totalizador Dispensado
            var alunoDispensadosConcluido = aluno.ComponentesConcluidos.Where(w => w.SituacaoHistoricoEscolar == SituacaoHistoricoEscolar.Dispensado).ToList();
            var alunoDispensadosExame = aluno.ComponentesExame.Where(w => w.SituacaoHistoricoEscolar == SituacaoHistoricoEscolar.Dispensado).ToList();

            var alunoDispensados = alunoDispensadosConcluido.Union(alunoDispensadosExame).ToList();

            var somaNotaDispensado = aluno.AproveitamentoCreditos.Where(w => w.Nota != null).Sum(s => Convert.ToDecimal(s.Nota));
            var qtdAlunoDispensado = aluno.AproveitamentoCreditos.Where(w => w.Nota != null).Count() == 0 ? 1 : aluno.AproveitamentoCreditos.Where(w => w.Nota != null).Count();

            if (alunoDispensados != null && alunoDispensados.Count > 0)
            {
                dispensados.QuantidadeComponente = alunoDispensados.Count();
                dispensados.TotalCargaHoraria = alunoDispensados.Sum(s => s.CargaHoraria).GetValueOrDefault();
                dispensados.TotalCredito = alunoDispensados.Sum(s => s.Credito).GetValueOrDefault();
                dispensados.MediaNota = somaNotaDispensado / qtdAlunoDispensado;
            }

            //Totalizador Dispensado com crédito maior que zero
            var alunoDispensadosConcluidoCreditoMaiorZero = aluno.ComponentesConcluidos.Where(w => w.SituacaoHistoricoEscolar == SituacaoHistoricoEscolar.Dispensado && w.Credito != null && w.Credito > 0).ToList();
            var alunoDispensadosExameCreditoMaiorZero = aluno.ComponentesExame.Where(w => w.SituacaoHistoricoEscolar == SituacaoHistoricoEscolar.Dispensado && w.Credito != null && w.Credito > 0).ToList();

            var alunoDispensadosCreditoMaiorZero = alunoDispensadosConcluidoCreditoMaiorZero.Union(alunoDispensadosExameCreditoMaiorZero).ToList();

            var somaNotaDispensadoCreditoMaiorZero = aluno.AproveitamentoCreditos.Where(w => w.Nota != null && w.Credito != null && w.Credito > 0).Sum(s => Convert.ToDecimal(s.Nota));
            var qtdAlunoDispensadoCreditoMaiorZero = aluno.AproveitamentoCreditos.Where(w => w.Nota != null && w.Credito != null && w.Credito > 0).Count() == 0 ? 1 : aluno.AproveitamentoCreditos.Where(w => w.Nota != null && w.Credito != null && w.Credito > 0).Count();

            if (alunoDispensadosCreditoMaiorZero != null && alunoDispensadosCreditoMaiorZero.Count > 0)
            {
                dispensados.QuantidadeComponenteCreditoMaiorZero = alunoDispensadosCreditoMaiorZero.Count();
                dispensados.TotalCargaHorariaCreditoMaiorZero = alunoDispensadosCreditoMaiorZero.Sum(s => s.CargaHoraria).GetValueOrDefault();
                dispensados.TotalCreditoCreditoMaiorZero = alunoDispensadosCreditoMaiorZero.Sum(s => s.Credito).GetValueOrDefault();
                dispensados.MediaNotaCreditoMaiorZero = somaNotaDispensadoCreditoMaiorZero / qtdAlunoDispensadoCreditoMaiorZero;
            }

            aluno.TotaisComponentesConcluidos.Add(dispensados);


            var reprovados = new TotaisComponentesCreditosVO() { Seq = aluno.Seq, Situacao = SituacaoHistoricoEscolar.Reprovado.SMCGetDescription() };

            //Totalizador Reprovados
            var alunoReprovadosConcluido = aluno.ComponentesConcluidos.Where(w => w.SituacaoHistoricoEscolar == SituacaoHistoricoEscolar.Reprovado).ToList();
            var alunoReprovadosExame = aluno.ComponentesExame.Where(w => w.SituacaoHistoricoEscolar == SituacaoHistoricoEscolar.Reprovado).ToList();

            var alunoReprovados = alunoReprovadosConcluido.Union(alunoReprovadosExame).ToList();

            var somaNotaReprovado = alunoReprovados.Where(w => w.Nota != null).Sum(s => Convert.ToDecimal(s.Nota));
            var qtdAlunoReprovado = alunoReprovados.Where(w => w.Nota != null).Count() == 0 ? 1 : alunoReprovados.Where(w => w.Nota != null).Count();

            if (alunoReprovados != null && alunoReprovados.Count > 0)
            {
                reprovados.QuantidadeComponente = alunoReprovados.Count();
                reprovados.TotalCargaHoraria = alunoReprovados.Sum(s => s.CargaHoraria).GetValueOrDefault();
                reprovados.TotalCredito = alunoReprovados.Sum(s => s.Credito).GetValueOrDefault();
                reprovados.MediaNota = somaNotaReprovado / qtdAlunoReprovado;
            }

            //Totalizador Reprovados com crédito maior que zero
            var alunoReprovadosConcluidoCreditoMaiorZero = aluno.ComponentesConcluidos.Where(w => w.SituacaoHistoricoEscolar == SituacaoHistoricoEscolar.Reprovado && w.Credito != null && w.Credito != null && w.Credito > 0).ToList();
            var alunoReprovadosExameCreditoMaiorZero = aluno.ComponentesExame.Where(w => w.SituacaoHistoricoEscolar == SituacaoHistoricoEscolar.Reprovado && w.Credito != null && w.Credito > 0).ToList();

            var alunoReprovadosCreditoMaiorZero = alunoReprovadosConcluidoCreditoMaiorZero.Union(alunoReprovadosExameCreditoMaiorZero).ToList();

            var somaNotaReprovadoCreditoMaiorZero = alunoReprovadosCreditoMaiorZero.Where(w => w.Nota != null && w.Credito != null && w.Credito > 0).Sum(s => Convert.ToDecimal(s.Nota));
            var qtdAlunoReprovadoCreditoMaiorZero = alunoReprovadosCreditoMaiorZero.Where(w => w.Nota != null && w.Credito != null && w.Credito > 0).Count() == 0 ? 1 : alunoReprovadosCreditoMaiorZero.Where(w => w.Nota != null && w.Credito != null && w.Credito > 0).Count();

            if (alunoReprovadosCreditoMaiorZero != null && alunoReprovadosCreditoMaiorZero.Count > 0)
            {
                reprovados.QuantidadeComponenteCreditoMaiorZero = alunoReprovadosCreditoMaiorZero.Count();
                reprovados.TotalCargaHorariaCreditoMaiorZero = alunoReprovadosCreditoMaiorZero.Sum(s => s.CargaHoraria).GetValueOrDefault();
                reprovados.TotalCreditoCreditoMaiorZero = alunoReprovadosCreditoMaiorZero.Sum(s => s.Credito).GetValueOrDefault();
                reprovados.MediaNotaCreditoMaiorZero = somaNotaReprovadoCreditoMaiorZero / qtdAlunoReprovadoCreditoMaiorZero;
            }

            aluno.TotaisComponentesConcluidos.Add(reprovados);


            var semApuracao = new TotaisComponentesCreditosVO() { Seq = aluno.Seq, Situacao = "Sem Apuração" };

            //Totalizador Sem Apuração
            var alunoSemApuracao = aluno.ComponentesSemApuracao.ToList();

            if (alunoSemApuracao != null && alunoSemApuracao.Count > 0)
            {
                semApuracao.QuantidadeComponente = alunoSemApuracao.Count();
                semApuracao.TotalCargaHoraria = alunoSemApuracao.Sum(s => s.CargaHoraria).GetValueOrDefault();
                semApuracao.TotalCredito = alunoSemApuracao.Sum(s => s.Credito).GetValueOrDefault();
                semApuracao.MediaNota = (decimal)0.00;
            }

            //Totalizador Sem Apuração com crédito maior que zero
            var alunoSemApuracaoCreditoMaiorZero = aluno.ComponentesSemApuracao.Where(c => c.Credito != null && c.Credito > 0).ToList();

            if (alunoSemApuracaoCreditoMaiorZero != null && alunoSemApuracaoCreditoMaiorZero.Count > 0)
            {
                semApuracao.QuantidadeComponenteCreditoMaiorZero = alunoSemApuracaoCreditoMaiorZero.Count();
                semApuracao.TotalCargaHorariaCreditoMaiorZero = alunoSemApuracaoCreditoMaiorZero.Sum(s => s.CargaHoraria).GetValueOrDefault();
                semApuracao.TotalCreditoCreditoMaiorZero = alunoSemApuracaoCreditoMaiorZero.Sum(s => s.Credito).GetValueOrDefault();
                semApuracao.MediaNotaCreditoMaiorZero = (decimal)0.00;
            }

            aluno.TotaisComponentesConcluidos.Add(semApuracao);

            var aprovadosDispensado = new TotaisComponentesCreditosVO() { Seq = aluno.Seq, Situacao = $"{SituacaoHistoricoEscolar.Aprovado.SMCGetDescription()} + {SituacaoHistoricoEscolar.Dispensado.SMCGetDescription()}" };

            //Acertar quantidades com nota em todos os tipos
            qtdAlunoNota = alunoAprovado.Where(w => w.Nota != null).Count();
            qtdAlunoDispensado = aluno.AproveitamentoCreditos.Where(w => w.Nota != null).Count();
            qtdAlunoReprovado = alunoReprovados.Where(w => w.Nota != null).Count();

            //Totalizador Aprovados + Dispensados
            aprovadosDispensado.QuantidadeComponente = aprovados.QuantidadeComponente + dispensados.QuantidadeComponente;
            aprovadosDispensado.TotalCargaHoraria = aprovados.TotalCargaHoraria + dispensados.TotalCargaHoraria;
            aprovadosDispensado.TotalCredito = aprovados.TotalCredito + dispensados.TotalCredito;

            var qtdAprovadoDispensado = qtdAlunoNota + qtdAlunoDispensado;

            if (qtdAprovadoDispensado == 0)
                qtdAprovadoDispensado = 1;

            aprovadosDispensado.MediaNota = ((qtdAlunoNota * aprovados.MediaNota) + (qtdAlunoDispensado * dispensados.MediaNota)) / qtdAprovadoDispensado;

            //Acertar quantidades com nota em todos os tipos com crédito maior que zero
            qtdAlunoNotaCreditoMaiorZero = alunoAprovadoCreditoMaiorZero.Where(w => w.Nota != null && w.Credito != null && w.Credito > 0).Count();
            qtdAlunoDispensadoCreditoMaiorZero = aluno.AproveitamentoCreditos.Where(w => w.Nota != null && w.Credito != null && w.Credito > 0).Count();
            qtdAlunoReprovadoCreditoMaiorZero = alunoReprovadosCreditoMaiorZero.Where(w => w.Nota != null && w.Credito != null && w.Credito > 0).Count();

            //Totalizador Aprovados + Dispensados com crédito maior que zero
            aprovadosDispensado.QuantidadeComponenteCreditoMaiorZero = aprovados.QuantidadeComponenteCreditoMaiorZero + dispensados.QuantidadeComponenteCreditoMaiorZero;
            aprovadosDispensado.TotalCargaHorariaCreditoMaiorZero = aprovados.TotalCargaHorariaCreditoMaiorZero + dispensados.TotalCargaHorariaCreditoMaiorZero;
            aprovadosDispensado.TotalCreditoCreditoMaiorZero = aprovados.TotalCreditoCreditoMaiorZero + dispensados.TotalCreditoCreditoMaiorZero;

            var qtdAprovadoDispensadoCreditoMaiorZero = qtdAlunoNotaCreditoMaiorZero + qtdAlunoDispensadoCreditoMaiorZero;

            if (qtdAprovadoDispensadoCreditoMaiorZero == 0)
                qtdAprovadoDispensadoCreditoMaiorZero = 1;

            aprovadosDispensado.MediaNotaCreditoMaiorZero = ((qtdAlunoNotaCreditoMaiorZero * aprovados.MediaNotaCreditoMaiorZero) + (qtdAlunoDispensadoCreditoMaiorZero * dispensados.MediaNotaCreditoMaiorZero)) / qtdAprovadoDispensadoCreditoMaiorZero;

            aluno.TotaisComponentesConcluidos.Add(aprovadosDispensado);


            var aprovadosReprovados = new TotaisComponentesCreditosVO() { Seq = aluno.Seq, Situacao = $"{SituacaoHistoricoEscolar.Aprovado.SMCGetDescription()} + {SituacaoHistoricoEscolar.Reprovado.SMCGetDescription()}" };

            //Totalizador Aprovados + Reprovados
            aprovadosReprovados.QuantidadeComponente = aprovados.QuantidadeComponente + reprovados.QuantidadeComponente;
            aprovadosReprovados.TotalCargaHoraria = aprovados.TotalCargaHoraria + reprovados.TotalCargaHoraria;
            aprovadosReprovados.TotalCredito = aprovados.TotalCredito + reprovados.TotalCredito;

            var qtdAprovadoReprovado = qtdAlunoNota + qtdAlunoReprovado;

            if (qtdAprovadoReprovado == 0)
                qtdAprovadoReprovado = 1;

            aprovadosReprovados.MediaNota = ((qtdAlunoNota * aprovados.MediaNota) + (qtdAlunoReprovado * reprovados.MediaNota)) / qtdAprovadoReprovado;

            //Totalizador Aprovados + Reprovados com crédito maior que zero
            aprovadosReprovados.QuantidadeComponenteCreditoMaiorZero = aprovados.QuantidadeComponenteCreditoMaiorZero + reprovados.QuantidadeComponenteCreditoMaiorZero;
            aprovadosReprovados.TotalCargaHorariaCreditoMaiorZero = aprovados.TotalCargaHorariaCreditoMaiorZero + reprovados.TotalCargaHorariaCreditoMaiorZero;
            aprovadosReprovados.TotalCreditoCreditoMaiorZero = aprovados.TotalCreditoCreditoMaiorZero + reprovados.TotalCreditoCreditoMaiorZero;

            var qtdAprovadoReprovadoCreditoMaiorZero = qtdAlunoNotaCreditoMaiorZero + qtdAlunoReprovadoCreditoMaiorZero;

            if (qtdAprovadoReprovadoCreditoMaiorZero == 0)
                qtdAprovadoReprovadoCreditoMaiorZero = 1;

            aprovadosReprovados.MediaNotaCreditoMaiorZero = ((qtdAlunoNotaCreditoMaiorZero * aprovados.MediaNotaCreditoMaiorZero) + (qtdAlunoReprovadoCreditoMaiorZero * reprovados.MediaNotaCreditoMaiorZero)) / qtdAprovadoReprovadoCreditoMaiorZero;

            aluno.TotaisComponentesConcluidos.Add(aprovadosReprovados);


            var aprovadosDispensadoReprovado = new TotaisComponentesCreditosVO() { Seq = aluno.Seq, Situacao = $"{SituacaoHistoricoEscolar.Aprovado.SMCGetDescription()} + {SituacaoHistoricoEscolar.Dispensado.SMCGetDescription()} + {SituacaoHistoricoEscolar.Reprovado.SMCGetDescription()}" };

            //Totalizador Aprovados + Dispensados + Reprovados
            aprovadosDispensadoReprovado.QuantidadeComponente = aprovados.QuantidadeComponente + dispensados.QuantidadeComponente + reprovados.QuantidadeComponente;
            aprovadosDispensadoReprovado.TotalCargaHoraria = aprovados.TotalCargaHoraria + dispensados.TotalCargaHoraria + reprovados.TotalCargaHoraria;
            aprovadosDispensadoReprovado.TotalCredito = aprovados.TotalCredito + dispensados.TotalCredito + reprovados.TotalCredito;

            var qtdAprovadoDispensadoReprovado = qtdAlunoNota + qtdAlunoDispensado + qtdAlunoReprovado;

            if (qtdAprovadoDispensadoReprovado == 0)
                qtdAprovadoDispensadoReprovado = 1;

            aprovadosDispensadoReprovado.MediaNota = ((qtdAlunoNota * aprovados.MediaNota) + (qtdAlunoDispensado * dispensados.MediaNota) + (qtdAlunoReprovado * reprovados.MediaNota)) / qtdAprovadoDispensadoReprovado;

            //Totalizador Aprovados + Dispensados + Reprovados com crédito maiso que zero
            aprovadosDispensadoReprovado.QuantidadeComponenteCreditoMaiorZero = aprovados.QuantidadeComponenteCreditoMaiorZero + dispensados.QuantidadeComponenteCreditoMaiorZero + reprovados.QuantidadeComponenteCreditoMaiorZero;
            aprovadosDispensadoReprovado.TotalCargaHorariaCreditoMaiorZero = aprovados.TotalCargaHorariaCreditoMaiorZero + dispensados.TotalCargaHorariaCreditoMaiorZero + reprovados.TotalCargaHorariaCreditoMaiorZero;
            aprovadosDispensadoReprovado.TotalCreditoCreditoMaiorZero = aprovados.TotalCreditoCreditoMaiorZero + dispensados.TotalCreditoCreditoMaiorZero + reprovados.TotalCreditoCreditoMaiorZero;

            var qtdAprovadoDispensadoReprovadoCreditoMaiorZero = qtdAlunoNotaCreditoMaiorZero + qtdAlunoDispensadoCreditoMaiorZero + qtdAlunoReprovadoCreditoMaiorZero;

            if (qtdAprovadoDispensadoReprovadoCreditoMaiorZero == 0)
                qtdAprovadoDispensadoReprovadoCreditoMaiorZero = 1;

            aprovadosDispensadoReprovado.MediaNotaCreditoMaiorZero = ((qtdAlunoNotaCreditoMaiorZero * aprovados.MediaNotaCreditoMaiorZero) + (qtdAlunoDispensadoCreditoMaiorZero * dispensados.MediaNotaCreditoMaiorZero) + (qtdAlunoReprovadoCreditoMaiorZero * reprovados.MediaNotaCreditoMaiorZero)) / qtdAprovadoDispensadoReprovadoCreditoMaiorZero;

            aluno.TotaisComponentesConcluidos.Add(aprovadosDispensadoReprovado);


            var aprovadosDispensadoSemApuracao = new TotaisComponentesCreditosVO() { Seq = aluno.Seq, Situacao = $"{SituacaoHistoricoEscolar.Aprovado.SMCGetDescription()} + {SituacaoHistoricoEscolar.Dispensado.SMCGetDescription()} + Sem Apuração" };

            //Totalizador Aprovados + Dispensados + Sem Apuração
            aprovadosDispensadoSemApuracao.QuantidadeComponente = aprovados.QuantidadeComponente + dispensados.QuantidadeComponente + semApuracao.QuantidadeComponente;
            aprovadosDispensadoSemApuracao.TotalCargaHoraria = aprovados.TotalCargaHoraria + dispensados.TotalCargaHoraria + semApuracao.TotalCargaHoraria;
            aprovadosDispensadoSemApuracao.TotalCredito = aprovados.TotalCredito + dispensados.TotalCredito + semApuracao.TotalCredito;
            aprovadosDispensadoSemApuracao.MediaNota = (decimal)0.00;

            //Totalizador Aprovados + Dispensados + Sem Apuração crédito maior que zero
            aprovadosDispensadoSemApuracao.QuantidadeComponenteCreditoMaiorZero = aprovados.QuantidadeComponenteCreditoMaiorZero + dispensados.QuantidadeComponenteCreditoMaiorZero + semApuracao.QuantidadeComponenteCreditoMaiorZero;
            aprovadosDispensadoSemApuracao.TotalCargaHorariaCreditoMaiorZero = aprovados.TotalCargaHorariaCreditoMaiorZero + dispensados.TotalCargaHorariaCreditoMaiorZero + semApuracao.TotalCargaHorariaCreditoMaiorZero;
            aprovadosDispensadoSemApuracao.TotalCreditoCreditoMaiorZero = aprovados.TotalCreditoCreditoMaiorZero + dispensados.TotalCreditoCreditoMaiorZero + semApuracao.TotalCreditoCreditoMaiorZero;
            aprovadosDispensadoSemApuracao.MediaNotaCreditoMaiorZero = (decimal)0.00;

            aluno.TotaisComponentesConcluidos.Add(aprovadosDispensadoSemApuracao);
        }

        private void CalcularTotalAprovados(ItemRelatorioHistoricoEscolarVO aluno, TotaisComponentesCreditosVO aprovados)
        {

        }

        #endregion [ Relatório de Histórico Escolar Interno ]

        /// <summary>
        /// Buscar os dados das parcelas em aberto do aluno
        /// </summary>
        /// <param name="seqPessoaAtuacao">Sequencial do aluno</param>
        /// <returns>Dados de parcela em aberto</returns>
        public List<EmitirBoletoAbertoVO> BuscarParcelasPagamentoEmAberto(long seqPessoaAtuacao)
        {
            var aluno = this.SearchByKey(new SMCSeqSpecification<Aluno>(seqPessoaAtuacao));

            if (aluno == null)
                return new List<EmitirBoletoAbertoVO>();

            var filtro = new ViewTitulosAbertosFiltroData
            {
                CodigoAluno = aluno.CodigoAlunoMigracao,
                SeqOrigemGRA = 1
            };

            var dadosParcelas = IntegracaoFinanceiroService.BuscarParcelasEmAbertoAluno(filtro);
            List<EmitirBoletoAbertoVO> retorno = new List<EmitirBoletoAbertoVO>();

            if (dadosParcelas != null)
            {
                dadosParcelas.GroupBy(g => g.SeqServico).SMCForEach(f =>
                {
                    var item = new EmitirBoletoAbertoVO();
                    item.SeqServico = f.First().SeqServico;
                    item.DescricaoServico = f.First().DescricaoServico;
                    item.Parcelas = new List<EmitirBoletoAbertoParcelaVO>();

                    foreach (var parcela in f.ToList())
                    {
                        var dadosParcela = new EmitirBoletoAbertoParcelaVO();
                        dadosParcela.NumeroParcela = parcela.NumeroParcela;
                        dadosParcela.SemestreParcela = parcela.SemestreParcela;
                        dadosParcela.AnoParcela = parcela.AnoParcela;
                        dadosParcela.DataVencimentoTitulo = parcela.DataVencimentoTitulo;
                        dadosParcela.DataLimitePagamento = parcela.DataLimitePagamento;
                        dadosParcela.Boletos = new List<EmitirBoletoAbertoBoletoVO>();
                        dadosParcela.Boletos.Add(new EmitirBoletoAbertoBoletoVO()
                        {
                            NomePessoa = parcela.NomePessoa,
                            // Regra de exibição do boleto: se o TipoSituacaoTitulo = 5 ou SeqSituacaoTitulo = 1 a situação do título é Bloqueado.
                            SituacaoTitulo = (parcela.TipoSituacaoTitulo == 5 || parcela.SeqSituacaoTitulo == 1) ? SituacaoTitulo.Bloqueado : (SituacaoTitulo)parcela.TipoSituacaoTitulo,
                            SeqTitulo = parcela.SeqTitulo,
                            SeqServico = parcela.SeqServico
                        });

                        item.Parcelas.Add(dadosParcela);
                    }

                    retorno.Add(item);
                });
            }

            return retorno;
        }

        /// <summary>
        /// Gerar o boleto de acordo com o título e o serviço no portal do aluno
        /// </summary>
        /// <param name="seqTitulo">Sequencial do titulo do boleto</param>
        /// <param name="seqServico">Sequencial do serviço</param>
        /// <param name="seqPessoaAtuacao">Sequencial do aluno solicitante</param>
        /// <returns></returns>
        public AlunoEmitirBoletoVO GerarBoletoAluno(long seqTitulo, long seqServico, long seqPessoaAtuacao)
        {
            // Recupera o sequencial do titulo corretamente
            var retorno = IntegracaoFinanceiroService.EmissaoBoletoWebRegistro((int)seqTitulo, (int)seqServico);

            if (retorno.Sucesso)
            {
                var seqTituloGerado = (int)retorno.Codigo;

                // Chama o serviço para geração dos dados usados para geração do boleto
                BoletoData dadosBoleto = IntegracaoFinanceiroService.BuscarBoleto(seqTituloGerado);
                dadosBoleto.ImagemBanco = SMCImageHelper.ImageToBase64(BuscarImagemBanco(dadosBoleto.Banco.Numero));
                dadosBoleto.ImagemCodigoBarras = SMCImageHelper.ImageToBase64(BuscarImagemCodigoBarras(dadosBoleto.CodigoBarras));

                var boleto = dadosBoleto.Transform<AlunoEmitirBoletoVO>();

                // Recupera qual é a descrição do vínculo da pessoa atuação
                var dadosBasicos = this.SearchProjectionByKey(new SMCSeqSpecification<Aluno>(seqPessoaAtuacao), x => new
                {
                    RegistroAcademico = (x as Aluno).CodigoAlunoMigracao,   //(x as Aluno).NumeroRegistroAcademico, Modificar quando os sistemas tiverem integrando o RA
                    DescricaoCurso = (x as Aluno).Historicos.OrderByDescending(o => o.DataInclusao).FirstOrDefault().CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.Nome,
                });

                boleto.RegistroAcademico = $"Matrícula: {dadosBasicos.RegistroAcademico}";
                boleto.DescricaoCurso = dadosBasicos.DescricaoCurso;

                return boleto;
            }
            else
            {
                //Ocorreu um ao registrar o Boleto
                throw new SMCApplicationException(retorno.Mensagem);
            }
        }

        // GET: /BoletoBancario/ImagemBanco/?codigoBanco=1
        private byte[] BuscarImagemBanco(int codigoBanco)
        {
            return IntegracaoFinanceiroService.BuscarImagemBanco(codigoBanco);
        }

        // GET: /BoletoBancario/ImagemCodigoBarras/codigoBarras=123456
        private byte[] BuscarImagemCodigoBarras(string codigoBarras)
        {
            return IntegracaoFinanceiroService.BuscarImagemCodigoBarras(codigoBarras);
        }

        /// <summary>
        /// Busca o registro acadêmico para comprovante de processos
        /// </summary>
        /// <param name="seqPessoaAtuacao">Sequencial do aluno</param>
        /// <returns>Número do registro acadêmico</returns>
        public long BuscarRegistroAcademicoAluno(long seqPessoaAtuacao)
        {
            var registro = this.SearchProjectionByKey(new SMCSeqSpecification<Aluno>(seqPessoaAtuacao), p => p.CodigoAlunoMigracao);

            return registro.GetValueOrDefault();
        }

        /// <summary>
        /// Busca os dados do aluno para o header BI_ALN_001 - Aluno - Cabeçalho
        /// </summary>
        /// <param name="seq">Sequencial do aluno</param>
        /// <returns>Dados do aluno para o header</returns>
        public AlunoCabecalhoVO BuscarAlunoCabecalho(long seq)
        {
            var aluno = this.SearchProjectionByKey(new SMCSeqSpecification<Aluno>(seq), p => new AlunoCabecalhoVO()
            {
                SeqNivelEnsino = p.Historicos.Where(w => w.Atual).FirstOrDefault().SeqNivelEnsino,
                SeqTipoVinculoAluno = p.SeqTipoVinculoAluno,
                SeqTipoTermoIntercambio = p.TermosIntercambio.OrderByDescending(o => o.Seq).FirstOrDefault().TermoIntercambio.ParceriaIntercambioTipoTermo.SeqTipoTermoIntercambio,
                NumeroRegistroAcademico = p.NumeroRegistroAcademico,
                Nome = p.DadosPessoais.Nome,
                NomeSocial = p.DadosPessoais.NomeSocial,
                DescricaoSituacaoMatricula = p.Historicos.FirstOrDefault(w => w.Atual).HistoricosCicloLetivo.OrderByDescending(o => o.CicloLetivo.Ano).ThenByDescending(o => o.CicloLetivo.Numero).FirstOrDefault(c => !c.DataExclusao.HasValue).AlunoHistoricoSituacao.OrderByDescending(o => o.DataInicioSituacao).FirstOrDefault(s => s.DataInicioSituacao <= DateTime.Today && !s.DataExclusao.HasValue).SituacaoMatricula.Descricao,
                Falecido = p.Pessoa.Falecido,
                NomeEntidadeResponsavel = p.Historicos.Where(w => w.Atual).FirstOrDefault().EntidadeVinculo.Nome,
                DescricaoVinculo = p.Descricao,
                DescricaoFormaIngresso = p.Historicos.FirstOrDefault(f => f.Atual).FormaIngresso.Descricao,
                DescricaoTipoTermoIntercambio = p.TermosIntercambio.OrderByDescending(o => o.Seq).FirstOrDefault().TermoIntercambio.ParceriaIntercambioTipoTermo.TipoTermoIntercambio.Descricao,
                TipoVinculoAluno = p.TipoVinculoAluno.Descricao,
                DataAdmissao = p.Historicos.FirstOrDefault(f => f.Atual).DataAdmissao,
                DescricaoNivelEsino = p.Historicos.FirstOrDefault(f => f.Atual).NivelEnsino.Descricao,
                DataInicioTermoIntercambio = p.TermosIntercambio.Min(mt => mt.Periodos.OrderByDescending(x => x.Seq).FirstOrDefault().DataInicio),
                DataFimTermoIntercambio = p.TermosIntercambio.Max(mt => mt.Periodos.OrderByDescending(x => x.Seq).FirstOrDefault().DataFim),
                DescricaoInstituicaoExterna = p.TermosIntercambio.FirstOrDefault().TermoIntercambio.ParceriaIntercambioInstituicaoExterna.InstituicaoExterna.Nome,
                CodigoAlunoMigracao = p.CodigoAlunoMigracao,
                DataLimiteConclusao = (DateTime?)p.Historicos.FirstOrDefault(f => f.Atual).PrevisoesConclusao.OrderByDescending(o => o.DataInclusao).FirstOrDefault().DataLimiteConclusao,
                DataPrevisaoConclusao = (DateTime?)p.Historicos.FirstOrDefault(f => f.Atual).PrevisoesConclusao.OrderByDescending(o => o.DataInclusao).FirstOrDefault().DataPrevisaoConclusao,
                SeqInstituicaoEnsino = p.Pessoa.SeqInstituicaoEnsino,
            });

            var configuracaoVinculo = this.InstituicaoNivelTipoVinculoAlunoDomainService
                .BuscarConfiguracaoVinculo(aluno.SeqNivelEnsino, aluno.SeqTipoVinculoAluno);

            aluno.ExigeParceriaIntercambioIngresso = configuracaoVinculo.ExigeParceriaIntercambioIngresso;
            aluno.ExigePeriodoIntercambioTermo = aluno.SeqTipoTermoIntercambio != null ? configuracaoVinculo.TiposTermoIntercambio.FirstOrDefault(f => f.SeqTipoTermoIntercambio == aluno.SeqTipoTermoIntercambio)?.ExigePeriodoIntercambioTermo ?? false : false;

            // RN_ALN_029 - Descrição Vínculo Tipo Termo Intercâmbio
            aluno.TipoVinculoAluno = RecuperarVinculoAluno(configuracaoVinculo,
                                                           aluno.SeqTipoTermoIntercambio,
                                                           aluno.TipoVinculoAluno,
                                                           aluno.DescricaoTipoTermoIntercambio);

            // RN_PES_023 - Nome e Nome Social - Visão Administrativo
            if (!string.IsNullOrEmpty(aluno.NomeSocial))
                aluno.Nome = $"{aluno.NomeSocial} ({aluno.Nome})";

            ///RN_ALN_057 - Exibição do ciclo letivo da situação de matrícula válida
            var dadosSituacaoMatricula = AlunoHistoricoSituacaoDomainService.BuscarSituacaoAtualAluno(seq);
            var descCicloLetivo = this.CicloLetivoDomainService.SearchProjectionByKey(dadosSituacaoMatricula.SeqCiclo, p => p.Descricao);
            aluno.DescricaoSituacaoMatricula = $"{descCicloLetivo} - {dadosSituacaoMatricula.Descricao}";

            var pessoaAtuacaoIntercambioSpec = new PessoaAtuacaoTermoIntercambioFilterSpecification { SeqNivelEnsino = aluno.SeqNivelEnsino, SeqPessoaAtuacao = seq, SeqTipoVinculo = aluno.SeqTipoVinculoAluno };
            var pessoaAtuacaoIntercambio = PessoaAtuacaoTermoIntercambioDomainService.SearchProjectionBySpecification(pessoaAtuacaoIntercambioSpec, x => x.SeqTermoIntercambio).ToArray();
            var termoIntercambioSpec = new TermoIntercambioFilterSpecification { SeqsTermosIntercambio = pessoaAtuacaoIntercambio };
            var termoIntercambio = TermoIntercambioDomainService.SearchProjectionBySpecification(termoIntercambioSpec, x => x.SeqParceriaIntercambioTipoTermo).ToArray();
            var parceriaTipoTermoSpec = new ParceriaIntercambioTipoTermoFilterSpecification { SeqsParceriaIntercambioTipoTermo = termoIntercambio };
            var parceriaTipoTermo = ParceriaIntercambioTipoTermoDomainService.SearchBySpecification(parceriaTipoTermoSpec);
            var tipoTermoConcedeFormacao = false;
            foreach (var item in parceriaTipoTermo)
            {
                var concedeFormacao = TipoTermoIntercambioDomainService.TipoTermoIntercambioConcedeFormacao(item.SeqTipoTermoIntercambio, aluno.SeqTipoVinculoAluno, aluno.SeqNivelEnsino, aluno.SeqInstituicaoEnsino);
                if (concedeFormacao)
                    tipoTermoConcedeFormacao = true;
            }

            aluno.ConcedeFormacao = tipoTermoConcedeFormacao;

            return aluno;
        }

        /// <summary>
        /// Busca o ciclo letivo atual de um aluno
        /// É considerado ciclo letivo atual:
        /// 1) o ciclo corrente, ou seja cujo período do ciclo letivo esteja vigente.
        /// 2) Caso não encontre o ciclo para o aluno com período vigente, busca o ultimo ciclo (maior ano/número)
        /// </summary>
        /// <param name="seqAluno">Sequencial do aluno</param>
        /// <param name="desativarFiltroDados">Desabilita o filtro de dados de hierarquia_entidade_organizacional para atender o caso de entidades compartilhadas da tela de consulta de solicitações de serviço </param>
        /// <returns>Ciclo letivo atual</returns>
        public long BuscarCicloLetivoAtual(long seqAluno, bool desativarFiltroDados = false)
        {
            if (desativarFiltroDados)
                this.DisableFilter(FILTER.HIERARQUIA_ENTIDADE_ORGANIZACIONAL);

            // Busca o curso-oferta-turno do aluno
            var specAluno = new SMCSeqSpecification<Aluno>(seqAluno);

            // Busca os dados de origem da pessoa atuação
            var dadosOrigem = PessoaAtuacaoDomainService.RecuperaDadosOrigem(seqAluno, desativarFiltroDados);

            // Recupera o ciclo letivo corrente (cujo periodo do evento letivo é corrente) para o curso-oferta-turno do aluno
            long seqCiclo = ConfiguracaoEventoLetivoDomainService.BuscarSeqCicloLetivo(DateTime.Today, dadosOrigem.SeqCursoOfertaLocalidadeTurno, null, TOKEN_TIPO_EVENTO.PERIODO_CICLO_LETIVO);

            // Se não encontou, busca o ultimo ciclo letivo do aluno (ciclo de maior ano/numero)
            if (seqCiclo == 0)
            {
                seqCiclo = BuscarUltimoCicloLetivoAlunoHistorico(seqAluno);
            }
            else
            {
                // Verifica se o aluno possui o ciclo letivo atual
                var possuiCicloAtual = this.SearchProjectionByKey(specAluno, x => x.Historicos
                                                                                   .FirstOrDefault(h => h.Atual)
                                                                                   .HistoricosCicloLetivo.Any(a => a.SeqCicloLetivo == seqCiclo && !a.DataExclusao.HasValue && a.AlunoHistoricoSituacao.Any(s => !s.DataExclusao.HasValue)));
                if (!possuiCicloAtual)
                    seqCiclo = BuscarUltimoCicloLetivoAlunoHistorico(seqAluno);
            }

            if (desativarFiltroDados)
                this.EnableFilter(FILTER.HIERARQUIA_ENTIDADE_ORGANIZACIONAL);

            return seqCiclo;
        }

        /// <summary>
        /// Recupera o último ciclo letivo de um aluno que possui histórico de situação não excluído
        /// </summary>
        /// <param name="seqAluno">Sequencial do aluno a ser utilizado</param>
        /// <returns>Sequencial do último ciclo letivo do aluno</returns>
        public long BuscarUltimoCicloLetivoAlunoHistorico(long seqAluno)
        {
            var specAluno = new SMCSeqSpecification<Aluno>(seqAluno);
            var retorno = this.SearchProjectionByKey(specAluno, x => x.Historicos.FirstOrDefault(h => h.Atual)
                                                               .HistoricosCicloLetivo
                                                               .OrderByDescending(c => c.CicloLetivo.Ano)
                                                               .ThenByDescending(c => c.CicloLetivo.Numero)
                                                               .FirstOrDefault(c => !c.DataExclusao.HasValue && c.AlunoHistoricoSituacao.Any(s => !s.DataExclusao.HasValue)));
            if (retorno == null)
                throw new SolicitacaoServicoSemAlunoHistoricoSituacaoException();

            return retorno.SeqCicloLetivo;
        }

        /// <summary>
        /// Busca os ciclos letivos do aluno histórico que teve em algum momento situação de
        /// matriculado, matriculado em mobilidade ou provável formando sem data de exclusão
        /// </summary>
        /// <param name="seqAluno">Sequencial do aluno</param>
        /// <returns>Ciclos letivos encontrados</returns>
        public List<SMCDatasourceItem> BuscarCiclosLetivosAlunoHistoricoSituacaoSelect(long seqAluno)
        {
            var ciclosLetivosAlunoHistorico = new List<SMCDatasourceItem>();
            var specAluno = new SMCSeqSpecification<Aluno>(seqAluno);
            var situacao = new List<string> { TOKENS_SITUACAO_MATRICULA.MATRICULADO, TOKENS_SITUACAO_MATRICULA.MATRICULADO_MOBILIDADE, TOKENS_SITUACAO_MATRICULA.PROVAVEL_FORMANDO };

            var retorno = this.SearchBySpecification(specAluno, d => d.Historicos[0].HistoricosCicloLetivo[0].CicloLetivo,
                                                                d => d.Historicos[0].HistoricosCicloLetivo[0].AlunoHistoricoSituacao[0].SituacaoMatricula).ToList();
            var historicosCicloLetivo = retorno.Select(h => h.Historicos.SelectMany(s => s.HistoricosCicloLetivo).OrderBy(o => o.CicloLetivo.AnoNumeroCicloLetivo).ToList());

            foreach (var historicoCicloLetivo in historicosCicloLetivo)
            {
                foreach (var item in historicoCicloLetivo)
                {
                    if (item.AlunoHistoricoSituacao.Where(w => !w.DataExclusao.HasValue && situacao.Contains(w.SituacaoMatricula.Token)).Any())
                        ciclosLetivosAlunoHistorico.Add(new SMCDatasourceItem() { Seq = item.CicloLetivo.Seq, Descricao = item.CicloLetivo.Descricao });
                }
            }

            return ciclosLetivosAlunoHistorico;
        }

        /// <summary>
        /// Busca o ciclos letivos do aluno histórico de um aluno
        /// </summary>
        /// <param name="seqAluno">Sequencial do aluno</param>
        /// <returns>Ciclos letivos encontrados</returns>
        public List<SMCDatasourceItem> BuscarCiclosLetivosAlunoHistoricoSelect(long seqAluno)
        {
            var ciclosLetivosAlunoHistorico = new List<SMCDatasourceItem>();

            var specAluno = new SMCSeqSpecification<Aluno>(seqAluno);

            var result = this.SearchProjectionByKey(specAluno,
                c => c.Historicos.SelectMany(h => h.HistoricosCicloLetivo)
                                 .Select(a => a.CicloLetivo)
                                 .OrderBy(x => x.AnoNumeroCicloLetivo))
                                 .ToList();

            result.ForEach(x =>
            {
                ciclosLetivosAlunoHistorico.Add(new SMCDatasourceItem() { Seq = x.Seq, Descricao = x.Descricao });
            });

            return ciclosLetivosAlunoHistorico;
        }

        /// <summary>
        /// Recupera a descrição do vínculo segundo as regras:
        /// RN_ALN_029 Descrição Vínculo Tipo Termo Intercâmbio
        ///   Caso a regra RN_ALN_031  - Consistência Vínculo Tipo Termo Intercâmbio ocorra, exibir a descrição do vínculo:  "Vínculo" + "-" + "Tipo de Termo de Intercâmbio"
        ///
        /// RN_ALN_031  Consistência Vínculo Tipo Termo Intercâmbio
        ///   Se o termo de intercâmbio associado à pessoa-atuação for de um tipo de termo de intercâmbio parametrizado para conceder formação de acordo com a Instituição de Ensino logada, Nível de Ensino e Vínculo da pessoa-atuação em questão.
        /// Ou
        ///   Se o vínculo da pessoa-atuação for parametrizado para exigir parceria no ingresso de acordo com a Instituição de Ensino logada e Nível de Ensino da pessoa-atuação em questão.
        /// </summary>
        /// <param name="configuracaoVinculo">Configurações do vínculo para o nível de ensino e tipo de vínculo do aluno</param>
        /// <param name="seqTipoTermoIntercambio">Sequencial do tipo de termo do aluno</param>
        /// <param name="descricaoVinculo">Descrição da pessoa atuação do aluno (já contêm o vínculo formatado)</param>
        /// <param name="descricaoTipoIntercambio">Descrição do tipo de termo de intercâmbio</param>
        /// <returns>Descrição do vínculo formatado segundo a regra RN_ALN_029</returns>
        public string RecuperarVinculoAluno(InstituicaoNivelTipoVinculoAluno configuracaoVinculo, long? seqTipoTermoIntercambio, string descricaoVinculo, string descricaoTipoIntercambio)
        {
            if (configuracaoVinculo != null)
            {
                var configuracaoTipoTermoIntercambio = configuracaoVinculo.TiposTermoIntercambio.FirstOrDefault(f => f.SeqTipoTermoIntercambio == seqTipoTermoIntercambio);
                if (  // RN_ALN_031  Consistência Vínculo Tipo Termo Intercâmbio
                      (configuracaoVinculo.ExigeParceriaIntercambioIngresso || (configuracaoTipoTermoIntercambio?.ConcedeFormacao ?? false))
                      // E tenha dados de intercâmbio para evitar [Descricao] - []
                      && descricaoTipoIntercambio != null
                   )
                {
                    return $"{descricaoVinculo} - {descricaoTipoIntercambio}";
                }
            }
            return descricaoVinculo;
        }

        private string RemoveEspacosEnter(string str)
        {
            return string.IsNullOrEmpty(str) ? string.Empty : Regex.Replace(str.Trim(), @"\t|\n|\r", "");
        }

        /// <summary>
        /// Valida se a pessoa atuação informada é aluno
        /// </summary>
        /// <param name="seqPessoaAtuacao">Sequencial da pessoa atuação</param>
        /// <returns>Verdadeiro caso seja um aluno</returns>
        public bool ValidarAtuacaoAluno(long seqPessoaAtuacao)
        {
            return Count(new SMCSeqSpecification<Aluno>(seqPessoaAtuacao)) > 0;
        }

        /// <summary>
        /// Recupera os sequenciais de alunos (pessoa atuação) para o mesmo sequencial de pessoa do aluno informado
        /// </summary>
        /// <param name="seqPessoaAtuacao">Sequencial do aluno</param>
        /// <returns>Lista de sequenciais de todas as atuações do aluno inclusive a atual</returns>
        public List<long> BuscarSequenciaisPessoaAtuacaoAlunoPessoa(long seqPessoaAtuacao)
        {
            var specAlunoAtual = new SMCSeqSpecification<Aluno>(seqPessoaAtuacao);

            var seqPessoa = this.SearchProjectionByKey(specAlunoAtual, p => p.SeqPessoa);

            var specAlunosPessoa = new AlunoFilterSpecification() { SeqPessoa = seqPessoa };

            var registros = this.SearchProjectionBySpecification(specAlunosPessoa, p => p.Seq).ToList();

            return registros;
        }

        public List<RelatorioAlunosPorSituacaoVO> BuscarDadosRelatorioAlunosPorSituacao(RelatorioAlunosPorSituacaoFiltroVO filtros)
        {
            // Formata a query
            string query = string.Empty;
            if (filtros.TipoAtuacao == TipoAtuacao.Aluno)
            {
                query = string.Format(QUERY_RELATORIO_ALUNO_POR_SITUACAO,
                    filtros.SeqCicloLetivo, // @SEQ_CICLO_LETIVO
                    filtros.SeqCicloLetivoIngresso.HasValue ? filtros.SeqCicloLetivoIngresso.Value.ToString() : "NULL", // @SEQ_CICLO_LETIVO_INGRESSO
                    filtros.SeqsEntidadesResponsaveis.SMCCount() > 0 ? string.Format("'{0}'", string.Join(",", filtros.SeqsEntidadesResponsaveis)) : "NULL", // @SEQS_ENTIDADES_RESPONSAVEIS
                    filtros.SeqCursoOfertaLocalidade.HasValue ? filtros.SeqCursoOfertaLocalidade.ToString() : "NULL", // @SEQ_CURSO_OFERTA_LOCALIDADE
                    filtros.SeqTurno.HasValue ? filtros.SeqTurno.Value.ToString() : "NULL", // @SEQ_TURNO
                    filtros.SeqsTipoSituacaoMatricula.SMCCount() > 0 ? string.Format("'{0}'", string.Join(",", filtros.SeqsTipoSituacaoMatricula)) : "NULL", // @TIPOS_SITUACAO_MATRICULA
                    filtros.SeqsSituacaoMatricula.SMCCount() > 0 ? string.Format("'{0}'", string.Join(",", filtros.SeqsSituacaoMatricula)) : "NULL", // @SITUACOES_MATRICULA
                    filtros.SeqsTipoVinculoAluno.SMCCount() > 0 ? string.Format("'{0}'", string.Join(",", filtros.SeqsTipoVinculoAluno)) : "NULL" // @TIPOS_VINCULO
                );
            }
            else if (filtros.TipoAtuacao == TipoAtuacao.Ingressante)
            {
                query = string.Format(QUERY_RELATORIO_INGRESSANTE_POR_SITUACAO,
                    filtros.SeqCicloLetivo, // @SEQ_CICLO_LETIVO
                    filtros.SeqsEntidadesResponsaveis.SMCCount() > 0 ? string.Format("'{0}'", string.Join(",", filtros.SeqsEntidadesResponsaveis)) : "NULL", // @SEQS_ENTIDADES_RESPONSAVEIS
                    filtros.SeqCursoOfertaLocalidade.HasValue ? filtros.SeqCursoOfertaLocalidade.ToString() : "NULL", // @SEQ_CURSO_OFERTA_LOCALIDADE
                    filtros.SeqTurno.HasValue ? filtros.SeqTurno.Value.ToString() : "NULL", // @SEQ_TURNO
                    filtros.SituacoesIngressante.SMCCount() > 0 ? string.Format("'{0}'", string.Join(",", filtros.SeqsSituacoesIngressante)) : "NULL", // @SITUACOES_INGRESSANTE
                    filtros.SeqsTipoVinculoAluno.SMCCount() > 0 ? string.Format("'{0}'", string.Join(",", filtros.SeqsTipoVinculoAluno)) : "NULL" // @TIPOS_VINCULO
                );
            }
            else
            {
                throw new TipoAtuacaoInvalidoException();
            }

            // Executa a query e retorna a lista
            return this.RawQuery<RelatorioAlunosPorSituacaoVO>(query);
        }

        public List<int> BuscarDadosArquivoSMDAlunosPorSituacao(RelatorioAlunosPorSituacaoFiltroVO filtros)
        {
            // Executa a query do relatório de alunos por situação
            var listaRelatorio = this.BuscarDadosRelatorioAlunosPorSituacao(filtros);

            // Para cada registro encontrado no relatório, busca o cod-aluno e inclui no retorno
            List<int> listaCodAluno = new List<int>();
            foreach (var item in listaRelatorio)
            {
                listaCodAluno.Add(PessoaDomainService.BuscarCodigoDePessoaNosDadosMestres(item.SeqPessoa, TipoPessoa.Fisica, null));
            }
            return listaCodAluno.Distinct().ToList();
        }

        public List<RelatorioAlunosPorComponenteListaVO> BuscarDadosRelatorioAlunosPorComponente(RelatorioAlunosPorComponenteFiltroVO filtros)
        {
            var query = string.Empty;

            //Se filtrado por turma
            if (filtros.SeqTurma.HasValue)
            {
                return this.RawQuery<RelatorioAlunosPorComponenteListaVO>(QUERY_RELATORIO_ALUNO_POR_COMPONENTE_TURMA,
                                                                       new SMCFuncParameter("SEQ_TURMA", filtros.SeqTurma));
            }
            else //Senão, filtrado por atividade acadêmica
            {
                return this.RawQuery<RelatorioAlunosPorComponenteListaVO>(QUERY_RELATORIO_ALUNO_POR_COMPONENTE_ATIVIDADE_ACADEMICA,
                                                                 new SMCFuncParameter("SEQ_CICLO_LETIVO", filtros.SeqCicloLetivo),
                                                                 new SMCFuncParameter("SEQ_CURSO_OFERTA_LOCALIDADE", filtros.SeqCursoOfertaLocalidade),
                                                                 new SMCFuncParameter("SEQ_TURNO", filtros.SeqTurno.GetValueOrDefault()),
                                                                 new SMCFuncParameter("SEQ_CONFIGURACAO_COMPONENTE", filtros.SeqConfiguracaoComponente.GetValueOrDefault()));
            }
        }

        /// <summary>
        /// Realiza o trancamento de matrícula do um aluno (RN_MAT_082)
        /// </summary>
        /// <param name="seqSolicitacaoServico">Solicitação de serviço para trancamento da matrícula</param>
        public void TrancarMatricula(long seqSolicitacaoServico)
        {
            // Busca os dados da solicitação
            var dadosSolicitacao = SolicitacaoServicoDomainService.SearchProjectionByKey(seqSolicitacaoServico, x => new
            {
                NumeroProtocolo = x.NumeroProtocolo,
                SeqPessoaAtuacao = x.SeqPessoaAtuacao,
                DataSolicitacao = x.DataSolicitacao,
                SeqCursoOfertaLocalidadeTurno = (x.PessoaAtuacao as Aluno).Historicos.FirstOrDefault(h => h.Atual).SeqCursoOfertaLocalidadeTurno,
                SeqProcesso = x.ConfiguracaoProcesso.SeqProcesso,
                SeqAlunoHistorico = x.SeqAlunoHistorico,
                DescricaoServico = x.ConfiguracaoProcesso.Processo.Servico.Descricao,
                NomeSocialSolicitante = x.PessoaAtuacao.DadosPessoais.NomeSocial,
                NomeSolicitante = x.PessoaAtuacao.DadosPessoais.Nome,
                DescricaoProcesso = x.ConfiguracaoProcesso.Processo.Descricao,
                CodigoAlunoSGP = (x.PessoaAtuacao as Aluno).CodigoAlunoMigracao,
                SeqCicloLetivoProcesso = x.ConfiguracaoProcesso.Processo.SeqCicloLetivo,
                AnoCicloProcesso = x.ConfiguracaoProcesso.Processo.CicloLetivo.Ano,
                NumeroCicloProcesso = x.ConfiguracaoProcesso.Processo.CicloLetivo.Numero
            });

            // Se a solicitação não possui o seq-aluno-historico informado, erro
            if (!dadosSolicitacao.SeqAlunoHistorico.HasValue)
                throw new SolicitacaoServicoSemAlunoHistoricoException();

            // Se o processo não possui ciclo letivo, erro
            if (!dadosSolicitacao.SeqCicloLetivoProcesso.HasValue)
                throw new SolicitacaoServicoSemCicloLetivoException();

            // Se o aluno não possui código de migração, erro
            if (!dadosSolicitacao.CodigoAlunoSGP.HasValue)
                throw new AlunoMigracaoInvalidoException();

            // 1. Verificar se o vínculo do aluno FOR de disciplina isolada(Verificar se no parâmetro de
            // vínculo -instituição - nível de ensino, o vínculo da pessoa - atuação em questão NÃO EXIGE
            // CURSO, de acordo com a Instituição de Ensino logada e o nível de ensino da pessoa - atuação).
            // SE NÃO EXIGIR CURSO, abortar a operação e exibir a seguinte mensagem de erro:
            // "Não é possível efetuar o trancamento de matrícula de aluno que não possui vínculo em curso"
            var dadosVinculo = InstituicaoNivelTipoVinculoAlunoDomainService.BuscarInstituicaoNivelTipoVinculoAlunoPorPessoaAtuacao(dadosSolicitacao.SeqPessoaAtuacao);
            if (!dadosVinculo.ExigeCurso.GetValueOrDefault())
                throw new TrancamentoMatriculaAlunoDIException();

            // 2. Verificar se o aluno possui parcelas vencidas e com a situação em aberto, anterior a data do
            // pedido da solicitacao.Se existir, exibir a seguinte mensagem de confirmação:
            // "Atenção! Esse aluno possui pendência financeira com a instituição. Gentileza orientá-lo a procurar
            // a divisão financeira para regularizar sua situação. Deseja realmente efetuar o trancamento?"
            // Se o usuário selecionar "NÃO", abortar a operação.
            // Se selecionar "SIM" continuar com as verificações abaixo.
            // FEITO NO CONTROLLER

            // 3. [Implementar FUTURA, quando existir grade horária]
            // Verificar se já transcorreu ¼ (um quarto) da carga horária de alguma das turmas do plano de
            // estudos atual do aluno.Se sim, exibir a seguinte mensagem de confirmação:
            // "Segundo o Artigo 42 das normas da instituição, não é possível efetuar o trancamento se já
            // houver transcorrido 1/4 da carga horária de alguma das turmas do aluno. Esse aluno possui
            // pelo menos uma turma nessa condição. Mesmo assim, deseja efetuar o trancamento? Se o usuário
            // selecionar "NÃO", abortar a operação. Se selecionar "SIM" continuar com as verificações abaixo.
            // [REGRA A SER IMPLEMENTADA APENAS QUANDO EXISTIR GRADE HORÁRIA).

            // 4. AJUSTES NO PLANO DE ESTUDO
            // - LIBERAR AS VAGAS OCUPADAS PELO ALUNO NAS TURMAS DO PLANO DE ENSINO ATUAL, CONSIDERANDO A
            // DATA DO PEDIDO DA SOLICITAÇÃO - Para cada divisão de turma existente no plano de estudo ATUAL do
            // ciclo letivo do processo da solicitação e o evento que se aplica ao aluno de acordo com as
            // parametrizações por entidade e nível de ensino, subtrair 1 da qtd_vagas_ocupadas.
            // - Finaliza as orientações de turma do plano atual
            // - Alterar o indicador de atual do plano de estudos ATUAL para o valor NÃO.
            // - Incluir um novo registro de plano de estudos sem item, de acordo com RN_MAT_112 - Inclusão plano
            // de estudo sem item*, passando como parâmetro o ciclo letivo em questão, a oferta de matriz do plano
            // de estudo em questão,caso exista e a solicitação de serviço, caso exista.

            // Busca o plano de estudo ATUAL do aluno no ciclo letivo do processo da solicitação
            var specPlano = new PlanoEstudoFilterSpecification()
            {
                SeqAluno = dadosSolicitacao.SeqPessoaAtuacao,
                Atual = true,
                SeqCicloLetivo = dadosSolicitacao.SeqCicloLetivoProcesso
            };
            var planoEstudo = PlanoEstudoDomainService.SearchByKey(specPlano, IncludesPlanoEstudo.Itens);
            if (planoEstudo != null)
            {
                // Para cada item do plano, libera a vaga.
                foreach (var item in planoEstudo.Itens.Where(i => i.SeqDivisaoTurma.HasValue))
                {
                    PlanoEstudoItemDomainService.LiberarVagaPlanoEstudoItem(item.Seq);
                }

                // Finaliza as orientações de turma do plano atual
                PlanoEstudoDomainService.FinalizaOrientacoesPlano(planoEstudo.Seq);

                // Altera o indicador de atual para false
                planoEstudo.Atual = false;
                PlanoEstudoDomainService.UpdateFields(planoEstudo, x => x.Atual);

                // Inclui novo plano de estudos atual sem item
                PlanoEstudo planoSemItem = new PlanoEstudo()
                {
                    SeqAlunoHistoricoCicloLetivo = planoEstudo.SeqAlunoHistoricoCicloLetivo,
                    SeqMatrizCurricularOferta = planoEstudo.SeqMatrizCurricularOferta,
                    SeqSolicitacaoServico = seqSolicitacaoServico,
                    Atual = true,
                    Observacao = "Plano de estudo sem componente curricular, devido à alteração da situação do aluno"
                };
                PlanoEstudoDomainService.InsertEntity(planoSemItem);
            }

            // 6. EXCLUIR AS SITUAÇÕES FUTURAS DO ALUNO
            // Se existir alguma situação de matrícula, no ciclo letivo do trancamento (ciclo do processo), com data
            // de inicio MAIOR que a data do pedido da solicitação e sem data de exclusão, setar para todas as situações
            // os valores abaixo:
            // - Data de exclusão: data atual(do sistema).
            // - Usuário de exclusão: usuário logado.
            // - Descrição observação da exclusão: "Excluído devido ao trancamento de matrícula"
            // - Solicitação de serviço de exclusão: solicitação que está sendo deferida.
            var situacoesFuturasAluno = AlunoHistoricoSituacaoDomainService.BuscarSituacoesFuturasAluno(dadosSolicitacao.SeqPessoaAtuacao, dadosSolicitacao.DataSolicitacao, dadosSolicitacao.SeqCicloLetivoProcesso);
            foreach (var situacao in situacoesFuturasAluno)
            {
                AlunoHistoricoSituacaoDomainService.ExcluirAlunoHistoricoSituacao(situacao.SeqAlunoHistoricoSituacao, "Excluído devido ao trancamento de matrícula", seqSolicitacaoServico);
            }

            // 5. ATUALIZAR A SITUAÇÃO DO ALUNO
            // Incluir novo registro no historico de situação do aluno, para o ciclo letivo do processo da solicitação
            // com os seguintes valores:
            // - Situação de matrícula = situação com token "TRANCADO".
            // - Solicitação de matrícula = solicitação que está sendo deferida.
            // - Data início da situação = a data do pedido da solicitação.
            var historico = new IncluirAlunoHistoricoSituacaoVO()
            {
                SeqAluno = dadosSolicitacao.SeqPessoaAtuacao,
                SeqCicloLetivo = dadosSolicitacao.SeqCicloLetivoProcesso,
                TokenSituacao = TOKENS_SITUACAO_MATRICULA.TRANCADO,
                SeqSolicitacaoServico = seqSolicitacaoServico,
                DataInicioSituacao = dadosSolicitacao.DataSolicitacao
            };
            AlunoHistoricoSituacaoDomainService.IncluirAlunoHistoricoSituacao(historico);

            // 7. ATUALIZAR O INDICADOR DE ATIVIDADE DA PESSOA-ATUAÇÃO[IMPLEMENTAÇÃO FUTURA – implementar no
            // futuro caso o indicador de atividade da pessoa atuação permaneça]
            // Atualizar o valor do ind_ativo de acordo com o tipo de situação da situação incluída(item 5).
            // [NÃO SERÁ IMPLEMENTADO AGORA. VERIFICAR SE O FLAG DE ATIVIDADE DA PESSOA ATUAÇÃO SERÁ MANTIDO]

            // 8. ENVIAR NOTIFICAÇÃO INFORMATIVA DE BENEFÍCIO QUE O ALUNO TENHA.
            // Verificar se o aluno possui pelo menos um benefício associado que esteja na situação "DEFERIDO"
            // e dentro do período de vigência.Caso possua, enviar a notificação conforme a regra:
            // RN_MAT_101 - Notificação - Deferimento pedido de trancamento de matrícula.
            var beneficio = PessoaAtuacaoBeneficioDomainService.BuscarPesssoasAtuacoesBeneficiosMatricula(dadosSolicitacao.SeqPessoaAtuacao, SituacaoChancelaBeneficio.Deferido);
            if (beneficio.SMCCount() > 0)
            {
                // Monta o dicionário para notificação
                Dictionary<string, string> dadosMerge = new Dictionary<string, string>();

                // 1.DSC_SERVICO: buscar a descrição do serviço, do processo correspondente à configuração do
                // processo da solicitação da pessoa-atuação.
                dadosMerge.Add(TOKEN_TAG_NOTIFICACAO.DSC_SERVICO, dadosSolicitacao.DescricaoServico);

                // 2.NOM_PESSOA: buscar o nome social do solicitante em questão, caso exista. Se não existir,
                // buscar o nome do solicitante.
                dadosMerge.Add(TOKEN_TAG_NOTIFICACAO.NOM_PESSOA, !string.IsNullOrEmpty(dadosSolicitacao.NomeSocialSolicitante) ? dadosSolicitacao.NomeSocialSolicitante : dadosSolicitacao.NomeSolicitante);

                // 3.DSC_PROCESSO: buscar a descrição do processo correspondente à configuração do processo da
                // solicitação da pessoa-atuação.
                dadosMerge.Add(TOKEN_TAG_NOTIFICACAO.DSC_PROCESSO, dadosSolicitacao.DescricaoProcesso);

                // 4.DSC_BENEFICIO: buscar os benefícios ATIVOS (situação "DEFERIDO" e dentro do período de
                // vigência) associados à pessoa - atuação em questão.
                // OBS: Caso exista mais de um benefício ativo associado a pessoa-atuação, concatenar de acordo
                // com exemplo: “< dsc_benefício >” + “, < dsc_benefício >”
                string dscBeneficio = string.Empty;
                foreach (var item in beneficio)
                {
                    dscBeneficio += (string.IsNullOrEmpty(dscBeneficio) ? string.Empty : ", ") + item.DescricaoBeneficio;
                }
                dadosMerge.Add(TOKEN_TAG_NOTIFICACAO.DSC_BENEFICIO, dscBeneficio);

                // Envia a notificação
                var parametros = new EnvioNotificacaoSolicitacaoServicoVO()
                {
                    SeqSolicitacaoServico = seqSolicitacaoServico,
                    TokenNotificacao = TOKEN_TIPO_NOTIFICACAO.TRANCAMENTO_MATRICULA_DEFERIDA,
                    DadosMerge = dadosMerge,
                    EnvioSolicitante = false,
                    ConfiguracaoPrimeiraEtapa = false
                };
                SolicitacaoServicoEnvioNotificacaoDomainService.EnviarNotificacaoSolicitacaoServico(parametros);
            }

            // 9. CANCELAR TODAS AS SOLICITAÇÕES DE SERVIÇO ABERTAS DO ALUNO.
            // 9.1.Para cada solicitação de serviço do aluno cuja ÚLTIMA situação tenha seja da categoria "Novo" ou
            // "Em andamento", exceto a que está sendo deferida e as que são de serviços cuja situação "TRANCADO"
            // esteja parametrizada para permitir, ao aluno, a criação, cancelar cada solicitação de acordo com a
            // RN_SRC_025 – Solicitação – Consistências quando cancelada a solicitação, informando o motivo de
            // trancamento com token “MATRICULA_TRANCADA” e a observação: "Solicitação cancelada devido ao deferimento
            // da solicitação de trancamento de matrícula nº <número protocolo da solicitação>".
            var tokenMotivoCancelamentoSolicitacoesAbertas = TOKEN_MOTIVO_SITUACAO_CANCELAMENTO.MATRICULA_TRANCADA;
            var obsTrancamento = string.Format("Solicitação cancelada devido ao deferimento da solicitação de trancamento de matrícula nº {0}.", dadosSolicitacao.NumeroProtocolo);
            this.CancelarTodasSolicitacoesAbertasAluno(seqSolicitacaoServico, dadosSolicitacao.SeqPessoaAtuacao, tokenMotivoCancelamentoSolicitacoesAbertas, obsTrancamento, TOKENS_SITUACAO_MATRICULA.TRANCADO);

            // 10. CHAMAR A ROTINA DE ATUALIZAÇÃO DO SGP.
            // Procedure: st_canc_tranc_aluno_SGP_ACADEMICO
            // Parâmetros:
            // - @ano_ciclo_letivo(smallint): ano do ciclo letivo considerando a data do pedido da solicitação e o
            //   evento que se aplica ao aluno de acordo com as parametrizações por entidade e nível de ensino.
            // - @num_ciclo_letivo(smallint): número do ciclo letivo considerando a data do pedido da solicitação
            //   e o  evento que se aplica ao aluno de acordo com as parametrizações por entidade e nível de ensino.
            // - @cod_aluno_SGP(int): código aluno migração.
            // - @dat_operacao(smalldatetime): data do pedido da solicitação.
            // - @tip_operacao(smallint): 1 – Trancamento
            // - @tip_cancelamento(smallint): 0: Cancelamento Normal.
            // - @usuario(varchar(30)): usuário logado.
            // - @dat_ini_estorno_GRA(smalldatetime): NULL
            // - @dat_fim_estorno_GRA(smalldatetime): NULL
            // - @dsc_justificativa_cancelamento(varchar(255)): NULL
            // - @ind_jubilado(bit): 0 - Não
            // - @dsc_erro(varchar(max)): output
            CancelarTrancarAlunoSGPData model = new CancelarTrancarAlunoSGPData()
            {
                AnoCicloLetivo = dadosSolicitacao.AnoCicloProcesso,
                NumeroCicloLetivo = dadosSolicitacao.NumeroCicloProcesso,
                CodigoAlunoSGP = dadosSolicitacao.CodigoAlunoSGP.GetValueOrDefault(),
                DataOperacao = dadosSolicitacao.DataSolicitacao,
                TipoOperacao = 1, // trancamento
                TipoCancelamento = 0, // cancelamento normal
                Usuario = SMCContext.User.Identity.Name,
                DataInicioEstornoGRA = null,
                DataFimEstornoGRA = null,
                JustificativaCancelamento = null,
                Jubilado = false
            };
            string mensagemFinal = IntegracaoAcademicoService.CancelarTrancarAlunoSGP(model);
        }

        /// <summary>
        /// Cancela uma matricula de um aluno (RN_MAT_083)
        /// </summary>
        /// <param name="modelo">Modelo com os dados para cancelamento</param>
        public string CancelarMatricula(CancelarMatriculaVO modelo)
        {
            // Verifica se informou o ciclo letivo de referencia
            if (!modelo.SeqCicloLetivoReferencia.HasValue)
                throw new CicloLetivoInvalidoException();

            // Busca o ciclo letivo de referencia
            var cicloReferencia = CicloLetivoDomainService.SearchByKey(modelo.SeqCicloLetivoReferencia.Value);

            // Busca o ciclo letivo POSTERIOR a data de referencia
            long? seqCicloPosterior = CicloLetivoDomainService.BuscarProximoCicloLetivo(modelo.SeqCicloLetivoReferencia.Value);

            // Busca a situação com o token informado.
            var specSituacao = new SituacaoMatriculaFilterSpecification() { Token = modelo.TokenSituacaoCancelamento };
            var descricaoSituacao = SituacaoMatriculaDomainService.SearchProjectionByKey(specSituacao, x => x.Descricao);
            if (string.IsNullOrEmpty(descricaoSituacao))
                throw new SituacaoMatriculaInvalidaException();

            // Inicia a transação
            string mensagemFinal = null;
            using (ISMCUnitOfWork tranCancela = SMCUnitOfWork.Begin())
            {
                // 1. Verificar se o aluno possui parcelas vencidas e com a situação em aberto, anterior a data
                // de cancelamento(parâmetro). Se existir, exibir a seguinte mensagem de confirmação:
                // "Atenção! Esse aluno possui pendência financeira com a instituição. Gentileza orientá-lo a
                // procurar a divisão financeira para regularizar sua situação. Deseja realmente efetuar o
                // cancelamento ?"
                // Se o usuário selecionar "NÃO", abortar a operação.
                // Se selecionar "SIM" continuar com as verificações abaixo.
                // [FEITO NO CONTROLLER]

                // 2.AJUSTES NO PLANO DE ESTUDO ATUAL E NO PLANO DE ESTUDOS FUTURO(CASO EXISTA)
                var ajustePlanoEstudoVO = new AjustePlanoEstudoVO()
                {
                    SeqPessoaAtuacao = modelo.SeqPessoaAtuacao,
                    SeqCicloLetivoReferencia = modelo.SeqCicloLetivoReferencia,
                    SeqSolicitacaoServico = modelo.SeqSolicitacaoServico,
                    Observacao = "Plano de estudo sem componente curricular, devido à alteração da situação do aluno"
                };
                PlanoEstudoDomainService.AjustarPlanoEstudo(ajustePlanoEstudoVO);

                // Atualiza o plano com o ciclo letivo posterior a data de referência
                if (seqCicloPosterior.HasValue && seqCicloPosterior.Value > 0)
                {
                    ajustePlanoEstudoVO.SeqCicloLetivoReferencia = seqCicloPosterior;

                    PlanoEstudoDomainService.AjustarPlanoEstudo(ajustePlanoEstudoVO);
                }

                // 4. EXCLUIR AS SITUAÇÕES FUTURAS DO ALUNO
                // Se existir alguma situação de matrícula no ciclo de referencia com data de inicio MAIOR que a data
                // do cancelamento da matrícula (parâmetro) e sem data de exclusão, setar para todas as situações encontradas
                // os valores abaixo:
                // - Data de exclusão: data atual(do sistema).
                // - Usuário de exclusão: usuário logado.
                // - Descrição observação da exclusão: "Excluído devido ao cancelamento de matrícula"
                // - Solicitação de serviço de exclusão: solicitação de serviço(parâmetro)
                var observacaoExclusaoSituacaoFutura = "Excluído devido ao cancelamento de matrícula";
                var situacoesFuturasAlunoCicloReferencia = AlunoHistoricoSituacaoDomainService.BuscarSituacoesFuturasAluno(modelo.SeqPessoaAtuacao, modelo.DataReferencia, modelo.SeqCicloLetivoReferencia);
                foreach (var situacao in situacoesFuturasAlunoCicloReferencia)
                {
                    AlunoHistoricoSituacaoDomainService.ExcluirAlunoHistoricoSituacao(situacao.SeqAlunoHistoricoSituacao, observacaoExclusaoSituacaoFutura, modelo.SeqSolicitacaoServico);
                }

                // 5. EXCLUIR AS SITUAÇÕES DO HISTÓRICO DE CICLO LETIVO POSTERIOR AO CICLO LETIVO ATUAL DO
                // ALUNO, CASO EXISTA.
                // - Data de exclusão: data atual(do sistema).
                // - Usuário de exclusão: usuário logado.
                // - Descrição observação da exclusão: "Excluído devido ao cancelamento de matrícula"
                // - Solicitação de serviço de exclusão: solicitação de serviço(parâmetro)
                if (seqCicloPosterior.HasValue && seqCicloPosterior > 0)
                {
                    // Busca as informações do aluno no ciclo letivo posterior
                    var specAlunoCicloPosterior = new AlunoHistoricoCicloLetivoFilterSpecification()
                    {
                        SeqPessoaAtuacaoAluno = modelo.SeqPessoaAtuacao,
                        SeqCicloLetivo = seqCicloPosterior.Value
                    };
                    var historicoCiclo = AlunoHistoricoCicloLetivoDomainService.SearchProjectionByKey(specAlunoCicloPosterior, x => new
                    {
                        PlanoAtual = x.PlanosEstudo.FirstOrDefault(p => p.Atual),
                        Situacoes = x.AlunoHistoricoSituacao
                    });

                    if (historicoCiclo != null)
                    {
                        // 5.1 Se existir situações no ciclo posterior ao [ciclo letivo], verificar se no ciclo posterior
                        // existe plano de estudo atual. Caso exista, setar o plano de estudo atual como não atual (ind_atual = 0)
                        if (historicoCiclo.Situacoes.Count > 0 && historicoCiclo.PlanoAtual != null)
                        {
                            PlanoEstudo planoDesativado = new PlanoEstudo()
                            {
                                Seq = historicoCiclo.PlanoAtual.Seq,
                                Atual = false
                            };
                            PlanoEstudoDomainService.UpdateFields(planoDesativado, p => p.Atual);
                        }

                        // Excluir as situações do ciclo posterior
                        foreach (var situacao in historicoCiclo.Situacoes)
                        {
                            AlunoHistoricoSituacaoDomainService.ExcluirAlunoHistoricoSituacao(situacao.Seq, observacaoExclusaoSituacaoFutura, modelo.SeqSolicitacaoServico);
                        }
                    }
                }

                // 3. ATUALIZAR A SITUAÇÃO DO ALUNO
                // Incluir novo registro no histórico de situação do aluno, para o ciclo letivo de acordo com a
                // data de cancelamento da matrícula(parâmetro) e o evento que se aplica ao aluno de acordo com
                // as parametrizações por entidade e nível de ensino, com os seguintes valores:
                // - Situação de matrícula = situação com token informado por parâmetro.
                // - Solicitação de matrícula = solicitação de serviço(parâmetro).
                // - Data início da situação = a data do cancelamento da matrícula.
                // - Observação = observação informada(parâmetro)
                // - Arquivo anexado = arquivo anexado ao cancelar matricula
                var historico = new IncluirAlunoHistoricoSituacaoVO()
                {
                    SeqAluno = modelo.SeqPessoaAtuacao,
                    SeqCicloLetivo = modelo.SeqCicloLetivoReferencia,
                    TokenSituacao = modelo.TokenSituacaoCancelamento,
                    SeqSolicitacaoServico = modelo.SeqSolicitacaoServico,
                    DataInicioSituacao = modelo.DataReferencia,
                    Observacao = modelo.ObservacaoSituacaoMatricula,
                    SeqArquivoAnexado = modelo.SeqArquivoAnexado,
                    ArquivoAnexado = modelo.ArquivoAnexado
                };
                AlunoHistoricoSituacaoDomainService.IncluirAlunoHistoricoSituacao(historico);

                // 6.1 Se a Situação de matrícula (parâmetro) for “CANCELADO_MOTIVO_FALECIMENTO“, atualizar
                // no ACADEMICO, DADOS_MESTRES e CAD, o indicador de falecido para "Sim".
                if (modelo.TokenSituacaoCancelamento == TOKENS_SITUACAO_MATRICULA.CANCELADO_MOTIVO_FALECIMENTO)
                {
                    var seqPessoa = PessoaAtuacaoDomainService.SearchProjectionByKey(modelo.SeqPessoaAtuacao, x => x.SeqPessoa);
                    var pessoaFalecida = new Pessoa() { Seq = seqPessoa, Falecido = true };
                    PessoaDomainService.UpdateFields(pessoaFalecida, x => x.Falecido);
                }

                // 6.2 ATUALIZAR O INDICADOR DE ATIVIDADE DA PESSOA-ATUAÇÃO[IMPLEMENTAÇÃO FUTURA – implementar no
                // futuro caso o indicador de atividade da pessoa atuação permaneça]
                // - Atualizar o valor do ind_ativo de acordo com o tipo de situação da situação incluída(item 3).

                // 7. CANCELAR OS BENEFÍCIOS DO ALUNO (Somente se Cancelar Benefício (parâmetro) for 1 - SIM)
                // Se o aluno que está tendo a matrícula cancelada possuir benefício associado que esteja com a
                // situação "DEFERIDO" e dentro do período de vigência, para cada um, incluir no histórico de vigência
                // um registro de acordo com os dados abaixo:
                // - Data início vigência: Data início do período de vigência atual.
                // - Data fim vigência: Último dia do mês de acordo com a data de cancelamento da matrícula(parâmetro)
                // - Descrição da observação: descrição da situação com token informado por parâmetro + “-” +
                // “< número do protocolo da solicitação(parâmetro) >”.
                // Obs.: Caso nenhuma solicitação tenha sido informada(parâmetro opcional) não concatenar o traço e nem o
                // número do protocolo.
                // - Ativo: “Sim”
                // - Motivo alteração do beneficio: Motivo com o token “CANCELAMENTO_MATRICULA” parametrizado por
                // Instituição de Ensino, acordo com a instituição logada.
                if (modelo.CancelarBeneficio)
                {
                    var listaBeneficio = PessoaAtuacaoBeneficioDomainService.BuscarPesssoasAtuacoesBeneficiosMatricula(modelo.SeqPessoaAtuacao, SituacaoChancelaBeneficio.Deferido);
                    if (listaBeneficio.SMCCount() > 0)
                    {
                        // Monta a justificativa
                        string justificativa = descricaoSituacao;
                        if (modelo.SeqSolicitacaoServico.HasValue)
                            justificativa += " - " + modelo.NumeroProtocolo;

                        // Calcula a data de fim do benefício
                        DateTime fimVigencia = new DateTime(modelo.DataReferencia.Year, modelo.DataReferencia.Month, 1).AddMonths(1).AddDays(-1);

                        // Busca o motivo de alteraçao
                        long seqMotivoAlteracaoBeneficio = this.MotivoAlteracaoBeneficioDomainService.BuscarMotivoAlteracaoBeneficioPorToken(TOKEN_MOTIVO_ALTERACAO_BENEFICIO.CANCELAMENTO_MATRICULA);
                        if (seqMotivoAlteracaoBeneficio <= 0)
                            throw new MotivoAlteracaoBeneficioNaoCadastradoException(TOKEN_MOTIVO_ALTERACAO_BENEFICIO.CANCELAMENTO_MATRICULA);

                        foreach (var beneficio in listaBeneficio)
                        {
                            BeneficioHistoricoVigencia novoHistoricoVigencia = new BeneficioHistoricoVigencia()
                            {
                                SeqPessoaAtuacaoBeneficio = beneficio.SeqPessoaAtuacaoBeneficio,
                                DataInicioVigencia = beneficio.DataInicioVigencia,
                                DataFimVigencia = fimVigencia,
                                Observacao = justificativa,
                                SeqMotivoAlteracaoBeneficio = seqMotivoAlteracaoBeneficio,
                                Atual = true
                            };
                            BeneficioHistoricoVigenciaDomainService.SaveEntity(novoHistoricoVigencia);
                        }
                    }
                }

                // 8. CANCELAR TODAS AS SOLICITAÇÕES DE SERVIÇO ABERTAS DO ALUNO.
                // Para cada solicitação de serviço do aluno cuja ÚLTIMA situação tenha seja da categoria “Novo” ou
                // “Em andamento” (exceto a que está sendo deferida caso informada por parâmetro), cancelar a
                // solicitação de acordo com a RN_SRC_025 – Solicitação – Consistências quando cancelada a solicitação,
                // informando o motivo de cancelamento com token “SOLICITACAO_MATRICULA_CANCELADA_MATRICULA_CANCELADA”
                // e a observação:
                // - Se informou o parâmetro de número de solicitação: "Solicitação cancelada devido ao deferimento da
                // solicitação de cancelamento de matrícula nº <Número do protocolo da solicitação (parâmetro)>."
                // - Se o flag de jubilado(parâmetro) for igual a SIM: “Solicitação cancelada devido ao cancelamento
                // da matrícula do aluno por descumprimento de normas acadêmicas”.
                var tokenMotivoCancelamentoSolicitacoesAbertas = TOKEN_MOTIVO_SITUACAO_CANCELAMENTO.MATRICULA_CANCELADA;
                this.CancelarTodasSolicitacoesAbertasAluno(modelo.SeqSolicitacaoServico, modelo.SeqPessoaAtuacao, tokenMotivoCancelamentoSolicitacoesAbertas, modelo.ObservacaoCancelamentoSolicitacao);

                // 9. CHAMAR A ROTINA DE ATUALIZAÇÃO DO SGP.
                // Procedure: st_canc_tranc_aluno_SGP_ACADEMICO
                // Parâmetros:
                // - @ano_ciclo_letivo(smallint): ano do ciclo letivo considerando a data de cancelamento de matrícula
                //   (parâmetro) e o  evento que se aplica ao aluno de acordo com as parametrizações por entidade e
                //   nível de ensino.
                // - @num_ciclo_letivo(smallint): número do ciclo letivo considerando a data de cancelamento da
                //   matrícula e o  evento que se aplica ao aluno de acordo com as parametrizações por entidade e
                //   nível de ensino.
                // - @cod_aluno_SGP(int): código aluno migração.
                // - @dat_operacao(smalldatetime): data de cancelamento da matrícula.
                // - @tip_operacao(smallint): 0 - Cancelamento
                // - @tip_cancelamento(smallint): tipo de cancelamento recebido por parâmetro
                // - @usuario(varchar(30)): usuário logado.
                // - @dat_ini_estorno_GRA(smalldatetime): NULL.
                // - @dat_fim_estorno_GRA(smalldatetime): NULL
                // - @dsc_justificativa_cancelamento(varchar(255)): situação com token informado por parâmetro
                // - @ind_jubilado(bit): flag de jubilamento informado por parâmetro
                // - @dsc_erro(varchar(max)): output
                CancelarTrancarAlunoSGPData model = new CancelarTrancarAlunoSGPData()
                {
                    AnoCicloLetivo = cicloReferencia.Ano,
                    NumeroCicloLetivo = cicloReferencia.Numero,
                    CodigoAlunoSGP = modelo.CodigoAlunoSGP.GetValueOrDefault(),
                    DataOperacao = modelo.DataReferencia,
                    TipoOperacao = 0, // cancelamento
                    TipoCancelamento = modelo.TipoCancelamentoSGP,
                    Usuario = SMCContext.User.Identity.Name,
                    DataInicioEstornoGRA = null,
                    DataFimEstornoGRA = null,
                    JustificativaCancelamento = descricaoSituacao,
                    Jubilado = modelo.Jubilado
                };
                mensagemFinal = IntegracaoAcademicoService.CancelarTrancarAlunoSGP(model);

                // Fim da transação
                tranCancela.Commit();
            }

            // Retorna a mensagem final da rotina do SGP
            return mensagemFinal;
        }

        /// <summary>
        /// Cancelar todas as solicitações de serviço que estão abertas para um aluno
        /// </summary>
        /// <param name="seqSolicitacaoOrigem">Sequencial da solicitação que deu origem ao cancelamento de outras solicitações</param>
        /// <param name="seqAluno">Sequencial do aluno</param>
        /// <param name="tokenMotivoCancelamento">Token motivo cancelamento das solicitações</param>
        /// <param name="observacao">Observação para cancelamento das solicitações</param>
        /// <param name="tokenSituacaoMatricula">Caso a solicitação permita a abertura de solicitações com pelo token da situação de matricula, não cancelar</param>
        private void CancelarTodasSolicitacoesAbertasAluno(long? seqSolicitacaoOrigem, long seqAluno, string tokenMotivoCancelamento, string observacao, string tokenSituacaoMatricula = null)
        {
            // Buscar todas as solicitações do aluno que estão abertas,
            // ou seja, cuja categoria da situação atual seja "Novo" ou "Em andamento".
            var filtroSpec = new SolicitacaoServicoFilterSpecification
            {
                SeqPessoaAtuacao = seqAluno,
                CategoriasSituacoes = new List<CategoriaSituacao> { CategoriaSituacao.EmAndamento, CategoriaSituacao.Novo }
            };
            var solicitacoesAberto = SolicitacaoServicoDomainService.SearchProjectionBySpecification(filtroSpec, x => new
            {
                SeqSolicitacaoServico = x.Seq,
                TokenMotivoCancelamento = tokenMotivoCancelamento,
                Observacao = observacao,
                SituacoesAluno = x.ConfiguracaoProcesso.Processo.Servico.SituacoesAluno.Where(a => a.PermissaoServico == PermissaoServico.CriarSolicitacao).Select(s => s.SituacaoMatricula.Token).ToList()
            });

            // Cancela todas as solicitações encontradas, exeto a solicitação de origem do cancelamento.
            foreach (var solicitacao in solicitacoesAberto.Where(x => x.SeqSolicitacaoServico != (seqSolicitacaoOrigem.HasValue ? seqSolicitacaoOrigem.Value : 0)))
            {
                if (string.IsNullOrEmpty(tokenSituacaoMatricula))
                    SolicitacaoServicoDomainService.SalvarCancelamentoSolicitacao(SMCMapperHelper.Create<CancelamentoSolicitacaoVO>(solicitacao));
                else
                {
                    // Verifica se a solicitação a ser cancelada permite abertura com esse token
                    if (solicitacao.SituacoesAluno.SMCCount() > 0 && !solicitacao.SituacoesAluno.Contains(tokenSituacaoMatricula))
                        SolicitacaoServicoDomainService.SalvarCancelamentoSolicitacao(SMCMapperHelper.Create<CancelamentoSolicitacaoVO>(solicitacao));
                }
            }
        }

        /// <summary>
        /// Verifica se solicitante possui alguma pendencia financeira
        /// </summary>
        /// <param name="codigoAlunoMigracao">Código do aluno a ser validado</param>
        /// <param name="dataVerificacao">Data para validação do nada consta</param>
        /// <returns>TRUE caso exista pendencia financeira e FALSE caso contrário</returns>
        public bool ValidarNadaConstaFinanceiro(int codigoAlunoMigracao, DateTime dataVerificacao)
        {
            /// Verificar se o aluno possui pendência financeira, passando os seguintes parâmetros para o objeto
            /// GRA.dbo.st_nada_consta_ACADEMICO:
            /// - @cod_aluno(int) - código aluno migração
            /// - @seq_origem(int) - 1
            /// - @dat_solicitacao(smalldatetime) - Data de cancelamento da matrícula(parâmetro)
            /// - @nom_usuario_operacao(varchar(60)) - usuário logado
            /// - @dsc_erro(varchar(255)) - output
            /// - @ind_possui_debito(bit) - output--(1 – possui débito, 0 – não possui débito)
            var data = new NadaConstaAcademicoData()
            {
                CodigoAluno = codigoAlunoMigracao,
                SeqOrigem = 1,
                DataSolicitacao = dataVerificacao,
                UsuarioOperacao = SMCContext.User.Identity.Name
            };
            return IntegracaoFinanceiroService.NadaConstaAcademico(data);
        }

        /// <summary>
        /// Verifica se o aluno possui plano de estudo para um determinado ciclo letivo
        /// </summary>
        /// <param name="seqAluno">Sequencial do aluno</param>
        /// <param name="seqCicloletivo">Sequencial do ciclo letivo</param>
        /// <returns>Aluno possui ou não plano de estudo para o ciclo em questão</returns>
        public bool AlunoPossuiPlanoEstudoNoCicloLetivo(long seqAluno, long seqCicloletivo)
        {
            return PlanoEstudoDomainService.Count(new PlanoEstudoFilterSpecification { SeqAluno = seqAluno, SeqCicloLetivo = seqCicloletivo, ComItensNoPlanoDeEstudo = true }) > 0;
        }

        /// <summary>
        /// Busca os alunos com as depêndencias apresentadas na listagem do seu cadastro
        /// </summary>
        /// <param name="filtros">Filtros do ingressante</param>
        /// <returns>Dados paginados de ingressante</returns>
        public SMCPagerData<AlunoListaVO> BuscarAlunos(AlunoFilterSpecification filtros)
        {   ///- Task 47412 Erro ao fazer a pesquisa com mais dados e validar a exibição do botão de associação de beneficio 
            //if (filtros.SeqsSituacaoMatriculaCicloLetivo.SMCAny() && !filtros.SeqCicloLetivoSituacaoMatricula.HasValue)
            //{
            //    return ViewAlunoDomainService.BuscarAlunos(filtros.Transform<ViewAlunoFilterSpecification>(new { SeqsSituacaoMatricula = filtros.SeqsSituacaoMatriculaCicloLetivo }));
            //}

            filtros.SetOrderBy(o => o.DadosPessoais.NomeSocial);
            filtros.SetOrderBy(o => o.DadosPessoais.Nome);

            var lista = this.SearchProjectionBySpecification(filtros, p => new AlunoListaVO()
            {
                Seq = p.Seq,
                CodigoAlunoMigracao = p.CodigoAlunoMigracao,
                SeqNivelEnsino = p.Historicos.FirstOrDefault(f => f.Atual).SeqNivelEnsino,
                SeqTipoVinculoAluno = p.SeqTipoVinculoAluno,
                SeqTipoTermoIntercambio = p.TermosIntercambio.FirstOrDefault().TermoIntercambio.ParceriaIntercambioTipoTermo.SeqTipoTermoIntercambio,
                NumeroRegistroAcademico = p.NumeroRegistroAcademico,
                Nome = string.IsNullOrEmpty(p.DadosPessoais.NomeSocial) ? p.DadosPessoais.Nome : p.DadosPessoais.NomeSocial + " (" + p.DadosPessoais.Nome + ")",
                Cpf = p.Pessoa.Cpf,
                NumeroPassaporte = p.Pessoa.NumeroPassaporte,
                DataNascimento = p.Pessoa.DataNascimento,
                Falecido = p.Pessoa.Falecido,
                DescricaoVinculo = p.TipoVinculoAluno.Descricao,
                DadosVinculo = p.Descricao,
                DescricaoFormaIngresso = p.Historicos.FirstOrDefault(f => f.Atual).FormaIngresso.Descricao,
                DataInicioTermoIntercambio = p.TermosIntercambio.Min(m => m.Periodos.OrderByDescending(x => x.Seq).FirstOrDefault().DataInicio),
                DataFimTermoIntercambio = p.TermosIntercambio.Max(m => m.Periodos.OrderByDescending(x => x.Seq).FirstOrDefault().DataFim),
                DescricaoInstituicaoExterna = p.TermosIntercambio.FirstOrDefault().TermoIntercambio.ParceriaIntercambioInstituicaoExterna.InstituicaoExterna.Nome,
                DescricaoCursoOferta = p.Historicos.FirstOrDefault(f => f.Atual).CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.CursoOferta.Descricao,
                DescricaoLocalidade = p.Historicos.FirstOrDefault(f => f.Atual).CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.HierarquiasEntidades.FirstOrDefault().ItemSuperior.Entidade.Nome,
                DescricaoTurno = p.Historicos.FirstOrDefault(f => f.Atual).CursoOfertaLocalidadeTurno.Turno.Descricao,
                DescricaoTipoTermoIntercambio = p.TermosIntercambio.FirstOrDefault().TermoIntercambio.ParceriaIntercambioTipoTermo.TipoTermoIntercambio.Descricao,
                TipoVinculoAluno = p.TipoVinculoAluno.Descricao,
                PermiteFormacaoEspecifica = p.TipoVinculoAluno.TipoVinculoAlunoFinanceiro != TipoVinculoAlunoFinanceiro.RegimeDisciplinaIsolada,
                VinculoAlunoAtivo = p.Ativo
            }, out int total).ToList();

            var seqsAlunos = lista.Select(s => s.Seq).ToArray();

            var configuracoesVinculos = this.InstituicaoNivelTipoVinculoAlunoDomainService
                .BuscarConfiguracoesVinculos(lista.Select(s => s.SeqNivelEnsino.GetValueOrDefault()).Distinct().ToArray(),
                                             lista.Select(s => s.SeqTipoVinculoAluno).Distinct().ToArray());

            var emailsAlunos = SearchProjectionBySpecification(new AlunoFilterSpecification() { Seqs = seqsAlunos }, p => new
            {
                p.Seq,
                Emails = p.EnderecosEletronicos.Where(w => w.EnderecoEletronico.TipoEnderecoEletronico == TipoEnderecoEletronico.Email)
                          .OrderBy(o => o.EnderecoEletronico.Descricao)
                          .Select(s => s.EnderecoEletronico.Descricao).ToList()
            }).ToList();

            // Verifica se os alunos tem numero de parcelas para habilitar a associação de benefício
            var listaAlunoMigracao = lista.Where(s => s.CodigoAlunoMigracao.HasValue).Select(s => s.CodigoAlunoMigracao.Value).ToList();
            if (listaAlunoMigracao == null || listaAlunoMigracao.Count == 0)
                listaAlunoMigracao.Add(0); // Caso a lista esteja vazia (não encontrou nenhum aluno), pesquisa pelo aluno de cod-aluno = 0 para não dar erro na pesuisa do GRA
            var parcelas = FinanceiroService.BuscarDataVencimentoBeneficios(listaAlunoMigracao.ToArray());

            foreach (var aluno in lista)
            {
                var configuracaoVinculo = configuracoesVinculos
                    .FirstOrDefault(f => f.InstituicaoNivel.SeqNivelEnsino == aluno.SeqNivelEnsino.GetValueOrDefault()
                                      && f.SeqTipoVinculoAluno == aluno.SeqTipoVinculoAluno);
                // RN_ALN_029 - Descrição Vínculo Tipo Termo Intercâmbio
                aluno.DescricaoVinculo = RecuperarVinculoAluno(configuracaoVinculo, aluno.SeqTipoTermoIntercambio,
                                                                                    aluno.DescricaoVinculo,
                                                                                    aluno.DescricaoTipoTermoIntercambio);
                aluno.ExigeParceriaIntercambioIngresso = configuracaoVinculo.ExigeParceriaIntercambioIngresso;
                aluno.ExigePeriodoIntercambioTermo = configuracaoVinculo.TiposTermoIntercambio.FirstOrDefault(f => f.SeqTipoTermoIntercambio == aluno.SeqTipoTermoIntercambio)?.ExigePeriodoIntercambioTermo ?? false;
                aluno.Emails = emailsAlunos.FirstOrDefault(f => f.Seq == aluno.Seq)?.Emails;

                var pessoaAtuacaoIntercambioSpec = new PessoaAtuacaoTermoIntercambioFilterSpecification { SeqNivelEnsino = aluno.SeqNivelEnsino, SeqPessoaAtuacao = aluno.Seq, SeqTipoVinculo = aluno.SeqTipoVinculoAluno };
                var pessoaAtuacaoIntercambio = PessoaAtuacaoTermoIntercambioDomainService.SearchProjectionBySpecification(pessoaAtuacaoIntercambioSpec, x => x.SeqTermoIntercambio).ToArray();

                var termoIntercambioSpec = new TermoIntercambioFilterSpecification { SeqsTermosIntercambio = pessoaAtuacaoIntercambio };
                var termoIntercambio = TermoIntercambioDomainService.SearchProjectionBySpecification(termoIntercambioSpec, x => x.SeqParceriaIntercambioTipoTermo).ToArray();

                var parceriaTipoTermoSpec = new ParceriaIntercambioTipoTermoFilterSpecification { SeqsParceriaIntercambioTipoTermo = termoIntercambio };
                var parceriaTipoTermo = ParceriaIntercambioTipoTermoDomainService.SearchBySpecification(parceriaTipoTermoSpec);

                var tipoTermoConcedeFormacao = false;
                foreach (var item in parceriaTipoTermo)
                {
                    var concedeFormacao = TipoTermoIntercambioDomainService.TipoTermoIntercambioConcedeFormacao(item.SeqTipoTermoIntercambio, aluno.SeqTipoVinculoAluno, aluno.SeqNivelEnsino.Value, configuracaoVinculo.InstituicaoNivel.SeqInstituicaoEnsino);
                    if (concedeFormacao)
                        tipoTermoConcedeFormacao = true;
                }
                aluno.ConcedeFormacao = tipoTermoConcedeFormacao;

                var nivelEnsino = NivelEnsinoDomainService.SearchByKey(new SMCSeqSpecification<NivelEnsino>(aluno.SeqNivelEnsino.Value));

                var situacoesMatricula = filtros.SeqCicloLetivoSituacaoMatricula.HasValue && !nivelEnsino.Token.Equals("ESPECIALIZACAO") ?
                    AlunoHistoricoSituacaoDomainService.SearchProjectionBySpecification(new AlunoHistoricoSituacaoFilterSpecification()
                    {
                        SeqsPessoaAtuacaoAluno = seqsAlunos,
                        SeqCicloLetivo = filtros.SeqCicloLetivoSituacaoMatricula,
                        SeqsSituacaoMatricula = filtros.SeqsSituacaoMatriculaCicloLetivo,
                        Atual = true
                    }, p => new AlunoListaSituacaoMatriculaVO()
                    {
                        Seq = p.AlunoHistoricoCicloLetivo.AlunoHistorico.SeqAluno,
                        SeqSituacaoMatricula = p.SeqSituacaoMatricula,
                        DescricaoSituacaoMatricula =
                            p.AlunoHistoricoCicloLetivo.CicloLetivo.Numero + "/" +
                            p.AlunoHistoricoCicloLetivo.CicloLetivo.Ano + " - " +
                            p.SituacaoMatricula.Descricao,
                        AnoNumeroCicloLetivo = p.AlunoHistoricoCicloLetivo.CicloLetivo.AnoNumeroCicloLetivo
                    }).ToList() :
                    ViewAlunoDomainService.SearchProjectionBySpecification(new ViewAlunoFilterSpecification() { Seqs = seqsAlunos }, p => new AlunoListaSituacaoMatriculaVO()
                    {
                        Seq = p.SeqPessoaAtuacao,
                        SeqSituacaoMatricula = p.SeqSituacaoMatricula,
                        DescricaoSituacaoMatricula = p.DescricaoCicloLetivoSituacaoMatricula,
                        AnoNumeroCicloLetivo = p.AnoNumeroCicloLetivo
                    }).ToList();

                var situacaoMatricula = situacoesMatricula.First(f => f.Seq == aluno.Seq);
                aluno.DescricaoSituacaoMatricula = situacaoMatricula.DescricaoSituacaoMatricula;
                aluno.SeqSituacaoMatricula = situacaoMatricula.SeqSituacaoMatricula;
                aluno.AnoNumeroCicloLetivo = situacaoMatricula.AnoNumeroCicloLetivo;

                aluno.PermiteCancelarMatricula = SituacaoMatriculaDomainService.SearchProjectionByKey(aluno.SeqSituacaoMatricula,
                                                    x => x.TipoSituacaoMatricula.Token != TOKENS_TIPO_SITUACAO_MATRICULA.FORMADO &&
                                                         x.TipoSituacaoMatricula.Token != TOKENS_TIPO_SITUACAO_MATRICULA.CANCELADO &&
                                                         x.TipoSituacaoMatricula.Token != TOKENS_TIPO_SITUACAO_MATRICULA.TRANSFERIDO);

                aluno.VinculoInstituicaoNivelEnsinoExigeCurso = configuracaoVinculo.ExigeCurso;

                if (parcelas != null)
                {
                    aluno.PermiteAssociacaoBeneficio = parcelas.Any(a => a.CodigoAluno == aluno.CodigoAlunoMigracao);
                }
                else
                    aluno.PermiteAssociacaoBeneficio = false;
            }

            return new SMCPagerData<AlunoListaVO>(lista.OrderBy(o => o.Nome).ThenByDescending(o => o.AnoNumeroCicloLetivo), total);
        }

        /// <summary>
        /// RN_ALN_059 - Cálculo do dígito verificador do código de migração
        /// </summary>
        /// <param name="codigoAluno">Código de migração do aluno</param>
        /// <returns>MOD11 do código de migração do aluno</returns>
        private char CalcularCodigoMigracaoDV(int codigoAluno)
        {
            var strCodigoAluno = codigoAluno.ToString();
            int total = 0;
            for (int i = 0, m = strCodigoAluno.Length + 1; i < strCodigoAluno.Length; i++, m--)
            {
                total += m * (strCodigoAluno[i] - '0');
            }
            Math.DivRem(total, 11, out int result);

            if (result == 1)
            {
                return 'X';
            }
            else if (result == 0)
            {
                return '0';
            }
            return (char)('0' + 11 - result);
        }

        /// <summary>
        /// Recupera os dados do aluno para abertura de chamados de suporte técnico
        /// </summary>
        /// <param name="seqAluno">Sequencial da pessoa atuação do aluno</param>
        /// <returns>Dados do aluno para abertura do chamado de suporte técnico</returns>
        public AlunoDadosSuporteTecnicoVO BuscarDadosSuporteTecnico(long seqAluno)
        {
            var spec = new SMCSeqSpecification<Aluno>(seqAluno);
            var dados = this.SearchProjectionByKey(spec, a => new AlunoDadosSuporteTecnicoVO()
            {
                NumeroRegistroAcademico = a.NumeroRegistroAcademico,
                DescricaoPessoaAtuacao = a.Descricao
            });

            var dadosOrigem = PessoaAtuacaoDomainService.RecuperaDadosOrigem(seqAluno);

            var specCursoOfertaLocalidadeTurno = new SMCSeqSpecification<CursoOfertaLocalidadeTurno>(dadosOrigem.SeqCursoOfertaLocalidadeTurno);
            var dadosCursoOfertaLocalidadeTurno = CursoOfertaLocalidadeTurnoDomainService.SearchProjectionByKey(specCursoOfertaLocalidadeTurno, x => new
            {
                NomeCurso = x.CursoOfertaLocalidade.CursoOferta.Curso.Nome,
                DescricaoTurno = x.Turno.Descricao,
                DescricaoUnidade = x.CursoOfertaLocalidade.CursoUnidade.HierarquiasEntidades.FirstOrDefault().ItemSuperior.Entidade.Nome,
                x.CursoOfertaLocalidade.CursoUnidade.HierarquiasEntidades.FirstOrDefault().ItemSuperior.Entidade.CodigoUnidadeSeo,
                x.CursoOfertaLocalidade.SeqOrigemFinanceira
            });

            dados.NomeCurso = dadosCursoOfertaLocalidadeTurno.NomeCurso;
            dados.DescricaoTurno = dadosCursoOfertaLocalidadeTurno.DescricaoTurno;
            dados.DescricaoUnidade = dadosCursoOfertaLocalidadeTurno.DescricaoUnidade;
            dados.CodigoUnidadeSeoLocalidade = dadosCursoOfertaLocalidadeTurno.CodigoUnidadeSeo;
            dados.CodigoOrigemFinanceira = dadosCursoOfertaLocalidadeTurno.SeqOrigemFinanceira; //dadosOrigem.SeqOrigem (só retorna origem = 1 se aluno)

            return dados;
        }

        /// <summary>
        /// Recupera o código de estabelecimento de um aluno 
        /// </summary>
        /// <param name="seqAluno">Sequencial da pessoa atuação do aluno</param>
        /// <returns>Recupera o código de estabelecimento de um aluno</returns>
        public string BuscarCodigoEstabelecimentoAluno(long seqAluno)
        {
            string codigoEstabelecimento = string.Empty;
            var dadosOrigem = PessoaAtuacaoDomainService.RecuperaDadosOrigem(seqAluno);

            var specCursoOfertaLocalidade = new SMCSeqSpecification<CursoOfertaLocalidade>(dadosOrigem.SeqCursoOfertaLocalidade);
            var codigoUnidadeSeoLocalidade = CursoOfertaLocalidadeDomainService.SearchProjectionByKey(specCursoOfertaLocalidade, x => x.CursoUnidade.HierarquiasEntidades.FirstOrDefault().ItemSuperior.Entidade.CodigoUnidadeSeo);

            if (codigoUnidadeSeoLocalidade.HasValue)
            {
                var unidade = EstruturaOrganizacionalService.BuscarUnidadePorId(codigoUnidadeSeoLocalidade.Value);

                if (unidade != null)
                {
                    codigoEstabelecimento = unidade.CodigoEstabelecimentoEms;
                }
            }

            return codigoEstabelecimento;
        }

        /// <summary>
        /// Chama a procedure ALN.st_carga_sincronismo_aluno_graduacao para sincronizar os dados do aluno
        /// </summary>
        /// <param name="codigoAlunoMigracao">Código do aluno migração a ser sincronizado</param>
        /// <param name="seqPessoaAtuacao">Sequencial da pessoa atuacao</param>
        /// <returns>Sequencial da pessoa atuação que foi sincronizada</returns>
        public long SincronizarDadosAluno(long codigoAlunoMigracao, long seqPessoaAtuacao)
        {
            var spec = new AlunoFilterSpecification() { CodigoAlunoMigracao = codigoAlunoMigracao };
            var alunosComCodigoMigracao = this.SearchBySpecification(spec).ToList();

            //Validando se existe mais de um cadastro de aluno com o codigo de migração enviado
            if (alunosComCodigoMigracao.Any(a => a.Seq != seqPessoaAtuacao))
                throw new SolicitacaoDocumentoConclusaoCodigoMigracaoDuplicadoException();

            var usuarioOperacao = string.Empty;
            var sequencialUsuario = SMCContext.User?.SMCGetSequencialUsuario();
            var nomeReduzido = SMCContext.User?.SMCGetNomeReduzido();

            if (sequencialUsuario != null && nomeReduzido != null)
            {
                usuarioOperacao = $"{sequencialUsuario}/{nomeReduzido.ToUpper()}";
            }
            else
            {
                usuarioOperacao = SMCContext.User?.Identity?.Name;
            }

            return AcademicoRepository.SincronizarDadosAluno((int)codigoAlunoMigracao, usuarioOperacao);
        }

        public long SincronizarDadosALunoSomenteComCodigoMigracao(int codigoAlunoMigracao)
        {
            var spec = new AlunoFilterSpecification() { CodigoAlunoMigracao = codigoAlunoMigracao };
            var alunosComCodigoMigracao = this.SearchBySpecification(spec).ToList();

            //Validando se existe mais de um cadastro de aluno com o codigo de migração enviado
            if (alunosComCodigoMigracao.Count() > 1)
                throw new SolicitacaoDocumentoConclusaoCodigoMigracaoDuplicadoException();

            var usuarioOperacao = string.Empty;
            var sequencialUsuario = SMCContext.User?.SMCGetSequencialUsuario();
            var nomeReduzido = SMCContext.User?.SMCGetNomeReduzido();

            if (sequencialUsuario != null && nomeReduzido != null)
            {
                usuarioOperacao = $"{sequencialUsuario}/{nomeReduzido.ToUpper()}";
            }
            else
            {
                usuarioOperacao = SMCContext.User?.Identity?.Name;
            }

            return AcademicoRepository.SincronizarDadosAluno((int)codigoAlunoMigracao, usuarioOperacao);
        }

        public List<PrevisaoConclusaoOrientacaoRelatorioVO> BuscarPrevisaoConclusaoOrientacaoRelatorio(AlunoFilterSpecification filtros)
        {
            #region Implementação antiga feita com SearchProjectionBySpecification (erro por não retornar dados, problema com filtro de dados)

            ////var testeLista = this.SearchBySpecification(filtros).ToList();

            //filtros.TokenTipoOrientacao = TOKEN_TIPO_ORIENTACAO.ORIENTACAO_CONCLUSAO_CURSO;

            //var lista = this.SearchProjectionBySpecification(filtros, s => new
            //{
            //    s.Seq,
            //    SeqEntidade = s.Historicos.FirstOrDefault(f => f.Atual).CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.CursoOferta.Curso.HierarquiasEntidades.FirstOrDefault().ItemSuperior.Seq,
            //    DescricaoEntidade = s.Historicos.FirstOrDefault(f => f.Atual).CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.CursoOferta.Curso.HierarquiasEntidades.FirstOrDefault().ItemSuperior.Entidade.Nome,
            //    Nome = string.IsNullOrEmpty(s.DadosPessoais.NomeSocial) ? s.DadosPessoais.Nome : s.DadosPessoais.NomeSocial + " (" + s.DadosPessoais.Nome + ")",
            //    s.Historicos.FirstOrDefault(f => f.Atual).DataAdmissao,
            //    s.Historicos.FirstOrDefault(f => f.Atual).PrevisoesConclusao.OrderByDescending(o => o.DataInclusao).FirstOrDefault().DataPrevisaoConclusao,
            //    s.Historicos.FirstOrDefault(f => f.Atual).PrevisoesConclusao.OrderByDescending(o => o.DataInclusao).FirstOrDefault().DataLimiteConclusao,
            //    s.Historicos.FirstOrDefault(f => f.Atual).SeqNivelEnsino,
            //    DescricaoNivel = s.Historicos.FirstOrDefault(f => f.Atual).NivelEnsino.Descricao,
            //    s.SeqTipoVinculoAluno,
            //    DescricaoVinculo = s.TipoVinculoAluno.Descricao,
            //    SeqTipoTermoIntercambio = (long?)s.TermosIntercambio.OrderByDescending(o => o.Seq).FirstOrDefault().TermoIntercambio.ParceriaIntercambioTipoTermo.SeqTipoTermoIntercambio,
            //    DescricaoTipoTermoIntercambio = s.TermosIntercambio.FirstOrDefault().TermoIntercambio.ParceriaIntercambioTipoTermo.TipoTermoIntercambio.Descricao,

            //    Orientador = s.OrientacoesPessoaAtuacao.Where(w => w.Orientacao.TipoOrientacao.Token == filtros.TokenTipoOrientacao).SelectMany(sm => sm.Orientacao.OrientacoesColaborador).Select(so => new
            //    {
            //        so.SeqColaborador,
            //        NomeColaborador = so.Colaborador.DadosPessoais.Nome,
            //        so.TipoParticipacaoOrientacao,
            //        so.DataInicioOrientacao,
            //        so.DataFimOrientacao
            //    }).OrderBy(o => o.NomeColaborador).ToList()

            //}).OrderBy(oa => oa.Nome).ToList();

            //var retorno = new List<PrevisaoConclusaoOrientacaoRelatorioVO>();
            //var entidades = lista.GroupBy(g => g.SeqEntidade).OrderBy(o => o.Key).ToList();

            //foreach (var entidade in entidades)
            //{
            //    var relatorio = new PrevisaoConclusaoOrientacaoRelatorioVO
            //    {
            //        SeqEntidade = entidade.Key,
            //        Alunos = new List<PrevisaoConclusaoOrientacaoRelatorioAlunosVO>()
            //    };

            //    foreach (var item in entidade)
            //    {
            //        var configuracaoVinculo = this.InstituicaoNivelTipoVinculoAlunoDomainService.BuscarConfiguracaoVinculo(item.SeqNivelEnsino, item.SeqTipoVinculoAluno);

            //        var dadosSituacaoMatricula = AlunoHistoricoSituacaoDomainService.BuscarSituacaoAtualAluno(item.Seq);
            //        var descCicloLetivo = this.CicloLetivoDomainService.SearchProjectionByKey(dadosSituacaoMatricula.SeqCiclo, p => p.Descricao);

            //        foreach (var orientador in item.Orientador)
            //        {
            //            var aluno = new PrevisaoConclusaoOrientacaoRelatorioAlunosVO
            //            {
            //                SeqEntidade = item.SeqEntidade,
            //                SeqAluno = item.Seq,
            //                Nome = item.Nome,
            //                DescricaoNivel = item.DescricaoNivel,
            //                DataAdmissao = item.DataAdmissao.SMCDataAbreviada(),
            //                DataPrevisaoConclusao = item.DataPrevisaoConclusao.SMCDataAbreviada(),
            //                DataLimiteConclusao = item.DataLimiteConclusao.SMCDataAbreviada(),
            //                DescricaoSituacaoMatricula = $"{descCicloLetivo} - {dadosSituacaoMatricula.Descricao}",
            //                DescricaoVinculo = RecuperarVinculoAluno(configuracaoVinculo,
            //                                                     item.SeqTipoTermoIntercambio,
            //                                                     item.DescricaoVinculo,
            //                                                     item.DescricaoTipoTermoIntercambio),
            //                NomeColaborador = $"{orientador.NomeColaborador} ({orientador.TipoParticipacaoOrientacao})",
            //                DataInicioOrientacao = orientador.DataInicioOrientacao.SMCDataAbreviada(),
            //                DataFimOrientacao = orientador.DataFimOrientacao.HasValue ? orientador.DataFimOrientacao.SMCDataAbreviada() : "-"
            //            };

            //            relatorio.Alunos.Add(aluno);
            //        }

            //        if (!item.Orientador.Any())
            //        {
            //            var aluno = new PrevisaoConclusaoOrientacaoRelatorioAlunosVO
            //            {
            //                SeqEntidade = item.SeqEntidade,
            //                SeqAluno = item.Seq,
            //                Nome = item.Nome,
            //                DescricaoNivel = item.DescricaoNivel,
            //                DataAdmissao = item.DataAdmissao.SMCDataAbreviada(),
            //                DataPrevisaoConclusao = item.DataPrevisaoConclusao.SMCDataAbreviada(),
            //                DataLimiteConclusao = item.DataLimiteConclusao.SMCDataAbreviada(),
            //                DescricaoSituacaoMatricula = $"{descCicloLetivo} - {dadosSituacaoMatricula.Descricao}",
            //                DescricaoVinculo = RecuperarVinculoAluno(configuracaoVinculo,
            //                                                     item.SeqTipoTermoIntercambio,
            //                                                     item.DescricaoVinculo,
            //                                                     item.DescricaoTipoTermoIntercambio),

            //                NomeColaborador = "-",
            //                DataInicioOrientacao = "-",
            //                DataFimOrientacao = "-"
            //            };

            //            relatorio.Alunos.Add(aluno);
            //        }

            //        relatorio.DescricaoEntidade = item.DescricaoEntidade;
            //    }

            //    retorno.Add(relatorio);
            //}

            //return retorno;

            #endregion

            filtros.SeqInstituicaoEnsino = null;
            filtros.TokenTipoOrientacao = TOKEN_TIPO_ORIENTACAO.ORIENTACAO_CONCLUSAO_CURSO;

            var queryRelatorioPrevisaoConclusao = QUERY_RELATORIO_PREVISAO_CONCLUSAO;

            if (filtros.SeqCicloLetivoIngresso.HasValue)
            {
                queryRelatorioPrevisaoConclusao += $"and ah.seq_ciclo_letivo = {filtros.SeqCicloLetivoIngresso.Value}";
            }

            if (filtros.SeqEntidadesResponsaveis != null && filtros.SeqEntidadesResponsaveis.Any())
            {
                queryRelatorioPrevisaoConclusao += $@"and ah.seq_entidade_vinculo in ({string.Join(",", filtros.SeqEntidadesResponsaveis)})";
            }

            if (filtros.SeqNivelEnsino.HasValue)
            {
                queryRelatorioPrevisaoConclusao += $@"and ah.seq_nivel_ensino = {filtros.SeqNivelEnsino.Value}";
            }

            if (filtros.SeqsTipoVinculoAluno != null && filtros.SeqsTipoVinculoAluno.Any())
            {
                queryRelatorioPrevisaoConclusao += $"and al.seq_tipo_vinculo_aluno in ({string.Join(",", filtros.SeqsTipoVinculoAluno)})";
            }

            if (filtros.PrazoEncerrado.HasValue)
            {
                queryRelatorioPrevisaoConclusao += $"and CONVERT(date, pc.dat_previsao_conclusao) = '{filtros.PrazoEncerrado.Value.ToString("yyyy-MM-dd")}'";
            }

            queryRelatorioPrevisaoConclusao += @"order by 
                                                 e.nom_entidade,
	                                             isnull(dp.nom_social, dp.nom_pessoa), 
	                                             ori.nom_pessoa";

            var lista = this.RawQuery<PrevisaoConclusaoOrientacaoRelatorioAuxiliarDadosVO>(queryRelatorioPrevisaoConclusao);

            var retorno = new List<PrevisaoConclusaoOrientacaoRelatorioVO>();

            var entidades = lista.GroupBy(g => g.SeqEntidade).OrderBy(o => o.Key).ToList();

            foreach (var entidade in entidades)
            {
                var relatorio = new PrevisaoConclusaoOrientacaoRelatorioVO
                {
                    SeqEntidade = entidade.Key,
                    Alunos = new List<PrevisaoConclusaoOrientacaoRelatorioAlunosVO>()
                };

                foreach (var item in entidade)
                {
                    var configuracaoVinculo = this.InstituicaoNivelTipoVinculoAlunoDomainService.BuscarConfiguracaoVinculo(item.SeqNivelEnsino, item.SeqTipoVinculoAluno);

                    var aluno = new PrevisaoConclusaoOrientacaoRelatorioAlunosVO
                    {
                        SeqEntidade = item.SeqEntidade,
                        SeqAluno = item.Seq,
                        Nome = item.Nome,
                        DescricaoNivel = item.DescricaoNivel,
                        DataAdmissao = item.DataAdmissao.SMCDataAbreviada(),
                        DataPrevisaoConclusao = item.DataPrevisaoConclusao.SMCDataAbreviada(),
                        DataLimiteConclusao = item.DataLimiteConclusao.SMCDataAbreviada(),
                        DescricaoSituacaoMatricula = $"{item.DescricaoCicloLetivoSituacao} - {item.DescricaoSituacaoMatricula}",
                        DescricaoVinculo = RecuperarVinculoAluno(configuracaoVinculo,
                                                             item.SeqTipoTermoIntercambio,
                                                             item.DescricaoVinculo,
                                                             item.DescricaoTipoTermoIntercambio),
                        NomeColaborador = !string.IsNullOrEmpty(item.NomeColaborador) ? $"{item.NomeColaborador} ({item.TipoParticipacaoOrientacao})" : "-",
                        DataInicioOrientacao = item.DataInicioOrientacao.HasValue ? item.DataInicioOrientacao.SMCDataAbreviada() : "-",
                        DataFimOrientacao = item.DataFimOrientacao.HasValue ? item.DataFimOrientacao.SMCDataAbreviada() : "-"
                    };

                    relatorio.Alunos.Add(aluno);
                    relatorio.DescricaoEntidade = item.DescricaoEntidade;
                }

                retorno.Add(relatorio);
            }

            return retorno;
        }

        public void EnviarDeclaracaoGenericaParaAssinatura(ConfigurarRelatorioDeclaracaoGenericaVO dados)
        {
            try
            {
                var instituicaoNivelTipoDocumentoAcademico = InstituicaoNivelTipoDocumentoAcademicoDomainService.BuscarConfiguracoesInstituicaoNivelTipoDocumentoAcademico(dados.SeqNivelEnsinoPorGrupoDocumentoAcademico, dados.SeqTipoDocumentoAcademico, dados.IdiomaDocumentoAcademico);
                if (!instituicaoNivelTipoDocumentoAcademico.SeqConfiguracaoGad.HasValue)
                    throw new InstituicaoNivelTipoDocumentoAcademicoConfiguracaoGADException();

                var bytesArquivo = BuscarRelatorioGenerico(dados);

                var emailsAlunos = SearchProjectionBySpecification(new AlunoFilterSpecification() { Seq = dados.SeqAluno }, p => new
                {
                    Emails = p.EnderecosEletronicos.Where(w => w.EnderecoEletronico.TipoEnderecoEletronico == TipoEnderecoEletronico.Email)
                          .OrderBy(o => o.EnderecoEletronico.Descricao)
                          .Select(s => s.EnderecoEletronico.Descricao).ToList()
                }).ToList();

                var emails = new StringBuilder();
                var listaEmail = emailsAlunos.FirstOrDefault()?.Emails ?? Enumerable.Empty<string>();

                foreach (var email in listaEmail)
                    emails.Append(email.Trim()).Append(",");

                if (emails.Length > 0)
                    emails.Length--;

                var emailsString = emails.ToString();

                var configuracao = new ConfiguracaoDocumentoGadVO()
                {
                    SeqConfiguracaoGad = instituicaoNivelTipoDocumentoAcademico.SeqConfiguracaoGad.Value,
                    TokenSistemaOrigemGAD = instituicaoNivelTipoDocumentoAcademico.TokenSistemaOrigemGAD,
                    NomeDocumento = $"{instituicaoNivelTipoDocumentoAcademico.DescricaoTipoDocumentoAcademico} - {dados.CodigoAlunoMigracao}",
                    NomeArquivo = $"{instituicaoNivelTipoDocumentoAcademico.DescricaoTipoDocumentoAcademico}_{dados.CodigoAlunoMigracao}.pdf",
                    Conteudo = bytesArquivo,
                    ExigeAprovacao = Convert.ToBoolean(ConfigurationManager.AppSettings["ExigeAprovacaoDeclaracaoGenericaParaAssinatura"]),
                    HabilitaNotificacao = Convert.ToBoolean(ConfigurationManager.AppSettings["HabilitaNotificacaoDeclaracaoGenericaParaAssinatura"]),
                    IndicadorRastreabilidade = Convert.ToBoolean(ConfigurationManager.AppSettings["IndicadorRastreabilidadeDeclaracaoGenericaParaAssinatura"]),
                    EmailNotificarAssinatura = emails.ToString(),
                    ParticipantesComTag = BuscarAssinantesDeclaracaoGenerica(dados.SeqAluno, dados.CodigoUnidadeSeo, dados.SeqNivelEnsinoPorGrupoDocumentoAcademico, instituicaoNivelTipoDocumentoAcademico.SeqConfiguracaoGad.Value),
                    Tags = new List<TagTokenVO>()
                    {
                        new TagTokenVO { Token = "COD_ALUNO", Descricao = dados.CodigoAlunoMigracao.ToString() },
                        new TagTokenVO { Token = "NOME_ALUNO", Descricao = dados.NomeAlunoOficial },
                        new TagTokenVO { Token = "TIPO_DOCUMENTO", Descricao = instituicaoNivelTipoDocumentoAcademico.DescricaoTipoDocumentoAcademico }
                    }
                };

                var tagExpiracaoDoc = dados.Tags.Where(w => w.DescricaoTag == TagsDeclaracaoGenerica.EXPIRACAO_DOC).ToList();
                if (tagExpiracaoDoc != null && tagExpiracaoDoc.Any())
                    configuracao.DataValidade = DateTime.Parse(tagExpiracaoDoc.FirstOrDefault().Valor);

                var seqDocumentoGad = GerarDocumentoGadPorConfiguracao(configuracao);

                var declaracaoGenerica = new DeclaracaoGenericaVO
                {
                    SeqTipoDocumentoAcademico = dados.SeqTipoDocumentoAcademico,
                    NumeroViaDocumento = 1,
                    SeqDocumentoGAD = seqDocumentoGad,
                    SeqPessoaAtuacao = dados.SeqAluno,
                    SeqPessoaDadosPessoais = dados.SeqDadosPessoais
                };
                var seqDeclaracaoGenerica = DeclaracaoGenericaDomainService.SalvarDeclaracaoGenerica(declaracaoGenerica);
                DocumentoAcademicoHistoricoSituacaoDomainService.InserirHistoricoAguardandoAssinatura(seqDeclaracaoGenerica);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private List<ParticipantesTagVO> BuscarAssinantesDeclaracaoGenerica(long seqAluno, int? codigoUnidadeSeo, long seqNivelEnsino, long seqConfiguracaoGad)
        {
            var participantes = new List<ParticipantesTagVO>();
            var tagsGAD = this.ConfiguracaoDocumentoService.BuscarTagsConfiguracaoDocumento(seqConfiguracaoGad);

            var listaTagsGAD = tagsGAD.Where(w => !w.TemParticipante).Select(s => s.TagPosicaoAssinatura).ToList();

            ValidarTags(listaTagsGAD);

            foreach (var token in listaTagsGAD)
            {
                if (token == TOKEN_TIPO_FUNCIONARIO.SECRETARIO)
                {
                    var tokenNivelEnsino = NivelEnsinoDomainService.SearchProjectionByKey(new SMCSeqSpecification<NivelEnsino>(seqNivelEnsino), x => x.Token);
                    if (tokenNivelEnsino == TOKEN_NIVEL_ENSINO.GRADUACAO)
                    {
                        if (codigoUnidadeSeo == UNIDADE_SEO.CODIGO_PUC_CORACAO_EUCARISTICO)
                        {
                            var spec = new AlunoFilterSpecification() { Seq = seqAluno };
                            var dados = this.SearchProjectionByKey(spec, s => new
                            {
                                seq = s.Historicos.FirstOrDefault(f => f.Atual).CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.CursoOferta.SeqCurso,
                            });
                            var seqEntidadehierarquia = HierarquiaEntidadeDomainService.BuscarEntidadeSuperior(dados.seq, TipoVisao.VisaoOrganizacional);
                            var participanteComVinculoUnico = FuncionarioVinculoDomainService.BuscarFuncionariosEntidadeVinculo(seqEntidadehierarquia, token);
                            participantes.AddRange(participanteComVinculoUnico);
                        }
                        else
                        {
                            var spec = new AlunoFilterSpecification() { Seq = seqAluno };
                            var dados = this.SearchProjectionByKey(spec, s => new
                            {
                                s.Historicos.FirstOrDefault(f => f.Atual).CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.CursoUnidade.HierarquiasEntidades.FirstOrDefault().ItemSuperior.Entidade.Seq,
                            });

                            var participanteComVinculoUnico = FuncionarioVinculoDomainService.BuscarFuncionariosEntidadeVinculo(dados.Seq, token);
                            participantes.AddRange(participanteComVinculoUnico);
                        }

                    }
                    else if (tokenNivelEnsino == TOKEN_NIVEL_ENSINO.MESTRADO_ACADEMICO ||
                             tokenNivelEnsino == TOKEN_NIVEL_ENSINO.MESTRADO_PROFISSIONAL ||
                             tokenNivelEnsino == TOKEN_NIVEL_ENSINO.DOUTORADO_ACADEMICO)
                    {
                        var spec = new AlunoFilterSpecification() { Seq = seqAluno };
                        var dados = this.SearchProjectionByKey(spec, s => new
                        {
                            s.Historicos.FirstOrDefault(f => f.Atual).EntidadeVinculo.Seq
                        });
                        var participanteComVinculoUnico = FuncionarioVinculoDomainService.BuscarFuncionariosEntidadeVinculo(dados.Seq, token);
                        participantes.AddRange(participanteComVinculoUnico);
                    }
                }
                else
                {
                    var specTipoFuncionario = new TipoFuncionarioFilterSpecification() { Token = token };
                    var tipoFuncionario = this.TipoFuncionarioDomainService.SearchByKey(specTipoFuncionario);

                    if (tipoFuncionario == null)
                        throw new FuncionarioNaoEncontradoException();

                    if (tipoFuncionario.ObrigatorioVinculoUnico)
                    {
                        var participanteComVinculoUnico = FuncionarioVinculoDomainService.BuscarFuncionarioComVinculoUnico(token);
                        participantes.Add(participanteComVinculoUnico);
                    }
                }
            }

            ValidarParticipantes(participantes, listaTagsGAD);

            return participantes;
        }

        private void ValidarTags(List<string> listaTagsGAD)
        {
            if (listaTagsGAD.Any())
            {
                var tipoVinculoFuncionario = this.TipoFuncionarioDomainService.SearchProjectionAll(x => x.Token).ToList();
                var tagNaoEncontrada = listaTagsGAD.Except(tipoVinculoFuncionario).ToList();
                if (tagNaoEncontrada.Any())
                {
                    string lista = string.Join(", ", tagNaoEncontrada);
                    throw new ParticipanteNaoEncontradoException(lista);
                }
            }
        }

        private void ValidarParticipantes(List<ParticipantesTagVO> participantes, List<string> tagsGAD)
        {
            var participantesSemEmail = participantes?.Where(w => w.EmailParticipante == null).ToList();
            if (participantesSemEmail != null && participantesSemEmail.Any())
            {
                var textoParticipanteSemEmail = " <br>";
                foreach (var participanteSemEmail in participantesSemEmail)
                    textoParticipanteSemEmail += $"{participanteSemEmail.NomeParticipante} <br>";

                throw new EmailParticipanteNaoEncontradoException(textoParticipanteSemEmail);
            }

            if (!participantes.Any() && tagsGAD.Any())
            {
                var textoTagVinculoGAD = " <br>";
                foreach (var tagVinculoGAD in tagsGAD)
                    textoTagVinculoGAD += $"{tagVinculoGAD} <br>";

                throw new ParticipanteNaoEncontradoException(textoTagVinculoGAD);
            }
        }

        private long GerarDocumentoGadPorConfiguracao(ConfiguracaoDocumentoGadVO configuracaoDocumentoGad)
        {
            var usuarioInclusao = UsuarioLogado.RetornaUsuarioLogado();
            var modeloDocumento = new ConfiguracaoDocumentoVO
            {
                Seq = configuracaoDocumentoGad.SeqConfiguracaoGad,
                UsuarioInclusao = usuarioInclusao,
                NovoDocumento = new DocumentoSistemaSeqOuTokenVO()
                {
                    NomeDocumento = configuracaoDocumentoGad.NomeDocumento,
                    Descricao = configuracaoDocumentoGad.NomeDocumento,
                    TokenSistemaOrigem = configuracaoDocumentoGad.TokenSistemaOrigemGAD,
                    ExigeAprovacao = configuracaoDocumentoGad.ExigeAprovacao,
                    IndicadorRastreabilidade = configuracaoDocumentoGad.IndicadorRastreabilidade,
                    HabilitaNotificacao = configuracaoDocumentoGad.HabilitaNotificacao,
                    EmailNotificarAssinatura = configuracaoDocumentoGad.EmailNotificarAssinatura,
                    Tags = configuracaoDocumentoGad.Tags,
                    ParticipantesComTag = configuracaoDocumentoGad.ParticipantesComTag,
                    DataValidade = configuracaoDocumentoGad.DataValidade,
                    Arquivo = new ArquivoVO()
                    {
                        Conteudo = configuracaoDocumentoGad.Conteudo,
                        Nome = configuracaoDocumentoGad.NomeArquivo,
                        Tipo = "application/pdf"
                    }
                }
            };

            var cabecalho = new Dictionary<string, string>
            {
                { "token", configuracaoDocumentoGad.TokenSistemaOrigemGAD },
                { "code", usuarioInclusao }
            };

            string requisicaoJson = JsonConvert.SerializeObject(modeloDocumento, Newtonsoft.Json.Formatting.Indented);

            var retornoGAD = APIDocumentoGAD.Execute<object>("CriarDocumentoPorConfiguracaoDocumento", cabecalho, modeloDocumento);
            var retorno = JsonConvert.DeserializeObject<RetornoDocumentoVO>(retornoGAD.ToString());

            if (!string.IsNullOrEmpty(retorno.ErrorMessage))
                throw new Exception(retorno.ErrorMessage);

            return retorno.Itens.FirstOrDefault().Seq;
        }

        public ConfigurarRelatorioDeclaracaoGenericaVO ConfigurarRelatorioDeclaracaoGenerica(long seqNivelEnsinoPorGrupoDocumentoAcademico, long seqTipoDocumentoAcademico, SMCLanguage idiomaDocumentoAcademico, long seqAluno)
        {
            var instituicaoNivelTipoDocumentoAcademico = InstituicaoNivelTipoDocumentoAcademicoDomainService.BuscarConfiguracoesInstituicaoNivelTipoDocumentoAcademico(seqNivelEnsinoPorGrupoDocumentoAcademico, seqTipoDocumentoAcademico, idiomaDocumentoAcademico);

            var tags = instituicaoNivelTipoDocumentoAcademico.Tags.SMCForEach(f => f.DescricaoTag = f.DescricaoTag.Replace("{{", "").Replace("}}", "")).ToList();
            var temSituacaoEnade = tags.Any(a => a.DescricaoTag == TagsDeclaracaoGenerica.SITUACAO_ENADE);

            var tagsSemQueryOrigem = tags.Where(w => w.QueryOrigem == null).ToList();
            var dadosAluno = BuscarDadosTagsRelatorioGenerico(seqAluno, temSituacaoEnade);
            var listaTagsPreenchidas = PreencherValorTagDeclaracaoGenerica(tagsSemQueryOrigem, dadosAluno);

            var tagsComQueryOrigem = tags.Where(w => w.QueryOrigem != null).ToList();
            if (dadosAluno.CodigoAlunoMigracao.HasValue)
            {
                foreach (var tag in tagsComQueryOrigem)
                {
                    var tagDeclaracaoGenerica = tag.Transform<ConfigurarTagsDeclaracaoGenericaVO>();
                    if (ValidaQuery(tag.QueryOrigem))
                    {
                        var dado = IntegracaoAcademicoService.BuscarDadosTagsRelatorioGenerico(tag.QueryOrigem, dadosAluno.CodigoAlunoMigracao.Value);
                        tagDeclaracaoGenerica.Valor = dado;
                    }
                    else
                        tagDeclaracaoGenerica.Valor = $"Houve um erro ao recuperar os dados da tag. Verifique a sua configuração.";

                    listaTagsPreenchidas.Add(tagDeclaracaoGenerica);
                }
            }

            var dados = new ConfigurarRelatorioDeclaracaoGenericaVO
            {
                DescricaoTipoDocumentoAcademico = instituicaoNivelTipoDocumentoAcademico.DescricaoTipoDocumentoAcademico,
                DescricaoNivelEnsino = instituicaoNivelTipoDocumentoAcademico.DescricaoNivelEnsino,
                DescricaoIdioma = idiomaDocumentoAcademico.GetDescriptionPortuguese(),
                SeqDadosPessoais = dadosAluno.SeqDadosPessoais,
                CodigoAlunoMigracao = dadosAluno.CodigoAlunoMigracao,
                NomeCivil = dadosAluno.NomeCivil,
                NomeSocial = dadosAluno.NomeSocial,
                NomeAlunoOficial = dadosAluno.NomeAlunoOficial,
                CodigoUnidadeSeo = dadosAluno.CodigoUnidadeSeo,
                Tags = listaTagsPreenchidas.OrderBy(o => o.DescricaoTag).ToList()
            };

            return dados;
        }

        private bool ValidaQuery(string query)
        {
            query = query.Trim();
            string[] palavrasRestritas = { "UPDATE", "DELETE", "INSERT", "CREATE", "ALTER TABLE", "ALTER PROCEDURE", "DROP", "CREATE", "EXEC", "TRUNCATE" };

            foreach (var palavra in palavrasRestritas)
            {
                if (query.IndexOf(palavra, StringComparison.OrdinalIgnoreCase) >= 0)
                    return false;
            }

            return query.StartsWith("SELECT", StringComparison.OrdinalIgnoreCase);
        }

        private List<ConfigurarTagsDeclaracaoGenericaVO> PreencherValorTagDeclaracaoGenerica(List<TipoDocumentoAcademicoTagVO> tags, AlunoTagsRelatorioGenericoVO dadosAluno)
        {
            var listaTags = new List<ConfigurarTagsDeclaracaoGenericaVO>();

            foreach (var tag in tags)
            {
                var tagDeclaracaoGenerica = tag.Transform<ConfigurarTagsDeclaracaoGenericaVO>();

                if (tag.TipoPreenchimentoTag == TipoPreenchimentoTag.Automatico)
                    switch (tag.DescricaoTag)
                    {
                        case TagsDeclaracaoGenerica.CICLO_LETIVO_ANO:
                            tagDeclaracaoGenerica.Valor = dadosAluno.AnoCicloLetivo.ToString()?.Trim();
                            break;

                        case TagsDeclaracaoGenerica.PREVISAO_FORMATURA_ANO:
                            tagDeclaracaoGenerica.Valor = dadosAluno.AnoPrevisaoFormatura.HasValue ? dadosAluno.AnoPrevisaoFormatura.ToString()?.Trim() : TagsDeclaracaoGenerica.VALOR_NAO_ENCONTRADO;
                            break;

                        case TagsDeclaracaoGenerica.COD_ALUNO_MIGRACAO:
                            tagDeclaracaoGenerica.Valor = dadosAluno.CodigoAlunoMigracao.ToString()?.Trim();
                            break;

                        case TagsDeclaracaoGenerica.DATA:
                            tagDeclaracaoGenerica.Valor = dadosAluno.Data.ToString()?.Trim();
                            break;

                        case TagsDeclaracaoGenerica.NOME_ALUNO_OFICIAL:
                            tagDeclaracaoGenerica.Valor = dadosAluno.NomeAlunoOficial?.Trim();
                            break;

                        case TagsDeclaracaoGenerica.FRASE_NOME_CIVIL:
                            tagDeclaracaoGenerica.Valor = dadosAluno.ExibirNomeSocial ? $"Nome civil: {dadosAluno.NomeCivil} CPF: {SMCMask.ApplyMaskCPF(dadosAluno.Cpf.Trim())}" : TagsDeclaracaoGenerica.NAO_APLICA;
                            break;

                        case TagsDeclaracaoGenerica.NOME_CIDADE:
                            tagDeclaracaoGenerica.Valor = !string.IsNullOrEmpty(dadosAluno.NomeCidade) ? dadosAluno.NomeCidade.ToString()?.Trim() : TagsDeclaracaoGenerica.VALOR_NAO_ENCONTRADO;
                            break;

                        case TagsDeclaracaoGenerica.NOME_CURSO:
                            tagDeclaracaoGenerica.Valor = !string.IsNullOrEmpty(dadosAluno.NomeCurso) ? dadosAluno.NomeCurso.ToString()?.Trim() : TagsDeclaracaoGenerica.VALOR_NAO_ENCONTRADO;
                            break;

                        case TagsDeclaracaoGenerica.CICLO_LETIVO_NUM:
                            tagDeclaracaoGenerica.Valor = dadosAluno.NumCicloLetivo.ToString()?.Trim();
                            break;

                        case TagsDeclaracaoGenerica.PREVISAO_FORMATURA_SEMESTRE:
                            tagDeclaracaoGenerica.Valor = dadosAluno.SemestrePrevisaoFormatura.HasValue ? dadosAluno.SemestrePrevisaoFormatura.ToString()?.Trim() : TagsDeclaracaoGenerica.VALOR_NAO_ENCONTRADO;
                            break;

                        case TagsDeclaracaoGenerica.CPF:
                            tagDeclaracaoGenerica.Valor = !string.IsNullOrEmpty(dadosAluno.Cpf) ? SMCMask.ApplyMaskCPF(dadosAluno.Cpf.Trim()) : string.Empty;
                            break;

                        case TagsDeclaracaoGenerica.GENERO:
                            tagDeclaracaoGenerica.Valor = dadosAluno.Sexo.ToString()?.Trim();
                            break;

                        case TagsDeclaracaoGenerica.DATA_ADMISSAO:
                            tagDeclaracaoGenerica.Valor = dadosAluno.DataAdmissao.ToString("dd/MM/yyyy").Trim();
                            break;

                        case TagsDeclaracaoGenerica.DATA_PREVISAO_CONCLUSAO:
                            tagDeclaracaoGenerica.Valor = dadosAluno.DataPrevisaoConclusao.HasValue ? dadosAluno.DataPrevisaoConclusao.Value.ToString("dd/MM/yyyy")?.Trim() : TagsDeclaracaoGenerica.VALOR_NAO_ENCONTRADO;
                            break;

                        case TagsDeclaracaoGenerica.DATA_LIMITE_CONCLUSAO:
                            tagDeclaracaoGenerica.Valor = dadosAluno.DataLimiteConclusao.HasValue ? dadosAluno.DataLimiteConclusao.Value.ToString("dd/MM/yyyy")?.Trim() : TagsDeclaracaoGenerica.VALOR_NAO_ENCONTRADO;
                            break;

                        case TagsDeclaracaoGenerica.TURNO:
                            tagDeclaracaoGenerica.Valor = !string.IsNullOrEmpty(dadosAluno.Turno) ? dadosAluno.Turno?.Trim() : TagsDeclaracaoGenerica.VALOR_NAO_ENCONTRADO;
                            break;

                        case TagsDeclaracaoGenerica.FORMA_INGRESSO:
                            tagDeclaracaoGenerica.Valor = !string.IsNullOrEmpty(dadosAluno.FormaIngresso) ? dadosAluno.FormaIngresso?.Trim() : TagsDeclaracaoGenerica.VALOR_NAO_ENCONTRADO;
                            break;

                        case TagsDeclaracaoGenerica.LOCAL:
                            tagDeclaracaoGenerica.Valor = !string.IsNullOrEmpty(dadosAluno.Local) ? dadosAluno.Local?.Trim() : TagsDeclaracaoGenerica.VALOR_NAO_ENCONTRADO;
                            break;

                        case TagsDeclaracaoGenerica.UNIDADE:
                            tagDeclaracaoGenerica.Valor = !string.IsNullOrEmpty(dadosAluno.Unidade) ? dadosAluno.Unidade?.Trim() : TagsDeclaracaoGenerica.VALOR_NAO_ENCONTRADO;
                            break;

                        case TagsDeclaracaoGenerica.SITUACAO_ENADE:
                            tagDeclaracaoGenerica.Valor = dadosAluno.SituacaoEnade?.Trim();
                            break;

                        case TagsDeclaracaoGenerica.CONCLUSAO_ANO:
                            tagDeclaracaoGenerica.Valor = dadosAluno.ConclusaoAno.HasValue ? dadosAluno.ConclusaoAno.ToString()?.Trim() : TagsDeclaracaoGenerica.VALOR_NAO_ENCONTRADO;
                            break;

                        case TagsDeclaracaoGenerica.CONCLUSAO_SEMESTRE:
                            tagDeclaracaoGenerica.Valor = dadosAluno.ConclusaoSemestre.HasValue ? dadosAluno.ConclusaoSemestre.ToString()?.Trim() : TagsDeclaracaoGenerica.VALOR_NAO_ENCONTRADO;
                            break;

                        case TagsDeclaracaoGenerica.INGRESSO_ANO:
                            tagDeclaracaoGenerica.Valor = dadosAluno.IngressoAno.ToString()?.Trim();
                            break;

                        case TagsDeclaracaoGenerica.INGRESSO_SEMESTRE:
                            tagDeclaracaoGenerica.Valor = dadosAluno.IngressoSemestre.ToString()?.Trim();
                            break;

                        case TagsDeclaracaoGenerica.SITUACAO_MATRICULA:
                            tagDeclaracaoGenerica.Valor = !string.IsNullOrEmpty(dadosAluno.SituacaoMatricula) ? dadosAluno.SituacaoMatricula?.Trim() : TagsDeclaracaoGenerica.VALOR_NAO_ENCONTRADO;
                            break;

                        case TagsDeclaracaoGenerica.AREA_CONHECIMENTO:
                            tagDeclaracaoGenerica.Valor = !string.IsNullOrEmpty(dadosAluno.AreaConhecimento) ? dadosAluno.AreaConhecimento?.Trim() : TagsDeclaracaoGenerica.VALOR_NAO_ENCONTRADO;
                            break;

                        case TagsDeclaracaoGenerica.SEMESTRES_MATRICULADOS:
                            tagDeclaracaoGenerica.Valor = dadosAluno.SemestresMatriculados.ToString()?.Trim();
                            break;

                        case TagsDeclaracaoGenerica.PERIODO:
                            tagDeclaracaoGenerica.Valor = dadosAluno.Perioodo.ToString()?.Trim();
                            break;
                    }
                listaTags.Add(tagDeclaracaoGenerica);
            }

            return listaTags;
        }

        private AlunoTagsRelatorioGenericoVO BuscarDadosTagsRelatorioGenerico(long seqAluno, bool temSituacaoEnade = false)
        {
            var spec = new AlunoFilterSpecification() { Seq = seqAluno };

            var dadosSituacaoMatricula = AlunoHistoricoSituacaoDomainService.BuscarSituacaoAtualAluno(seqAluno);

            var dados = this.SearchProjectionByKey(spec, s => new AlunoTagsRelatorioGenericoVO()
            {
                SeqDadosPessoais = s.DadosPessoais.Seq,
                SeqPessoa = s.DadosPessoais.SeqPessoa,
                NomeCivil = s.DadosPessoais.Nome,
                NomeSocial = s.DadosPessoais.NomeSocial,
                CodigoAlunoMigracao = s.CodigoAlunoMigracao,
                Cpf = s.Pessoa.Cpf,
                Sexo = s.DadosPessoais.Sexo,
                DataAdmissao = s.Historicos.FirstOrDefault(f => f.Atual).DataAdmissao,
                DataPrevisaoConclusao = (DateTime?)s.Historicos.FirstOrDefault(f => f.Atual).PrevisoesConclusao.OrderByDescending(o => o.DataInclusao).FirstOrDefault().DataPrevisaoConclusao,
                DataLimiteConclusao = (DateTime?)s.Historicos.FirstOrDefault(f => f.Atual).PrevisoesConclusao.OrderByDescending(o => o.DataInclusao).FirstOrDefault().DataLimiteConclusao,
                NomeCurso = s.Historicos.FirstOrDefault(f => f.Atual).CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.CursoOferta.Descricao,
                AreaConhecimento = s.Historicos.FirstOrDefault(f => f.Atual).CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.CursoOferta.Curso.Classificacoes.FirstOrDefault().Classificacao.Descricao,
                CodigoCursoOfertaLocalidade = s.Historicos.FirstOrDefault(f => f.Atual).CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.Codigo,
                FormaIngresso = s.Historicos.FirstOrDefault(f => f.Atual).FormaIngresso.Descricao,
                Turno = s.Historicos.FirstOrDefault(f => f.Atual).CursoOfertaLocalidadeTurno.Turno.Descricao,
                Local = s.Historicos.FirstOrDefault(f => f.Atual).CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.HierarquiasEntidades.FirstOrDefault().ItemSuperior.Entidade.Nome,
                Unidade = s.Historicos.FirstOrDefault(f => f.Atual).CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.CursoUnidade.HierarquiasEntidades.FirstOrDefault().ItemSuperior.Entidade.Nome,
                CodigoUnidadeSeo = s.Historicos.FirstOrDefault(f => f.Atual).CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.CursoUnidade.HierarquiasEntidades.FirstOrDefault().ItemSuperior.Entidade.CodigoUnidadeSeo,
                NomeCidadeEntidadeVinculo = s.Historicos.FirstOrDefault(f => f.Atual).EntidadeVinculo.Enderecos.Where(w => w.TipoEndereco == TipoEndereco.Comercial).FirstOrDefault().NomeCidade,
                NomeCidade = s.Historicos.FirstOrDefault(f => f.Atual).CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.Enderecos.Where(w => w.TipoEndereco == TipoEndereco.Comercial).FirstOrDefault().NomeCidade,
                NumCicloLetivo = s.Historicos.FirstOrDefault(f => f.Atual).HistoricosCicloLetivo.OrderByDescending(o => o.CicloLetivo.Ano).ThenByDescending(o => o.CicloLetivo.Numero).FirstOrDefault(c => !c.DataExclusao.HasValue).CicloLetivo.Numero,
                AnoCicloLetivo = s.Historicos.FirstOrDefault(f => f.Atual).HistoricosCicloLetivo.OrderByDescending(o => o.CicloLetivo.Ano).ThenByDescending(o => o.CicloLetivo.Numero).FirstOrDefault(c => !c.DataExclusao.HasValue).CicloLetivo.Ano,
                IngressoAno = s.Historicos.FirstOrDefault(f => f.Atual).CicloLetivo.Ano,
                IngressoSemestre = s.Historicos.FirstOrDefault(f => f.Atual).CicloLetivo.Numero,
                SemestresMatriculados = s.Historicos.FirstOrDefault().HistoricosCicloLetivo.Count(a => a.AlunoHistoricoSituacao.Any(b => b.SituacaoMatricula.Token == TOKENS_SITUACAO_MATRICULA.MATRICULADO || b.SituacaoMatricula.Token == TOKENS_SITUACAO_MATRICULA.PROVAVEL_FORMANDO || b.SituacaoMatricula.Token == TOKENS_SITUACAO_MATRICULA.FORMADO || b.SituacaoMatricula.Token == TOKENS_SITUACAO_MATRICULA.MATRICULADO_MOBILIDADE)),
                Perioodo = s.Historicos.FirstOrDefault(f => f.Atual).HistoricosCicloLetivo.Count(),
                ConclusaoAno = s.Historicos.FirstOrDefault(f => f.Atual).HistoricosCicloLetivo.FirstOrDefault(a => a.AlunoHistoricoSituacao.Any(b => b.SituacaoMatricula.Token == TOKENS_SITUACAO_MATRICULA.FORMADO)).CicloLetivo.Ano,
                ConclusaoSemestre = s.Historicos.FirstOrDefault(f => f.Atual).HistoricosCicloLetivo.FirstOrDefault(a => a.AlunoHistoricoSituacao.Any(b => b.SituacaoMatricula.Token == TOKENS_SITUACAO_MATRICULA.FORMADO)).CicloLetivo.Numero,
            });

            if (dados.CodigoAlunoMigracao.HasValue && temSituacaoEnade)
            {
                var dadosSituacaoEnade = IntegracaoAcademicoService.BuscarSituacaoEnade(dados.CodigoAlunoMigracao.Value, dados.CodigoCursoOfertaLocalidade);
                var dadoSituacaoEnade = dadosSituacaoEnade?.FirstOrDefault();

                dados.SituacaoEnade = dadoSituacaoEnade != null ? $"{dadoSituacaoEnade.Categoria.Trim()} - {dadoSituacaoEnade.SituacaoEnadeXsd}" : TagsDeclaracaoGenerica.VALOR_NAO_ENCONTRADO;
            }

            dados.SituacaoMatricula = dadosSituacaoMatricula.Descricao;
            dados.Cpf = dados.Cpf.SMCRemoveNonDigits();
            dados.CodigoAlunoMigracao = dados.CodigoAlunoMigracao.HasValue ? dados.CodigoAlunoMigracao : 0;
            dados.Data = DateTime.Now.ToString("dd 'de' MMMMM 'de' yyyy");
            dados.ExibirNomeSocial = DocumentoConclusaoDomainService.ExibirNomeSocial(dados.SeqPessoa, TipoPessoa.Fisica);
            dados.NomeAlunoOficial = dados.ExibirNomeSocial ? dados.NomeSocial : dados.NomeCivil;
            dados.NomeCidade = !string.IsNullOrEmpty(dados.NomeCidade) ? dados.NomeCidade : dados.NomeCidadeEntidadeVinculo;

            return dados;
        }

        public byte[] BuscarRelatorioGenerico(ConfigurarRelatorioDeclaracaoGenericaVO dados)
        {
            var configuracao = InstituicaoNivelTipoDocumentoAcademicoDomainService.BuscarConfiguracoesInstituicaoNivelTipoDocumentoAcademico(dados.SeqNivelEnsinoPorGrupoDocumentoAcademico, dados.SeqTipoDocumentoAcademico, dados.IdiomaDocumentoAcademico);
            var modelDic = new Dictionary<string, object>[1];
            var dic = new Dictionary<string, object>();
            foreach (var tag in dados.Tags)
            {
                if (tag.DescricaoTag == TagsDeclaracaoGenerica.FRASE_NOME_CIVIL && tag.Valor == TagsDeclaracaoGenerica.NAO_APLICA)
                    tag.Valor = " ";

                dic[tag.DescricaoTag] = tag.Valor;
            }
            modelDic[0] = dic;

            string json = JsonConvert.SerializeObject(dic);
            var arquivo = SautinSoftHelper.MailMergeToPdf(configuracao.ModelosRelatorio.ArquivoModelo.FileData, configuracao.ModelosRelatorio.ArquivoModelo.Name, "dotx", json);

            return arquivo;
        }
    }
}