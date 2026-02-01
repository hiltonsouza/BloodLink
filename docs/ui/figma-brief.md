# BloodLink – Especificação de Telas (Referência estilo Figma)

> Objetivo: detalhar **todas as telas** e componentes necessários para criar a interface do BloodLink em Angular.
> Este documento serve como um **guia de layout, fluxo e conteúdo** para protótipos (Figma) e implementação.

## 1) Visão Geral

### 1.1 Perfis de usuário
- **Doador**: pessoa que agenda doações e acompanha histórico.
- **Profissional do banco de sangue**: gerencia campanhas, estoques, agendamentos.
- **Administrador**: gerencia usuários, permissões, conteúdos e relatórios.

### 1.2 Fluxo macro (mapa do produto)
1. **Onboarding público**
2. **Autenticação** (login/registro/recuperação)
3. **Área do Doador**
4. **Área do Banco de Sangue**
5. **Área do Administrador**
6. **Ajuda/FAQ/Contato**

---

## 2) Diretrizes visuais (base para Figma)

### 2.1 Grid e estrutura
- **Desktop**: 12 colunas (80–120px gutters), base 1440px.
- **Tablet**: 8 colunas (base 768px).
- **Mobile**: 4 colunas (base 375px).

### 2.2 Tipografia (sugestão)
- Títulos: Inter / Poppins (700)
- Texto: Inter (400–500)

### 2.3 Cores (sugestão)
- Primária: vermelho (doação) – #E53935
- Secundária: azul saúde – #1565C0
- Neutros: #111827, #6B7280, #F9FAFB
- Status: sucesso #16A34A, alerta #F59E0B, erro #DC2626

### 2.4 Componentes globais
- **AppBar** (logo + navegação + CTA)
- **Sidebar** (área logada)
- **Cards** (campanhas, estoques, posts)
- **Modais** (confirmação, edição, alertas)
- **Tabelas** (listagens)
- **Forms** (input, select, datepicker, textarea, checkbox, radio)
- **Estados** (loading, empty, error)

---

## 3) Telas Públicas (Sem Login)

### 3.1 Home / Landing
**Objetivo**: apresentar a plataforma e direcionar para ações principais.
- Hero com CTA: “Quero doar” / “Sou banco de sangue”.
- Cards de benefícios.
- Seção “Como funciona”.
- Campanhas em destaque.
- FAQ resumido.
- Rodapé com links e contato.

### 3.2 Sobre
- Missão, visão, valores.
- Parceiros e apoiadores.

### 3.3 Campanhas públicas
- Listagem com filtros (cidade, data, tipo sanguíneo).
- Detalhe da campanha (local, data, requisitos, organizador).

### 3.4 Encontre um banco de sangue
- Mapa + lista.
- Filtros por cidade, atendimento, horário.
- Tela de detalhe do banco (serviços, contatos, endereço).

### 3.5 FAQ
- Accordion com perguntas comuns.

### 3.6 Contato
- Formulário: nome, email, assunto, mensagem.
- Informações de atendimento.

---

## 4) Autenticação

### 4.1 Login
- Campos: email, senha.
- Ações: “Entrar”, “Esqueci minha senha”, “Criar conta”.
- Social login (opcional).

### 4.2 Cadastro (escolha de perfil)
- Modal ou tela: “Sou doador” / “Sou banco de sangue”.

### 4.3 Cadastro de Doador (wizard)
**Etapas sugeridas**:
1. Dados pessoais (nome, CPF, data de nascimento, sexo, telefone)
2. Endereço (CEP, rua, número, cidade/UF)
3. Dados de saúde (tipo sanguíneo, peso, doenças preexistentes)
4. Termos e consentimento

### 4.4 Cadastro de Banco de Sangue
- Dados da instituição (nome, CNPJ, responsável, telefone)
- Endereço e horário de funcionamento
- Documentação/validação

### 4.5 Recuperar senha
- Envio de email.
- Confirmação de código.
- Nova senha.

---

## 5) Área do Doador

### 5.1 Dashboard do Doador
- Saudações + resumo
- Próxima doação agendada
- Últimas doações
- Campanhas sugeridas

### 5.2 Meu Perfil
- Dados pessoais (editar)
- Endereço
- Preferências (notificações)

### 5.3 Agendar Doação
- Escolher banco de sangue
- Calendário com horários
- Confirmação e checklist pré-doação

### 5.4 Minhas Doações (Histórico)
- Lista de doações com status
- Detalhe de cada doação

### 5.5 Convites e Campanhas
- Lista de convites recebidos
- CTA para aceitar/recusar

### 5.6 Notificações
- Feed de notificações + filtros

### 5.7 Carteira do Doador (Digital)
- QR Code / ID doador
- Tipo sanguíneo e status de aptidão

---

## 6) Área do Banco de Sangue

### 6.1 Dashboard do Banco
- Estoque geral por tipo sanguíneo
- Agendamentos do dia
- Alertas de baixo estoque

### 6.2 Gestão de Agendamentos
- Lista/agenda por data
- Detalhe do agendamento
- Confirmação / cancelamento

### 6.3 Gestão de Estoque
- Tabela por tipo sanguíneo
- Entradas e saídas
- Exportar relatório

### 6.4 Campanhas
- Criar campanha
- Editar/pausar campanhas
- Lista com performance

### 6.5 Convocar Doadores
- Filtros por perfil (tipo sanguíneo, distância)
- Envio de convites

### 6.6 Perfil da Instituição
- Dados e configurações
- Horário de atendimento

---

## 7) Área do Administrador

### 7.1 Dashboard Admin
- Métricas gerais
- Usuários ativos
- Campanhas ativas

### 7.2 Gestão de Usuários
- Listagem de doadores
- Listagem de bancos de sangue
- Bloquear/ativar

### 7.3 Gestão de Conteúdo
- FAQ
- Banners de campanhas
- Informações públicas

### 7.4 Relatórios
- Doações por período
- Estoques por região
- Exportação CSV/PDF

---

## 8) Estados e Fluxos Transversais

### 8.1 Estados de sistema
- Loading
- Vazio (sem dados)
- Erro (com opção de retry)

### 8.2 Fluxos críticos
- **Doação concluída**: notificação + atualização de histórico.
- **Alerta de estoque baixo**: banner + push para banco.

---

## 9) Componentes Reutilizáveis

- Card Campanha
- Card Banco de Sangue
- Card Doação
- Badge de status
- Tabela base
- Stepper de cadastro (wizard)

---

## 10) Estrutura de Navegação (Sitemap)

```
/ (Landing)
|-- /sobre
|-- /campanhas
|-- /bancos
|-- /faq
|-- /contato
|-- /login
|-- /cadastro
|   |-- /cadastro/doador
|   |-- /cadastro/banco
|-- /recuperar-senha
|-- /doador
|   |-- /doador/dashboard
|   |-- /doador/perfil
|   |-- /doador/agendar
|   |-- /doador/historico
|   |-- /doador/convites
|   |-- /doador/notificacoes
|   |-- /doador/carteira
|-- /banco
|   |-- /banco/dashboard
|   |-- /banco/agendamentos
|   |-- /banco/estoque
|   |-- /banco/campanhas
|   |-- /banco/convocar
|   |-- /banco/perfil
|-- /admin
|   |-- /admin/dashboard
|   |-- /admin/usuarios
|   |-- /admin/conteudo
|   |-- /admin/relatorios
```

---

## 11) Observações para o protótipo (Figma)

- Criar variantes de **desktop/tablet/mobile** para as telas principais.
- Priorizar fluxo completo do **doador** e do **banco**.
- Criar componentes reutilizáveis e documentar estados (hover, focus, disabled, error).
- Incluir tokens de cor e tipografia na biblioteca.

---

## 12) Próximos passos

1. Aprovar a lista de telas.
2. Definir identidade visual definitiva.
3. Montar biblioteca de componentes.
4. Criar protótipos navegáveis no Figma.

