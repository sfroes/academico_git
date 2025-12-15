using SMC.Academico.Common.Areas.CNC.Enums;
using SMC.Academico.Domain.Areas.CNC.DomainServices;
using SMC.Academico.Domain.Areas.CNC.Models;
using SMC.Academico.Domain.Areas.CNC.Specifications;
using SMC.Academico.Domain.Areas.CNC.ValueObjects;
using SMC.Academico.ServiceContract.Areas.CNC.Data;
using SMC.Academico.ServiceContract.Areas.CNC.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Service;
using SMC.Framework.Specification;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Service.Areas.CNC.Services
{
    public class SituacaoDocumentoAcademicoService : SMCServiceBase, ISituacaoDocumentoAcademicoService
    {
        #region [ DomainService ]

        private SituacaoDocumentoAcademicoDomainService SituacaoDocumentoAcademicoDomainService => Create<SituacaoDocumentoAcademicoDomainService>();

        #endregion [ DomainService ]

        public SituacaoDocumentoAcademicoData BuscarSituacaoDocumentoAcademicoPorSituacaoAtual(long seqDocumentoAcademicoHistoricoSituacaoAtual)
        {
            return SituacaoDocumentoAcademicoDomainService.BuscarSituacaoDocumentoAcademicoPorSituacaoAtual(seqDocumentoAcademicoHistoricoSituacaoAtual).Transform<SituacaoDocumentoAcademicoData>();
        }

        public List<SMCDatasourceItem> BuscarSituacoesDocumentoAcademicoSelect()
        {
            var lista = SituacaoDocumentoAcademicoDomainService.SearchProjectionAll(x => new SMCDatasourceItem()
            {
                Seq = x.Seq,
                Descricao = x.Descricao
            },
            i => i.Ordem).ToList();

            return lista;
        }

        public SituacaoDocumentoAcademicoData BuscarSituacaoDocumentoAcademico(long seq)
        {
            return this.SituacaoDocumentoAcademicoDomainService.SearchByKey(new SMCSeqSpecification<SituacaoDocumentoAcademico>(seq)).Transform<SituacaoDocumentoAcademicoData>();
        }

        public long Salvar(SituacaoDocumentoAcademicoData modelo)
        {
            return SituacaoDocumentoAcademicoDomainService.Salvar(modelo.Transform<SituacaoDocumentoAcademicoVO>());
        }

        public List<SMCDatasourceItem> BuscarSituacoesDocumentoAcademicoPorGrupoSelect(List<GrupoDocumentoAcademico> listaGrupoDocumentoAcademico)
        {
            return SituacaoDocumentoAcademicoDomainService.BuscarSituacoesDocumentoConclusaoPorGrupoSelect(listaGrupoDocumentoAcademico);
        }

        public SMCPagerData<SituacaoDocumentoAcademicoListarData> BuscarSituacaoDocumentoAcademicoFiltro(SituacaoDocumentoAcademicoFiltroData filtro)
        {
            var spec = filtro.Transform<SituacaoDocumentoAcademicoFilterSpecification>();

            var lista = SituacaoDocumentoAcademicoDomainService.BuscarSituacaoDocumentoAcademicoFiltro(spec);

            var result = lista.Transform<SMCPagerData<SituacaoDocumentoAcademicoListarData>>();

            return new SMCPagerData<SituacaoDocumentoAcademicoListarData>(result, lista.Total);
        }
    }
}
