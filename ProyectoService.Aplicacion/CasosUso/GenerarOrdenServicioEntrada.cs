using ProyectoService.Aplicacion.ICasosUso;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.ComponentModel.DataAnnotations.Schema;
using ProyectoService.LogicaNegocio.IRepositorios;
using ProyectoService.LogicaNegocio.Modelo;

namespace ProyectoService.Aplicacion.CasosUso
{
    public class GenerarOrdenServicioEntrada : IGenerarOrdenServicioEntrada
    {
        private readonly IEmpresaRepositorio repoEmpresa;
        public GenerarOrdenServicioEntrada(IEmpresaRepositorio repoEmpresa)
        {
            this.repoEmpresa = repoEmpresa;
        }

        public async Task<byte[]> Ejecutar(Reparacion rep,Empresa emp, Sucursal suc, byte[] foto)
        {
            byte[] imageData = foto;
            var data = Document.Create(document =>
            {
                document.Page(page =>
                {
                    page.Margin(30);
                    page.Header().ShowOnce().Row(row =>
                    {
                        row.ConstantItem(150).Image(imageData);
                        row.RelativeItem().Column(col =>
                        { // estos van a ser datos de la empresa. 
                            col.Item().AlignCenter().Text(emp.NombreFantasia.ToUpper()).Bold().FontSize(14);
                            col.Item().AlignCenter().Text(suc.Direccion).FontSize(9);
                            col.Item().AlignCenter().Text(suc.Telefono).FontSize(9);
                            col.Item().AlignCenter().Text(suc.Email).FontSize(9);
                        });
                        row.RelativeItem().Column(col =>
                        {
                            col.Item().Border(1).BorderColor("#257272")
                            .AlignCenter().Text("Numero de Orden");
                            //ID
                            col.Item().Background("#257272").Border(1)
                            .BorderColor("#257272").AlignCenter()
                            .Text(rep.Id.ToString()).FontColor("#fff");
                            //FECHA DE INGRESO
                            col.Item().Border(1).BorderColor("#257272")
                            .AlignCenter().Text(rep.Fecha.ToString("dd-MM-yyyy"));
                        });
                    });
                    page.Content().PaddingVertical(10).Column(col1 =>
                    { //DATOS DEL CLIENTE
                        col1.Item().Column(col2 =>
                        {
                            col2.Item().Text("Datos del cliente").Underline().Bold();
                            col2.Item().Text(txt =>
                            {
                                txt.Span("Nombre: ").SemiBold().FontSize(10);
                                txt.Span(rep.Cliente.Nombre + " " + rep.Cliente.Apellido).FontSize(10);
                            });
                            col2.Item().Text(txt =>
                            {
                                txt.Span("Celular/Telefono: ").SemiBold().FontSize(10);
                                txt.Span(rep.Cliente.Telefono).FontSize(10);
                            });

                            col2.Item().Text(txt =>
                            {
                                txt.Span("Email: ").SemiBold().FontSize(10);
                                txt.Span(rep.Cliente.Email.Value).FontSize(10);
                            });
                            col2.Item().Text(txt =>
                            {
                                txt.Span("Direccion: ").SemiBold().FontSize(10);
                                txt.Span(rep.Cliente.Direccion).FontSize(10);
                            });
                        });
                        col1.Item().LineHorizontal(0.5f);
                        col1.Item().Table(tabla =>
                        {
                            tabla.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn(3);
                                columns.RelativeColumn(3);
                                columns.RelativeColumn(5);
                                columns.RelativeColumn(2);

                            });
                            tabla.Header(header =>
                            {
                                header.Cell().Background("#257272")
                                .Padding(2).Text("Producto").FontColor("#fff");
                                header.Cell().Background("#257272")
                                .Padding(2).Text("Numero de serie").FontColor("#fff");
                                header.Cell().Background("#257272")
                                .Padding(2).Text("Descripcion problema").FontColor("#fff");
                                header.Cell().Background("#257272")
                               .Padding(2).Text("Tecnico").FontColor("#fff");

                            });
                            tabla.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9")
                            .Padding(2).Text(rep.Producto.Marca + " " + rep.Producto.Modelo + " " + rep.Producto.Version).FontSize(10);
                            tabla.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9")
                            .Padding(2).Text(rep.NumeroSerie).FontSize(10);
                            tabla.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9")
                            .Padding(2).Text(rep.Descripcion).FontSize(10);
                            tabla.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9")
                            .Padding(2).Text(rep.Tecnico.Nombre).FontSize(10);
                        });
                        if(rep.Estado == "Presupuestada" || rep.Estado == "Terminada")
                        {
                            col1.Item().Table(tabla =>
                            {
                                tabla.ColumnsDefinition(columns =>
                                {
                                    columns.RelativeColumn(6);
                                    columns.RelativeColumn(4);
                                    columns.RelativeColumn(2);
                                });
                                tabla.Header(header =>
                                {
                                    header.Cell().Background("#257272")
                                    .Padding(2).Text("Detalles presupuesto").FontColor("#fff");
                                    header.Cell().Background("#257272")
                                    .Padding(2).Text("Fecha aproximada de entrega").FontColor("#fff");
                                    header.Cell().Background("#257272")
                                    .Padding(2).Text("Costo").FontColor("#fff");
                                });
                                tabla.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9")
                                .Padding(2).Text(rep.DescripcionPresupuesto).FontSize(10);
                                tabla.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9")
                                .Padding(2).Text(rep.FechaPromesaEntrega.ToString("dd-MM-yyyy")).FontSize(10);
                                tabla.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9")
                                .Padding(2).Text(rep.ManoDeObra.ToString()).FontSize(10);
                            });
                        }
                        if (rep.Estado == "Entregada")
                        {
                            col1.Item().LineHorizontal(0.5f);
                            col1.Item().Table(tabla =>
                            {
                                tabla.ColumnsDefinition(columns =>
                                {
                                    columns.RelativeColumn(3);
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                });
                                tabla.Header(header =>
                                {
                                    header.Cell().Background("#257272")
                                    .Padding(2).Text("Estado de la reparacion").FontColor("#fff");
                                    header.Cell().Background("#257272")
                                    .Padding(2).Text("Fecha de entrega").FontColor("#fff");
                                    header.Cell().Background("#257272")
                                    .Padding(2).Text("Costo final").FontColor("#fff");
                                });
                                var estadoreparacion = "Reparada";
                                if (!rep.Reparada)
                                {
                                    estadoreparacion = "No reparada";
                                }
                                tabla.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9")
                                .Padding(2).Text(estadoreparacion).FontSize(10);
                                tabla.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9")
                                .Padding(2).Text(rep.FechaEntrega.ToString("dd-MM-yyyy")).FontSize(10);
                                tabla.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9")
                                .Padding(2).Text(rep.CostoFinal.ToString()).FontSize(10);
                            });

                        }
                            col1.Item().Background(Colors.Grey.Lighten3).Padding(10)
                            .Column(column =>
                            {
                                column.Item().Text("Politicas").FontSize(14);
                                //aca va algun atributo de empresa con sus politicas
                                column.Item().Text(emp.PoliticasEmpresa);
                                column.Spacing(5);
                            });

                        col1.Spacing(10);
                    });

                    page.Footer()
                    .AlignRight()
                    .Text(txt =>
                    {
                        txt.Span("Pagina ").FontSize(10);
                        txt.CurrentPageNumber().FontSize(10);
                        txt.Span(" de ").FontSize(10);
                        txt.TotalPages().FontSize(10);
                    });
                });
            }).GeneratePdf();

            return data;
        }
    }
}
