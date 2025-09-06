using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Mathematics;

namespace Second_Homework_Draw_a_Computer
{
    public class Objeto
    {
        // Implementar  interface Dibujar(), Rotar,Trasladar.

        public Dictionary<string, Parte> Partes { get; set; }

        public Punto centroMasa { get; set; } = new Punto(0, 0, 0);
        //Cambiar a clase Punto
        public Objeto()
        {
            Partes = new Dictionary<string, Parte>();
        }

        public void AgregarParte(string clave, Parte parte)
        {
            Partes.Add(clave, parte);
        }

        public void EliminarParte(String clave)
        {
            Partes.Remove(clave);
        }

        public void Dibujar()
        {
            foreach (var parte in Partes.Values)
            {
                parte.centroMasa = centroMasa;
                parte.Dibujar();
            }
        }
    }
}
