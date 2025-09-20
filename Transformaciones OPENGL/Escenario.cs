using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transformaciones_OPENGL
{
    public class Escenario
    {
        public Dictionary<string, Objeto> Objetos { get; set; }

        public Punto centroGeometrico = new Punto(0, 0, 0); // Para rotación

        public Punto centroMasa = new Punto(0, 0, 0);       // Para posición de dibujo

        public Escenario()
        {
            Objetos = new Dictionary<string, Objeto>();
        }

        public Escenario(Dictionary<string, Objeto> objetos, Punto centroMasa)
        {
            Objetos = objetos;
            this.centroMasa = centroMasa;
        }

        public void AgregarObjeto(string clave, Objeto objeto)
        {
            Objetos.Add(clave, objeto);
        }

        public void EliminarObjeto(string clave)
        {
            Objetos.Remove(clave);
        }

        public Objeto ObtenerObjeto(string clave)
        {
            return Objetos[clave];
        }

        public void Dibujar()
        {
            foreach (var objeto in Objetos.Values)
            {
                objeto.centroMasa = centroMasa;
                objeto.Dibujar();
            }
        }


        public void CalcularCentroGeometrico()
        {
            if (Objetos.Count == 0) return;

            float sumaX = 0, sumaY = 0, sumaZ = 0;

            foreach (var objeto in Objetos.Values)
            {
                objeto.CalcularCentroGeometrico();

                sumaX += objeto.centroGeometrico.X;
                sumaY += objeto.centroGeometrico.Y;
                sumaZ += objeto.centroGeometrico.Z;
            }

            centroGeometrico.X = sumaX / Objetos.Count;
            centroGeometrico.Y = sumaY / Objetos.Count;
            centroGeometrico.Z = sumaZ / Objetos.Count;
        }

        public void Rotar(float angulo, Vector3 eje)
        {
            CalcularCentroGeometrico();

            foreach (var objeto in Objetos.Values)
            {
                objeto.Rotar(angulo, eje, centroGeometrico);
            }
        }
        public void Trasladar(float x, float y, float z)
        {
            foreach (var objeto in Objetos.Values)
            {
                objeto.Trasladar(x, y, z);
            }
        }

        public void Escalar(float escala)
        {
            CalcularCentroGeometrico();

            foreach (var objeto in Objetos.Values)
            {
                objeto.Escalar(escala, centroGeometrico);
            }
        }
        public void Reflexionar(Vector3 eje)
        {
            foreach (var objeto in Objetos.Values)
            {
                objeto.Reflexionar(eje);
            }
        }

        public void ResetearTransformaciones()
        {
            foreach (var objeto in Objetos.Values)
            {
                objeto.ResetearTransformaciones();
            }
        }
    }
}
