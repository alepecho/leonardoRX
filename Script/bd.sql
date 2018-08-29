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


CREATE TABLE Usuario(
	IdUsuario int identity(1,1) primary key,
	NombreUsuario varchar(20) unique,
	Contrasenna varchar(20),
	IdNivel int,
	FOREIGN KEY (IdNivel) REFERENCES NivelUsuario(IdNivel)
);

CREATE TABLE RegistroResultados(
	IdRegistro int identity(1,1) primary key,
	fechaRegistro date,
	fechaEstudio date,
	Hallazgos varchar(1000),
	Conclusiones varchar(1000),
	IdPersona int,
	IdMedico int,
	IdRadiologo int,
	IdRegion int,
	IdSector int,
	IdCentro int,
	IdUsuario int,
	IdTipoConsulta int,
	FOREIGN KEY (IdPersona) REFERENCES Persona(IdPersona),
	FOREIGN KEY (IdMedico) REFERENCES Medico(IdMedico),
	FOREIGN KEY (IdRadiologo) REFERENCES Radiologo(IdRadiologo),
	FOREIGN KEY (IdRegion) REFERENCES RegionEstudio(IdRegion),
	FOREIGN KEY (IdSector) REFERENCES Sector(IdSector),
	FOREIGN KEY (IdCentro) REFERENCES CentroSalud(IdCentro),
	FOREIGN KEY (IdUsuario) REFERENCES Usuario(IdUsuario),
	FOREIGN KEY (IdTipoConsulta) REFERENCES TipoConsulta(IdTipoConsulta)
);
