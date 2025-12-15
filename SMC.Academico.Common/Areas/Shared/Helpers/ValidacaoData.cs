using SMC.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Common.Areas.Shared.Helpers
{
    public class ValidacaoData
    {
        public static bool ValidarSobreposicaoPeriodos<T>(List<T> lista, string propertyDataInicio, string propertyDataFinal)
        {
            if (lista != null && lista
               .Any(a =>
               {
                   return lista.FirstOrDefault(f =>
                           !object.ReferenceEquals(f, a) &&
                           (
                              //Caso seja uma data sem término, não pode iniciar antes do término de nenhuma outra
                              (
                                   !GetPropValue(a, propertyDataFinal).HasValue &&
                                    GetPropValue(a, propertyDataInicio) < (GetPropValue(f, propertyDataFinal) ?? DateTime.MaxValue)
                              ) ||

                              //Caso seja uma data fechada, não pode colidir com o intervalo de nenhuma outra (outras datas sem témino terão seu término extrapolado para Max)
                              (
                                   GetPropValue(a, propertyDataInicio) >= GetPropValue(f, propertyDataInicio) &&
                                   GetPropValue(a, propertyDataInicio) <= (GetPropValue(f, propertyDataFinal) ?? DateTime.MaxValue) ||
                                   GetPropValue(a, propertyDataFinal) >= GetPropValue(f, propertyDataInicio) &&
                                   GetPropValue(a, propertyDataFinal) <= (GetPropValue(f, propertyDataFinal) ?? DateTime.MaxValue)
                              )

                           )) != null;
               }))
            {
                //Período inválido
                return false;
            }
            else
            {
                //Período válido
                return true;
            }
        }

        /// <summary>
        /// Valida a continuidade numa sequencia de períodos
        /// </summary>
        /// <typeparam name="T">Tipo dos itens com os períodos</typeparam>
        /// <param name="itens">Itens com os períodos</param>
        /// <param name="dataInicio">Função para recuperar a data de inicio</param>
        /// <param name="dataFim">Função para recuperar a data fim</param>
        /// <returns>Verdadeiro caso não tenha intervalos entre os periodos, tenha apenas um período ou nenhum período</returns>
        public static bool ValidarContinuidadePeriodos<T>(IEnumerable<T> itens, Func<T, DateTime> dataInicio, Func<T, DateTime?> dataFim)
        {
            if (itens.SMCCount() <= 1)
                return true;

            // Ordena os itens
            var itensOrdenados = itens.OrderBy(dataInicio);
            // Reseta a última da fim
            var ultimaDataFim = DateTime.MinValue;

            // Valida se não existe intervalo entre os periodos
            foreach (var item in itensOrdenados)
            {
                // Caso a última da fim esteja setada e seja menor que a data inicio atual menos um dia, existe intervalo
                if (ultimaDataFim != DateTime.MinValue && dataInicio(item).AddDays(-1) > ultimaDataFim)
                    return false;
                ultimaDataFim = dataFim(item) ?? DateTime.MinValue;
            }

            return true;
        }

        public static DateTime? GetPropValue(object src, string propName)
        {
            return (DateTime?)src.GetType().GetProperty(propName).GetValue(src, null);
        }
    }
}