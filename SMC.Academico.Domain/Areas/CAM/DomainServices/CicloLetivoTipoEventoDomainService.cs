using SMC.Academico.Common.Areas.CAM.Includes;
using SMC.Academico.Domain.Areas.CAM.Models;
using SMC.Academico.Domain.Areas.CAM.Specifications;
using SMC.Academico.Domain.Areas.CAM.ValueObjects;
using SMC.Academico.Domain.Areas.ORG.DomainServices;
using SMC.Academico.Domain.Areas.ORG.Models;
using SMC.Calendarios.ServiceContract.Areas.CLD.Interfaces;
using SMC.Framework.Domain;
using SMC.Framework.Extensions;
using SMC.Framework.Specification;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Areas.CAM.DomainServices
{
    public class CicloLetivoTipoEventoDomainService : AcademicoContextDomain<CicloLetivoTipoEvento>
    {
        #region DomainServices

        private InstituicaoTipoEventoDomainService InstituicaoTipoEventoDomainService
        {
            get { return this.Create<InstituicaoTipoEventoDomainService>(); }
        }

        private NivelEnsinoDomainService NivelEnsinoDomainService
        {
            get { return this.Create<NivelEnsinoDomainService>(); }
        }

        #endregion DomainServices

        #region DomainServices

        private ITipoEventoService TipoEventoService
        {
            get { return this.Create<ITipoEventoService>(); }
        }

        #endregion DomainServices

        public long SalvarCicloLetivoTipoEvento(CicloLetivoTipoEventoVO modelo)
        {
            var cicloLetivoTipoEvento = modelo.Transform<CicloLetivoTipoEvento>();

            //Necessário instancias novas listas para que os registros anteriores sejam apagados do banco de dados
            if (cicloLetivoTipoEvento.NiveisEnsino == null)
                cicloLetivoTipoEvento.NiveisEnsino = new List<NivelEnsino>();

            if (cicloLetivoTipoEvento.Parametros == null)
                cicloLetivoTipoEvento.Parametros = new List<CicloLetivoTipoEventoParametro>();

            //Carrega o seq da instituicao tipo evento e seta
            var specInstituicaoTipoEvento = modelo.Transform<InstituicaoTipoEventoFilterSpecification>();
            var instituicaoTipoEvento = this.InstituicaoTipoEventoDomainService.SearchBySpecification(specInstituicaoTipoEvento);
            cicloLetivoTipoEvento.SeqIntituicaoTipoEvento = instituicaoTipoEvento.FirstOrDefault().Seq;

            this.SaveEntity(cicloLetivoTipoEvento);

            return cicloLetivoTipoEvento.Seq;
        }

        public List<CicloLetivoTipoEventoListaVO> BuscarClclosLetivosTiposEventos(CicloLetivoTipoEventoFiltroVO filtros)
        {
            var spec = filtros.Transform<CicloLetivoTipoEventoFilterSpecification>();

            var result = this.SearchBySpecification(spec, IncludesCicloLetivoTipoEvento.InstituicaoTipoEvento | IncludesCicloLetivoTipoEvento.NiveisEnsino | IncludesCicloLetivoTipoEvento.Parametros_InstituicaoTipoEventoParametro).TransformList<CicloLetivoTipoEventoListaVO>();

            foreach (var item in result)
            {
                item.DescricaoTipoEvento = this.TipoEventoService.BuscarTipoEvento(item.SeqTipoEventoAgd).Descricao;
            }

            return result;
        }

        public CicloLetivoTipoEventoVO BuscarClcloLetivoTipoEvento(long seq)
        {
            var result = this.SearchByKey(new SMCSeqSpecification<CicloLetivoTipoEvento>(seq), IncludesCicloLetivoTipoEvento.InstituicaoTipoEvento | IncludesCicloLetivoTipoEvento.NiveisEnsino | IncludesCicloLetivoTipoEvento.Parametros_InstituicaoTipoEventoParametro).Transform<CicloLetivoTipoEventoVO>();

            return result;
        }
    }
}