<%@ Page Title="Pagina Principal" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ProyectoWebMininosUCA._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
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
                        <img src="images/minino_template2.png" alt="logo" />
                    </div>
                </div>
                <div class="col-half">
                    <div class="center">
                        <img src="images/minino_template1.png" alt="logo" />
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

        

        <div class="container-secondary">
            <div class="content-container body-content">
                <div class="row">
                    <div class="col-full">
                        <h1>¡Gracias por su apoyo! </h1>
                        <p>
                            Mininos UCA es una organización sin fines de lucro dedicada al rescate y cuidado de gatos en situación de calle.
                    Nuestro objetivo es darles una mejor calidad de vida y encontrarles hogares amorosos. Con tu ayuda podemos seguir salvando vidas felinas. 
                    ¡Únete a nuestra causa! ¡Ayúdanos a ayudar! Puedes hacer una donación para apoyar nuestra labor.
                        </p>
                         
                    </div>
                </div>
            </div>

            <div class="content-container body-content">
                <div class="row">
                    <div class="col-full">
             <h1>Colaboracion de donantes monetarios </h1>
                         </div>
                </div>
            </div>
            <div class="caroussel-container">
                <div class="wrapper">
                    <i id="left2" class="fa-solid fa-angle-left"></i>
                    <ul id="caroussel2" class="caroussel">
                        <% foreach (var donante in CargarDonantesMonetarios())
                            { %>
                        <li class="card">
                            <div class="caroussel-image">
                                <img src="images/donacion_monetaria_logo.png" alt="logo" />
                            </div>
                            <h2>
                                <span class="caroussel-data">Nombre: <%= donante.Nombre %></span>
                                <span class="caroussel-data">Apellido: <%= donante.Apellido %></span>
                                <span class="caroussel-data">Alias: <%= donante.Alias %></span>
                                <span class="caroussel-data">Monto: C$ <%= donante.Monto %></span>
                            </h2>
                        </li>
                        <% } %>
                    </ul>
                    <i id="right2" class="fa-solid fa-angle-right"></i>
                </div>
            </div>

            <div class="space"></div> 

            <div class="content-container body-content">
                <div class="row">
                    <div class="col-full">
             <h1>Colaboracion de donantes de especias </h1>
                         </div>
                </div>
            </div>
            <div class="caroussel-container">
                 <div class="scroll-container">
                <div class="wrapper">
                    <i id="left3" class="fa-solid fa-angle-left"></i>
                    <ul id="caroussel3" class="caroussel">
                        <% foreach (var donante2 in CargarDonantesEspecias())
                            { %>
                        <li class="card">
                            <div class="caroussel-image">
                                <img src="images/donacion_especias_logo.png" alt="logo" />
                            </div>
                            <h2>
                                <span class="caroussel-data">Nombre: <%= donante2.Nombre %></span>
                                <span class="caroussel-data">Apellido: <%= donante2.Apellido %></span>
                                <span class="caroussel-data">Alias: <%= donante2.Alias %></span>
                                <span class="caroussel-data">Tipo: <%= donante2.Tipo %></span>
                                <span class="caroussel-data">Cantidad: <%= donante2.Cantidad %></span>
                                <span class="caroussel-data">Medida: <%= donante2.Medida %></span>
                            </h2>
                        </li>
                        <% } %>
                    </ul>
                    <i id="right3" class="fa-solid fa-angle-right"></i>
                </div>
            </div>
   </div>

        </div>
    </div>
    <div class="space"></div> 

    <div class="banner-small">
        <div class="content-container  body-content">
            <h1>¡Apoyanos!</h1>
            <p>Mininos UCA se sostiene a traves de donaciones de personas que se interesen por el bienestar de los gatitos, apoyanos para que continuemos ayudando mas gatitos.</p>
            <div class="center">
                <a runat="server" href="~/wfDonarDonante" class="mininos-button-secondary">Donar</a>
            </div>
        </div>
    </div>


</asp:Content>
