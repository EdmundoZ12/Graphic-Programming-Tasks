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
        public Dictionary<string, Parte> Partes { get; set; }

        public Vector3 centroMasa { get; set; } = default;

        public Objeto()
        {
            Partes = new Dictionary<string, Parte>();
        }

        public void AgregarParte(string clave, Parte parte)
        {
            Partes[clave] = parte;
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
