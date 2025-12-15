using System;
using SMC.Academico.Common.Areas.ORG.Enums;
using SMC.Academico.Domain.Areas.ORG.Models;
using SMC.Framework.Domain;
using SMC.Academico.Domain.Areas.ORG.Specifications;
using SMC.Academico.Common.Areas.ORG.Includes;
using System.Linq;
using SMC.Academico.Domain.Areas.ORG.ValueObjects;
using SMC.Framework.Specification;
using SMC.Framework.Model;

namespace SMC.Academico.Domain.Areas.ORG.DomainServices
{
    public class InstituicaoNivelModeloRelatorioDomainService : AcademicoContextDomain<InstituicaoNivelModeloRelatorio>
    {
        public InstituicaoNivelModeloRelatorioVO BuscarInstituicaoNivelModeloRelatorio(long seq)
        {
            return this.SearchProjectionByKey(new SMCSeqSpecification<InstituicaoNivelModeloRelatorio>(seq), x => new InstituicaoNivelModeloRelatorioVO
            {
                Seq = x.Seq,
                SeqInstituicaoNivel = x.SeqInstituicaoNivel,
                DescricaoInstituicaoNivel = x.InstituicaoNivel.NivelEnsino.Descricao,
                ModeloRelatorio = x.ModeloRelatorio,
                ArquivoModelo = new SMCUploadFile
                {
                    GuidFile = x.ArquivoModelo.UidArquivo.ToString(),
                    Name = x.ArquivoModelo.Nome,
                    Size = x.ArquivoModelo.Tamanho,
                    Type = x.ArquivoModelo.Tipo
                },
                SeqArquivoModelo = x.SeqArquivoModelo
            });
        }

        public InstituicaoNivelModeloRelatorio BuscarTemplateModeloRelatorio(long seqInstituicaoNivel, ModeloRelatorio modeloRelatorio)
        {
            var spec = new InstituicaoNivelModeloRelatorioFilterSpecification() { SeqInstituicaoNivel = seqInstituicaoNivel, ModeloRelatorio = modeloRelatorio };

            var obj = SearchBySpecification(spec, IncludesModeloRelatorio.ArquivoModelo).FirstOrDefault();

            return obj;
        }

        /// <summary>
        /// Salva um modelo de relatório para um nivel de  instituição
        /// </summary>
        /// <param name="modelo">Modelo de relatorio do nivel de Instituição a ser salvo</param>
        /// <returns>Sequencial do modelo de relatorio de nivel de intituição salvo</returns>
        public long SalvarInstituicaoNivelModelo(InstituicaoNivelModeloRelatorio modelo)
        {
            this.EnsureFileIntegrity(modelo, x => x.SeqArquivoModelo, x => x.ArquivoModelo);

            // Salva o modelo
            this.SaveEntity(modelo);

            // Retorna o sequencial salvo
            return modelo.Seq;
        }
    }
}

