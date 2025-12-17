# Guia de Migração Angular 10 → 19
## Sistema Acadêmico SMC

---

## ⚠️ PREPARAÇÃO OBRIGATÓRIA

### 1. Backup Completo
```powershell
# Criar backup da pasta inteira
Copy-Item -Path "D:\SMC\academico_git" -Destination "D:\SMC\academico_git_BACKUP_v10" -Recurse
```

### 2. Controle de Versão
```powershell
# Commit tudo antes de começar
git add .
git commit -m "backup: antes da migração angular 10 -> 19"
git branch migration-angular-19
git checkout migration-angular-19
```

### 3. Node.js e NPM
- **Angular 10**: Node 12.x ou 14.x
- **Angular 19**: Node 18.x ou 20.x (LTS recomendado)

```powershell
node --version  # Verificar versão atual
npm --version
```

---

## 🎯 ESTRATÉGIA DE MIGRAÇÃO

### Você TEM 3 opções:

#### ✅ **OPÇÃO 1: Migração Incremental (RECOMENDADA)**
Migrar uma versão por vez: 10→11→12→13→14→15→16→17→18→19

**Vantagens:**
- Mais segura
- Identifica problemas específicos de cada versão
- Documentação oficial para cada passo

**Desvantagens:**
- Mais demorada
- Mais commits intermediários

#### ⚡ **OPÇÃO 2: Migração por Saltos**
Migrar em blocos: 10→13→15→17→19

**Vantagens:**
- Mais rápida
- Menos passos intermediários

**Desvantagens:**
- Mais breaking changes de uma vez
- Debugging mais difícil

#### 🚀 **OPÇÃO 3: Migração Direta**
Tentar ir direto para 19 (NÃO RECOMENDADO para projetos complexos)

---

## 📝 ROTEIRO PASSO A PASSO

### **FASE 1: Angular 10 → 11**

#### Pré-requisitos
- Node.js 12.20+, 14.15+, ou 16.10+
- TypeScript 4.0

#### Comandos
```powershell
cd D:\SMC\academico_git\SMC.Academico.Client

# Atualizar Angular CLI globalmente
npm install -g @angular/cli@11

# Atualizar projeto
ng update @angular/core@11 @angular/cli@11

# Atualizar Material/CDK se necessário
ng update @angular/cdk@11
```

#### Breaking Changes Principais
- **Removed**: `ViewEncapsulation.Native` (usar `ViewEncapsulation.ShadowDom`)
- **Mudança**: Lazy loading agora usa `import()` nativo
- **Removido**: Suporte para TSLint (migrar para ESLint)

#### Bibliotecas de Terceiros
```powershell
# PrimeNG
npm install primeng@11.4.4 --save

# PO-UI - verificar compatibilidade
npm install @po-ui/ng-components@4.x --save

# Angular Calendar
npm install angular-calendar@0.28.x --save
```

#### Testar
```powershell
npm run administrativo
npm run aluno
npm run professor
```

---

### **FASE 2: Angular 11 → 12** ✅ CONCLUÍDO

#### Pré-requisitos
- Node.js 16.x (usado: 16.20.2)
- TypeScript 4.3.5

#### Status: MIGRADO COM SUCESSO
- ✅ package.json atualizado para Angular 12.2.17
- ✅ TypeScript atualizado para 4.3.5
- ✅ npm install concluído
- ✅ Build testado com sucesso (22s)
- ✅ Commit realizado

#### Observações
- View Engine foi REMOVIDO - apenas Ivy
- Suporte para IE11 removido
- PrimeNG mantido em 11.2.0
- PO-UI mantido em 4.11.0

---

### **FASE 3: Angular 12 → 13** ✅ CONCLUÍDO

#### Pré-requisitos
- Node.js 16.x (usado: 16.20.2)
- TypeScript 4.4.4

#### Status: MIGRADO COM SUCESSO
- ✅ package.json atualizado para Angular 13.4.0
- ✅ TypeScript atualizado para 4.4.4
- ✅ npm install concluído (com --legacy-peer-deps)
- ✅ Build testado com sucesso (23s)

#### Breaking Changes Principais
- **REMOVIDO**: Suporte para IE11 completamente
- **Removido**: View Engine completamente
- **Mudança**: `ng build` produz ES2020 por padrão
- **APF**: Formato de pacote atualizado
- **Bibliotecas View Engine**: Warnings esperados

#### Observações
- PrimeNG mantido em 11.2.0 (View Engine warning)
- PO-UI mantido em 4.11.0 (View Engine warning)
- angular-calendar 0.28.22 (View Engine warning)
- Warnings de autoprefixer do PO-UI (não bloqueante)

---

### **FASE 4: Angular 13 → 14** ✅ CONCLUÍDO

#### Pré-requisitos
- Node.js 14.15+, ou 16.10+
- TypeScript 4.6

#### Comandos
```powershell
ng update @angular/core@14 @angular/cli@14
ng update @angular/cdk@14
```

#### Breaking Changes Principais
- **Formulários**: `FormControlStatus` tipado estritamente
- **Typed Forms**: Forms agora são tipados por padrão
- **Standalone Components**: Introduzidos (OPCIONAL)

#### Bibliotecas
```powershell
npm install primeng@14.2.x --save
npm install @po-ui/ng-components@7.x --save (verificar)
npm install angular-calendar@0.31.x --save
```

---

### **FASE 5: Angular 14 → 15** ✅ CONCLUÍDO

#### Pré-requisitos
- Node.js 14.20+, 16.13+, ou 18.10+
- TypeScript 4.8

#### Comandos
```powershell
ng update @angular/core@15 @angular/cli@15
ng update @angular/cdk@15
```

#### Breaking Changes Principais
- **REMOVIDO**: `@angular/flex-layout` descontinuado (migrar para CSS Grid/Flexbox)
- **Standalone**: APIs melhoradas
- **Injeção de dependências**: `inject()` function disponível

#### Atenção Especial
Se usar `@angular/flex-layout`, precisa migrar para CSS puro ou biblioteca alternativa.

#### Bibliotecas
```powershell
npm install primeng@15.4.x --save
npm install angular-calendar@0.31.x --save
```

---

### **FASE 6: Angular 15 → 16** ✅ CONCLUÍDO

#### Pré-requisitos
- Node.js 18.10+ ou 22.x (usado: 22.21.0)
- TypeScript 4.9.3+

#### Status: MIGRADO COM SUCESSO
- ✅ package.json atualizado para Angular 16.2.12
- ✅ @angular/cdk atualizado para 16.2.14
- ✅ @perfectmemory/ngx-contextmenu atualizado para 16.0.2
- ✅ @po-ui/ng-components atualizado para 16.16.0
- ✅ primeng atualizado para 16.9.x
- ✅ angular-calendar atualizado para 0.31.1
- ✅ TypeScript 4.9.5
- ✅ zone.js atualizado para 0.13.3
- ✅ npm install com --legacy-peer-deps
- ✅ Build testado com sucesso (~16s)

#### Correções Necessárias
- ❌ Removido `extractCss` do angular.json (obsoleto)
- ✅ Corrigido `SmcCalendarCustomFormatter`: Injetado `DateAdapter` no construtor
- ✅ Removido import desnecessário em `boolean.pipe.ts`

#### Breaking Changes Principais
- **Signals**: Introduzido (novo sistema de reatividade)
- **Required Inputs**: `@Input({ required: true })`
- **Self-closing tags**: Suporte para `<component />`
- **extractCss**: Removido do angular.json (agora é comportamento padrão)

#### Observações
- Warnings de SASS deprecation (não bloqueante)
- Warning de CommonJS para `moment` (não bloqueante)
- Build warning de budget exceeded (2.52 MB vs 2.00 MB)

---

### **FASE 7: Angular 16 → 17**

#### Pré-requisitos
- Node.js 18.13+, ou 20.9+
- TypeScript 5.2+

#### Comandos
```powershell
ng update @angular/core@17 @angular/cli@17
ng update @angular/cdk@17
```

#### Breaking Changes Principais
- **Nova sintaxe de controle de fluxo** (OPCIONAL - antiga ainda funciona):
  - `@if` ao invés de `*ngIf`
  - `@for` ao invés de `*ngFor`
  - `@switch` ao invés de `*ngSwitch`
- **Deferrable Views**: `@defer` para lazy loading
- **Vite** como opção de build

#### Exemplo de Nova Sintaxe (OPCIONAL)
```typescript
// Antiga (ainda funciona)
<div *ngIf="user">{{ user.name }}</div>

// Nova (opcional)
@if (user) {
  <div>{{ user.name }}</div>
}
```

#### Bibliotecas
```powershell
npm install primeng@17.18.x --save
npm install angular-calendar@0.32.x --save
```

---

### **FASE 8: Angular 17 → 18**

#### Pré-requisitos
- Node.js 18.19+, ou 20.9+
- TypeScript 5.4+

#### Comandos
```powershell
ng update @angular/core@18 @angular/cli@18
ng update @angular/cdk@18
```

#### Breaking Changes Principais
- **Zoneless**: Preparação para aplicações sem Zone.js
- **Material 3**: Novos temas
- **Hydration**: Melhorias para SSR

#### Bibliotecas
```powershell
npm install primeng@17.18.x --save
```

---

### **FASE 9: Angular 18 → 19**

#### Pré-requisitos
- Node.js 18.19+, 20.9+, ou 22.0+
- TypeScript 5.5+

#### Comandos
```powershell
ng update @angular/core@19 @angular/cli@19
ng update @angular/cdk@19
```

#### Breaking Changes Principais
- **Resource API**: Nova API para carregamento de dados
- **LinkedSignal**: Novo tipo de signal
- **Melhorias em Signals** e reatividade

#### Bibliotecas
```powershell
npm install primeng@18.0.x --save
```

---

## 🧪 CHECKLIST DE TESTES (PARA CADA VERSÃO)

### ✅ Verificações Obrigatórias

```powershell
# 1. Build sem erros
npm run build

# 2. Testes unitários passando
npm run test

# 3. Aplicação rodando em dev
npm run administrativo
npm run aluno
npm run professor

# 4. Verificar funcionalidades críticas:
```

- [ ] Login/Autenticação
- [ ] Navegação entre módulos
- [ ] Formulários principais
- [ ] Listagens com tabelas
- [ ] Upload de arquivos
- [ ] Calendário (angular-calendar)
- [ ] Componentes PO-UI
- [ ] Componentes PrimeNG

---

## 🐛 PROBLEMAS COMUNS E SOLUÇÕES

### Problema 1: Erros de TypeScript após upgrade
```powershell
# Limpar cache e reinstalar
rm -rf node_modules package-lock.json
npm cache clean --force
npm install
```

### Problema 2: Bibliotecas incompatíveis
```powershell
# Ver versões incompatíveis
npm ls

# Forçar resolução de peer dependencies (usar com cuidado)
npm install --legacy-peer-deps
```

### Problema 3: Build falhando
```typescript
// angular.json - Adicionar em allowedCommonJsDependencies
"allowedCommonJsDependencies": [
  "angular-calendar",
  "moment",
  "uuid",
  // adicionar outras bibliotecas que causam warnings
]
```

### Problema 4: Testes quebrando
```typescript
// Atualizar imports de testes
import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';
```

---

## 📚 DECISÃO: STANDALONE OU NÃO?

### Manter NgModules (RECOMENDADO para seu caso)

**Quando manter:**
- ✅ Sistema em produção com muitas telas
- ✅ Equipe acostumada com NgModules
- ✅ Código estável e funcionando
- ✅ Prazo apertado para migração

**Código continua assim:**
```typescript
@NgModule({
  declarations: [AppComponent, HeaderComponent],
  imports: [CommonModule, SharedModule, AprModule],
  providers: [/* ... */]
})
export class AppModule {}
```

### Migrar para Standalone (OPCIONAL)

**Quando considerar:**
- Componentes novos
- Refatoração planejada
- Simplificar arquitetura futura

**Não é obrigatório e pode ser feito gradualmente depois!**

---

## 🎯 ESTRATÉGIA RECOMENDADA PARA SEU PROJETO

### Fase 1: Migração Rápida (1-2 semanas)
1. Angular 10 → 13 (3 versões)
2. Testar tudo profundamente
3. Deploy em homologação

### Fase 2: Migração Intermediária (1 semana)
1. Angular 13 → 15 (2 versões)
2. Resolver problemas do @angular/flex-layout se houver
3. Testar novamente

### Fase 3: Migração Final (1 semana)
1. Angular 15 → 17 (2 versões)
2. Angular 17 → 19 (2 versões)
3. Testes completos
4. Deploy final

**Total estimado: 3-4 semanas de trabalho focado**

---

## 📞 PRÓXIMOS PASSOS

1. **Criar backup** (conforme início deste guia)
2. **Verificar Node.js** está na versão adequada
3. **Começar com Angular 10 → 11**
4. **Testar cada etapa** antes de avançar

---

## 🔗 Links Úteis

- [Angular Update Guide](https://update.angular.io/)
- [PrimeNG Versões](https://www.primefaces.org/primeng-v17-lts/)
- [PO-UI Docs](https://po-ui.io/)
- [Angular Calendar](https://mattlewis92.github.io/angular-calendar/)

---

**Última atualização:** Dezembro 2025
**Projeto:** SMC Acadêmico
**Versão atual:** Angular 10.0.14
**Versão alvo:** Angular 19
