using System;
using System.Collections.Generic;
using System.Linq;
using Fusee.Base.Common;
using Fusee.Base.Core;
using Fusee.Engine.Common;
using Fusee.Engine.Core;
using Fusee.Math.Core;
using Fusee.Serialization;
using Fusee.Xene;
using static System.Math;
using static Fusee.Engine.Core.Input;
using static Fusee.Engine.Core.Time;

namespace Fusee.Tutorial.Core
{
    public class FirstSteps : RenderCanvas
    {
        private SceneContainer _scene;
        private SceneRenderer _sceneRenderer;
        private float _camAngle = 0;
        private TransformComponent _cubeTransform;
        private TransformComponent _cubeTransform1;
        private TransformComponent _cubeTransform2;

        public override void Init()
        {
            // Set the clear color for the backbuffer to white (100% intentsity in all color channels R, G, B, A).
            RC.ClearColor = new float4(2f, 2f, 2.7f, 5);

            // Create scene with cubes 
            // The three components: one XForm, one Shader and the Mesh
            _cubeTransform = new TransformComponent {Scale = new float3(1, 0.1f, 1), Translation = new float3(2, 0.7f, 0)};
            var cubeShader = new ShaderEffectComponent
            { 
                Effect = SimpleMeshes.MakeShaderEffect(new float3 (1, 4, 2), new float3 (6, 4, 2),  3)
            };
            var cubeMesh = SimpleMeshes.CreateCuboid(new float3(4, 5, 7));
            var cubeMesh1 = SimpleMeshes.CreateCuboid(new float3(3, 1, 15));
            var cubeMesh2 = SimpleMeshes.CreateCuboid(new float3(5, 7, 9));

            _cubeTransform1 = new TransformComponent {Scale = new float3(1.2f, 2.7f, 0.1f), Translation = new float3(10, 5, 5)};
            var cubeShader1 = new ShaderEffectComponent
            { 
                Effect = SimpleMeshes.MakeShaderEffect(new float3 (0.2f, 0.4f, 0.6f), new float3 (1, 1, 1),  4)
            };

            _cubeTransform2 = new TransformComponent {Scale = new float3(0.5f, 0.5f, 0.5f), Translation = new float3(30, 14, 19)};
            var cubeShader2 = new ShaderEffectComponent
            { 
                Effect = SimpleMeshes.MakeShaderEffect(new float3 (2, 1, 20), new float3 (1, 1, 1),  5)
            };
           
            // Assemble the cubes
            var cubeNode = new SceneNodeContainer();
            cubeNode.Components = new List<SceneComponentContainer>();
            cubeNode.Components.Add(_cubeTransform);
            cubeNode.Components.Add(cubeShader);
            cubeNode.Components.Add(cubeMesh);

             var cubeNode1 = new SceneNodeContainer();
            cubeNode1.Components = new List<SceneComponentContainer>();
            cubeNode1.Components.Add(_cubeTransform1);
            cubeNode1.Components.Add(cubeShader1);
            cubeNode1.Components.Add(cubeMesh1);

             var cubeNode2 = new SceneNodeContainer();
            cubeNode2.Components = new List<SceneComponentContainer>();
            cubeNode2.Components.Add(_cubeTransform2);
            cubeNode2.Components.Add(cubeShader2);
            cubeNode2.Components.Add(cubeMesh2);
    
           
            _scene = new SceneContainer();
            _scene.Children = new List<SceneNodeContainer>();
            _scene.Children.Add(cubeNode);
            _scene.Children.Add(cubeNode1);
             _scene.Children.Add(cubeNode2);

            
            _sceneRenderer = new SceneRenderer(_scene);
        }

        // RenderAFrame is called once a frame
        public override void RenderAFrame()
        {

           // Clear the backbuffer
            RC.Clear(ClearFlags.Color | ClearFlags.Depth);

           // Animate the camera angle
            _camAngle = _camAngle + 90.0f * M.Pi/180.0f * DeltaTime ;

           
            _cubeTransform.Translation = new float3(0, 8 * M.Sin(1 * TimeSinceStart), 0);

            
            RC.View = float4x4.CreateTranslation(0, 0, 20) * float4x4.CreateRotationY(_camAngle);


            
            _sceneRenderer.Render(RC);

            
            Present();
        }


       // Is called when the window was resized
        public override void Resize()
        {
            
            RC.Viewport(0, 0, Width, Height);

          
            var aspectRatio = Width / (float)Height;

           
            var projection = float4x4.CreatePerspectiveFieldOfView(M.PiOver4, aspectRatio, 1, 20000);
            RC.Projection = projection;
        }
    }
}