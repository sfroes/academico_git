using SMC.Academico.Common.Areas.DCT.Constants;
using SMC.Academico.Domain.Areas.DCT.Models;
using SMC.Academico.Domain.Areas.DCT.Specifications;
using SMC.Academico.Domain.Areas.ORG.DomainServices;
using SMC.Academico.Domain.Areas.ORG.Models;
using SMC.Framework.Model;
using SMC.Framework.Security.Util;
using SMC.Framework.Specification;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Areas.DCT.DomainServices
{
    public class InstituicaoTipoEntidadeVinculoColaboradorDomainService : AcademicoContextDomain<InstituicaoTipoEntidadeVinculoColaborador>
    {
        #region [ Services ]

        private EntidadeDomainService EntidadeDomainService
        {
            get { return this.Create<EntidadeDomainService>(); }
        }

        private ColaboradorVinculoDomainService ColaboradorVinculoDomainService
        {
            get { return this.Create<ColaboradorVinculoDomainService>(); }
        }

        #endregion [ Services ]

        /// <summary>
        /// Busca os tipos de vinculos de colaborador na instituição para o tipo de uma entidade
        /// </summary>
        /// <param name="seqEntidadeVinculo">Sequencial da entidade vinculada</param>
        /// <param name="seqColaboradorVinculo">Sequencial do colaborador vinculo</param>
        /// <returns>Dados dos tipos de vinculo na instituição para o tipo da entidade informada</returns>
        public List<SMCDatasourceItem> BuscarTiposVinculoColaboradorPorEntidadeSelect(long seqEntidadeVinculo, long? seqColaboradorVinculo)
        {
            // Recupera o sequencial do tipo da entidade vinculada
            var specEntidade = new SMCSeqSpecification<Entidade>(seqEntidadeVinculo);
            long seqTipoEntidade = this.EntidadeDomainService.SearchProjectionByKey(specEntidade, p => p.SeqTipoEntidade);

            // Recupera os tipos de vinculo pelo tipo da entidade
            var specTipoVinculo = new InstituicaoTipoEntidadeVinculoColaboradorFilterSpecification() { SeqTipoEntidade = seqTipoEntidade };

            /*•	UC_DCT_001_06_04 - Manter Vínculo do Colaborador - NV02
            Na inclusão:
            Se o usuário logado NÃO tiver permissão no token Permitir Selecionar Tipo Vinculo, listar os tipos de vinculo
            parametrizados para a instituição logada e o tipo da entidade selecionada e que esteja configurado para
            não integrar o corpo docente(ind_integra_corpo_docente = 0).
            Caso o usuário possua permissão no toke Permitir Selecionar Tipo Vinculo,listar os tipos de vinculo parametrizados
            para a instituição logada e o tipo da entidade selecionada, que são permitdos pelo token e que esteja configurado
            para permitir a inclusão manual do vinculo. (ind_permite_inclusao_manual _ vinculo = 1)*/
            if (seqColaboradorVinculo.GetValueOrDefault() == 0)
            {
                if (SMCSecurityHelper.Authorize(UC_DCT_001_06_02.PERMITIR_SELECIONAR_TIPO_VINCULO))
                {
                    specTipoVinculo.PermiteInclusaoManualVinculo = true;
                }
                else
                {
                    specTipoVinculo.IntegraCorpoDocente = false;
                }
            }
            else
            {
                /*Na alteração:
                 - Se o vínculo foi inserido por carga:
                    Listar os tipos de vínculo parametrizados para a instituição logada e o tipo de entidade da entidade selecionada, que
                    estejam configurados como "Integrante do corpo docente da instituição".
                 - Caso contrário:
                    Se o usuário logado NÃO tiver permissão no token Permitir Selecionar Tipo Vinculo, listar os tipos de vinculo
                    parametrizados para a instituição logada e o tipo da entidade selecionada e que esteja configurado para
                    não integrar o corpo docente(ind_integra_corpo_docente = 0).
                    Caso o usuário possua permissão no token Permitir Selecionar Tipo Vinculo,listar os tipos de vinculo parametrizados
                    para a instituição logada e o tipo da entidade selecionada, que são permitdos pelo token e que esteja configurado
                    para permitir a inclusão manual do vinculo. (ind_permite_inclusao_manual _ vinculo = 1)*/
                var specColaboradorVinculo = new SMCSeqSpecification<ColaboradorVinculo>(seqColaboradorVinculo.Value);
                var inseridoPorCarga = this.ColaboradorVinculoDomainService.SearchProjectionByKey(specColaboradorVinculo, p => p.InseridoPorCarga);

                // E que estejam configurados como IntegraCorpoDocente para vinculos da carga inicial
                if (inseridoPorCarga)
                {
                    specTipoVinculo.IntegraCorpoDocente = true;
                }
                else
                {
                    if (SMCSecurityHelper.Authorize(UC_DCT_001_06_02.PERMITIR_SELECIONAR_TIPO_VINCULO))
                    {
                        specTipoVinculo.PermiteInclusaoManualVinculo = true;
                    }
                    else
                    {
                        specTipoVinculo.IntegraCorpoDocente = false;
                    }
                }
            }

            specTipoVinculo.SetOrderBy(p => p.TipoVinculoColaborador.Descricao);
            return this.SearchProjectionBySpecification(specTipoVinculo,
                p => new SMCDatasourceItem()
                {
                    Seq = p.SeqTipoVinculoColaborador,
                    Descricao = p.TipoVinculoColaborador.Descricao,
                    DataAttributes = new List<SMCKeyValuePair>()
                    {
                        new SMCKeyValuePair() { Key = "data-supervisor", Value = p.TipoVinculoColaborador.RequerAcompanhamentoSupervisor ? "true" : "false" },
                        new SMCKeyValuePair() { Key = "data-formacao", Value = p.TipoVinculoColaborador.ExigeFormacaoAcademica ? "true" : "false" }
                    }
                })
                .ToList();
        }
    }
}