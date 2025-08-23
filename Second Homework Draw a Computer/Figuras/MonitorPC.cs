using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Mathematics;
using Second_Homework_Draw_a_Computer; // AGREGAR ESTA LÍNEA

namespace Second_Homework_Draw_a_Computer.Componentes
{
    public class MonitorPC
    {
        Objeto monitor;

        public MonitorPC()
        {
            monitor = new Objeto();
            CrearMonitor();
        }

        public Objeto ObtenerObjeto()
        {
            return monitor;
        }

        private void CrearMonitor()
        {
            // Crear pantalla 
            Parte pantalla = new Parte();
            CrearPantalla(pantalla);
            monitor.AgregarParte("pantalla", pantalla);

            // Crear base del monitor
            Parte baseMonitor = new Parte();
            CrearBase(baseMonitor);
            monitor.AgregarParte("baseMonitor", baseMonitor);
        }

        private void CrearPantalla(Parte pantalla)
        {
            // Cara trasera (negra)
            Poligono caraTrasera = new Poligono();
            caraTrasera.Color = new Vector3(0.1f, 0.1f, 0.1f);
            caraTrasera.AgregarPunto(new Punto(-0.4f, 0.3f, -0.03f));  // arriba izq
            caraTrasera.AgregarPunto(new Punto(0.4f, 0.3f, -0.03f));   // arriba der
            caraTrasera.AgregarPunto(new Punto(-0.4f, -0.3f, -0.03f)); // abajo izq
            caraTrasera.AgregarPunto(new Punto(0.4f, -0.3f, -0.03f));  // abajo der
            caraTrasera.SetIndices(0, 1, 2, 1, 3, 2);
            pantalla.AgregarPoligono("caraTrasera", caraTrasera);

            // Cara frontal (pantalla blanca)
            Poligono caraFrontal = new Poligono();
            caraFrontal.Color = new Vector3(1f, 1f, 1f);
            caraFrontal.AgregarPunto(new Punto(-0.35f, 0.25f, 0.03f));   // arriba izq
            caraFrontal.AgregarPunto(new Punto(0.35f, 0.25f, 0.03f));    // arriba der
            caraFrontal.AgregarPunto(new Punto(-0.35f, -0.25f, 0.03f));  // abajo izq
            caraFrontal.AgregarPunto(new Punto(0.35f, -0.25f, 0.03f));   // abajo der
            caraFrontal.SetIndices(0, 1, 2, 1, 3, 2);
            pantalla.AgregarPoligono("caraFrontal", caraFrontal);

            // Marco superior
            Poligono marcoSuperior = new Poligono();
            marcoSuperior.Color = new Vector3(0.2f, 0.2f, 0.2f); 
            marcoSuperior.AgregarPunto(new Punto(-0.4f, 0.3f, -0.03f)); // tras izq
            marcoSuperior.AgregarPunto(new Punto(0.4f, 0.3f, -0.03f));  // tras der
            marcoSuperior.AgregarPunto(new Punto(-0.35f, 0.25f, 0.03f));  // front izq
            marcoSuperior.AgregarPunto(new Punto(0.35f, 0.25f, 0.03f));   // front der
            marcoSuperior.SetIndices(0, 1, 2, 1, 3, 2);
            pantalla.AgregarPoligono("marcoSuperior", marcoSuperior);

            // Marco inferior
            Poligono marcoInferior = new Poligono();
            marcoInferior.Color = new Vector3(0.2f, 0.2f, 0.2f); 
            marcoInferior.AgregarPunto(new Punto(-0.35f, -0.25f, 0.03f)); // front izq
            marcoInferior.AgregarPunto(new Punto(0.35f, -0.25f, 0.03f));  // front der
            marcoInferior.AgregarPunto(new Punto(-0.4f, -0.3f, -0.03f)); // tras izq
            marcoInferior.AgregarPunto(new Punto(0.4f, -0.3f, -0.03f));  // tras der
            marcoInferior.SetIndices(0, 1, 2, 1, 3, 2);
            pantalla.AgregarPoligono("marcoInferior", marcoInferior);

            // Marco izquierdo
            Poligono marcoIzquierdo = new Poligono();
            marcoIzquierdo.Color = new Vector3(0.2f, 0.2f, 0.2f);
            marcoIzquierdo.AgregarPunto(new Punto(-0.4f, 0.3f, -0.03f)); // tras arriba
            marcoIzquierdo.AgregarPunto(new Punto(-0.35f, 0.25f, 0.03f));  // front arriba
            marcoIzquierdo.AgregarPunto(new Punto(-0.4f, -0.3f, -0.03f)); // tras abajo
            marcoIzquierdo.AgregarPunto(new Punto(-0.35f, -0.25f, 0.03f)); // front abajo
            marcoIzquierdo.SetIndices(0, 1, 2, 1, 3, 2);
            pantalla.AgregarPoligono("marcoIzquierdo", marcoIzquierdo);

            // Marco derecho
            Poligono marcoDerecho = new Poligono();
            marcoDerecho.Color = new Vector3(0.2f, 0.2f, 0.2f); 
            marcoDerecho.AgregarPunto(new Punto(0.35f, 0.25f, 0.03f));  // front arriba
            marcoDerecho.AgregarPunto(new Punto(0.4f, 0.3f, -0.03f)); // tras arriba
            marcoDerecho.AgregarPunto(new Punto(0.35f, -0.25f, 0.03f)); // front abajo
            marcoDerecho.AgregarPunto(new Punto(0.4f, -0.3f, -0.03f)); // tras abajo
            marcoDerecho.SetIndices(0, 1, 2, 1, 3, 2);
            pantalla.AgregarPoligono("marcoDerecho", marcoDerecho);
        }

        private void CrearBase(Parte baseMonitor)
        {
            // Base superior 
            Poligono baseSuperior = new Poligono();
            baseSuperior.Color = new Vector3(0.3f, 0.3f, 0.3f);
            baseSuperior.AgregarPunto(new Punto(-0.15f, -0.3f, 0.1f));
            baseSuperior.AgregarPunto(new Punto(0.15f, -0.3f, 0.1f));
            baseSuperior.AgregarPunto(new Punto(-0.15f, -0.3f, -0.1f));
            baseSuperior.AgregarPunto(new Punto(0.15f, -0.3f, -0.1f));
            baseSuperior.SetIndices(0, 1, 2, 1, 3, 2);
            baseMonitor.AgregarPoligono("baseSuperior", baseSuperior);

            // Base inferior
            Poligono baseInferior = new Poligono();
            baseInferior.Color = new Vector3(0.25f, 0.25f, 0.25f);
            baseInferior.AgregarPunto(new Punto(-0.15f, -0.35f, 0.1f));
            baseInferior.AgregarPunto(new Punto(0.15f, -0.35f, 0.1f));
            baseInferior.AgregarPunto(new Punto(-0.15f, -0.35f, -0.1f));
            baseInferior.AgregarPunto(new Punto(0.15f, -0.35f, -0.1f));
            baseInferior.SetIndices(0, 1, 2, 1, 3, 2);
            baseMonitor.AgregarPoligono("baseInferior", baseInferior);

            // Frente de la base
            Poligono baseFrente = new Poligono();
            baseFrente.Color = new Vector3(0.28f, 0.28f, 0.28f);
            baseFrente.AgregarPunto(new Punto(-0.15f, -0.3f, 0.1f));
            baseFrente.AgregarPunto(new Punto(0.15f, -0.3f, 0.1f));
            baseFrente.AgregarPunto(new Punto(-0.15f, -0.35f, 0.1f));
            baseFrente.AgregarPunto(new Punto(0.15f, -0.35f, 0.1f));
            baseFrente.SetIndices(0, 1, 2, 1, 3, 2);
            baseMonitor.AgregarPoligono("baseFrente", baseFrente);

            // Frente de la base
            Poligono baseTrasera = new Poligono();
            baseTrasera.Color = new Vector3(0.28f, 0.28f, 0.28f);
            baseTrasera.AgregarPunto(new Punto(-0.15f, -0.3f, -0.1f));
            baseTrasera.AgregarPunto(new Punto(0.15f, -0.3f, -0.1f));
            baseTrasera.AgregarPunto(new Punto(-0.15f, -0.35f, -0.1f));
            baseTrasera.AgregarPunto(new Punto(0.15f, -0.35f, -0.1f));
            baseTrasera.SetIndices(0, 1, 2, 1, 3, 2);
            baseMonitor.AgregarPoligono("baseTrasera", baseTrasera);
        }
    }
}