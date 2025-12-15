using SMC.Academico.Domain.Areas.CAM.Models;
using SMC.Academico.Domain.Areas.CAM.Specifications;
using SMC.Inscricoes.ServiceContract.Areas.INS.Data;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.CAM.DomainServices
{
    public class ConvocadoDomainService : AcademicoContextDomain<Convocado>
    {
        #region [ DomainService ]

        private ConvocadoOfertaDomainService ConvocadoOfertaDomainService => Create<ConvocadoOfertaDomainService>();

        #endregion [ DomainService ]

        /// <summary>
        /// Processa um inscrito para criação ou atualização do convocado
        /// </summary>
        /// <param name="inscrito">Dados do inscrito</param>
        /// <param name="seqChamada">Sequencial da chamada</param>
        /// <returns>Dados do convocado criado ou atualizado</returns>
        public Convocado ProcessarConvocado(PessoaIntegracaoData inscrito, long seqChamada)
        {
            var convocado = SearchByKey(new ConvocadoFilterSpecification() { SeqInscricaoGPI = inscrito.SeqInscricao }, x => x.Ofertas);

            if (convocado == null)
            {
                convocado = new Convocado
                {
                    SeqChamada = seqChamada,
                    SeqInscricaoGpi = inscrito.SeqInscricao,
                    Ofertas = new List<ConvocadoOferta>()
                };
                SaveEntity(convocado);
            }

            // Para cada oferta para qual o inscrito foi convocado
            foreach (var inscritoOferta in inscrito.Ofertas)
            {
                // Cria o convocado_oferta
                var oferta = new ConvocadoOferta
                {
                    SeqChamada = seqChamada,
                    SeqConvocado = convocado.Seq,
                    SeqInscricaoOfertaGpi = inscritoOferta.SeqInscricaoOferta
                };
                convocado.Ofertas.Add(oferta);
                ConvocadoOfertaDomainService.SaveEntity(oferta);
            }

            return convocado;
        }
    }
}
