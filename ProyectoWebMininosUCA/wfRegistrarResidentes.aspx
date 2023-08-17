<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Admin.Master" AutoEventWireup="true" CodeBehind="wfRegistrarResidentes.aspx.cs" Inherits="ProyectoWebMininosUCA.wfGestionarResidentes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
        <div class="banner-medium">
        <h1>Registrar residente</h1>
        </div>
      <div class="content-container body-content">
          <div class="row">
             <div class="col-full">
                 <div class="mininos-form-container">
                     <h1>Registrar un residente</h1>
                     <div class="mininos-form">
            <div class="form-row-1">
             <asp:TextBox ID="txtNombre" runat="server" Class="mininos-input" placeholder="Nombre"/>
            </div>
            <div class="form-row-1">
             <asp:TextBox ID="txtDescripcion" runat="server" Class="mininos-input" placeholder="Descripcion"/>
            </div>
            <div class="form-row-2">
             <asp:DropDownList ID="ddlFechaIngreso" runat="server"  Class="mininos-input" Height="39px" Width="183px">
                 <asp:ListItem Text="Fecha de ingreso" Value=""></asp:ListItem>
             </asp:DropDownList>
             <asp:DropDownList ID="ddlFechaNacimiento" runat="server" Class="mininos-input" Height="39px" Width="183px">
                 <asp:ListItem Text="Fecha de nacimiento" Value=""></asp:ListItem>
             </asp:DropDownList>
            </div>
            <div class="form-row-2">
             <asp:DropDownList ID="ddlFechaDefuncion" runat="server" Class="mininos-input" Height="39px" Width="183px">
                 <asp:ListItem Text="Fecha de defuncion" Value=""></asp:ListItem>
             </asp:DropDownList>
             <asp:DropDownList ID="ddlFechaDesaparicion" runat="server" Class="mininos-input" Height="39px" Width="183px">
                 <asp:ListItem Text="Fecha de desaparicion" Value=""></asp:ListItem>
             </asp:DropDownList>
            </div>
             <div class="form-row-2">
              <asp:DropDownList ID="ddlSexo" runat="server" Class="mininos-input" Height="39px" Width="183px">
                  <asp:ListItem Text="Sexo" Value=""></asp:ListItem>
             </asp:DropDownList>
                 <asp:TextBox ID="txtPatologia" runat="server" Class="mininos-input" placeholder="Patologia" Height="39px" Width="183px"/>
            </div>
         <div class="form-row-2">
             <asp:DropDownList ID="ddlZona" runat="server" Class="mininos-input" Height="39px" Width="183px">
                 <asp:ListItem Text="Zona" Value=""></asp:ListItem>
             </asp:DropDownList>

            <asp:DropDownList ID="ddlEsterilizacion" runat="server" Class="mininos-input" Height="39px" Width="183px">
                <asp:ListItem Text="Esterilizacion" Value=""></asp:ListItem>
                <asp:ListItem Text="Si" Value="1"></asp:ListItem>
                <asp:ListItem Text="No" Value="1"></asp:ListItem>
            </asp:DropDownList>
             </div>
                         <div class="form-row-2">
                         <br />
                         <asp:Label runat="server">Subir foto:</asp:Label>
                         <asp:FileUpload ID="fuResidente" runat="server" Class="mininos-input" Height="39px" Width="183px"/>
                         <br />
                             </div>
                         </div>
            
        
                     <br />
    <asp:Button ID="btnRegistrarResidente" runat="server"   Class="mininos-button-primary"  Text="Registrar nuevo residente"> </asp:Button>
                     <br />
                     </div>
                 <!--
   <div class="mininos-form-container"> 
       <h1>Gestionar residente</h1>
                     <div class="mininos-form">
    <asp:GridView runat="server" Class="grid-view">
        <Columns>
            <asp:TemplateField HeaderText="Acciones">
                <ItemTemplate>
                    <asp:Button runat="server"  Class="mininos-button-primary"  Text="Editar" Height="39px" Width="183px"/>
                    <asp:Button runat="server"  Class="mininos-button-primary" Text="Eliminar" Height="39px" Width="183px"/>
                </ItemTemplate>
            </asp:TemplateField>
             <asp:BoundField DataField="id" HeaderText="Id" Visible="False"/>
                        <asp:BoundField DataField="nombre" HeaderText="Nombre" />
                        <asp:BoundField DataField="descripcion" HeaderText="Descripcion" />
                        <asp:BoundField DataField="fechaIngreso" HeaderText="Fecha de ingreso" />
                        <asp:BoundField DataField="fechaNacimiento" HeaderText="Fecha de nacimiento" />
                        <asp:BoundField DataField="sexo" HeaderText="Sexo" />
                        <asp:BoundField DataField="zona" HeaderText="Zona" />
                        <asp:BoundField DataField="patologia" HeaderText="Patologia" />
                        <asp:BoundField DataField="esterilizado" HeaderText="Esterilizacion" />
                        <asp:BoundField DataField="fechaDefuncion" HeaderText="Fecha de defuncion" />
                        <asp:BoundField DataField="fechaDesaparicion" HeaderText="Fecha de desaparicion" />
        </Columns>
    </asp:GridView>
                         </div>
       </div> -->
                 </div>
               
             </div>
 </div>
    </asp:Content>
     
