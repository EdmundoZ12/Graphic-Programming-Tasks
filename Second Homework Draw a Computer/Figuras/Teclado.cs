using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Mathematics;

namespace Second_Homework_Draw_a_Computer.Componentes
{
    public class Teclado
    {
        Objeto teclado;

        public Teclado()
        {
            teclado = new Objeto();
            CrearTeclado();
        }

        public Objeto ObtenerObjeto()
        {
            return teclado;
        }

        private void CrearTeclado()
        {
            // Base del teclado
            Parte baseTeclado = new Parte();
            CrearBase(baseTeclado);
            teclado.AgregarParte("baseTeclado", baseTeclado);

            // Teclas
            Parte teclas = new Parte();
            CrearTeclas(teclas);
            teclado.AgregarParte("teclas", teclas);
        }

        private void CrearBase(Parte baseTeclado)
        {
            // Cara superior (donde van las teclas)
            Poligono caraSuperior = new Poligono();
            caraSuperior.Color = new Vector3(0.2f, 0.2f, 0.2f); 
            caraSuperior.AgregarPunto(new Punto(-0.5f, -0.4f, 0.4f));  // izq front
            caraSuperior.AgregarPunto(new Punto(0.5f, -0.4f, 0.4f));   // der front
            caraSuperior.AgregarPunto(new Punto(-0.5f, -0.4f, 0.05f)); // izq tras
            caraSuperior.AgregarPunto(new Punto(0.5f, -0.4f, 0.05f));  // der tras
            caraSuperior.SetIndices(0, 1, 2, 1, 3, 2);
            baseTeclado.AgregarPoligono("caraSuperior", caraSuperior);

            // Cara inferior
            Poligono caraInferior = new Poligono();
            caraInferior.Color = new Vector3(0.15f, 0.15f, 0.15f); 
            caraInferior.AgregarPunto(new Punto(-0.5f, -0.45f, 0.4f));  // izq front
            caraInferior.AgregarPunto(new Punto(0.5f, -0.45f, 0.4f));   // der front
            caraInferior.AgregarPunto(new Punto(-0.5f, -0.45f, 0.05f)); // izq tras
            caraInferior.AgregarPunto(new Punto(0.5f, -0.45f, 0.05f));  // der tras
            caraInferior.SetIndices(0, 1, 2, 1, 3, 2);
            baseTeclado.AgregarPoligono("caraInferior", caraInferior);

            // Cara frontal
            Poligono caraFrontal = new Poligono();
            caraFrontal.Color = new Vector3(0.18f, 0.18f, 0.18f); 
            caraFrontal.AgregarPunto(new Punto(-0.5f, -0.4f, 0.4f));  // arriba izq
            caraFrontal.AgregarPunto(new Punto(0.5f, -0.4f, 0.4f));   // arriba der
            caraFrontal.AgregarPunto(new Punto(-0.5f, -0.45f, 0.4f)); // abajo izq
            caraFrontal.AgregarPunto(new Punto(0.5f, -0.45f, 0.4f));  // abajo der
            caraFrontal.SetIndices(0, 1, 2, 1, 3, 2);
            baseTeclado.AgregarPoligono("caraFrontal", caraFrontal);

            // Cara trasera
            Poligono caraTrasera = new Poligono();
            caraTrasera.Color = new Vector3(0.18f, 0.18f, 0.18f); 
            caraTrasera.AgregarPunto(new Punto(0.5f, -0.4f, 0.05f));   // arriba der
            caraTrasera.AgregarPunto(new Punto(-0.5f, -0.4f, 0.05f));  // arriba izq
            caraTrasera.AgregarPunto(new Punto(0.5f, -0.45f, 0.05f));  // abajo der
            caraTrasera.AgregarPunto(new Punto(-0.5f, -0.45f, 0.05f)); // abajo izq
            caraTrasera.SetIndices(0, 1, 2, 1, 3, 2);
            baseTeclado.AgregarPoligono("caraTrasera", caraTrasera);

            // Lados izquierdo y derecho
            Poligono ladoIzquierdo = new Poligono();
            ladoIzquierdo.Color = new Vector3(0.18f, 0.18f, 0.18f);
            ladoIzquierdo.AgregarPunto(new Punto(-0.5f, -0.4f, 0.05f)); // arriba tras
            ladoIzquierdo.AgregarPunto(new Punto(-0.5f, -0.4f, 0.4f));  // arriba front
            ladoIzquierdo.AgregarPunto(new Punto(-0.5f, -0.45f, 0.05f)); // abajo tras
            ladoIzquierdo.AgregarPunto(new Punto(-0.5f, -0.45f, 0.4f)); // abajo front
            ladoIzquierdo.SetIndices(0, 1, 2, 1, 3, 2);
            baseTeclado.AgregarPoligono("ladoIzquierdo", ladoIzquierdo);

            Poligono ladoDerecho = new Poligono();
            ladoDerecho.Color = new Vector3(0.18f, 0.18f, 0.18f);
            ladoDerecho.AgregarPunto(new Punto(0.5f, -0.4f, 0.4f));   // arriba front
            ladoDerecho.AgregarPunto(new Punto(0.5f, -0.4f, 0.05f));  // arriba tras
            ladoDerecho.AgregarPunto(new Punto(0.5f, -0.45f, 0.4f));  // abajo front
            ladoDerecho.AgregarPunto(new Punto(0.5f, -0.45f, 0.05f)); // abajo tras
            ladoDerecho.SetIndices(0, 1, 2, 1, 3, 2);
            baseTeclado.AgregarPoligono("ladoDerecho", ladoDerecho);
        }

        private void CrearTeclas(Parte teclas)
        {
            // Crear algunas teclas representativas
            float teclaAncho = 0.06f;
            float teclaAlto = 0.06f;
            float espaciado = 0.08f;

            // Fila de teclas QWERTY
            string[] fila1 = { "Q", "W", "E", "R", "T", "Y", "U", "I", "O", "P" };
            for (int i = 0; i < fila1.Length; i++)
            {
                float x = -0.36f + (i * espaciado);
                CrearTecla(teclas, fila1[i], x, -0.40f, 0.18f, teclaAncho, teclaAlto);
            }

            // Fila de teclas ASDF
            string[] fila2 = { "A", "S", "D", "F", "G", "H", "J", "K", "L" };
            for (int i = 0; i < fila2.Length; i++)
            {
                float x = -0.32f + (i * espaciado);
                CrearTecla(teclas, fila2[i], x, -0.40f, 0.25f, teclaAncho, teclaAlto);
            }

            // Barra espaciadora
            CrearTecla(teclas, "SPACE", 0f, -0.40f, 0.35f, 0.3f, teclaAlto);
        }

        private void CrearTecla(Parte teclas, string nombre, float x, float y, float z, float ancho, float alto)
        {
            Poligono tecla = new Poligono();
            tecla.Color = new Vector3(0.8f, 0.8f, 0.8f);

            float mitadAncho = ancho / 2;
            float mitadAlto = alto / 2;

            tecla.AgregarPunto(new Punto(x - mitadAncho, y + 0.001f, z + mitadAlto)); // arriba izq
            tecla.AgregarPunto(new Punto(x + mitadAncho, y + 0.001f, z + mitadAlto)); // arriba der
            tecla.AgregarPunto(new Punto(x - mitadAncho, y + 0.001f, z - mitadAlto)); // abajo izq
            tecla.AgregarPunto(new Punto(x + mitadAncho, y + 0.001f, z - mitadAlto)); // abajo der
            tecla.SetIndices(0, 1, 2, 1, 3, 2);

            teclas.AgregarPoligono(nombre, tecla);
        }
    }
}