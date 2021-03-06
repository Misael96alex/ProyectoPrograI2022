<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="agregarEmpleado.aspx.cs" Inherits="ConsumeServicio.PaginasEmpleados.agregarEmpleado" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <LINK REL=StyleSheet HREF="agregarEmpleado.css" TYPE="text/css"/>
    <title>Agregar Empleado</title>
</head>
<body>
    <form id="form1" runat="server">
        <header id="main-header">
		
		<a id="logo-header" href="#">
			SISTEMA CONSULTA MEDICA <span class="site-desc">Programacion I</span>
		</a> <!-- / #logo-header -->

		<nav>
			<ul>
				<li><a href="https://localhost:44321/PaginasEmpleados/menuEmpleado.aspx">Empleado</a></li>
				
			</ul>
		</nav><!-- / nav -->

	</header><!-- / #main-header -->

        <div>
			<div class ="titulo"><center><asp:Label ID="Label1" runat="server" Text="Agregar Empleado"></asp:Label></center>
            </div>
			<center>
				<div class="agregar">
					
					<asp:DropDownList Class="clinica" ID="clinica" runat="server" Height="54px" style="margin-left: 0px" Width="300px" margin-bottom="10px">
                    </asp:DropDownList>
					<asp:TextBox ID="nombre" runat="server" placeholder="Nombre" required="required"></asp:TextBox>
					<asp:TextBox ID="apellido" runat="server" placeholder="Apellido" required="required"></asp:TextBox>
					<asp:TextBox ID="dpi" runat="server" placeholder="DPI" required="required"></asp:TextBox>
					<asp:TextBox ID="nit" runat="server" placeholder="Nit" required="required"></asp:TextBox>
					<asp:TextBox ID="direccion" runat="server" placeholder="Dirección" required="required"></asp:TextBox>
					<asp:TextBox ID="correo" runat="server" placeholder="E-mail" required="required"></asp:TextBox>
					<asp:TextBox ID="telefono" runat="server" placeholder="Número de Telefono" required="required"></asp:TextBox>
					<asp:Button ID="agregar" class="botones btn" runat="server" Text="Agregar" Width="288px" OnClick="agregar_Click"  />
					<asp:Label ID="salidaLabel" runat="server" Text=" " ForeColor="White"></asp:Label>
				
				</div>
		        
		</center>
        </div>
    </form>
</body>
</html>
