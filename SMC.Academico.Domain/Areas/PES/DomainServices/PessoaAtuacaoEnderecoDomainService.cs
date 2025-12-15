using SMC.Academico.Common.Areas.PES.Includes;
using SMC.Academico.Domain.Areas.PES.Models;
using SMC.Academico.Domain.Areas.PES.Specifications;
using SMC.Academico.Domain.Areas.PES.ValueObjects;
using SMC.Framework.Domain;
using SMC.Framework.Extensions;
using SMC.Framework.Specification;
using SMC.Framework.Util;
using SMC.Localidades.Common.Constants;
using SMC.Localidades.ServiceContract.Areas.LOC.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Areas.PES.DomainServices
{
    public class PessoaAtuacaoEnderecoDomainService : AcademicoContextDomain<PessoaAtuacaoEndereco>
    {
        #region [ DataSources ]

        private PessoaDomainService PessoaDomainService
        {
            get { return this.Create<PessoaDomainService>(); }
        }

        private ILocalidadeService LocalidadeService
        {
            get { return this.Create<ILocalidadeService>(); }
        }

        #endregion [ DataSources ]

        /// <summary>
        /// Recupera os endereços associados a uma PessoaAtuacaoEndereco.
        /// Caso o endereço não esteja associado recupera o endreço da PessoaEndereco
        /// </summary>
        /// <param name="filtro">Filtro com os sequencias dos PessoaEndereco associados e da Pessoa</param>
        /// <returns>Lista de PessoaAtuacaoEndereco asociada
        /// (os PessoaEndereco não associados à PessoaAtuacao serão retornados como novos registros sem seq)
        /// </returns>
        public List<PessoaAtuacaoEnderecoLookupVO> BuscarPessoaAtuacaoEnderecosLookup(PessoaAtuacaoEnderecoFilterSpecification pessoaAtuacaoEnderecoFilterSpecification)
        {
            var enderecosSelecionados = new List<PessoaAtuacaoEnderecoLookupVO>();
            var seqsSelecionados = new List<long>();

            var specPessoa = new SMCSeqSpecification<Pessoa>(pessoaAtuacaoEnderecoFilterSpecification.SeqPessoa.Value);
            var includesPessoa = IncludesPessoa.Atuacoes_Enderecos_PessoaEndereco_Endereco
                               | IncludesPessoa.Enderecos_Endereco;
            var pessoa = this.PessoaDomainService.SearchByKey(specPessoa, includesPessoa);

            var atuacao = pessoa.Atuacoes.SingleOrDefault(s => s.TipoAtuacao == pessoaAtuacaoEnderecoFilterSpecification.TipoAtuacao && s.Seq == pessoaAtuacaoEnderecoFilterSpecification.SeqPessoaAtuacao);

            // Caso já tenha uma atuação cadastrada, recupera os endereços já associados
            if (atuacao != null)
            {
                enderecosSelecionados.AddRange(atuacao.Enderecos
                    .Where(w => pessoaAtuacaoEnderecoFilterSpecification.Seqs.Contains(w.SeqPessoaEndereco))
                    .Select(s => s.Transform<PessoaAtuacaoEnderecoLookupVO>()));

                //FIX: Definir como tratar a diferênça de modelo
                enderecosSelecionados.ForEach(f => f.Seq = f.SeqPessoaEndereco);

                seqsSelecionados = enderecosSelecionados.Select(s => s.SeqPessoaEndereco).ToList();
            }

            // Recupera os endereços que ainda não foram associados
            var novosEnderecos = pessoa.Enderecos
                .Where(w => pessoaAtuacaoEnderecoFilterSpecification.Seqs.Contains(w.Seq) && !seqsSelecionados.Contains(w.Seq));
            foreach (var novoEndereco in novosEnderecos)
            {
                // Converte uma PessoaEndereco num novo PessoaAtuacaoEndereco
                var novoEnderecoLookup = novoEndereco.Transform<PessoaAtuacaoEnderecoLookupVO>();
                novoEnderecoLookup.SeqPessoaEndereco = novoEndereco.Seq;
                //FIX: Definir como tratar a diferênça de modelo
                //novoEnderecoLookup.Seq = 0;
                enderecosSelecionados.Add(novoEnderecoLookup);
            }

            // Preenche a descrição dos países
            var paises = this.LocalidadeService.BuscarPaisesValidosCorreios();
            foreach (var endereco in enderecosSelecionados)
            {
                endereco.DescPais = paises.FirstOrDefault(f => f.Codigo == endereco.CodigoPais)?.Nome;
                if (endereco.CodigoPais == LocalidadesDefaultValues.SEQ_PAIS_BRASIL)
                    endereco.CEP = SMCMask.ApplyMaskCEP(endereco.CEP);
            }

            return enderecosSelecionados;
        }
    }
}