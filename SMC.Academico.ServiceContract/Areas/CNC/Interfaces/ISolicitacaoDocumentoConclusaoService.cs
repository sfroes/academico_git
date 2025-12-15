using SMC.Academico.Common.Areas.CNC.Enums;
using SMC.Academico.ServiceContract.Areas.CNC.Data;
using SMC.Academico.ServiceContract.Areas.CNC.Data.SolicitacaoDocumentoConclusao;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.CNC.Interfaces
{
    public interface ISolicitacaoDocumentoConclusaoService : ISMCService
    {
        SolicitacaoAnaliseEmissaoDocumentoConclusaoData BuscarDadosSolicitacaoDocumentoConclusao(long seqSolicitacaoServico);

        List<SMCDatasourceItem> BuscarTiposHistoricoEscolarSelect();

        string ConsultarInformacoesRVDD(long seqSolicitacaoServico, TipoIdentidade? tipoIdentidade);

        SolicitacaoAnaliseEmissaoDocumentoConclusaoAtosCursoData BuscarAtosNormativosCurso(long seqCursoOfertaLocalidade, long seqGrauAcademico, DateTime? dataConclusao, bool exibirErro = false);

        List<SMCDatasourceItem> BuscarTiposDocumentoDocumentacaoComprobatoriaSelect();

        List<SMCDatasourceItem> BuscarTiposDocumentoRequeridoPorEtapaSelect(long seqSolicitacaoServico);

        List<SolicitacaoAnaliseEmissaoDocumentoConclusaoMensagemListarData> BuscarMensagens(SolicitacaoAnaliseEmissaoDocumentoConclusaoMensagemFiltroData filtro);

        SolicitacaoAnaliseEmissaoDocumentoConclusaoMensagemData BuscarMensagem(long seq, long seqPessoaAtuacao, long seqSolicitacaoServico);

        List<SMCDatasourceItem> BuscarTiposMensagemPorInstituicaoNivel(string tokenTipoDocumentoAcademico, long? seqTipoDocumentoSolicitado, long seqInstituicaoEnsino, long seqInstituicaoNivel, bool primeiraVia, string documentoAcademico);

        void SalvarMensagem(SolicitacaoAnaliseEmissaoDocumentoConclusaoMensagemData model);

        void ExcluirMensagem(long seq, long seqPessoaAtuacao, long seqSolicitacaoServico);

        SolicitacaoAnaliseEmissaoDocumentoConclusaoConsultarHistoricoData BuscarDadosConsultaHistorico(long seqSolicitacaoServico);

        SMCPagerData<SolicitacaoAnaliseEmissaoDocumentoConclusaoConsultarHistoricoListarData> BuscarHistoricoEscolar(SolicitacaoAnaliseEmissaoDocumentoConclusaoConsultarHistoricoData filtro);

        SMCPagerData<SolicitacaoAnaliseEmissaoDocumentoConclusaoConsultarAtividadeComplementarListarData> BuscarHistoricoEscolarAtividadeComplementar(SolicitacaoAnaliseEmissaoDocumentoConclusaoConsultarHistoricoData filtro);

        void SalvarAtendimentoEmissaoDocumentoConclusao(SolicitacaoAnaliseEmissaoDocumentoConclusaoData model);

        DadosCargaHorariaHistoricoData RetornarDadosCargaHorariaHistorico(long codigoAlunoMigracao, long? numeroVia);
    }
}