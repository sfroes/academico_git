using SMC.Academico.Common.Areas.ORG.Exceptions;
using SMC.Academico.Common.Areas.ORG.Includes;
using SMC.Academico.Domain.Areas.ORG.Models;
using SMC.Academico.Domain.Areas.ORG.Specifications;
using SMC.Academico.Domain.Areas.ORG.ValueObjects;
using SMC.Calendarios.ServiceContract.Areas.CLD.Interfaces;
using SMC.Framework.Domain;
using SMC.Framework.Extensions;
using SMC.Framework.Specification;
using System.Linq;

namespace SMC.Academico.Domain.Areas.ORG.DomainServices
{
    public class InstituicaoLocalidadeAgendaDomainService : AcademicoContextDomain<InstituicaoLocalidadeAgenda>
    {
        #region Serviços

        private IEventoService EventoService
        {
            get { return this.Create<IEventoService>(); }
        }

        private IAgendaService AgendaService
        {
            get { return this.Create<IAgendaService>(); }
        }

        private TipoAgendaDomainService TipoAgendaDomainService
        {
            get { return this.Create<TipoAgendaDomainService>(); }
        }

        #endregion Serviços

        public long SalvarInstituicaoLocalidadeAgenda(InstituicaoLocalidadeAgendaVO modelo)
        {
            //Transformando o modelo
            var instituicaoLocalidadeAgenda = modelo.Transform<InstituicaoLocalidadeAgenda>();

            //Recupera o registo atual
            var instituicaoLocalidadeAgendaExistente = this.SearchByKey(new SMCSeqSpecification<InstituicaoLocalidadeAgenda>(modelo.Seq), IncludesInstituicaoLocalidadeAgenda.TipoAgenda);

            //Valida o tipo de agenda e lançamento
            ValidarTipoAgendaLancamento(instituicaoLocalidadeAgendaExistente, instituicaoLocalidadeAgenda);

            //Caso seja uma edição
            if (modelo.Seq > 0)
            {
                //Valida a troca da agenda
                ValidarTrocaAgenda(instituicaoLocalidadeAgendaExistente, instituicaoLocalidadeAgenda);
            }

            //Salva o modelo
            this.SaveEntity(instituicaoLocalidadeAgenda);

            return instituicaoLocalidadeAgenda.Seq;
        }

        public void ExcluirInstituicaoLocalidadeAgenda(long seqInstituicaoLocalidadeAgenda)
        {
            var instituicaoLocalidadeAgendaeExistente = this.SearchByKey(new SMCSeqSpecification<InstituicaoLocalidadeAgenda>(seqInstituicaoLocalidadeAgenda));

            //Verifica se a agenda existente possui eventos no AGD e dispara excessão
            if (ExisteEventoAgenda(instituicaoLocalidadeAgendaeExistente.SeqAgendaAgd))
            {
                var agenda = this.AgendaService.BuscarAgenda(instituicaoLocalidadeAgendaeExistente.SeqAgendaAgd);

                throw new ExisteEventoAgendaException(agenda.Descricao);
            }
        }

        private bool ExisteEventoAgenda(long seqAgendaAgd)
        {
            return this.EventoService.ExisteEventoAgenda(seqAgendaAgd);
        }

        private void ValidarTipoAgendaLancamento(InstituicaoLocalidadeAgenda modeloExistente, InstituicaoLocalidadeAgenda modelo)
        {
            //Recupera o tipo da agenda informado
            var tipoAgenda = this.TipoAgendaDomainService.SearchByKey(new SMCSeqSpecification<TipoAgenda>(modelo.SeqTipoAgenda));

            //Se o tipo de agenda informado estiver configurado para ter lançamento de evento letivo e
            //estiver configurado para replicar o lançamento por localidade
            if (tipoAgenda.EventoLetivo && tipoAgenda.ReplicarLancamentoPorLocalidade)
            {
                //Cria p specification para o filtro
                var spec = new InstituicaoLocalidadeAgendaFilterSpecification() { SeqTipoAgenda = modelo.SeqTipoAgenda, SeqEntidadeLocalidade = modelo.SeqEntidadeLocalidade };

                //Recuperar as agendas existentes
                var agendas = this.SearchBySpecification(spec);

                //Não permitir que uma mesma agenda se repita por localidade e tipo de agenda
                if (agendas.Any(a => a.SeqAgendaAgd == modelo.SeqAgendaAgd))
                {
                    var agenda = this.AgendaService.BuscarAgenda(modelo.SeqAgendaAgd);

                    if (modelo.Seq > 0)
                        throw new ObrigatorioAgendaDistintaAlteracaoException(tipoAgenda.Descricao, agenda.Descricao);
                    else
                        throw new ObrigatorioAgendaDistintaInclusaoException(tipoAgenda.Descricao, agenda.Descricao);
                }
            }

            //Se o tipo de agenda informado estiver configurado para ter lançamento de evento letivo e
            //NÃO estiver configurado para replicar o lançamento por localidade
            if (tipoAgenda.EventoLetivo && !tipoAgenda.ReplicarLancamentoPorLocalidade)
            {
                //Cria p specification para o filtro
                var spec = new InstituicaoLocalidadeAgendaFilterSpecification() { SeqTipoAgenda = modelo.SeqTipoAgenda, SeqEntidadeLocalidade = modelo.SeqEntidadeLocalidade };

                //Recuperar as agendas existentes
                var agendas = this.SearchBySpecification(spec);

                //Não permitir que a agenda seja distinta entre as localidades
                if (agendas.Any(a => a.SeqAgendaAgd != modelo.SeqAgendaAgd))
                {
                    var agenda = this.AgendaService.BuscarAgenda(modelo.SeqAgendaAgd);

                    if (modelo.Seq > 0)
                        throw new ObrigatorioMesmaAgendaAlteracaoException(tipoAgenda.Descricao);
                    else
                        throw new ObrigatorioMesmaAgendaInclusaoException(tipoAgenda.Descricao);
                }
            }
        }

        private void ValidarTrocaAgenda(InstituicaoLocalidadeAgenda modeloExistente, InstituicaoLocalidadeAgenda modelo)
        {
            //Se o tipo de agenda indicar dia letivo
            if (modeloExistente.TipoAgenda.EventoLetivo)
            {
                //Caso a agenda tenha sido alterada pelo usuário
                if (modeloExistente.SeqAgendaAgd != modelo.SeqAgendaAgd)
                {
                    //Verifica se a agenda atual possui eventos no AGD e dispara excessão
                    if (ExisteEventoAgenda(modeloExistente.SeqAgendaAgd))
                    {
                        var agenda = this.AgendaService.BuscarAgenda(modeloExistente.SeqAgendaAgd);

                        throw new ExisteEventoAgendaException(agenda.Descricao);
                    }
                }
            }
        }
    }
}