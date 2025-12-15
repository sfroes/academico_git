using SMC.Academico.Domain.Areas.CUR.DomainServices;
using SMC.Academico.Domain.Areas.CUR.ValueObjects;
using SMC.Academico.ServiceContract.Areas.CUR.Data;
using SMC.Academico.ServiceContract.Areas.CUR.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.Service.Areas.CUR.Services
{
    public class GrupoCurricularComponenteService : SMCServiceBase, IGrupoCurricularComponenteService
    {
        #region [ DomainService ]

        private GrupoCurricularComponenteDomainService GrupoCurricularComponenteDomainService => this.Create<GrupoCurricularComponenteDomainService>();

        #endregion [ DomainService ]

        public GrupoCurricularComponenteListaData[] BuscarGruposCurricularesComponentesLookup(GrupoCurricularComponenteFiltroData grupoCurricularComponenteFiltro)
        {
            var gruposCurricularesComponentesVO = GrupoCurricularComponenteDomainService.BuscarGruposCurricularesComponentesLookup(grupoCurricularComponenteFiltro.Transform<GrupoCurricularComponenteFiltroVO>());
            return gruposCurricularesComponentesVO.TransformToArray<GrupoCurricularComponenteListaData>();
        }

        public GrupoCurricularComponenteData[] BuscarGruposCurricularesComponentesLookupSelecionado(long[] seqsGruposCurricularesComponentes)
        {
            var gruposCurricularesComponentesVO = GrupoCurricularComponenteDomainService.BuscarGruposCurricularesComponentesLookupSelecionado(seqsGruposCurricularesComponentes);
            return gruposCurricularesComponentesVO.TransformToArray<GrupoCurricularComponenteData>();
        }

        public List<SMCDatasourceItem> BuscarComponentesCurricularesPadrao(long seqCurriculoCursoOferta)
        {
            return GrupoCurricularComponenteDomainService.BuscarComponentesCurricularesPadrao(seqCurriculoCursoOferta);
        }

        public void Excluir(long seq)
        {
            GrupoCurricularComponenteDomainService.Excluir(seq);
        }
    }
}