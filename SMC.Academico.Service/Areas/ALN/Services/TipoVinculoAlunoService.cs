using SMC.Academico.Domain.Areas.ALN.DomainServices;
using SMC.Academico.ServiceContract.Areas.ALN.Interfaces;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.Service.Areas.ALN.Services
{
    public class TipoVinculoAlunoService : SMCServiceBase, ITipoVinculoAlunoService
    {
        #region [ DomainService ]

        private TipoVinculoAlunoDomainService TipoVinculoAlunoDomainService
        {
            get { return this.Create<TipoVinculoAlunoDomainService>(); }
        }

        #endregion [ DomainService ]

        public List<SMCDatasourceItem> BuscarTiposVinculoAlunoSelect()
        {
            return TipoVinculoAlunoDomainService.BuscarTiposVinculoAlunoSelect();
        }

        public List<SMCDatasourceItem> BuscarTiposVinculoAlunoPorServicoSelect(long seqServico)
        {
            return TipoVinculoAlunoDomainService.BuscarTiposVinculoAlunoPorServicoSelect(seqServico);
        }

        /// <summary>
        /// Busca o tipo de processo vinculado ao processo
        /// </summary>
        /// <param name="seqProcessoSeletivo"></param>
        /// <returns></returns>
        public List<SMCDatasourceItem> BuscarTiposVinculoAlunoPorProcessoSeletivo(long seqProcessoSeletivo)
        {
            return TipoVinculoAlunoDomainService.BuscarTiposVinculoAlunoPorProcessoSeletivo(seqProcessoSeletivo);
        }

        /// <summary>
        /// Busca todos tipos de vínculos de aluno configurados na instituição
        /// </summary>
        /// <param name="seqNivelEnsino">Sequencial do nível de ensino</param>
        /// <returns>Dados dos tipos de vínculo de aluno</returns>
        public List<SMCDatasourceItem> BuscarTipoVinculoAlunoNaInstituicaoSelect(long? seqNivelEnsino)
        {
            return TipoVinculoAlunoDomainService.BuscarTipoVinculoAlunoNaInstituicaoSelect(seqNivelEnsino);
        }

        public List<SMCDatasourceItem> BuscarTipoVinculoAlunoPorTipoProcessoSeletivo(long seqTipoProcessoSeletivo)
        {
            return TipoVinculoAlunoDomainService.BuscarTipoVinculoAlunoPorTipoProcessoSeletivo(seqTipoProcessoSeletivo);
        }
    }
}