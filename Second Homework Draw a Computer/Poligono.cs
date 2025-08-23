using System;
using System.Collections.Generic;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace Second_Homework_Draw_a_Computer
{
    public class Poligono
    {
        public List<Punto> Puntos { get; set; }
        public List<uint> Indices { get; set; }
        public Vector3 Color { get; set; } = new Vector3(0, 1, 0); // Verde por defecto

        public Vector3 centroMasa { get; set; } = default;

        public Poligono()
        {
            Puntos = new List<Punto>();
            Indices = new List<uint>();
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

        public float[] GetVerticesArray()
        {
            List<float> vertices = new List<float>();

            foreach (var punto in Puntos)
            {
                vertices.Add(punto.X + centroMasa.X);
                vertices.Add(punto.Y + centroMasa.Y);
                vertices.Add(punto.Z + centroMasa.Z);
            }

            return vertices.ToArray();
        }

        public void Dibujar()
        {
            if (Puntos.Count < 3 || Indices.Count < 3) return; // Necesita al menos 3 puntos e índices

            float[] vertices = GetVerticesArray();

            // Generar buffers
            int vao = GL.GenVertexArray();
            int vbo = GL.GenBuffer();
            int ebo = GL.GenBuffer();

            GL.BindVertexArray(vao);

            // VBO - datos de vértices
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);

            // EBO - datos de índices
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ebo);
            GL.BufferData(BufferTarget.ElementArrayBuffer, Indices.Count * sizeof(uint), Indices.ToArray(), BufferUsageHint.StaticDraw);

            // Configurar atributos
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            // Enviar color al shader
            int colorLocation = GL.GetUniformLocation(GL.GetInteger(GetPName.CurrentProgram), "color");
            GL.Uniform3(colorLocation, Color);

            // Dibujar usando índices
            GL.DrawElements(PrimitiveType.Triangles, Indices.Count, DrawElementsType.UnsignedInt, 0);

            // Limpiar buffers
            GL.DeleteBuffer(vbo);
            GL.DeleteBuffer(ebo);
            GL.DeleteVertexArray(vao);
        }
    }
}