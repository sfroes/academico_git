using SMC.Academico.Common.Constants;
using SMC.Academico.ServiceContract.Areas.ALN.Data;
using SMC.Academico.ServiceContract.Data;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;
using System.ServiceModel;

namespace SMC.Academico.ServiceContract.Areas.ALN.Interfaces
{
    [ServiceContract(Namespace = NAMESPACES.SERVICE)]
    public interface IFormaIngressoService : ISMCService
    {
        List<SMCDatasourceItem> BuscarFormasIngressoSelect(FormaIngressoFiltroData filtro);

        FormaIngressoData BuscarFormaIngresso(long seq);

        /// <summary>
        /// Recupera todas formas de ingresso associadas a algum tipo de vínculo de aluno da instituição
        /// <param name="filtro">Dados do filtro</param>
        /// </summary>
        /// <returns>Formas de vínculo associadas ordenadas por descrição</returns>
        List<SMCDatasourceItem> BuscarFormasIngressoInstituicaoNivelVinculoSelect(FormaIngressoFiltroData filtro);
    }
}