using SMC.Academico.Domain.Areas.ORT.DomainServices;
using SMC.Academico.ServiceContract.Areas.ORT.Data;
using SMC.Academico.ServiceContract.Areas.ORT.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Service.Areas.ORT.Services
{
    public class TipoOrientacaoService : SMCServiceBase, ITipoOrientacaoService
    {
        #region DomainService

        private TipoOrientacaoDomainService TipoOrientacaoDomainService
        {
            get { return this.Create<TipoOrientacaoDomainService>(); }
        }

        #endregion DomainService

        public List<SMCDatasourceItem> BuscarTipoOrientacaoSelect()
        {
            return TipoOrientacaoDomainService.SearchAll(i => i.Descricao).OrderBy(w => w.Descricao).TransformList<SMCDatasourceItem>();
        }

        /// <summary>
        /// Listar os tipos de orientação, que estejam configurados para permitir manutenção 
        /// manual da orientação e que estejam parametrizados por Instituição Nível - Vínculo para 
        /// a instituição logada e de acordo com o filtro de dados descrito pela regra 
        /// RN_USG_004 - Filtro por Nível de Ensino.
        /// </summary>
        /// <returns></returns>
        public List<SMCDatasourceItem> BuscarTipoOrientacaoManutencaoManualSelect()
        {
            return TipoOrientacaoDomainService.SearchAll(i => i.Descricao).Where(w => w.PermiteManutencaoManual).OrderBy(o => o.Descricao).TransformList<SMCDatasourceItem>();
        }

        public List<SMCDatasourceItem> BuscarTipoOrientacaoNaoOrientacaoTurmaSelect()
        {
            return TipoOrientacaoDomainService.SearchAll(i => i.Descricao).Where(f => !f.OrientacaoTurma).TransformList<SMCDatasourceItem>();
        }

        /// <summary>
        /// Buscar tipo de orientação
        /// </summary>
        /// <param name="seq">Sequencial tipo de orientação</param>
        /// <returns>Tipo de orientação</returns>
        public TipoOrientacaoData BuscarTipoOrientacao(long seq)
        {
            return this.TipoOrientacaoDomainService.BuscarTipoOrientacao(seq).Transform<TipoOrientacaoData>();
        }

        /// <summary>
        /// Valida se o tipo de orientação e de conclusão de curso - TCC
        /// </summary>
        /// <param name="seq">Sequencial do tipo de orientação</param>
        /// <returns>Verdaeiro caso seja TCC</returns>
        public bool ValidarTipoOrientacaoConclucaoCurso(long seq)
        {
            return this.TipoOrientacaoDomainService.ValidarTipoOrientacaoConclucaoCurso(seq);
        }
    }
}