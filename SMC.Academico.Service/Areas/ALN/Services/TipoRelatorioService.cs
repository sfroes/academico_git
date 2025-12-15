using SMC.Academico.Common.Areas.ALN.Constants;
using SMC.Academico.Common.Areas.ALN.Enums;
using SMC.Academico.Common.Areas.APR.Constants;
using SMC.Academico.Common.Areas.MAT.Constants;
using SMC.Academico.ServiceContract.Areas.ALN.Interfaces;
using SMC.Framework.Model;
using SMC.Framework.Security.Util;
using SMC.Framework.Service;
using SMC.Framework.Util;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Service.Areas.ALN.Services
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
                    case TipoRelatorio.DeclaracaoDisciplinasCursadas:
                        if (SMCSecurityHelper.Authorize(UC_APR_002_07_01.EXIBIR_RELATORIO_DECLARACAO_DISCIPLINAS_CURSADAS))
                            listaTiposRelatorio.Add(new SMCDatasourceItem((long)tipoRelatorio.Key, tipoRelatorio.Value));
                        break;

                    case TipoRelatorio.DeclaracaoMatricula:
                        if (SMCSecurityHelper.Authorize(UC_MAT_005_06_01.EXIBIR_RELATORIO_DECLARACAO_MATRICULA))
                            listaTiposRelatorio.Add(new SMCDatasourceItem((long)tipoRelatorio.Key, tipoRelatorio.Value));
                        break;

                    case TipoRelatorio.HistoricoEscolar:
                        if (SMCSecurityHelper.Authorize(UC_APR_002_03_01.EXIBIR_RELATORIO_HISTORICO_ESCOLAR))
                            listaTiposRelatorio.Add(new SMCDatasourceItem((long)tipoRelatorio.Key, tipoRelatorio.Value));
                        break;

                    case TipoRelatorio.HistoricoEscolarInterno:
                        if (SMCSecurityHelper.Authorize(UC_APR_002_05_01.EXIBIR_RELATORIO_HISTORICO_ESCOLAR_INTERNO))
                            listaTiposRelatorio.Add(new SMCDatasourceItem((long)tipoRelatorio.Key, tipoRelatorio.Value));
                        break;

                    case TipoRelatorio.IdentidadeEstudantil:
                        if (SMCSecurityHelper.Authorize(UC_ALN_001_06_01.EXIBIR_RELATORIO_IDENTIDADE_ESTUDANTIL))
                            listaTiposRelatorio.Add(new SMCDatasourceItem((long)tipoRelatorio.Key, tipoRelatorio.Value));
                        break;

                    case TipoRelatorio.ListagemAssinatura:
                        if (SMCSecurityHelper.Authorize(UC_ALN_001_10_01.EXIBIR_RELATORIO_LISTAGEM_ASSINATURA))
                            listaTiposRelatorio.Add(new SMCDatasourceItem((long)tipoRelatorio.Key, tipoRelatorio.Value));
                        break;

                    case TipoRelatorio.DeclaracaoGenerica:
                        if (SMCSecurityHelper.Authorize(UC_ALN_001_12_01.EMITIR_RELATORIO_DECLARACAO_GENERICA))
                        listaTiposRelatorio.Add(new SMCDatasourceItem((long)tipoRelatorio.Key, tipoRelatorio.Value));
                        break;
                }
            }

            return listaTiposRelatorio.OrderBy(o => o.Descricao).ToList();
        }
    }
}