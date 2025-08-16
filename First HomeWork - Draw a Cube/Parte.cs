using System;
using System.Collections.Generic;

namespace First_HomeWork___Draw_a_Cube
{
    public class Parte
    {
        public Dictionary<string, Poligono> Poligonos { get; set; }

        public Parte()
        {
            Poligonos = new Dictionary<string, Poligono>();
        }

        public void AgregarPoligono(string clave, Poligono poligono)
        {
            Poligonos[clave] = poligono;
        }

        public void Dibujar()
        {
            foreach (var poligono in Poligonos.Values)
            {
                poligono.Dibujar();
            }
        }
    }
}