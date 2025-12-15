using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Academico.ServiceContract.Areas.PES.Data;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.PES.Interfaces
{
    public interface ITipoMensagemService : ISMCService
    { 
        List<SMCDatasourceItem> BuscarTiposMensagemSelect();

        SMCPagerData<TipoMensagemListaData> ListarTipoMensagem(TipoMensagemFiltroData filtro);

        List<SMCDatasourceItem> BuscarTagsSelect(TipoTag filtro);

        long SalvarTipoMensagem(TipoMensagemData tipoMensagem);

        bool PermiteCadastroManual(long seqTipoMensagem);

        List<string> BuscarTiposAtuacao(long seqTipoMensagem);

        List<string> BuscarTiposUso(long seqTipoMensagem);

        List<string> BuscarTags(long seqTipoMensagem);

        /// <summary>
        /// Buscar tipo de mensagem
        /// </summary>
        /// <param name="seq">Seuencial do tipo de mensagem</param>
        /// <returns></returns>
        TipoMensagemData BuscarTipoMensagem(long seq);
    }
}
