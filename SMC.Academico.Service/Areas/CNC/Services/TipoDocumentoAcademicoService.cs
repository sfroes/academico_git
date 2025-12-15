using SMC.Academico.Common.Areas.CNC.Enums;
using SMC.Academico.Common.Areas.SRC.Constants;
using SMC.Academico.Domain.Areas.CNC.DomainServices;
using SMC.Academico.Domain.Areas.CNC.Specifications;
using SMC.Academico.Domain.Areas.CNC.ValueObjects;
using SMC.Academico.ServiceContract.Areas.CNC.Data;
using SMC.Academico.ServiceContract.Areas.CNC.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Security.Util;
using SMC.Framework.Service;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Service.Areas.CNC.Services
{
    public class TipoDocumentoAcademicoService : SMCServiceBase, ITipoDocumentoAcademicoService
    {
        #region [ DomainService ]

        private TipoDocumentoAcademicoDomainService TipoDocumentoAcademicoDomainService => Create<TipoDocumentoAcademicoDomainService>();

        #endregion [ DomainService ]

        /// <summary>
        /// Buscar tipo de documento de conclusão
        /// </summary>
        /// <param name="seq">Sequencial do tipo de documento</param>
        /// <returns>Tipo de documento de conclusão</returns>
        public TipoDocumentoAcademicoData BuscarTipoDocumentoAcademico(long seq)
        {
            var retorno = this.TipoDocumentoAcademicoDomainService.BuscarTipoDocumentoAcademico(seq).Transform<TipoDocumentoAcademicoData>();

            return retorno;
        }

        /// <summary>
        /// Busca a lista de tipos de documentos de conclusão da instituição de ensino logada para popular um Select
        /// </summary>
        /// <returns>Lista de tipos de documentos de conclusão/returns>
        public List<SMCDatasourceItem> BuscarTiposDocumentoAcademicoSelect()
        {
            var lista = TipoDocumentoAcademicoDomainService.SearchProjectionAll(x => new SMCDatasourceItem()
            {
                Seq = x.Seq,
                Descricao = x.Descricao
            },
            i => i.Descricao).ToList();

            return lista;
        }



        /// <summary>
        /// Grava um tipo de documento de conclusão
        /// </summary>
        /// <param name="modelo">Tipo de documento de conclusão a ser gravado</param>
        /// <returns>Sequencia do tipo de documento de conclusao gravado</returns>
        public long SalvarTipoDocumentoAcademico(TipoDocumentoAcademicoData modelo)
        {
            return TipoDocumentoAcademicoDomainService.SalvarTipoDocumentoAcademico(modelo.Transform<TipoDocumentoAcademicoVO>());
        }

        public List<SMCDatasourceItem> BuscarTiposDocumentoConclusaoSelect(List<GrupoDocumentoAcademico> gruposDocumentoAcademico, long? seqInstituicaoEnsino)
        {
            return TipoDocumentoAcademicoDomainService.BuscarTiposDocumentoConclusaoSelect(gruposDocumentoAcademico, seqInstituicaoEnsino);
        }

        /// <summary>
        /// Busca tipos de documento acadêmico por grupo de documento aplicando no retorno da listagem um token de segurança 
        /// </summary>
        /// <param name="grupoDocumentoAcademico"></param>        
        /// <returns>List<SMCDatasourceItem></returns>
        public List<SMCDatasourceItem> BuscarTiposDocumentoAcademicoPorTipoGrupoDocAcadSelect(GrupoDocumentoAcademico grupoDocumentoAcademico) 
        {
            return TipoDocumentoAcademicoDomainService.BuscarTiposDocumentoAcademicoPorTipoGrupoDocAcadSelect(grupoDocumentoAcademico);
        }

    }
}
