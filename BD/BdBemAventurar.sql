-- --------------------------------------------------------
-- Servidor:                     BLANCOMASTER\SQLEXPRESS
-- Versão do servidor:           Microsoft SQL Server 2022 (RTM) - 16.0.1000.6
-- OS do Servidor:               Windows 10 Pro 10.0 <X64> (Build 26100: ) (Hypervisor)
-- HeidiSQL Versão:              12.8.0.6908
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES  */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;


-- Copiando estrutura do banco de dados para BemAventurar
CREATE DATABASE IF NOT EXISTS "BemAventurar";
USE "BemAventurar";

-- Copiando estrutura para tabela BemAventurar.Eventos
CREATE TABLE IF NOT EXISTS "Eventos" (
	"EventoID" INT NOT NULL,
	"Nome_evento" NVARCHAR(150) NOT NULL COLLATE 'Latin1_General_CI_AS',
	"Desc_evento" NVARCHAR(max) NOT NULL COLLATE 'Latin1_General_CI_AS',
	"Sobre_evento" NVARCHAR(max) NOT NULL COLLATE 'Latin1_General_CI_AS',
	"Local_evento" NVARCHAR(150) NOT NULL COLLATE 'Latin1_General_CI_AS',
	"Material_evento" NVARCHAR(150) NOT NULL COLLATE 'Latin1_General_CI_AS',
	"Preco_evento" DECIMAL(10,2) NOT NULL,
	"Data_evento" DATE NOT NULL,
	"CriadoEm" DATETIME NULL DEFAULT 'getdate()',
	PRIMARY KEY ("EventoID")
);

-- Copiando dados para a tabela BemAventurar.Eventos: 0 rows
DELETE FROM "Eventos";
/*!40000 ALTER TABLE "Eventos" DISABLE KEYS */;
/*!40000 ALTER TABLE "Eventos" ENABLE KEYS */;

-- Copiando estrutura para tabela BemAventurar.Evento_faqs
CREATE TABLE IF NOT EXISTS "Evento_faqs" (
	"FaqID" INT NOT NULL,
	"EventoID" INT NOT NULL,
	"Pergunta_faq" NVARCHAR(150) NULL DEFAULT NULL COLLATE 'Latin1_General_CI_AS',
	"Resposta_faq" NVARCHAR(150) NULL DEFAULT NULL COLLATE 'Latin1_General_CI_AS',
	"CriadoEm" DATETIME NULL DEFAULT 'getdate()',
	FOREIGN KEY INDEX "FK__Evento_fa__Criad__5BE2A6F2" ("EventoID"),
	PRIMARY KEY ("FaqID"),
	CONSTRAINT "FK__Evento_fa__Criad__5BE2A6F2" FOREIGN KEY ("EventoID") REFERENCES "Eventos" ("EventoID") ON UPDATE NO_ACTION ON DELETE NO_ACTION
);

-- Copiando dados para a tabela BemAventurar.Evento_faqs: 0 rows
DELETE FROM "Evento_faqs";
/*!40000 ALTER TABLE "Evento_faqs" DISABLE KEYS */;
/*!40000 ALTER TABLE "Evento_faqs" ENABLE KEYS */;

-- Copiando estrutura para tabela BemAventurar.Evento_fotos
CREATE TABLE IF NOT EXISTS "Evento_fotos" (
	"FotoID" INT NOT NULL,
	"EventoID" INT NOT NULL,
	"nome_foto" NVARCHAR(150) NOT NULL COLLATE 'Latin1_General_CI_AS',
	"Link_foto" NVARCHAR(150) NOT NULL COLLATE 'Latin1_General_CI_AS',
	"CriadoEm" DATETIME NULL DEFAULT 'getdate()',
	FOREIGN KEY INDEX "FK__Evento_fo__Criad__5FB337D6" ("EventoID"),
	PRIMARY KEY ("FotoID"),
	CONSTRAINT "FK__Evento_fo__Criad__5FB337D6" FOREIGN KEY ("EventoID") REFERENCES "Eventos" ("EventoID") ON UPDATE NO_ACTION ON DELETE NO_ACTION
);

-- Copiando dados para a tabela BemAventurar.Evento_fotos: 0 rows
DELETE FROM "Evento_fotos";
/*!40000 ALTER TABLE "Evento_fotos" DISABLE KEYS */;
/*!40000 ALTER TABLE "Evento_fotos" ENABLE KEYS */;

-- Copiando estrutura para tabela BemAventurar.Evento_itinerarios
CREATE TABLE IF NOT EXISTS "Evento_itinerarios" (
	"ItinerarioID" INT NOT NULL,
	"EventoID" INT NOT NULL,
	"Itinerario" NVARCHAR(150) NOT NULL COLLATE 'Latin1_General_CI_AS',
	"CriadoEm" DATETIME NULL DEFAULT 'getdate()',
	FOREIGN KEY INDEX "FK__Evento_it__Criad__5812160E" ("EventoID"),
	PRIMARY KEY ("ItinerarioID"),
	CONSTRAINT "FK__Evento_it__Criad__5812160E" FOREIGN KEY ("EventoID") REFERENCES "Eventos" ("EventoID") ON UPDATE NO_ACTION ON DELETE NO_ACTION
);

-- Copiando dados para a tabela BemAventurar.Evento_itinerarios: 0 rows
DELETE FROM "Evento_itinerarios";
/*!40000 ALTER TABLE "Evento_itinerarios" DISABLE KEYS */;
/*!40000 ALTER TABLE "Evento_itinerarios" ENABLE KEYS */;

-- Copiando estrutura para tabela BemAventurar.Inscricoes
CREATE TABLE IF NOT EXISTS "Inscricoes" (
	"InscricaoID" INT NOT NULL,
	"EventoID" INT NOT NULL,
	"Cliente" NVARCHAR(150) NOT NULL COLLATE 'Latin1_General_CI_AS',
	"Cpf" NVARCHAR(11) NOT NULL COLLATE 'Latin1_General_CI_AS',
	"Tel" NVARCHAR(11) NOT NULL COLLATE 'Latin1_General_CI_AS',
	"Telzap" NVARCHAR(11) NOT NULL COLLATE 'Latin1_General_CI_AS',
	"Email" NVARCHAR(11) NOT NULL COLLATE 'Latin1_General_CI_AS',
	"Data_nasc" DATE NOT NULL,
	"Num_pessoas" NVARCHAR(150) NOT NULL COLLATE 'Latin1_General_CI_AS',
	"Obs" NVARCHAR(150) NULL DEFAULT NULL COLLATE 'Latin1_General_CI_AS',
	"Data_insc" DATE NOT NULL,
	"Status" NVARCHAR(3) NULL DEFAULT NULL COLLATE 'Latin1_General_CI_AS',
	"CriadoEm" DATETIME NULL DEFAULT 'getdate()',
	FOREIGN KEY INDEX "FK__Inscricoe__Criad__6383C8BA" ("EventoID"),
	PRIMARY KEY ("InscricaoID"),
	CONSTRAINT "FK__Inscricoe__Criad__6383C8BA" FOREIGN KEY ("EventoID") REFERENCES "Eventos" ("EventoID") ON UPDATE NO_ACTION ON DELETE NO_ACTION
);

-- Copiando dados para a tabela BemAventurar.Inscricoes: 0 rows
DELETE FROM "Inscricoes";
/*!40000 ALTER TABLE "Inscricoes" DISABLE KEYS */;
/*!40000 ALTER TABLE "Inscricoes" ENABLE KEYS */;

-- Copiando estrutura para tabela BemAventurar.Logs
CREATE TABLE IF NOT EXISTS "Logs" (
	"LogID" INT NOT NULL,
	"ClienteID" INT NOT NULL,
	"Nome_log" NVARCHAR(150) NOT NULL COLLATE 'Latin1_General_CI_AS',
	"Data_log" DATE NOT NULL,
	"CriadoEm" DATETIME NULL DEFAULT 'getdate()',
	FOREIGN KEY INDEX "FK__Logs__CriadoEm__5165187F" ("ClienteID"),
	PRIMARY KEY ("LogID"),
	CONSTRAINT "FK__Logs__CriadoEm__5165187F" FOREIGN KEY ("ClienteID") REFERENCES "Usuarios" ("ClienteID") ON UPDATE NO_ACTION ON DELETE NO_ACTION
);

-- Copiando dados para a tabela BemAventurar.Logs: 0 rows
DELETE FROM "Logs";
/*!40000 ALTER TABLE "Logs" DISABLE KEYS */;
/*!40000 ALTER TABLE "Logs" ENABLE KEYS */;

-- Copiando estrutura para tabela BemAventurar.Modulos
CREATE TABLE IF NOT EXISTS "Modulos" (
	"ModuloID" INT NOT NULL,
	"Nome_Modulo" VARCHAR(100) NOT NULL COLLATE 'Latin1_General_CI_AS',
	"Link_Modulo" VARCHAR(100) NOT NULL COLLATE 'Latin1_General_CI_AS',
	"CriadoEm" DATETIME NULL DEFAULT 'getdate()',
	PRIMARY KEY ("ModuloID")
);

-- Copiando dados para a tabela BemAventurar.Modulos: 0 rows
DELETE FROM "Modulos";
/*!40000 ALTER TABLE "Modulos" DISABLE KEYS */;
/*!40000 ALTER TABLE "Modulos" ENABLE KEYS */;

-- Copiando estrutura para tabela BemAventurar.Permissoes
CREATE TABLE IF NOT EXISTS "Permissoes" (
	"PermissaoID" INT NOT NULL,
	"ClienteID" INT NOT NULL,
	"ModuloID" INT NOT NULL,
	"Permitir" INT NOT NULL,
	"CriadoEm" DATETIME NULL DEFAULT 'getdate()',
	FOREIGN KEY INDEX "FK_Modulos" ("ModuloID"),
	FOREIGN KEY INDEX "FK_Permissoes" ("ClienteID"),
	PRIMARY KEY ("PermissaoID"),
	CONSTRAINT "FK_Permissoes" FOREIGN KEY ("ClienteID") REFERENCES "Usuarios" ("ClienteID") ON UPDATE NO_ACTION ON DELETE NO_ACTION,
	CONSTRAINT "FK_Modulos" FOREIGN KEY ("ModuloID") REFERENCES "Modulos" ("ModuloID") ON UPDATE NO_ACTION ON DELETE NO_ACTION
);

-- Copiando dados para a tabela BemAventurar.Permissoes: 0 rows
DELETE FROM "Permissoes";
/*!40000 ALTER TABLE "Permissoes" DISABLE KEYS */;
/*!40000 ALTER TABLE "Permissoes" ENABLE KEYS */;

-- Copiando estrutura para tabela BemAventurar.Usuarios
CREATE TABLE IF NOT EXISTS "Usuarios" (
	"ClienteID" INT NOT NULL,
	"Nome" NVARCHAR(150) NOT NULL COLLATE 'Latin1_General_CI_AS',
	"Email" NVARCHAR(150) NOT NULL COLLATE 'Latin1_General_CI_AS',
	"Senha" NVARCHAR(150) NOT NULL COLLATE 'Latin1_General_CI_AS',
	"CriadoEm" DATETIME NULL DEFAULT 'getdate()',
	PRIMARY KEY ("ClienteID")
);

-- Copiando dados para a tabela BemAventurar.Usuarios: 2 rows
DELETE FROM "Usuarios";
/*!40000 ALTER TABLE "Usuarios" DISABLE KEYS */;
INSERT INTO "Usuarios" ("ClienteID", "Nome", "Email", "Senha", "CriadoEm") VALUES
	(1, 'admin', 'christiano.blanco@hotmail.com', '$2a$11$dJr7rtX6pTqW.SKpEQWs7u5YPr27JH9kqPVaCdFOFA.5ZdhbxTE8W', '2025-07-18 19:50:03.880'),
	(2, 'Marley Blanco Silva', 'Beagle.Marley@terra.com', '$2a$11$yGh8Hhnaql.nyP02OXyFMOXZAgooGqFQwZNx7g4.X38KgisvrqynK', '2025-07-23 00:13:23.533');
/*!40000 ALTER TABLE "Usuarios" ENABLE KEYS */;

/*!40103 SET TIME_ZONE=IFNULL(@OLD_TIME_ZONE, 'system') */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
