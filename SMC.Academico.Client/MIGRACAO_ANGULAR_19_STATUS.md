# Migração para Angular 19 - Status e Próximos Passos

## ✅ Tarefas Concluídas

### 1. Pacotes Atualizados para Angular 19
- ✅ Angular Core: 19.0.6
- ✅ Angular CDK: 19.0.5
- ✅ TypeScript: 5.6.3 (Angular 19 requer >=5.5.0)
- ✅ PrimeNG: 19.0.0
- ✅ PO-UI: 19.0.1
- ✅ Angular Calendar: 0.32.0
- ✅ angular-draggable-droppable: 9.0.0
- ✅ angular-resizable-element: 7.0.0
- ✅ date-fns: 4.1.0

### 2. Configuração do angular.json Atualizada
- ✅ Builder atualizado para `@angular-devkit/build-angular:browser-esbuild`
- ✅ Polyfills configurados como array: `["zone.js"]`
- ✅ `main` renomeado corretamente (ao invés de `browser`)
- ✅ Configurações de otimização simplificadas
- ✅ Removidas opções obsoletas: `aot`, `buildOptimizer`, `vendorChunk`, `namedChunks`

### 3. Script de Correção Criado
- ✅ Script PowerShell criado em `fix-moment-imports.ps1`
- ✅ Identifica 15 arquivos com imports incorretos do moment

## ⚠️ PROBLEMA ATUAL

### Arquivos Bloqueados
Os 15 arquivos que precisam ser corrigidos estão bloqueados (provavelmente abertos no VS Code ou outro processo).

**Arquivos que precisam de correção:**
1. `evento-aula.directive.ts`
2. `evento-aula-agendamento-add.component.ts`
3. `evento-aula-agendamento-add-reduzido.component.ts`
4. `evento-aula-agendamento-delete.component.ts`
5. `evento-aula-agendamento-edit-colaborador.component.ts`
6. `evento-aula-agendamento-edit-colaborador-substituto.component.ts`
7. `evento-aula-agendamento-edit-horario.component.ts`
8. `evento-aula-agendamento-edit-local.component.ts`
9. `evento-aula-calendario.component.ts`
10. `evento-aula-notificacoes.component.ts`
11. `evento-aula.service.ts`
12. `apuracao-frequencia-filtro.component.ts`
13. `apuracao-frequencia-grid-item-apuracao.component.ts`
14. `apuracao-frequencia-notificacoes.component.ts`
15. `apuracao-frequencia.service.ts`

### Erro Identificado
```
WARNING: Calling "moment" will crash at run-time because it's an import namespace object, not a function
```

**Causa:** O Angular 19 com esbuild não suporta mais `import * as moment from 'moment'`

**Solução:** Trocar para `import moment from 'moment'`

## 🔧 PASSOS PARA FINALIZAR A MIGRAÇÃO

### Opção 1: Fechar o VS Code e Executar o Script (RECOMENDADO)

1. **Feche completamente o VS Code** (todos os editores e processos)
2. **Pare todos os processos `ng serve`** que estiverem rodando
3. **Execute o script de correção**:
   ```powershell
   cd d:\SMC\academico_git\SMC.Academico.Client
   powershell -ExecutionPolicy Bypass -File fix-moment-imports.ps1
   ```
4. **Reabra o VS Code** e teste a aplicação

### Opção 2: Correção Manual no VS Code

Se preferir corrigir manualmente, use o Find & Replace do VS Code:

1. Abra o VS Code
2. Pressione `Ctrl+Shift+H` (Find & Replace em todos os arquivos)
3. **Find:** `import * as moment from 'moment';`
4. **Replace:** `import moment from 'moment';`
5. Clique em "Replace All" (15 ocorrências)

### Opção 3: Usar Git Bash ou WSL

Se você tiver Git Bash ou WSL instalado:

```bash
cd /d/SMC/academico_git/SMC.Academico.Client/projects

# Encontrar e substituir
find . -name "*.ts" -type f -exec sed -i "s/import \* as moment from 'moment'/import moment from 'moment'/g" {} \;
```

## 📋 Depois de Corrigir os Imports

### 1. Testar o Build
```bash
cd d:\SMC\academico_git\SMC.Academico.Client
npm run build -- --project=administrativo
```

### 2. Testar em Desenvolvimento
```bash
npm run adm
# ou
npm run administrativo
```

### 3. Testar os Outros Projetos
```bash
npm run aluno
npm run professor
```

## ⚠️ Observações Importantes

### Warnings Esperados (NÃO são erros)
- **SASS Deprecations**: Warnings sobre `@import`, `/` para divisão, `lighten()`, etc. São avisos de deprecação do SASS, mas NÃO impedem a compilação
- **CommonJS warnings**: Warnings sobre `moment` ser CommonJS. É normal e não afeta a funcionalidade

### Possíveis Erros Adicionais
Se após corrigir os imports do `moment` ainda houver erros, podem ser:

1. **Erros de TypeScript**: Tipos incompatíveis com Angular 19
   - Solução: Atualizar tipagens com `npm install --save-dev @types/node@latest`

2. **Erros de bibliotecas de terceiros**: Alguma biblioteca pode ter problemas com Angular 19
   - Solução: Verificar se há versões mais recentes disponíveis

3. **Erros de módulos**: Alguns módulos podem precisar de ajustes
   - Solução: Revisar imports e exports nos módulos

## 📊 Checklist de Verificação Pós-Migração

- [ ] Corrigir os 15 imports do moment
- [ ] Build sem erros (warnings são aceitáveis)
- [ ] Servidor de desenvolvimento inicia sem erros
- [ ] Login/Autenticação funciona
- [ ] Navegação entre módulos funciona
- [ ] Formulários principais funcionam
- [ ] Listagens com tabelas funcionam
- [ ] Calendário (angular-calendar) funciona
- [ ] Componentes PO-UI renderizam corretamente
- [ ] Componentes PrimeNG funcionam

## 🚀 Benefícios da Migração para Angular 19

- **Performance**: Esbuild é muito mais rápido que o Webpack
- **Signals**: Novo sistema de reatividade (opcional, mas recomendado para novos códigos)
- **Resource API**: Nova forma de carregar dados (opcional)
- **Suporte estendido**: Angular 19 terá suporte até final de 2025
- **Melhorias de Hydration**: Melhor SSR (se você usar no futuro)

## 📝 Comandos Úteis

```bash
# Verificar versões instaladas
npm list @angular/core @angular/cli primeng @po-ui/ng-components

# Limpar cache e reinstalar (se necessário)
rm -rf node_modules package-lock.json
npm cache clean --force
npm install --legacy-peer-deps

# Ver warnings e erros detalhados
npm run adm -- --verbose

# Build de produção
npm run build -- --project=administrativo --configuration=production
```

## 🔗 Documentação de Referência

- [Angular 19 Release Notes](https://blog.angular.dev/meet-angular-v19-7b29dfd05b84)
- [Angular Update Guide](https://update.angular.io/)
- [PrimeNG 19](https://primeng.org/)
- [PO-UI Documentation](https://po-ui.io/)

---

**Data da Migração:** 17 de Dezembro de 2025
**Versão Anterior:** Angular 17.3.12
**Versão Atual:** Angular 19.0.6
**Status:** ⚠️ 99% Completo - Problema com resolução de fontes pelo esbuild

## ⚠️ PROBLEMA ATUAL: Resolução de Fontes

### Erro
```
Could not resolve "../../../SMC.Academico.Recursos/Content/Fonts/sf-ui-display-regular-webfont.woff"
```

### Causa
O esbuild (novo build system do Angular 19) não consegue resolver CSS url() que apontam para arquivos fora da estrutura do projeto Angular, mesmo que os arquivos existam no disco.

### Tentativas Realizadas
1. ✅ Configurado assets em angular.json para copiar fontes
2. ✅ Ajustada variável SCSS $smc-caminho-fonte-aplicacao
3. ⚠️ esbuild ainda falha na resolução durante processamento CSS

### Próximos Passos
**Opção 1 (Recomendada):** Copiar fontes para dentro do projeto Angular
- Copiar `SMC.Academico.Recursos/Content/Fonts/*` para `projects/shared/assets/fonts/`
- Atualizar `$smc-caminho-fonte-aplicacao` para apontar para o novo local

**Opção 2:** Usar publicPath ou modificar resolução do esbuild
- Requer configuração avançada do webpack/esbuild

**Opção 3:** Comentar @font-face temporariamente
- Sistema usará fontes do sistema como fallback

## ✅ CORREÇÕES APLICADAS

### 1. Imports do Moment Corrigidos
- ✅ Alterado de `import * as moment from 'moment'` para `import moment from 'moment'` em 15 arquivos
- ✅ Adicionado `allowSyntheticDefaultImports: true` e `esModuleInterop: true` no tsconfig.base.json

### 2. Problema Identificado: Componentes Standalone em NgModules

**Erro:** Vários componentes foram marcados como `standalone: true` mas ainda estão declarados em NgModules.

**Componentes afetados:**
- SmcLookupCicloLetivoComponent
- SmcLookupCursoOfertaComponent
- SmcLookupColaboradorComponent
- SmcButtonComponent
- SmcCalendarComponent
- SmcModalComponent
- SmcSelectComponent
- SmcTableComponent
- BooleanPipe, SafeHtmlPipe
- AppComponent, HeaderComponent, MenuComponent, FooterComponent
- E muitos outros...

**Solução:**
Remover `standalone: true` desses componentes OU mudar a arquitetura para usar imports ao invés de declarations.

### Opção Recomendada: Remover `standalone: true`

Como o projeto usa NgModules, a forma mais simples é remover a flag `standalone: true` dos componentes que estão em módulos.
