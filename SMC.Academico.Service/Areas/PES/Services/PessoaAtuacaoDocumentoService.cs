using SMC.Academico.Domain.Areas.PES.DomainServices;
using SMC.Academico.Domain.Areas.PES.ValueObjects;
using SMC.Academico.ServiceContract.Areas.PES.Data;
using SMC.Academico.ServiceContract.Areas.PES.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.Service.Areas.PES.Services
{
    public class PessoaAtuacaoDocumentoService : SMCServiceBase, IPessoaAtuacaoDocumentoService
    {
        #region [Domain Services]

        private PessoaAtuacaoDocumentoDomainService PessoaAtuacaoDocumentoDomainService => Create<PessoaAtuacaoDocumentoDomainService>();

        #endregion [Domain Services]

        //public List<SMCDatasourceItem> BuscarTiposDocumentoSelect(long seqPessoaAtuacao)
        //{
        //    return this.PessoaAtuacaoDocumentoDomainService.BuscarTiposDocumentoSelect(seqPessoaAtuacao);
        //}

        public List<SMCDatasourceItem> BuscarDocumentosSelect(long seqPessoaAtuacao, long? seq)
        {
            return this.PessoaAtuacaoDocumentoDomainService.BuscarDocumentosSelect(seqPessoaAtuacao, seq);
        }

        public SMCPagerData<PessoaAtuacaoDocumentoListarData> BuscarTiposDocumentoLista(PessoaAtuacaoDocumentoFiltroData filtro)
        {

            var lista = PessoaAtuacaoDocumentoDomainService.BuscarTiposDocumentoLista(filtro.Transform<PessoaAtuacaoDocumentoFiltroVO>());
            return new SMCPagerData<PessoaAtuacaoDocumentoListarData>(lista.TransformList<PessoaAtuacaoDocumentoListarData>(), lista.Total);

        }

         public List<string> BuscarItensBloqueio(long seqPessoaAtuacao)
        {
            return PessoaAtuacaoDocumentoDomainService.BuscarItensBloqueio(seqPessoaAtuacao);
        }

        public void ExcluirDocumento(long seqPessoaAtuacaoDocumento)
        {
            PessoaAtuacaoDocumentoDomainService.ExcluirDocumento(seqPessoaAtuacaoDocumento);
        }

        public bool VerificaDocumentoObrigatorio(long seqTipoDocumento, long seqPessoaAtuacao)
        {
            return PessoaAtuacaoDocumentoDomainService.VerificaDocumentoObrigatorio(seqTipoDocumento, seqPessoaAtuacao);
        }

        public void GerarBloqueio(long seqPessoaAtuacao, long seqTipoDocumento)
        {
            PessoaAtuacaoDocumentoDomainService.GerarBloqueio(seqPessoaAtuacao, seqTipoDocumento);           
        }

        public long SalvarDocumento(PessoaAtuacaoDocumentoData pessoaAtuacaoDocumento)
        {
            var PessoaAtuacaoDocumentoListarVO = pessoaAtuacaoDocumento.Transform<PessoaAtuacaoDocumentoListarVO>();
            return PessoaAtuacaoDocumentoDomainService.SalvarDocumento(PessoaAtuacaoDocumentoListarVO);
        }

        public bool VerificarServico(long seqPessoaAtuacao)
        {
            return PessoaAtuacaoDocumentoDomainService.VerificarServico(seqPessoaAtuacao);
        }

        public bool NaoExisteProcesso(long seqPessoaAtuacao)
        {
            return PessoaAtuacaoDocumentoDomainService.NaoExisteProcesso(seqPessoaAtuacao);
        }  
    }
}
