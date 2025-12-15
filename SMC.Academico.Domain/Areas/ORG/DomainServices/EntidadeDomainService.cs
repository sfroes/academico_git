using System;
using System.Collections.Generic;
using System.Linq;
using SMC.Academico.Common.Areas.ORG.Enums;
using SMC.Academico.Common.Areas.ORG.Exceptions;
using SMC.Academico.Common.Areas.ORG.Includes;
using SMC.Academico.Common.Constants;
using SMC.Academico.Common.Enums;
using SMC.Academico.Domain.Areas.APR.DomainServices;
using SMC.Academico.Domain.Areas.CSO.DomainServices;
using SMC.Academico.Domain.Areas.CSO.Models;
using SMC.Academico.Domain.Areas.CSO.Specifications;
using SMC.Academico.Domain.Areas.CSO.ValueObjects;
using SMC.Academico.Domain.Areas.DCT.DomainServices;
using SMC.Academico.Domain.Areas.ORG.Models;
using SMC.Academico.Domain.Areas.ORG.Specifications;
using SMC.Academico.Domain.Areas.ORG.Validators;
using SMC.Academico.Domain.Areas.ORG.ValueObjects;
using SMC.Academico.Domain.Helpers;
using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.Framework;
using SMC.Framework.Domain;
using SMC.Framework.Domain.Exceptions;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Repository;
using SMC.Framework.Specification;
using SMC.Framework.Validation;
using SMC.Localidades.Common.Areas.LOC.Enums;
using SMC.Localidades.ServiceContract.Areas.LOC.Interfaces;

namespace SMC.Academico.Domain.Areas.ORG.DomainServices
{
    public class EntidadeDomainService : AcademicoContextDomain<Entidade>
    {
        #region Query

        private const string BUSCAR_ENTIDADE_SUPERIOR_HIERARQUIA = "select seq_entidade from org.fn_buscar_entidade_superior_hierarquia(@seq_entidade_origem, @tipo_visao, @seq_tipo_entidade_destino)";

        #endregion Query

        #region Serviços

        private OrigemAvaliacaoDomainService OrigemAvaliacaoDomainService => Create<OrigemAvaliacaoDomainService>();

        private InstituicaoTipoEntidadeDomainService InstituicaoTipoEntidadeDomainService
        {
            get { return this.Create<InstituicaoTipoEntidadeDomainService>(); }
        }

        private InstituicaoNivelDomainService InstituicaoNivelDomainService
        {
            get { return this.Create<InstituicaoNivelDomainService>(); }
        }

        private InstituicaoEnsinoDomainService InstituicaoEnsinoDomainService
        {
            get { return this.Create<InstituicaoEnsinoDomainService>(); }
        }

        private TipoEntidadeDomainService TipoEntidadeDomainService
        {
            get { return this.Create<TipoEntidadeDomainService>(); }
        }

        private HierarquiaEntidadeItemDomainService HierarquiaEntidadeItemDomainService
        {
            get { return this.Create<HierarquiaEntidadeItemDomainService>(); }
        }

        private TipoHierarquiaEntidadeItemDomainService TipoHierarquiaEntidadeItemDomainService
        {
            get { return this.Create<TipoHierarquiaEntidadeItemDomainService>(); }
        }

        private InstituicaoTipoEntidadeHierarquiaClassificacaoDomainService GetInstituicaoTipoEntidadeHierarquiaClassificacaoDomainService()
        {
            return this.Create<InstituicaoTipoEntidadeHierarquiaClassificacaoDomainService>();
        }

        private InstituicaoNivelHierarquiaClassificacaoDomainService InstituicaoNivelHierarquiaClassificacaoDomainService
        {
            get { return this.Create<InstituicaoNivelHierarquiaClassificacaoDomainService>(); }
        }

        private InstituicaoTipoEntidadeVinculoColaboradorDomainService InstituicaoTipoEntidadeVinculoColaboradorDomainService
        {
            get { return this.Create<InstituicaoTipoEntidadeVinculoColaboradorDomainService>(); }
        }

        private AtoNormativoDomainService AtoNormativoDomainService
        {
            get { return this.Create<AtoNormativoDomainService>(); }
        }

        private CursoOfertaLocalidadeDomainService CursoOfertaLocalidadeDomainService
        {
            get { return this.Create<CursoOfertaLocalidadeDomainService>(); }
        }
        private ILocalidadeService LocalidadeService => Create<ILocalidadeService>();

        #endregion Serviços

        public long SalvarEntidade(EntidadeVO entidadeVo)
        {
            var entidade = entidadeVo.Transform<Entidade>();

            // Carrega as classificações selecionadas para a entidade
            entidade.Classificacoes = GerarEntidadeClassificacoes(entidadeVo);

            // Valida a entidade
            var validator = new EntidadeValidator();
            var results = validator.Validate(entidade);
            if (!results.IsValid)
            {
                var errorList = new List<SMCValidationResults>();
                errorList.Add(results);
                throw new SMCInvalidEntityException(errorList);
            }

            ValidarDadosContatoObrigatorios(entidade);

            // Se o arquivo do logotipo não foi alterado, atualiza com conteúdo com o que está no banco
            this.EnsureFileIntegrity(entidade, x => x.SeqArquivoLogotipo, x => x.ArquivoLogotipo);

            // Faz a inclusão da situação atual
            if (entidade.Seq == 0)
            {
                // Faz a inclusão da situação
                IncluirSituacao(entidade);
            }

            // Salva a entidade
            this.SaveEntity(entidade);

            // Retorna o sequencial da entidade salva
            return entidade.Seq;
        }

        public EntidadeVO BuscarEntidade(long seqEntidade)
        {
            // Recupera a entidade
            var includes = IncludesEntidade.HistoricoSituacoes_SituacaoEntidade
                         | IncludesEntidade.ArquivoLogotipo
                         | IncludesEntidade.Enderecos
                         | IncludesEntidade.EnderecosEletronicos
                         | IncludesEntidade.Telefones
                         | IncludesEntidade.TipoEntidade
                         | IncludesEntidade.Classificacoes_Classificacao_HierarquiaClassificacao;
            var entidade = this.SearchByKey(new EntidadeFilterSpecification { Seq = seqEntidade }, includes);

            // Recupera as informações do tipo de entidade para a instituição
            var includeTipo = IncludesInstituicaoTipoEntidade.TiposEndereco
                            | IncludesInstituicaoTipoEntidade.TiposTelefone
                            | IncludesInstituicaoTipoEntidade.TiposEnderecoEletronico;
            var specInstituicaoTipoEntidade = new InstituicaoTipoEntidadeFilterSpecification
            {
                SeqInstituicaoEnsino = entidade.SeqInstituicaoEnsino,
                SeqTipoEntidade = entidade.SeqTipoEntidade,
            };
            var instituicaoTipoEntidade = InstituicaoTipoEntidadeDomainService.SearchByKey(specInstituicaoTipoEntidade, includeTipo);

            // Gera o VO considerando os dados da entidade e do tipo de entidade
            var entidadeValue = entidade.Transform<EntidadeVO>(instituicaoTipoEntidade);

            // Ajusta os tipos de endereço
            entidadeValue.TiposEnderecos = new List<TipoEndereco>();
            if (instituicaoTipoEntidade?.TiposEndereco.SMCCount() > 0)
            {
                foreach (var item in instituicaoTipoEntidade.TiposEndereco)
                {
                    entidadeValue.TiposEnderecos.Add((TipoEndereco)item.TipoEndereco);
                }
            }

            // Ajusta os tipos de telefone
            entidadeValue.TiposTelefone = new List<SMCDatasourceItem<string>>();
            if (instituicaoTipoEntidade?.TiposTelefone.SMCCount() > 0)
            {
                foreach (var tipoTelefone in instituicaoTipoEntidade.TiposTelefone)
                {
                    var dscTipoTelefone = tipoTelefone.CategoriaTelefone.HasValue && tipoTelefone.CategoriaTelefone != CategoriaTelefone.Nenhum
                                        ? $"{tipoTelefone.TipoTelefone.SMCGetDescription()} - {tipoTelefone.CategoriaTelefone.SMCGetDescription()}"
                                        : tipoTelefone.TipoTelefone.SMCGetDescription();
                    SMCDatasourceItem<string> novoTipoTelefone = new SMCDatasourceItem<string>()
                    {
                        Descricao = dscTipoTelefone,
                        Seq = dscTipoTelefone
                    };
                    entidadeValue.TiposTelefone.Add(novoTipoTelefone);
                }
                entidadeValue.TiposTelefone = entidadeValue.TiposTelefone.OrderBy(t => t.Descricao).ToList();
            }

            // Ajusta os tipos de endereço eletrônico
            entidadeValue.TiposEnderecoEletronico = new List<SMCDatasourceItem<string>>();
            if (instituicaoTipoEntidade?.TiposEnderecoEletronico.SMCCount() > 0)
            {
                foreach (var tipoEE in instituicaoTipoEntidade.TiposEnderecoEletronico)
                {
                    var descricaoTipoEE = tipoEE.CategoriaEnderecoEletronico.HasValue && tipoEE.CategoriaEnderecoEletronico != CategoriaEnderecoEletronico.Nenhum
                                        ? $"{tipoEE.TipoEnderecoEletronico.SMCGetDescription()} - {tipoEE.CategoriaEnderecoEletronico.SMCGetDescription()}"
                                        : tipoEE.TipoEnderecoEletronico.SMCGetDescription();
                    SMCDatasourceItem<string> novoTipoEE = new SMCDatasourceItem<string>()
                    {
                        Descricao = descricaoTipoEE,
                        Seq = descricaoTipoEE
                    };
                    entidadeValue.TiposEnderecoEletronico.Add(novoTipoEE);
                }
                entidadeValue.TiposEnderecoEletronico = entidadeValue.TiposEnderecoEletronico.OrderBy(t => t.Descricao).ToList();
            }

            // Preenche as hierarquias de classificações com suas configurações e itens
            entidadeValue.HierarquiasClassificacoes = this.GerarEntidadeClassificacoes(entidadeValue.SeqTipoEntidade, entidadeValue.Classificacoes);

            // Verifica se a entidade está ativa baseando-se no histórico da situações e na ultima situação da entidade
            entidadeValue.Ativa = entidade?.SituacaoAtual?.CategoriaAtividade != Common.Areas.ORG.Enums.CategoriaAtividade.Inativa;

            entidadeValue.AtivaAbaAtoNormativo = TipoEntidadeDomainService.PermiteAtoNormativo(entidadeValue.SeqTipoEntidade, string.Empty);
            if (entidadeValue.AtivaAbaAtoNormativo)
            {
                var retorno = AtoNormativoDomainService.BuscarAtoNormativoPorEntidade(seqEntidade: entidadeValue.Seq, SeqInstituicaoEnsino: entidadeValue.SeqInstituicaoEnsino);

                entidadeValue.HabilitaColunaGrauAcademico = retorno.Where(w => w.DescricaoGrauAcademico != null).Select(s => s.DescricaoGrauAcademico).Any();
                entidadeValue.AtoNormativo = retorno;
            }

            // Retorna
            return entidadeValue;
        }

        public Dictionary<long, string> BuscarEntidadesNomes(List<long> seqsEntidades)
        {
            var entidades = this.SearchProjectionBySpecification(new EntidadeFilterSpecification { Seqs = seqsEntidades.ToArray() }, e => new
            {
                e.Seq,
                e.Nome
            }).ToDictionary(x => x.Seq, x => x.Nome);

            // Retorna
            return entidades;
        }

        public string BuscarEntidadeNome(long seqEntidade)
        {
            this.DisableFilter(FILTER.INSTITUICAO_ENSINO);
            var nome = this.SearchProjectionByKey(seqEntidade, e => e.Nome);
            this.EnableFilter(FILTER.INSTITUICAO_ENSINO);
            return nome;
        }

        /// <summary>
        /// Buscar uma lista de entidades com seus históricos de situação e tipo
        /// </summary>
        /// <param name="filtros">Filtros para pesquisa</param>
        /// <returns>Lista de entidades ordenadas pela descrição do tipo e nome da entidade</returns>
        public SMCPagerData<Entidade> BuscarEntidades(EntidadeFiltroVO filtros)
        {
            try
            {
                DisableFilter(FILTER.HIERARQUIA_ENTIDADE_ORGANIZACIONAL);
                int total = 0;

                var spec = filtros.Transform<EntidadeFilterSpecification>();
                var includes = IncludesEntidade.HistoricoSituacoes_SituacaoEntidade | IncludesEntidade.TipoEntidade;

                if (filtros.TipoOrgaoRegulador.HasValue || filtros.CodigoOrgaoRegulador.HasValue)
                {
                    var specCursoOfertaLocalidade = new CursoOfertaLocalidadeFilterSpecification();

                    if (filtros.TipoOrgaoRegulador.HasValue)
                    {
                        var specInstituicaoNivel = new InstituicaoNivelFilterSpecification { TipoOrgaoRegulador = filtros.TipoOrgaoRegulador };
                        var seqsNiveisEnsino = InstituicaoNivelDomainService.SearchBySpecification(specInstituicaoNivel).Select(s => s.SeqNivelEnsino).ToList();
                        specCursoOfertaLocalidade.SeqsNiveisEnsino = seqsNiveisEnsino;
                    }

                    if (filtros.CodigoOrgaoRegulador.HasValue)
                    {
                        specCursoOfertaLocalidade.CodigoOrgaoRegulador = filtros.CodigoOrgaoRegulador;
                    }

                    var seqsCursoOfertaLocalidade = this.CursoOfertaLocalidadeDomainService.SearchProjectionBySpecification(specCursoOfertaLocalidade, x => x.Seq).ToArray();

                    if (!spec.Seq.HasValue)
                    {
                        //Adiciona os sequenciais de entidade encontrados se não tiver filtrado por um sequencial de entidade específico
                        spec.Seqs = seqsCursoOfertaLocalidade;
                    }
                    else
                    {
                        if (!seqsCursoOfertaLocalidade.Contains(spec.Seq.Value))
                        {
                            //Se tiver filtrado por um sequencial de entidade específico e essa entidade não
                            //for referente aos filtros de orgão pesquisados, não retorna nada
                            var listaVazia = new List<Entidade>();
                            return new SMCPagerData<Entidade>(listaVazia, 0);
                        }
                    }

                    //Se não retornou nenhum sequencial de entidade, quer dizer que não existe nenhum curso oferta localidade
                    //que corresponda ao filtro selecionado, então não deve retornar nenhum registro
                    if (!seqsCursoOfertaLocalidade.Any())
                    {
                        var listaVazia = new List<Entidade>();
                        return new SMCPagerData<Entidade>(listaVazia, 0);
                    }
                }

                if (filtros.PageSettings != null)
                {
                    List<SMCSortInfo> listaOrdenacao = filtros.PageSettings.SortInfo;

                    if (!listaOrdenacao.Any())
                    {
                        //ORDENAÇÃO DEFAULT PELO TIPO DE ENTIDADE, E DEPOIS O NOME. COLOCANDO A ANOTAÇÃO SMCSORTABLE A ORDEM
                        //ESTÁ NOME E DEPOIS TIPO, ENTÃO ORDENA ERRADO, E A LISTAGEM DEVE SER NESSA ORDEM
                        spec.SetOrderBy(o => o.TipoEntidade.Descricao)
                            .SetOrderBy(o => o.Nome);
                    }
                }

                var result = SearchBySpecification(spec, out total, includes).ToList();

                if (filtros.LookupEntidadeAtoNormativo.GetValueOrDefault())
                {
                    var entidadesTipoCursoOfertaLocalidade = result.Where(a => a.TipoEntidade.Token == TOKEN_TIPO_ENTIDADE.CURSO_OFERTA_LOCALIDADE).ToList();

                    if (entidadesTipoCursoOfertaLocalidade.Any())
                    {
                        var seqsEntidadesTipoCursoOfertaLocalidade = entidadesTipoCursoOfertaLocalidade.Select(a => a.Seq).ToArray();

                        var specCursoOfertaLocalidade = new CursoOfertaLocalidadeFilterSpecification() { Seqs = seqsEntidadesTipoCursoOfertaLocalidade };
                        var cursoOfertaLocalidades = this.CursoOfertaLocalidadeDomainService.SearchBySpecification(specCursoOfertaLocalidade, x => x.CursoOferta.Curso).ToList();
                        var cursoOfertaLocalidadesComOrgao = cursoOfertaLocalidades.Where(a => a.CodigoOrgaoRegulador.HasValue).ToList();

                        foreach (var cursoOfertaLocalidade in cursoOfertaLocalidadesComOrgao)
                        {
                            var entidade = entidadesTipoCursoOfertaLocalidade.FirstOrDefault(a => a.Seq == cursoOfertaLocalidade.Seq);
                            var selectTipoOrgaoRegulador = this.InstituicaoNivelDomainService.BuscarTipoOrgaoReguladorInstituicaoNivelEnsino(cursoOfertaLocalidade.CursoOferta.Curso.SeqNivelEnsino, entidade.SeqInstituicaoEnsino.Value);
                            var tipoOrgaoRegulador = selectTipoOrgaoRegulador.FirstOrDefault().Descricao;

                            entidade.Nome += $" ({tipoOrgaoRegulador}: {cursoOfertaLocalidade.CodigoOrgaoRegulador})";
                        }
                    }
                }

                return new SMCPagerData<Entidade>(result, total);
            }
            finally
            {
                EnableFilter(FILTER.HIERARQUIA_ENTIDADE_ORGANIZACIONAL);
            }
        }

        /// <summary>
        /// Buscar uma lista de entidades para o lookup de entidades seleção multipla
        /// </summary>
        /// <param name="filtros">Filtros para pesquisa</param>
        /// <returns>Lista de entidades</returns>
        public SMCPagerData<Entidade> BuscarEntidadesLookupSelecaoMultipla(EntidadeFiltroVO filtros)
        {
            // Monta o specification base
            var spec = filtros.Transform<EntidadeResponsavelVisaoSpecification>();
            spec.Tipovisao = TipoVisao.VisaoOrganizacional;
            spec.ListarApenasEntidadesAtivas = true;

            // Se tem algum item selecionado, atualiza o specification
            if (filtros.SeqsEntidadesResponsaveis != null && filtros.SeqsEntidadesResponsaveis.Count > 0)
                spec.SeqsEntidadesSelecionadas = filtros.SeqsEntidadesResponsaveis;
            else if (filtros.SeqsEntidadesCompartilhadas != null && filtros.SeqsEntidadesCompartilhadas.Count > 0)
                spec.SeqsEntidadesSelecionadas = filtros.SeqsEntidadesCompartilhadas;

            var includes = IncludesEntidade.HistoricoSituacoes_SituacaoEntidade |
                            IncludesEntidade.TipoEntidade;

            int total = 0;
            var result = SearchBySpecification(spec, out total, includes).ToList();
            return new SMCPagerData<Entidade>(result, total);
        }

        /// <summary>
        /// Buscar uma lista de entidades para lookup
        /// </summary>
        /// <param name="filtros">Filtros para pesquisa</param>
        /// <returns>Lista de entidades ordenadas pela descrição do tipo e nome da entidade</returns>
        public SMCPagerData<Entidade> BuscarEntidadesLookup(long seq)
        {
            try
            {
                EntidadeFilterSpecification filtros = new EntidadeFilterSpecification() { Seq = seq };
                DisableFilter(FILTER.HIERARQUIA_ENTIDADE_ORGANIZACIONAL);
                int total = 0;

                filtros.SetOrderBy(o => o.TipoEntidade.Descricao)
                       .SetOrderBy(o => o.Nome);

                var includes = IncludesEntidade.HistoricoSituacoes_SituacaoEntidade |
                               IncludesEntidade.TipoEntidade;

                var result = SearchBySpecification(filtros, out total, includes);

                return new SMCPagerData<Entidade>(result, total);
            }
            finally
            {
                EnableFilter(FILTER.HIERARQUIA_ENTIDADE_ORGANIZACIONAL);
            }
        }

        public List<SMCDatasourceItem> BuscarEntidadesSelect(string token, long seqInstituicaoLogada)
        {
            var spec = new EntidadeTokenTipoSpecification();
            spec.Token = token;
            spec.SeqInstituicaoEnsino = seqInstituicaoLogada;
            spec.CategoriaAtividadeIguais = new CategoriaAtividade[] { CategoriaAtividade.Ativa, CategoriaAtividade.EmDesativacao };
            return SearchProjectionBySpecification(spec, p => new SMCDatasourceItem() { Seq = p.Seq, Descricao = p.Nome }).ToList();
        }

        public List<SMCDatasourceItem> BuscarGruposProgramasSelect()
        {
            var spec = new EntidadeGrupoProgramaFilterSpecification();
            return SearchProjectionBySpecification(spec, p => new SMCDatasourceItem() { Seq = p.Seq, Descricao = p.Nome }).OrderBy(p => p.Descricao).ToList();
        }

        /// <summary>
        /// Listar departamento do grupo de programa para select
        /// </summary>
        /// <param name="usarNomeReduzido">Se irá usar o nome reduzido caso exista</param>
        /// <returns>Lista SMCDatasourceItem</returns>
        public List<SMCDatasourceItem> BuscarDepartamentosGruposProgramasSelect(bool usarNomeReduzido = false)
        {
            try
            {
                DisableFilter(FILTER.HIERARQUIA_ENTIDADE_ORGANIZACIONAL);
                List<SMCDatasourceItem> retono = new List<SMCDatasourceItem>();

                var spec = new EntidadeFilterSpecification();
                spec.SeqInstituicaoEnsino = InstituicaoEnsinoDomainService.SearchBySpecification(new InstituicaoEnsinoFilterSpecification()).FirstOrDefault().SeqInstituicaoEnsino;
                var tokensTipoEntidade = new List<string> { TOKEN_TIPO_ENTIDADE.DEPARTAMENTO, TOKEN_TIPO_ENTIDADE.GRUPO_PROGRAMA };
                var retornoTipoEntidade = this.TipoEntidadeDomainService.BuscarTiposEntidade(new TipoEntidadeFilterSpecification() { Tokens = tokensTipoEntidade });
                spec.SeqsTipoEntidade = retornoTipoEntidade.Select(a => a.Seq).ToArray();

                var result = SearchProjectionBySpecification(spec, p => new { Seq = p.Seq, Nome = p.Nome, NomeReduzido = p.NomeReduzido }).ToList();
                if (usarNomeReduzido)
                {
                    result.ForEach(f =>
                    {
                        retono.Add(new SMCDatasourceItem() { Seq = f.Seq, Descricao = !string.IsNullOrEmpty(f.NomeReduzido) ? f.NomeReduzido : f.Nome });
                    });
                }
                else
                {
                    result.ForEach(f =>
                    {
                        retono.Add(new SMCDatasourceItem() { Seq = f.Seq, Descricao = f.Nome });
                    });
                }

                return retono.OrderBy(o => o.Descricao).ToList();
            }
            finally
            {
                EnableFilter(FILTER.HIERARQUIA_ENTIDADE_ORGANIZACIONAL);
            }
        }

        /// <summary>
        /// Busca todas as entidades que sejam de tipos vinculáveis aos colaboradores na instituição atual.
        /// </summary>
        /// <param name="ignorarFiltros">Quando setado ignora todos filtros exceto o filtro de instituição</param>
        /// <returns>Sequenciais e nomes das entidades encontradas</returns>
        public List<SMCDatasourceItem> BuscarEntidadesVinculoColaboradorSelect(bool ignorarFiltros)
        {
            if (ignorarFiltros)
            {
                FilterHelper.AtivarApenasFiltros(this, FILTER.INSTITUICAO_ENSINO);
            }

            var seqsTiposEntidadesVinculadas = this.InstituicaoTipoEntidadeVinculoColaboradorDomainService
                .SearchProjectionAll(p => p.InstituicaoTipoEntidade.SeqTipoEntidade, o => o.InstituicaoTipoEntidade.SeqTipoEntidade, isDistinct: true)
                .ToArray();

            var spec = new EntidadeFilterSpecification() { SeqsTipoEntidade = seqsTiposEntidadesVinculadas };
            var result = this.SearchProjectionBySpecification(spec, p => new SMCDatasourceItem() { Seq = p.Seq, Descricao = p.Nome })
                .OrderBy(o => o.Descricao)
                .ToList();

            if (ignorarFiltros)
            {
                FilterHelper.AtivarFiltros(this);
            }

            return result;
        }

        /// <summary>
        /// Converte e inclui a situação atual na nova entidade
        /// </summary>
        /// <param name="entidade">Entidade a ser gravada</param>
        public void IncluirSituacao(Entidade entidade)
        {
            entidade.HistoricoSituacoes = entidade.HistoricoSituacoes ?? new List<EntidadeHistoricoSituacao>();
            entidade.HistoricoSituacoes.Add(new EntidadeHistoricoSituacao
            {
                DataInicio = entidade.DataInicioSituacaoAtual,
                DataFim = entidade.DataFimSituacaoAtual,
                SeqSituacaoEntidade = entidade.SeqSituacaoAtual
            });
        }

        /// <summary>
        /// Atualiza a sequencia dos itens superiores da hierarquida de entidade da entidade externada quando a entidade responsável for atualizada
        /// </summary>
        /// <param name="entidadeExternada">Entidade externada com o SeqHierarquiaEntidadeItemSuperior preenchido</param>
        /// <param name="tokenTipoEntidadeExternada">Token do tipo da entidade externada</param>
        public void AtualizarHierarquiaEntidadeExternada(Entidade entidadeExternada, string tokenTipoEntidadeExternada, bool desativarfiltrosHierarquia = false)
        {
            if (desativarfiltrosHierarquia)
            {
                this.DisableFilter(FILTER.HIERARQUIA_ENTIDADE_LOCALIDADES);
                this.DisableFilter(FILTER.HIERARQUIA_ENTIDADE_ORGANIZACIONAL);
                this.DisableFilter(FILTER.HIERARQUIA_ENTIDADE_POLOS_VIRTUAIS);
                this.DisableFilter(FILTER.HIERARQUIA_ENTIDADE_UNIDADE_GESTORA);

                HierarquiaEntidadeItemDomainService.DisableFilter(FILTER.HIERARQUIA_ENTIDADE_LOCALIDADES);
                HierarquiaEntidadeItemDomainService.DisableFilter(FILTER.HIERARQUIA_ENTIDADE_ORGANIZACIONAL);
                HierarquiaEntidadeItemDomainService.DisableFilter(FILTER.HIERARQUIA_ENTIDADE_POLOS_VIRTUAIS);
                HierarquiaEntidadeItemDomainService.DisableFilter(FILTER.HIERARQUIA_ENTIDADE_UNIDADE_GESTORA);
            }

            var seqTipoEntidadeExternada = this.TipoEntidadeDomainService
                .BuscarTipoEntidadeNaInstituicao(tokenTipoEntidadeExternada)
                .Seq;

            foreach (var itemHierarquia in entidadeExternada.HierarquiasEntidades)
            {
                var itemHierarquiaSuperior = this.HierarquiaEntidadeItemDomainService
                    .SearchByKey(new SMCSeqSpecification<HierarquiaEntidadeItem>(itemHierarquia.SeqItemSuperior.Value));

                var tipoHierarquiaEntidadeExternadaItem = TipoHierarquiaEntidadeItemDomainService
                    .SearchByKey(new TipoHierarquiaEntidadeItemFilterSpecification()
                    {
                        SeqItemSuperior = itemHierarquiaSuperior.SeqTipoHierarquiaEntidadeItem,
                        SeqTipoEntidade = seqTipoEntidadeExternada
                    });

                itemHierarquia.SeqHierarquiaEntidade = itemHierarquiaSuperior.SeqHierarquiaEntidade;
                itemHierarquia.SeqTipoHierarquiaEntidadeItem = tipoHierarquiaEntidadeExternadaItem.Seq;
            }

            FilterHelper.AtivarFiltros(this);
        }

        public List<SMCDatasourceItem> BuscarUnidadesResponsaveisGPISelect()
        {
            return SearchProjectionBySpecification(new UnidadeResponsavelGPISpecification(), p => new SMCDatasourceItem() { Seq = p.Seq, Descricao = p.Nome }).OrderBy(o => o.Descricao).ToList();
        }

        public List<SMCDatasourceItem> BuscarEntidadesResponsaveisVisaoOrganizacionalSelect(bool ListarApenasEntidadesAtivas, bool usarNomeReduzido)
        {
            var spec = new EntidadeResponsavelVisaoSpecification()
            {
                Tipovisao = TipoVisao.VisaoOrganizacional,
                SeqsInstituicoesEnsino = GetDataFilter(FILTER.INSTITUICAO_ENSINO).ToList(),
                ListarApenasEntidadesAtivas = ListarApenasEntidadesAtivas
            };
            spec.SetOrderBy(e => usarNomeReduzido ? e.NomeReduzido : e.Nome);
            var lista = SearchProjectionBySpecification(spec, p => new SMCDatasourceItem() 
            { 
                Seq = p.Seq, 
                Descricao = usarNomeReduzido ? p.NomeReduzido : p.Nome
            }).ToList();

            return lista;
        }

        #region [ Classificações da Entidade ]

        /// <summary>
        /// Busca as hierarquias de classificação disponíveis para a edição, assim como seus filhos cadastrados (se houverem)
        /// </summary>
        /// <param name="seqEntidade"></param>
        /// <returns></returns>
        private List<EntidadeHierarquiaClassificacaoVO> BuscarEntidadeHierarquiasClassificacaoPorTipoEntidade(long seqTipoEntidade)
        {
            //retorno
            var listaRetorno = new List<EntidadeHierarquiaClassificacaoVO>();

            //tipo da entidade para a instituicao
            InstituicaoTipoEntidade instituicaoTipoEntidade = this.InstituicaoTipoEntidadeDomainService.BuscarTipoEntidadeDaInstituicao(seqTipoEntidade);

            this.GetInstituicaoTipoEntidadeHierarquiaClassificacaoDomainService()
                .BuscarInstituicaoTipoEntidadeHierarquiasClassificacao(instituicaoTipoEntidade.Seq)
                .ForEach(instituicaoTipoEntidadeHierarquiaClassificacao =>

                    //Populando a lista de retorno (sem popular os filhos)
                    listaRetorno.Add(new EntidadeHierarquiaClassificacaoVO()
                    {
                        Seq = instituicaoTipoEntidadeHierarquiaClassificacao.SeqHierarquiaClassificacao,
                        Descricao = instituicaoTipoEntidadeHierarquiaClassificacao.HierarquiaClassificacao.Descricao,
                        SeqTipoClassificacao = instituicaoTipoEntidadeHierarquiaClassificacao.SeqTipoClassificacao,
                        QuantidadeMaxima = instituicaoTipoEntidadeHierarquiaClassificacao.QuantidadeMaxima,
                        QuantidadeMinima = instituicaoTipoEntidadeHierarquiaClassificacao.QuantidadeMinima
                    })
                );

            return listaRetorno;
        }

        /// <summary>
        /// Busca as hierarquias de classificação disponíveis para a edição, assim como seus filhos cadastrados (se houverem)
        /// </summary>
        /// <param name="seqEntidade"></param>
        /// <returns></returns>
        public List<EntidadeHierarquiaClassificacaoVO> BuscarEntidadeHierarquiasClassificacaoPorNivelEnsino(long seqNivelEnsino, IList<EntidadeClassificacao> classificacoes = null)
        {
            //retorno
            var listaRetorno = new List<EntidadeHierarquiaClassificacaoVO>();

            this.InstituicaoNivelHierarquiaClassificacaoDomainService
                .BuscarInstituicaoNivelHierarquiasClassificacao(seqNivelEnsino)
                .ForEach(instituicaoNivelHierarquiaClassificacao =>

                    //Populando a lista de retorno (sem popular os filhos)
                    listaRetorno.Add(new EntidadeHierarquiaClassificacaoVO()
                    {
                        Seq = instituicaoNivelHierarquiaClassificacao.SeqHierarquiaClassificacao,
                        Descricao = instituicaoNivelHierarquiaClassificacao.HierarquiaClassificacao.Descricao,
                        SeqTipoClassificacao = instituicaoNivelHierarquiaClassificacao.SeqTipoClassificacao,
                        QuantidadeMaxima = Convert.ToInt16(instituicaoNivelHierarquiaClassificacao.QuantidadeMaxima),
                        QuantidadeMinima = Convert.ToInt16(instituicaoNivelHierarquiaClassificacao.QuantidadeMinima),
                        Classificacoes = classificacoes?.Select(s => s.Classificacao).Where(w => w.SeqHierarquiaClassificacao == instituicaoNivelHierarquiaClassificacao.SeqHierarquiaClassificacao)?.TransformList<ClassificacaoVO>()
                    })
                );

            return listaRetorno;
        }

        /// <summary>
        /// Gera uma lista com as hierarquias de classificação de um tipo de entidae populadas com as classificações informadas
        /// </summary>
        /// <param name="seqTipoEntidade">Sequencial do tipo da entidade</param>
        /// <param name="listaEntidadeClassificacoes">Listas de classificações da entidade</param>
        /// <returns>Lista das Hierarquias de Classificação do tipo de entidade com os itens de tipo correspondente da listaEntidadeClassificacoes</returns>
        public List<EntidadeHierarquiaClassificacaoVO> GerarEntidadeClassificacoes(long seqTipoEntidade, IList<EntidadeClassificacao> listaEntidadeClassificacoes)
        {
            var listaHierarquiaClassificacao = new List<EntidadeHierarquiaClassificacaoVO>();

            if (listaEntidadeClassificacoes != null)
            {
                //Monta a lista de hierarquias de classificação selecionáveis para a entidade
                List<EntidadeHierarquiaClassificacaoVO> listaEntidadeHierarquiasClassificacaoData = this.BuscarEntidadeHierarquiasClassificacaoPorTipoEntidade(seqTipoEntidade);

                //Caregando os filhos cadastrados de acordo com as hierarquias (se houvererem)
                listaEntidadeHierarquiasClassificacaoData.ForEach(entidadeHierarquiaClassificacao =>
                {
                    //Instanciando a lista de filhos
                    entidadeHierarquiaClassificacao.Classificacoes = new List<ClassificacaoVO>();

                    //Varrendo as Entidades de Hierarquia (HierarquiaEntidade) em busca das classificações filhas
                    listaEntidadeClassificacoes
                   .Where(w => w.Classificacao.SeqHierarquiaClassificacao == entidadeHierarquiaClassificacao.Seq)
                   .ToList()
                   .ForEach(entidadeClassificacao =>
                   {
                       Classificacao classificacao = entidadeClassificacao.Classificacao;

                       //adição dos filhos
                       entidadeHierarquiaClassificacao.Classificacoes.Add(new ClassificacaoVO()
                       {
                           Seq = classificacao.Seq,
                           CodigoExterno = classificacao.CodigoExterno,
                           Descricao = classificacao.Descricao,
                           SeqHierarquiaClassificacao = entidadeHierarquiaClassificacao.Seq,
                       });
                   });

                    //Adicionando a hierarquia ao retorno
                    listaHierarquiaClassificacao.Add(entidadeHierarquiaClassificacao);
                });
            }

            return listaHierarquiaClassificacao;
        }

        /// <summary>
        /// Converte a lista de HierarquiasClassificações da entidade numa lista de Classificações
        /// </summary>
        /// <param name="entidade">VO da entidade com a hierarquia de classificações</param>
        /// <returns>Lista de clasificações</returns>
        public static List<EntidadeClassificacao> GerarEntidadeClassificacoes(EntidadeVO entidade)
        {
            List<EntidadeClassificacao> listaRetorno = null;
            if (entidade != null && entidade.HierarquiasClassificacoes != null)
            {
                listaRetorno = new List<EntidadeClassificacao>();

                //Varrendo as hierarquias de classificação em busca das classificações
                entidade.HierarquiasClassificacoes.SMCForEach(hierarquiaClassificacao =>
                {
                    //Obtendo as classificacoes
                    if (hierarquiaClassificacao.Classificacoes != null)
                    {
                        hierarquiaClassificacao.Classificacoes.SMCForEach(classificacao => listaRetorno.Add(new EntidadeClassificacao()
                        {
                            SeqClassificacao = classificacao.Seq
                        }));
                    }
                });
            }
            return listaRetorno != null && listaRetorno.Count() > 0 ? listaRetorno : null;
        }

        #endregion [ Classificações da Entidade ]

        #region [Validações]

        public void ValidarNomeEntidadeInstituicaoEnsino(Entidade entidade, string descricaoTipoEntidade)
        {
            // Monta o spec para consulta duplicata
            var specEntidade = new EntidadeFilterSpecification() 
            { 
                SeqTipoEntidade = entidade.SeqTipoEntidade, 
                Nome = entidade.Nome 
            };
            var entidades = this.SearchBySpecification(specEntidade).ToList();

            // Se é inclusão e retornou alguma entidade do banco, erro.
            if (entidade.IsNew() && entidades.Count > 0)
                throw new EntidadeMesmoNomeException(descricaoTipoEntidade.ToLower());

            // Se é alteração e a entidade retornada do banco tem seq diferente, erro
            if (!entidade.IsNew() && entidades.Any(e => e.Seq != entidade.Seq))
                throw new EntidadeMesmoNomeException(descricaoTipoEntidade.ToLower());
        }

        public void ValidarSiglaEntidadeInstituicaoEnsino(Entidade entidade, string descricaoTipoEntidade)
        {
            // Monta o spec para consulta duplicata
            var specEntidade = new EntidadeFilterSpecification() 
            { 
                SeqTipoEntidade = entidade.SeqTipoEntidade, 
                Sigla = entidade.Sigla 
            };
            var entidades = this.SearchBySpecification(specEntidade).ToList();

            // Se é inclusão e retornou alguma entidade do banco, erro.
            if (entidade.IsNew() && entidades.Count > 0)
                throw new EntidadeMesmaSiglaException(descricaoTipoEntidade.ToLower());

            // Se é alteração e a entidade retornada do banco tem seq diferente, erro
            if (!entidade.IsNew() && entidades.Any(e => e.Seq != entidade.Seq))
                throw new EntidadeMesmaSiglaException(descricaoTipoEntidade.ToLower());
        }

        public void ValidarDadosContatoObrigatorios(Entidade entidade)
        {
            // Busca a configuração por instituição x tipo de entidade
            var includeTipo = IncludesInstituicaoTipoEntidade.TiposEndereco
                            | IncludesInstituicaoTipoEntidade.TiposTelefone
                            | IncludesInstituicaoTipoEntidade.TiposEnderecoEletronico;
            var specInstituicaoTipoEntidade = new InstituicaoTipoEntidadeFilterSpecification
            {
                SeqInstituicaoEnsino = entidade.SeqInstituicaoEnsino,
                SeqTipoEntidade = entidade.SeqTipoEntidade,
            };
            var instituicaoTipoEntidade = InstituicaoTipoEntidadeDomainService.SearchByKey(specInstituicaoTipoEntidade, includeTipo);

            // Verifica se os endereços marcados como obrigatório foram informados
            instituicaoTipoEntidade.TiposEndereco.SMCForEach(te =>
            {
                if (te.Obrigatorio && entidade.Enderecos.SMCCount(x => x.TipoEndereco == te.TipoEndereco) == 0)
                    throw new EntidadeEnderecoObrigatorioException(te.TipoEndereco.SMCGetDescription());
            });

            // Verifica se os tipos de telefone marcados como obrigatório foram informados
            instituicaoTipoEntidade.TiposTelefone.SMCForEach(tt =>
            {
                if (tt.Obrigatorio && entidade.Telefones.SMCCount(x => x.TipoTelefone == tt.TipoTelefone && x.CategoriaTelefone == tt.CategoriaTelefone) == 0)
                {
                    var descricao = tt.CategoriaTelefone.HasValue && tt.CategoriaTelefone != Academico.Common.Enums.CategoriaTelefone.Nenhum
                                  ? $"{tt.TipoTelefone.SMCGetDescription()} - {tt.CategoriaTelefone.SMCGetDescription()}"
                                  : tt.TipoTelefone.SMCGetDescription();
                    throw new EntidadeTelefoneObrigatorioException(descricao);
                }
            });

            // Verifica se os tipos de endereço eletrônicos marcados como obrigatórios foram informados
            instituicaoTipoEntidade.TiposEnderecoEletronico.SMCForEach(tee =>
            {
                if (tee.Obrigatorio && entidade.EnderecosEletronicos.SMCCount(x => x.TipoEnderecoEletronico == tee.TipoEnderecoEletronico && x.CategoriaEnderecoEletronico == tee.CategoriaEnderecoEletronico) == 0)
                {
                    var descricao = tee.CategoriaEnderecoEletronico.HasValue && tee.CategoriaEnderecoEletronico != Academico.Common.Enums.CategoriaEnderecoEletronico.Nenhum
                                  ? $"{tee.TipoEnderecoEletronico.SMCGetDescription()} - {tee.CategoriaEnderecoEletronico.SMCGetDescription()}"
                                  : tee.TipoEnderecoEletronico.SMCGetDescription();
                    throw new EntidadeEnderecoEletronicoObrigatorioException(descricao);
                }
            });
        }

        #endregion [Validações]

        /// <summary>
        /// Buscar unidades responsaveis que tenha correspondência com a Unidade Responsável do sistema de Notificação
        /// SeqUnidadeResponsavelNotificacao != null
        /// </summary>
        /// <returns>Lista das unidades responsaveisque tenha correspondência com a Unidade Responsável do sistema de Notificação</returns>
        public List<SMCDatasourceItem> BuscarUnidadesResponsaveisCorrespondenciaNotificacaoSelect()
        {
            var spec = new EntidadeFilterSpecification() { PossuiSeqUnidadeResponsavelNotificacao = true };
            return this.SearchProjectionBySpecification(spec, p => new SMCDatasourceItem()
            {
                Seq = p.Seq,
                Descricao = p.Nome
            }, isDistinct: true).OrderBy(x => x.Descricao).ToList();
        }

        /// <summary>
        /// Recuperar todas entidades superiores com o tipoEntidadeSuperior, superiores a seqEntidadeOrigem na visao tipoVisao
        /// </summary>
        /// <param name="seqEntidadeOrigem">Sequencial da entidade de origem</param>
        /// <param name="tipoVisao">Tipo da visão a ser considerada</param>
        /// <param name="tokenTipoEntidadeSuperior">Tipo das entidades superiores desejadas</param>
        /// <returns>Sequenciais das entidades superiores a entidade de origem com o tipo informada na visão informada</returns>
        public List<long> BuscarSeqsEntidadesSuperiores(long seqEntidadeOrigem, TipoVisao tipoVisao, string tokenTipoEntidadeSuperior)
        {
            var tipoEntidade = TipoEntidadeDomainService.BuscarTipoEntidadeNaInstituicao(tokenTipoEntidadeSuperior);
            return RawQuery<long>(BUSCAR_ENTIDADE_SUPERIOR_HIERARQUIA, new SMCFuncParameter("seq_entidade_origem", seqEntidadeOrigem)
                                                                     , new SMCFuncParameter("tipo_visao", tipoVisao)
                                                                     , new SMCFuncParameter("seq_tipo_entidade_destino", tipoEntidade.Seq));
        }

        /// <summary>
        /// Recupera o código da unidade SEO segundo a RN_GRD_007
        /// </summary>
        /// <param name="seqOrigemAvaliacao">Sequencial da origem de avaliacao</param>
        /// <returns>Código da unidade SEO responsável pelo curso oferta localidade informado</returns>
        public int? RecuperarCodigoUnidadeSeoPorSeqOrigemAvaliacao(long seqOrigemAvaliacao)
        {
            var seqCursoOfertaLocalidade = (long?)OrigemAvaliacaoDomainService.SearchProjectionByKey(seqOrigemAvaliacao, x => x.MatrizCurricularOferta.CursoOfertaLocalidadeTurno.SeqCursoOfertaLocalidade);
            return RecuperarCodigoUnidadeSeo(seqCursoOfertaLocalidade ?? 0);
        }

        /// <summary>
        /// Recupera o código da unidade SEO segundo a RN_GRD_007
        /// </summary>
        /// <param name="seqCursoOfertaLocalidade">Sequencial do curso oferta localidade</param>
        /// <returns>Código da unidade SEO responsável pelo curso oferta localidade informado</returns>
        public int? RecuperarCodigoUnidadeSeo(long seqCursoOfertaLocalidade)
        {
            /*RN_GRD_007  - Buscar Unidade SEO da Localidade da Turma para fins de Grade
            **********OBSERVAÇÃO: Por hora buscar fixo como ind_aula_outro_local = false*******
            Se a turma está configurada com o ind_aula_outro_local = TRUE considerar qualquer local para agendamento da aula e retornar NULL
            Se a turma está configurada com ind_aula_outro_local = FALSE buscar o código de unidade SEO da Localidade do curso x oferta x localidade x turno principal
            da turma e passar e retornar esse código de unidade SEO.
            * Para buscar a localidade do curso x oferta x localidade x turno é preciso consular hierarquia de entidades de visão "Visão de localidades" (tipo visão = 3)
            buscando a entidade que é "pai" do curso x oferta x localidade cujo token do tipo de entidade superior seja UNIDADE_EDUCACIONAL ou UNIDADE_ACADEMICA_ESPECIAL.
            Retornar o codigo unidade SEO desta entidade superior.*/
            int? codigoUnidadeSeo;
            this.DisableFilter(FILTER.HIERARQUIA_ENTIDADE_ORGANIZACIONAL);

            var seqsEntidadesSuperiores = BuscarSeqsEntidadesSuperiores(seqCursoOfertaLocalidade,
                                                                                              TipoVisao.VisaoLocalidades,
                                                                                              TOKEN_TIPO_ENTIDADE.UNIDADE_EDUCACIONAL);
            if (!seqsEntidadesSuperiores.SMCAny())
            {
                seqsEntidadesSuperiores = BuscarSeqsEntidadesSuperiores(seqCursoOfertaLocalidade,
                                                                                              TipoVisao.VisaoLocalidades,
                                                                                              TOKEN_TIPO_ENTIDADE.UNIDADE_ACADEMICA_ESPECIAL);
            }
            codigoUnidadeSeo = seqsEntidadesSuperiores.SMCAny() ?
                this.SearchProjectionByKey(seqsEntidadesSuperiores.First(), p => p.CodigoUnidadeSeo) :
                null;

            this.EnableFilter(FILTER.HIERARQUIA_ENTIDADE_ORGANIZACIONAL);

            return codigoUnidadeSeo;
        }

        public List<SMCDatasourceItem> BsucarEntidadePorTipoEntidade(long seqTipoEntidade)
        {
            var spec = new EntidadeFilterSpecification() { SeqTipoEntidade = seqTipoEntidade };

            var entidades = this.SearchProjectionBySpecification(spec, s => new SMCDatasourceItem()
            {
                Seq = s.Seq,
                Descricao = s.Nome

            }).GroupBy(g=>g.Descricao)
            .Select(s=>s.FirstOrDefault())
            .OrderBy(o=>o.Descricao)
            .ToList();

            return entidades;
        }

        public long BuscarTipoEntidadeporEntidade(long seqEntidade)
        {
            var spec = new EntidadeFilterSpecification() { Seq = seqEntidade };

            var seqTipoEntidade = this.SearchProjectionByKey(spec, s => s.SeqTipoEntidade);

            return seqTipoEntidade;
        }

        public EntidadeRodapeVO BuscarInformacoesRodapeEntidade(string siglaEntidade, string tokenEntidade)
        {
            var entidadeSpec = new EntidadeFilterSpecification() { Sigla = siglaEntidade, Token = tokenEntidade };
            var entidadeRodapeVO = this.SearchProjectionByKey(entidadeSpec,
                e => new EntidadeRodapeVO
                {
                    Nome = e.Nome,
                    Seq = e.Seq,
                    Logradouro = e.Enderecos.Where(end => end.TipoEndereco == TipoEndereco.Comercial).Select(end => end.Logradouro).FirstOrDefault(),
                    NumeroLogradouro = e.Enderecos.Where(end => end.TipoEndereco == TipoEndereco.Comercial).Select(end => end.Numero).FirstOrDefault(),
                    ComplementoLogradouro = e.Enderecos.Where(end => end.TipoEndereco == TipoEndereco.Comercial).Select(end => end.Complemento).FirstOrDefault(),
                    Bairro = e.Enderecos.Where(end => end.TipoEndereco == TipoEndereco.Comercial).Select(end => end.Bairro).FirstOrDefault(),
                    Cep = e.Enderecos.Where(end => end.TipoEndereco == TipoEndereco.Comercial).Select(end => end.Cep).FirstOrDefault(),
                    NomeCidade = e.Enderecos.Where(end => end.TipoEndereco == TipoEndereco.Comercial).Select(end => end.NomeCidade).FirstOrDefault(),
                    SiglaUf = e.Enderecos.Where(end => end.TipoEndereco == TipoEndereco.Comercial).Select(end => end.SiglaUf).FirstOrDefault(),
                    CodigoPais = e.Enderecos.Where(end => end.TipoEndereco == TipoEndereco.Comercial).Select(end => end.CodigoPais).FirstOrDefault(),
                    CodigoAreaTelefone = e.Telefones.Where(t => t.TipoTelefone == TipoTelefone.Comercial).Select(t => t.CodigoArea).FirstOrDefault(),
                    NumeroTelefone = e.Telefones.Where(t => t.TipoTelefone == TipoTelefone.Comercial).Select(t => t.Numero).FirstOrDefault(),
                    Email = e.EnderecosEletronicos.Where(em => em.TipoEnderecoEletronico == TipoEnderecoEletronico.Email).Select(em => em.Descricao).FirstOrDefault()
                });
            
            if (entidadeRodapeVO is null)
                return null;

            entidadeRodapeVO.TrimCamposTexto();

            if (entidadeRodapeVO.CodigoPais > 0)
                entidadeRodapeVO.NomePais = LocalidadeService.BuscarPais(entidadeRodapeVO.CodigoPais)?.Nome;

            if (!string.IsNullOrWhiteSpace(entidadeRodapeVO.SiglaUf))
                entidadeRodapeVO.DescricaoUf = LocalidadeService.BuscarNomeUF(entidadeRodapeVO.SiglaUf);

            return entidadeRodapeVO;
        }
    }
}