CREATE TABLE Sexo(
	IdSexo int identity(1,1) primary key,
	descripcion varchar(50)
);

CREATE TABLE Persona(
	IdPersona int identity(1,1) primary key,
	Nombre varchar(40),
	ApellidoUno varchar(50),
	ApellidoDos varchar(50),
	Cedula int unique,
	FechaNacimiento date,
	Telefono int,
	Direccion varchar(250),
	IdSexo int,
	FOREIGN KEY (IdSexo) REFERENCES Sexo(IdSexo)
);

CREATE TABLE NivelUsuario(
	IdNivel int identity(1,1) primary key,
	Descripcion varchar(50)
);

CREATE TABLE CentroSalud(
	IdCentro int identity(1,1) primary key,
	UP int unique,
	NombreCentro varchar(100)
);

CREATE TABLE Especialidad(
	IdEspecialidad int identity(1,1) primary key,
	NombreEspecialidad varchar(50)
);

CREATE TABLE TipoConsulta(
	IdTipoConsulta int identity(1,1) primary key,
	NombreConsulta varchar(25) unique
);

CREATE TABLE Medico(
	IdMedico int identity(1,1) primary key,
	CodMedico int unique,
	IdEspecialidad int,
	IdPersona int,
	FOREIGN KEY (IdEspecialidad) REFERENCES Especialidad(IdEspecialidad),
	FOREIGN KEY (IdPersona) REFERENCES Persona(IdPersona)
);

CREATE TABLE Radiologo(
	IdRadiologo int identity(1,1) primary key,
	CodRadiologo int unique,
	IdPersona int,
	FOREIGN KEY (IdPersona) REFERENCES Persona(IdPersona)
);

CREATE TABLE Sector(
	IdSector int identity(1,1) primary key,
	Nombre varchar(50),
	CodSector int unique
);

CREATE TABLE Paciente(
	IdPaciente int identity(1,1) primary key,
	IdCentro int,
	IdSector int,
	IdPersona int,
	FOREIGN KEY (IdCentro) REFERENCES CentroSalud(IdCentro),
	FOREIGN KEY (IdSector) REFERENCES Sector(IdSector),
	FOREIGN KEY (IdPersona) REFERENCES Persona(IdPersona)
);



CREATE TABLE RegionEstudio(
	IdRegion int identity(1,1) primary key,
	CodRegion int unique,
	Nombre varchar(50)
);
