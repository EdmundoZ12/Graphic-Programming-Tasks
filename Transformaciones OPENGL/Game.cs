using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL;
using OpenTK.Graphics;
using OpenTK.Input;
using Transformaciones_OPENGL.Componentes;

namespace Transformaciones_OPENGL
{
    public class Game : GameWindow
    {
        Escenario escenario;
        int contador = 1; // 1=Rotar, 2=Trasladar, 3=Escalar, 4=Reflexionar
        bool teclaPresionada = false;
        Objeto o;
        public Game(int width, int height, string title) : base(width, height, GraphicsMode.Default, title) { }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            GL.ClearColor(0.1f, 0.1f, 0.2f, 1.0f);
            GL.Enable(EnableCap.DepthTest);

            ConfigurarProyeccion();

            ConfigurarCamara();

            CargarEscenario();
        }

        private void ConfigurarProyeccion()
        {
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();

            float aspectRatio = (float)Width / Height;
            float fovy = 45.0f * (float)Math.PI / 180.0f;
            float top = (float)Math.Tan(fovy / 2.0f) * 0.1f;
            float bottom = -top;
            float right = top * aspectRatio;
            float left = -right;

            GL.Frustum(left, right, bottom, top, 0.1, 100.0);
        }

        private void ConfigurarCamara()
        {
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();
            GL.Translate(0.0f, 0.0f, -4.0f);
        }

        private void CargarEscenario()
        {
            //escenario = new Escenario();
            //escenario.AgregarObjeto("CPU", new CPU().ObtenerObjeto());
            //escenario.AgregarObjeto("MONITOR", new MonitorPC().ObtenerObjeto());
            //o = new MonitorPC().ObtenerObjeto();
            //o.centroMasa = new Punto(1f, 0, 0);
            //escenario.AgregarObjeto("TECLADO", new Teclado().ObtenerObjeto());

            //Serializar.GuardarComoJson(escenario, "escenario.json");

            escenario = Serializar.CargarDesdeJson<Escenario>("escenario.json");
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            GL.Viewport(0, 0, Width, Height);
            ConfigurarProyeccion();
            ConfigurarCamara();
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            if (escenario != null)
            {
                escenario.Dibujar();
            }

            //o.Dibujar();

            SwapBuffers();
        }

        protected override void OnUnload(EventArgs e)
        {
            base.OnUnload(e);
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);
            var keyboard = Keyboard.GetState();

            if (keyboard[Key.Space] && !teclaPresionada)
            {
                contador++;
                if (contador > 4) contador = 1;
                teclaPresionada = true;
            }
            if (!keyboard[Key.Space])
                teclaPresionada = false;

            string modo = "";
            switch (contador)
            {
                case 1: modo = "ROTACIÓN"; break;
                case 2: modo = "TRASLACIÓN"; break;
                case 3: modo = "ESCALADO"; break;
                case 4: modo = "REFLEXIÓN"; break;
            }
            this.Title = $"Transformaciones - Modo {contador}: {modo}";

            if (contador == 1) 
            {
                if (keyboard[Key.X])
                {
                    escenario.Rotar(2.0f, new Vector3(1, 0, 0)); // X
                }
                if (keyboard[Key.Y])
                {
                    escenario.Rotar(2.0f, new Vector3(0, 1, 0)); // Y
                }

                if (keyboard[Key.Z])
                {
                    escenario.Rotar(2.0f, new Vector3(0, 0, 1)); // Z
                }
            }
            else if (contador == 2) 
            {
                if (keyboard[Key.X])
                {
                    escenario.Trasladar(0.05f, 0, 0); // X positivo 
                }
                if (keyboard[Key.Y])
                {
                    escenario.Trasladar(0, 0.05f, 0); // Y positivo
                }
                if (keyboard[Key.Z])
                {
                    escenario.Trasladar(0, 0, 0.05f); // Z positivo
                }
                if (keyboard[Key.A])
                {
                    escenario.Trasladar(-0.05f, 0, 0); // X negativo
                }
                if (keyboard[Key.B])
                {
                    escenario.Trasladar(0, -0.05f, 0); // Y negativo
                }
                if (keyboard[Key.C])
                {
                    escenario.Trasladar(0, 0, -0.05f); // Z negativo
                }
            }
            else if (contador == 3) 
            {
                if (keyboard[Key.Plus] || keyboard[Key.KeypadPlus])
                {
                    escenario.Escalar(1.02f); // Agrandar
                }
                if (keyboard[Key.Minus] || keyboard[Key.KeypadMinus])
                {
                    escenario.Escalar(0.98f); // Achicar
                }
            }
            else if (contador == 4)
            {
                if (keyboard[Key.X])
                {
                    escenario.Reflexionar(new Vector3(1, 0, 0)); // X
                }
                if (keyboard[Key.Y])
                {
                    escenario.Reflexionar(new Vector3(0, 1, 0)); // Y
                }
                if (keyboard[Key.Z])
                {
                    escenario.Reflexionar(new Vector3(0, 0, 1)); // Z
                }
            }

            // Reset
            if (keyboard[Key.R])
                escenario.ResetearTransformaciones();
        }
    }
}