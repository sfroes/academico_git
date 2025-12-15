using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Academico.Domain.Areas.PES.DomainServices;
using SMC.Academico.Domain.Areas.PES.Models;
using SMC.Academico.Domain.Areas.PES.Specifications;
using SMC.Academico.Domain.Areas.PES.ValueObjects;
using SMC.Academico.ServiceContract.Areas.PES.Data;
using SMC.Academico.ServiceContract.Areas.PES.Interfaces;
using SMC.Framework;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Service;
using SMC.Framework.Specification;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Service.Areas.PES.Services
{
    public class TipoMensagemService : SMCServiceBase, ITipoMensagemService
    {
        #region [ DomainServices ]

        private TipoMensagemDomainService TipoMensagemDomainService
        {
            get { return Create<TipoMensagemDomainService>(); }
        }

        private TagDomainService TagDomainService
        {
            get { return Create<TagDomainService>(); }
        }

        #endregion [ DomainServices ]

        public List<SMCDatasourceItem> BuscarTiposMensagemSelect()
        {
            return TipoMensagemDomainService.SearchAll().OrderBy(a => a.Descricao).TransformList<SMCDatasourceItem>();
        }

        public SMCPagerData<TipoMensagemListaData> ListarTipoMensagem(TipoMensagemFiltroData filtro)
        {
            var filtroVO = filtro.Transform<TipoMensagemFiltroVO>();

            var list = TipoMensagemDomainService.ListarTipoMensagem(filtroVO);

            var result = list.TransformList<TipoMensagemListaData>();

            return new SMCPagerData<TipoMensagemListaData>(result, list.Total);
        }

        public List<SMCDatasourceItem> BuscarTagsSelect(TipoTag tipoTag)
        {
            TagFilterSpecification spec = new TagFilterSpecification() { TipoTag = tipoTag };

            spec.SetOrderBy( x => x.Descricao);

            var lista = TagDomainService.SearchBySpecification(spec).TransformList<SMCDatasourceItem>();

            //Se isto não for feito, vai dar pau no arquivo http://localhost/Recursos/4.0/Scripts/libs/jsrender.min.js
            //e os itens do mestre detalhe não vão aparecer.
            foreach (var item in lista)
            {
                item.Descricao = item.Descricao.Replace("{", string.Empty);
                item.Descricao = item.Descricao.Replace("}", string.Empty);
            }

            return lista;
        }

        public long SalvarTipoMensagem(TipoMensagemData tipoMensagem)
        {
            var dominio = tipoMensagem.Transform<TipoMensagem>();
            return TipoMensagemDomainService.SalvarTipoMensagem(dominio);
        }

        public bool PermiteCadastroManual(long seqTipoMensagem)
        {
            return TipoMensagemDomainService.SearchByKey(new SMCSeqSpecification<TipoMensagem>(seqTipoMensagem)).PermiteCadastroManual;
        }

        public List<string> BuscarTiposAtuacao(long seqTipoMensagem)
        {
            var tipos = TipoMensagemDomainService.SearchByKey(new SMCSeqSpecification<TipoMensagem>(seqTipoMensagem), a => a.TiposAtuacao).TiposAtuacao;
            List<string> listaTipos = new List<string>();
            foreach (var item in tipos)
            {
                listaTipos.Add(item.TipoAtuacao.SMCGetDescription());
            }
            return listaTipos;
        }

        public List<string> BuscarTiposUso(long seqTipoMensagem)
        {
            List<string> tags = new List<string>();
            var listaTags = TipoMensagemDomainService.SearchByKey(new SMCSeqSpecification<TipoMensagem>(seqTipoMensagem), a => a.TiposUso).TiposUso;
            foreach (var tag in listaTags)
            {
                tags.Add(tag.TipoUsoMensagem.SMCGetDescription());
            }
            return tags;
        }

        public List<string> BuscarTags(long seqTipoMensagem)
        {
            List<string> tags = new List<string>();
            var listaTags = TipoMensagemDomainService.SearchByKey(new SMCSeqSpecification<TipoMensagem>(seqTipoMensagem), a => a.Tags[0].Tag).Tags;
            foreach (var tag in listaTags)
            {
                tags.Add(tag.Tag.Descricao);
            }
            return tags;
        }

        /// <summary>
        /// Buscar tipo de mensagem
        /// </summary>
        /// <param name="seq">Seuencial do tipo de mensagem</param>
        /// <returns></returns>
        public TipoMensagemData BuscarTipoMensagem(long seq)
        {
            return TipoMensagemDomainService.BuscarTipoMensagem(seq).Transform<TipoMensagemData>();
        }
    }
}
