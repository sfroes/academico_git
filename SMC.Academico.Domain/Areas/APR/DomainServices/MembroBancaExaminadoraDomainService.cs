using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Academico.Domain.Areas.APR.Models;
using SMC.Academico.Domain.Areas.APR.Specifications;
using SMC.Academico.Domain.Areas.APR.ValueObjects;
using SMC.Academico.Domain.Areas.ORT.DomainServices;
using SMC.Framework;
using SMC.Framework.Model;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Areas.APR.DomainServices
{
    public class MembroBancaExaminadoraDomainService : AcademicoContextDomain<MembroBancaExaminadora>
    {
        #region [ Domain Services ]

        private TipoOrientacaoDomainService TipoOrientacaoDomainService { get => Create<TipoOrientacaoDomainService>(); }

        #endregion [ Domain Services ]

        /// <summary>
        /// Buscar a orientação do tipo "Orientação em Trabalho de Conclusão" e para professor cujo tipo de participação na orientação é
        /// "Orientador" ou "Co-orientador" e com o período vigente na data da avaliação incluir informação conforme abaixo.
        /// Os dados de cada membro devem ser exibidos concatenados da seguinte forma:
        /// "Prof(a). "+ [Nome do membro] + "("+ [Instituição associada]  +" - "+  [Complemento instituição]  + ")" +' - Orientador'.
        /// "Prof(a). "+ [Nome do membro] + "("+ [Instituição associada]  +" - "+  [Complemento instituição]  + ")" +' - Coorientador'.
        /// </summary>
        /// <param name="seqAplicacaoAvaliacao">Sequencial da aplicação de avaliação.</param>
        /// <returns>Lista de membros da banca examinadora da aplicação da avaliação.</returns>
        public List<SMCDatasourceItem> BuscarMembrosBancaExaminadora(long seqAplicacaoAvaliacao)
        {
            MembroBancaExaminadoraFilterSpecification spec = new MembroBancaExaminadoraFilterSpecification();
            spec.SeqAplicacaoAvaliacao = seqAplicacaoAvaliacao;

            spec.SetOrderBy(x => x.NomeColaborador);

            var membros = SearchProjectionBySpecification(spec, a => new MembroBancaExaminadoraVO
            {
                Instituicao = a.SeqInstituicaoExterna.HasValue ? a.InstituicaoExterna.Nome : a.NomeInstituicaoExterna,
                ComplementoInstituicao = a.ComplementoInstituicao,
                TipoMembroBanca = a.TipoMembroBanca,
                Nome = a.SeqColaborador.HasValue ? a.Colaborador.DadosPessoais.Nome : a.NomeColaborador,
                Sexo = a.SeqColaborador.HasValue ? a.Colaborador.DadosPessoais.Sexo : DadosMestres.Common.Areas.PES.Enums.Sexo.Nenhum,
                Participou = a.Participou,
                AvaliacaoPossuiApuracao = a.AplicacaoAvaliacao.ApuracoesAvaliacao.Any()
            }).ToList();

            List<SMCDatasourceItem> banca = new List<SMCDatasourceItem>();
            foreach (var membro in membros)
            {
                if (!membro.AvaliacaoPossuiApuracao)
                {
                    banca.Add(new SMCDatasourceItem
                    {
                        Seq = seqAplicacaoAvaliacao,
                        Descricao = RecuperarDescricaoMembro(membro)
                    });
                }
                else
                {
                    if (membro.Participou.HasValue && membro.Participou.Value)
                    {
                        banca.Add(new SMCDatasourceItem
                        {
                            Seq = seqAplicacaoAvaliacao,
                            Descricao = RecuperarDescricaoMembro(membro)
                        });
                    }
                }
            }
            return banca;
        }

        private string RecuperarDescricaoMembro(MembroBancaExaminadoraVO membro)
        {
            var sexo = "Prof(a).";

            //TODO: Implementação futura. Quando pudermos identificar o sexo de todos os colaboradores, será alterado o prefixo para
            //masculino e feminino conforme o sexo.
            //switch (membro.Sexo)
            //{
            //    case DadosMestres.Common.Areas.PES.Enums.Sexo.Feminino:
            //        sexo = "Profa.";
            //        break;

            //    case DadosMestres.Common.Areas.PES.Enums.Sexo.Masculino:
            //        sexo = "Prof.";
            //        break;
            //}

            var tipoMembro = membro.TipoMembroBanca == TipoMembroBanca.Orientador || membro.TipoMembroBanca == TipoMembroBanca.Coorientador ?
                $" - {membro.TipoMembroBanca.SMCGetDescription()}" : "";
            var instituicaoComplemento = string.IsNullOrEmpty(membro.Instituicao) ?
                // instituição null
                "" :
                string.IsNullOrEmpty(membro.ComplementoInstituicao) ?
                    // complemento null
                    $" ({membro.Instituicao})" :
                    // complemento e instituição com valor
                    $" ({membro.Instituicao} - {membro.ComplementoInstituicao})";
            return $"{sexo} {membro.Nome}{instituicaoComplemento}{tipoMembro}";
        }
    }
}