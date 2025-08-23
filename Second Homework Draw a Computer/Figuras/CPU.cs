using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Mathematics;
using Second_Homework_Draw_a_Computer;

namespace Second_Homework_Draw_a_Computer.Componentes
{
    public class CPU
    {
        Objeto cpu;

        public CPU()
        {
            cpu = new Objeto();
            CrearCPU();
        }

        public Objeto ObtenerObjeto()
        {
            return cpu;
        }

        private void CrearCPU()
        {
            // Crear gabinete
            Parte gabinete = new Parte();
            CrearGabinete(gabinete);
            cpu.AgregarParte("gabinete", gabinete);
        }

        private void CrearGabinete(Parte gabinete)
        {
            // Cara frontal
            Poligono caraFrontal = new Poligono();
            caraFrontal.Color = new Vector3(0.15f, 0.15f, 0.15f);
            caraFrontal.AgregarPunto(new Punto(0.6f, 0.4f, 0.25f));   // arriba izq
            caraFrontal.AgregarPunto(new Punto(0.9f, 0.4f, 0.25f));    // arriba der
            caraFrontal.AgregarPunto(new Punto(0.6f, -0.35f, 0.25f)); // abajo izq
            caraFrontal.AgregarPunto(new Punto(0.9f, -0.35f, 0.25f));  // abajo der
            caraFrontal.SetIndices(0, 1, 2, 1, 3, 2);
            gabinete.AgregarPoligono("caraFrontal", caraFrontal);

            // Cara trasera
            Poligono caraTrasera = new Poligono();
            caraTrasera.Color = new Vector3(0.1f, 0.1f, 0.1f); 
            caraTrasera.AgregarPunto(new Punto(0.9f, 0.4f, -0.25f));   // arriba der
            caraTrasera.AgregarPunto(new Punto(0.6f, 0.4f, -0.25f));  // arriba izq
            caraTrasera.AgregarPunto(new Punto(0.9f, -0.35f, -0.25f)); // abajo der
            caraTrasera.AgregarPunto(new Punto(0.6f, -0.35f, -0.25f)); // abajo izq
            caraTrasera.SetIndices(0, 1, 2, 1, 3, 2);
            gabinete.AgregarPoligono("caraTrasera", caraTrasera);

            // Cara izquierda
            Poligono caraIzquierda = new Poligono();
            caraIzquierda.Color = new Vector3(0.12f, 0.12f, 0.12f);
            caraIzquierda.AgregarPunto(new Punto(0.6f, 0.4f, -0.25f));  // arriba tras
            caraIzquierda.AgregarPunto(new Punto(0.6f, 0.4f, 0.25f));   // arriba front
            caraIzquierda.AgregarPunto(new Punto(0.6f, -0.35f, -0.25f)); // abajo tras
            caraIzquierda.AgregarPunto(new Punto(0.6f, -0.35f, 0.25f));  // abajo front
            caraIzquierda.SetIndices(0, 1, 2, 1, 3, 2);
            gabinete.AgregarPoligono("caraIzquierda", caraIzquierda);

            // Cara derecha
            Poligono caraDerecha = new Poligono();
            caraDerecha.Color = new Vector3(0.12f, 0.12f, 0.12f); 
            caraDerecha.AgregarPunto(new Punto(0.9f, 0.4f, 0.25f));   // arriba front
            caraDerecha.AgregarPunto(new Punto(0.9f, 0.4f, -0.25f));  // arriba tras
            caraDerecha.AgregarPunto(new Punto(0.9f, -0.35f, 0.25f)); // abajo front
            caraDerecha.AgregarPunto(new Punto(0.9f, -0.35f, -0.25f)); // abajo tras
            caraDerecha.SetIndices(0, 1, 2, 1, 3, 2);
            gabinete.AgregarPoligono("caraDerecha", caraDerecha);

            // Cara superior
            Poligono caraSuperior = new Poligono();
            caraSuperior.Color = new Vector3(0.18f, 0.18f, 0.18f);
            caraSuperior.AgregarPunto(new Punto(0.6f, 0.4f, -0.25f)); // izq tras
            caraSuperior.AgregarPunto(new Punto(0.9f, 0.4f, -0.25f));  // der tras
            caraSuperior.AgregarPunto(new Punto(0.6f, 0.4f, 0.25f));  // izq front
            caraSuperior.AgregarPunto(new Punto(0.9f, 0.4f, 0.25f));   // der front
            caraSuperior.SetIndices(0, 1, 2, 1, 3, 2);
            gabinete.AgregarPoligono("caraSuperior", caraSuperior);

            // Cara inferior
            Poligono caraInferior = new Poligono();
            caraInferior.Color = new Vector3(0.08f, 0.08f, 0.08f);
            caraInferior.AgregarPunto(new Punto(0.6f, -0.35f, 0.25f));  // izq front
            caraInferior.AgregarPunto(new Punto(0.9f, -0.35f, 0.25f));   // der front
            caraInferior.AgregarPunto(new Punto(0.6f, -0.35f, -0.25f)); // izq tras
            caraInferior.AgregarPunto(new Punto(0.9f, -0.35f, -0.25f));  // der tras
            caraInferior.SetIndices(0, 1, 2, 1, 3, 2);
            gabinete.AgregarPoligono("caraInferior", caraInferior);

            // Panel frontal (botón de encendido)
            Poligono panelFrontal = new Poligono();
            panelFrontal.Color = new Vector3(0.0f, 0.5f, 0.0f); // Verde
            panelFrontal.AgregarPunto(new Punto(0.7f, 0.3f, 0.26f));  // pequeño cuadrado
            panelFrontal.AgregarPunto(new Punto(0.8f, 0.3f, 0.26f));
            panelFrontal.AgregarPunto(new Punto(0.7f, 0.2f, 0.26f));
            panelFrontal.AgregarPunto(new Punto(0.8f, 0.2f, 0.26f));
            panelFrontal.SetIndices(0, 1, 2, 1, 3, 2);
            gabinete.AgregarPoligono("panelFrontal", panelFrontal);
        }
    }
}