using SMC.Academico.Domain.Areas.SRC.DomainServices;
using SMC.Academico.Domain.Areas.SRC.ValueObjects;
using SMC.Academico.ServiceContract.Areas.SRC.Data;
using SMC.Academico.ServiceContract.Areas.SRC.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.Service;

namespace SMC.Academico.Service.Areas.SRC.Services
{
    public class TextoSecaoPaginaService : SMCServiceBase, ITextoSecaoPaginaService
    {
        #region Domain Service

        private TextoSecaoPaginaDomainService TextoSecaoPaginaDomainService { get => Create<TextoSecaoPaginaDomainService>(); }

        #endregion

        public TextoSecaoPaginaData BuscarTextoSecaoPagina(long seqTextoSecaoPagina)
        {
            return TextoSecaoPaginaDomainService.BuscarTextoSecaoPagina(seqTextoSecaoPagina).Transform<TextoSecaoPaginaData>();
        }
       
        public long Salvar(TextoSecaoPaginaData modelo)
        {
            return TextoSecaoPaginaDomainService.Salvar(modelo.Transform<TextoSecaoPaginaVO>());
        }
    }
}
