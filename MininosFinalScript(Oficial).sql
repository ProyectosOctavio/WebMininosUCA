USE [mininosUCA]
GO
/****** Object:  User [mininos]    Script Date: 8/18/2023 8:25:16 PM ******/
CREATE USER [mininos] FOR LOGIN [mininos] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_owner] ADD MEMBER [mininos]
GO
ALTER ROLE [db_datareader] ADD MEMBER [mininos]
GO
ALTER ROLE [db_datawriter] ADD MEMBER [mininos]
GO
/****** Object:  Table [dbo].[AES]    Script Date: 8/18/2023 8:25:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AES](
	[idAes] [int] IDENTITY(1,1) NOT NULL,
	[idUsuario] [int] NULL,
	[token] [nvarchar](50) NULL,
	[iv] [nvarchar](50) NULL,
	[estado] [int] NOT NULL,
 CONSTRAINT [PK_AES] PRIMARY KEY CLUSTERED 
(
	[idAes] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Album]    Script Date: 8/18/2023 8:25:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Album](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[foto] [varbinary](max) NOT NULL,
	[residenteId] [int] NOT NULL,
	[estado] [int] NOT NULL,
 CONSTRAINT [PK_Album] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Contacto]    Script Date: 8/18/2023 8:25:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Contacto](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[telefono] [nvarchar](max) NULL,
	[correo] [nvarchar](max) NULL,
	[correo2] [nvarchar](max) NULL,
	[twitter] [nvarchar](max) NULL,
	[insta] [nvarchar](max) NULL,
	[facebook] [nvarchar](max) NULL,
 CONSTRAINT [PK_Contacto] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DonacionEspecies]    Script Date: 8/18/2023 8:25:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DonacionEspecies](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[tipoEspecie] [nvarchar](max) NOT NULL,
	[cantidad] [float] NOT NULL,
	[unidadMedida] [nvarchar](max) NOT NULL,
	[fecha] [date] NOT NULL,
	[donanteId] [int] NOT NULL,
	[estado] [int] NOT NULL,
 CONSTRAINT [PK_DonacionEspecies] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DonacionMonetaria]    Script Date: 8/18/2023 8:25:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DonacionMonetaria](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[montoNi] [float] NOT NULL,
	[voucher] [varbinary](max) NOT NULL,
	[fecha] [date] NOT NULL,
	[donanteId] [int] NOT NULL,
	[estado] [int] NOT NULL,
 CONSTRAINT [PK_DonacionMonetaria] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Donante]    Script Date: 8/18/2023 8:25:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Donante](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[fechaIngreso] [date] NOT NULL,
	[alias] [nvarchar](max) NULL,
	[nombre] [nvarchar](max) NOT NULL,
	[apellido] [nvarchar](max) NOT NULL,
	[correo] [nvarchar](max) NOT NULL,
	[numTelefono] [nvarchar](max) NOT NULL,
	[pais] [nvarchar](max) NOT NULL,
	[ciudad] [nvarchar](max) NOT NULL,
	[estado] [int] NOT NULL,
 CONSTRAINT [PK_Donante] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Incidente]    Script Date: 8/18/2023 8:25:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Incidente](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nivelRiesgoId] [int] NULL,
	[descripcion] [nvarchar](max) NOT NULL,
	[foto] [varbinary](max) NULL,
	[residenteId] [int] NULL,
	[usuarioId] [int] NOT NULL,
	[fechaHora] [datetime] NOT NULL,
	[estado] [int] NOT NULL,
 CONSTRAINT [PK_Incidente] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[InfoBancaria]    Script Date: 8/18/2023 8:25:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InfoBancaria](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[banco] [nvarchar](max) NOT NULL,
	[numeroCuenta] [nvarchar](max) NOT NULL,
	[moneda] [nvarchar](max) NOT NULL,
	[estado] [int] NOT NULL,
 CONSTRAINT [PK_InfoBancaria] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NivelDeRiesgo]    Script Date: 8/18/2023 8:25:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NivelDeRiesgo](
	[descripcion] [nvarchar](max) NULL,
	[id] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_NivelDeRiesgo] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Opcion]    Script Date: 8/18/2023 8:25:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Opcion](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[descripcion] [nvarchar](max) NOT NULL,
	[accion] [nvarchar](max) NOT NULL,
	[url] [nvarchar](max) NOT NULL,
	[estado] [int] NOT NULL,
 CONSTRAINT [PK_Opcion] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Patologia]    Script Date: 8/18/2023 8:25:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Patologia](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[descripcion] [nvarchar](max) NULL,
	[estado] [int] NOT NULL,
 CONSTRAINT [PK_Patologia] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Publicacion]    Script Date: 8/18/2023 8:25:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Publicacion](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[fotoPublicacion] [varbinary](max) NULL,
	[titulo] [nvarchar](max) NOT NULL,
	[tipo] [nvarchar](max) NOT NULL,
	[contenido] [nvarchar](max) NOT NULL,
	[fecha] [date] NOT NULL,
	[estado] [int] NOT NULL,
 CONSTRAINT [PK_Publicacion] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Residente]    Script Date: 8/18/2023 8:25:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Residente](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[fotoId] [varbinary](max) NULL,
	[nombre] [nvarchar](max) NOT NULL,
	[descripcion] [nvarchar](max) NOT NULL,
	[sexo] [bit] NULL,
	[esterilizado] [bit] NOT NULL,
	[fechaIngreso] [date] NOT NULL,
	[fechaNacimiento] [date] NULL,
	[fechaDesaparicion] [date] NULL,
	[fechaDefuncion] [date] NULL,
	[zonaId] [int] NULL,
	[estado] [int] NOT NULL,
 CONSTRAINT [PK_Residente] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ResidenteDonante]    Script Date: 8/18/2023 8:25:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ResidenteDonante](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[donanteId] [int] NULL,
	[residenteId] [int] NULL,
	[estado] [int] NOT NULL,
 CONSTRAINT [PK_ResidenteDonante] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ResidentePatologia]    Script Date: 8/18/2023 8:25:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ResidentePatologia](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[patologiaId] [int] NULL,
	[residenteId] [int] NULL,
	[estado] [int] NOT NULL,
 CONSTRAINT [PK_ResidentePatologia] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Rol]    Script Date: 8/18/2023 8:25:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Rol](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [nvarchar](max) NOT NULL,
	[descripcion] [nvarchar](max) NOT NULL,
	[estado] [int] NOT NULL,
 CONSTRAINT [PK_Rol] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RolOpcion]    Script Date: 8/18/2023 8:25:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RolOpcion](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[opcionId] [int] NULL,
	[rolId] [int] NULL,
	[estado] [int] NOT NULL,
 CONSTRAINT [PK_RolOpcion] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuario]    Script Date: 8/18/2023 8:25:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuario](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[fotoId] [varbinary](max) NULL,
	[nombre] [nvarchar](max) NOT NULL,
	[apellido] [nvarchar](max) NOT NULL,
	[username] [nvarchar](max) NOT NULL,
	[pw] [nvarchar](max) NOT NULL,
	[email] [nvarchar](max) NOT NULL,
	[telefonoCel] [nvarchar](max) NULL,
	[fechaCreacion] [date] NOT NULL,
	[rolId] [int] NULL,
	[estado] [int] NOT NULL,
	[TokenRestablecimiento] [nvarchar](100) NULL,
	[FechaCreacionToken] [datetime] NULL,
 CONSTRAINT [PK_Usuario] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Zona]    Script Date: 8/18/2023 8:25:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Zona](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [nvarchar](max) NOT NULL,
	[estado] [int] NOT NULL,
 CONSTRAINT [PK_Zona] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[Donante] ADD  CONSTRAINT [DF_Donante_fechaIngreso]  DEFAULT (getdate()) FOR [fechaIngreso]
GO
ALTER TABLE [dbo].[Incidente] ADD  CONSTRAINT [DF_Incidente_fechaHora]  DEFAULT (getdate()) FOR [fechaHora]
GO
ALTER TABLE [dbo].[Residente] ADD  CONSTRAINT [DF_Residente_fechaIngreso]  DEFAULT (getdate()) FOR [fechaIngreso]
GO
ALTER TABLE [dbo].[AES]  WITH CHECK ADD  CONSTRAINT [FK_Usuario_utiliza] FOREIGN KEY([idUsuario])
REFERENCES [dbo].[Usuario] ([id])
GO
ALTER TABLE [dbo].[AES] CHECK CONSTRAINT [FK_Usuario_utiliza]
GO
ALTER TABLE [dbo].[Album]  WITH CHECK ADD  CONSTRAINT [FK_Album_tiene] FOREIGN KEY([residenteId])
REFERENCES [dbo].[Residente] ([id])
GO
ALTER TABLE [dbo].[Album] CHECK CONSTRAINT [FK_Album_tiene]
GO
ALTER TABLE [dbo].[DonacionEspecies]  WITH CHECK ADD  CONSTRAINT [FK_Donacion especies_dona] FOREIGN KEY([donanteId])
REFERENCES [dbo].[Donante] ([id])
GO
ALTER TABLE [dbo].[DonacionEspecies] CHECK CONSTRAINT [FK_Donacion especies_dona]
GO
ALTER TABLE [dbo].[DonacionMonetaria]  WITH CHECK ADD  CONSTRAINT [FK_Donacion monetaria_dona] FOREIGN KEY([donanteId])
REFERENCES [dbo].[Donante] ([id])
GO
ALTER TABLE [dbo].[DonacionMonetaria] CHECK CONSTRAINT [FK_Donacion monetaria_dona]
GO
ALTER TABLE [dbo].[Incidente]  WITH CHECK ADD  CONSTRAINT [FK_Incidente_implica] FOREIGN KEY([nivelRiesgoId])
REFERENCES [dbo].[NivelDeRiesgo] ([id])
GO
ALTER TABLE [dbo].[Incidente] CHECK CONSTRAINT [FK_Incidente_implica]
GO
ALTER TABLE [dbo].[Incidente]  WITH CHECK ADD  CONSTRAINT [FK_Incidente_ocurren] FOREIGN KEY([residenteId])
REFERENCES [dbo].[Residente] ([id])
GO
ALTER TABLE [dbo].[Incidente] CHECK CONSTRAINT [FK_Incidente_ocurren]
GO
ALTER TABLE [dbo].[Incidente]  WITH CHECK ADD  CONSTRAINT [FK_Usuario_reporta] FOREIGN KEY([usuarioId])
REFERENCES [dbo].[Usuario] ([id])
GO
ALTER TABLE [dbo].[Incidente] CHECK CONSTRAINT [FK_Usuario_reporta]
GO
ALTER TABLE [dbo].[Residente]  WITH CHECK ADD  CONSTRAINT [FK_Residente_se ubica] FOREIGN KEY([zonaId])
REFERENCES [dbo].[Zona] ([id])
GO
ALTER TABLE [dbo].[Residente] CHECK CONSTRAINT [FK_Residente_se ubica]
GO
ALTER TABLE [dbo].[ResidenteDonante]  WITH CHECK ADD  CONSTRAINT [FK_apadrinado_Donante] FOREIGN KEY([donanteId])
REFERENCES [dbo].[Donante] ([id])
GO
ALTER TABLE [dbo].[ResidenteDonante] CHECK CONSTRAINT [FK_apadrinado_Donante]
GO
ALTER TABLE [dbo].[ResidenteDonante]  WITH CHECK ADD  CONSTRAINT [FK_apadrinado_Residente] FOREIGN KEY([residenteId])
REFERENCES [dbo].[Residente] ([id])
GO
ALTER TABLE [dbo].[ResidenteDonante] CHECK CONSTRAINT [FK_apadrinado_Residente]
GO
ALTER TABLE [dbo].[ResidentePatologia]  WITH CHECK ADD  CONSTRAINT [FK_padece_Patologia] FOREIGN KEY([patologiaId])
REFERENCES [dbo].[Patologia] ([id])
GO
ALTER TABLE [dbo].[ResidentePatologia] CHECK CONSTRAINT [FK_padece_Patologia]
GO
ALTER TABLE [dbo].[ResidentePatologia]  WITH CHECK ADD  CONSTRAINT [FK_padece_Residente] FOREIGN KEY([residenteId])
REFERENCES [dbo].[Residente] ([id])
GO
ALTER TABLE [dbo].[ResidentePatologia] CHECK CONSTRAINT [FK_padece_Residente]
GO
ALTER TABLE [dbo].[RolOpcion]  WITH CHECK ADD  CONSTRAINT [FK_tiene_Opcion] FOREIGN KEY([opcionId])
REFERENCES [dbo].[Opcion] ([id])
GO
ALTER TABLE [dbo].[RolOpcion] CHECK CONSTRAINT [FK_tiene_Opcion]
GO
ALTER TABLE [dbo].[RolOpcion]  WITH CHECK ADD  CONSTRAINT [FK_tiene_Rol] FOREIGN KEY([rolId])
REFERENCES [dbo].[Rol] ([id])
GO
ALTER TABLE [dbo].[RolOpcion] CHECK CONSTRAINT [FK_tiene_Rol]
GO
ALTER TABLE [dbo].[Usuario]  WITH CHECK ADD  CONSTRAINT [FK_Usuario_tiene] FOREIGN KEY([rolId])
REFERENCES [dbo].[Rol] ([id])
GO
ALTER TABLE [dbo].[Usuario] CHECK CONSTRAINT [FK_Usuario_tiene]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'El album contendria todas las fotos de los mininos o cualquier otro evento de Mininos UCA.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Album'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Cuando un donante hace una donacion de especies, este ultimo lleva los datos de la fecha en que se dono y la cantidad exacta.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DonacionEspecies'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Cuando un donante hace una donacion monetaria, este ultimo lleva los datos de la fecha en que se dono y el monto en cordobas.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DonacionMonetaria'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Cuando un donante apadrina un minino este tiene que dar los siguientes datos: nombres, apellidos, alias(caso que no quiera dar nombre completo), correo, numero de telefono, ciudad y el pais.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Donante'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'El inicidente significa cualquier problema que puede ocurrir a los mininos y debe de comunicarse inmediatamente. El incidente tiene una fecha y hora de cuando ocurrio, una descripcion de lo que ocurrio, un nivel de riesgo  y el estado de este mismo.
El estado del incidente es si el inicidente ya fue atendido o si sigue pendiente a hacer.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Incidente'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'El nivel de riesgo contiene una descripcion del problema del minino, El nivel de riesgo que implica para el inicidente, muestra que tan grave es el problema a atender, ya que en un dia se pueden recibir muchas notificaciones y estos deben ordenarse de forma prioritaria.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'NivelDeRiesgo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'La patologia contiene una descripcion de la enfermedad o enfermedades que padece  cada minino.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Patologia'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'La publicacion tiene un titulo y contenido de algun evento. Se considera este dato como parte del sistema o parte del sistema pero funcionando de manera externa, es decir desde otra pagina integrada en el sistema como lo seria wordress.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Publicacion'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'El residente hace referencia a cada minino de la UCA. Cada minino sera guardado en el sistema con los datos importantes como:  Un nombre , una breve descripcion de como es fisicamente, sexo y si esta esterilizado o no. Tambien contaran con una fecha en la cual ingreso a la UCA.  Tienen una fecha de nacimiento en el caso que otro gato le haya dado a luz dentro de la UCA. Tienen una fecha de defuncion, que indica cuando murieron.  Y Tienen una fecha de desaparicion.
Cada gato sera identificado en el sistema con una "fotoId" que significa que cada gato es unico porque contara con un id y una foto diferente dentro del sistema.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Residente'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'La zona es la ubicacion o el punto que se le da a cada uno de los mininos. En este caso el dato que se proporciona es el nombre de dicha zona.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Zona'
GO
