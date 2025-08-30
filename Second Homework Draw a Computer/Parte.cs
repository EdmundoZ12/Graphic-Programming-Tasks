using System;
using System.Collections.Generic;
using OpenTK.Mathematics;


namespace Second_Homework_Draw_a_Computer
{
    public class Parte
    {
        public Dictionary<string, Poligono> Poligonos { get; set; }
        public Vector3 centroMasa { get; set; } = default;

        public Parte()
        {
            Poligonos = new Dictionary<string, Poligono>();
        }

        public void AgregarPoligono(string clave, Poligono poligono)
        {
            Poligonos[clave] = poligono;//Arreglar con Add
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