using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Graphics.OpenGL4;

namespace First_HomeWork___Draw_a_Cube
{
    internal class Game : GameWindow
    {
        // Render Pipeline vars
        int shaderProgram;

        // Figura
        Parte cubo;

        // Variables de transformación
        Matrix4 model = Matrix4.Identity;
        Matrix4 view = Matrix4.Identity;
        Matrix4 projection = Matrix4.Identity;

        // Variables del mouse
        Vector2 lastMousePos;
        float rotationX = 0f;
        float rotationY = 0f;
        bool firstMouse = true;
        bool isMousePressed = false;
        float sensitivity = 0.002f;

        // CONSTANTS
        int height, width;

        public Game(int width, int height) : base(GameWindowSettings.Default, NativeWindowSettings.Default)
        {
            this.height = height;
            this.width = width;
            // center the window on monitor
            this.CenterWindow(new Vector2i(width, height));
        }

        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);
            GL.Viewport(0, 0, e.Width, e.Height);
            this.height = e.Height;
            this.width = e.Width;

            // Actualizar matriz de proyección
            projection = Matrix4.CreatePerspectiveFieldOfView(
                MathHelper.DegreesToRadians(45f),
                (float)e.Width / e.Height,
                0.1f,
                100f);
        }

        protected override void OnLoad()
        {
            base.OnLoad();

            // create the shader program
            shaderProgram = GL.CreateProgram();

            int vertexShader = GL.CreateShader(ShaderType.VertexShader);
            string vertexShaderSource = LoadShaderSource("Default.vert");
            GL.ShaderSource(vertexShader, vertexShaderSource);
            GL.CompileShader(vertexShader);

            GL.GetShader(vertexShader, ShaderParameter.CompileStatus, out int success);
            if (success == 0)
            {
                string infoLog = GL.GetShaderInfoLog(vertexShader);
                Console.WriteLine("ERROR: Vertex Shader compilation failed: " + infoLog);
            }

            int fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
            string fragmentShaderSource = LoadShaderSource("Default.frag");
            GL.ShaderSource(fragmentShader, fragmentShaderSource);
            GL.CompileShader(fragmentShader);

            GL.GetShader(fragmentShader, ShaderParameter.CompileStatus, out success);
            if (success == 0)
            {
                string infoLog = GL.GetShaderInfoLog(fragmentShader);
                Console.WriteLine("ERROR: Fragment Shader compilation failed: " + infoLog);
            }

            GL.AttachShader(shaderProgram, vertexShader);
            GL.AttachShader(shaderProgram, fragmentShader);

            GL.LinkProgram(shaderProgram);

            GL.GetProgram(shaderProgram, GetProgramParameterName.LinkStatus, out success);
            if (success == 0)
            {
                string infoLog = GL.GetProgramInfoLog(shaderProgram);
                Console.WriteLine("ERROR: Shader Program linking failed: " + infoLog);
            }

            // delete the shaders
            GL.DeleteShader(vertexShader);
            GL.DeleteShader(fragmentShader);

            // Configurar matrices
            view = Matrix4.LookAt(Vector3.UnitZ * 3, Vector3.Zero, Vector3.UnitY);
            projection = Matrix4.CreatePerspectiveFieldOfView(
                MathHelper.DegreesToRadians(45f),
                (float)width / height,
                0.1f,
                100f);

            // Habilitar depth test
            GL.Enable(EnableCap.DepthTest);

            // Deshabilitar backface culling para ver todas las caras
            GL.Disable(EnableCap.CullFace);

            // Crear el cubo usando nuestras clases
            CrearCubo();
        }

        private void CrearCubo()
        {
            cubo = new Parte();

            // Cara frontal
            Poligono caraFrontal = new Poligono();
            caraFrontal.Color = new Vector3(1.0f, 0.0f, 0.0f); // Rojo
            caraFrontal.AgregarPunto(new Punto(-0.5f, 0.5f, 0.5f));   // arriba izq
            caraFrontal.AgregarPunto(new Punto(0.5f, 0.5f, 0.5f));    // arriba der
            caraFrontal.AgregarPunto(new Punto(-0.5f, -0.5f, 0.5f));  // abajo izq
            caraFrontal.AgregarPunto(new Punto(0.5f, 0.5f, 0.5f));    // arriba der
            caraFrontal.AgregarPunto(new Punto(0.5f, -0.5f, 0.5f));   // abajo der
            caraFrontal.AgregarPunto(new Punto(-0.5f, -0.5f, 0.5f));  // abajo izq
            cubo.AgregarPoligono("caraFrontal", caraFrontal);

            // Cara trasera
            Poligono caraTrasera = new Poligono();
            caraTrasera.Color = new Vector3(0.0f, 0.0f, 1.0f); // Azul
            caraTrasera.AgregarPunto(new Punto(0.5f, 0.5f, -0.5f));   // arriba der
            caraTrasera.AgregarPunto(new Punto(-0.5f, 0.5f, -0.5f));  // arriba izq
            caraTrasera.AgregarPunto(new Punto(0.5f, -0.5f, -0.5f));  // abajo der
            caraTrasera.AgregarPunto(new Punto(-0.5f, 0.5f, -0.5f));  // arriba izq
            caraTrasera.AgregarPunto(new Punto(-0.5f, -0.5f, -0.5f)); // abajo izq
            caraTrasera.AgregarPunto(new Punto(0.5f, -0.5f, -0.5f));  // abajo der
            cubo.AgregarPoligono("caraTrasera", caraTrasera);

            // Cara izquierda
            Poligono caraIzquierda = new Poligono();
            caraIzquierda.Color = new Vector3(0.0f, 1.0f, 0.0f); // Verde
            caraIzquierda.AgregarPunto(new Punto(-0.5f, 0.5f, -0.5f)); // arriba tras
            caraIzquierda.AgregarPunto(new Punto(-0.5f, 0.5f, 0.5f));  // arriba front
            caraIzquierda.AgregarPunto(new Punto(-0.5f, -0.5f, -0.5f)); // abajo tras
            caraIzquierda.AgregarPunto(new Punto(-0.5f, 0.5f, 0.5f));  // arriba front
            caraIzquierda.AgregarPunto(new Punto(-0.5f, -0.5f, 0.5f)); // abajo front
            caraIzquierda.AgregarPunto(new Punto(-0.5f, -0.5f, -0.5f)); // abajo tras
            cubo.AgregarPoligono("caraIzquierda", caraIzquierda);

            // Cara derecha
            Poligono caraDerecha = new Poligono();
            caraDerecha.Color = new Vector3(1.0f, 1.0f, 0.0f); // Amarillo
            caraDerecha.AgregarPunto(new Punto(0.5f, 0.5f, 0.5f));   // arriba front
            caraDerecha.AgregarPunto(new Punto(0.5f, 0.5f, -0.5f));  // arriba tras
            caraDerecha.AgregarPunto(new Punto(0.5f, -0.5f, 0.5f));  // abajo front
            caraDerecha.AgregarPunto(new Punto(0.5f, 0.5f, -0.5f));  // arriba tras
            caraDerecha.AgregarPunto(new Punto(0.5f, -0.5f, -0.5f)); // abajo tras
            caraDerecha.AgregarPunto(new Punto(0.5f, -0.5f, 0.5f));  // abajo front
            cubo.AgregarPoligono("caraDerecha", caraDerecha);

            // Cara superior
            Poligono caraSuperior = new Poligono();
            caraSuperior.Color = new Vector3(1.0f, 0.0f, 1.0f); // Magenta
            caraSuperior.AgregarPunto(new Punto(-0.5f, 0.5f, -0.5f)); // izq tras
            caraSuperior.AgregarPunto(new Punto(0.5f, 0.5f, -0.5f));  // der tras
            caraSuperior.AgregarPunto(new Punto(-0.5f, 0.5f, 0.5f));  // izq front
            caraSuperior.AgregarPunto(new Punto(0.5f, 0.5f, -0.5f));  // der tras
            caraSuperior.AgregarPunto(new Punto(0.5f, 0.5f, 0.5f));   // der front
            caraSuperior.AgregarPunto(new Punto(-0.5f, 0.5f, 0.5f));  // izq front
            cubo.AgregarPoligono("caraSuperior", caraSuperior);

            // Cara inferior
            Poligono caraInferior = new Poligono();
            caraInferior.Color = new Vector3(0.0f, 1.0f, 1.0f); // Cian
            caraInferior.AgregarPunto(new Punto(-0.5f, -0.5f, 0.5f));  // izq front
            caraInferior.AgregarPunto(new Punto(0.5f, -0.5f, 0.5f));   // der front
            caraInferior.AgregarPunto(new Punto(-0.5f, -0.5f, -0.5f)); // izq tras
            caraInferior.AgregarPunto(new Punto(0.5f, -0.5f, 0.5f));   // der front
            caraInferior.AgregarPunto(new Punto(0.5f, -0.5f, -0.5f));  // der tras
            caraInferior.AgregarPunto(new Punto(-0.5f, -0.5f, -0.5f)); // izq tras
            cubo.AgregarPoligono("caraInferior", caraInferior);
        }

        protected override void OnUnload()
        {
            base.OnUnload();
            GL.DeleteProgram(shaderProgram);
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            GL.ClearColor(0.6f, 0.3f, 1f, 1f);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            // Actualizar rotación
            model = Matrix4.CreateRotationX(rotationX) * Matrix4.CreateRotationY(rotationY);

            // Draw Figures
            GL.UseProgram(shaderProgram);

            // Enviar matrices al shader
            int modelLoc = GL.GetUniformLocation(shaderProgram, "model");
            int viewLoc = GL.GetUniformLocation(shaderProgram, "view");
            int projLoc = GL.GetUniformLocation(shaderProgram, "projection");

            GL.UniformMatrix4(modelLoc, false, ref model);
            GL.UniformMatrix4(viewLoc, false, ref view);
            GL.UniformMatrix4(projLoc, false, ref projection);

            cubo.Dibujar();

            Context.SwapBuffers();
            base.OnRenderFrame(args);
        }

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);

            // Salir con ESC
            if (KeyboardState.IsKeyDown(Keys.Escape))
            {
                Close();
            }
        }

        protected override void OnMouseMove(MouseMoveEventArgs e)
        {
            if (isMousePressed)
            {
                if (firstMouse)
                {
                    lastMousePos = new Vector2(e.X, e.Y);
                    firstMouse = false;
                }

                Vector2 deltaPos = new Vector2(e.X, e.Y) - lastMousePos;
                lastMousePos = new Vector2(e.X, e.Y);

                rotationY += deltaPos.X * sensitivity;
                rotationX += deltaPos.Y * sensitivity;

                // Sin límites de rotación - libertad total
            }

            base.OnMouseMove(e);
        }

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            if (e.Button == MouseButton.Left)
            {
                isMousePressed = true;
                firstMouse = true;
            }
            base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            if (e.Button == MouseButton.Left)
            {
                isMousePressed = false;
            }
            base.OnMouseUp(e);
        }

        // Function to load a text file and return its contents as a string
        public static string LoadShaderSource(string filePath)
        {
            string shaderSource = "";

            try
            {
                using (StreamReader reader = new StreamReader("../../../Shaders/" + filePath))
                {
                    shaderSource = reader.ReadToEnd();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to load shader source file: " + e.Message);
            }

            return shaderSource;
        }
    }
}