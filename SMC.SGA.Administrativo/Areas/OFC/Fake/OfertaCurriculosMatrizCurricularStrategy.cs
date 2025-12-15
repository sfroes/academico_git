using SMC.Framework.Fake;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace SMC.SGA.Administrativo.Areas.OFC.Fake
{
    public class OfertaCurriculosMatrizCurricularStrategy : SMCFakeStrategyBase
    {
        /// <summary>
        /// Define a prioridade da estratégia de fake
        /// </summary>
        public override int Priority
        {
            get { return 99; }
        }

        protected override bool CheckProperty(PropertyInfo propertyInfo)
        {
            return propertyInfo.PropertyType == typeof(List<string>) && propertyInfo.Name == "Ofertas";
        }

        protected override object CreateProperty(PropertyInfo propertyInfo)
        {
            return GeraLista();
        }

        private List<string> GeraLista()
        {
            List<string> lista = new List<string>();

            Random r = new Random(DateTime.Now.Millisecond);
            int random = r.Next(100);

            if (random <= 50)
            {
                lista.Add("Coração Eucarístico - Coração Eucarístico - À Distância - Manhã");
                lista.Add("Coração Eucarístico - Praça da Liberdade - À Distância - Tarde");
                lista.Add("São Gabriel - São Gabriel - Presencial - Tarde");
            }
            else
            {
                lista.Add("Betim - Betim - Presencial - Noite");
                lista.Add("Coração Eucarístico - Coração Eucarístico - Presencial - Noite");
            }

            return lista;
        }
    }
}