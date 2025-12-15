using SMC.Academico.Domain.Areas.DCT.DomainServices;
using SMC.Academico.Domain.Areas.DCT.Models;
using SMC.Academico.Domain.Areas.DCT.Specifications;
using SMC.Academico.Domain.Areas.DCT.ValueObjects;
using SMC.Academico.ServiceContract.Areas.DCT.Data;
using SMC.Academico.ServiceContract.Areas.DCT.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.Service.Areas.DCT.Services
{
    public class ColaboradorAptoComponenteService : SMCServiceBase, IColaboradorAptoComponenteService
    {
        #region [ Services ]

        private ColaboradorAptoComponenteDomainService ColaboradorAptoComponenteDomainService
        {
            get { return this.Create<ColaboradorAptoComponenteDomainService>(); }
        }

        #endregion [ Services ] 

        public SMCPagerData<ColaboradorAptoComponenteData> BuscarColadoradorAptoComponentes(ColaboradorAptoComponenteFiltroData filtros)
        {
            var result = ColaboradorAptoComponenteDomainService.BuscarColaboradorAptoComponentes(filtros.Transform<ColaboradorAptoComponenteFiltroVO>()).TransformList<ColaboradorAptoComponenteData>();

            return new SMCPagerData<ColaboradorAptoComponenteData>(result, result.Count);
        }

        public long SalvarColadoradorAptoComponente(ColaboradorAptoComponenteData modelo)
        {
            return ColaboradorAptoComponenteDomainService.SalvarColaboradorAptoComponente(modelo.Transform<ColaboradorAptoComponenteVO>());
        }

        public bool ValidarFormacaoAcademica(long seqAtuacaoColaborador)
        {
            return ColaboradorAptoComponenteDomainService.ValidarFormacaoAcademica(seqAtuacaoColaborador);
        }
    }
}