using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.Academico.Common.Areas.SRC.Exceptions;
using SMC.Academico.Domain.Areas.SRC.Models;
using SMC.Academico.Domain.Areas.SRC.Specifications;
using SMC.Academico.Domain.Areas.SRC.ValueObjects;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Specification;
using SMC.Framework.UnitOfWork;
using System;
using System.Linq;

namespace SMC.Academico.Domain.Areas.SRC.DomainServices
{
    public class ConfiguracaoEtapaBloqueioDomainService : AcademicoContextDomain<ConfiguracaoEtapaBloqueio>
    {
        #region DomainServices
      
        private ConfiguracaoEtapaDomainService ConfiguracaoEtapaDomainService
        {
            get { return this.Create<ConfiguracaoEtapaDomainService>(); }
        }

        private SolicitacaoServicoDomainService SolicitacaoServicoDomainService
        {
            get { return this.Create<SolicitacaoServicoDomainService>(); }
        }

        #endregion

        public ConfiguracaoEtapaBloqueioVO BuscarConfiguracaoEtapaBloqueio(long seqConfiguracaoEtapaBloqueio)
        {            
            var configuracaoEtapaBloqueio = this.SearchByKey(new SMCSeqSpecification<ConfiguracaoEtapaBloqueio>(seqConfiguracaoEtapaBloqueio), x => x.ConfiguracaoEtapa.ProcessoEtapa, x => x.MotivoBloqueio);
            var situacaoEtapa = configuracaoEtapaBloqueio.ConfiguracaoEtapa.ProcessoEtapa.SituacaoEtapa;

            var retorno = configuracaoEtapaBloqueio.Transform<ConfiguracaoEtapaBloqueioVO>();
            retorno.DescricaoConfiguracaoEtapa = configuracaoEtapaBloqueio.ConfiguracaoEtapa.Descricao;
            retorno.SeqProcessoEtapa = configuracaoEtapaBloqueio.ConfiguracaoEtapa.SeqProcessoEtapa;
            retorno.DescricaoEtapaSgf = configuracaoEtapaBloqueio.ConfiguracaoEtapa.ProcessoEtapa.DescricaoEtapa;
            retorno.DescricaoMotivo = configuracaoEtapaBloqueio.MotivoBloqueio.Descricao;
            retorno.SolicitacaoAssociada = SolicitacaoServicoDomainService.Count(new SolicitacaoServicoFilterSpecification() { SeqProcesso = configuracaoEtapaBloqueio.ConfiguracaoEtapa.ProcessoEtapa.SeqProcesso }) > 0;
            retorno.CamposReadyOnly = situacaoEtapa == SituacaoEtapa.Liberada || situacaoEtapa == SituacaoEtapa.Encerrada;

            return retorno;
        }

        public SMCPagerData<ConfiguracaoEtapaBloqueioListarVO> BuscarConfiguracoesEtapaBloqueio(ConfiguracaoEtapaBloqueioFiltroVO filtro)
        {
            var spec = filtro.Transform<ConfiguracaoEtapaBloqueioFilterSpecification>();
            spec.SetOrderBy(o => o.MotivoBloqueio.TipoBloqueio.Descricao);
            spec.SetOrderBy(o => o.MotivoBloqueio.Descricao);

            var lista = this.SearchProjectionBySpecification(spec, a => new ConfiguracaoEtapaBloqueioListarVO()
            {
                Seq = a.Seq,
                SeqConfiguracaoEtapa = a.SeqConfiguracaoEtapa,
                SeqProcessoEtapa = a.ConfiguracaoEtapa.SeqProcessoEtapa,
                SituacaoEtapa = a.ConfiguracaoEtapa.ProcessoEtapa.SituacaoEtapa,
                SeqMotivoBloqueio = a.SeqMotivoBloqueio,
                DescricaoMotivo = a.MotivoBloqueio.TipoBloqueio.Descricao + " - " + a.MotivoBloqueio.Descricao,
                AmbitoBloqueio = a.AmbitoBloqueio

            }, out int total).ToList();

            return new SMCPagerData<ConfiguracaoEtapaBloqueioListarVO>(lista, total);            
        }       

        public long Salvar(ConfiguracaoEtapaBloqueioVO modelo)
        {
            ValidarModeloSalvar(modelo);

            var dominio = modelo.Transform<ConfiguracaoEtapaBloqueio>();
          
            this.SaveEntity(dominio);

            return dominio.Seq;
        }

        public void ValidarModeloSalvar(ConfiguracaoEtapaBloqueioVO modelo)
        {
            var situacaoEtapa = this.ConfiguracaoEtapaDomainService.SearchProjectionByKey(new SMCSeqSpecification<ConfiguracaoEtapa>(modelo.SeqConfiguracaoEtapa), x => x.ProcessoEtapa.SituacaoEtapa);
            var configuracaoBloqueioPorMotivoBloqueio = this.SearchBySpecification(new ConfiguracaoEtapaBloqueioFilterSpecification() { SeqConfiguracaoEtapa = modelo.SeqConfiguracaoEtapa, SeqMotivoBloqueio = modelo.SeqMotivoBloqueio }).ToList();

            if (situacaoEtapa == SituacaoEtapa.Liberada || situacaoEtapa == SituacaoEtapa.Encerrada)
                throw new ConfiguracaoEtapaOperacaoNaoPermitidaException();

            if (configuracaoBloqueioPorMotivoBloqueio.Any(a => a.Seq != modelo.Seq))
                throw new MotivoBloqueioJaAssociadoEmOutraEtapaBloqueioException();
        }

        public void Excluir(long seq)
        {
            using (var unitOfWork = SMCUnitOfWork.Begin())
            {
                try
                {
                    ValidarModeloExcluir(seq);

                    var configuracaoEtapaBloqueio = this.SearchByKey(new SMCSeqSpecification<ConfiguracaoEtapaBloqueio>(seq));                                      
                    this.DeleteEntity(configuracaoEtapaBloqueio);

                    unitOfWork.Commit();
                }
                catch (Exception)
                {
                    unitOfWork.Rollback();
                    throw;
                }
            }
        }

        public void ValidarModeloExcluir(long seq)
        {
            var configuracaoEtapaBloqueio = this.SearchByKey(new SMCSeqSpecification<ConfiguracaoEtapaBloqueio>(seq), x => x.ConfiguracaoEtapa.ProcessoEtapa);

            var situacaoEtapa = configuracaoEtapaBloqueio.ConfiguracaoEtapa.ProcessoEtapa.SituacaoEtapa;
            if (situacaoEtapa == SituacaoEtapa.Liberada || situacaoEtapa == SituacaoEtapa.Encerrada)
                throw new ConfiguracaoEtapaOpcaoIndisponivelEtapaException();
        }
    }
}
