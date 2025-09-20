using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Transformaciones_OPENGL
{
    public class Poligono
    {
        public List<Punto> Puntos { get; set; }
        public List<uint> Indices { get; set; }
        public Punto Color { get; set; } = new Punto(0, 0, 0);

        [Newtonsoft.Json.JsonIgnore]
        public Matrix4 matrizTransformacion { get; set; } = Matrix4.Identity;

        public Punto centroGeometrico = new Punto(0, 0, 0); // Para rotación
        public Punto centroMasa = new Punto(0, 0, 0);       // Para posición de dibujo

        public Poligono()
        {
            Puntos = new List<Punto>();
            Indices = new List<uint>();
            matrizTransformacion = Matrix4.Identity;
        }

        public Poligono(List<Punto> puntos, List<uint> indices, Punto color, Punto centroMasa)
        {
            Puntos = puntos;
            Indices = indices;
            Color = color;
            this.centroMasa = centroMasa;
            matrizTransformacion = Matrix4.Identity;
        }

        public void AgregarPunto(Punto punto)
        {
            Puntos.Add(punto);
        }

        public void AgregarIndice(uint indice)
        {
            Indices.Add(indice);
        }

        public void SetIndices(params uint[] indices)
        {
            Indices.Clear();
            Indices.AddRange(indices);
        }

        public void Dibujar()
        {
            if (Puntos.Count == 0 || Indices.Count == 0) return;

            if (Indices.Count % 3 != 0) return;

            GL.Color3(Color.X, Color.Y, Color.Z);

            GL.Begin(PrimitiveType.Triangles);

            for (int i = 0; i < Indices.Count; i += 3)
            {
                for (int j = 0; j < 3; j++)
                {
                    uint indice = Indices[i + j];
                    if (indice < Puntos.Count)
                    {
                        Punto punto = Puntos[(int)indice];

                        float x = punto.X + centroMasa.X;
                        float y = punto.Y + centroMasa.Y;
                        float z = punto.Z + centroMasa.Z;

                        Vector4 puntoOriginal = new Vector4(x, y, z, 1.0f);
                        Vector4 puntoTransformado = Vector4.Transform(puntoOriginal, matrizTransformacion);

                        GL.Vertex3(puntoTransformado.X, puntoTransformado.Y, puntoTransformado.Z);
                    }
                }
            }

            GL.End();
        }

        public void CalcularCentroGeometrico()
        {
            if (Puntos.Count == 0) return;

            float sumaX = 0, sumaY = 0, sumaZ = 0;

            foreach (var punto in Puntos)
            {
                sumaX += punto.X;
                sumaY += punto.Y;
                sumaZ += punto.Z;
            }

            centroGeometrico.X = sumaX / Puntos.Count;
            centroGeometrico.Y = sumaY / Puntos.Count;
            centroGeometrico.Z = sumaZ / Puntos.Count;
        }

        public void Rotar(float angulo, Vector3 eje, Punto origen = null)
        {
            if (origen == null)
            {
                CalcularCentroGeometrico();
                origen = centroGeometrico;
            }

            float radianes = angulo * (float)Math.PI / 180.0f;

            Matrix4 Tp=Matrix4.CreateTranslation(-origen.X,-origen.Y,-origen.Z);
            Matrix4 R=Matrix4.Identity;

            if (eje.X == 1)
                R = Matrix4.CreateRotationX(radianes);
            else if (eje.Y == 1)
                R = Matrix4.CreateRotationY(radianes);
            else if (eje.Z == 1)
                R = Matrix4.CreateRotationZ(radianes);

            Matrix4 T = Matrix4.CreateTranslation(origen.X, origen.Y, origen.Z);

            // Combinar transformaciones: Trasladar al origen → Rotar → Regresar
            Matrix4 transformacion = Tp * R * T;

            matrizTransformacion = transformacion * matrizTransformacion;
        }


        public void Trasladar(float x, float y, float z)
        {
            Matrix4 traslacion = Matrix4.CreateTranslation(x, y, z);
            matrizTransformacion = traslacion * matrizTransformacion;
        }

        public void Escalar(float escala, Punto origen = null)
        {
            if (origen == null)
            {
                CalcularCentroGeometrico();
                origen = centroGeometrico;
            }

            Matrix4 Tp = Matrix4.CreateTranslation(-origen.X, -origen.Y, -origen.Z);
            Matrix4 S = Matrix4.CreateScale(escala, escala, escala);
            Matrix4 T = Matrix4.CreateTranslation(origen.X, origen.Y, origen.Z);

            Matrix4 transformacion = Tp * S * T;

            matrizTransformacion = transformacion * matrizTransformacion;
        }

        public void ResetearTransformaciones()
        {
            matrizTransformacion = Matrix4.Identity;
        }

        public void Reflexionar(Vector3 eje, Punto plano = null)
        {
            if (plano == null)
            {
                CalcularCentroGeometrico();
                plano = centroGeometrico;
            }

            Matrix4 reflexion = Matrix4.Identity;

            if (eje.X == 1) // Reflexión en X
            {
                reflexion = new Matrix4(
                    -1, 0, 0, 0,
                     0, 1, 0, 0,
                     0, 0, 1, 0,
                     0, 0, 0, 1
                );
            }
            else if (eje.Y == 1) // Reflexión en Y
            {
                reflexion = new Matrix4(
                     1, 0, 0, 0,
                     0, -1, 0, 0,
                     0, 0, 1, 0,
                     0, 0, 0, 1
                );
            }
            else if (eje.Z == 1) // Reflexión en Z
            {
                reflexion = new Matrix4(
                     1, 0, 0, 0,
                     0, 1, 0, 0,
                     0, 0, -1, 0,
                     0, 0, 0, 1
                );
            }

            matrizTransformacion = reflexion * matrizTransformacion;

        }
    }
}