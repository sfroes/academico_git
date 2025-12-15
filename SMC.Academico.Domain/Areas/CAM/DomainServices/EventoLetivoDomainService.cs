using SMC.Academico.Common.Areas.CAM.Enums;
using SMC.Academico.Common.Areas.CAM.Exceptions;
using SMC.Academico.Common.Areas.CAM.Includes;
using SMC.Academico.Domain.Areas.CAM.Models;
using SMC.Academico.Domain.Areas.CAM.Specifications;
using SMC.Academico.Domain.Areas.CAM.ValueObjects;
using SMC.Academico.Domain.Areas.ORG.DomainServices;
using SMC.Academico.Domain.Areas.ORG.Models;
using SMC.Calendarios.ServiceContract.Areas.CLD.Data;
using SMC.Calendarios.ServiceContract.Areas.CLD.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SMC.Academico.Domain.Areas.CAM.DomainServices
{
    public class EventoLetivoDomainService : AcademicoContextDomain<EventoLetivo>
    {
        #region DomainServices

        private InstituicaoEnsinoDomainService InstituicaoEnsinoDomainService
        {
            get { return this.Create<InstituicaoEnsinoDomainService>(); }
        }

        private InstituicaoTipoEventoParametroDomainService InstituicaoTipoEventoParametroDomainService
        {
            get { return Create<InstituicaoTipoEventoParametroDomainService>(); }
        }

        private CicloLetivoDomainService CicloLetivoDomainService
        {
            get { return Create<CicloLetivoDomainService>(); }
        }

        private NivelEnsinoDomainService NivelEnsinoDomainService
        {
            get { return Create<NivelEnsinoDomainService>(); }
        }

        private EntidadeDomainService EntidadeDomainService
        {
            get { return Create<EntidadeDomainService>(); }
        }

        #endregion DomainServices

        #region Services

        private ITipoEventoService TipoEventoService
        {
            get { return this.Create<ITipoEventoService>(); }
        }

        #endregion Services

        public EventoLetivoVO BuscarEventoLetivo(long seqEventoLetivo)
        {
            var result = this.SearchByKey(new SMCSeqSpecification<EventoLetivo>(seqEventoLetivo));

            return result.Transform<EventoLetivoVO>();
        }

        public SMCPagerData<EventoLetivoListaVO> BuscarEventosLetivos(EventoLetivoFiltroVO filtros)
        {
            //Resupera a instituição de ensino
            var instituicaoEnsino = this.InstituicaoEnsinoDomainService.SearchByKey(new SMCSeqSpecification<InstituicaoEnsino>(filtros.SeqInstituicaoEnsino.GetValueOrDefault()));

            //Cria o specification para consulta
            var spec = filtros.Transform<EventoLetivoFilterSpecification>();

            //Recupera os eventos letivos
            var result = this.SearchProjectionBySpecification(spec, eventoLetivo => new EventoLetivoListaVO()
            {
                Seq = eventoLetivo.Seq,
                Descricao = eventoLetivo.Descricao,
                CicloLetivo = eventoLetivo.CicloLetivoTipoEvento.CicloLetivo.Descricao,
                DataInicio = eventoLetivo.DataInicio,
                DataFim = eventoLetivo.DataFim,
                NivelEnsino = eventoLetivo.NiveisEnsino.Select(n => n.Descricao).ToList(),
                SeqTipoEvento = eventoLetivo.CicloLetivoTipoEvento.InstituicaoTipoEvento.SeqTipoEventoAgd,
            }, out int total).ToList();

            //Recupera os tipos de eventos do AGD da instituição de ensino, para setar a descrição
            var tiposEventos = this.TipoEventoService.BuscarTiposEventosSelect(new TipoEventoFiltroData()
            {
                SeqUnidadeResponsavel = instituicaoEnsino.SeqUnidadeResponsavelAgd,
                Seqs = result.Select(x => x.SeqTipoEvento).ToList()
            });

            //Seta a descrição do tipo de evento
            foreach (var item in result)
                item.TipoEvento = tiposEventos.FirstOrDefault(t => t.Seq == item.SeqTipoEvento).Descricao;

            return new SMCPagerData<EventoLetivoListaVO>(result, total);
        }

        public long SalvarEventoLetivo(EventoLetivoVO modelo)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Busca o período do evento letivo. RN_CAM_30.
        /// </summary>
        public (DateTime DataInicio, DateTime DataFim) BuscarPeriodoEventoLetivo(EventoLetivoSpecification filtro)
        {
            var paramSpec = filtro.Transform<ParametrosObrigatoriosInstituicaoTipoEventoSpecification>();
            var instituicaoTipoEventoParametros = InstituicaoTipoEventoParametroDomainService
                                                    .SearchBySpecification(paramSpec, IncludesInstituicaoTipoEvento.InstituicaoTipoEvento).ToList();

            var parametrosFaltantes = new List<string>();
            if (instituicaoTipoEventoParametros.Any(f => f.TipoParametroEvento == TipoParametroEvento.Localidade))
            {
                if (!filtro.SeqLocalidade.HasValue)
                {
                    parametrosFaltantes.Add(nameof(filtro.SeqLocalidade));
                }
            }
            else
            {
                filtro.SeqLocalidade = null;
            }

            if (instituicaoTipoEventoParametros.Any(f => f.TipoParametroEvento == TipoParametroEvento.CursoOfertaLocalidade))
            {
                if (!filtro.SeqCursoOfertaLocalidade.HasValue)
                {
                    parametrosFaltantes.Add(nameof(filtro.SeqCursoOfertaLocalidade));
                }
            }
            else
            {
                filtro.SeqCursoOfertaLocalidade = null;
            }

            if (instituicaoTipoEventoParametros.Any(f => f.TipoParametroEvento == TipoParametroEvento.EntidadeResponsavel))
            {
                if (!filtro.SeqEntidadeResponsavel.HasValue)
                {
                    parametrosFaltantes.Add(nameof(filtro.SeqEntidadeResponsavel));
                }
            }
            else
            {
                filtro.SeqEntidadeResponsavel = null;
            }

            if (instituicaoTipoEventoParametros.Any(f => f.TipoParametroEvento == TipoParametroEvento.TipoAluno))
            {
                if (!filtro.TipoAluno.HasValue)
                    parametrosFaltantes.Add(nameof(filtro.TipoAluno));
            }
            else
            {
                filtro.TipoAluno = null;
            }

            if (instituicaoTipoEventoParametros.Any() &&
                instituicaoTipoEventoParametros.First().InstituicaoTipoEvento.AbrangenciaEvento == AbrangenciaEvento.PorNivelEnsino)
            {
                if (!filtro.SeqNivelEnsino.HasValue)
                    parametrosFaltantes.Add(nameof(filtro.SeqNivelEnsino));
            }
            else
            {
                // Se o parâmetro não é obrigatório mas foi informado, limpa.
                filtro.SeqNivelEnsino = null;
            }

            if (parametrosFaltantes.Count > 0)
                throw new PeriodoEventoLetivoNullArgumentException(string.Join(", ", parametrosFaltantes));

            var datas = SearchProjectionByKey(filtro, x => new { x.DataInicio, x.DataFim, x.CicloLetivoTipoEvento.InstituicaoTipoEvento.AbrangenciaEvento });

            if (datas == null)
            {
                // Se nenhum evento letivo seja encontrado, aborta a operação e retorna erro
                var cicloLetivo = CicloLetivoDomainService.SearchProjectionByKey(new SMCSeqSpecification<CicloLetivo>(filtro.SeqCicloLetivo), x => x.Descricao);
                var tipoEvento = TipoEventoService.BuscarTipoEventoPorToken(filtro.TokenTipoEvento).Descricao;
                var exceptionMessage = new StringBuilder($@"Ciclo letivo: {cicloLetivo}, Tipo de evento: {tipoEvento}");
                if (filtro.SeqNivelEnsino.HasValue)
                {
                    var nivelEnsino = NivelEnsinoDomainService.SearchProjectionByKey(new SMCSeqSpecification<NivelEnsino>(filtro.SeqNivelEnsino.Value), x => x.Descricao);
                    exceptionMessage.Append($", Nivel ensino: {nivelEnsino}");
                }
                if (filtro.TipoAluno.HasValue)
                {
                    exceptionMessage.Append($", Tipo aluno: {filtro.TipoAluno.ToString()}");
                }
                if (filtro.SeqLocalidade.HasValue)
                {
                    var localidade = EntidadeDomainService.SearchProjectionByKey(new SMCSeqSpecification<Entidade>(filtro.SeqLocalidade.Value), x => x.Nome);
                    exceptionMessage.Append($", Localidade: {localidade}");
                }
                if (filtro.SeqCursoOfertaLocalidade.HasValue)
                {
                    var cursoOfertaLocalidade = EntidadeDomainService.SearchProjectionByKey(new SMCSeqSpecification<Entidade>(filtro.SeqCursoOfertaLocalidade.Value), x => x.Nome);
                    exceptionMessage.Append($", Curso oferta localidade: {cursoOfertaLocalidade}");
                }
                if (filtro.SeqEntidadeResponsavel.HasValue)
                {
                    var entidadeResponsavel = EntidadeDomainService.SearchProjectionByKey(new SMCSeqSpecification<Entidade>(filtro.SeqEntidadeResponsavel.Value), x => x.Nome);
                    exceptionMessage.Append($", Entidade responsável: {entidadeResponsavel}");
                }

                throw new PeriodoEventoLetivoVazioException(exceptionMessage.ToString());
            }

            return (datas.DataInicio, datas.DataFim);
        }
    }
}