using SMC.Academico.Domain.DomainServices;
using SMC.Academico.Domain.Models;
using SMC.Academico.ServiceContract.Data;
using SMC.Academico.ServiceContract.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Service;
using SMC.Framework.Specification;
using System;
using System.Collections.Generic;
using System.IO;

namespace SMC.Academico.Service.Services
{
    public class ArquivoAnexadoService : SMCServiceBase, IArquivoAnexadoService
    {
        #region DomainService

        private ArquivoAnexadoDomainService ArquivoAnexadoDomainService
        {
            get { return Create<ArquivoAnexadoDomainService>(); }
        }

        #endregion DomainService

        public SMCUploadFile BuscarArquivoAnexado(long seq)
        {
            return ArquivoAnexadoDomainService.SearchByKey<ArquivoAnexado, SMCUploadFile>(seq);
        }

        public ArquivoAnexadoData BuscarArquivoAnexadoData(long seq)
        {
            return ArquivoAnexadoDomainService.SearchByKey<ArquivoAnexado, ArquivoAnexadoData>(seq);
        }

        public SMCUploadFile BuscarArquivoAnexadoPorGuid(Guid uidArquivo)
        {
            return ArquivoAnexadoDomainService.BuscarArquivoAnexadoPorGuid(uidArquivo);
        }

        public FileInfo BuscarECompactarArquivos(IEnumerable<long> seqs)
        {
            return ArquivoAnexadoDomainService.BuscarECompactarArquivos(seqs);
        }

        public long SalvarArquivo(SMCUploadFile arquivo)
        {
            var arquivoAnexado = arquivo.Transform<ArquivoAnexado>();
            ArquivoAnexadoDomainService.SaveEntity(arquivoAnexado);
            return arquivoAnexado.Seq;
        }
    }
}