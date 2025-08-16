using System;
using System.Collections.Generic;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace First_HomeWork___Draw_a_Cube
{
    public class Poligono
    {
        public List<Punto> Puntos { get; set; }
        public Vector3 Color { get; set; } = new Vector3(0, 1, 0); // Verde por defecto

        public Poligono()
        {
            Puntos = new List<Punto>();
        }

        public void AgregarPunto(Punto punto)
        {
            Puntos.Add(punto);
        }

        public float[] GetVerticesArray()
        {
            List<float> vertices = new List<float>();

            foreach (var punto in Puntos)
            {
                vertices.Add(punto.X);
                vertices.Add(punto.Y);
                vertices.Add(punto.Z);
            }

            return vertices.ToArray();
        }

        public void Dibujar()
        {
            if (Puntos.Count < 3) return; // Necesita al menos 3 puntos para un triángulo

            float[] vertices = GetVerticesArray();

            // Generar buffers temporales para esta parte
            int vao = GL.GenVertexArray();
            int vbo = GL.GenBuffer();

            GL.BindVertexArray(vao);
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);

            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            // Enviar color al shader
            int colorLocation = GL.GetUniformLocation(GL.GetInteger(GetPName.CurrentProgram), "color");
            GL.Uniform3(colorLocation, Color);

            // Dibujar como triángulos (asumiendo que los puntos están en orden correcto)
            GL.DrawArrays(PrimitiveType.Triangles, 0, Puntos.Count);

            // Limpiar buffers temporales
            GL.DeleteBuffer(vbo);
            GL.DeleteVertexArray(vao);
        }
    }
}