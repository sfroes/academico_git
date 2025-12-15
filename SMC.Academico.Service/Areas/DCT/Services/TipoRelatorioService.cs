using SMC.Academico.Common.Areas.DCT.Constants;
using SMC.Academico.Common.Areas.DCT.Enums;
using SMC.Academico.ServiceContract.Areas.DCT.Interfaces;
using SMC.Framework.Model;
using SMC.Framework.Security.Util;
using SMC.Framework.Service;
using SMC.Framework.Util;
using System.Collections.Generic;

namespace SMC.Academico.Service.Areas.DCT.Services
{
    public class TipoRelatorioService : SMCServiceBase, ITipoRelatorioService
    {
        public List<SMCDatasourceItem> BuscarTiposRelatorioSelect()
        {
            var listaTiposRelatorio = new List<SMCDatasourceItem>();

            foreach (var tipoRelatorio in SMCEnumHelper.GenerateKeyValuePair<TipoRelatorio>())
            {
                switch (tipoRelatorio.Key)
                {
                    case TipoRelatorio.LogAtualizacaoColaborador:
                        if (SMCSecurityHelper.Authorize(UC_DCT_001_07_02.EMITIR_RELATORIO_LOG_ATUALIZACAO_COLABORADOR))
                            listaTiposRelatorio.Add(new SMCDatasourceItem((long)tipoRelatorio.Key, tipoRelatorio.Value));
                        break;
                    case TipoRelatorio.CertificadoPosDoutor:
                        if (SMCSecurityHelper.Authorize(UC_DCT_001_07_01.CENTRAL_RELATORIOS_COLABORADOR))
                            listaTiposRelatorio.Add(new SMCDatasourceItem((long)tipoRelatorio.Key, tipoRelatorio.Value));
                        break;
                }
            }

            return listaTiposRelatorio;
        }
    }
}