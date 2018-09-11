CREATE TABLE Sexo(
	IdSexo int identity(1,1) primary key,
	Descripcion varchar(50) not null
);

CREATE TABLE CentroSalud(
	UnidadProgramatica int primary key,
	NombreCentro varchar(100) not null
);

CREATE TABLE Sector(
	CodigoSector int primary key,
	UnidadProgramatica int not null,
	Nombre varchar(50) not null,
	FOREIGN KEY (UnidadProgramatica) REFERENCES CentroSalud(UnidadProgramatica),
);

CREATE TABLE Persona(
	Cedula int primary key,
	Nombre varchar(40) not null,
	ApellidoUno varchar(50) not null,
	ApellidoDos varchar(50) not null,
	FechaNacimiento date not null,
	Telefono int not null,
	Direccion varchar(250) not null,
	IdSexo int not null,
	CodigoSector int not null,
	FOREIGN KEY (IdSexo) REFERENCES Sexo(IdSexo),
	FOREIGN KEY (CodigoSector) REFERENCES Sector(CodigoSector)
);

CREATE TABLE NivelUsuario(
	IdNivel int identity(1,1) primary key,
	Descripcion varchar(50) not null
);

CREATE TABLE Usuario(
	NombreUsuario varchar(20) primary key,
	Contrasenna varchar(20) not null,
	IdNivel int not null,
	Cedula int not null,
	FOREIGN KEY (IdNivel) REFERENCES NivelUsuario(IdNivel),
	FOREIGN KEY (Cedula) REFERENCES Persona(Cedula)
);

CREATE TABLE Especialidad(
	IdEspecialidad int identity(1,1) primary key,
	NombreEspecialidad varchar(50) not null
);

CREATE TABLE Medico(
	CodigoMedico int primary key,
	IdEspecialidad int not null,
	NombreUsuario varchar(20) not null,
	FOREIGN KEY (IdEspecialidad) REFERENCES Especialidad(IdEspecialidad),
	FOREIGN KEY (NombreUsuario) REFERENCES Usuario(NombreUsuario)
);

CREATE TABLE Radiologo(
	CodigoRadiologo int primary key,
	NombreUsuario varchar(20) not null,
	FOREIGN KEY (NombreUsuario) REFERENCES Usuario(NombreUsuario)
);

CREATE TABLE RegionEstudio(
	CodigoRegion int primary key,
	Nombre varchar(50) not null
);

CREATE TABLE TipoConsulta(
	IdTipoConsulta int identity(1,1) primary key,
	NombreConsulta varchar(25) unique not null
);

CREATE TABLE RegistroResultados(
	IdRegistro int identity(1,1) primary key,
	fechaRegistro date not null,
	fechaEstudio date not null,
	Hallazgos varchar(1000) not null,
	Conclusiones varchar(1000) not null,
	CedulaPaciente int not null,
	CodigoMedico int not null,
	CodigoRadiologo int not null,
	IdRegion int not null,
	NombreUsuario varchar(20) not null,
	IdTipoConsulta int not null,
	FOREIGN KEY (CedulaPaciente) REFERENCES Persona(Cedula),
	FOREIGN KEY (CodigoMedico) REFERENCES Medico(CodigoMedico),
	FOREIGN KEY (CodigoRadiologo) REFERENCES Radiologo(CodigoRadiologo),
	FOREIGN KEY (IdRegion) REFERENCES RegionEstudio(CodigoRegion),
	FOREIGN KEY (NombreUsuario) REFERENCES Usuario(NombreUsuario),
	FOREIGN KEY (IdTipoConsulta) REFERENCES TipoConsulta(IdTipoConsulta)
);
