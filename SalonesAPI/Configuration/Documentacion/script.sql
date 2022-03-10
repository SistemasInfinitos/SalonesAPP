--drop table Personas
CREATE TABLE Personas
(
	id int not null primary key identity(1,1),
	identificacion varchar(15) not null,
	primerNombre varchar(100) not null,
    segundoNombre  varchar(100) not null,
    primerApellido varchar(100) not null,
    segundoApellido varchar(100) not null,
    telefono varchar(10) not null,
    correo nvarchar(100) not null,
    --idDepartamento int not null,
    idCiudad int not null,
    edad int not null,
    fechaCreacion datetime not null default GETDATE(),
	fechaActualizacion datetime null default GETDATE(),
    estado bit not null default 1
)
--drop table Salones
CREATE TABLE Salones
(
  id int not null primary key identity(1,1),
  idPersonaCliente int not null,
  fechaEvento datetime not null default GETDATE(),
  cantidadPersona int not null,
  idMotivo int not null,
  observacion nvarchar(max)  not null,
  estado bit not null default 1
)


CREATE TABLE [dbo].[Paises](
	[paisCodigo] [char](3) NOT NULL CONSTRAINT [DF__Pais__PaisCodigo__00200768]  DEFAULT (''),
	[paisNombre] [char](52) NOT NULL CONSTRAINT [DF__Pais__PaisNombre__01142BA1]  DEFAULT (''),
	[paisContinente] [varchar](50) NOT NULL CONSTRAINT [DF__Pais__PaisContin__02084FDA]  DEFAULT ('America del Sur'),
	[id] [int] NOT NULL,
	[codigoDian] [int] NULL,
 CONSTRAINT [PK_Pais] PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UK_Pais] UNIQUE NONCLUSTERED 
(
	[PaisCodigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


CREATE TABLE [dbo].[Departamentos](
	[Id] [int] NOT NULL,
	[IdPais] [int] NOT NULL,
	[DistritoDepartamento] [varchar](50) NULL,
	[CodigoDian] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]



ALTER TABLE [dbo].[Departamentos]  WITH CHECK ADD  CONSTRAINT [DepartamentosPais] FOREIGN KEY([IdPais])
REFERENCES [dbo].[Paises] ([Id])
GO

ALTER TABLE [dbo].[Departamentos] CHECK CONSTRAINT [DepartamentosPais]
GO


CREATE TABLE [dbo].[Ciudades](
	[id] [int] NOT NULL,
	[ciudadNombre] [char](35) NOT NULL DEFAULT (''),
	[idDepartamento] [int] NULL,
	[codigoDian] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Ciudades]  WITH CHECK ADD  CONSTRAINT [CiudadesDepartamentos] FOREIGN KEY([IdDepartamento])
REFERENCES [dbo].[Departamentos] ([Id])
GO

ALTER TABLE [dbo].[Ciudades] CHECK CONSTRAINT [CiudadesDepartamentos]
GO

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
--drop view ViewSolicitudesPorFecha
CREATE VIEW  ViewSolicitudesPorFecha as
select p.id
,s.fechaEvento
,fechaEventoTex =CONVERT(varchar,FORMAT(s.fechaEvento, 'yyyy/MM/dd HH:mm','en-US'))
,s.estado
,p.primerNombre
,p.segundoNombre
,p.primerApellido
,p.segundoApellido
,p.correo
,p.edad
,p.identificacion
,p.telefono
,s.cantidadPersona
,s.observacion
,m.motivo
,c.ciudadNombre
,d.DistritoDepartamento
,pp.paisNombre
from Salones s
join Personas p on p.id=s.idPersonaCliente
join Ciudades c on c.id =p.idCiudad
join Departamentos d on d.Id=c.idDepartamento
join Paises pp on pp.id=d.IdPais
join Motivos m on m.id=s.idMotivo