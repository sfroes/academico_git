using SMC.Academico.Common.Areas.ORT.Enums;
using SMC.Academico.Domain.Areas.ORT.DomainServices;
using SMC.Academico.Domain.Areas.ORT.Models;
using SMC.Academico.Domain.Areas.ORT.Specifications;
using SMC.Academico.Domain.Areas.ORT.ValueObjects;
using SMC.Academico.ServiceContract.Areas.ORT.Data;
using SMC.Academico.ServiceContract.Areas.ORT.Interfaces;
using SMC.Framework;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Service;
using SMC.Framework.Specification;
using SMC.Framework.Util;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Service.Areas.ORT.Services
{
    public class PublicacaoBdpService : SMCServiceBase, IPublicacaoBdpService
    {
        #region DomainServices

        private PublicacaoBdpDomainService PublicacaoBdpDomainService { get => Create<PublicacaoBdpDomainService>(); }

        private PublicacaoBdpAutorizacaoDomainService PublicacaoBdpAutorizacaoDomainService => this.Create<PublicacaoBdpAutorizacaoDomainService>();

        #endregion DomainServices

        //public void AlterarSituacao(long seqPublicacaoBdp, SituacaoTrabalhoAcademico situacao)
        //{
        //    PublicacaoBdpDomainService.AlterarSituacao(seqPublicacaoBdp, situacao);
        //}

        public void RetornarSituacaoAlunoBdp(long seqPublicacaoBdp)
        {
            PublicacaoBdpDomainService.RetornarSituacaoAlunoBdp(seqPublicacaoBdp);
        }

        public void LiberarConferenciaBiblioteca(long seqPublicacaoBdp)
        {
            PublicacaoBdpDomainService.LiberarConferenciaBiblioteca(seqPublicacaoBdp);
        }

        public bool ValidarLiberacaoConferenciaBiblioteca(long seqPublicacaoBdp)
        {
            return PublicacaoBdpDomainService.ValidarLiberacaoConferenciaBiblioteca(seqPublicacaoBdp);
        }

        public void LiberarConsulta(long seqPublicacaoBdp)
        {
            PublicacaoBdpDomainService.ValidarLiberacaoConsulta(seqPublicacaoBdp);
        }

        /// <summary>
        /// Buscar publicações bdps do aluno
        /// </summary>
        /// <param name="seqAluno">Sequencial do Aluno</param>
        /// <returns>Lista das publicações bdps do aluno</returns>
        public List<PublicacaoBdpData> BuscarPublicacoesBdpsAluno(long seqAluno)
        {
            return PublicacaoBdpDomainService.BuscarPublicacoesBdpsAluno(seqAluno).TransformList<PublicacaoBdpData>();
        }

        /// <summary>
        /// Buscar publicação Bdp
        /// </summary>
        /// <param name="seq">Sequencial do publicação bdp</param>
        /// <returns>Publicação Bdp/returns>
        public PublicacaoBdpData BuscarPubicacaoBdp(long seq)
        {
            return this.PublicacaoBdpDomainService.BuscarPublicacaoBdp(seq).Transform<PublicacaoBdpData>();
        }

        public FichaCatalograficaData BuscarDadosImpressaoFichaCatalografica(long seqPublicacaoBdp)
        {

            var dadosFichaCatalografica = this.PublicacaoBdpDomainService.BuscarDadosImpressaoFichaCatalografica(seqPublicacaoBdp).Transform<FichaCatalograficaData>();

            return dadosFichaCatalografica;
        }

        /// <summary>
        /// Editar publicacao bdp
        /// </summary>
        /// <param name="model">Dados a serem salvos</param>
        /// <returns>Sequencial da publicação BDP</returns>
        public long SalvarPublicacaoBdp(PublicacaoBdpData model)
        {
            return this.PublicacaoBdpDomainService.SalvarPublicacaoBdp(model.Transform<PublicacaoBdpVO>());
        }

        /// <summary>
        /// Autorizar a publicacao do bdp
        /// </summary>
        /// <param name="model">Dados a serem salvos</param>
        /// <returns></returns>
        public void AutorizarPublicacaoBdp(PublicacaoBdpData model)
        {
            this.PublicacaoBdpDomainService.AutorizarPublicacaoBdp(model.Transform<PublicacaoBdpVO>());
        }

        /// <summary>
        /// Dados para exibir autorização da publicação bdp
        /// </summary>
        /// <param name="seq">Sequencial da publicação bdp</param>
        /// <returns>Dados do aluno</returns>
        public PublicacaoBdpAutorizacaoData DadosAutorizacaoPublicacaoBdp(long seq)
        {
            return this.PublicacaoBdpDomainService.DadosAutorizacaoPublicacaoBdp(seq).Transform<PublicacaoBdpAutorizacaoData>();
        }

        public List<SMCDatasourceItem> BuscarTiposAutorizacao(long seqAluno, short? numeroDiasAutorizacaoParcial)
        {
            var listaAutorizacoesPrograma = PublicacaoBdpDomainService.BuscarTiposAutorizacaoPrograma(seqAluno);
            var possuiAutorizacaoParcial = listaAutorizacoesPrograma.Contains(TipoAutorizacao.Parcial);

            var retorno = new List<SMCDatasourceItem>();

            foreach (var autorizacao in listaAutorizacoesPrograma)
                retorno.Add(new SMCDatasourceItem() { Seq = (long)autorizacao, Descricao = SMCEnumHelper.GetDescription(autorizacao) });

            if ((!possuiAutorizacaoParcial) && numeroDiasAutorizacaoParcial > 0)
            {
                retorno.Add(new SMCDatasourceItem() { Seq = (long)TipoAutorizacao.Parcial, Descricao = SMCEnumHelper.GetDescription(TipoAutorizacao.Parcial) });
            }

            return retorno;
        }

        public void NotificarBibliotecaTrabalhoComMudanca(MudancaTipoTrabalhoAcademicoSATData filtro)
        {
            this.PublicacaoBdpDomainService.NotificarBibliotecaTrabalhoComMudanca(filtro.Transform<MudancaTipoTrabalhoAcademicoSATVO>());
        }
    }
}