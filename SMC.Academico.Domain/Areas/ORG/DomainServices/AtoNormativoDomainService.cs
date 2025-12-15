using SMC.Academico.Common.Areas.ORG.Includes;
using SMC.Academico.Common.Areas.SRC.Exceptions;
using SMC.Academico.Common.Constants;
using SMC.Academico.Domain.Areas.CSO.DomainServices;
using SMC.Academico.Domain.Areas.CSO.Specifications;
using SMC.Academico.Domain.Areas.ORG.Models;
using SMC.Academico.Domain.Areas.ORG.Specifications;
using SMC.Academico.Domain.Areas.ORG.Specifications.AtoNormativo;
using SMC.Academico.Domain.Areas.ORG.ValueObjects;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Specification;
using System.Collections.Generic;
using System.Linq;


namespace SMC.Academico.Domain.Areas.ORG.DomainServices
{
    public class AtoNormativoDomainService : AcademicoContextDomain<AtoNormativo>
    {
        #region [ DomainService ]

        private CursoOfertaLocalidadeDomainService CursoOfertaLocalidadeDomainService => Create<CursoOfertaLocalidadeDomainService>();

        private InstituicaoNivelDomainService InstituicaoNivelDomainService => Create<InstituicaoNivelDomainService>();

        #endregion [ DomainService ]

        public SMCPagerData<AtoNormativoListarVO> BuscarAtosNormativos(AtoNormativoFiltroVO filtros)
        {
            var specAtoNormativo = filtros.Transform<AtoNormativoFilterSpecification>();

            if (filtros.TipoOrgaoRegulador.HasValue || filtros.CodigoOrgaoRegulador.HasValue || filtros.CodigoCursoOferta.HasValue)
            {
                var specCursoOfertaLocalidade = new CursoOfertaLocalidadeFilterSpecification() { AssociadaAtoNormativo = true };

                if (filtros.TipoOrgaoRegulador.HasValue)
                {
                    var specInstituicaoNivel = new InstituicaoNivelFilterSpecification { TipoOrgaoRegulador = filtros.TipoOrgaoRegulador };
                    var seqsNiveisEnsino = InstituicaoNivelDomainService.SearchBySpecification(specInstituicaoNivel).Select(s => s.SeqNivelEnsino).ToList();
                    specCursoOfertaLocalidade.SeqsNiveisEnsino = seqsNiveisEnsino;
                }

                if (filtros.CodigoCursoOferta.HasValue)
                {
                    specCursoOfertaLocalidade.Codigo = filtros.CodigoCursoOferta;
                }

                if (filtros.CodigoOrgaoRegulador.HasValue)
                {
                    specCursoOfertaLocalidade.CodigoOrgaoRegulador = filtros.CodigoOrgaoRegulador;
                }

                var seqsCursoOfertaLocalidade = this.CursoOfertaLocalidadeDomainService.SearchProjectionBySpecification(specCursoOfertaLocalidade, x => x.Seq).ToList();

                if (!specAtoNormativo.SeqEntidade.HasValue)
                {
                    //Adiciona os sequenciais de entidade encontrados se não tiver filtrado por um sequencial de entidade específico
                    specAtoNormativo.SeqsEntidades = seqsCursoOfertaLocalidade;
                }
                else
                {
                    if (!seqsCursoOfertaLocalidade.Contains(specAtoNormativo.SeqEntidade.Value))
                    {
                        //Se tiver filtrado por um sequencial de entidade específico e essa entidade não 
                        //for referente aos filtros de orgão pesquisados, não retorna nada
                        var listaVazia = new List<AtoNormativoListarVO>();
                        return new SMCPagerData<AtoNormativoListarVO>(listaVazia, 0);
                    }
                }

                //Se não retornou nenhum sequencial de entidade, quer dizer que não existe nenhum curso oferta localidade
                //associado aos atos normativos que corresponda ao filtro selecionado, então não deve retornar nenhum registro
                if (!seqsCursoOfertaLocalidade.Any())
                {
                    var listaVazia = new List<AtoNormativoListarVO>();
                    return new SMCPagerData<AtoNormativoListarVO>(listaVazia, 0);
                }
            }

            var lista = SearchProjectionBySpecification(specAtoNormativo, x => new AtoNormativoListarVO()
            {
                Seq = x.Seq,
                SeqAssuntoNormativo = x.SeqAssuntoNormativo,
                DescricaoAssuntoNormativo = x.AssuntoNormativo.Descricao,
                SeqTipoAtoNormativo = x.SeqTipoAtoNormativo,
                DescricaoTipoAtoNormativo = x.TipoAtoNormativo.Descricao,
                NumeroDocumento = x.NumeroDocumento,
                DataDocumento = x.DataDocumento,
                Descricao = x.Descricao
            }, out int total).ToList();

            return new SMCPagerData<AtoNormativoListarVO>(lista, total);
        }

        public List<AtoNormativoVisualizarVO> BuscarAtoNormativoPorEntidade(long? seqEntidade = null, long? SeqInstituicaoEnsino = null)
        {
            var retorno = this.SearchProjectionBySpecification(new AtoNormativoFilterSpecification()
            { SeqAtoNormativoEntidade = seqEntidade, SeqInstituicaoEnsino = SeqInstituicaoEnsino },
                x => new AtoNormativoVisualizarVO()
                {
                    Seq = x.Seq,
                    DescricaoAssuntoNormativo = x.AssuntoNormativo.Descricao,
                    DescricaoTipoAtoNormativo = x.TipoAtoNormativo.Descricao,
                    NumeroDocumento = x.NumeroDocumento,
                    DataDocumento = x.DataDocumento,
                    GrauAcademico = x.Entidades.Where(w => w.Entidade.TipoEntidade.Token == TOKEN_TIPO_ENTIDADE_EXTERNADA.CURSO_OFERTA_LOCALIDADE &&
                                                           w.Entidade.Seq == seqEntidade)
                                               .Select(s => s.GrauAcademico.Descricao).Distinct().ToList()
                }).OrderBy(o => o.DataDocumento).ToList();

            foreach (var item in retorno)
            {
                if (item.GrauAcademico.Where(w => w != null).Any())
                    item.DescricaoGrauAcademico = string.Join("/", item.GrauAcademico).TrimEnd('/');
            }

            return retorno;
        }

        public AtoNormativoVO BuscarAtoNormativo(long seqAtoNormativo)
        {
            var atoNormativo = this.SearchByKey(new SMCSeqSpecification<AtoNormativo>(seqAtoNormativo),
                                                IncludesAtoNormativo.ArquivoAnexado
                                              | IncludesAtoNormativo.Entidades_Entidade
                                              | IncludesAtoNormativo.AssuntoNormativo
                                              | IncludesAtoNormativo.TipoAtoNormativo);

            var retorno = atoNormativo.Transform<AtoNormativoVO>();

            if (atoNormativo.AssuntoNormativo != null)
                retorno.DescricaoAssuntosNormativos = atoNormativo.AssuntoNormativo.Descricao;
            if (atoNormativo.TipoAtoNormativo != null)
                retorno.DescricaoTiposAtoNormativos = atoNormativo.TipoAtoNormativo.Descricao;

            if (retorno.ArquivoAnexado != null)
                retorno.ArquivoAnexado.GuidFile = atoNormativo.ArquivoAnexado.UidArquivo.ToString();

            retorno.HabilitaCampo = retorno.VeiculoPublicacao != null;
            
            return retorno;
        }

        public long SalvarAtoNormativo(AtoNormativoVO modelo)
        {
            var dominio = modelo.Transform<AtoNormativo>();

            // Se o arquivo anexo não foi alterado, atualiza com o conteúdo que está no banco
            EnsureFileIntegrity(dominio, x => x.SeqArquivoAnexado, x => x.ArquivoAnexado);

            if (modelo.NumeroDocumento.Equals("0") || modelo.NumeroSecaoPublicacao == 0 || modelo.NumeroPublicacao == 0 || modelo.NumeroPaginaPublicacao == 0)
                throw new AtoNormativoCamposZeradosException();


            this.SaveEntity(dominio);

            return dominio.Seq;
        }      
    }
}