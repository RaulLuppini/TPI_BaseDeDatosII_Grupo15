# TP-ECOMMERCE-21-B
TP ECOMMERCE
•	 eCommerce WebForms TP Cuatrimestral


🛒 Descripción

Este proyecto es un sistema de comercio electrónico desarrollado como Trabajo Práctico final para la materia de Programación 3 con Web Forms. Permite gestionar productos, usuarios y pedidos, simulando una tienda online funcional con enfoque en modularidad, buenas prácticas y documentación clara para facilitar el mantenimiento y la colaboración.

🧰 Tecnologías y Herramientas Utilizadas
- ASP.NET Web Forms (C#)
- SQL Server
- ADO.NET
- HTML5, CSS3, JavaScript
- Bootstrap 5
- Visual Studio 2022

🧱 Entidades Principales

🧱 Entidades Principales 
- Usuario: Datos personales, autenticación, rol (cliente/admin) 
- Producto: Código, nombre, descripción, marca, categoría, imágenes, precios, stock 
- Marca y Categoría: Clasificación de productos 
- Imagen: URLs asociadas a productos 
 -Pedido: Fecha, total, estado, relación con usuario
 - Detalle Pedido: Productos dentro de cada pedido, cantidad, subtotal
 - Dirección: Calle, ciudad, provincia, código postal, observaciones (asociada al usuario)
🚀 Funcionalidades

- Registro y autenticación de usuarios
- ABM de productos (solo para administradores)
- Navegación por catálogo y búsqueda
- Carrito de compras con persistencia
- Confirmación de pedidos y resumen
- Validaciones del lado cliente y servidor


 📌 Consideraciones Técnicas

- Separación de capas: presentación, lógica de negocio y acceso a datos
- Uso de controles de servidor y validadores de Web Forms
- Manejo de sesiones para carrito y autenticación
- Código comentado y estructurado para facilitar el onboarding
👥 Autores
- Raul Oscar Luppini
- Alejandro Jesús Maza Márquez
📄 Licencia

Uso académico. Proyecto desarrollado para fines educativos.

