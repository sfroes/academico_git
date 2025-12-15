using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Academico.Domain.Areas.ORG.DomainServices;
using SMC.Academico.Domain.Areas.ORG.Models;
using SMC.Academico.Domain.Areas.PES.Models;
using SMC.Academico.Domain.Areas.PES.Specifications;
using SMC.Framework.Model;
using SMC.Framework.Specification;  
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Areas.PES.DomainServices
{
    public class InstituicaoNivelTipoMensagemDomainService : AcademicoContextDomain<InstituicaoNivelTipoMensagem>
    {
        #region [ Domain Services ]

        private PessoaAtuacaoDomainService PessoaAtuacaoDomainService
        {
            get { return Create<PessoaAtuacaoDomainService>(); }
        }

        private NivelEnsinoDomainService NivelEnsinoDomainService
        {
            get { return Create<NivelEnsinoDomainService>(); }
        }

        #endregion [ Domain Services ]

        public List<InstituicaoNivelTipoMensagem> BuscarInstituicaoNivelTipoMensagens(InstituicaoNivelTipoMensagemFilterSpecification filtro)
        {
            RefinarFiltro(filtro);
            return SearchBySpecification(filtro).ToList();
        }

        public List<SMCDatasourceItem> BuscarTiposMensagemSelect(long seqPessoaAtuacao, bool apenasCadastroManual)
        {
            List<SMCDatasourceItem> tiposMensagens = new List<SMCDatasourceItem>();

            //Nível de ensino
            NivelEnsino nivel = NivelEnsinoDomainService.BuscarNivelEnsinoAluno(seqPessoaAtuacao);

            //Recuperar o Pessoa Atuacao para obter o Tipo de Atuação.
            var pessoaAtuacao = PessoaAtuacaoDomainService.SearchByKey(new SMCSeqSpecification<PessoaAtuacao>(seqPessoaAtuacao));

            var spec = new InstituicaoNivelTipoMensagemFilterSpecification() { TipoAtuacao = pessoaAtuacao.TipoAtuacao, SeqNivelEnsino = nivel.Seq, PermiteCadastroManual = apenasCadastroManual ? (bool?)true : null };
            var lista = SearchBySpecification(spec, a => a.TipoMensagem).ToList();

            foreach (var item in lista)
            {
                if (!tiposMensagens.Any(a => a.Seq == item.TipoMensagem.Seq))
                {
                    tiposMensagens.Add(new SMCDatasourceItem(item.TipoMensagem.Seq, item.TipoMensagem.Descricao));
                }
            }

            return tiposMensagens.OrderBy(a => a.Descricao).ToList();
        }

        public InstituicaoNivelTipoMensagem BuscarInstituicaoNivelTipoMensagem(InstituicaoNivelTipoMensagemFilterSpecification filtro)
        {
            if (!filtro.SeqTipoMensagem.HasValue)
            {
                return new InstituicaoNivelTipoMensagem();
            }
            RefinarFiltro(filtro);
            var lista = SearchBySpecification(filtro, a => a.TipoMensagem).ToList();
            return (lista != null && lista.Count > 0) ? lista.FirstOrDefault() : new InstituicaoNivelTipoMensagem();
        }

        private void RefinarFiltro(InstituicaoNivelTipoMensagemFilterSpecification filtro)
        {
            //Recuperar o Pessoa Atuacao para obter o Tipo de Atuação.
            var pessoaAtuacao = PessoaAtuacaoDomainService.SearchByKey(new SMCSeqSpecification<PessoaAtuacao>(filtro.SeqPessoaAtuacao.Value));
            filtro.TipoAtuacao = pessoaAtuacao.TipoAtuacao;

            if (filtro.TipoAtuacao == TipoAtuacao.Aluno)
            {
                //Nível de ensino
                NivelEnsino nivel = NivelEnsinoDomainService.BuscarNivelEnsinoAluno(filtro.SeqPessoaAtuacao.Value);
                filtro.SeqNivelEnsino = nivel.Seq;
            }
        }

        public InstituicaoNivelTipoMensagem BuscarInstituicaoNivelTipoMensagemSemRefinar(InstituicaoNivelTipoMensagemFilterSpecification filtro)
        {
            var lista = SearchBySpecification(filtro).ToList();
            return (lista != null && lista.Count > 0) ? lista.FirstOrDefault() : new InstituicaoNivelTipoMensagem();
        }
    }
}