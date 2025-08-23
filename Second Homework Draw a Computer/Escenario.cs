using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Mathematics;

namespace Second_Homework_Draw_a_Computer
{
    public class Escenario
    {
        public Dictionary<string, Objeto> Objetos { get; set; }

        public Vector3 centroMasa { get; set; } = default;

        public Escenario()
        {
            Objetos = new Dictionary<string, Objeto>();
        }

        public void AgregarObjeto(string clave, Objeto objeto)
        {
            Objetos[clave] = objeto;
        }

        public void Dibujar()
        {
            foreach (var objeto in Objetos.Values)
            {
                objeto.centroMasa = centroMasa;
                objeto.Dibujar();
            }
        }

    }
}
