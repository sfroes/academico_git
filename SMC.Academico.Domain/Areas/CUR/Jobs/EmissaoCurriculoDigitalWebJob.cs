using SMC.Academico.Domain.Areas.CUR.DomainServices;
using SMC.Academico.Domain.Areas.CUR.ValueObjects;
using SMC.Framework.Domain;
using SMC.Framework.Exceptions;
using SMC.Framework.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Areas.CUR.Jobs
{
    public class EmissaoCurriculoDigitalWebJob : SMCWebJobBase<EmissaoCurriculoDigitalSATVO, CurriculoDigitalVO>
    {
        #region [DomainService]

        private CurriculoDomainService CurriculoDomainService => this.Create<CurriculoDomainService>();

        #endregion

        public int TotalCurriculos { get; set; }

        public int QuantidadeCurriculosProcessados { get; set; }

        public override ICollection<CurriculoDigitalVO> GetItems(EmissaoCurriculoDigitalSATVO filter)
        {
            Scheduler.LogSucess($"Recuperando os currículos que serão emitidos");

            var listaCurriculo = new List<CurriculoDigitalVO>();

            if (filter.SeqCurriculo.HasValue && filter.SeqCurriculo.Value == 0)
                filter.SeqCurriculo = null;
            if (filter.CodigoCurriculoMigracao.HasValue && filter.CodigoCurriculoMigracao.Value == 0)
                filter.CodigoCurriculoMigracao = null;

            if ((filter.SeqCurriculo.HasValue) ||
                (filter.CodigoCurriculoMigracao.HasValue))
            {
                var dadosCurriculo = CurriculoDomainService.BuscarDadosCurriculo(filter.SeqCurriculo, filter.CodigoCurriculoMigracao);
                if (dadosCurriculo != null)
                    listaCurriculo.Add(dadosCurriculo);
            }
            else
            {
                listaCurriculo = CurriculoDomainService.BuscarCurriculosParaEmissaoDigital();
            }

            if (listaCurriculo.Any() && listaCurriculo.Count > 0)
            {
                Scheduler.LogSucess($"Foram encontrados {listaCurriculo.Count} currículos");
                TotalCurriculos = listaCurriculo.Count;
                QuantidadeCurriculosProcessados = 0;
            }
            else
                Scheduler.LogSucess("Não foram encontrados currículos para emissão");

            return listaCurriculo;
        }

        public override bool ProcessItem(CurriculoDigitalVO item)
        {
            using (var transaction = SMCUnitOfWork.Begin())
            {
                try
                {
                    Scheduler.LogSucess($"Processando dados do currículo: {item.SeqCurriculo}.");

                    CurriculoDomainService.EmitirCurriculo(item);
                    QuantidadeCurriculosProcessados++;

                    if ((QuantidadeCurriculosProcessados % 50) == 0)
                    {
                        Scheduler.LogSucess($"Processando {QuantidadeCurriculosProcessados} de {TotalCurriculos}");
                    }

                    transaction.Commit();
                }
                catch (SMCApplicationException ex)
                {
                    Scheduler.LogWaring($"Ocorreu um erro ao emitir o currículo {item.SeqCurriculo}. Erro: {ex.Message}");
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    Scheduler.LogWaring($"Ocorreu um erro ao emitir o currículo {item.SeqCurriculo}. Erro: {ex.Message}");
                    return false;
                }

                return true;
            }
        }
    }
}
