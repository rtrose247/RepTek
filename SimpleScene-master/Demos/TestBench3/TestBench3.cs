﻿using System;
using System.Collections.Generic;
using SimpleScene;
using SimpleScene.Demos;
using OpenTK;
using OpenTK.Input;
using OpenTK.Graphics;

namespace TestBench3
{
    public class TestBench3 : TestBenchBootstrap
    {
        protected enum MissileLaunchers : int 
            {AttackerDrone, VandalShip, Camera, End }
        protected enum MissileTargets : int 
            { TargetDrone, VandalShip, Camera, Selected, AttackerDrone, End}
        protected enum HitTimeMode : int 
            { Auto, Disabled, Fixed5s, Fixed10s, Fixed15s, Fixed20s, End }

        protected SExplosionRenderer explosionManager;
        protected SSpaceMissilesRenderManager missileManager;
        protected SHudTargetsManager.TargetSpecific targetHud;

        protected SSObjectMesh vandalShip;
        protected SSObjectMesh attackerDrone;
        protected SSObjectMesh targetDrone;
        protected Vector3 vandalVelocity = Vector3.Zero;

        protected BodiesFieldGenerator attackerDroneFieldGen;
        protected SSpaceMissileVisualParameters attackerDroneMissileParams;
        protected BodiesFieldGenerator vandalShipFieldGen;
        protected SSpaceMissileVisualParameters vandalShipMissileParams;
        protected SSpaceMissileVisualParameters cameraMissileParams;

        protected MissileLaunchers missileLauncher = MissileLaunchers.VandalShip;
        protected MissileTargets missileTarget = MissileTargets.TargetDrone;
        protected HitTimeMode hitTimeMode = HitTimeMode.Auto;
        protected SSObjectGDISurface_Text missileStatsText;

        protected Dictionary<SSObject, ISSpaceMissileTarget> targets 
            = new Dictionary<SSObject, ISSpaceMissileTarget> ();
        protected int clusterSize = 1;

        protected float localTime = 0f;

        public TestBench3 ()
            : base("TestBench3: Missiles")
        {
            if (shadowmapDebugQuad != null) {
                shadowmapDebugQuad.renderState.visible = false;
            }
        }

        static void Main()
        {
            // The 'using' idiom guarantees proper resource cleanup.
            // We request 30 UpdateFrame events per second, and unlimited
            // RenderFrame events (as fast as the computer can handle).
            using (var game = new TestBench3()) {
                game.Run(30.0);
            }
        }

        protected override void setupScene ()
        {
            base.setupScene();

            var droneMesh = SSAssetManager.GetInstance<SSMesh_wfOBJ> ("./drone2/Drone2.obj");
            //var droneMesh = SSAssetManager.GetInstance<SSMesh_wfOBJ> ("missiles", "missile.obj");
            var vandalMesh = SSAssetManager.GetInstance<SSMesh_wfOBJ> ("missiles/vandal_assembled.obj");

            // add drones
            attackerDrone = new SSObjectMesh (droneMesh);
            attackerDrone.Pos = new OpenTK.Vector3(-20f, 0f, 0f);
            attackerDrone.Orient(Vector3.UnitX, Vector3.UnitY);
            // attackerDrone.AmbientMatColor = new Color4(0.1f,0.1f,0.1f,0.1f);            
            // attackerDrone.DiffuseMatColor = new Color4(0.3f,0.3f,0.3f,0.3f);
            // attackerDrone.SpecularMatColor = new Color4(0.3f,0.3f,0.3f,0.3f);
            // attackerDrone.EmissionMatColor = new Color4(0.3f,0.3f,0.3f,0.3f);
            attackerDrone.Name = "attacker drone";
            main3dScene.AddObject (attackerDrone);

            targetDrone = new SSObjectMesh (droneMesh);
            targetDrone.Pos = new OpenTK.Vector3(200f, 0f, 0f);
            targetDrone.Orient(-Vector3.UnitX, Vector3.UnitY);
            // targetDrone.AmbientMatColor = new Color4(0.1f,0.1f,0.1f,0.1f);
            // targetDrone.DiffuseMatColor = new Color4(0.3f,0.3f,0.3f,0.3f);
            // targetDrone.SpecularMatColor = new Color4(0.3f,0.3f,0.3f,0.3f);
            // targetDrone.EmissionMatColor = new Color4(0.3f,0.3f,0.3f,0.3f);
            targetDrone.Name = "target drone";
            targetDrone.MainColor = new Color4(1f, 0f, 0.7f, 1f);
            main3dScene.AddObject (targetDrone);

            vandalShip = new SSObjectMesh (vandalMesh);
            vandalShip.Pos = new OpenTK.Vector3(100f, 0f, 0f);
            vandalShip.Scale = new Vector3 (0.05f);
            // vandalShip.AmbientMatColor = new Color4(0.1f,0.1f,0.1f,0.1f);
            // vandalShip.DiffuseMatColor = new Color4(0.1f,0.1f,0.1f,0.1f);
            // vandalShip.SpecularMatColor = new Color4(0.1f,0.1f,0.1f,0.1f);
            // vandalShip.EmissionMatColor = new Color4(0.0f,0.0f,0.0f,0.0f);
            vandalShip.Name = "Vandal ship";
            vandalShip.MainColor = new Color4 (0.6f, 0.6f, 0.6f, 1f);
            //vandalShip.MainColor = new Color4(1f, 0f, 0.7f, 1f);
            //droneObj2.renderState.visible = false;
            vandalShip.Orient((targetDrone.Pos-vandalShip.Pos).Normalized(), Vector3.UnitY);
            main3dScene.AddObject (vandalShip);

            // shows explosions
            explosionManager = new SExplosionRenderer ();
            explosionManager.parameters.doShockwave = false;
            explosionManager.parameters.doDebris = false;
            explosionManager.parameters.timeScale = 3f;
            //explosionManager.renderState.visible = false;
            alpha3dScene.AddObject(explosionManager);

            // attacker drone missile parameters
            attackerDroneFieldGen = new BodiesFieldGenerator (
                new ParticlesSphereGenerator (Vector3.Zero, 1f));
            attackerDroneMissileParams = new SSpaceMissileVisualParameters();

            // vandal missile params
            vandalShipFieldGen = new BodiesRingGenerator (
                new ParticlesOvalGenerator (1f, 1f),
                ringCenter: new Vector3 (0f, 0f, 0f),
                up: Vector3.UnitZ,
                ringRadius: 80f,
                oriPolicy: BodiesFieldGenerator.OrientPolicy.None
            );

            vandalShipMissileParams = new SSpaceMissileVisualParameters();
            vandalShipMissileParams.ejectionMaxRotationVel = 0.05f;
            vandalShipMissileParams.ejectionVelocity = 30f;
            vandalShipMissileParams.pursuitActivationTime = 0.1f;
            vandalShipMissileParams.ejectionSmokeDuration = 0.5f;
            vandalShipMissileParams.ejectionSmokeSizeMax = 5f;

            cameraMissileParams = new SSpaceMissileVisualParameters();
            //cameraMissileParams.spawnGenerator = null;
            //cameraMissileParams.spawnTxfm = straightMissileSpawnTxfm;
            cameraMissileParams.ejectionMaxRotationVel = 0.05f;
            cameraMissileParams.ejectionVelocity = 10f;

            // missile manager
            missileManager = new SSpaceMissilesRenderManager(main3dScene, alpha3dScene, hud2dScene);

            // additional statistics text
            missileStatsText = new SSObjectGDISurface_Text();
            missileStatsText.alphaBlendingEnabled = true;
            missileStatsText.Label = "stats placeholder...";
            missileStatsText.Pos = new Vector3 (100f, 100f, 0f);
            //missileStatsText.Size = 20f;
            hud2dScene.AddObject(missileStatsText);

            var targetsManager = new SHudTargetsManager (main3dScene, hud2dScene);
            targetHud = targetsManager.addTarget(
                (o) => Color4.Red,
                _showDistanceFunc,
                (o) => o != null ? o.Name : "none",
                getTargetObject()
            );


            // trails test
            var trailsRenderer = new STrailsRenderer(
                () => vandalShip.Pos,
                () => vandalShip.Dir,
				() => vandalShip.Up
            );
            alpha3dScene.AddObject(trailsRenderer);
        }

        protected string _showDistanceFunc(SSObject target)
        {
            var launcherObj = getLauncherObject();
            var targetPos = target != null ? target.Pos : launcherObj.Pos;
            return "dist:" + Math.Floor((launcherObj.Pos - targetPos).LengthFast).ToString();
        }

        protected void missileKeyUpHandler(object sender, KeyboardKeyEventArgs e)
        {
            switch(e.Key) {
            case Key.Q:
                _launchMissiles();
                break;
            case Key.M:
                var camera = (main3dScene.ActiveCamera as SSCameraThirdPerson);
                if (camera != null) {
                    var target = camera.FollowTarget;
                    if (target == null) {
                        camera.FollowTarget = attackerDrone;
                    } else if (target == attackerDrone) {
                        camera.FollowTarget = vandalShip;
                    } else if (target == vandalShip) {
                        camera.FollowTarget = targetDrone;
                    } else {
                        camera.FollowTarget = null;
                    }
                    updateTextDisplay();
                }
                break;
            case Key.T:
                do {
                    switchTarget();
                } while (getTargetObject() == getLauncherObject());
                updateTextDisplay();
                var targetObj = getTargetObject();
                targetHud.targetObj =
                    (targetObj == main3dScene.ActiveCamera) ? null : targetObj;
                break;
            case Key.L: 
                switchLauncher();
                while (missileTarget != MissileTargets.Selected
                  && getTargetObject() == getLauncherObject()) { 
                    switchTarget();
                }
                updateTextDisplay();
                break;
            case Key.H:
                switchHitTime();
                updateTextDisplay();
                break;
            case Key.Minus:
                clusterSize = Math.Max(1, clusterSize - 1);
                updateTextDisplay();
                break;
            case Key.Plus:
                clusterSize++;
                updateTextDisplay();
                break;
            case Key.V:
                attackerDroneMissileParams.debuggingAid = !attackerDroneMissileParams.debuggingAid;
                vandalShipMissileParams.debuggingAid = !vandalShipMissileParams.debuggingAid;
                cameraMissileParams.debuggingAid = !cameraMissileParams.debuggingAid;
                updateTextDisplay();
                break;
            }
        }

        protected override void mouseDownHandler (object sender, MouseButtonEventArgs e)
        {
            base.mouseDownHandler(sender, e);

            if (this.missileTarget == MissileTargets.Selected) {
                targetHud.targetObj = 
                    (selectedObject == main3dScene.ActiveCamera) ? null : selectedObject;
            }
        }

        protected override void setupInput()
        {
            base.setupInput();
            this.KeyUp += missileKeyUpHandler;
        }

        protected override void setupCamera()
        {
            var camera = new SSCameraThirdPerson (null);
            camera.Name = "camera";
            camera.basePos = new Vector3 (100f, 0f, 30f);
            camera.Pos = new Vector3 (170f, 20f, 245f);
            camera.followDistance = 225f;
            camera.localBoundingSphereRadius = 0.1f;

            main3dScene.ActiveCamera = camera;
        }

        protected override void OnUpdateFrame (FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            float timeElapsed = (float)e.Time;
            moveShips(timeElapsed);
        }

        protected override void OnRenderFrame (FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            updateMissileStatistics();
        }

        protected override void OnResize (EventArgs e)
        {
            base.OnResize(e);

            var sz = this.ClientSize;
            missileStatsText.Pos = new Vector3 (
                10f, sz.Height - 10f - missileStatsText.getGdiSize.Height, 0f
            );
        }

        protected override void updateTextDisplay ()
        {
            base.updateTextDisplay ();
            var text =  "\n[Q] to fire missiles";

            // camera mode
            var camera = main3dScene.ActiveCamera as SSCameraThirdPerson;
            if (camera != null) {
                var target = camera.FollowTarget;
                text += "\n[M] toggle camera target: [";
                text += (target == null ? "none" : target.Name) + ']';
            }

            // attacker
            text += "\n[L] toggle missile launcher: ";
            var launcherObj = getLauncherObject();
            text += '[' + (launcherObj == null ? "none" : launcherObj.Name) + ']';

            // target
            text += "\n[T] toggle missile target: ";
            if (missileTarget == MissileTargets.Selected) {
                text += "selected: ";
            }
            var targetObj = getTargetObject();
            text += '[' + (targetObj == null ? "none" : targetObj.Name) + ']';

            // hit time
            text += "\n[H] toggle hit time: [" + hitTimeMode.ToString() + ']';

            // num missiles
            text += "\n[+/-] cluster size: [" + clusterSize + ']';

            // debugging
            text += "\n[V] visual debugigng aid: [";
            text += (attackerDroneMissileParams.debuggingAid ? "ON" : "OFF")  + ']';

            textDisplay.Label += text;
        }

        public virtual void updateMissileStatistics()
        {
            string text 
                = "#missile renders: " + missileManager.numRenderedMissiles
                + "\n#particles: " + missileManager.numRenderParticles
                + "\n#missile clusters: " + missileManager.numMissileClusters;
            missileStatsText.Label = text;
        }

        // going deeper into demo logic...

        protected void _launchMissiles()
        {
            if (targets.Count == 0) {
                // initialize targets
                targets.Add(attackerDrone, new SSpaceMissileObjectTarget(attackerDrone));
                targets.Add(targetDrone, new SSpaceMissileObjectTarget(targetDrone));
                targets.Add(vandalShip, new SSpaceMissileObjectTarget(vandalShip));
                targets.Add(main3dScene.ActiveCamera, new SSpaceMissileObjectTarget(main3dScene.ActiveCamera));
            }

            var target = targets[getTargetObject()];
            var launcher = getLauncherObject();
            var hitTime = getHitTime();
            var mParams = getLauncherParams();
            var launcherVel = (missileLauncher == MissileLaunchers.VandalShip) ? vandalVelocity : Vector3.Zero;
            var localPosOffsets = getLauncherLocalPosOffsets();
            var localDirs = getLauncherLocalDirs();
            var localBodyGen = getLauncherMissileBodyGen();

            mParams.pursuitHitTimeCorrection = (hitTimeMode != HitTimeMode.Disabled);
            if (target != null) {
                missileManager.launchCluster(
                    launcher.worldMat, 
                    launcherVel,
                    clusterSize,
                    target,
                    mParams,
                    localPosOffsets,
                    localDirs,
                    localBodyGen,
                    targetHitHandler,
                    hitTime
                );
            }
        }

        protected void moveShips(float timeElapsed)
        {
            if (timeElapsed <= 0f) return;

            // make the target drone move from side to side
            localTime += timeElapsed;
			//localTime += timeElapsed * 0.3f;
            Vector3 pos = targetDrone.Pos;
            pos.Z = 30f * (float)Math.Sin(localTime);
            targetDrone.Pos = pos;

            // make the vandal ship orbit missile target
            Vector3 desiredPos;
            Vector3 desiredDir;
			float angle = localTime * 0.5f;
			//float angle = localTime * 0.01f;
            #if true
            float desiredXOffset = 100f * (float)Math.Cos(angle);
            float desiredYOffset = 20f * (float)Math.Sin(angle * 0.77f);
            float desiredZOffset = 80f * (float)Math.Sin(angle * 0.88f);
            #else
            float desiredXOffset = 100f * (float)Math.Cos(angle);
            float desiredYOffset = 100f * (float)Math.Sin(angle);
            float desiredZOffset = 0f;
            #endif

            Vector3 desiredOffset = new Vector3 (desiredXOffset, desiredYOffset, desiredZOffset);

            var target = getTargetObject();
            if (missileLauncher != MissileLaunchers.VandalShip || target == null || target == vandalShip) {
                desiredPos = new Vector3 (100f, 0f, 0f);
                desiredDir = -Vector3.UnitX;
            }
            else if (target == main3dScene.ActiveCamera) {
                desiredPos = main3dScene.ActiveCamera.Pos + -main3dScene.ActiveCamera.Dir * 300f;
                Quaternion cameraOrient = OpenTKHelper.neededRotationQuat(Vector3.UnitZ, -main3dScene.ActiveCamera.Up);
                desiredPos += Vector3.Transform(desiredOffset * 0.1f, cameraOrient); 
                desiredDir = (target.Pos - vandalShip.Pos).Normalized();

            } else {
                //float desiredZOffset = 5f * (float)Math.Sin(angle + 0.2f);
                desiredPos = target.Pos + desiredOffset;
                desiredDir = (target.Pos - vandalShip.Pos).Normalized();
            }

            Vector3 desiredMotion = desiredPos - vandalShip.Pos;
            const float vel = 100f;
            float displacement = vel * timeElapsed;
            Vector3 vandalNewPos;
            if (displacement > desiredMotion.LengthFast) {
                vandalNewPos = desiredPos;
            } else {
                vandalNewPos = vandalShip.Pos + desiredMotion.Normalized() * displacement;
            }

            vandalVelocity = (vandalNewPos - vandalShip.Pos) / timeElapsed;
            vandalShip.Pos = vandalNewPos;

            Quaternion vandalOrient = OpenTKHelper.neededRotationQuat(Vector3.UnitZ, desiredDir);
            vandalShip.Orient(desiredDir, Vector3.Transform(Vector3.UnitY, vandalOrient));
        }

        protected void targetHitHandler(SSpaceMissileData missileData)
        {
            explosionManager.showExplosion(missileData.position, 2.5f);
        }

        protected Matrix4 straightMissileSpawnTxfm(ISSpaceMissileTarget target, 
                                                   Vector3 launcherPos, Vector3 launcherVel,
                                                   int id, int clusterSize)
        {
            const float sideDispersal = 2f;
            float angle = (float)Math.PI * 2f / (float)clusterSize * id;
            Vector3 toTarget = (target.position - launcherPos);
            float length = toTarget.Length;
            if (length < 0.0001f) {
                toTarget = -Vector3.UnitZ;
            } else {
                // normalize
                toTarget /= length;
            }
            Quaternion orientQuat = OpenTKHelper.neededRotationQuat(Vector3.UnitZ, toTarget);
            Matrix4 orientMat = Matrix4.CreateFromQuaternion(orientQuat);
            Matrix4 disperalMat = Matrix4.CreateTranslation(sideDispersal * (float)Math.Cos(angle), 
                                      sideDispersal * (float)Math.Sin(angle), 0f);
            return disperalMat * orientMat * Matrix4.CreateTranslation(launcherPos) * Matrix4.CreateTranslation(toTarget * 7f);
        }

        protected void switchTarget()
        {
            int a = (int)missileTarget;
            a = (a + 1) % (int)MissileTargets.End;
            missileTarget = (MissileTargets)a;
        }

        protected void switchLauncher()
        {
            int l = (int)missileLauncher;
            l = (l + 1) % (int)MissileLaunchers.End;
            missileLauncher = (MissileLaunchers)l;
        }

        protected void switchHitTime()
        {
            int h = (int)hitTimeMode;
            h = (h + 1) % (int)HitTimeMode.End;
            hitTimeMode = (HitTimeMode)h;
        }

        protected SSObject getLauncherObject()
        {
            switch (missileLauncher) {
            case MissileLaunchers.AttackerDrone: return attackerDrone;
            case MissileLaunchers.VandalShip: return vandalShip;
            case MissileLaunchers.Camera: return main3dScene.ActiveCamera;
            }
            throw new Exception ("unhandled enum");
        }

        protected SSpaceMissileVisualParameters getLauncherParams()
        {
            switch (missileLauncher) {
            case MissileLaunchers.AttackerDrone: return attackerDroneMissileParams;
            case MissileLaunchers.VandalShip: return vandalShipMissileParams;
            case MissileLaunchers.Camera: return cameraMissileParams;
            }
            throw new Exception ("unhandled enum");
        }

        protected BodiesFieldGenerator getLauncherMissileBodyGen()
        {
            if (missileLauncher == MissileLaunchers.AttackerDrone) {
                return attackerDroneFieldGen;
            } else if (missileLauncher == MissileLaunchers.VandalShip) {
                return vandalShipFieldGen;
            } else {
                return null;
            }
        }

        protected Vector3[] getLauncherLocalPosOffsets()
        {
            switch (missileLauncher) {
            case MissileLaunchers.VandalShip:
            case MissileLaunchers.Camera:
                Vector3[] ret = new Vector3[clusterSize];
                for (int i = 0; i < clusterSize; ++i) {
                    const float sideDispersal = 2f;
                    const float zOffset = 7f;
                    float angle = 2f * (float)Math.PI / (float)clusterSize * (float)i;
                    ret [i] = new Vector3 (sideDispersal * (float)Math.Cos(angle),
                                           sideDispersal * (float)Math.Sin(angle),
                                           zOffset);
                }
                return ret;
            default:
                return null;
            }
        }

        protected Vector3[] getLauncherLocalDirs()
        {
            switch (missileLauncher) {
            case MissileLaunchers.VandalShip:
                return new Vector3[] { Vector3.UnitZ };
            case MissileLaunchers.Camera:
                return new Vector3[] { Vector3.UnitZ };
            default:
                return null;
            }
        }

        protected SSObject getTargetObject()
        {
            switch (missileTarget) {
            case MissileTargets.TargetDrone: return targetDrone;
            case MissileTargets.AttackerDrone: return attackerDrone;
            case MissileTargets.VandalShip: return vandalShip;
            case MissileTargets.Camera: return main3dScene.ActiveCamera;
            case MissileTargets.Selected: return selectedObject ?? main3dScene.ActiveCamera;
            }
            throw new Exception ("unhandled enum");
        }

        protected float getHitTime()
        {
            switch (hitTimeMode) {
            case HitTimeMode.Disabled: return 0f;
            case HitTimeMode.Fixed5s: return 5f;
            case HitTimeMode.Fixed10s: return 10f;
            case HitTimeMode.Fixed15s: return 15f;
            case HitTimeMode.Fixed20s: return 20f;
            case HitTimeMode.Auto:
                // guess based on distance
                var launcher = getLauncherObject();
                var target = getTargetObject();
                float dist = (target.Pos - launcher.Pos).LengthFast;
                return dist / 45f;
            }
            throw new Exception ("unhandled enum");
        }
    }
}

