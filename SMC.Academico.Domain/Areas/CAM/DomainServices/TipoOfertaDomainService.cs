using SMC.Academico.Common.Areas.CAM.Exceptions;
using SMC.Academico.Domain.Areas.CAM.Models;
using SMC.Academico.Domain.Areas.CAM.Specifications;
using SMC.Academico.Domain.Areas.CAM.Validators;
using SMC.Framework.Domain;
using SMC.Framework.Domain.Exceptions;
using SMC.Framework.Specification;
using SMC.Framework.Validation;
using System.Collections.Generic;
using System.Linq;
using SMC.Framework.Model;
using System;
using SMC.Framework.Extensions;
using SMC.Academico.Domain.Areas.CAM.ValueObjects;

namespace SMC.Academico.Domain.Areas.CAM.DomainServices
{
    public class TipoOfertaDomainService : AcademicoContextDomain<TipoOferta>
    {
        #region [ Serviços ]

        private TipoOfertaUnidadeResponsavelDomainService TipoOfertaUnidadeResponsavelDomainService => Create<TipoOfertaUnidadeResponsavelDomainService>();

        private CampanhaDomainService CampanhaDomainService => Create<CampanhaDomainService>();

        private CampanhaOfertaDomainService CampanhaOfertaDomainService
        {
            get { return Create<CampanhaOfertaDomainService>(); }
        }

        private ProcessoSeletivoDomainService ProcessoSeletivoDomainService => Create<ProcessoSeletivoDomainService>();

        private ProcessoSeletivoOfertaDomainService ProcessoSeletivoOfertaDomainService
        {
            get { return Create<ProcessoSeletivoOfertaDomainService>(); }
        }

        #endregion [ Serviços ]

        public long SalvarTipoOferta(TipoOferta tipoOferta)
        {
            Validar(tipoOferta);
            SaveEntity(tipoOferta);

            return tipoOferta.Seq;
        }

        public List<SMCDatasourceItem> BuscarTiposOfertaSelect()
        {
            return SearchAll().OrderBy(a => a.Descricao).TransformList<SMCDatasourceItem>();
        }

        /// <summary>
        /// Mesmo método BuscarTiposOfertaSelect(), porém, com DataAttributes com tipo formacao especifica (true|false)
        /// </summary>
        /// <param name="seqCampanha"></param>
        /// <returns>Lista de todos os tipos de ofertas</returns>
        public List<SMCDatasourceItem> BuscarTiposOfertaDataAttribute()
        {
            return SearchProjectionAll(x => new SMCDatasourceItem
            {
                Seq = x.Seq,
                Descricao = x.Descricao,
                DataAttributes = new List<SMCKeyValuePair>()
                                {
                                    new SMCKeyValuePair(){ Key = "formacaoespecifica", Value = x.SeqTipoFormacaoEspecifica.HasValue ? "true" : "false" }
                                }
            }, orderBy: o => o.Descricao
             ).ToList();
        }

        public List<SMCDatasourceItem> BuscarTiposOfertaDaCampanhaSelect(long seqCampanha)
        {
            var seqEntidadeResponsavel = CampanhaDomainService.SearchProjectionByKey(new SMCSeqSpecification<Campanha>(seqCampanha), x => x.SeqEntidadeResponsavel);
            return TipoOfertaUnidadeResponsavelDomainService.SearchProjectionBySpecification(new TipoOfertaUnidadeResponsavelSpecification() { SeqEntidadeResponsavel = seqEntidadeResponsavel },
                                x => new SMCDatasourceItem
                                {
                                    Seq = x.SeqTipoOferta,
                                    Descricao = x.TipoOferta.Descricao,
                                    DataAttributes = new List<SMCKeyValuePair>()
                                    {
                                        new SMCKeyValuePair(){ Key = "formacaoespecifica", Value = x.TipoOferta.SeqTipoFormacaoEspecifica.HasValue ? "true" : "false" }
                                    }
                                }).ToList();
        }

        public List<SMCDatasourceItem> BuscarTiposOfertaPorProcessoSeletivoSelect(long seqProcessoSeletivo)
        {
            var seqTipoProcessoSeletivo = ProcessoSeletivoDomainService.SearchProjectionByKey(new SMCSeqSpecification<ProcessoSeletivo>(seqProcessoSeletivo), x => x.SeqTipoProcessoSeletivo);

            var spec = new TipoOfertaFilterSpecification() { SeqTipoProcessoSeletivo = seqTipoProcessoSeletivo };
            return SearchProjectionBySpecification(spec, x => new SMCDatasourceItem
            {
                Seq = x.Seq,
                Descricao = x.Descricao
            }).OrderBy(o => o.Descricao).ToList();
        }

        public TipoOfertaSelecaoOfertaVO BuscarTipoOfertaSelecaoOfertaCampanha(long seqTipoOferta, long seqCampanha)
        {
            var campanha = this.CampanhaDomainService.SearchProjectionByKey(new SMCSeqSpecification<Campanha>(seqCampanha), c => new
            {
                CiclosLetivos = c.CiclosLetivos.Select(cl=>cl.CicloLetivo.Descricao).ToList()
            });

            var tipoOferta = this.SearchProjectionByKey(new SMCSeqSpecification<TipoOferta>(seqTipoOferta), t => new TipoOfertaSelecaoOfertaVO()
            {
                Seq = t.Seq,
                 ExigeCursoOfertaLocalidadeTurno =t.ExigeCursoOfertaLocalidadeTurno,
                 Token = t.Token,
            });

            tipoOferta.DescricaoCicloLetivo = string.Join(" | ", campanha.CiclosLetivos);

            return tipoOferta;
        }

        private void Validar(TipoOferta tipoOferta)
        {
            var validator = new TipoOfertaValidator();
            var results = validator.Validate(tipoOferta);
            if (!results.IsValid)
            {
                var errorList = new List<SMCValidationResults>();
                errorList.Add(results);
                throw new SMCInvalidEntityException(errorList);
            }
            RegrasValidas(tipoOferta);
        }

        private void RegrasValidas(TipoOferta tipoOferta)
        {
            if (tipoOferta.Seq == default(long))
            {
                return;
            }
            else
            {
                TipoOferta tipoOfertaOld = SearchByKey(new SMCSeqSpecification<TipoOferta>(tipoOferta.Seq));
                CampanhaOfertaFilterSpecification spec = new CampanhaOfertaFilterSpecification();
                spec.SeqTipoOferta = tipoOferta.Seq;
                var campanhas = CampanhaOfertaDomainService.SearchBySpecification(spec).ToList();
                if (campanhas != null && campanhas.Count > 0)
                {
                    /*
                    Ao alterar o indicador exige curso oferta localidade turno, verificar se existe oferta, do tipo em questão,
                    associada a alguma campanha. Caso exista, abortar a operação e emitir a mensagem de erro:
                    "Este tipo de oferta já está sendo utilizado por uma oferta de uma campanha. Não é possível alterar o indicador "Exige curso oferta localidade turno"".
                    */
                    if (tipoOfertaOld.ExigeCursoOfertaLocalidadeTurno != tipoOferta.ExigeCursoOfertaLocalidadeTurno)
                    {
                        throw new TipoOfertaExigeCursoOfertaLocalidadeTurnoException();
                    }

                    /*
                    Ao alterar o tipo de formação específica, verificar se existe oferta, do tipo em questão, associada a  alguma campanha.
                    Caso exista, abortar a operação e emitir a mensagem de erro:
                    "Este tipo de oferta já está sendo utilizado por uma oferta de uma campanha. Não é possível alterar o tipo de formação específica".
                    */
                    if (tipoOfertaOld.SeqTipoFormacaoEspecifica != tipoOferta.SeqTipoFormacaoEspecifica)
                    {
                        throw new TipoOfertaTipoFormacaoEspecificaException();
                    }
                }

                /*
                Ao alterar o tipo de item de hierarquia de oferta do GPI, verificar se existe oferta, do tipo em questão, associada a algum processo seletivo,
                que está vinculado a algum processo do GPI. Caso exista, abortar a operação e emitir a mensagem de erro:
                "Este tipo de oferta já está sendo utilizado por um processo do GPI. Não é possível alterar o tipo de item de hierarquia de oferta"
                */
                if (tipoOfertaOld.SeqTipoItemHierarquiaOfertaGpi != tipoOferta.SeqTipoItemHierarquiaOfertaGpi)
                {
                    ProcessoSeletivoOfertaFilterSpecification specPS = new ProcessoSeletivoOfertaFilterSpecification();
                    specPS.SeqTipoOferta = tipoOferta.Seq;
                    var processos = ProcessoSeletivoOfertaDomainService.SearchBySpecification(specPS).ToList();
                    if (processos != null && processos.Count > 0)
                    {
                        throw new TipoOfertaTipoItemHierarquiaOfertaGpiException();
                    }
                }
            }
        }
    }
}