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