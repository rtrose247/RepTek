using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
//
using Assimp.Configs;
using Assimp;




//
namespace GLGUI.Example
{
	public class MainForm : GameWindow
	{
		GLGui glgui;
        GLLabel fpsLabel;
        GLLabel console;
        LineWriter consoleWriter;

        int fpsCounter = 0;
        int fpsSecond = 1;
        double time = 0.0;
        //
        private Scene m_model;
        private Vector3 m_sceneCenter, m_sceneMin, m_sceneMax;
        private float m_angle;
        private int m_displayList;
        private int m_texId;
        //
        private Hexgrid h = null;

        public MainForm() : base(1024, 600, new GraphicsMode(new ColorFormat(8, 8, 8, 8), 24, 0, 4), "GLGUI Example")
		{
            consoleWriter = new LineWriter();
            Console.SetOut(consoleWriter);
            Console.SetError(consoleWriter);

			this.Load += OnLoad;
			this.Resize += (s, e) => GL.Viewport(ClientSize);
			this.RenderFrame += OnRender;
            //
            String fileName = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "blade_runner_police_spinner_blue.rot.4.obj");

            AssimpContext importer = new AssimpContext();
            importer.SetConfig(new NormalSmoothingAngleConfig(66.0f));
            m_model = importer.ImportFile(fileName, PostProcessPreset.TargetRealTimeMaximumQuality);
            ComputeBoundingBox();
        }

        //============
        private void ComputeBoundingBox()
        {
            m_sceneMin = new Vector3(1e10f, 1e10f, 1e10f);
            m_sceneMax = new Vector3(-1e10f, -1e10f, -1e10f);
            Matrix4 identity = Matrix4.Identity;

            ComputeBoundingBox(m_model.RootNode, ref m_sceneMin, ref m_sceneMax, ref identity);

            m_sceneCenter.X = (m_sceneMin.X + m_sceneMax.X) / 2.0f;
            m_sceneCenter.Y = (m_sceneMin.Y + m_sceneMax.Y) / 2.0f;
            m_sceneCenter.Z = (m_sceneMin.Z + m_sceneMax.Z) / 2.0f;
        }

        private void ComputeBoundingBox(Node node, ref Vector3 min, ref Vector3 max, ref Matrix4 trafo)
        {
            Matrix4 prev = trafo;
            trafo = Matrix4.Mult(prev, FromMatrix(node.Transform));

            if (node.HasMeshes)
            {
                foreach (int index in node.MeshIndices)
                {
                    Mesh mesh = m_model.Meshes[index];
                    for (int i = 0; i < mesh.VertexCount; i++)
                    {
                        Vector3 tmp = FromVector(mesh.Vertices[i]);
                        Vector3.Transform(ref tmp, ref trafo, out tmp);

                        min.X = Math.Min(min.X, tmp.X);
                        min.Y = Math.Min(min.Y, tmp.Y);
                        min.Z = Math.Min(min.Z, tmp.Z);

                        max.X = Math.Max(max.X, tmp.X);
                        max.Y = Math.Max(max.Y, tmp.Y);
                        max.Z = Math.Max(max.Z, tmp.Z);
                    }
                }
            }

            for (int i = 0; i < node.ChildCount; i++)
            {
                ComputeBoundingBox(node.Children[i], ref min, ref max, ref trafo);
            }
            trafo = prev;
        }

        private void OnLoad(object sender, EventArgs e)
		{
			VSync = VSyncMode.Off; // vsync is nice, but you can't really measure performance while it's on

			glgui = new GLGui(this);
            
            var mainAreaControl = glgui.Add(new GLGroupLayout(glgui) { Size = new Size(ClientSize.Width, ClientSize.Height - 200), Anchor = GLAnchorStyles.All });
            // change background color:
            var mainSkin = mainAreaControl.Skin;
			mainSkin.BackgroundColor = glgui.Skin.FormActive.BackgroundColor;
			mainSkin.BorderColor = glgui.Skin.FormActive.BorderColor;
            mainAreaControl.Skin = mainSkin;

            //         //============
            //         //var consoleScrollControl = glgui.Add(new GLScrollableControl(glgui) { Outer = new Rectangle(0, ClientSize.Height - 200, ClientSize.Width, 200), Anchor = GLAnchorStyles.Left | GLAnchorStyles.Right | GLAnchorStyles.Bottom });
            //         //console = consoleScrollControl.Add(new GLLabel(glgui) { AutoSize = true, Multiline = true });

            fpsLabel = mainAreaControl.Add(new GLLabel(glgui) { Location = new Point(10, 10), AutoSize = true });
            // change font and background color:
            var skin = fpsLabel.SkinEnabled;
            skin.Font = new GLFont(new Font("Arial", 12.0f));
            skin.BackgroundColor = glgui.Skin.TextBoxActive.BackgroundColor;
            fpsLabel.SkinEnabled = skin;

            //         var helloWorldForm = mainAreaControl.Add(new GLForm(glgui) { Title = "Hello World", Outer = new Rectangle(50, 100, 200, 150), AutoSize = false });
            //         helloWorldForm.Add(new GLForm(glgui) { Title = "Hello Form", Outer = new Rectangle(100, 32, 100, 100), AutoSize = false })
            //             .MouseMove += (s, w) => Console.WriteLine(w.Position.ToString());

            //         var flow = helloWorldForm.Add(new GLFlowLayout(glgui) { FlowDirection = GLFlowDirection.BottomUp, Location = new Point(10, 10), Size = helloWorldForm.InnerSize, AutoSize = true });
            //         for (int i = 0; i < 5; i++)
            //             flow.Add(new GLButton(glgui) { Text = "Button" + i, Size = new Size(150, 0) })
            //                 .Click += (s, w) => Console.WriteLine(s + " pressed.");
            //flow.Add(new GLButton(glgui) { Text = "Hide Cursor", Size = new Size(150, 0) })
            //             .Click += (s, w) => glgui.Cursor = GLCursor.None;

            //         var loremIpsumForm = mainAreaControl.Add(new GLForm(glgui) { Title = "Lorem Ipsum", Location = new Point(600, 100), Size = new Size(300, 200) });
            //         loremIpsumForm.Add(new GLTextBox(glgui) {
            //             Text = "Lorem ipsum dolor sit amet,\nconsetetur sadipscing elitr,\nsed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat,\nsed diam voluptua.\n\nAt vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet. Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet.",
            //             Multiline = true, 
            //             WordWrap = true, 
            //             Outer = new Rectangle(4, 4, loremIpsumForm.Inner.Width - 8, loremIpsumForm.Inner.Height - 8), 
            //             Anchor = GLAnchorStyles.All 
            //         }).Changed += (s, w) => Console.WriteLine(s + " text length: " + ((GLTextBox)s).Text.Length);

            //         var fixedSizeForm = mainAreaControl.Add(new GLForm(glgui) { Title = "Fixed size Form", Location = new Point(64, 300), Size = new Size(100, 200), AutoSize = true });
            //         var fooBarFlow = fixedSizeForm.Add(new GLFlowLayout(glgui) { FlowDirection = GLFlowDirection.TopDown, AutoSize = true });
            //         fooBarFlow.Add(new GLCheckBox(glgui) { Text = "CheckBox A", AutoSize = true })
            //             .Changed += (s, w) => Console.WriteLine(s + ": " + ((GLCheckBox)s).Checked);
            //         fooBarFlow.Add(new GLCheckBox(glgui) { Text = "CheckBox B", AutoSize = true })
            //             .Changed += (s, w) => Console.WriteLine(s + ": " + ((GLCheckBox)s).Checked);
            //         fooBarFlow.Add(new GLCheckBox(glgui) { Text = "Totally different CheckBox", AutoSize = true })
            //             .Changed += (s, w) => Console.WriteLine(s + ": " + ((GLCheckBox)s).Checked);
            //         fooBarFlow.Add(new GLCheckBox(glgui) { Text = "Go away!", AutoSize = true })
            //             .Changed += (s, w) => Console.WriteLine(s + ": " + ((GLCheckBox)s).Checked);

            //background
            for (int i = 0; i < 1; i++)
            {
                var viewportForm = mainAreaControl.Add(new GLForm(glgui) { Title = "Cube", Location = new Point(300 + i * 16, 64 + i * 16), Size = new Size(400, 400) });
                viewportForm.Add(new GLViewport(glgui) { Size = viewportForm.InnerSize, Anchor = GLAnchorStyles.All })
                    .RenderViewport += OnCustomRenderBackground;
            }

            //middle-ground
            for (int i = 0; i < 1; i++)
            {
                var viewportForm = mainAreaControl.Add(new GLForm(glgui) { Title = "Cube", Location = new Point(300 + i * 16, 164 + i * 16), Size = new Size(400, 400) });
                viewportForm.Add(new GLViewport(glgui) { Size = viewportForm.InnerSize, Anchor = GLAnchorStyles.All })
                    .RenderViewport += OnCustomRenderMidground;
            }

            //foreground 
            for (int i = 0; i < 1; i++)
			{
                var viewportForm = mainAreaControl.Add(new GLForm(glgui) { Title = "Cube", Location = new Point(300 + i * 16, 364 + i * 16), Size = new Size(400, 400) });
                viewportForm.Add(new GLViewport(glgui) { Size = viewportForm.InnerSize, Anchor = GLAnchorStyles.All })
                    .RenderViewport += OnCustomRenderViewport;
			}

		}

        private void OnRender(object sender, FrameEventArgs e)
		{
			time += e.Time;

			if (time >= fpsSecond)
			{
				fpsLabel.Text = string.Format("Application: {0:0}FPS. GLGUI: {1:0.0}ms", fpsCounter, glgui.RenderDuration);
				fpsCounter = 0;
				fpsSecond++;
			}

			if (consoleWriter.Changed)
			{
				console.Text = string.Join("\n", consoleWriter.Lines);
				consoleWriter.Changed = false;
			}

			glgui.Render();
			SwapBuffers();

			fpsCounter++;
		}

        // draws a simple colored cube in a GLViewport control
        private void OnRenderViewport(object sender, double deltaTime)
        {
            var viewport = (GLViewport)sender;

            GL.Enable(EnableCap.DepthTest);
            GL.ClearColor(0, 0, 0, 1);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.MatrixMode(MatrixMode.Projection);
            var proj = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(90.0f), viewport.AspectRatio, 1.0f, 100.0f);
            GL.LoadMatrix(ref proj);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();

            GL.Translate(0, 0, -2.0f);
            GL.Rotate(time * 100.0f, 1, 0, 0);
            GL.Rotate(time * 42.0f, 0, 1, 0);

            //GL.Begin(Assimp.PrimitiveType.Quads);
            //GL.Color3(1.0, 0.0, 0.0);
            //GL.Vertex3(0.5, -0.5, -0.5);
            //GL.Color3(0.0, 1.0, 0.0);
            //GL.Vertex3(0.5, 0.5, -0.5);
            //GL.Color3(0.0, 0.0, 1.0);
            //GL.Vertex3(-0.5, 0.5, -0.5);
            //GL.Color3(1.0, 0.0, 1.0);
            //GL.Vertex3(-0.5, -0.5, -0.5);

            //GL.Color3(1.0, 1.0, 1.0);
            //GL.Vertex3(0.5, -0.5, 0.5);
            //GL.Vertex3(0.5, 0.5, 0.5);
            //GL.Vertex3(-0.5, 0.5, 0.5);
            //GL.Vertex3(-0.5, -0.5, 0.5);

            //GL.Color3(1.0, 0.0, 1.0);
            //GL.Vertex3(0.5, -0.5, -0.5);
            //GL.Vertex3(0.5, 0.5, -0.5);
            //GL.Vertex3(0.5, 0.5, 0.5);
            //GL.Vertex3(0.5, -0.5, 0.5);

            //GL.Color3(0.0, 1.0, 0.0);
            //GL.Vertex3(-0.5, -0.5, 0.5);
            //GL.Vertex3(-0.5, 0.5, 0.5);
            //GL.Vertex3(-0.5, 0.5, -0.5);
            //GL.Vertex3(-0.5, -0.5, -0.5);

            //GL.Color3(0.0, 0.0, 1.0);
            //GL.Vertex3(0.5, 0.5, 0.5);
            //GL.Vertex3(0.5, 0.5, -0.5);
            //GL.Vertex3(-0.5, 0.5, -0.5);
            //GL.Vertex3(-0.5, 0.5, 0.5);

            //GL.Color3(1.0, 0.0, 0.0);
            //GL.Vertex3(0.5, -0.5, -0.5);
            //GL.Vertex3(0.5, -0.5, 0.5);
            //GL.Vertex3(-0.5, -0.5, 0.5);
            //GL.Vertex3(-0.5, -0.5, -0.5);
            //GL.End();

            //GL.Disable(EnableCap.DepthTest);
        }


        private void OnCustomRenderViewport(object sender, double deltaTime)
        {
            var viewport = (GLViewport)sender;

            GL.Enable(EnableCap.DepthTest);
            //GL.ClearColor(0, 0, 0, 1);
            GL.Enable(EnableCap.ColorArray);
            GL.ClearColor(0, 0, 1, 1);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.MatrixMode(MatrixMode.Projection);
            var proj = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(90.0f), viewport.AspectRatio, 1.0f, 100.0f);
            GL.LoadMatrix(ref proj);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();

            GL.Translate(0, 0, -2.0f);
            GL.Rotate(time * 100.0f, 1, 0, 0);
            GL.Rotate(time * 42.0f, 0, 1, 0);
            //============
            //m_angle += 25f * (float)deltaTime;
            if (m_angle > 360)
            {
                m_angle = 0.0f;
            }
            //============
            GL.ClearColor(Color.CornflowerBlue);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.Enable(EnableCap.Texture2D);
            GL.Hint(HintTarget.PerspectiveCorrectionHint, HintMode.Nicest);
            GL.Enable(EnableCap.Lighting);
            GL.Enable(EnableCap.Light0);
            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.Normalize);
            GL.FrontFace(FrontFaceDirection.Ccw);

            GL.MatrixMode(MatrixMode.Modelview);
            Matrix4 lookat = Matrix4.LookAt(0, 5, 5, 0, 0, 0, 0, 1, 0);
            GL.LoadMatrix(ref lookat);

            GL.Rotate(m_angle, 0.0f, 1.0f, 0.0f);
            //============
            GL.Enable(EnableCap.DepthTest);
            GL.ClearColor(0, 0, 0, 1);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            //============
            float tmp = m_sceneMax.X - m_sceneMin.X;
            tmp = Math.Max(m_sceneMax.Y - m_sceneMin.Y, tmp);
            tmp = Math.Max(m_sceneMax.Z - m_sceneMin.Z, tmp);
            tmp = 1.0f / tmp;
            GL.Scale(tmp * 2, tmp * 2, tmp * 2);

            GL.Translate(-m_sceneCenter);

            if (m_displayList == 0)
            {
                m_displayList = GL.GenLists(1);
                GL.NewList(m_displayList, ListMode.Compile);
                RecursiveRender(m_model, m_model.RootNode);
                GL.EndList();
            }

            GL.CallList(m_displayList);

            //SwapBuffers();

        }

        //============
        private void OnCustomRenderBackground(object sender, double deltaTime)
        {
            var viewport = (GLViewport)sender;
            //
            GL.Enable(EnableCap.DepthTest);
            //GL.ClearColor(0, 0, 0, 1);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.MatrixMode(MatrixMode.Projection);
            var proj = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(90.0f), viewport.AspectRatio, 1.0f, 100.0f);
            GL.LoadMatrix(ref proj);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();

            GL.Translate(0, 0, -2.0f);
            GL.Rotate(time * 100.0f, 1, 0, 0);
            GL.Rotate(time * 42.0f, 0, 1, 0);
            //============
            //m_angle += 25f * (float)deltaTime;
            if (m_angle > 360)
            {
                m_angle = 0.0f;
            }
            //============
            GL.ClearColor(Color.CornflowerBlue);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.Enable(EnableCap.Texture2D);
            GL.Hint(HintTarget.PerspectiveCorrectionHint, HintMode.Nicest);
            GL.Enable(EnableCap.Lighting);
            GL.Enable(EnableCap.Light0);
            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.Normalize);
            GL.FrontFace(FrontFaceDirection.Ccw);

            GL.MatrixMode(MatrixMode.Modelview);
            Matrix4 lookat = Matrix4.LookAt(0, 5, 5, 0, 0, 0, 0, 1, 0);
            GL.LoadMatrix(ref lookat);

            GL.Rotate(m_angle, 0.0f, 1.0f, 0.0f);
            //============
            GL.Enable(EnableCap.DepthTest);
            GL.ClearColor(0, 0, 0, 1);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            //============
            float tmp = m_sceneMax.X - m_sceneMin.X;
            tmp = Math.Max(m_sceneMax.Y - m_sceneMin.Y, tmp);
            tmp = Math.Max(m_sceneMax.Z - m_sceneMin.Z, tmp);
            tmp = 1.0f / tmp;
            GL.Scale(tmp * 2, tmp * 2, tmp * 2);

            GL.Translate(-m_sceneCenter);

            if (m_displayList == 0)
            {
                m_displayList = GL.GenLists(1);
                GL.NewList(m_displayList, ListMode.Compile);
                RecursiveRender(m_model, m_model.RootNode);
                GL.EndList();
            }

            GL.CallList(m_displayList);
            //
            if (h == null)
            {
                h = new Hexgrid();
                h.Create();
            }

            h.Draw2();


        }


        private void OnCustomRenderMidground(object sender, double deltaTime)
        {
            var viewport = (GLViewport)sender;
            //
            GL.Enable(EnableCap.DepthTest);
            GL.ClearColor(0, 0, 0, 1);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.MatrixMode(MatrixMode.Projection);
            var proj = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(90.0f), viewport.AspectRatio, 1.0f, 100.0f);
            GL.LoadMatrix(ref proj);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();
            GL.Translate(0, 0, -2.0f);
            //
            if (h == null)
            {
                h = new Hexgrid();
                h.Create();
            }

            h.Draw2();

        }


        //============
        private void RecursiveRender(Scene scene, Node node)
        {
            Matrix4 m = FromMatrix(node.Transform);
            m.Transpose();
            GL.PushMatrix();
            GL.MultMatrix(ref m);

            if (node.HasMeshes)
            {
                foreach (int index in node.MeshIndices)
                {
                    Mesh mesh = scene.Meshes[index];
                    ApplyMaterial(scene.Materials[mesh.MaterialIndex]);

                    if (mesh.HasNormals)
                    {
                        GL.Enable(EnableCap.Lighting);
                    }
                    else
                    {
                        GL.Disable(EnableCap.Lighting);
                    }

                    bool hasColors = mesh.HasVertexColors(0);
                    if (hasColors)
                    {
                        GL.Enable(EnableCap.ColorMaterial);
                    }
                    else
                    {
                        GL.Disable(EnableCap.ColorMaterial);
                    }

                    bool hasTexCoords = mesh.HasTextureCoords(0);

                    foreach (Face face in mesh.Faces)
                    {
                        BeginMode faceMode;
                        switch (face.IndexCount)
                        {
                            case 1:
                                faceMode = BeginMode.Points;
                                break;
                            case 2:
                                faceMode = BeginMode.Lines;
                                break;
                            case 3:
                                faceMode = BeginMode.Triangles;
                                break;
                            default:
                                faceMode = BeginMode.Polygon;
                                break;
                        }

                        GL.Begin(faceMode);
                        for (int i = 0; i < face.IndexCount; i++)
                        {
                            int indice = face.Indices[i];
                            if (hasColors)
                            {
                                Color4 vertColor = FromColor(mesh.VertexColorChannels[0][indice]);
                            }
                            if (mesh.HasNormals)
                            {
                                Vector3 normal = FromVector(mesh.Normals[indice]);
                                GL.Normal3(normal);
                            }
                            if (hasTexCoords)
                            {
                                Vector3 uvw = FromVector(mesh.TextureCoordinateChannels[0][indice]);
                                GL.TexCoord2(uvw.X, 1 - uvw.Y);
                            }
                            Vector3 pos = FromVector(mesh.Vertices[indice]);
                            GL.Vertex3(pos);
                        }
                        GL.End();
                    }
                }
            }

            for (int i = 0; i < node.ChildCount; i++)
            {
                RecursiveRender(m_model, node.Children[i]);
            }
        }

        private void LoadTexture(String fileName)
        {
            fileName = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), fileName);
            if (!File.Exists(fileName))
            {
                return;
            }
            Bitmap textureBitmap = new Bitmap(fileName);
            BitmapData TextureData =
                    textureBitmap.LockBits(
                    new System.Drawing.Rectangle(0, 0, textureBitmap.Width, textureBitmap.Height),
                    System.Drawing.Imaging.ImageLockMode.ReadOnly,
                    System.Drawing.Imaging.PixelFormat.Format24bppRgb
                );
            m_texId = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, m_texId);

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgb, textureBitmap.Width, textureBitmap.Height, 0,
                OpenTK.Graphics.OpenGL.PixelFormat.Bgr, PixelType.UnsignedByte, TextureData.Scan0);
            textureBitmap.UnlockBits(TextureData);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
        }

        private void ApplyMaterial(Material mat)
        {
            if (mat.GetMaterialTextureCount(TextureType.Diffuse) > 0)
            {
                TextureSlot tex;
                if (mat.GetMaterialTexture(TextureType.Diffuse, 0, out tex))
                    LoadTexture(tex.FilePath);
            }

            Color4 color = new Color4(.8f, .8f, .8f, 1.0f);
            if (mat.HasColorDiffuse)
            {
                // color = FromColor(mat.ColorDiffuse);
            }
            GL.Material(MaterialFace.FrontAndBack, MaterialParameter.Diffuse, color);

            color = new Color4(0, 0, 0, 1.0f);
            if (mat.HasColorSpecular)
            {
                color = FromColor(mat.ColorSpecular);
            }
            GL.Material(MaterialFace.FrontAndBack, MaterialParameter.Specular, color);

            color = new Color4(.2f, .2f, .2f, 1.0f);
            if (mat.HasColorAmbient)
            {
                color = FromColor(mat.ColorAmbient);
            }
            GL.Material(MaterialFace.FrontAndBack, MaterialParameter.Ambient, color);

            color = new Color4(0, 0, 0, 1.0f);
            if (mat.HasColorEmissive)
            {
                color = FromColor(mat.ColorEmissive);
            }
            GL.Material(MaterialFace.FrontAndBack, MaterialParameter.Emission, color);

            float shininess = 1;
            float strength = 1;
            if (mat.HasShininess)
            {
                shininess = mat.Shininess;
            }
            if (mat.HasShininessStrength)
            {
                strength = mat.ShininessStrength;
            }

            GL.Material(MaterialFace.FrontAndBack, MaterialParameter.Shininess, shininess * strength);
        }

        private Matrix4 FromMatrix(Matrix4x4 mat)
        {
            Matrix4 m = new Matrix4();
            m.M11 = mat.A1;
            m.M12 = mat.A2;
            m.M13 = mat.A3;
            m.M14 = mat.A4;
            m.M21 = mat.B1;
            m.M22 = mat.B2;
            m.M23 = mat.B3;
            m.M24 = mat.B4;
            m.M31 = mat.C1;
            m.M32 = mat.C2;
            m.M33 = mat.C3;
            m.M34 = mat.C4;
            m.M41 = mat.D1;
            m.M42 = mat.D2;
            m.M43 = mat.D3;
            m.M44 = mat.D4;
            return m;
        }

        private Vector3 FromVector(Vector3D vec)
        {
            Vector3 v;
            v.X = vec.X;
            v.Y = vec.Y;
            v.Z = vec.Z;
            return v;
        }

        private Color4 FromColor(Color4D color)
        {
            Color4 c;
            c.R = color.R;
            c.G = color.G;
            c.B = color.B;
            c.A = color.A;
            return c;
        }
    }
}

