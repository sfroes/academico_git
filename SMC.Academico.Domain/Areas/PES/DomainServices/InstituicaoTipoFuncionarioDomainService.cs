using SMC.Academico.Common.Areas.PES.Exceptions;
using SMC.Academico.Common.Constants;
using SMC.Academico.Domain.Areas.PES.Models;
using SMC.Academico.Domain.Areas.PES.Specifications;
using SMC.Framework.Model;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Areas.PES.DomainServices
{
    public class InstituicaoTipoFuncionarioDomainService : AcademicoContextDomain<InstituicaoTipoFuncionario>
    {
        private InstituicaoTipoEntidadeTipoFuncionarioDomainService InstituicaoTipoEntidadeTipoFuncionarioDomainService => Create<InstituicaoTipoEntidadeTipoFuncionarioDomainService>();

        /// <summary>
        /// Buscar tipo funcionario por instituição de ensino
        /// </summary>
        /// <param name="seqInstituicaoEnsino">Sequencial da instituição de ensino</param>
        /// <returns>Lista de todos os tipos de funcionários da instituição de ensino</returns>
        public List<SMCDatasourceItem> BuscarTipoFuncionarioInstituicao()
        {
            var seqInstituicaoEnsinoLogada = GetDataFilter(FILTER.INSTITUICAO_ENSINO).FirstOrDefault();
            var spec = new InstituicaoTipoFuncionarioFilterSpecification() { SeqInstituicaoEnsino = seqInstituicaoEnsinoLogada };

            var lista = this.SearchProjectionBySpecification(spec, x => new SMCDatasourceItem()
            {
                Seq = x.SeqTipoFuncionario,
                Descricao = x.TipoFuncionario.DescricaoMasculino
            }).OrderBy(o => o.Descricao).ToList();

            return lista;
        }
        
        /// <summary>
        /// Buscar tipo funcionario por instituição de ensino
        /// </summary>
        /// <param name="seqInstituicaoEnsino">Sequencial da instituição de ensino</param>
        /// <returns>Lista de todos os tipos de funcionários da instituição de ensino</returns>
        public List<SMCDatasourceItem> BuscarTipoFuncionarioInstituicaoETipoEntidadePorFuncionario(long seqInstituicaoEnsino)
        {
            List<SMCDatasourceItem> instTipoFunc = new List<SMCDatasourceItem>();
            var spec = new InstituicaoTipoFuncionarioFilterSpecification() { SeqInstituicaoEnsino = seqInstituicaoEnsino };
            List<InstituicaoTipoFuncionario> result = SearchBySpecification(spec, i => i.TipoFuncionario)
                                                                           .OrderBy(o => o.TipoFuncionario.DescricaoMasculino)
                                                                           .ToList();
            if (result.Count() > 0)
            {
                instTipoFunc = result.Select(s => new SMCDatasourceItem() { Seq = s.SeqTipoFuncionario, Descricao = s.TipoFuncionario.DescricaoMasculino }).ToList();
            }

            var tipoEntidadesPorTipoFunc = InstituicaoTipoEntidadeTipoFuncionarioDomainService.BuscarTipoEntidadePorInstituicaoLogada(seqInstituicaoEnsino);

            var retorno = instTipoFunc.Union(tipoEntidadesPorTipoFunc)
                                      .OrderBy(o=>o.Descricao)
                                      .GroupBy(g=>g.Descricao)
                                      .Select(s=>s.FirstOrDefault())
                                      .ToList();

            return retorno;
        }




        public long Salvar(InstituicaoTipoFuncionario instituicaoTipoFuncionario)
        {
            var spec = new InstituicaoTipoEntidadeTipoFuncionarioFilterSpecification() 
            { 
                SeqTipoFuncionario = instituicaoTipoFuncionario.SeqTipoFuncionario
            };

            // Verifica se existe o funcionario ja associado a uma parametrização de tipo entidade x tipo vinculo funcionario
            var seqFuncionarioEncontrado = InstituicaoTipoEntidadeTipoFuncionarioDomainService.SearchProjectionByKey(spec, x => x.Seq);
            
            if (seqFuncionarioEncontrado > 0)
            {
                throw new InstituicaoTipoFuncionarioVinculoAssociadoException();
            }

            this.SaveEntity(instituicaoTipoFuncionario);

            return instituicaoTipoFuncionario.Seq;
        }
    }
}
