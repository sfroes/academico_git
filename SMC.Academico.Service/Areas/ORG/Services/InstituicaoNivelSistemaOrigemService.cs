using SMC.Academico.Common.Areas.ORG.Enums;
using SMC.Academico.Domain.Areas.ORG.DomainServices;
using SMC.Academico.Domain.Areas.ORG.Specifications;
using SMC.Academico.Domain.Areas.ORG.ValueObjects;
using SMC.Academico.ServiceContract.Areas.ORG.Data;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Framework;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.Service.Areas.ORG.Services
{
    public class InstituicaoNivelSistemaOrigemService : SMCServiceBase, IInstituicaoNivelSistemaOrigemService
    {
        #region [ Dominio ]

        private InstituicaoNivelSistemaOrigemDomainService InstituicaoNivelSistemaOrigemDomainService => this.Create<InstituicaoNivelSistemaOrigemDomainService>();

        #endregion

        public List<SMCDatasourceItem> BuscarConfiguracaoDiplomaGADSelect()
        {
            return InstituicaoNivelSistemaOrigemDomainService.BuscarConfiguracaoDiplomaGADSelect();
        }

        public List<SMCDatasourceItem> BuscarConfiguracaoDiplomaPorNivelEnsinoGADSelect(long seqInstituicaoNivel, UsoSistemaOrigem? usoSistemaOrigem = null)
        {
            return InstituicaoNivelSistemaOrigemDomainService.BuscarConfiguracaoDiplomaPorNivelEnsinoGADSelect(seqInstituicaoNivel, usoSistemaOrigem);
        }

        public List<SMCDatasourceItem<string>> BuscarSistemaOrigemGADSelect()
        {
            return InstituicaoNivelSistemaOrigemDomainService.BuscarSistemaOrigemGADSelect();
        }

        public SMCPagerData<InstituicaoNivelSistemaOrigemData> BuscarInstituicaoNivelSistemaOrigemGAD(InstituicaoNivelSistemaOrigemFiltroData filtros)
        {
            return InstituicaoNivelSistemaOrigemDomainService.BuscarInstituicaoNivelSistemaOrigemGAD(filtros.Transform<InstituicaoNivelSistemaOrigemFilterSpecification>())
                                                             .Transform<SMCPagerData<InstituicaoNivelSistemaOrigemData>>();
        }

        public long SalvarInstituicaoNivelSIstemaOrigemGAD(InstituicaoNivelSistemaOrigemData instituicaoNivelSistemaOrigemData)
        {
            return InstituicaoNivelSistemaOrigemDomainService.SalvarInstituicaoNivelSIstemaOrigemGAD(instituicaoNivelSistemaOrigemData
                                                             .Transform<InstituicaoNivelSistemaOrigemVO>());
        }

        public List<SMCDatasourceItem> BuscarTipoUsoNivelEnsino(long seqNivelEnsino)
        {
            return InstituicaoNivelSistemaOrigemDomainService.BuscarTipoUsoNivelEnsino(seqNivelEnsino);
        }

        public List<SMCDatasourceItem> BuscarTiposUsoNiveisEnsino()
        {
            var dataSrc = new List<SMCDatasourceItem>();

            dataSrc.Add(UsoSistemaOrigem.ArquivoPDF.SMCToSelectItem());
            dataSrc.Add(UsoSistemaOrigem.ArquivoXML.SMCToSelectItem());
            
            return dataSrc;
        }
    }
}
