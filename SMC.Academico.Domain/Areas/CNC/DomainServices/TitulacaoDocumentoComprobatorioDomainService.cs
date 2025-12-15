using SMC.Academico.Domain.Areas.CNC.Models;
using SMC.Academico.Domain.Areas.CNC.Specifications;
using SMC.DadosMestres.ServiceContract.Areas.GED.Interfaces;
using SMC.Framework.Model;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Areas.CNC.DomainServices
{
    public class TitulacaoDocumentoComprobatorioDomainService : AcademicoContextDomain<TitulacaoDocumentoComprobatorio>
    {
        #region [ Services ]

        private ITipoDocumentoService TipoDocumentoService => Create<ITipoDocumentoService>();

        #endregion [ Services ]

        /// <summary>
        /// Busca os tipos de documentos associados à titulação informada
        /// </summary>
        /// <param name="seqTitulacao">Sequencial da titulação</param>
        /// <returns>Dados dos tipos de documentos associadoas à titulação informada</returns>
        public List<SMCDatasourceItem> BuscarTitulacaoDocumentosComprobatorios(long seqTitulacao)
        {
            var spec = new TitulacaoDocumentoComprobatorioFilterSpecification()
            {
                SeqTitulacao = seqTitulacao
            };
            var tiposDocumentoTitulacao = SearchProjectionBySpecification(spec, p => new
            {
                p.Seq,
                p.SeqTipoDocumento
            }).ToArray();

            var tiposDocumentos = TipoDocumentoService.BuscarTiposDocumentos();

            return tiposDocumentoTitulacao
                .Select(s => new SMCDatasourceItem(s.Seq, tiposDocumentos.FirstOrDefault(f => f.Seq == s.SeqTipoDocumento)?.Descricao))
                .OrderBy(o => o.Descricao)
                .ToList();
        }
    }
}