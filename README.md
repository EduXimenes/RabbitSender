# 🐇 RabbitSender

RabbitSender é uma aplicação full stack para envio simulado de e-mails em massa, utilizando .NET, RabbitMQ, PostgreSQL, Docker e React.

O sistema é composto por:
- Backend em ASP.NET Core
- Worker .NET para consumo de mensagens da fila
- Frontend em React + Tailwind
- Fila com RabbitMQ
- Banco de dados PostgreSQL

---

## 🚀 Tecnologias

- ASP.NET Core 8 (Web API)
- RabbitMQ (mensageria)
- PostgreSQL (persistência)
- React + Tailwind CSS (frontend)
- Docker & Docker Compose

---

## 📁 Estrutura do Projeto

```bash
RabbitSender/
│
├── RabbitSender.API/           # Backend (Web API)
├── RabbitSender.Worker/        # Worker que processa os envios
├── Dockerfile.api              # Dockerfile para o backend
├── Dockerfile.worker           # Dockerfile para o worker
├── Dockerfile.frontend         # Dockerfile para o frontend
├── docker-compose.yml
├── README.md
├── RabbitSenderUi/
   └── rabbit-sender/          # Frontend React
```

## ⚙️ Requisitos
- Docker e Docker Compose
- Git

## 📦 Executando com Docker Compose
1. Clone o repositório
```bash
git clone https://github.com/seuusuario/RabbitSender.git
cd RabbitSender
```
2. Suba os containers
```bash
docker-compose up --build
```

Acesse: <br>

Frontend: http://localhost:3000 <br>
Backend: http://localhost:5206/swagger <br>
RabbitMQ: http://localhost:15672 (login: admin, senha: admin) <br>

## 🧪 Teste Rápido
Acesse o frontend em http://localhost:3000 <br>
Preencha os campos: Título e Corpo do email <br>
Clique em "Enviar" — isso publicará a mensagem no RabbitMQ <br>
O worker processará e armazenará a simulação no banco de dados <br>

## 🔄 Comunicação entre serviços
O frontend envia requisições via HTTP para o backend (.NET) <br>
O backend publica mensagens na fila email-queue no RabbitMQ <br>
O worker consome da fila e "envia" (simula) o email, salvando no PostgreSQL <br>

## 🧠 Futuras melhorias
- Integração real com servidor SMTP
- Filtros e paginação no histórico
- Autenticação JWT
- Dashboard com analytics


