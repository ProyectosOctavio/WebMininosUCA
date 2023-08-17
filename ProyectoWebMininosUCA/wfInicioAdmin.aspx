<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Admin.Master" AutoEventWireup="true" CodeBehind="wfInicioAdmin.aspx.cs" Inherits="ProyectoWebMininosUCA.wfInicioAdmin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="banner">
        <img class="banner-logo" src="images/mininos_logo.png" alt="logo" />
    </div>
    <div>
        <div class="content-container body-content">
            <div class="row">
                <div class="col-half">
                    <h1>¿Quienes Somos?</h1>
                    <p>Como iniciativa estudiantil su Misión es concientizar a la comunidad universitaria sobre la presencia de felinos en el campus y su sana convivencia. Como parte de sus objetivos está el facilitar las necesidades básicas de los gatitos (comida, agua, esterilización y atención veterinaria).</p>
                    <div class="center">
                        <img src="images/minino_template2.png" alt="logo"/>
                    </div>
                </div>
                <div class="col-half">
                    <div class="center">
                        <img src="images/minino_template1.png" alt="logo"/>
                    </div>
                    <h1>Nuestra mision, vision y valores</h1>
                    <p>
                        Se tiene como Misión concientizar sobre la presencia de felinos en el campus para su sana convivencia. Como Visión se pretende facilitar las necesidades básicas de los gatitos (comida, agua, esterilización, atención veterinaria). Nuestros valores son: Persistencia, Amor hacia los felinos, Colaboración.
                    </p>
                </div>
            </div>
        </div>
        <div class="container-secondary">
            <div class="content-container body-content">
                <div class="row">
                    <div class="col-full">
                        <h1>Equipo de Trabajo</h1>
                        <p>Mininos UCA esta organizado por: Encargados y Centinelas gatunos. El primer grupo es más de logística, días para alimentar, donaciones, planear que se hará y actuar de manera inmediata. En cuanto a los centinelas, son estudiantes que cuidan y alimentan a los gatitos, nos ayudan a compartir información y decidir, Esta iniciativa tiene como lugar el campus universitario, los beneficiados son los felinos y consideramos que cualquier persona dentro de la UCA debe ser tolerante y respetuoso con estos animalitos.</p>
                    </div>
                </div>
            </div>
            <div class="caroussel-container">
                <div class="wrapper">
                    <i id="left" class="fa-solid fa-angle-left"></i>
                    <ul id="caroussel" class="caroussel">
                        <% foreach (var user in CargarUsuarios())
                            { %>
                        <li class="card">
                            <div class="caroussel-image">
                                <img src="data:image/jpeg;base64,<%= Convert.ToBase64String(user.fotoId) %>" alt="logo" />
                            </div>
                            <h2><%= user.nombre %> <%= user.apellido %></h2>
                        </li>
                        <% } %>
                    </ul>
                    <i id="right" class="fa-solid fa-angle-right"></i>
                </div>
            </div>
        </div>
    </div>
    <div class="banner-small">
        <div class="content-container  body-content">
            <h1>¡Apoyanos!</h1>
            <p>Mininos UCA se sostiene a traves de donaciones de personas que se interesen por el bienestar de los gatitos, apoyanos para que continuemos ayudando mas gatitos.</p>
        </div>
    </div>
</asp:Content>
