using SMC.Academico.Domain.Areas.SRC.DomainServices;
using SMC.Academico.Domain.Areas.SRC.Specifications;
using SMC.Academico.Domain.Areas.SRC.ValueObjects;
using SMC.Academico.ServiceContract.Areas.SRC.Data;
using SMC.Academico.ServiceContract.Areas.SRC.Interfaces;
using SMC.DadosMestres.ServiceContract.Areas.GED.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Service.Areas.SRC.Services
{
    public class DocumentoRequeridoService : SMCServiceBase, IDocumentoRequeridoService
    {
        #region [ DomainService ]

        private DocumentoRequeridoDomainService DocumentoRequeridoDomainService
        {
            get { return this.Create<DocumentoRequeridoDomainService>(); }
        }

        private ServicoTipoDocumentoDomainService ServicoTipoDocumentoDomainService {
            get { return this.Create<ServicoTipoDocumentoDomainService>(); }
        }

        #endregion [ DomainService ]

        #region Services

        private ITipoDocumentoService TipoDocumentoService
        {
            get { return this.Create<ITipoDocumentoService>(); }
        }

        #endregion

        public List<SMCDatasourceItem> BuscarTiposDocumentoSelect()
        {
            var lista = TipoDocumentoService.BuscarTiposDocumentos();

            List<SMCDatasourceItem> retorno = new List<SMCDatasourceItem>();

            foreach (var item in lista)
                retorno.Add(new SMCDatasourceItem(item.Seq, item.Descricao));

            return retorno;
        }

        public List<SMCDatasourceItem> BuscarTiposDocumentoPorServicoSelect(long seqServico)
        {
            List<SMCDatasourceItem> retorno = new List<SMCDatasourceItem>();
            var lista = TipoDocumentoService.BuscarTiposDocumentos();

            var specTipoDocumentoServico = new ServicoTipoDocumentoFilterSpeficication() { SeqServico = seqServico };
            var tipoDocumentoPorServico = ServicoTipoDocumentoDomainService.SearchBySpecification(specTipoDocumentoServico).ToList();
            
            if(tipoDocumentoPorServico.Count > 0)
            {
                var seqsTiposDocumentosServico = tipoDocumentoPorServico.Select(c => c.SeqTipoDocumento);

                var novaLista = lista.Where(c => seqsTiposDocumentosServico.Contains(c.Seq)).OrderBy(c => c.Descricao);
            
                foreach (var item in novaLista)
                    retorno.Add(new SMCDatasourceItem(item.Seq, item.Descricao));
            }

            return retorno;
        }


        public DocumentoRequeridoData BuscarDocumentoRequerido(long seqDocumentoRequerido)
        {
            return DocumentoRequeridoDomainService.BuscarDocumentoRequerido(seqDocumentoRequerido).Transform<DocumentoRequeridoData>();
        }

        public SMCPagerData<DocumentoRequeridoListarData> BuscarDocumentosRequeridos(DocumentoRequeridoFiltroData filtro)
        {
            return DocumentoRequeridoDomainService.BuscarDocumentosRequeridos(filtro.Transform<DocumentoRequeridoFiltroVO>()).Transform<SMCPagerData<DocumentoRequeridoListarData>>();
        }

        public long Salvar(DocumentoRequeridoData modelo)
        {
            return this.DocumentoRequeridoDomainService.Salvar(modelo.Transform<DocumentoRequeridoVO>());
        }

        public void ValidarModeloSalvar(DocumentoRequeridoData modelo)
        {
            this.DocumentoRequeridoDomainService.ValidarModeloSalvar(modelo.Transform<DocumentoRequeridoVO>());
        }

        public void Excluir(long seq)
        {
            this.DocumentoRequeridoDomainService.Excluir(seq);
        }

        public DocumentoRequeridoData BuscarDescricaoDocumentoRequeridoPermiteVarios(long seqDocumentoRequerido)
        {
            return DocumentoRequeridoDomainService.BuscarDescricaoDocumentoRequeridoPermiteVarios(seqDocumentoRequerido).Transform<DocumentoRequeridoData>();
        }
    }
}
