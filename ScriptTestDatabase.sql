USE [Test]
GO
/****** Object:  Table [dbo].[Producto]    Script Date: 22/11/2022 1:49:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Producto](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](max) NULL,
	[Precio] [real] NOT NULL,
	[IdTipoProducto] [int] NOT NULL,
	[Created_At] [datetime2](7) NOT NULL,
	[Deleted] [bit] NOT NULL,
 CONSTRAINT [PK_Producto] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Stock]    Script Date: 22/11/2022 1:49:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Stock](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdProducto] [int] NOT NULL,
	[Cantidad] [real] NOT NULL,
	[Created_At] [datetime2](7) NOT NULL,
	[Deleted] [bit] NOT NULL,
 CONSTRAINT [PK_Stock] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[vw_StockProducto]    Script Date: 22/11/2022 1:49:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create view [dbo].[vw_StockProducto] 
as
select p.nombre, p.precio, s.cantidad from Producto as P inner join Stock as S on S.idProducto = P.id;
GO
/****** Object:  Table [dbo].[TipoProducto]    Script Date: 22/11/2022 1:49:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TipoProducto](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Descripcion] [nvarchar](max) NULL,
	[Created_At] [datetime2](7) NOT NULL,
	[Deleted] [bit] NOT NULL,
 CONSTRAINT [PK_TipoProducto] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  View [dbo].[vw_StockProductoTipo]    Script Date: 22/11/2022 1:49:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vw_StockProductoTipo]
AS
SELECT        P.Nombre, P.Precio, S.Cantidad, t.Descripcion, P.Id
FROM            dbo.Producto AS P INNER JOIN
                         dbo.Stock AS S ON S.IdProducto = P.Id INNER JOIN
                         dbo.TipoProducto AS t ON t.Id = P.IdTipoProducto
WHERE        (P.Deleted = 0)
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 22/11/2022 1:49:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20221115004613_InitDB', N'5.0.16')
GO
ALTER TABLE [dbo].[Producto]  WITH CHECK ADD  CONSTRAINT [FK_Producto_TipoProducto_IdTipoProducto] FOREIGN KEY([IdTipoProducto])
REFERENCES [dbo].[TipoProducto] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Producto] CHECK CONSTRAINT [FK_Producto_TipoProducto_IdTipoProducto]
GO
ALTER TABLE [dbo].[Stock]  WITH CHECK ADD  CONSTRAINT [FK_Stock_Producto_IdProducto] FOREIGN KEY([IdProducto])
REFERENCES [dbo].[Producto] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Stock] CHECK CONSTRAINT [FK_Stock_Producto_IdProducto]
GO
/****** Object:  StoredProcedure [dbo].[sp_EliminarProducto]    Script Date: 22/11/2022 1:49:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[sp_EliminarProducto]
@id int
as 
delete from Producto where id = @id;
GO
/****** Object:  StoredProcedure [dbo].[sp_InsertarProducto]    Script Date: 22/11/2022 1:49:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[sp_InsertarProducto]
@tipoProducto int,
@nombre as nvarchar(50),
@precio as float
as
insert into Producto(idTipoProducto, nombre, precio) values(@tipoProducto, @nombre, @precio)
GO
/****** Object:  StoredProcedure [dbo].[sp_ModificarProducto]    Script Date: 22/11/2022 1:49:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[sp_ModificarProducto]
@tipoProducto int,
@nombre as nvarchar(50),
@precio as float,
@busqueda nvarchar(50)
as
UPDATE Producto SET idTipoProducto = @tipoProducto, nombre = @nombre, precio = @precio
  WHERE nombre = @busqueda;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "P"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 209
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "S"
            Begin Extent = 
               Top = 6
               Left = 247
               Bottom = 136
               Right = 417
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "t"
            Begin Extent = 
               Top = 138
               Left = 38
               Bottom = 268
               Right = 208
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vw_StockProductoTipo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vw_StockProductoTipo'
GO
