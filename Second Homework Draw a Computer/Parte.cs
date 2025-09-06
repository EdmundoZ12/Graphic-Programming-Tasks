using System;
using System.Collections.Generic;
using OpenTK.Mathematics;


namespace Second_Homework_Draw_a_Computer
{
    public class Parte
    {
        public Dictionary<string, Poligono> Poligonos { get; set; }
        public Punto centroMasa { get; set; } = new Punto(0, 0, 0);

        public Parte()
        {
            Poligonos = new Dictionary<string, Poligono>();
        }

        public void AgregarPoligono(string clave, Poligono poligono)
        {
            Poligonos.Add(clave, poligono);
        }

        public void Dibujar()
        {
            foreach (var poligono in Poligonos.Values)
            {
                poligono.centroMasa = centroMasa;
                poligono.Dibujar();
            }
        }
    }
}