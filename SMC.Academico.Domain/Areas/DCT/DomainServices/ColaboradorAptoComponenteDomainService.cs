using SMC.Academico.Domain.Areas.ALN.Models;
using SMC.Academico.Domain.Areas.CUR.DomainServices;
using SMC.Academico.Domain.Areas.DCT.Models;
using SMC.Academico.Domain.Areas.DCT.Specifications;
using SMC.Academico.Domain.Areas.DCT.ValueObjects;
using SMC.Framework.Domain;
using SMC.Framework.Extensions;
using SMC.Framework.Util;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Areas.DCT.DomainServices
{
    public class ColaboradorAptoComponenteDomainService : AcademicoContextDomain<ColaboradorAptoComponente>
    {
        #region [ DomainService ]

        private ConfiguracaoComponenteDomainService ConfiguracaoComponenteDomainService => Create<ConfiguracaoComponenteDomainService>();

        private ColaboradorDomainService ColaboradorDomainService => Create<ColaboradorDomainService>();

        #endregion

        public List<ColaboradorAptoComponenteListaVO> BuscarColaboradorAptoComponentes(ColaboradorAptoComponenteFiltroVO filtros)
        {
            var lista = new List<ColaboradorAptoComponenteListaVO>();

            var specColaboradorAptoComponente = filtros.Transform<ColaboradorAptoComponenteFilterSpecification>();

            var colaboradorAptoComponentes = this.SearchProjectionBySpecification(specColaboradorAptoComponente, c => new
            {
                c.Seq,
                c.SeqAtuacaoColaborador,
                c.SeqComponenteCurricular,
                c.ComponenteCurricular.Codigo,
                c.ComponenteCurricular.Descricao,
                c.ComponenteCurricular.Credito,
                c.ComponenteCurricular.CargaHoraria,
                c.ComponenteCurricular.SeqTipoComponenteCurricular,
                c.ComponenteCurricular.NiveisEnsino.Where(w => w.Responsavel == true).FirstOrDefault().SeqNivelEnsino,

            }).ToList();

            foreach (var colaboradorAptoComponente in colaboradorAptoComponentes)
            {
                var formatoCargaHoraria = ConfiguracaoComponenteDomainService.BuscarFormatoCargaHoraria(colaboradorAptoComponente.SeqNivelEnsino, colaboradorAptoComponente.SeqTipoComponenteCurricular);

                lista.Add(new ColaboradorAptoComponenteListaVO()
                {
                    Seq = colaboradorAptoComponente.Seq,
                    SeqAtuacaoColaborador = colaboradorAptoComponente.SeqAtuacaoColaborador,
                    SeqComponenteCurricular = colaboradorAptoComponente.SeqComponenteCurricular,
                    DescricaoComponenteCurricular = ComponenteCurricularDomainService.GerarDescricaoComponenteCurricular(
                        colaboradorAptoComponente.Codigo,
                        colaboradorAptoComponente.Descricao,
                        colaboradorAptoComponente.Credito,
                        colaboradorAptoComponente.CargaHoraria,
                        formatoCargaHoraria)
                });
            }

            return lista;
        }

        public bool ValidarFormacaoAcademica(long seqAtuacaoColaborador) 
        {
            var possuiFormacaoAcademica = ColaboradorDomainService.SearchProjectionByKey(seqAtuacaoColaborador, c => c.FormacoesAcademicas.Any());

            return possuiFormacaoAcademica;
        }

        public long SalvarColaboradorAptoComponente(ColaboradorAptoComponenteVO modelo) 
        {
            var colaboradorAptoComponente = modelo.Transform<ColaboradorAptoComponente>();

            this.SaveEntity(colaboradorAptoComponente);

            return colaboradorAptoComponente.Seq;
        }
    }
}