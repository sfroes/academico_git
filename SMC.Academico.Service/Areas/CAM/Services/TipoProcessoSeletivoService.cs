using SMC.Academico.Domain.Areas.CAM.DomainServices;
using SMC.Academico.Domain.Areas.CAM.Specifications;
using SMC.Academico.ServiceContract.Areas.CAM.Data;
using SMC.Academico.ServiceContract.Areas.CAM.Interfaces;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Service.Areas.CAM.Services
{
    public class TipoProcessoSeletivoService : SMCServiceBase, ITipoProcessoSeletivoService
    {
        #region DomainService

        private ProcessoSeletivoDomainService ProcessoSeletivoDomainService => Create<ProcessoSeletivoDomainService>();

        private TipoProcessoSeletivoDomainService TipoProcessoSeletivoDomainService => Create<TipoProcessoSeletivoDomainService>();

        private InstituicaoNivelTipoProcessoSeletivoDomainService InstituicaoNivelTipoProcessoSeletivoDomainService => Create<InstituicaoNivelTipoProcessoSeletivoDomainService>();

        #endregion DomainService

        public List<SMCDatasourceItem> BuscarTiposProcessoSeletivoSelect()
        {
            var lista = TipoProcessoSeletivoDomainService.SearchAll().OrderBy(w => w.Descricao);
            List<SMCDatasourceItem> retorno = new List<SMCDatasourceItem>();
            foreach (var item in lista)
                retorno.Add(new SMCDatasourceItem(item.Seq, item.Descricao));
            return retorno;
        }

        public List<SMCDatasourceItem> BuscarTiposProcessoSeletivoPorCampanhaSelect(long SeqCampanha)
        {
            var spec = new ProcessoSeletivoFilterSpecification() { SeqCampanha = SeqCampanha };

            var result = this.ProcessoSeletivoDomainService.SearchProjectionBySpecification(spec, p => new SMCDatasourceItem()
            {
                Seq = p.TipoProcessoSeletivo.Seq,
                Descricao = p.TipoProcessoSeletivo.Descricao
            }, isDistinct: true);

            return result.OrderBy(o => o.Descricao).ToList();
        }

        public List<SMCDatasourceItem> BuscarTiposProcessoSeletivoPorNivelEnsinoSelect(TipoProcessoSeletivoSelectFiltroData filtro)
        {
            return InstituicaoNivelTipoProcessoSeletivoDomainService.BuscarTiposProcessoSeletivoPorNivelEnsino(filtro.SeqsNivelEnsino);            
        }
    }
}