using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Second_Homework_Draw_a_Computer
{
    public class Serializar
    {
        static string path = "../../../ArchivosJson/";

        public static void GuardarComoJson<T>(T objeto, string nombreArchivo)
        {
            try
            {
                string json = JsonConvert.SerializeObject(objeto, Formatting.Indented);
                string ruta = Path.Combine(path, nombreArchivo);
                File.WriteAllText(ruta, json);
                Console.WriteLine($"Archivo guardado en: {ruta}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al guardar el archivo: {ex.Message}");
            }
        }

        public static T CargarDesdeJson<T>(string nombreArchivo)
        {
            try
            {
                string ruta = Path.Combine(path, nombreArchivo);
                if (!File.Exists(ruta))
                {
                    Console.WriteLine("El archivo no existe en la ruta especificada.");
                    return default;
                }
                string json = File.ReadAllText(ruta);
                return JsonConvert.DeserializeObject<T>(json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al cargar el archivo: {ex.Message}");
                return default;
            }
        }
    }
}