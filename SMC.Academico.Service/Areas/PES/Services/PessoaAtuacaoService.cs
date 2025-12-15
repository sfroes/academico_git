using SMC.Academico.Common.Areas.PES.Includes;
using SMC.Academico.Domain.Areas.PES.DomainServices;
using SMC.Academico.Domain.Areas.PES.Models;
using SMC.Academico.Domain.Areas.PES.Specifications;
using SMC.Academico.Domain.Areas.PES.ValueObjects;
using SMC.Academico.Domain.Helpers;
using SMC.Academico.ServiceContract.Areas.ALN.Data;
using SMC.Academico.ServiceContract.Areas.PES.Data;
using SMC.Academico.ServiceContract.Areas.PES.Interfaces;
using SMC.DadosMestres.ServiceContract.Areas.GED.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Service;
using SMC.Framework.Specification;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Service.Areas.PES.Services
{
    public class PessoaAtuacaoService : SMCServiceBase, IPessoaAtuacaoService
    {
        #region DomainService

        private PessoaAtuacaoDomainService PessoaAtuacaoDomainService
        {
            get { return this.Create<PessoaAtuacaoDomainService>(); }
        }

        private ITipoDocumentoService TipoDocumentoService
        {
            get { return this.Create<ITipoDocumentoService>(); }
        }

        #endregion DomainService

        public PessoaAtuacaoData BuscarPessoaAtuacao(long seq)
        {
            try
            {
                FilterHelper.DesativarFiltros(PessoaAtuacaoDomainService);
                var includes = IncludesPessoaAtuacao.DadosPessoais
                             | IncludesPessoaAtuacao.Pessoa;
                return PessoaAtuacaoDomainService.SearchByKey(new SMCSeqSpecification<PessoaAtuacao>(seq), includes).Transform<PessoaAtuacaoData>();
            }
            finally
            {
                FilterHelper.AtivarFiltros(PessoaAtuacaoDomainService);
            }
        }

        public SMCPagerData<PessoaAtuacaoListaData> BuscarPessoaAtuacoes(PessoaAtuacaoFiltroData filtro)
        {
            return PessoaAtuacaoDomainService.SearchBySpecification<PessoaAtuacaoFilterSpecification,
                            PessoaAtuacaoFiltroData,
                            PessoaAtuacaoListaData,
                            PessoaAtuacao>(filtro, IncludesPessoaAtuacao.DadosPessoais | IncludesPessoaAtuacao.Pessoa);
        }

        /// <summary>
        /// Busca os dados de cabeçalho de uma pessoa atuação
        /// </summary>
        /// <param name="seq"></param>
        /// <returns></returns>
        public PessoaAtuacaoCabecalhoData BuscarPessoaAtuacaoCabecalho(long seq)
        {
            return PessoaAtuacaoDomainService.BuscarPessoaAtuacaoCabecalho(seq).Transform<PessoaAtuacaoCabecalhoData>();
        }

        public PessoaAtuacaoVisualizacaoDocumentoData BuscarDocumentosPessoaAtuacao(long seqPessoaAtuacao)
        {
            return this.PessoaAtuacaoDomainService.BuscarDocumentosPessoaAtuacao(seqPessoaAtuacao).Transform<PessoaAtuacaoVisualizacaoDocumentoData>();
        }

        public PessoaAtuacaoRegistroDocumentoData PrepararModeloRegistroDocumento(long seqPessoaAtuacao, long seqTipoDocumento, List<long> seqsSolicitacoesServico)
        {
            return this.PessoaAtuacaoDomainService.PrepararModeloRegistroDocumento(seqPessoaAtuacao, seqTipoDocumento, seqsSolicitacoesServico).Transform<PessoaAtuacaoRegistroDocumentoData>();
        }

        public string BuscarDescricaoTipoDocumento(long seqTipoDocumento)
        {
            var tiposDocumentos = TipoDocumentoService.BuscarTiposDocumentos().ToList();

            return tiposDocumentos.FirstOrDefault(t => t.Seq == seqTipoDocumento).Descricao;
        }

        public void SalvarRegistroDocumento(PessoaAtuacaoRegistroDocumentoData model)
        {
            this.PessoaAtuacaoDomainService.SalvarRegistroDocumento(model.Transform<PessoaAtuacaoRegistroDocumentoVO>());
        }

        /// <summary>
        /// Busca os alunos e colaboradores para emissão da identidade estudantil pelos seqs informados
        /// </summary>
        /// <param name="seqsAlunos">Seqs dos alunos para pesquisa</param>
        /// <param name="seqsColaboradores">Seqs dos colaboradores para pesquisa</param>
        /// <returns>Lista de alunos e colaboradores para emissão da identidade estudantil</returns>
        public List<RelatorioIdentidadeEstudantilData> BuscarPessoaAtuacaoIdentidadeEstudantil(List<long> seqsAlunos, List<long> seqsColaboradores)
        {
            return this.PessoaAtuacaoDomainService.BuscarPessoaAtuacaoIdentidadeEstudantil(seqsAlunos, seqsColaboradores).TransformList<RelatorioIdentidadeEstudantilData>();
        }

        public PessoaAtuacaoDadosOrigemData RecuperaDadosOrigem(long seqAluno, bool desativarFiltroDados = false)
        {
            return PessoaAtuacaoDomainService.RecuperaDadosOrigem(seqAluno, desativarFiltroDados).Transform<PessoaAtuacaoDadosOrigemData>();
        }

        /// <summary>
        /// Busca os dados da pessoa atuação para header da tela de mensagem
        /// </summary>
        /// <param name="seq">Sequencial da pessoa atuação</param>
        /// <returns>Dados da pessoa atuação</returns>
        public PessoaAtuacaoMensagemHeaderData BuscarPessoaAtuacaoHeaderMensagem(long seq)
        {
            return PessoaAtuacaoDomainService.BuscarPessoaAtuacaoHeaderMensagem(seq).Transform<PessoaAtuacaoMensagemHeaderData>();
        }
    }
}