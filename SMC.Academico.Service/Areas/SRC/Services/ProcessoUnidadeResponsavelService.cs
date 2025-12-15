using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.Academico.Domain.Areas.SRC.DomainServices;
using SMC.Academico.ServiceContract.Areas.SRC.Interfaces;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.Service.Areas.SRC.Services
{
    public class ProcessoUnidadeResponsavelService : SMCServiceBase, IProcessoUnidadeResponsavelService
    {
        #region [ DomainService ]

        private ProcessoUnidadeResponsavelDomainService ProcessoUnidadeResponsavelDomainService
        {
            get { return this.Create<ProcessoUnidadeResponsavelDomainService>(); }
        }

        #endregion [ DomainService ]

        public List<SMCDatasourceItem> BuscarUnidadesResponsaveisVinculadasProcessoSelect(TipoUnidadeResponsavel tipoUnidadeResponsavel = TipoUnidadeResponsavel.Nenhum, bool usarNomeReduzido = false)
        {
            return this.ProcessoUnidadeResponsavelDomainService.BuscarUnidadesResponsaveisVinculadasProcessoSelect(tipoUnidadeResponsavel, usarNomeReduzido);
        }

        /// <summary>
        /// Buscar unidades responsaveis do processo
        /// </summary>
        /// <param name="seqProcesso">Sequencial do Processo</param>
        /// <returns>Lista das unidades responsaveis de um processo</returns>
        public List<SMCDatasourceItem> BuscarUnidadesResponsaveisPorProcessoSelect(long seqProcesso)
        {
            return this.ProcessoUnidadeResponsavelDomainService.BuscarUnidadesResponsaveisPorProcessoSelect(seqProcesso);
        }
    }
}