create database Dynamicore

create table Usuarios(
id int identity(1,1) primary key,
nombre varchar(50),
primerApellido varchar(50),
segundoApellido varchar(50),
correo varchar (50),
curp varchar (18)

)


create table Contactos(
id int identity(1,1) primary  key,
nombre varchar(50),
primerApellido varchar(50),
segundoApellido varchar(50),
correo varchar (50),
curp varchar (18),
fecha date,
descripcion varchar(100)
)

create table Directorio(

id int identity(1,1) primary key,
idUsuario int foreign key references Usuarios,
idContacto int foreign key references Contactos

)

drop table Directorio

create table Acceso(

id int identity(1,1) primary key,
nombre varchar (50),
correo varchar(100),
contrasena varchar (100)

)

drop table Acceso



select * from Usuarios





