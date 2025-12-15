using SMC.Academico.Common.Areas.CNC.Enums;
using SMC.Academico.Domain.Areas.CNC.DomainServices;
using SMC.Academico.Domain.Areas.CNC.ValueObjects;
using SMC.Academico.ServiceContract.Areas.CNC.Data;
using SMC.Academico.ServiceContract.Areas.CNC.Data.SolicitacaoDocumentoConclusao;
using SMC.Academico.ServiceContract.Areas.CNC.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System;
using System.Collections.Generic;

namespace SMC.Academico.Service.Areas.CNC.Services
{
    public class SolicitacaoDocumentoConclusaoService : SMCServiceBase, ISolicitacaoDocumentoConclusaoService
    {
        #region DomainServices

        private SolicitacaoDocumentoConclusaoDomainService SolicitacaoDocumentoConclusaoDomainService => Create<SolicitacaoDocumentoConclusaoDomainService>();

        #endregion DomainServices

        public SolicitacaoAnaliseEmissaoDocumentoConclusaoData BuscarDadosSolicitacaoDocumentoConclusao(long seqSolicitacaoServico)
        {
            return SolicitacaoDocumentoConclusaoDomainService.BuscarDadosSolicitacaoDocumentoConclusao(seqSolicitacaoServico).Transform<SolicitacaoAnaliseEmissaoDocumentoConclusaoData>();
        }

        public List<SMCDatasourceItem> BuscarTiposHistoricoEscolarSelect()
        {
            return SolicitacaoDocumentoConclusaoDomainService.BuscarTiposHistoricoEscolarSelect();
        }

        public string ConsultarInformacoesRVDD(long seqSolicitacaoServico, TipoIdentidade? tipoIdentidade)
        {
            return SolicitacaoDocumentoConclusaoDomainService.ConsultarInformacoesRVDD(seqSolicitacaoServico, tipoIdentidade);
        }

        public SolicitacaoAnaliseEmissaoDocumentoConclusaoAtosCursoData BuscarAtosNormativosCurso(long seqCursoOfertaLocalidade, long seqGrauAcademico, DateTime? dataConclusao, bool exibirErro = false)
        {
            return SolicitacaoDocumentoConclusaoDomainService.BuscarAtosNormativosCurso(seqCursoOfertaLocalidade, seqGrauAcademico, dataConclusao, exibirErro).Transform<SolicitacaoAnaliseEmissaoDocumentoConclusaoAtosCursoData>();
        }

        public List<SMCDatasourceItem> BuscarTiposDocumentoDocumentacaoComprobatoriaSelect()
        {
            return SolicitacaoDocumentoConclusaoDomainService.BuscarTiposDocumentoDocumentacaoComprobatoriaSelect();
        }

        public List<SMCDatasourceItem> BuscarTiposDocumentoRequeridoPorEtapaSelect(long seqSolicitacaoServico)
        {
            return SolicitacaoDocumentoConclusaoDomainService.BuscarTiposDocumentoRequeridoPorEtapaSelect(seqSolicitacaoServico);
        }

        public List<SolicitacaoAnaliseEmissaoDocumentoConclusaoMensagemListarData> BuscarMensagens(SolicitacaoAnaliseEmissaoDocumentoConclusaoMensagemFiltroData filtro)
        {
            return SolicitacaoDocumentoConclusaoDomainService.BuscarMensagens(filtro.Transform<SolicitacaoAnaliseEmissaoDocumentoConclusaoMensagemFiltroVO>()).TransformList<SolicitacaoAnaliseEmissaoDocumentoConclusaoMensagemListarData>();
        }

        public SolicitacaoAnaliseEmissaoDocumentoConclusaoMensagemData BuscarMensagem(long seq, long seqPessoaAtuacao, long seqSolicitacaoServico)
        {
            return SolicitacaoDocumentoConclusaoDomainService.BuscarMensagem(seq, seqPessoaAtuacao, seqSolicitacaoServico).Transform<SolicitacaoAnaliseEmissaoDocumentoConclusaoMensagemData>();
        }

        public List<SMCDatasourceItem> BuscarTiposMensagemPorInstituicaoNivel(string tokenTipoDocumentoAcademico, long? seqTipoDocumentoSolicitado, long seqInstituicaoEnsino, long seqInstituicaoNivel, bool primeiraVia, string documentoAcademico)
        {
            return SolicitacaoDocumentoConclusaoDomainService.BuscarTiposMensagemPorInstituicaoNivel(tokenTipoDocumentoAcademico, seqTipoDocumentoSolicitado, seqInstituicaoEnsino, seqInstituicaoNivel, primeiraVia, documentoAcademico);
        }

        public void SalvarMensagem(SolicitacaoAnaliseEmissaoDocumentoConclusaoMensagemData model)
        {
            SolicitacaoDocumentoConclusaoDomainService.SalvarMensagem(model.Transform<SolicitacaoAnaliseEmissaoDocumentoConclusaoMensagemVO>());
        }

        public void ExcluirMensagem(long seq, long seqPessoaAtuacao, long seqSolicitacaoServico)
        {
            SolicitacaoDocumentoConclusaoDomainService.ExcluirMensagem(seq, seqPessoaAtuacao, seqSolicitacaoServico);
        }

        public SolicitacaoAnaliseEmissaoDocumentoConclusaoConsultarHistoricoData BuscarDadosConsultaHistorico(long seqSolicitacaoServico)
        {
            return SolicitacaoDocumentoConclusaoDomainService.BuscarDadosConsultaHistorico(seqSolicitacaoServico).Transform<SolicitacaoAnaliseEmissaoDocumentoConclusaoConsultarHistoricoData>();
        }

        public SMCPagerData<SolicitacaoAnaliseEmissaoDocumentoConclusaoConsultarHistoricoListarData> BuscarHistoricoEscolar(SolicitacaoAnaliseEmissaoDocumentoConclusaoConsultarHistoricoData filtro)
        {
            return SolicitacaoDocumentoConclusaoDomainService.BuscarHistoricoEscolar(filtro.Transform<SolicitacaoAnaliseEmissaoDocumentoConclusaoConsultarHistoricoVO>()).Transform<SMCPagerData<SolicitacaoAnaliseEmissaoDocumentoConclusaoConsultarHistoricoListarData>>();
        }

        public SMCPagerData<SolicitacaoAnaliseEmissaoDocumentoConclusaoConsultarAtividadeComplementarListarData> BuscarHistoricoEscolarAtividadeComplementar(SolicitacaoAnaliseEmissaoDocumentoConclusaoConsultarHistoricoData filtro)
        {
            return SolicitacaoDocumentoConclusaoDomainService.BuscarHistoricoEscolarAtividadeComplementar(filtro.Transform<SolicitacaoAnaliseEmissaoDocumentoConclusaoConsultarHistoricoVO>()).Transform<SMCPagerData<SolicitacaoAnaliseEmissaoDocumentoConclusaoConsultarAtividadeComplementarListarData>>();
        }

        public void SalvarAtendimentoEmissaoDocumentoConclusao(SolicitacaoAnaliseEmissaoDocumentoConclusaoData model)
        {
            SolicitacaoDocumentoConclusaoDomainService.SalvarAtendimentoEmissaoDocumentoConclusao(model.Transform<SolicitacaoAnaliseEmissaoDocumentoConclusaoVO>());
        }

        public DadosCargaHorariaHistoricoData RetornarDadosCargaHorariaHistorico(long codigoAlunoMigracao, long? numeroVia)
        {
            // Chama a procedure que retorna os dados do histórico
            var dados = SolicitacaoDocumentoConclusaoDomainService.RetornarDadosElementoHistorico(codigoAlunoMigracao, numeroVia, false);
            return dados.Transform<DadosCargaHorariaHistoricoData>();
        }
    }
}