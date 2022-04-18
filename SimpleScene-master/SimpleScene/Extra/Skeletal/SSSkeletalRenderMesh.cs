﻿using System;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace SimpleScene.Demos
{
	/// <summary>
	/// Manages skeletal runtime hierarchy and animation channels.
	/// Invokes draw on render submeshes.
	/// </summary>
    public class SSSkeletalRenderMesh : SSAbstractMesh, ISSInstancable
	{
		// TODO Fix multiple objects updating a single mesh twice

		public float timeScale = 1f;

		public readonly List<RenderSubMesh> renderSubMeshes = new List<RenderSubMesh> ();
		protected readonly SSSkeletalHierarchyRuntime _hierarchy;
		protected readonly List<SSSkeletalChannelController> _channelControllers
			= new List<SSSkeletalChannelController> ();

		public override bool alphaBlendingEnabled {
			get { return base.alphaBlendingEnabled; }
			set {
				base.alphaBlendingEnabled = value;
				if (renderSubMeshes != null) {
					foreach (var child in renderSubMeshes) {
						child.alphaBlendingEnabled = value;
					}
				}
			}
		}

		public override Vector3 boundingSphereCenter {
			get { return base.boundingSphereCenter; }
		}

		public override float boundingSphereRadius {
			get { return base.boundingSphereRadius; }
		}

		public SSSkeletalRenderMesh(SSSkeletalMesh[] subMeshArray)
		{
			_hierarchy = new SSSkeletalHierarchyRuntime (subMeshArray [0].joints);
			foreach (var subMesh in subMeshArray) {
				_hierarchy.verifyJoints (subMesh.joints);
				attachMesh (subMesh);
			}
			_channelControllers.Add (new SSBindPoseSkeletalController ());
		}

		public SSSkeletalRenderMesh(SSSkeletalMesh mesh) 
		{
			_hierarchy = new SSSkeletalHierarchyRuntime (mesh.joints);
			attachMesh (mesh);
			_channelControllers.Add (new SSBindPoseSkeletalController ());
		}

		/// <summary>
		/// This can be used to loop an animation without having to set up a state machine explicitly
		/// For a more sophisticated control use AddController()
		/// </summary>
		public void playAnimationLoop(SSSkeletalAnimation anim, float transitionTime = 0f, 
									  int[] topLevelJoints = null)
		{
			_hierarchy.verifyAnimation (anim);
			var loopSM = new SSAnimationStateMachine ();
			loopSM.addState ("default", anim, true);
			loopSM.addAnimationEndsTransition ("default", "default", transitionTime);
			if (topLevelJoints == null) {
				topLevelJoints = _hierarchy.topLevelJoints;
			}
			var loopController = new SSAnimationStateMachineSkeletalController (loopSM, topLevelJoints);
			addController (loopController);
		}

		public void playAnimationLoop(SSSkeletalAnimation anim, float transitionTime = 0f,
									  params string[] topLevelJointNames) 
		{
			playAnimationLoop (anim, transitionTime, _hierarchy.jointIndices(topLevelJointNames));
		}

		public SSAnimationStateMachineSkeletalController addStateMachine(SSAnimationStateMachine description,
																		 int[] topLevelJoints = null)
		{
			var smController = new SSAnimationStateMachineSkeletalController (description, topLevelJoints);
			addController (smController);
			return smController;
		}

		public SSAnimationStateMachineSkeletalController addStateMachine(SSAnimationStateMachine description, 
																		 params string[] topLevelJointNames)
		{
			return addStateMachine (description, _hierarchy.jointIndices (topLevelJointNames));
		}

		public void addCustomizedJoint(int jointIdx, SSCustomizedJoint parJoint)
		{
			SSCustomizedJointsController ctrl = null;
			if (_channelControllers.Count > 0) {
				// avoid adding chains of parametric joint controllers if they can be combined
				// in one controller
				ctrl = _channelControllers [_channelControllers.Count - 1] as SSCustomizedJointsController;
			}
			if (ctrl == null) {
				ctrl = new SSCustomizedJointsController ();
				addController (ctrl);
			}
			ctrl.addJoint (jointIdx, parJoint);
		}

		public void addCustomizedJoint(string jointName, SSCustomizedJoint parJoint)
		{
			addCustomizedJoint (_hierarchy.jointIndex (jointName), parJoint);
		}

		public void removeParametricJoint(int jointIdx)
		{
			foreach (var ctrl in _channelControllers) {
				var parCtrl = ctrl as SSCustomizedJointsController;
				if (parCtrl != null && parCtrl.isActive (_hierarchy.joints [jointIdx])) {
					parCtrl.removeJoint (jointIdx);
				}
			}
		}

		public void addController(SSSkeletalChannelController controller)
		{
			_channelControllers.Add (controller);
		}

		/// <summary>
		/// Adds the state machine that will control the mesh.
		/// </summary>
		/// <param name="stateMachine">
		/// A runtime state machine component that can be used to trigger animation state transitions on demand,
		/// from keyboard etc.
		/// </param>
		public SSAnimationStateMachineSkeletalController addStateMachine(
			SSAnimationStateMachine stateMachine)
		{
			var newSMRuntime = new SSAnimationStateMachineSkeletalController (stateMachine);
			_channelControllers.Add (newSMRuntime);
			return newSMRuntime;
		}

		/// <summary>
		/// Adds a sumbesh that will share the existing joint hierarchy
		/// </summary>
		public void attachMesh(SSSkeletalMesh mesh)
		{
			var newRender = new RenderSubMesh (mesh, _hierarchy);
			newRender.alphaBlendingEnabled = this.alphaBlendingEnabled;
			renderSubMeshes.Add (newRender);
		}

		public override void update(float elapsedS)
		{
			elapsedS *= timeScale;
			foreach (var channelController in _channelControllers) {
				channelController.update (elapsedS);
			}
		}

		public override void renderMesh (SSRenderConfig renderConfig)
		{
			// apply animation channels
			_hierarchy.applySkeletalControllers (_channelControllers);

			SSAABB totalAABB = new SSAABB (float.PositiveInfinity, float.NegativeInfinity);
			foreach (var sub in renderSubMeshes) {
				SSAABB aabb = sub.ComputeVertices ();
				sub.renderMesh (renderConfig);
				totalAABB.ExpandBy (aabb);
			}
			// update the bounding sphere
			var sphere = totalAABB.ToSphere ();
			base.boundingSphereCenter = sphere.center;
			base.boundingSphereRadius = sphere.radius;
			NotifyMeshPositionOrSizeChanged ();
		}

		public void drawInstanced(SSRenderConfig cfg, int instanceCount, PrimitiveType primType) 
		{
			foreach (var sub in renderSubMeshes) {
                sub.drawInstanced (cfg, instanceCount, primType);
			}
		}

        public void drawSingle(SSRenderConfig renderConfig, PrimitiveType primType)
        {
            renderMesh(renderConfig);
        }

		public override bool preciseIntersect (ref SSRay localRay, out float nearestLocalRayContact)
        {
            nearestLocalRayContact = float.PositiveInfinity;
			foreach (var s in renderSubMeshes) {
                float contact;
                if (s.preciseIntersect(ref localRay, out contact)
                    && contact < nearestLocalRayContact) {
                    nearestLocalRayContact = contact;
                }
			}
            return nearestLocalRayContact < float.PositiveInfinity;
		}

		// *****************************

		/// <summary>
		/// Draws the submeshes
		/// </summary>
		public class RenderSubMesh : SSIndexedMesh<SSVertex_PosNormTex>
		{
			protected readonly SSSkeletalMeshRuntime _runtimeMesh;
			protected readonly SSVertex_PosNormTex[] _vertices;

			public RenderSubMesh (SSSkeletalMesh skeletalMesh, SSSkeletalHierarchyRuntime hierarchy=null)
				: base(null, skeletalMesh.triangleIndices)
			{
				_runtimeMesh = new SSSkeletalMeshRuntime(skeletalMesh, hierarchy);

				_vertices = new SSVertex_PosNormTex[_runtimeMesh.numVertices];
				for (int v = 0; v < _runtimeMesh.numVertices; ++v) {
					_vertices [v].TexCoord = _runtimeMesh.textureCoords (v);
					_vertices [v].Normal = _runtimeMesh.bindPoseNormal(v);
				}
				ComputeVertices ();

				string matString = skeletalMesh.materialShaderString;
				if (matString != null && matString.Length > 0) {
					base.textureMaterial 
                    = SSTextureMaterial.FromMaterialString (skeletalMesh.basePath, matString);
				}

                // don't use BVH because vertices keep changing on every render frame
                // TODO can we "update" BVH efficiently, instead?
                base.useBVHForIntersections = false;
			}

			public override void renderMesh(SSRenderConfig renderConfig)
			{
				base.renderMesh (renderConfig);

				// debugging vertex normals... 
				#if false
				{
				// do not change the order!!
				renderFaceNormals();               // these are correct..
				renderFaceAveragedVertexNormals(); // these are correct..                
				// renderBindPoseVertexNormals ();
				renderAnimatedVertexNormals();     // these are currently WRONG
				}
				#endif

				#if false
				SSShaderProgram.DeactivateAll ();
				// bounding box debugging
				GL.PolygonMode(MaterialFace.Front, PolygonMode.Line);
				GL.Disable (EnableCap.Texture2D);
				GL.Translate (aabb.Center ());
				GL.Scale (aabb.Diff ());
				GL.Color4 (1f, 0f, 0f, 0.1f);
				SSTexturedCube.Instance.DrawArrays (ref renderConfig, PrimitiveType.Triangles);
				GL.PolygonMode(MaterialFace.Front, PolygonMode.Fill);
				#endif
			}

			/// <summary>
			/// Computes vertex positions and normals (based on the state of runtime joint hierarchy).
			/// Updates VBO with the result.
			/// </summary>
			/// <returns>AABB of the vertices.</returns>
			public SSAABB ComputeVertices()
			{
				SSAABB aabb= new SSAABB (float.PositiveInfinity, float.NegativeInfinity);
				for (int v = 0; v < _runtimeMesh.numVertices; ++v) {
					// position
					Vector3 pos = _runtimeMesh.computeVertexPos (v);
					_vertices [v].Position = pos;
					aabb.UpdateMin (pos);
					aabb.UpdateMax (pos);
					// normal
					_vertices [v].Normal = _runtimeMesh.computeVertexNormal (v);
				}
				_vbo.UpdateBufferData (_vertices);
                _bvh = null; // invalidate bvh
				return aabb;
			}


			private void renderFaceNormals() 
			{
				SSShaderProgram.DeactivateAll();
				GL.Color4(Color4.Green);
				for (int i=0;i<_runtimeMesh.numTriangles;i++) {
					int baseIdx = i * 3;                
					Vector3 p0 = _runtimeMesh.computeVertexPosFromTriIndex(baseIdx);
					Vector3 p1 = _runtimeMesh.computeVertexPosFromTriIndex(baseIdx + 1);
					Vector3 p2 = _runtimeMesh.computeVertexPosFromTriIndex(baseIdx + 2);

					Vector3 face_center = (p0 + p1 + p2) / 3.0f;
					Vector3 face_normal = Vector3.Cross(p1 - p0, p2 - p0).Normalized();

					GL.Begin(PrimitiveType.Lines);
					GL.Vertex3(face_center);
					GL.Vertex3(face_center + face_normal * 0.2f);
					GL.End();                
				}
			}

			private void renderAnimatedVertexNormals() {
				SSShaderProgram.DeactivateAll();
				GL.Color4(Color4.Magenta);
				for (int v = 0; v < _vertices.Length; ++v) {                

					GL.Begin(PrimitiveType.Lines);
					GL.Vertex3(_vertices[v].Position);
					GL.Vertex3(_vertices[v].Position + _vertices[v].Normal * 0.2f);
					GL.End();
				}
			}

			private void renderBindPoseVertexNormals()
			{
				SSShaderProgram.DeactivateAll ();
				GL.Color4 (Color4.White);
				for (int v = 0; v < _vertices.Length; ++v) {                
					GL.Begin (PrimitiveType.Lines);
					GL.Vertex3 (_vertices [v].Position);
					GL.Vertex3 (_vertices [v].Position + _runtimeMesh.bindPoseNormal(v) * 0.3f); 
					GL.End ();
				}
			}

			public void renderFaceAveragedVertexNormals() {
				Vector3[] perVertexNormals = new Vector3[_runtimeMesh.numVertices];

				for (int i = 0; i < _runtimeMesh.numTriangles; i++) {
					int baseIdx = i * 3;
					Vector3 p0 = _runtimeMesh.computeVertexPosFromTriIndex(baseIdx);
					Vector3 p1 = _runtimeMesh.computeVertexPosFromTriIndex(baseIdx + 1);
					Vector3 p2 = _runtimeMesh.computeVertexPosFromTriIndex(baseIdx + 2);

					Vector3 face_normal = Vector3.Cross(p1 - p0, p2 - p0).Normalized();

					int v0 = _runtimeMesh.indices[baseIdx];
					int v1 = _runtimeMesh.indices[baseIdx + 1];
					int v2 = _runtimeMesh.indices[baseIdx + 2];

					perVertexNormals[v0] += face_normal;
					perVertexNormals[v1] += face_normal;
					perVertexNormals[v2] += face_normal;

				}

				// render face averaged vertex normals

				SSShaderProgram.DeactivateAll();
				GL.Color4(Color4.Yellow);
				for (int v=0;v<perVertexNormals.Length;v++) {
					GL.Begin(PrimitiveType.Lines);
					GL.Vertex3(_runtimeMesh.computeVertexPos(v));
					GL.Vertex3(_runtimeMesh.computeVertexPos(v) + perVertexNormals[v].Normalized() * 0.5f);
					GL.End();
				}
			}
		}
	};


}

