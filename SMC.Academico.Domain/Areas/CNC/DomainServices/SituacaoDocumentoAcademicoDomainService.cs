using iTextSharp.text;
using SMC.Academico.Common.Areas.CNC.Enums;
using SMC.Academico.Common.Areas.CNC.Includes;
using SMC.Academico.Domain.Areas.CNC.Models;
using SMC.Academico.Domain.Areas.CNC.Specifications;
using SMC.Academico.Domain.Areas.CNC.ValueObjects;
using SMC.Academico.Domain.Areas.CSO.DomainServices;
using SMC.Framework;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Security.Model;
using SMC.Framework.Specification;
using SMC.Framework.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Areas.CNC.DomainServices
{
    public class SituacaoDocumentoAcademicoDomainService : AcademicoContextDomain<SituacaoDocumentoAcademico>
    {
        #region DomainServices

        private DocumentoAcademicoHistoricoSituacaoDomainService DocumentoAcademicoHistoricoSituacaoDomainService
        {
            get { return this.Create<DocumentoAcademicoHistoricoSituacaoDomainService>(); }
        }

        private SituacaoDocumentoAcademicoGrupoDoctoDomainService SituacaoDocumentoAcademicoGrupoDoctoDomainService
        {
            get { return this.Create<SituacaoDocumentoAcademicoGrupoDoctoDomainService>(); }
        }

        #endregion DomainServices

        public SituacaoDocumentoAcademicoVO BuscarSituacaoDocumentoAcademicoPorSituacaoAtual(long seqDocumentoAcademicoHistoricoSituacaoAtual)
        {
            var situacaoDocumento = this.DocumentoAcademicoHistoricoSituacaoDomainService.SearchProjectionByKey(new SMCSeqSpecification<DocumentoAcademicoHistoricoSituacao>(seqDocumentoAcademicoHistoricoSituacaoAtual), x => new SituacaoDocumentoAcademicoVO()
            {
                Seq = x.SituacaoDocumentoAcademico.Seq,
                Descricao = x.SituacaoDocumentoAcademico.Descricao,
                Token = x.SituacaoDocumentoAcademico.Token,
                ClasseSituacaoDocumento = x.SituacaoDocumentoAcademico.ClasseSituacaoDocumento,
                Ordem = x.SituacaoDocumentoAcademico.Ordem
            });

            return situacaoDocumento;
        }

        public SMCPagerData<SituacaoDocumentoAcademicoListaVO> BuscarSituacaoDocumentoAcademicoFiltro(SituacaoDocumentoAcademicoFilterSpecification filtro) 
        {
            int total = 0;
            var lista = this.SearchProjectionBySpecification(filtro, d => new SituacaoDocumentoAcademicoListaVO()
            {
                Seq = d.Seq,
                Descricao = d.Descricao,
                GruposDocumentoAcademico = d.GruposDocumento.Select(x => x.GrupoDocumentoAcademico).ToList()
            }, out total).ToList();
            

            return new SMCPagerData<SituacaoDocumentoAcademicoListaVO>(lista, total);
        }

        public long Salvar(SituacaoDocumentoAcademicoVO modelo)
        {
            using (var unityOfWork = SMCUnitOfWork.Begin())
            {
                try
                {
                    if (modelo.Seq == 0)
                    {
                        var novaSituacao = new SituacaoDocumentoAcademico
                        {
                            UsuarioInclusao = SMCContext.User.Identity.Name,
                            DataInclusao = DateTime.Now,
                            Descricao = modelo.Descricao,
                            ClasseSituacaoDocumento = modelo.ClasseSituacaoDocumento,
                            Ordem = modelo.Ordem,
                            Token = modelo.Token
                        };

                        this.SaveEntity(novaSituacao);

                        if (modelo.GruposDocumento.Count() > 0)
                        {
                            novaSituacao.GruposDocumento = new List<SituacaoDocumentoAcademicoGrupoDocto>();
                            foreach (var item in modelo.GruposDocumento)
                            {
                                novaSituacao.GruposDocumento.Add(new SituacaoDocumentoAcademicoGrupoDocto
                                {
                                    UsuarioInclusao = SMCContext.User.Identity.Name,
                                    DataInclusao = DateTime.Now,
                                    SeqSituacaoDocumentoAcademico = novaSituacao.Seq,
                                    GrupoDocumentoAcademico = item.GrupoDocumentoAcademico
                                });

                            }

                            SituacaoDocumentoAcademicoGrupoDoctoDomainService.SaveEntity(novaSituacao.GruposDocumento);
                            this.UpdateEntity(novaSituacao, x => x.GruposDocumento);
                        }

                        unityOfWork.Commit();

                        return novaSituacao.Seq;
                    }
                    else
                    {
                        var situacaoDocumento = this.SearchByKey(new SMCSeqSpecification<SituacaoDocumentoAcademico>(modelo.Seq), IncludesSituacaoDocumentoAcademico.GruposDocumento);
                        if (situacaoDocumento != null)
                        {
                            situacaoDocumento.UsuarioAlteracao = SMCContext.User.Identity.Name;
                            situacaoDocumento.DataAlteracao = DateTime.Now;
                            situacaoDocumento.Descricao = modelo.Descricao;
                            situacaoDocumento.ClasseSituacaoDocumento = modelo.ClasseSituacaoDocumento;
                            situacaoDocumento.Ordem = modelo.Ordem;
                            situacaoDocumento.Token = modelo.Token;

                            situacaoDocumento.GruposDocumento = modelo.GruposDocumento.TransformList<SituacaoDocumentoAcademicoGrupoDocto>();

                            foreach (var item in situacaoDocumento.GruposDocumento)
                            {
                                if (item.Seq == 0)
                                {
                                    item.UsuarioInclusao = SMCContext.User.Identity.Name;
                                    item.DataInclusao = DateTime.Now;
                                    item.SeqSituacaoDocumentoAcademico = situacaoDocumento.Seq;
                                }
                                else
                                {
                                    item.DataAlteracao = DateTime.Now;
                                    item.UsuarioAlteracao = SMCContext.User.Identity.Name;
                                }
                            }
                        }


                        this.UpdateEntity(situacaoDocumento, x => x.GruposDocumento);
                        this.SaveEntity(situacaoDocumento);

                        unityOfWork.Commit();


                        return situacaoDocumento.Seq;
                    }
                }
                catch (Exception)
                {
                    unityOfWork.Rollback();
                    throw;
                }
            }
        }

        public List<SMCDatasourceItem> BuscarSituacoesDocumentoConclusaoPorGrupoSelect(List<GrupoDocumentoAcademico> listaGrupoDocumentoAcademico)
        {
            var spec = new SituacaoDocumentoAcademicoFilterSpecification() { ListaGrupoDocumentoAcademico = listaGrupoDocumentoAcademico };
            spec.SetOrderBy(s => s.Ordem);

            var lista = this.SearchProjectionBySpecification(spec, x => new SMCDatasourceItem()
            {
                Seq = x.Seq,
                Descricao = x.Descricao
            }).ToList();

            return lista;
        }

    }
}
