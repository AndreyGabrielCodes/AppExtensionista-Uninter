# 🍎 App Extensionista - Controle de Alimentos

Sistema de gerenciamento de estoque e controle de vencimento de alimentos desenvolvido em **C# / .NET 8** com interface moderna em **Blazor Hybrid (Photino)** e banco de dados **SQLite**. Desenvolvido como projeto de atividade extensionista para a UNINTER, por Andrey Moraes, RU 4731589.

---

## 🚀 O que o Sistema Faz?

O **App Extensionista** foi criado para otimizar o controle de mantimentos em residências, ajudando a combater o desperdício de alimentos através de funcionalidades inteligentes:

* **Gestão de Estoque:** Permite cadastrar alimentos, controlar quantidades e registrar múltiplas datas de validade para o mesmo item.
* **Alertas de Vencimento:** Identifica visualmente e sinaliza produtos próximos da data de vencimento ou já vencidos.
* **Guia de Armazenamento Integrado:** Permite que o usuário crie guias/dicas de conservação, os quais aparecerão diretamente nos próximos cadastros do mesmo produto.
* **Baixa por Consumo e Descarte:** Facilita a atualização rápida do inventário à medida que os itens são consumidos ou descartados.

---

## 📦 Como Instalar (Para Usuários)

Se você recebeu apenas o instalador do aplicativo, o processo é extremamente simples e não exige conhecimentos técnicos:

1. Baixe o arquivo **`AppExtensionista_Setup.exe`**, localizado dentro da pasta **`AppExtensionista.Instalador`** > **`Output`**.
2. Dê um duplo clique no arquivo baixado.
3. Se o Windows exibir um aviso de proteção (*"O Windows protegeu o seu computador"*), clique em **Mais informações** e depois em **Executar assim mesmo**.
4. Siga as etapas na tela do assistente de instalação, certifique-se de marcar a opção para criar ícone na área de trabalho e clique em **Avançar** até concluir.
5. Um ícone de Atalho será criado automaticamente na sua **Área de Trabalho**. Basta abri-lo e começar a usar!

---

## 🛠️ Guia para Desenvolvedores (Como Compilar e Gerar o Instalador)

Se você deseja clonar o repositório e compilar o código-fonte ou gerar o seu próprio instalador do zero, siga os passos abaixo:

### Pré-requisitos
* [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) instalado na máquina.
* [Inno Setup](https://jrsoftware.org/isdl.php) (utilizado para gerar o pacote de instalação).

### 1. Clonar o Repositório
```bash
git clone https://github.com/AndreyGabrielCodes/AppExtensionista-Uninter.git
cd AppExtensionista-Uninter/AppExtensionista
```

### 2. Abrir e Limpar a Solução no Visual Studio 2022
1. Abra a pasta do projeto ou o arquivo da solução diretamente no Visual Studio 2022.
2. No menu superior, vá em **Build > Clean Solution (Limpar Solução)** para expurgar quaisquer caches ou arquivos residuais de compilações passadas.
3. Em seguida, clique em **Build > Build Solution** para compilar a estrutura base.

### 3. Publicar via Developer PowerShell
1. Ainda no Visual Studio 2022, abra o terminal integrado Developer PowerShell (vá no menu superior em **Tools > Command Line > Developer PowerShell**).
2. Na linha de comando, certifique-se de estar apontando para o diretório do projeto e execute o comando de publicação otimizada:

```powershell
dotnet publish -c Release -r win-x64
```

Os binários otimizados de publicação estarão disponíveis na pasta: `bin\Release\net8.0\win-x64\publish`.

### 4. Gerar o Instalador com Inno Setup
1. Abra o software **Inno Setup**.
2. Abra o script `.iss` localizado dentro da pasta `instalador` do repositório.
3. Confirme se a diretiva `#define SourcePath` aponta corretamente para a pasta `publish` gerada no passo anterior.
4. Clique no botão de **Compile** (ícone de Play verde) para gerar o `AppExtensionista_Setup.exe` pronto para distribuição.

---

## 👨‍💻 Autor

* **Nome:** Andrey Gabriel de Andrade Moraes
* **RU:** 4731589
* **Instituição:** UNINTER — Escola Superior Politécnica
