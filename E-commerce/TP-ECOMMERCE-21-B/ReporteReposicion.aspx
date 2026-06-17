<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReporteReposicion.aspx.cs" Inherits="TP_ECOMMERCE_21_B.ReporteReposicion" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Reporte de Reposición</title>
</head>
<body>
    <form id="form1" runat="server">
        <div style="font-family: Arial, sans-serif; margin: 20px;">
            <h2>Productos que requieren reposición crítica</h2>
            <hr />
            
            <asp:GridView ID="dgvFaltantes" runat="server" AutoGenerateColumns="false" CellPadding="8" ForeColor="#333333" GridLines="None">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:BoundField DataField="Codigo" HeaderText="Código" />
                    <asp:BoundField DataField="Nombre" HeaderText="Producto" />
                    <asp:BoundField DataField="IdMarca.Nombre" HeaderText="Marca" />
                    <asp:BoundField DataField="IdCategoria.Nombre" HeaderText="Categoría" />
                    <asp:BoundField DataField="StockActual" HeaderText="Stock Actual" />
                    <asp:BoundField DataField="StockMinimo" HeaderText="Stock Mínimo" />
                    <asp:BoundField DataField="CantidadAReponer" HeaderText="A Reponer" />
                </Columns>
                <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
            </asp:GridView>

            <asp:Label ID="lblMensaje" runat="server" ForeColor="Red" style="display:block; margin-top:10px;"></asp:Label>
        </div>
    </form>
</body>
</html>