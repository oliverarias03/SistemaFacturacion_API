Create database Facturacion
GO
USE Facturacion
GO
--ESTADO = Activo / Inactivo
Create Table Articulos(
Id int not null identity(1,1) primary key,
Descripcion varchar(100),
Precio decimal(18,2),
Estado varchar(10)
)
GO
Create Table Clientes(
Id int not null identity(1,1) primary key,
Nombre varchar(100),
Rnc varchar(15),
CuentaContable varchar(20),
Estado varchar(10)
)
GO
Create Table Vendedores(
Id int not null identity(1,1) primary key,
Nombre varchar(100),
Cedula varchar(15),
Clave varchar(20),
Comision decimal(18,2),
Estado varchar(10)
)
GO
Create Table Facturacion(
Id int not null identity(1,1) primary key,
IdVendedor int foreign key references Vendedores(Id),
IdCliente int foreign key references Clientes(Id),
IdArticulo int foreign key references Articulos(Id),
Fecha datetime,
Comentario varchar(100),
Cantidad int,
PrecioUnitario decimal(18,2)
)
GO
--Tipo Moviemiento DB / CR
Create Table AsientosContables(
Id int not null identity(1,1) primary key,
Descripcion varchar(100),
IdCliente int foreign key references Clientes(Id),
Cuenta varchar(20),
TipoMovimiento varchar(2),
FechaAsiento datetime,
MontoAsiento decimal(18,2),
Estado varchar(10)
)
GO