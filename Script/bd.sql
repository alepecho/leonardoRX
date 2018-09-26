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
	Activo bit not null,
	CambioContra bit not null,
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

CREATE TABLE TipoExamen(
	IdTipoExamen int identity(1,1) primary key,
	Descripcion varchar(30)
);

CREATE TABLE RegistroResultados(
	IdRegistro int identity(1,1) primary key,
	fechaRegistro datetime not null,
	fechaEstudio datetime not null,
	Hallazgos varchar(1000) not null,
	Conclusiones varchar(1000) not null,
	CedulaPaciente int not null,
	CodigoMedico int not null,
	CodigoRadiologo int not null,
	IdRegion int not null,
	NombreUsuario varchar(20) not null,
	IdTipoConsulta int not null,
	IdTipoExamen int not null,
	UltimoUsuarioModificar varchar(30),
	FOREIGN KEY (CedulaPaciente) REFERENCES Persona(Cedula),
	FOREIGN KEY (CodigoMedico) REFERENCES Medico(CodigoMedico),
	FOREIGN KEY (CodigoRadiologo) REFERENCES Radiologo(CodigoRadiologo),
	FOREIGN KEY (IdRegion) REFERENCES RegionEstudio(CodigoRegion),
	FOREIGN KEY (NombreUsuario) REFERENCES Usuario(NombreUsuario),
	FOREIGN KEY (IdTipoConsulta) REFERENCES TipoConsulta(IdTipoConsulta),
	FOREIGN KEY (IdTipoExamen) REFERENCES TipoExamen(IdTipoExamen)
);


/*
connection string para local:
data source=LEO-PC\SQL_SERVER_LEO;initial catalog=SIMAMUS;user id=Admin;password=Admin;
*/


/* inserts */
INSERT INTO Sexo values('Masculino')
INSERT INTO Sexo values('Femenino')

INSERT INTO CentroSalud values(10, 'GOICOECHEA 2')

INSERT INTO Sector values(1, 10, 'Pilar Jimenez')

INSERT INTO Persona values(123, 'Pollito', 'Gallo', 'Pinto', '2018/05/05', 88888888, 'En el gallinero de Tibas', 1, 1)

INSERT INTO Persona values(12345, 'Jorge', 'Luis', 'Pinto', '2018/05/05', 88888888, 'En el gallinero de Tibas', 1, 1)

INSERT INTO NivelUsuario values('SuperAdministrador')

INSERT INTO Usuario values('pollito', 'pollo', 1, 123, 1, 0)

INSERT INTO Usuario values('pollita', 'polla', 1, 123, 1, 0)

INSERT INTO Especialidad values('Especial')

INSERT INTO Medico values(1, 1, 'pollita')

Insert INTO Radiologo values(1, 'pollita')

Insert INTO RegionEstudio values(1, 'espalda')

Insert INTO TipoConsulta values('query')

Insert INTO TipoExamen values('Ultrasonido')

Insert INTO TipoExamen values('Mamografia')


INSERT INTO RegistroResultados values('2018-02-27','2018-02-27', 'asdfasdf', 'asdfas', 12345, 1,1,1, 'pollita', 1,1, 'pollita')

INSERT INTO RegistroResultados values('2018-02-27','2018-02-27', 'asdfasdf', 'asdfas', 123, 1,1,1, 'pollita', 1,1, 'pollita')


--fin de inserts

-- procedure de registroresultados


CREATE PROCEDURE pro_ReportesPrueba 
AS
BEGIN

select rr.IdRegistro as 'codigo', rr.fechaRegistro, rr.fechaEstudio, rr.Hallazgos, rr.Conclusiones, 
rr.CedulaPaciente as 'cedula',
pe.Nombre + ' ' + pe.ApellidoUno + ' '  + pe.ApellidoDos as 'Nombre del Paciente',
peMed.Nombre + ' ' + peMed.ApellidoUno + ' '  + peMed.ApellidoDos as 'Nombre del Medico',
peRad.Nombre + ' ' + peRad.ApellidoUno + ' '  + peRad.ApellidoDos as 'Nombre del Radiologo',
peReg.Nombre + ' ' + peReg.ApellidoUno + ' '  + peReg.ApellidoDos as 'Nombre del Funcionario',
tc.NombreConsulta as 'Tipo de la consulta',
te.Descripcion as 'Tipo de Examen'

from RegistroResultados rr inner join

persona pe on pe.Cedula = rr.CedulaPaciente inner join
Medico med on med.CodigoMedico = rr.CodigoMedico inner join
usuario usuMed on usuMed.NombreUsuario = med.NombreUsuario inner join
persona peMed on peMed.Cedula = usuMed.Cedula inner join 
Radiologo rad on rad.CodigoRadiologo = rr.CodigoRadiologo inner join
usuario usuRad on usuRad.NombreUsuario = rad.NombreUsuario inner join
persona peRad on peRad.Cedula = usuRad.Cedula inner join
usuario usuReg on usuReg.NombreUsuario = rr.NombreUsuario inner join
persona peReg on peReg.Cedula = usuReg.Cedula inner join
TipoConsulta tc on tc.IdTipoConsulta = rr.IdTipoConsulta inner join
TipoExamen te on te.IdTipoExamen = rr.IdTipoExamen

END
