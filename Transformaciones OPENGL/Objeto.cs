using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transformaciones_OPENGL
{
    public class Objeto
    {
        public Dictionary<string, Parte> Partes;

        public Punto centroGeometrico = new Punto(0, 0, 0); // Para rotación

        public Punto centroMasa = new Punto(0, 0, 0);       // Para posición de dibujo

        public Objeto()
        {
            Partes = new Dictionary<string, Parte>();
        }
        public Objeto(Dictionary<string, Parte> partes, Punto centroMasa)
        {
            Partes = partes;
            this.centroMasa = centroMasa;
        }

        public void AgregarParte(string clave, Parte parte)
        {
            Partes.Add(clave, parte);
        }

        public void EliminarParte(String clave)
        {
            Partes.Remove(clave);
        }

        public Parte ObtenerParte(String clave)
        {
            return Partes[clave];
        }

        public void Dibujar()
        {
            foreach (var parte in Partes.Values)
            {
                parte.centroMasa = centroMasa;
                parte.Dibujar();
            }
        }

        public void CalcularCentroGeometrico()
        {
            if (Partes.Count == 0) return;

            float sumaX = 0, sumaY = 0, sumaZ = 0;

            foreach (var parte in Partes.Values)
            {
                parte.CalcularCentroGeometrico();

                sumaX += parte.centroGeometrico.X;
                sumaY += parte.centroGeometrico.Y;
                sumaZ += parte.centroGeometrico.Z;
            }

            centroGeometrico.X = sumaX / Partes.Count;
            centroGeometrico.Y = sumaY / Partes.Count;
            centroGeometrico.Z = sumaZ / Partes.Count;
        }

        public void Rotar(float angulo, Vector3 eje, Punto puntoRotacion = null)
        {
            if (puntoRotacion == null)
            {
                CalcularCentroGeometrico();
                puntoRotacion = centroGeometrico;
            }

            foreach (var parte in Partes.Values)
            {
                parte.Rotar(angulo, eje, puntoRotacion);
            }
        }

        public void Trasladar(float x, float y, float z)
        {
            foreach (var parte in Partes.Values)
            {
                parte.Trasladar(x, y, z);
            }
        }

        public void Escalar(float escala, Punto origen = null)
        {
            if (origen == null)
            {
                CalcularCentroGeometrico();
                origen = centroGeometrico;
            }

            foreach (var parte in Partes.Values)
            {
                parte.Escalar(escala, origen);
            }
        }

        public void Reflexionar(Vector3 eje, Punto plano = null)
        {
            foreach (var parte in Partes.Values)
            {
                if (plano == null)
                    parte.Reflexionar(eje);
                else
                    parte.Reflexionar(eje, plano);
            }
        }

        public void ResetearTransformaciones()
        {
            foreach (var parte in Partes.Values)
            {
                parte.ResetearTransformaciones();
            }
        }
    }
}
