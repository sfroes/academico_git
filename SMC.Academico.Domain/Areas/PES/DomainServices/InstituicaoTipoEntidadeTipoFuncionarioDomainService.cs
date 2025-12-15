using SMC.Academico.Common.Areas.PES.Exceptions;
using SMC.Academico.Domain.Areas.PES.Models;
using SMC.Academico.Domain.Areas.PES.Specifications;
using SMC.Framework.Model;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Areas.PES.DomainServices
{
    public class InstituicaoTipoEntidadeTipoFuncionarioDomainService : AcademicoContextDomain<InstituicaoTipoEntidadeTipoFuncionario>
    {
        private InstituicaoTipoFuncionarioDomainService InstituicaoTipoFuncionarioDomainService => Create<InstituicaoTipoFuncionarioDomainService>();

        public long Salvar(InstituicaoTipoEntidadeTipoFuncionario instituicaoTipoEntidadeTipoFuncionario)
        {
            var spec = new InstituicaoTipoFuncionarioFilterSpecification()
            {
                SeqTipoFuncionario = instituicaoTipoEntidadeTipoFuncionario.SeqTipoFuncionario
            };

            // Verifica se existe o funcionario ja associado a uma parametrização de tipo entidade x tipo vinculo funcionario
            var seqFuncionarioEncontrado = InstituicaoTipoFuncionarioDomainService.SearchProjectionByKey(spec, x => x.Seq);

            if (seqFuncionarioEncontrado > 0)
            {
                throw new InstituicaoTipoEntidadeTipoFuncionarioAssociadoException();
            }

            this.SaveEntity(instituicaoTipoEntidadeTipoFuncionario);

            return instituicaoTipoEntidadeTipoFuncionario.Seq;
        }

        public List<SMCDatasourceItem> BuscarTipoEntidadePorInstituicaoLogada(long seqInstituicaoEnsino)
        {
            var spec = new InstituicaoTipoEntidadeTipoFuncionarioFilterSpecification() { SeqInstituicaoEnsino = seqInstituicaoEnsino };

            var result = this.SearchProjectionBySpecification(spec, x => new SMCDatasourceItem()
            {
                Seq = x.SeqTipoFuncionario,
                Descricao = x.TipoFuncionario.DescricaoMasculino
            }).OrderBy(o=>o.Descricao).ToList();

            return result;
        }
        
        public List<SMCDatasourceItem> BuscarTipoEntidadePorTipoFuncionario(long seqTipoFuncionario)
        {
            var spec = new InstituicaoTipoEntidadeTipoFuncionarioFilterSpecification() { SeqTipoFuncionario = seqTipoFuncionario };

            var result = this.SearchProjectionBySpecification(spec, x => new SMCDatasourceItem()
            {
                Seq = x.InstituicaoTipoEntidade.TipoEntidade.Seq,
                Descricao = x.InstituicaoTipoEntidade.TipoEntidade.Descricao
            }).OrderBy(o=>o.Descricao).ToList();

            return result;
        }

        public bool ListaTiposEntidadePorTipoFuncionario(long seqTipoFuncionario)
        {
            var spec = new InstituicaoTipoEntidadeTipoFuncionarioFilterSpecification() { SeqTipoFuncionario = seqTipoFuncionario };

            var result = this.SearchBySpecification(spec).Select(x => x.SeqInstituicaoTipoEntidade).Any();

            return result;
        }
    }
}
