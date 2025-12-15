using SMC.Academico.Domain.Areas.ALN.DomainServices;
using SMC.Academico.Domain.Areas.ALN.Models;
using SMC.Academico.Domain.Areas.ALN.Specifications;
using SMC.Academico.Domain.Areas.ALN.ValueObjects;
using SMC.Academico.Domain.Areas.MAT.Models;
using SMC.Academico.Domain.Areas.PES.DomainServices;
using SMC.Academico.ServiceContract.Areas.ALN.Data;
using SMC.Academico.ServiceContract.Areas.ALN.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Service;
using SMC.Framework.Specification;
using SMC.Inscricoes.ServiceContract.Areas.INS.Data;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Service.Areas.ALN.Services
{
    public class IngressanteService : SMCServiceBase, IIngressanteService
    {
        
        private PessoaAtuacaoDomainService PessoaAtuacaoDomainService
        {
            get { return this.Create<PessoaAtuacaoDomainService>(); }
        }

        private IngressanteDomainService IngressanteDomainService
        {
            get { return this.Create<IngressanteDomainService>(); }
        }

        private InstituicaoNivelTipoVinculoAlunoDomainService InstituicaoNivelTipoVinculoAlunoDomainService
        {
            get { return this.Create<InstituicaoNivelTipoVinculoAlunoDomainService>(); }
        }

        /// <summary>
        /// Busca as atuações de ingressante de uma pessoa com os dados pessoais
        /// </summary>
        /// <param name="filtro">Filtro com o seq da pessoa</param>
        /// <returns>Dados de ingressante da pessoa informada</returns>
        public IngressanteData[] BuscarIngressantesPessoa(IngressanteFiltroData filtro)
        {
            var spec = filtro.Transform<IngressanteFilterSpecification>();
            return IngressanteDomainService.BuscarIngressantesPessoa(spec).TransformListToArray<IngressanteData>();
        }

        /// <summary>
        /// Busca um ingressante com suas dependências
        /// </summary>
        /// <param name="seq">Sequencial do ingressante</param>
        /// <returns>Dados do ingressante com suas dependências</returns>
        public IngressanteData BuscarIngressante(long seq)
        {
            return IngressanteDomainService.BuscarIngressante(seq).Transform<IngressanteData>();
        }

        public long BuscarSeqSolicitacaoMatricula(long seqIngressante)
        {
            return IngressanteDomainService.SearchProjectionByKey(new SMCSeqSpecification<Ingressante>(seqIngressante), x => x.SolicitacoesServico.OfType<SolicitacaoMatricula>().FirstOrDefault().Seq);
        }

        public List<SMCDatasourceItem> BuscarIngressantesSelect(IngressanteFiltroData filtro)
        {
            var spec = filtro.Transform<IngressanteFilterSpecification>();
            return IngressanteDomainService.BuscarIngressantesSelect(spec);
        }

        public IngressanteCabecalhoData BuscarCabecalhoIngressantes(long seqIngressante)
        {
            return IngressanteDomainService.BuscarCabecalhoIngressantes(seqIngressante).Transform<IngressanteCabecalhoData>();
        }

        public SMCPagerData<IngressanteProcessoSeletivoListaData> BuscarIngressantesLista(IngressanteFiltroData filtro)
        {
            var spec = filtro.Transform<IngressanteFilterSpecification>();
            var dados = IngressanteDomainService.BuscarIngressantesLista(spec).TransformList<IngressanteProcessoSeletivoListaData>();
            return new SMCPagerData<IngressanteProcessoSeletivoListaData>(dados, dados.Count);
        }

        /// <summary>
        /// Busca os ingressantes com as depêndencias apresentadas na listagem do seu cadastro
        /// </summary>
        /// <param name="filtro">Filtros do ingressante</param>
        /// <returns>Dados paginados de ingressante</returns>
        public SMCPagerData<IngressanteListaData> BuscarIngressantes(IngressanteFiltroData filtro)
        {
            return IngressanteDomainService.BuscarIngressantes(filtro.Transform<IngressanteFilterSpecification>()).Transform<SMCPagerData<IngressanteListaData>>();
        }

        /// <summary>
        /// Busca os dados acadêmicos de um ingressante
        /// </summary>
        /// <param name="seq">Sequencial do ingressante</param>
        /// <returns>Dados paginados de ingressante</returns>
        public IngressanteListaData BuscarDadosAcademicosIngressante(long seq)
        {
            return IngressanteDomainService.BuscarDadosAcademicosIngressante(seq).Transform<IngressanteListaData>();
        }

        public long BuscarSeqInstituicaoNivelEnsinoPorIngressante(long seqIngressante)
        {
            return PessoaAtuacaoDomainService.BuscarInstituicaoNivelEnsinoESequenciaisPorPessoaAtuacao(seqIngressante).SeqInstituicaoNivelEnsino;
        }

        public AssociacaoIngressanteLoteCabecalhoData BuscarCabecalhoAssociacaoIngressanteLote(long seqIngressante)
        {
            var result = this.IngressanteDomainService.BuscarCabecalhoAssociacaoIngressanteLote(new long[] { seqIngressante });

            return result.First().Transform<AssociacaoIngressanteLoteCabecalhoData>();
        }

        public AssociacaoFormacaoEspecificaIngressanteData BuscarAssociacaoFormacoesEspecificasIngressante(long seqIngressante)
        {
            var result = this.IngressanteDomainService.BuscarAssociacaoFormacoesEspecificasIngressante(seqIngressante);

            return result.Transform<AssociacaoFormacaoEspecificaIngressanteData>();
        }

        public long SalvarAssociacaoFormacaoEspecíficaIngressante(long seqInstituicao, AssociacaoFormacaoEspecificaIngressanteData modelo)
        {
            return this.IngressanteDomainService.SalvarAssociacaoFormacaoEspecificaIngressante(seqInstituicao, modelo.Transform<AssociacaoFormacaoEspecificaIngressanteVO>());
        }

        public AssociacaoOrientadorIngressanteData BuscarAssociacaoOrientadorIngressante(long seqIngressante)
        {
            var result = this.IngressanteDomainService.BuscarAssociacaoOrientadorIngressante(seqIngressante);

            return result.Transform<AssociacaoOrientadorIngressanteData>();
        }

        public long SalvarAssociacaoOrientadorIngressante(AssociacaoOrientadorIngressanteData modelo)
        {
            return this.IngressanteDomainService.SalvarAssociacaoOrientadorIngressante(modelo.Transform<AssociacaoOrientadorIngressanteVO>());
        }

        public List<AssociacaoOrientadorIngressanteItemData> BuscarOrientacoesIngressante(AssociacaoOrientadorIngressanteData modelo)
        {
            return this.IngressanteDomainService.BuscarAssociacaoOrientadorIngressante(modelo.SeqIngressante, modelo.SeqTipoOrientacao)
                .Orientacoes
                .ToList()
                .TransformList<AssociacaoOrientadorIngressanteItemData>();
        }

        /// <summary>
        /// Aplica a validação da regra RN_ALN_031  Consistência Vínculo Tipo Termo Intercâmbio
        /// </summary>
        /// <param name="ingressante">Dados do ingressante com nível de ensino, tipo de vínculo e termo de intercâmbio</param>
        /// <returns>Verdaderio caso a regra 31 ocorra</returns>
        public bool ConsistenciaVinculoTipoTermoIntercambio(IngressanteData ingressante)
        {
            return this.IngressanteDomainService.ConsistenciaVinculoTipoTermoIntercambio(ingressante.Transform<IngressanteVO>());
        }

        /// <summary>
        /// Grava o ingressante e suas dependências
        /// </summary>
        /// <param name="ingressante">Dados do ingressante e de suas depêndencias</param>
        /// <returns>Sequencial do ingressante gravado</returns>
        public long SalvarIngressante(IngressanteData ingressante)
        {
            return IngressanteDomainService.PrepararIngressanteGravacao(ingressante.Transform<IngressanteVO>());
        }

        /// <summary>
        /// Valida a configuração da atuação de ingressante na instituição e retorna um novo ingressante caso a atuação esteja configurada
        /// </summary>
        /// <returns>Novo ingressante caso esteja configurado</returns>
        /// <exception cref="InstituicaoTipoAtuacaoNaoConfiguradaException">Caso o tipo de atuação não esteja configurado na instituição</exception>
        public IngressanteData BuscarConfiguracaoIngressante()
        {
            return IngressanteDomainService.BuscarConfiguracaoIngressante().Transform<IngressanteData>();
        }

        /// <summary>
        /// Valida se o ingressante têm impedimento segundo a regra RN_ALN_003 - Consistência situação ingressante
        /// </summary>
        /// <param name="seq">Sequencial do ingressante</param>
        /// <returns>Verdadeiro caso a situação do ingressante não permita alterações</returns>
        public bool ValidarSituacaoImpeditivaIngressante(long seq)
        {
            return IngressanteDomainService.ValidarSituacaoImpeditivaIngressante(seq);
        }

        /// <summary>
        /// Validação de contatos segundo as regras:
        /// RN_ALN_006 Consistência endereço
        /// RN_ALN_008 Consistência endereço eletrônico
        /// </summary>
        /// <param name="ingressante">Ingressante com os contatos</param>
        /// <exception cref="AtuacaoSemEnderecoException">Caso a regra RN_ALN_006 não seja atendida</exception>
        /// <exception cref="AtuacaoSemEmailException">Caso a regra RN_ALN_008 não seja atendida</exception>
        public void ValidarContatosIngressante(IngressanteData ingressante)
        {
            IngressanteDomainService.ValidarContatosIngressante(ingressante.Transform<IngressanteVO>());
        }

        /// <summary>
        /// Valida se se nenhum item do grupo de escalonamento já expirou
        /// </summary>
        /// <param name="seqGrupoEscalonamento">Sequencial do grupo de escalonamento</param>
        /// <exception cref="GrupoEscalonamentoExpiradoException">Caso o grupo de escalonamento tenha um item expirado</exception>
        public void ValidarGrupoEscalonamentoIngressante(long seqGrupoEscalonamento)
        {
            IngressanteDomainService.ValidarGrupoEscalonamentoIngressante(seqGrupoEscalonamento);
        }

        /// <summary>
        /// Valida se os termos associados ao ingressante são compatíveis
        /// </summary>
        /// <param name="ingressante">Dados do ingressante</param>
        /// <exception cref="IngressanteTermoIntercambioTipoDiferenteException">Caso os termos tenham mais de um tipo</exception>
        /// <exception cref="IngressanteTermoIntercambioParceriaDiferenteException">Caso os termos tenham mais de uma parceria</exception>
        /// <exception cref="IngressanteTermoIntercambioIntervaloVigenciaException">Caso exista algum intervalo sem vigência entre os termos</exception>
        public void ValidarTermosIntercambioIngressante(IngressanteData ingressante)
        {
            IngressanteDomainService.ValidarTermosIntercambioIngressante(ingressante.Transform<IngressanteVO>());
        }

        /// <summary>
        /// Realiza as validações de acordo com RN_ALN_043 e libera o ingressante para realizar a matrícula
        /// </summary>
        /// <param name="seq">Sequencial do ingressante</param>
        /// <exception cref="LiberacaoIngressaneInvalidaException">Caso o ingressante tenha origem ou situação inválida</exception>
        /// <exception cref="OrientacaoConvocadosException">Caso alguma orientação requerida não seja configurada</exception>
        /// <exception cref="SMCApplicationException">Caso o grupo de escalonamento do ingressante esteja expirado ou uma formação específica requerida não seja associada</exception>
        public void LiberarIngressanteMatricula(long seq)
        {
            IngressanteDomainService.LiberarIngressanteMatricula(seq);
        }

        /// <summary>
        /// Valida as orientações exigidas e participações de orientações obrigatórias segundo o nível de ensino e tipo de vínculo do ingressante
        /// </summary>
        /// <param name="ingressanteData">Ingressante com o nível de ensino, tipo de vínculo e orintações</param>
        /// <exception cref="OrientacoesExigidasPorNivelEnsinoException">Caso algum tipo de orientação exigido não seja informado</exception>
        /// <exception cref="TiposParticipacaoOrientacaoObrigatoriosException">Caso algum tipo de orientação obrigatória não seja informada</exception>
        /// <exception cref="OrigemParticipacaoOrientcaoInvalidaException">Caso alguma participação de orientação esteja associada ao tipo errado de instituição</exception>
        public void ValidarOrientacaoIngressante(IngressanteData ingressanteData)
        {
            IngressanteDomainService.ValidarOrientacaoIngressante(ingressanteData.Transform<IngressanteVO>());
        }

        /// <summary>
        /// Valida a quantidade de vagas por oferta para o ingressante caso o vínculo não exija curso
        /// </summary>
        /// <param name="ingressanteVO">Dados do ingressante</param>
        /// <exception cref="OfertasSemVagasException">Caso alguma das ofertas validadas não tenha vagas</exception>
        public void ValidarVagasOfertasDisciplinaIsoladaIngressante(IngressanteData ingressanteData)
        {
            IngressanteDomainService.ValidarVagasOfertasDisciplinaIsoladaIngressante(ingressanteData.Transform<IngressanteVO>());
        }

        /// <summary>
        /// Exclui um ingressante e suas dependências
        /// </summary>
        /// <param name="seq">Sequencial do ingressante</param>
        public void ExcluirIngressante(long seq)
        {
            IngressanteDomainService.ExcluirIngressante(seq);
        }

        /// <summary>
        /// Processa um inscrito gerando um novo registro de ingressante e marcando o inscrito como importado no GPI
        /// </summary>
        /// <param name="inscrito">Dados do inscrito</param>
        /// <param name="seqEntidadeInstituicao">Sequencial da instituição</param>
        /// <param name="seqChamada">Sequencial da chamada</param>
        public void ProcessarInscrito(PessoaIntegracaoData inscrito, long seqEntidadeInstituicao, long seqChamada)
        {
            IngressanteDomainService.ProcessarInscrito(inscrito, seqEntidadeInstituicao, seqChamada);
        }

        /// <summary>
        /// Valida a quantidade de vagas por oferta para o ingressante caso o vínculo não exija curso
        /// </summary>
        /// <param name="ingressanteData">Dados do ingressante</param>
        /// <exception cref="OfertasSemVagasException">Caso alguma das ofertas validadas não tenha vagas</exception>
        public void ValidarBloqueioPessoa(IngressanteData ingressanteData)
        {
            IngressanteDomainService.ValidarBloqueioPessoa(ingressanteData.Transform<IngressanteVO>());
        }
    }
}