CREATE TABLE Edades
(
	id int not null primary key identity(1,1),
	edad int not null,
	Descripcion varchar(20)not null,
    fechaCreacion datetime not null default GETDATE(),
	fechaActualizacion datetime null default GETDATE(),
    estado bit not null default 1
)

ALTER TABLE Personas ADD  CONSTRAINT [UQ_PersonasIdentida] UNIQUE NONCLUSTERED(identificacion)
ALTER TABLE Personas  WITH noCHECK ADD  CONSTRAINT [FK_Personas] FOREIGN KEY(idCiudad)REFERENCES Ciudades (id)
--ALTER TABLE Personas  WITH noCHECK ADD  CONSTRAINT [FK_edad] FOREIGN KEY(edad)REFERENCES Edades (id)

ALTER TABLE Salones  WITH noCHECK ADD  CONSTRAINT [FK_Salones] FOREIGN KEY(idPersonaCliente)REFERENCES Personas (id)


CREATE TABLE Motivos
(
	id int not null primary key identity(1,1),
	edad int not null,
	motivo nvarchar(60)not null,
    fechaCreacion datetime not null default GETDATE(),
	fechaActualizacion datetime null,
    estado bit not null default 1
)


ALTER TABLE Salones  WITH noCHECK ADD  CONSTRAINT [FK_Motivos] FOREIGN KEY(idMotivo)REFERENCES Motivos (id)