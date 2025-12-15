using SMC.Academico.Domain.Areas.SRC.DomainServices;
using SMC.Academico.Domain.Areas.SRC.ValueObjects;
using SMC.Academico.ServiceContract.Areas.SRC.Data;
using SMC.Academico.ServiceContract.Areas.SRC.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.Service.Areas.SRC.Services
{
    public class GrupoDocumentoRequeridoService : SMCServiceBase, IGrupoDocumentoRequeridoService
    {
        #region [ DomainService ]

        private GrupoDocumentoRequeridoDomainService GrupoDocumentoRequeridoDomainService
        {
            get { return this.Create<GrupoDocumentoRequeridoDomainService>(); }
        }

        #endregion [ DomainService ]

        public GrupoDocumentoRequeridoData BuscarGrupoDocumentoRequerido(long seqGrupoDocumentoRequerido)
        {
            return GrupoDocumentoRequeridoDomainService.BuscarGrupoDocumentoRequerido(seqGrupoDocumentoRequerido).Transform<GrupoDocumentoRequeridoData>();
        }

        public SMCPagerData<GrupoDocumentoRequeridoListarData> BuscarGruposDocumentosRequeridos(GrupoDocumentoRequeridoFiltroData filtro)
        {
            return GrupoDocumentoRequeridoDomainService.BuscarGruposDocumentosRequeridos(filtro.Transform<GrupoDocumentoRequeridoFiltroVO>()).Transform<SMCPagerData<GrupoDocumentoRequeridoListarData>>();
        }

        public List<SMCDatasourceItem> BuscarDocumentosRequeridosSelect(bool uploadObrigatorio, long seqConfiguracaoEtapa)
        {
            return GrupoDocumentoRequeridoDomainService.BuscarDocumentosRequeridosSelect(uploadObrigatorio, seqConfiguracaoEtapa);
        }

        public long Salvar(GrupoDocumentoRequeridoData modelo)
        {
            return this.GrupoDocumentoRequeridoDomainService.Salvar(modelo.Transform<GrupoDocumentoRequeridoVO>());
        }

        public void ValidarModeloSalvar(GrupoDocumentoRequeridoData modelo)
        {
            this.GrupoDocumentoRequeridoDomainService.ValidarModeloSalvar(modelo.Transform<GrupoDocumentoRequeridoVO>());
        }

        public void Excluir(long seq)
        {
            this.GrupoDocumentoRequeridoDomainService.Excluir(seq);
        }
    }
}
