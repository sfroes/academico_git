using SMC.Academico.ServiceContract.Areas.SRC.Data;
using SMC.Framework.Service;

namespace SMC.Academico.ServiceContract.Areas.SRC.Interfaces
{
    public interface ITextoSecaoPaginaService : ISMCService
    {
        TextoSecaoPaginaData BuscarTextoSecaoPagina(long seqTextoSecaoPagina);

        long Salvar(TextoSecaoPaginaData modelo);
    }
}
