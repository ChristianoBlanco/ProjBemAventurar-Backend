CREATE TABLE Usuarios (
    ClienteID INT PRIMARY KEY IDENTITY(1,1),
    Nome NVARCHAR(150) NOT NULL,
    Email NVARCHAR(150) NOT NULL,
    Senha NVARCHAR(150) NOT NULL,
    CriadoEm DATETIME DEFAULT GETDATE()
);

CREATE TABLE Permissoes (
    PermissaoID INT PRIMARY KEY IDENTITY(1,1),
    ClienteID INT NOT NULL,
    Nome_modulo NVARCHAR(100) NOT NULL,
    CriadoEm DATETIME DEFAULT GETDATE()
    
    FOREIGN KEY (ClienteID) REFERENCES Usuarios(ClienteID) 
);

CREATE TABLE Logs (
    LogID INT PRIMARY KEY IDENTITY(1,1),
    ClienteID INT NOT NULL,
    Nome_log NVARCHAR(150) NOT NULL,
    Data_log DATE NOT NULL,
    CriadoEm DATETIME DEFAULT GETDATE()
    
    FOREIGN KEY (ClienteID) REFERENCES Usuarios(ClienteID) 
);

CREATE TABLE Eventos (
    EventoID INT PRIMARY KEY IDENTITY(1,1),
    Nome_evento NVARCHAR(150) NOT NULL,
    Desc_evento NVARCHAR(MAX) NOT NULL,
    Sobre_evento NVARCHAR(MAX) NOT NULL,
    Local_evento NVARCHAR(150) NOT NULL,
    Material_evento NVARCHAR(150) NOT NULL,
    Preco_evento DECIMAL(10, 2) NOT NULL,
    Data_evento DATE NOT NULL,
    CriadoEm DATETIME DEFAULT GETDATE()
    
);

CREATE TABLE Evento_itinerarios (
    ItinerarioID INT PRIMARY KEY IDENTITY(1,1),
    EventoID INT NOT NULL,
    Itinerario NVARCHAR(150) NOT NULL,
    CriadoEm DATETIME DEFAULT GETDATE()
    
    FOREIGN KEY (EventoID) REFERENCES Eventos(EventoID) 
);

CREATE TABLE Evento_faqs (
    FaqID INT PRIMARY KEY IDENTITY(1,1),
    EventoID INT NOT NULL,
    Pergunta_faq NVARCHAR(150) NULL,
    Resposta_faq NVARCHAR(150) NULL,
    CriadoEm DATETIME DEFAULT GETDATE()
    
    FOREIGN KEY (EventoID) REFERENCES Eventos(EventoID) 
);

CREATE TABLE Evento_fotos (
    FotoID INT PRIMARY KEY IDENTITY(1,1),
    EventoID INT NOT NULL,
    nome_foto NVARCHAR(150) NOT NULL,
    Link_foto NVARCHAR(150) NOT NULL,
    CriadoEm DATETIME DEFAULT GETDATE()
    
    FOREIGN KEY (EventoID) REFERENCES Eventos(EventoID) 
);

CREATE TABLE Evento_videos (
    VideoID INT PRIMARY KEY IDENTITY(1,1),
    EventoID INT NOT NULL,
    nome_video NVARCHAR(150) NOT NULL,
    Link_video NVARCHAR(150) NOT NULL,
    CriadoEm DATETIME DEFAULT GETDATE()
    
    FOREIGN KEY (EventoID) REFERENCES Eventos(EventoID) 
);

CREATE TABLE Inscricoes (
    InscricaoID INT PRIMARY KEY IDENTITY(1,1),
    EventoID INT NOT NULL,
    Cliente NVARCHAR(150) NOT NULL,
    Cpf NVARCHAR(11) NOT NULL,
    Tel NVARCHAR(11) NOT NULL,
    Telzap NVARCHAR(11) NOT NULL,
    Email NVARCHAR(11) NOT NULL,
    Data_nasc DATE NOT NULL,
    Num_pessoas NVARCHAR(150) NOT NULL,
    Obs NVARCHAR(150) NULL,
    Data_insc DATE NOT NULL,
    Status NVARCHAR(3) NULL,
    CriadoEm DATETIME DEFAULT GETDATE()
    
    FOREIGN KEY (EventoID) REFERENCES Eventos(EventoID) 
);




