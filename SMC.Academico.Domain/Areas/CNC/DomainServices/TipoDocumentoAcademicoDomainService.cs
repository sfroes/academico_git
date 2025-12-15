using Microsoft.SqlServer.Server;
using SMC.Academico.Common.Areas.CNC.Enums;
using SMC.Academico.Common.Areas.CNC.Exceptions;
using SMC.Academico.Common.Areas.SRC.Constants;
using SMC.Academico.Domain.Areas.CNC.Models;
using SMC.Academico.Domain.Areas.CNC.Specifications;
using SMC.Academico.Domain.Areas.CNC.ValueObjects;
using SMC.Academico.Domain.Areas.ORG.Models;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Security.Util;
using SMC.Framework.Specification;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Areas.CNC.DomainServices
{
    public class TipoDocumentoAcademicoDomainService : AcademicoContextDomain<TipoDocumentoAcademico>
    {
        #region DomainServices

        private TipoDocumentoAcademicoServicoDomainService TipoDocumentoAcademicoServicoDomainService
        {
            get { return this.Create<TipoDocumentoAcademicoServicoDomainService>(); }
        }

        private InstituicaoNivelTipoDocumentoAcademicoDomainService InstituicaoNivelTipoDocumentoAcademicoDomainService
        {
            get { return Create<InstituicaoNivelTipoDocumentoAcademicoDomainService>(); }
        }

        #endregion

        /// <summary>
        /// Grava um tipo de documento de conclusão
        /// </summary>
        /// <param name="modelo">Tipo de documento de conclusão a ser gravado</param>
        /// <returns>Sequencia do tipo de documento de conclusao gravado</returns>
        public long SalvarTipoDocumentoAcademico(TipoDocumentoAcademicoVO modelo)
        {
            ValidarModelo(modelo);

            var dominio = modelo.Transform<TipoDocumentoAcademico>();

            this.SaveEntity(dominio);

            return dominio.Seq;
        }

        private void ValidarModelo(TipoDocumentoAcademicoVO modelo)
        {
            foreach (var servico in modelo.Servicos)
            {
                var tipoDocumentoPorServico = this.TipoDocumentoAcademicoServicoDomainService.SearchBySpecification(new TipoDocumentoAcademicoServicoFilterSpecification() { SeqInstituicaoEnsino = modelo.SeqInstituicaoEnsino, SeqServico = servico.SeqServico }).ToList();

                if (tipoDocumentoPorServico.Any(a => a.SeqTipoDocumentoAcademico != modelo.Seq))
                    throw new ServicoJaAssociadoEmOutroTipoDocumentoException();
            }
        }

        public List<SMCDatasourceItem> BuscarTiposDocumentoConclusaoSelect(List<GrupoDocumentoAcademico> gruposDocumentoConclusao, long? seqInstituicaoEnsino)
        {
            var spec = new TipoDocumentoAcademicoFilterSpecification()
            {
                GruposDocumentoAcademico = gruposDocumentoConclusao,
                SeqInstituicaoEnsino = seqInstituicaoEnsino
            };
            var lista = this.SearchProjectionBySpecification(spec, x => new SMCDatasourceItem()
            {
                Seq = x.Seq,
                Descricao = x.Descricao
            }).OrderBy(o => o.Descricao).ToList();

            return lista;
        }

        public TipoDocumentoAcademicoVO BuscarTipoDocumentoAcademico(long seq)
        {
            var retorno = this.SearchProjectionByKey(new SMCSeqSpecification<TipoDocumentoAcademico>(seq),
                x => new TipoDocumentoAcademicoVO
                {
                    Seq = x.Seq,
                    Descricao = x.Descricao,
                    GrupoDocumentoAcademico = x.GrupoDocumentoAcademico,
                    SeqInstituicaoEnsino = x.SeqInstituicaoEnsino,
                    Token = x.Token,
                    Tags = x.Tags.Select(t => new TipoDocumentoAcademicoTagVO()
                    {
                        Seq = t.Seq,
                        SeqTag = t.SeqTag,
                        SeqTipoDocumentoAcademico = t.SeqTipoDocumentoAcademico,
                        InformacaoTag = t.InformacaoTag,
                        TipoPreenchimentoTag = t.Tag.TipoPreenchimentoTag,
                        PermiteEditarDado = t.PermiteEditarDado,
                        TipoReadOnly = t.Tag.TipoPreenchimentoTag == Common.Areas.PES.Enums.TipoPreenchimentoTag.Automatico,
                        DescricaoTag = t.Tag.Descricao
                    }).OrderBy(o => o.DescricaoTag).ToList(),
                    Servicos = x.Servicos.Select(s => new TipoDocumentoAcademicoServicoVO()
                    {
                        Seq = s.Seq,
                        SeqServico = s.SeqServico,
                        SeqTipoDocumentoAcademico = s.SeqTipoDocumentoAcademico,
                    }).ToList()
                });

            return retorno;
        }

        /// <summary>
        /// Busca tipos de documento acadêmico por grupo de documento aplicando no retorno da listagem um token de segurança 
        /// </summary>
        /// <param name="grupoDocumentoAcademico"></param>        
        /// <returns>List<SMCDatasourceItem></returns>
        public List<SMCDatasourceItem> BuscarTiposDocumentoAcademicoPorTipoGrupoDocAcadSelect(GrupoDocumentoAcademico grupoDocumentoAcademico)
        {
            var lista = new List<SMCDatasourceItem>();

            var retorno = InstituicaoNivelTipoDocumentoAcademicoDomainService.SearchProjectionBySpecification(
                                    new InstituicaoNivelTipoDocumentoAcademicoFilterSpecification()
                                    { GrupoDocumentoAcademico = grupoDocumentoAcademico },
                                    tipDoc => new
                                    {
                                        tipDoc.TipoDocumentoAcademico.Seq,
                                        tipDoc.TipoDocumentoAcademico.Descricao,
                                        tipDoc.TokenPermissaoEmissaoDocumento,
                                    }).OrderBy(x => x.Descricao).ToList();

            foreach (var item in retorno)
            {
                if (!lista.Any(a => a.Seq == item.Seq) &&
                    ((!string.IsNullOrEmpty(item.TokenPermissaoEmissaoDocumento) &&
                        SMCSecurityHelper.Authorize(item.TokenPermissaoEmissaoDocumento.Trim())) ||
                        SMCSecurityHelper.Authorize(UC_SRC_002_01_01.MANTER_PROCESSO_RESTRITO_ADMINISTRADOR)))
                {
                    lista.Add(new SMCDatasourceItem() { Seq = item.Seq, Descricao = item.Descricao });
                }
            }

            return lista;
        }
    }
}
