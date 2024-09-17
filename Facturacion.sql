--CREATE DATABASE FACTURACION

--USE FACTURACION
-------------------------------------------------
CREATE TABLE ARTICULOS(
  ID int identity(1,1),
  nombre varchar(50) NOT NULL,
  precioUnitario decimal(9,3) NOT NULL
  CONSTRAINT pk_Articulos primary key (ID)
);
-------------------------------------------------
CREATE TABLE FormasPago(
  ID int identity(1,1),
  nombre varchar(50) NOT NULL
  CONSTRAINT pk_Formas primary key (ID)
);
-------------------------------------------------
CREATE TABLE Facturas(
  nroFactura int identity(1,1),
  fecha date NOT NULL,
  formaPago int NOT NULL,
  cliente varchar(100) NOT NULL
  CONSTRAINT pk_Facturas primary key (nroFactura),
  CONSTRAINT fk_Facturas_FormasP foreign key (formaPago)
    REFERENCES FormasPago (ID)
);
-------------------------------------------------
CREATE TABLE DetallesFactura(
  ID int identity(1,1),
  nroFactura int NOT NULL,
  articulo int NOT NULL,
  cantidad int NOT NULL
  CONSTRAINT pk_DetallesF primary key (ID),
  CONSTRAINT fk_DetallesF_Facturas foreign key (nroFactura)
    REFERENCES Facturas (nroFactura),
  CONSTRAINT fk_DetallesF_Articulos foreign key (articulo)
    REFERENCES Articulos (ID)
);
      -----------------------------STORED PROCEDURES----------------------------------------------

            -------------------------- ARTÍCULOS --------------------------------
CREATE PROCEDURE SP_ObtenerTodosLosArticulos
AS
BEGIN
    SELECT ID, nombre, precioUnitario
    FROM ARTICULOS;
END;
----------------------------------------------------------
CREATE PROCEDURE SP_INSERTAR_ARTICULOS
@nombre VARCHAR(50),
@precioUni decimal(9,3)
AS
	BEGIN 
		INSERT INTO ARTICULOS (nombre, precioUnitario)
						VALUES(@nombre, @precioUni)
END
----------------------------------------------------------
CREATE PROCEDURE SP_ELIMINAR_ARTICULOS
@id int
AS
	BEGIN
		DELETE FROM ARTICULOS
		WHERE ID = @id
END
----------------------------------------------------------
CREATE PROCEDURE SP_ACTUALIZAR_ARTICULOS
@id int,
@nombre varchar(50),
@precioUni decimal (9,3)
AS
	BEGIN 
		UPDATE ARTICULOS
		SET nombre = @nombre,
		    precioUnitario = @precioUni
		WHERE id = @id
     END
