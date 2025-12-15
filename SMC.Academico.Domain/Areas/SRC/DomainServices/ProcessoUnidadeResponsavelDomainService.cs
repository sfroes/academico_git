using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.Academico.Domain.Areas.SRC.Models;
using SMC.Academico.Domain.Areas.SRC.Specifications;
using SMC.Framework.Model;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Areas.SRC.DomainServices
{
    public class ProcessoUnidadeResponsavelDomainService : AcademicoContextDomain<ProcessoUnidadeResponsavel>
    {
        public List<SMCDatasourceItem> BuscarUnidadesResponsaveisVinculadasProcessoSelect(TipoUnidadeResponsavel tipoUnidadeResponsavel = TipoUnidadeResponsavel.Nenhum, bool usarNomeReduzido = false)
        {
            if (tipoUnidadeResponsavel != TipoUnidadeResponsavel.Nenhum)
            {
                var specProcessoUnidadeResponsavel = new ProcessoUnidadeResponsavelFilterSpecification() { TipoUnidadeResponsavel = tipoUnidadeResponsavel };

                return this.SearchProjectionBySpecification(specProcessoUnidadeResponsavel, u => new SMCDatasourceItem()
                {
                    Seq = u.SeqEntidadeResponsavel,
                    Descricao = usarNomeReduzido ? u.EntidadeResponsavel.NomeReduzido : u.EntidadeResponsavel.Nome,
                }, isDistinct: true).OrderBy(x => x.Descricao).ToList();
            }
            else
            {
                return this.SearchProjectionAll(u => new SMCDatasourceItem()
                {
                    Seq = u.SeqEntidadeResponsavel,
                    Descricao = usarNomeReduzido ? u.EntidadeResponsavel.NomeReduzido : u.EntidadeResponsavel.Nome,
                }, isDistinct: true).OrderBy(x => x.Descricao).ToList();
            }
        }

        /// <summary>
        /// Buscar unidades responsaveis do processo
        /// </summary>
        /// <param name="seqProcesso">Sequencial do Processo</param>
        /// <returns>Lista das unidades responsaveis de um processo</returns>
        public List<SMCDatasourceItem> BuscarUnidadesResponsaveisPorProcessoSelect(long seqProcesso)
        {
            var spec = new ProcessoUnidadeResponsavelFilterSpecification() { SeqProcesso = seqProcesso };
            return this.SearchProjectionBySpecification(spec, p => new SMCDatasourceItem()
            {
                Seq = p.Seq,
                Descricao = p.EntidadeResponsavel.Nome
            }, isDistinct: true).OrderBy(x => x.Descricao).ToList();
        }
    }
}