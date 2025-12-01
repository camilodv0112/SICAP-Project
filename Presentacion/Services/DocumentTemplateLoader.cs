using System;
using System.Windows;
using System.Windows.Documents;

namespace Presentacion.Services
{
    /// <summary>
    /// Servicio para cargar plantillas de documentos dinámicamente
    /// </summary>
    public static class DocumentTemplateLoader
    {
        /// <summary>
        /// Carga una plantilla de documento desde un ResourceDictionary
        /// </summary>
        /// <param name="templateType">Tipo de plantilla a cargar</param>
        /// <returns>FlowDocument con la plantilla cargada</returns>
        public static FlowDocument LoadTemplate(DocumentTemplateType templateType)
        {
            string templatePath = GetTemplatePath(templateType);
            string resourceKey = GetResourceKey(templateType);

            try
            {
                // Cargar el ResourceDictionary CADA VEZ para obtener una instancia fresca
                // Esto preserva los bindings correctamente
                var dict = new ResourceDictionary();
                dict.Source = new Uri(templatePath, UriKind.Relative);

                // Obtener el FlowDocument del diccionario
                if (dict[resourceKey] is FlowDocument template)
                {
                    return template;
                }

                throw new InvalidOperationException($"No se encontró la plantilla '{resourceKey}' en '{templatePath}'");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al cargar la plantilla {templateType}: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Obtiene la ruta del archivo de plantilla
        /// </summary>
        private static string GetTemplatePath(DocumentTemplateType templateType)
        {
            return templateType switch
            {
                DocumentTemplateType.JustificacionEstudiante => 
                    "/Resources/DocumentTemplates/JustificacionEstudianteTemplate.xaml",
                DocumentTemplateType.SolicitudTransporte => 
                    "/Resources/DocumentTemplates/SolicitudTransporteTemplate.xaml",
                DocumentTemplateType.SolicitudRecursos => 
                    "/Resources/DocumentTemplates/SolicitudRecursosTemplate.xaml",
                DocumentTemplateType.SolicitudEspacios => 
                    "/Resources/DocumentTemplates/SolicitudEspaciosTemplate.xaml",
                _ => throw new ArgumentException($"Tipo de plantilla no soportado: {templateType}")
            };
        }

        /// <summary>
        /// Obtiene la clave del recurso en el diccionario
        /// </summary>
        private static string GetResourceKey(DocumentTemplateType templateType)
        {
            return templateType switch
            {
                DocumentTemplateType.JustificacionEstudiante => "JustificacionEstudianteTemplate",
                DocumentTemplateType.SolicitudTransporte => "SolicitudTransporteTemplate",
                DocumentTemplateType.SolicitudRecursos => "SolicitudRecursosTemplate",
                DocumentTemplateType.SolicitudEspacios => "SolicitudEspaciosTemplate",
                _ => throw new ArgumentException($"Tipo de plantilla no soportado: {templateType}")
            };
        }

    }

    /// <summary>
    /// Tipos de plantillas de documentos disponibles
    /// </summary>
    public enum DocumentTemplateType
    {
        JustificacionEstudiante,
        SolicitudTransporte,
        SolicitudRecursos,
        SolicitudEspacios
    }
}
