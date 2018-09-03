CREATE TABLE Sexo(
	IdSexo int identity(1,1) primary key,
	Descripcion varchar(50) not null
);

CREATE TABLE CentroSalud(
	IdCentro int identity(1,1) primary key,
	UP int unique not null,
	NombreCentro varchar(100) not null
);

CREATE TABLE Sector(
	IdSector int identity(1,1) primary key,
	Nombre varchar(50) not null,
	CodSector int unique not null
);

CREATE TABLE Persona(
	IdPersona int identity(1,1) primary key,
	Nombre varchar(40) not null,
	ApellidoUno varchar(50) not null,
	ApellidoDos varchar(50) not null,
	Cedula int unique not null,
	FechaNacimiento date not null,
	Telefono int not null,
	Direccion varchar(250) not null,
	IdSexo int not null,
	IdCentro int not null,
	IdSector int not null,
	FOREIGN KEY (IdSexo) REFERENCES Sexo(IdSexo),
	FOREIGN KEY (IdCentro) REFERENCES CentroSalud(IdCentro),
	FOREIGN KEY (IdSector) REFERENCES Sector(IdSector)
);

CREATE TABLE NivelUsuario(
	IdNivel int identity(1,1) primary key,
	Descripcion varchar(50) not null
);

CREATE TABLE Especialidad(
	IdEspecialidad int identity(1,1) primary key,
	NombreEspecialidad varchar(50) not null
);

CREATE TABLE TipoConsulta(
	IdTipoConsulta int identity(1,1) primary key,
	NombreConsulta varchar(25) unique not null
);

CREATE TABLE Usuario(
	IdUsuario int identity(1,1) primary key,
	NombreUsuario varchar(20) unique  not null,
	Contrasenna varchar(20) not null,
	IdNivel int not null,
	IdPersona int not null,
	FOREIGN KEY (IdNivel) REFERENCES NivelUsuario(IdNivel),
	FOREIGN KEY (IdPersona) REFERENCES Persona(IdPersona)
);

CREATE TABLE Medico(
	IdMedico int identity(1,1) primary key,
	CodMedico int unique not null,
	IdEspecialidad int not null,
	IdUsuario int not null,
	FOREIGN KEY (IdEspecialidad) REFERENCES Especialidad(IdEspecialidad),
	FOREIGN KEY (IdUsuario) REFERENCES Usuario(IdUsuario)
);

CREATE TABLE Radiologo(
	IdRadiologo int identity(1,1) primary key,
	CodRadiologo int unique not null,
	IdUsuario int not null,
	FOREIGN KEY (IdUsuario) REFERENCES Usuario(IdUsuario)
);

CREATE TABLE RegionEstudio(
	IdRegion int identity(1,1) primary key,
	CodRegion int unique not null,
	Nombre varchar(50) not null
);

CREATE TABLE RegistroResultados(
	IdRegistro int identity(1,1) primary key,
	fechaRegistro date not null,
	fechaEstudio date not null,
	Hallazgos varchar(1000) not null,
	Conclusiones varchar(1000) not null,
	IdPersona int not null,
	IdMedico int not null,
	IdRadiologo int not null,
	IdRegion int not null,
	IdUsuario int not null,
	IdTipoConsulta int not null,
	FOREIGN KEY (IdPersona) REFERENCES Persona(IdPersona),
	FOREIGN KEY (IdMedico) REFERENCES Medico(IdMedico),
	FOREIGN KEY (IdRadiologo) REFERENCES Radiologo(IdRadiologo),
	FOREIGN KEY (IdRegion) REFERENCES RegionEstudio(IdRegion),
	FOREIGN KEY (IdUsuario) REFERENCES Usuario(IdUsuario),
	FOREIGN KEY (IdTipoConsulta) REFERENCES TipoConsulta(IdTipoConsulta)
);

ALTER AUTHORIZATION ON DATABASE::SIMAMUS TO [LEO-PC\lbren];
