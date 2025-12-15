using SMC.Academico.Domain.Areas.FIN.DomainServices;
using SMC.Academico.ServiceContract.Areas.FIN;
using SMC.Academico.ServiceContract.Areas.FIN.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;
using System.Linq;
using SMC.Academico.Domain.Areas.FIN.Models;
using SMC.Academico.ServiceContract.Areas.FIN.Data;
using SMC.Framework.Specification;
using SMC.Academico.Common.Areas.FIN.Includes;
using SMC.Framework.Mapper;
using SMC.Academico.Domain.Areas.CSO.DomainServices;
using SMC.Academico.Common.Areas.CSO.Includes;
using SMC.Academico.Domain.Areas.CSO.Models;
using SMC.Academico.Domain.Areas.FIN.ValueObjects;
using System; 

namespace SMC.Academico.Service.Areas.FIN.Services
{
    public class TermoAdesaoService : SMCServiceBase, ITermoAdesaoService
    {
        private ContratoDomainService ContratoDomainService
        {
            get { return this.Create<ContratoDomainService>(); }
        } 

        private CursoOfertaLocalidadeDomainService CursoOfertaLocalidadeDomainService
        {
            get { return this.Create<CursoOfertaLocalidadeDomainService>(); }
        }

        private TermoAdesaoDomainService TermoAdesaoDomainService
        {
            get { return this.Create<TermoAdesaoDomainService>(); }
        } 

        public TermoAdesaoCabecalhoData BuscarCabecalhoTermoAdesao(long seqContrato)
        {
           var result = this.ContratoDomainService.SearchByKey(new SMCSeqSpecification<Contrato>(seqContrato),
                 IncludesContrato.Cursos
               | IncludesContrato.Cursos_Curso
               | IncludesContrato.NiveisEnsino 
               | IncludesContrato.NiveisEnsino_NivelEnsino);

            var data = SMCMapperHelper.Create<TermoAdesaoCabecalhoData>(result);
            data.SeqContrato = result.Seq;
            data.DataInicioValidade = result.DataInicioValidade.SMCDataAbreviada();
            data.DataFimValidade = (result.DataFimValidade == null || result.DataFimValidade == DateTime.MinValue) ? "-" : result.DataFimValidade.SMCDataAbreviada(); 

            return data;
        }

        public SMCPagerData<TermoAdesaoListarData> ListarTermosAdesao(TermoAdesaoFiltroData filtro)
        {
            var filtroVO = filtro.Transform<TermoAdesaoFiltroVO>();

            var list = TermoAdesaoDomainService.ListarTermosAdesao(filtroVO);

            var result = list.TransformList<TermoAdesaoListarData>();

            return new SMCPagerData<TermoAdesaoListarData>(result, list.Total);
        }

        public TermoAdesaoData BuscarTermoAdesao()
        {
            var dto = new TermoAdesaoData();

            if (!dto.Ativo.HasValue)
                dto.Ativo = true;

            return dto;
        }
 
        public long SalvarTermoAdesao(TermoAdesaoData termo)
        {
            var vo = termo.Transform<TermoAdesaoVO>();

            return this.TermoAdesaoDomainService.SalvarTermoAdesao(vo);
        }

    }
}
