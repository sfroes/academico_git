using SMC.Academico.Domain.Areas.ORG.Models;
using SMC.Framework.Model;
using System.Collections.Generic;
using SMC.Academico.Domain.Areas.ORG.Specifications;
using SMC.Academico.Common.Areas.ORG.Enums;
using SMC.AssinaturaDigital.ServiceContract.Areas.DDG.Interfaces;
using SMC.AssinaturaDigital.ServiceContract.Areas.CAD.Interfaces;
using SMC.Framework.Extensions;
using System.Linq;
using SMC.Academico.Common.Areas.ORG.Includes;
using SMC.Framework.Specification;
using SMC.Framework;
using SMC.Academico.Common.Areas.ORG.Exceptions.InstituicaoNivelSistemaOrigem;
using SMC.Academico.Domain.Areas.ORG.ValueObjects;
using SMC.AssinaturaDigital.ServiceContract.Areas.DOC.Interfaces;
using SMC.Framework.Domain;

namespace SMC.Academico.Domain.Areas.ORG.DomainServices
{
    public class InstituicaoNivelSistemaOrigemDomainService : AcademicoContextDomain<InstituicaoNivelSistemaOrigem>
    {
        #region [ DomainService ]

        private InstituicaoNivelDomainService InstituicaoNivelDomainService => Create<InstituicaoNivelDomainService>();

        #endregion [ DomainService ]

        #region [ Service ]

        private IConfiguracaoDocumentoAcademicoService ConfiguracaoDocumentoAcademicoService { get => Create<IConfiguracaoDocumentoAcademicoService>(); }
        private IConfiguracaoDocumentoService ConfiguracaoDocumentoService { get => Create<IConfiguracaoDocumentoService>(); }

        private ISistemaOrigemService SistemaOrigemService { get => Create<ISistemaOrigemService>(); }

        #endregion [ Service ]

        public List<SMCDatasourceItem> BuscarConfiguracaoDiplomaGADSelect()
        {
            var spec = new InstituicaoNivelSistemaOrigemFilterSpecification { UsoSistemaOrigem = UsoSistemaOrigem.ArquivoXML };
            var tokenSistemaOrigemGAD = this.SearchProjectionByKey(spec, p => new { p.TokenSistemaOrigemGAD });
            var retorno = new List<SMCDatasourceItem>();

            if (tokenSistemaOrigemGAD != null)
                retorno = ConfiguracaoDocumentoAcademicoService.BuscarConfiguracaoDocumentoAcademicoSelect(tokenSistemaOrigemGAD.TokenSistemaOrigemGAD);

            return retorno;
        }

        public List<SMCDatasourceItem> BuscarConfiguracaoDiplomaPorNivelEnsinoGADSelect(long seqNivelEnsino, UsoSistemaOrigem? usoSistemaOrigem = null)
        {
            var retorno = new List<SMCDatasourceItem>();
            var spec = new InstituicaoNivelSistemaOrigemFilterSpecification
            {
                SeqInstituicaoNivel = seqNivelEnsino,
                UsoSistemaOrigem = usoSistemaOrigem
            };

            var tokenSistemaOrigemGAD = this.SearchProjectionByKey(spec, p => new { p.TokenSistemaOrigemGAD });

            //NV06 -
            // Se Tipo de uso" for igual a "Arquivo XML", o campo Configuração-GAD deverá listar em ordem alfabética:
            // As configurações-documento - acadêmico da aplicação GAD que estão associadas aos sistemas-origem - GAD
            // Se não
            // "Tipo de uso" for igual a "Arquivo PDF", o campo Configuração-GAD deverá listar em ordem alfabética:
            // As configurações-documento da aplicação GAD que estão associadas aos

            if (tokenSistemaOrigemGAD != null && usoSistemaOrigem == UsoSistemaOrigem.ArquivoXML)
            {
                retorno = ConfiguracaoDocumentoAcademicoService.BuscarConfiguracaoDocumentoAcademicoSelect(tokenSistemaOrigemGAD.TokenSistemaOrigemGAD);
            }
            else if (tokenSistemaOrigemGAD != null && usoSistemaOrigem == UsoSistemaOrigem.ArquivoPDF)
            {
                retorno = ConfiguracaoDocumentoService.BuscarConfiguracaoDocumentoSelect(tokenSistemaOrigemGAD.TokenSistemaOrigemGAD);
            }

            return retorno;
        }

        public SMCPagerData<InstituicaoNivelSistemaOrigemVO> BuscarInstituicaoNivelSistemaOrigemGAD(InstituicaoNivelSistemaOrigemFilterSpecification filtros)
        {
            var lista = this.SearchProjectionBySpecification(filtros, p => new InstituicaoNivelSistemaOrigemVO()
            {
                Seq = p.Seq,
                SeqInstituicaoNivel = p.SeqInstituicaoNivel,
                TokenSistemaOrigemGAD = p.TokenSistemaOrigemGAD,
                UsoSistemaOrigem = p.UsoSistemaOrigem

            }, out int total).ToList();

            // Preencher o nome do Sistema Origem e Nivel de Ensino
            if (total > 0)
            {
                var selectSistemaOrigem = BuscarSistemaOrigemGADSelect();
                var selectInstituicaoNivel = InstituicaoNivelDomainService.SearchAll(i => i.NivelEnsino.Descricao, IncludesInstituicaoNivel.NivelEnsino).ToList();

                foreach (var item in lista)
                {
                    item.DescricaoSistemaOrigemGAD = selectSistemaOrigem.Where(c => c.Seq == item.TokenSistemaOrigemGAD).FirstOrDefault()?.Descricao;
                    item.DescricaoInstituicaoNivel = selectInstituicaoNivel.Where(c => c.Seq == item.SeqInstituicaoNivel).FirstOrDefault()?.NivelEnsino?.Descricao;
                }
            }

            // Faz o retorno ordenando por Nivel de Ensino e depois pela Descrição do sistema de Origem GAD
            return new SMCPagerData<InstituicaoNivelSistemaOrigemVO>(lista.OrderBy(c => c.DescricaoInstituicaoNivel)
                .ThenBy(c => c.DescricaoSistemaOrigemGAD)
                .ThenBy(c => c.UsoSistemaOrigem)
                .ToList(), total);
        }

        public List<SMCDatasourceItem<string>> BuscarSistemaOrigemGADSelect()
        {
            return SistemaOrigemService.BuscaDadosSelectSistemaOrigemPorSigla("SGA");
        }

        public long SalvarInstituicaoNivelSIstemaOrigemGAD(InstituicaoNivelSistemaOrigemVO instituicaoNivelSistemaOrigemVO)
        {
            var spec = new InstituicaoNivelSistemaOrigemFilterSpecification
            {
                SeqInstituicaoNivel = instituicaoNivelSistemaOrigemVO.SeqInstituicaoNivel,
                TokenSistemaOrigemGAD = instituicaoNivelSistemaOrigemVO.TokenSistemaOrigemGAD
            };

            var retorno = this.SearchBySpecification(spec).Where(c => c.Seq != instituicaoNivelSistemaOrigemVO.Seq).ToList();

            if (retorno.SMCAny())
            {
                throw new InstituicaoNivelSistemaOrigemInclusaoException();
            }

            var modelo = instituicaoNivelSistemaOrigemVO.Transform<InstituicaoNivelSistemaOrigem>();
            this.SaveEntity(modelo);

            return modelo.Seq;
        }

        public List<SMCDatasourceItem> BuscarTipoUsoNivelEnsino(long seqInstituicaoNivel)
        {
            var spec = new InstituicaoNivelSistemaOrigemFilterSpecification() { SeqInstituicaoNivel = seqInstituicaoNivel };
            var tipoUso = this.SearchProjectionBySpecification(spec, s => new
            {
                TipoUso = s.UsoSistemaOrigem
            }).ToList();
            var arquivoPdf = tipoUso.Where(f => f.TipoUso == UsoSistemaOrigem.ArquivoPDF).Any();
            var arquivoXML = tipoUso.Where(f => f.TipoUso == UsoSistemaOrigem.ArquivoXML).Any();

            var retorno = new List<SMCDatasourceItem>();

            //Valida se o nivel de ensino possui arquivo PDF ou arquivo XML registrado
            if (arquivoPdf && !arquivoXML)
            {
                retorno.Add(UsoSistemaOrigem.ArquivoPDF.SMCToSelectItem());
            }
            else if (!arquivoPdf && arquivoXML)
            {
                retorno.Add(UsoSistemaOrigem.ArquivoXML.SMCToSelectItem());
            }
            else if (arquivoPdf && arquivoXML)
            {
                retorno.Add(UsoSistemaOrigem.ArquivoPDF.SMCToSelectItem());
                retorno.Add(UsoSistemaOrigem.ArquivoXML.SMCToSelectItem());
            }

            return retorno;
        }

        public string BuscarTokenSistemaOrigemGAD(long seqNivelEnsino, UsoSistemaOrigem usoSistemaOrigem)
        {
            var spec = new InstituicaoNivelSistemaOrigemFilterSpecification
            {
                SeqInstituicaoNivel = seqNivelEnsino,
                UsoSistemaOrigem = usoSistemaOrigem
            };
            var tokenSistemaOrigemGAD = this.SearchProjectionByKey(spec, p => p.TokenSistemaOrigemGAD);

            return tokenSistemaOrigemGAD;
        }
    }
}