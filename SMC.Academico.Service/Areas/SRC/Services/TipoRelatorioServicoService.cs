using SMC.Academico.Common.Areas.SRC.Constants;
using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.Academico.ServiceContract.Areas.SRC.Interfaces;
using SMC.Framework.Model;
using SMC.Framework.Security.Util;
using SMC.Framework.Service;
using SMC.Framework.Util;
using System.Collections.Generic;

namespace SMC.Academico.Service.Areas.SRC.Services
{
    public class TipoRelatorioServicoService : SMCServiceBase, ITipoRelatorioServicoService
    {
        public List<SMCDatasourceItem> BuscarTiposRelatorioServicoSelect()
        {
            var listaTiposRelatorioServico = new List<SMCDatasourceItem>();

            foreach (var tipoRelatorio in SMCEnumHelper.GenerateKeyValuePair<TipoRelatorioServico>())
            {
                switch (tipoRelatorio.Key)
                {
                    case TipoRelatorioServico.PosicaoConsolidadaServicoCicloLetivo:
                        if (SMCSecurityHelper.Authorize(UC_SRC_005_03_02.EXIBIR_RELATORIO_POSICAO_CONSOLIDADA_SERVICO_CICLO_LETIVO))
                            listaTiposRelatorioServico.Add(new SMCDatasourceItem((long)tipoRelatorio.Key, tipoRelatorio.Value));
                        break;

                    case TipoRelatorioServico.SolicitacoesBloqueio:
                        if (SMCSecurityHelper.Authorize(UC_SRC_005_03_03.EXIBIR_RELATORIO_SOLICITACOES_COM_BLOQUEIOS))
                            listaTiposRelatorioServico.Add(new SMCDatasourceItem((long)tipoRelatorio.Key, tipoRelatorio.Value));
                        break;
                }
            }

            return listaTiposRelatorioServico;
        }
    }
}
