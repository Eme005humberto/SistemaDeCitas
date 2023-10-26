--Creamos la base de datos del Sistema de Citas Clinicas
Create database Sistema_Citas

Use Sistema_Citas

--Creamos la tabla Citas
Create table TblCitas(
IdCita Int Primary Key Identity(1,1),
Descripcion Varchar(200)not null
)

--Creamos la tabla Paciente
Create table TblPacientes(
IdPaciente Int Primary Key Identity(1,1),
NombreCompleto Varchar(100)not null,
Telefono Varchar(10)not null,
FechaCita date not null,
IdCita Int FOREIGN KEY REFERENCES TblCitas(IdCita)not null
)

--Ingresamos unos datos
Insert into TblCitas(Descripcion)Values('Control de Migraña')
Insert into TblPacientes(NombreCompleto,Telefono,FechaCita,IdCita)Values('Emerson Humberto Carpaño Granados','7503-0232',GETDATE(),1)
drop table TblCitas

Select * from TblPacientes
Select * from TblCitas