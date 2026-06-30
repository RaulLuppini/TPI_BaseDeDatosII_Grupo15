USE [TPI_BDII];
GO

INSERT INTO Categoria (IDCategoria, NombreCategoria) VALUES 
(1, 'Electrónica y Tecnología'),
(2, 'Indumentaria Deportiva'),
(3, 'Hogar y Muebles');
GO

INSERT INTO Marca (IDMarca, Nombre) VALUES 
(1, 'Samsung'),
(2, 'Nike'),
(3, 'LG');
GO

INSERT INTO EstadosPedido (IDEstado, Nombre) VALUES 
(1, 'Pendiente de Pago'),
(2, 'Pagado / Preparando'),
(3, 'Enviado'),
(4, 'Entregado'),
(5, 'Cancelado'); 
GO

INSERT INTO MetodoPago (IDMetodoPago, Nombre) VALUES 
(1, 'Tarjeta de Crédito'),
(2, 'Transferencia Bancaria'),
(3, 'Mercado Pago');
GO


INSERT INTO Usuario (DNI, Nombre, Apellido, Correo, Contrasenia, Rol, Telefono, Direccion, CodigoPostal, Estado) VALUES 
('11111111', 'ADMIN', 'Sistema', 'admin@ecommerce.com', 'Admin123!', 'Administrador', '1122334455', 'Av. Corrientes 1234', 'C1043', 1),
('22222222', 'JUAN', 'Pérez', 'juan@cliente.com', 'Juan123!', 'Cliente', '1199887766', 'Calle Falsa 123', 'B1648', 1);
GO

INSERT INTO Producto (Codigo, Nombre, IDMarca, IDCategoria, Descripcion, PrecioCompra, PorcentajeGanancia, PrecioVenta, StockActual, StockMinimo, Estado) VALUES 
('TEC-001', 'Smart TV 50 Pulgadas 4K', 1, 1, 'Televisor inteligente con resolución 4K y Android TV.', 200000.00, 30.00, 260000.00, 45, 10, 1),
('DEP-001', 'Zapatillas Running Pro', 2, 2, 'Calzado deportivo para asfalto, talle 42.', 40000.00, 50.00, 60000.00, 5, 15, 1), 
('HOG-001', 'Heladera No Frost 400L', 3, 3, 'Heladera con freezer superior, eficiencia A+.', 450000.00, 25.00, 562500.00, 20, 5, 1);
GO

INSERT INTO Imagenes (IDImagen, IDProducto, URL) VALUES 
(1, 1, 'https://png.pngtree.com/png-vector/20250812/ourmid/pngtree-wall-mountable-lcd-tv-screen-png-image_17137045.webp'),
(2, 1, 'https://http2.mlstatic.com/D_Q_NP_2X_908347-MLA104127137314_012026-T.webp'),
(3, 2, 'https://acdn-us.mitiendanube.com/stores/005/566/415/products/ayv0353neg_1-1-537196a236fa72041917532204652809-1024-1024.webp');
GO


INSERT INTO Pedido (IDUsuario, PrecioTotal, IDEstado, IDMetodoPago, FechaPedido) VALUES 
(2, 260000.00, 2, 1, GETDATE()), 
(2, 120000.00, 4, 3, GETDATE()); 
GO

INSERT INTO DetalleProducto (IDProducto, IDPedido, Cantidad, PrecioUnitario, PrecioRebajado) VALUES 
(1, 1, 1, 260000.00, NULL), 
(2, 2, 2, 60000.00, NULL);  
GO