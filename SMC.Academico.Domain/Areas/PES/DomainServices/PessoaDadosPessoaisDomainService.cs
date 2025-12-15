using SMC.Academico.Domain.Areas.PES.Models;
using SMC.Academico.Domain.Areas.PES.ValueObjects;
using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.Framework.Specification;
using SMC.Localidades.ServiceContract.Areas.LOC.Interfaces;

namespace SMC.Academico.Domain.Areas.PES.DomainServices
{
    public class PessoaDadosPessoaisDomainService : AcademicoContextDomain<PessoaDadosPessoais>
    {
        #region [ Service ]

        private ILocalidadeService LocalidadeService
        {
            get => Create<ILocalidadeService>();
        }

        #endregion [ Service ]

        /// <summary>
        /// Retorna o nome segundo a RN_PES_023 - Nome e Nome Social - Visão Administrativo
        ///     Quando a pessoa-atuação possuir nome social, exibi-lo seguido do nome da pessoa-atuação entre parênteses:
        ///     "Nome social" + "(" + "Nome" + ")"
        /// </summary>
        /// <param name="nome">Nome</param>
        /// <param name="nomeSocial">Nome socical ou nulo</param>
        /// <returns>Nome segundo a RN_PES_023</returns>
        public static string FormatarNomeSocial(string nome, string nomeSocial)
        {
            return string.IsNullOrEmpty(nomeSocial) ? nome : $"{nomeSocial} ({nome})";
        }

        public PessoaDadosPessoaisVO VisualizarDadosPessoais(long seqPessoaDadosPessoais)
        {
            var spec = new SMCSeqSpecification<PessoaDadosPessoais>(seqPessoaDadosPessoais);
            var retorno = this.SearchProjectionByKey(spec, s => new PessoaDadosPessoaisVO()
            {
                Seq = s.Seq,
                SeqPessoa = s.SeqPessoa,
                Nome = s.Nome,
                NomeSocial = s.NomeSocial,
                Sexo = s.Sexo,
                DataNascimento = s.Pessoa.DataNascimento,
                DataInclusao = s.DataInclusao,
                UsuarioInclusao = s.UsuarioInclusao,
                NumeroIdentidade = s.NumeroIdentidade,
                OrgaoEmissorIdentidade = s.OrgaoEmissorIdentidade,
                UfIdentidade = s.UfIdentidade,
                DataExpedicaoIdentidade = s.DataExpedicaoIdentidade,
                TipoNacionalidade = s.Pessoa.TipoNacionalidade,
                UfNaturalidade = s.UfNaturalidade,
                CodigoCidadeNaturalidade = s.CodigoCidadeNaturalidade,
                DescricaoNaturalidadeEstrangeira = s.DescricaoNaturalidadeEstrangeira,
                CodigoPaisNacionalidade = s.Pessoa.CodigoPaisNacionalidade
            });

            if (retorno.TipoNacionalidade != TipoNacionalidade.Brasileira)
            {
                var pais = LocalidadeService.BuscarPais(retorno.CodigoPaisNacionalidade);
                retorno.Nacionalidade = $"{retorno.TipoNacionalidade}: {pais.Nome}";
            }
            else
            {
                var pais = LocalidadeService.BuscarPais(retorno.CodigoPaisNacionalidade);
                retorno.Nacionalidade = pais.Nome;
            }

            if (retorno.TipoNacionalidade == TipoNacionalidade.Brasileira || retorno.TipoNacionalidade == TipoNacionalidade.BrasileiraNaturalizado)
            {
                if (retorno.CodigoCidadeNaturalidade.HasValue && !string.IsNullOrEmpty(retorno.UfNaturalidade))
                {
                    var cidade = LocalidadeService.BuscarCidade(retorno.CodigoCidadeNaturalidade.Value, retorno.UfNaturalidade);
                    retorno.Naturalidade = $"{cidade.Nome.Trim()} - {cidade.CodigoUf}";
                }
                else
                    retorno.Naturalidade = retorno.DescricaoNaturalidadeEstrangeira;
            }
            else
            {
                retorno.Naturalidade = retorno.DescricaoNaturalidadeEstrangeira;
            }

            return retorno;
        }
    }
}
