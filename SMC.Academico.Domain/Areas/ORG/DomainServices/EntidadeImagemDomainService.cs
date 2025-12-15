using SMC.Academico.Common.Areas.ORG.Enums;
using SMC.Academico.Common.Constants;
using SMC.Academico.Domain.Areas.ORG.Models;
using SMC.Academico.Domain.Areas.ORG.Specifications;
using SMC.Academico.Domain.Areas.ORG.ValueObjects;
using SMC.Framework.Model;
using System.Linq;


namespace SMC.Academico.Domain.Areas.ORG.DomainServices
{
    public class EntidadeImagemDomainService : AcademicoContextDomain<EntidadeImagem>
    {

        public EntidadeImagemVO BuscarEntidadeImagem(long seq)
        {
            this.DisableFilter(FILTER.INSTITUICAO_ENSINO);

            return this.SearchProjectionByKey(new EntidadeImagemFilterSpecification() { Seq = seq },
                                                     entidade => new EntidadeImagemVO
                                                     {
                                                         Seq = entidade.Seq,
                                                         SeqEntidade = entidade.SeqEntidade,
                                                         TipoImagem = entidade.TipoImagem,
                                                         SeqArquivoAnexado = entidade.SeqArquivoAnexado,
                                                         ArquivoAnexado = new SMCUploadFile
                                                         {
                                                             GuidFile = entidade.ArquivoAnexado.UidArquivo.ToString(),
                                                             Name = entidade.ArquivoAnexado.Nome,
                                                             Size = entidade.ArquivoAnexado.Tamanho,
                                                             Type = entidade.ArquivoAnexado.Tipo
                                                         },

                                                     });

            this.EnableFilter(FILTER.INSTITUICAO_ENSINO);

        }

        public SMCPagerData<EntidadeImagemVO> BuscarEntidadeImagens(EntidadeImagemFilterSpecification filtros) 
        {
            this.DisableFilter(FILTER.INSTITUICAO_ENSINO);

            int total = 0;
            var lista = this.SearchProjectionBySpecification(filtros, e => new EntidadeImagemVO()
            {
                Seq = e.Seq,
                SeqEntidade = e.SeqEntidade,
                TipoImagem = e.TipoImagem,
                SeqArquivoAnexado = e.SeqArquivoAnexado,
                ArquivoAnexado = new SMCUploadFile
                {
                    GuidFile = e.ArquivoAnexado.UidArquivo.ToString(),
                    Name = e.ArquivoAnexado.Nome,
                    Size = e.ArquivoAnexado.Tamanho,
                    Type = e.ArquivoAnexado.Tipo
                }
            }, out total).ToList();

            this.EnableFilter(FILTER.INSTITUICAO_ENSINO);

            return new SMCPagerData<EntidadeImagemVO>(lista, total);
        }

        public SMCUploadFile BuscarImagemEntidade(long seqEntidade, TipoImagem tipoImagem)
        {
            var spec = new EntidadeImagemFilterSpecification() { SeqEntidade = seqEntidade, TipoImagem = tipoImagem };
            var retorno = this.SearchProjectionByKey(spec, s => new
            {
                s.Seq,
                ArquivoAnexado = new SMCUploadFile
                {
                    FileData = s.ArquivoAnexado.Conteudo,
                    GuidFile = s.ArquivoAnexado.UidArquivo.ToString(),
                    Name = s.ArquivoAnexado.Nome,
                    Size = s.ArquivoAnexado.Tamanho,
                    Type = s.ArquivoAnexado.Tipo
                }
            });

            if (retorno == null)
                return null;

            return retorno.ArquivoAnexado;
        }

        public long SalvarEntidadeImagem(EntidadeImagem imagem)
        {
            // Se o arquivo do logotipo não foi alterado, atualiza com o conteúdo que está no banco
            this.EnsureFileIntegrity(imagem, x => x.SeqArquivoAnexado, x => x.ArquivoAnexado);
            
            this.SaveEntity(imagem);

            return imagem.Seq;
        }

    }
}
