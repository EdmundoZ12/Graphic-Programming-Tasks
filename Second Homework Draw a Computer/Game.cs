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
using Second_Homework_Draw_a_Computer.Componentes;

namespace Second_Homework_Draw_a_Computer
{
    internal class Game : GameWindow
    {
        // Render Pipeline vars
        int shaderProgram;

        // Escenario
        Escenario escena;

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

            // Crear el escenario usando nuestras clases
            CrearEscena();
        }

        private void CrearEscena()
        {
            escena = new Escenario();

            //escena.centroMasa=new Vector3(0.5f, 0.5f, 0.5f);

            MonitorPC monitorComponent = new MonitorPC();
            escena.AgregarObjeto("monitor", monitorComponent.ObtenerObjeto());

            CPU cpu = new CPU();
            escena.AgregarObjeto("CPU", cpu.ObtenerObjeto());

            Teclado tecladoComponent = new Teclado();
            escena.AgregarObjeto("teclado", tecladoComponent.ObtenerObjeto());

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

            escena.Dibujar();

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