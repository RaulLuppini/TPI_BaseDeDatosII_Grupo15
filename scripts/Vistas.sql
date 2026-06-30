USE [TPI_BDII];
GO

--Vista para identificar clientes con más compras y su gasto total
CREATE VIEW VW_ClientesFrecuentes AS
SELECT
    u.IDUsuario,
    u.Nombre,
    u.Apellido,
    u.Correo,
    COUNT(p.IDPedido) AS CantidadPedidos,
    SUM(p.Precio) AS TotalGastado
FROM Usuario u
INNER JOIN Pedido p
    ON u.IDUsuario = p.IDUsuario
GROUP BY
    u.IDUsuario,
    u.Nombre,
    u.Apellido,
    u.Correo;
GO

----------------------------------------------------------

CREATE VIEW VW_ListarProductos AS
SELECT 
    p.IDProducto,
    p.Codigo,
    p.Nombre,
    m.IDMarca AS MarcaId,
    m.Nombre AS MarcaNombre,
    c.IDCategoria AS CategoriaId,
    c.Nombre AS CategoriaNombre,
    p.Descripcion,
    p.PrecioCompra,
    p.PorcentajeGanancia,
    p.PrecioVenta,
    p.StockActual,
    p.StockMinimo,
    p.Estado
FROM Producto p
INNER JOIN Marca m ON p.IDMarca = m.IDMarca
INNER JOIN Categoria c ON p.IDCategoria = c.IDCategoria;
GO

----------------------------------------------------------

CREATE VIEW VW_PedidosConUsuarios AS
SELECT 
    p.IDPedido,
    p.IDUsuario,
    p.Precio AS PrecioTotal,
    p.FechaPedido,
    u.Nombre AS NombreUsuario,
    u.Apellido AS ApellidoUsuario,
    e.Nombre AS Estado,
    m.Nombre AS MetodoPago
FROM Pedido p
INNER JOIN Usuario u ON p.IDUsuario = u.IDUsuario
INNER JOIN EstadoPedido e ON p.IDEstado = e.IDEstado
INNER JOIN MetodoPago m ON p.IDMetodoPago = m.IDMetodoPago;
GO

----------------------------------------------------------

CREATE VIEW VW_ListarPedidos AS
SELECT 
    p.IDPedido,
    p.IDUsuario,
    e.Nombre AS Estado,
    m.Nombre AS MetodoPago,
    p.FechaPedido,
    SUM(dp.Cantidad * dp.PrecioUnitario) AS PrecioTotal
FROM Pedido p
INNER JOIN EstadoPedido e ON p.IDEstado = e.IDEstado
INNER JOIN MetodoPago m ON p.IDMetodoPago = m.IDMetodoPago
INNER JOIN DetalleProducto dp ON p.IDPedido = dp.IDPedido
GROUP BY p.IDPedido, p.IDUsuario, e.Nombre, m.Nombre, p.FechaPedido;
GO

----------------------------------------------------------

CREATE VIEW VW_ListarUsuario AS
SELECT 
    IDUsuario,
    DNI,
    Nombre,
    Apellido,
    Correo,
    Contrasenia,
    Rol,
    Telefono,
    Direccion,
    CodigoPostal,
    Estado
FROM Usuario;
GO
----------------------------------------------------------

--Vista para chequear productos que requieren reposici�n
CREATE VIEW VW_ProductosAReponer AS
SELECT 
    p.IDProducto,
    p.Codigo,
    p.Nombre AS Producto,
    m.Nombre AS Marca,
    c.Nombre AS Categoria,
    p.StockActual,
    p.StockMinimo,
    (p.StockMinimo - p.StockActual) AS CantidadAReponer
FROM Producto p
INNER JOIN Marca m ON p.IDMarca = m.IDMarca
INNER JOIN Categoria c ON p.IDCategoria = c.IDCategoria
WHERE p.StockActual <= p.StockMinimo 
  AND p.Estado = 1;
GO
----------------------------------------------------------

CREATE VIEW VW_ProductoMasVendido as
Select 
P.IDProducto,
P.Nombre,
SUM (DP.Cantidad) as Venta_Total

from Producto as P
inner join DetalleProducto as DP
on DP.IDProducto = P.IDProducto
group by 
P.IDProducto,
P.Nombre;

GO