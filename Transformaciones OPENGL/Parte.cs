using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transformaciones_OPENGL
{
    public class Parte
    {
        public Dictionary<string, Poligono> Poligonos { get; set; }

        public Punto centroGeometrico = new Punto(0, 0, 0); // Para rotación

        public Punto centroMasa = new Punto(0, 0, 0);       // Para posición de dibujo

        public Parte()
        {
            Poligonos = new Dictionary<string, Poligono>();
        }

        public Parte(Dictionary<string, Poligono> poligonos, Punto centroMasa)
        {
            Poligonos = poligonos;
            this.centroMasa = centroMasa;
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

        public Poligono ObtenerPoligono(String clave)
        {
            return Poligonos[clave];
        }

        public void CalcularCentroGeometrico()
        {
            if (Poligonos.Count == 0) return;

            float sumaX = 0, sumaY = 0, sumaZ = 0;

            foreach (var poligono in Poligonos.Values)
            {
                poligono.CalcularCentroGeometrico();

                sumaX += poligono.centroGeometrico.X;
                sumaY += poligono.centroGeometrico.Y;
                sumaZ += poligono.centroGeometrico.Z;
            }

            centroGeometrico.X = sumaX / Poligonos.Count;
            centroGeometrico.Y = sumaY / Poligonos.Count;
            centroGeometrico.Z = sumaZ / Poligonos.Count;
        }
        public void Rotar(float angulo, Vector3 eje, Punto puntoRotacion = null)
        {
            if (puntoRotacion == null)
            {
                CalcularCentroGeometrico();
                puntoRotacion = centroGeometrico;
            }

            foreach (var poligono in Poligonos.Values)
            {
                poligono.Rotar(angulo, eje, puntoRotacion);
            }
        }

        public void Trasladar(float x, float y, float z)
        {
            foreach (var poligono in Poligonos.Values)
            {
                poligono.Trasladar(x, y, z);
            }
        }

        public void Escalar(float escala, Punto origen = null)
        {
            if (origen == null)
            {
                CalcularCentroGeometrico();
                origen = centroGeometrico;
            }

            foreach (var poligono in Poligonos.Values)
            {
                poligono.Escalar(escala, origen);
            }
        }

        public void Reflexionar(Vector3 eje, Punto plano = null)
        {

            foreach (var poligono in Poligonos.Values)
            {
                if (plano == null)
                {
                    poligono.Reflexionar(eje);
                }
                else
                {
                    poligono.Reflexionar(eje, plano);
                }
            }
        }

        public void ResetearTransformaciones()
        {
            foreach (var poligono in Poligonos.Values)
            {
                poligono.ResetearTransformaciones();
            }
        }
    }
}
