using SMC.Academico.Common.Areas.ORG.Exceptions;
using SMC.Academico.Common.Areas.ORG.Includes;
using SMC.Academico.Common.Constants;
using SMC.Academico.Common.Enums;
using SMC.Academico.Domain.Areas.ORG.DomainServices;
using SMC.Academico.Domain.Areas.ORG.Models;
using SMC.Academico.Domain.Areas.ORG.Resources;
using SMC.Academico.Domain.Areas.ORG.Specifications;
using SMC.Academico.Domain.Areas.ORG.ValueObjects;
using SMC.Academico.ServiceContract.Areas.ORG.Data;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Academico.ServiceContract.Data;
using SMC.Framework;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Service;
using SMC.Framework.Specification;
using SMC.Localidades.Common.Areas.LOC.Enums;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;

namespace SMC.Academico.Service.Areas.ORG.Services
{
    public class EntidadeService : SMCServiceBase, IEntidadeService
    {
        #region Serviços

        private EntidadeDomainService EntidadeDomainService
        {
            get { return this.Create<EntidadeDomainService>(); }
        }

        private TipoEntidadeDomainService TipoEntidadeDomainService
        {
            get { return this.Create<TipoEntidadeDomainService>(); }
        }

        private InstituicaoTipoEntidadeDomainService InstituicaoTipoEntidadeDomainService
        {
            get { return this.Create<InstituicaoTipoEntidadeDomainService>(); }
        }

        private Calendarios.ServiceContract.Areas.CLD.Interfaces.IUnidadeResponsavelService UnidadeResponsavelAGDService
        {
            get { return this.Create<Calendarios.ServiceContract.Areas.CLD.Interfaces.IUnidadeResponsavelService>(); }
        }

        private Inscricoes.ServiceContract.Areas.RES.Interfaces.IUnidadeResponsavelService UnidadeResponsavelGPIService
        {
            get { return this.Create<SMC.Inscricoes.ServiceContract.Areas.RES.Interfaces.IUnidadeResponsavelService>(); }
        }

        private Notificacoes.ServiceContract.Areas.NTF.Interfaces.INotificacaoService UnidadeResponsavelNotificacoesService
        {
            get { return this.Create<SMC.Notificacoes.ServiceContract.Areas.NTF.Interfaces.INotificacaoService>(); }
        }

        private Formularios.ServiceContract.Areas.FRM.Interfaces.IUnidadeResponsavelService UnidadeResponsavelFormulariosService
        {
            get { return this.Create<SMC.Formularios.ServiceContract.Areas.FRM.Interfaces.IUnidadeResponsavelService>(); }
        }

        #endregion Serviços

        public EntidadeData BuscarEntidade(long seq)
        {
            return this.EntidadeDomainService.BuscarEntidade(seq).Transform<EntidadeData>();
        }

        public EntidadeCabecalhoData BuscarEntidadeCabecalho(long seq)
        {
            var spec = new EntidadeFilterSpecification() { Seq = seq };
            var entidadeCabecalho = EntidadeDomainService.SearchProjectionByKey(spec, p => new EntidadeCabecalhoData() { Seq = p.Seq, Nome = p.Nome, Sigla = p.Sigla });
            return entidadeCabecalho;
        }

        /// <summary>
        /// Buscar uma lista de entidades
        /// </summary>
        /// <param name="filtros">Filtros para pesquisa</param>
        /// <returns>Lista de entidades</returns>
        public SMCPagerData<EntidadeListaData> BuscarEntidades(EntidadeFiltroData filtros)
        {
            return this.EntidadeDomainService.BuscarEntidades(filtros.Transform<EntidadeFiltroVO>()).Transform<SMCPagerData<EntidadeListaData>>();
        }

        /// <summary>
        /// Buscar uma lista de entidades para o lookup de entidades seleção multipla
        /// </summary>
        /// <param name="filtros">Filtros para pesquisa</param>
        /// <returns>Lista de entidades</returns>
        public SMCPagerData<EntidadeListaData> BuscarEntidadesLookupSelecaoMultipla(EntidadeFiltroData filtros)
        {
            return this.EntidadeDomainService.BuscarEntidadesLookupSelecaoMultipla(filtros.Transform<EntidadeFiltroVO>()).Transform<SMCPagerData<EntidadeListaData>>();
        }

        /// <summary>
        /// Buscar uma lista de entidades para lookup
        /// </summary>
        /// <param name="filtros">Filtros para pesquisa</param>
        /// <returns>Lista de entidades ordenadas pela descrição do tipo e nome da entidade</returns>
        public SMCPagerData<EntidadeListaData> BuscarEntidadesLookup(long seq)
        {
            return this.EntidadeDomainService.BuscarEntidadesLookup(seq).Transform<SMCPagerData<EntidadeListaData>>();
        }

        /// <summary>
        /// Busca todas as entidades que sejam de tipos vinculáveis aos colaboradores na instituição atual.
        /// </summary>
        /// <param name="ignorarFiltros">Quando setado ignora todos filtros exceto o filtro de instituição</param>
        /// <returns>Sequenciais e nomes das entidades encontradas</returns>
        public List<SMCDatasourceItem> BuscarEntidadesVinculoColaboradorSelect(bool ignorarFiltros)
        {
            return this.EntidadeDomainService.BuscarEntidadesVinculoColaboradorSelect(ignorarFiltros);
        }

        public void ExcluirEntidade(long seqEntidade)
        {
            this.EntidadeDomainService.DeleteEntity(seqEntidade);
        }

        public long SalvarEntidade(EntidadeData entidade)
        {
            return this.EntidadeDomainService.SalvarEntidade(entidade.Transform<EntidadeVO>());
        }

        /// <summary>
        /// Busca as configurações de entidade para cadastro de uma entidade externada
        /// </summary>
        /// <param name="tokenEntidadeExternada">Token do tipo de entidade externada</param>
        /// <returns>Dados da configuração de entidade</returns>
        public EntidadeData BuscaConfiguracoesEntidadeExternada(string tokenEntidadeExternada)
        {
            TipoEntidade tipoEntidade = TipoEntidadeDomainService.BuscarTipoEntidadeNaInstituicao(tokenEntidadeExternada);

            // Verifica a existência da configuração do tipo de entidade na instituição de ensino
            // (separado da recuperação de depêndencias para não invalidar o filtro global)
            var specConfig = new InstituicaoTipoEntidadeFilterSpecification { SeqTipoEntidade = tipoEntidade.Seq };
            var includesConfig = IncludesInstituicaoTipoEntidade.InstituicaoEnsino;
            var config = InstituicaoTipoEntidadeDomainService.SearchByKey(specConfig, includesConfig);

            // Se não encontrou a configuração, erro!
            if (config == null)
            {
                switch (tokenEntidadeExternada)
                {
                    case TOKEN_TIPO_ENTIDADE_EXTERNADA.PROGRAMA: throw new TipoEntidadeNaoConfiguradaException(MessagesResource.TIPO_ENTIDADE_Programa);
                    case TOKEN_TIPO_ENTIDADE_EXTERNADA.CURSO: throw new TipoEntidadeNaoConfiguradaException(MessagesResource.TIPO_ENTIDADE_Curso);
                    case TOKEN_TIPO_ENTIDADE_EXTERNADA.CURSO_UNIDADE: throw new TipoEntidadeNaoConfiguradaException(MessagesResource.TIPO_ENTIDADE_CursoUnidade);
                    case TOKEN_TIPO_ENTIDADE_EXTERNADA.CURSO_OFERTA_LOCALIDADE: throw new TipoEntidadeNaoConfiguradaException(MessagesResource.TIPO_ENTIDADE_CursoOfertaLocalidade);
                    case TOKEN_TIPO_ENTIDADE_EXTERNADA.INSTITUICAO_ENSINO: throw new TipoEntidadeNaoConfiguradaException(MessagesResource.TIPO_ENTIDADE_InstituicaoEnsino);
                    default: throw new TipoEntidadeNaoAssociadoInstituicaoException();
                }
            }

            // Busca as configurações do tipo de entidade programa para a instituição de ensino
            includesConfig = IncludesInstituicaoTipoEntidade.TiposEndereco
                           | IncludesInstituicaoTipoEntidade.TiposTelefone
                           | IncludesInstituicaoTipoEntidade.TiposEnderecoEletronico;
            config = InstituicaoTipoEntidadeDomainService.SearchByKey(new SMCSeqSpecification<InstituicaoTipoEntidade>(config.Seq), includesConfig);

            // Cria o dto de retorno
            var entidadeData = new EntidadeData();
            entidadeData.SeqTipoEntidade = tipoEntidade.Seq;
            entidadeData.DescricaoTipoEntidade = tipoEntidade.Descricao;

            // Atualiza as informações de obrigatoriedade e visibilidade da entidade
            entidadeData.SeqInstituicaoEnsino = config.SeqInstituicaoEnsino;
            entidadeData.NomeReduzidoObrigatorio = config.NomeReduzidoObrigatorio.GetValueOrDefault();
            entidadeData.NomeReduzidoVisivel = config.NomeReduzidoVisivel;
            entidadeData.SiglaObrigatoria = config.SiglaObrigatoria.GetValueOrDefault();
            entidadeData.SiglaVisivel = config.SiglaVisivel;
            entidadeData.UnidadeSeoVisivel = config.UnidadeSeoVisivel;
            entidadeData.UnidadeSeoObrigatorio = config.UnidadeSeoObrigatorio.GetValueOrDefault();
            entidadeData.LogotipoObrigatorio = config.LogotipoObrigatorio.GetValueOrDefault();
            entidadeData.LogotipoVisivel = config.LogotipoVisivel;
            entidadeData.NomeComplementarObrigatorio = config.NomeComplementarObrigatorio.GetValueOrDefault();
            entidadeData.NomeComplementarVisivel = config.NomeComplementarVisivel;
            entidadeData.UnidadeAgdVisivel = config.UnidadeAgdVisivel;
            entidadeData.UnidadeAgdObrigatorio = config.UnidadeAgdObrigatorio.GetValueOrDefault();
            entidadeData.UnidadeGpiVisivel = config.UnidadeGpiVisivel;
            entidadeData.UnidadeGpiObrigatorio = config.UnidadeGpiObrigatorio.GetValueOrDefault();
            entidadeData.UnidadeNotificacaoVisivel = config.UnidadeNotificacaoVisivel;
            entidadeData.UnidadeNotificacaoObrigatorio = config.UnidadeNotificacaoObrigatorio.GetValueOrDefault();
            entidadeData.UnidadeFormularioVisivel = config.UnidadeFormularioVisivel;
            entidadeData.UnidadeFormularioObrigatorio = config.UnidadeFormularioObrigatorio.GetValueOrDefault();

            // Inicializa as listas de tipo com vazio para garantir que os componentes de endereços e telefones não sobrescrevam os tipos
            entidadeData.TiposEnderecos = new List<TipoEndereco>();
            entidadeData.TiposTelefone = new List<SMCDatasourceItem<string>>();
            entidadeData.TiposEnderecoEletronico = new List<SMCDatasourceItem<string>>();

            // Verifica obrigatoriedade de endereço
            if ((entidadeData.HabilitaEnderecos = config.TiposEndereco.SMCCount() > 0))
            {
                entidadeData.Enderecos = new List<EnderecoData>();
                foreach (var i in config.TiposEndereco)
                {
                    if (i.Obrigatorio)
                        entidadeData.Enderecos.Add(new EnderecoData() { TipoEndereco = i.TipoEndereco });
                    entidadeData.TiposEnderecos.Add(i.TipoEndereco);
                }
            }

            // Verifica obrigatoriedade de telefone
            if ((entidadeData.HabilitaTelefones = config.TiposTelefone.SMCCount() > 0))
            {
                entidadeData.Telefones = new List<TelefoneData>();

                // Cria lista com tipos de telefones
                foreach (var tipoTelefone in config.TiposTelefone)
                {
                    var descricao = tipoTelefone.CategoriaTelefone.HasValue && tipoTelefone.CategoriaTelefone != CategoriaTelefone.Nenhum
                                ? $"{tipoTelefone.TipoTelefone.SMCGetDescription()} - {tipoTelefone.CategoriaTelefone.SMCGetDescription()}"
                                : tipoTelefone.TipoTelefone.SMCGetDescription();

                    if (tipoTelefone.Obrigatorio)
                        entidadeData.Telefones.Add(new TelefoneData() { DescricaoTipoTelefone = descricao });

                    SMCDatasourceItem<string> novoTelefone = new SMCDatasourceItem<string>()
                    {
                        Descricao = descricao,
                        Seq = descricao
                    };
                    entidadeData.TiposTelefone.Add(novoTelefone);
                }
                entidadeData.TiposTelefone = entidadeData.TiposTelefone.OrderBy(t => t.Descricao).ToList();
            }

            // Verifica obrigatoriedade de endereço eletrônico
            if ((entidadeData.HabilitaEnderecosEletronicos = config.TiposEnderecoEletronico.SMCCount() > 0))
            {
                entidadeData.EnderecosEletronicos = new List<EnderecoEletronicoData>();

                // Cria a lista com os tipos de endereço eletrônico
                foreach (var tipoEE in config.TiposEnderecoEletronico)
                {
                    var descricao = tipoEE.CategoriaEnderecoEletronico.HasValue && tipoEE.CategoriaEnderecoEletronico != CategoriaEnderecoEletronico.Nenhum
                                    ? $"{tipoEE.TipoEnderecoEletronico.SMCGetDescription()} - {tipoEE.CategoriaEnderecoEletronico.SMCGetDescription()}"
                                    : tipoEE.TipoEnderecoEletronico.SMCGetDescription();

                    if (tipoEE.Obrigatorio)
                        entidadeData.EnderecosEletronicos.Add(new EnderecoEletronicoData() { DescricaoTipoEnderecoEletronico = descricao });

                    SMCDatasourceItem<string> novotipoEE = new SMCDatasourceItem<string>()
                    {
                        Descricao = descricao,
                        Seq = descricao
                    };
                    entidadeData.TiposEnderecoEletronico.Add(novotipoEE);
                }
                entidadeData.TiposEnderecoEletronico = entidadeData.TiposEnderecoEletronico.OrderBy(t => t.Descricao).ToList();
            }

            return entidadeData;
        }

        /// <summary>
        /// Busca as configurações de um tipo de entidade com as configurações de classificação
        /// </summary>
        /// <param name="tokenEntidade">Token do tipo da entidade</param>
        /// <returns>Configurações da entidade com a lista de hierarquias de entidades</returns>
        public EntidadeData BuscarConfiguracoesEntidadeComClassificacao(string tokenEntidade)
        {
            var entidadeData = this.BuscaConfiguracoesEntidadeExternada(tokenEntidade);

            entidadeData.HierarquiasClassificacoes = this.EntidadeDomainService
                .GerarEntidadeClassificacoes(entidadeData.SeqTipoEntidade, new List<EntidadeClassificacao>())
                .TransformList<EntidadeHierarquiaClassificacaoData>();

            return entidadeData;
        }

        /// <summary>
        /// Busca as hierarquias de classificação por nivel de ensino
        /// </summary>
        /// <param name="seqNivelEnsino">Seq do nível de ensino</param>
        /// <returns>Lista de hierarquias de entidade</returns>
        public List<EntidadeHierarquiaClassificacaoData> BuscarHierarquiaClassificacaoPorNivelEnsino(long seqNivelEnsino)
        {
            var hierarquiasData = this.EntidadeDomainService
            .BuscarEntidadeHierarquiasClassificacaoPorNivelEnsino(seqNivelEnsino)
            .TransformList<EntidadeHierarquiaClassificacaoData>();
            return hierarquiasData;
        }

        public List<SMCDatasourceItem> BuscarUnidadesResponsaveisAGDSelect()
        {
            return this.UnidadeResponsavelAGDService.BuscarUnidadesResponsaveisSelect();
        }

        /// <summary>
        /// Busca todas as unidades responsáveis no sistema GPI
        /// </summary>
        /// <returns>Lista com as unidades responsáveis do GPI</returns>
        public List<SMCDatasourceItem> BuscarUnidadesResponsaveisGPISelect()
        {
            return UnidadeResponsavelGPIService.BuscarUnidadesResponsaveisKeyValue();
        }

        /// <summary>
        /// Busca todas as unidades responsáveis no sistema de notificações
        /// </summary>
        /// <returns>Lista com as unidades responsáveis do sistema de notificações</returns>
        public List<SMCDatasourceItem> BuscarUnidadesResponsaveisNotificacaoSelect()
        {
            return UnidadeResponsavelNotificacoesService.BuscarUnidadesResponsaveisKeyValue().TransformList<SMCDatasourceItem>();
        }

        public List<SMCDatasourceItem> BuscarUnidadesResponsaveisFormularioSelect()
        {
            return UnidadeResponsavelFormulariosService.BuscarUnidadesResponsaveisSelect().TransformList<SMCDatasourceItem>();
        }

        public List<SMCDatasourceItem> BuscarUnidadesResponsaveisGPILocalSelect()
        {
            return EntidadeDomainService.BuscarUnidadesResponsaveisGPISelect();
        }

        public List<SMCDatasourceItem> BuscarEntidadesSelect(string token, long seqInstituicaoLogada)
        {
            return EntidadeDomainService.BuscarEntidadesSelect(token, seqInstituicaoLogada);
        }

        public List<SMCDatasourceItem> BuscarGruposProgramasSelect()
        {
            return EntidadeDomainService.BuscarGruposProgramasSelect();
        }

        /// <summary>
        /// Listar departamento do grupo de programa para select
        /// </summary>
        /// <param name="usarNomeReduzido">Se irá usar o nome reduzido caso exista</param>
        /// <returns>Lista SMCDatasourceItem</returns>
        public List<SMCDatasourceItem> BuscarDepartamentosGruposProgramasSelect(bool usarNomeReduzido = false)
        {
            return EntidadeDomainService.BuscarDepartamentosGruposProgramasSelect(usarNomeReduzido);
        }

        public Dictionary<long, string> BuscarEntidadesNomes(List<long> seqsEntidades)
        {
            return EntidadeDomainService.BuscarEntidadesNomes(seqsEntidades);
        }

        /// <summary>
        /// Buscar unidades responsaveis que tenha correspondência com a Unidade Responsável do sistema de Notificação
        /// SeqUnidadeResponsavelNotificacao != null
        /// </summary>
        /// <returns>Lista das unidades responsaveisque tenha correspondência com a Unidade Responsável do sistema de Notificação</returns>
        public List<SMCDatasourceItem> BuscarUnidadesResponsaveisCorrespondenciaNotificacaoSelect()
        {
            return EntidadeDomainService.BuscarUnidadesResponsaveisCorrespondenciaNotificacaoSelect();
        }

        /// <summary>
        /// Recupera o código da unidade SEO segundo a RN_GRD_007
        /// </summary>
        /// <param name="seqOrigemAvaliacao">Sequencial da origem de avaliacao</param>
        /// <returns>Código da unidade SEO responsável pelo curso oferta localidade informado</returns>
        public int? RecuperarCodigoUnidadeSeoPorSeqOrigemAvaliacao(long seqOrigemAvaliacao)
        {
            return EntidadeDomainService.RecuperarCodigoUnidadeSeoPorSeqOrigemAvaliacao(seqOrigemAvaliacao);
        }

        public List<SMCDatasourceItem> BuscarEntidadePorTipoEntidade(long seqTipoEntidade)
        {
            return EntidadeDomainService.BsucarEntidadePorTipoEntidade(seqTipoEntidade);
        }

        public string BuscarEntidadeNome(long seqEntidade)
        {
            return EntidadeDomainService.BuscarEntidadeNome(seqEntidade);
        }

        /// <summary>
        /// Lista as entidades responsáveis na visão organizacional para select
        /// </summary>
        /// <param name="listarApenasEntidadesAtivas">Listar apenas as entidades ativas?</param>
        /// <param name="usarNomeReduzido">Indica se a listagem deve apresentar o nome reduzido ou completo da entidade</param>
        /// <returns>Lista de entidades responsáveis</returns>
        public List<SMCDatasourceItem> BuscarEntidadesResponsaveisVisaoOrganizacionalSelect(bool listarApenasEntidadesAtivas = false, bool usarNomeReduzido = false)
        {
            return EntidadeDomainService.BuscarEntidadesResponsaveisVisaoOrganizacionalSelect(listarApenasEntidadesAtivas, usarNomeReduzido);
        }

        public EntidadeRodapeData BuscarInformacoesRodapeEntidade(string siglaEntidade, string tokenEntidade)
        {
            return EntidadeDomainService.BuscarInformacoesRodapeEntidade(siglaEntidade, tokenEntidade).Transform<EntidadeRodapeData>();
        }
    }
}