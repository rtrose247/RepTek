﻿#define MISSILE_SHOW
#define MISSILE_DEBUG

using System;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using SimpleScene.Util;

#if MISSILE_DEBUG
using System.Drawing; // RectangleF
#endif

namespace SimpleScene.Demos
{
    public class SSpaceMissilesRenderManager
    {
        enum ParticleEffectorMasks : ushort { FlameToSmoke=1, EjectionSmoke=2 };

        protected readonly SSpaceMissilesVisualSimulation _simulation;
        protected readonly SSScene _objScene;
        protected readonly SSScene _particleScene;
        protected readonly SSScene _screenScene;

        protected readonly SSParticleSystemData _particlesData;
        protected readonly SSInstancedMeshRenderer _particleRenderer;

        protected SSColorKeyframesEffector _flameSmokeColorEffector = null;
        protected SSColorKeyframesEffector _ejectionSmokeColorEffector = null;
        protected SSMasterScaleKeyframesEffector _flameSmokeScaleEffector = null;
        protected SSMasterScaleKeyframesEffector _ejectionSmokeScaleEffector = null;

        readonly List<SSpaceMissileRenderInfo> _missileRuntimes = new List<SSpaceMissileRenderInfo> ();

        public SSpaceMissilesVisualSimulation simulation { get { return _simulation; } }

        public int numRenderedMissiles { get { return _missileRuntimes.Count; } }
        public int numRenderParticles { get { return _particlesData.numElements; } }
        public int numMissileClusters { get { return _simulation.numClusters; } }

        public SSpaceMissilesRenderManager (SSScene objScene, SSScene particleScene, SSScene screenScene,
                                            int particleCapacity = 10000)
        {
            _simulation = new SSpaceMissilesVisualSimulation ();

            _objScene = objScene;
            objScene.preRenderHooks += this._preRenderUpdate;

            objScene.preUpdateHooks += _simulation.updateSimulation;
            objScene.preRenderHooks += simulation.updateSimulation;

            _particleScene = particleScene;
            _screenScene = screenScene;

            // particle system
            _particlesData = new SSParticleSystemData (particleCapacity);
            _particleRenderer = new SSInstancedMeshRenderer (
                _particlesData, SSTexturedQuad.singleFaceInstance);
            _particleRenderer.renderState.alphaBlendingOn = true;
            _particleRenderer.renderState.castsShadow = false;
            _particleRenderer.renderState.receivesShadows = false;
            _particleRenderer.renderState.depthTest = true;
            _particleRenderer.renderState.depthWrite = false;
            //_particleRenderer.renderMode = SSInstancedMeshRenderer.RenderMode.CpuFallback;
            //_particleRenderer.renderMode = SSInstancedMeshRenderer.RenderMode.GpuInstancing;
            //_particleRenderer.renderState.visible = false;

            _particleRenderer.colorMaterial = SSColorMaterial.pureAmbient;

            _particleRenderer.Name = "missile smoke renderer";
            _particleScene.AddObject(_particleRenderer);
        }

        ~SSpaceMissilesRenderManager()
        {
            foreach (var missile in _missileRuntimes) {
                _removeMissileRender(missile);
            }
            _particleRenderer.renderState.toBeDeleted = true;
        }

        public SSpaceMissileClusterVisualData launchCluster(
            Matrix4 launcherWorldMat, Vector3 launchVel, int numMissiles,
            ISSpaceMissileTarget target,
            SSpaceMissileVisualParameters clusterParams,
            Vector3[] localPositioningOffsets = null,
            Vector3[] localDirections = null,
            BodiesFieldGenerator localPositioningGenerator = null,
            SSpaceMissileVisualData.AtTargetFunc missileAtTargetFunc = null,
            float timeToHit = float.NaN
        )
        {
            _initParamsSpecific(clusterParams);
            var cluster = _simulation.launchCluster(
                launcherWorldMat, launchVel, numMissiles,
                target, timeToHit, clusterParams, 
                localPositioningOffsets, localDirections, localPositioningGenerator,
                missileAtTargetFunc);
            foreach (var missile in cluster.missiles) {
                _addMissileRender(missile);
            }
            return cluster;
        }

        public SSpaceMissileRenderInfo addMissileRender(SSpaceMissileVisualData missile)
        {
            _initParamsSpecific(missile.parameters as SSpaceMissileVisualParameters);
            return _addMissileRender(missile);
        }

        public void removeMissileRender(SSpaceMissileRenderInfo missileRenderInfo)
        {
            _removeMissileRender(missileRenderInfo);
        }

        public void fadeOutMissile(SSpaceMissileRenderInfo missilerRenderInfo)
        {
            missilerRenderInfo.missile.fadeOut();
        }

        public void removeAll()
        {
            foreach (var rt in _missileRuntimes) {
                removeMissileRender(rt);
            }
        }

        protected void _initParamsSpecific(SSpaceMissileVisualParameters mParams)
        {
            // smoke effectors
            if (_particleRenderer.textureMaterial == null) {
                _particleRenderer.textureMaterial = new SSTextureMaterial (mParams.smokeParticlesTexture());
            }
            if (_flameSmokeColorEffector == null) {
                _flameSmokeColorEffector = new SSColorKeyframesEffector ();
                _flameSmokeColorEffector.maskMatchFunction = SSParticleEffector.MatchFunction.Equals;
                _flameSmokeColorEffector.effectorMask = (ushort)ParticleEffectorMasks.FlameToSmoke;
                _flameSmokeColorEffector.particleLifetime = mParams.flameSmokeDuration;
                //_smokeColorEffector.colorMask = ;
                _flameSmokeColorEffector.keyframes.Clear();
                var flameColor = mParams.innerFlameColor;
                flameColor.A = 0.9f;
                _flameSmokeColorEffector.keyframes.Add(0f, flameColor);
                flameColor = mParams.outerFlameColor;
                flameColor.A = 0.7f;
                _flameSmokeColorEffector.keyframes.Add(0.1f, flameColor);
                var smokeColor = mParams.smokeColor;
                smokeColor.A = 0.2f;
                _flameSmokeColorEffector.keyframes.Add(0.2f, smokeColor);
                smokeColor.A = 0f;
                _flameSmokeColorEffector.keyframes.Add(1f, smokeColor);

                _particlesData.addEffector(_flameSmokeColorEffector);
            }
            if (_ejectionSmokeColorEffector == null) {
                _ejectionSmokeColorEffector = new SSColorKeyframesEffector ();
                _ejectionSmokeColorEffector.maskMatchFunction = SSParticleEffector.MatchFunction.Equals;
                _ejectionSmokeColorEffector.effectorMask = (ushort)ParticleEffectorMasks.EjectionSmoke;
                _ejectionSmokeColorEffector.particleLifetime = mParams.flameSmokeDuration;
                _ejectionSmokeColorEffector.keyframes.Clear();
                var smokeColor = mParams.smokeColor;
                smokeColor.A = 0.3f;
                _ejectionSmokeColorEffector.keyframes.Add(0f, smokeColor);
                smokeColor.A = 0.14f;
                _ejectionSmokeColorEffector.keyframes.Add(0.2f, smokeColor);
                smokeColor.A = 0f;
                _ejectionSmokeColorEffector.keyframes.Add(1f, smokeColor);

                _particlesData.addEffector(_ejectionSmokeColorEffector);
            }
            if (_flameSmokeScaleEffector == null) {
                _flameSmokeScaleEffector = new SSMasterScaleKeyframesEffector ();
                _flameSmokeScaleEffector.maskMatchFunction = SSParticleEffector.MatchFunction.Equals;
                _flameSmokeScaleEffector.effectorMask = (ushort)ParticleEffectorMasks.FlameToSmoke;
                _flameSmokeScaleEffector.particleLifetime = mParams.flameSmokeDuration;
                _flameSmokeScaleEffector.keyframes.Clear();
                _flameSmokeScaleEffector.keyframes.Add(0f, 0.3f);
                _flameSmokeScaleEffector.keyframes.Add(0.1f, 1f);
                _flameSmokeScaleEffector.keyframes.Add(1f, 2f);

                _particlesData.addEffector(_flameSmokeScaleEffector);
            }
            if (_ejectionSmokeScaleEffector == null) {
                _ejectionSmokeScaleEffector = new SSMasterScaleKeyframesEffector ();
                _ejectionSmokeScaleEffector.maskMatchFunction = SSParticleEffector.MatchFunction.Equals;
                _ejectionSmokeScaleEffector.effectorMask = (ushort)ParticleEffectorMasks.EjectionSmoke;
                _ejectionSmokeScaleEffector.particleLifetime = mParams.ejectionSmokeDuration;
                _ejectionSmokeScaleEffector.keyframes.Clear();
                _ejectionSmokeScaleEffector.keyframes.Add(0f, 0.1f);
                _ejectionSmokeScaleEffector.keyframes.Add(0.35f, 0.8f);
                _ejectionSmokeScaleEffector.keyframes.Add(1f, 1.35f);

                _particlesData.addEffector(_ejectionSmokeScaleEffector);
            }
        }

        protected SSpaceMissileRenderInfo _addMissileRender(SSpaceMissileVisualData missile)
        {
            var missileRuntime = new SSpaceMissileRenderInfo (missile);
            #if MISSILE_SHOW
            _objScene.AddObject(missileRuntime.bodyObj);
            _particlesData.addEmitter(missileRuntime.flameSmokeEmitter);
            #endif
            _missileRuntimes.Add(missileRuntime);

            #if MISSILE_DEBUG
            _objScene.AddObject(missileRuntime.debugRays);
            _screenScene.AddObject(missileRuntime.debugCountdown);
            #endif

            return missileRuntime;
        }

        protected void _removeMissileRender(SSpaceMissileRenderInfo missileRuntime)
        {
            #if MISSILE_SHOW
            missileRuntime.bodyObj.renderState.toBeDeleted = true;
            _particlesData.removeEmitter(missileRuntime.flameSmokeEmitter);
            #endif

            #if MISSILE_DEBUG
            missileRuntime.debugRays.renderState.toBeDeleted = true;
            missileRuntime.debugCountdown.renderState.toBeDeleted = true;
            #endif
        }

        protected void _preRenderUpdate(float timeElapsed)
        {
            for (int i = 0; i < _missileRuntimes.Count; ++i) {
                var missileRuntime = _missileRuntimes [i];
                var missile = missileRuntime.missile;
                if (missile.state == SSpaceMissileVisualData.State.Terminated) {
                    _removeMissileRender(missileRuntime);
                } else {
                    if (missile.state == SSpaceMissileData.State.FadingOut
                     && missileRuntime.bodyObj.alphaBlendingEnabled == false) {
                        // fading missile needs to be moved to the alpha scene (to be drawn last)
                        missileRuntime.bodyObj.alphaBlendingEnabled = true;
                        _objScene.RemoveObject(missileRuntime.bodyObj);
                        _particleScene.AddObject(missileRuntime.bodyObj);
                    }
                    missileRuntime.preRenderUpdate(timeElapsed);
                }
            }
            _missileRuntimes.RemoveAll(
                (missileRuntime) => missileRuntime.missile.state == SSpaceMissileVisualData.State.Terminated);

            // debugging
            #if false
            System.Console.WriteLine("num missiles = " + _missileRuntimes.Count);
            System.Console.WriteLine("num particles = " + _particlesData.numElements);
            #endif
        }

        public class SSpaceMissileRenderInfo
        {
            public readonly SSObjectMesh bodyObj;
            public readonly MissileDebugRays debugRays;
            public readonly SSObject2DSurface_AGGText debugCountdown;

            public readonly SSRadialEmitter flameSmokeEmitter;
            public readonly SSpaceMissileVisualData missile;

            public SSpaceMissileRenderInfo(SSpaceMissileVisualData missile)
            {
                this.missile = missile;
                var mParams = missile.parameters as SSpaceMissileVisualParameters;
                bodyObj = new SSObjectMesh(mParams.missileBodyMesh());
                bodyObj.Scale = new Vector3(mParams.missileBodyScale);
                bodyObj.renderState.castsShadow = false;
                bodyObj.renderState.receivesShadows = false;
                //bodyObj.renderState.visible = false;
                bodyObj.Name = "a missile body";

                #if MISSILE_DEBUG
                debugRays = new MissileDebugRays(missile);
                debugCountdown = new SSObject2DSurface_AGGText();
                debugCountdown.Size = 2f;
                debugCountdown.MainColor = Color4Helper.RandomDebugColor();
                #endif

                flameSmokeEmitter = new SSRadialEmitter();
                flameSmokeEmitter.effectorMask = (ushort)ParticleEffectorMasks.EjectionSmoke;
                flameSmokeEmitter.life = mParams.flameSmokeDuration;
                flameSmokeEmitter.color = new Color4(1f, 1f, 1f, 1f);
                flameSmokeEmitter.billboardXY = true;
                flameSmokeEmitter.particlesPerEmissionMin = mParams.smokePerEmissionMin;
                flameSmokeEmitter.particlesPerEmissionMax = mParams.smokePerEmissionMax;
                flameSmokeEmitter.spriteRectangles = mParams.smokeSpriteRects;
                //smokeEmitter.phiMin = 0f;
                //smokeEmitter.phiMax = (float)Math.PI/6f;
                flameSmokeEmitter.phiMin = (float)Math.PI/3f;
                flameSmokeEmitter.phiMax = (float)Math.PI/2f;
                flameSmokeEmitter.orientationMin = new Vector3 (0f, 0f, 0f);
                flameSmokeEmitter.orientationMax = new Vector3 (0f, 0f, 2f * (float)Math.PI);
                flameSmokeEmitter.angularVelocityMin = new Vector3 (0f, 0f, -1f);
                flameSmokeEmitter.angularVelocityMax = new Vector3 (0f, 0f, +1f);

                flameSmokeEmitter.radiusOffsetMin = 0f;
                flameSmokeEmitter.radiusOffsetMax = 0.1f;  

                // positions emitters and mesh
                preRenderUpdate(0f);
            }

            public void preRenderUpdate(float timeElapsed)
            {
                var mParams = missile.parameters as SSpaceMissileVisualParameters;

                bodyObj.Pos = missile.displayPosition;
                bodyObj.Orient(missile.visualDirection, missile.up);

                bool ejection = missile.state == SSpaceMissileVisualData.State.Ejection;
                float smokeFrequency = Interpolate.Lerp(
                    mParams.smokeEmissionFrequencyMin, mParams.smokeEmissionFrequencyMax, missile.visualSmokeAmmount);
                float sizeMin = ejection ? mParams.ejectionSmokeSizeMin : mParams.flameSmokeSizeMin;
                float sizeMax = ejection ? mParams.ejectionSmokeSizeMax : mParams.flameSmokeSizeMax;
                float smokeSize = Interpolate.Lerp(sizeMin, sizeMax, 
                    (float)SSpaceMissilesVisualSimulation.rand.NextDouble());

                //flameSmokeEmitter.velocity = -missile.velocity;
                flameSmokeEmitter.center = missile.jetPosition();
                flameSmokeEmitter.up = -missile.visualDirection;
                flameSmokeEmitter.emissionInterval = 1f / smokeFrequency;
                flameSmokeEmitter.componentScale = new Vector3(smokeSize);
                flameSmokeEmitter.effectorMask = (ushort)
                    (ejection ? ParticleEffectorMasks.EjectionSmoke : ParticleEffectorMasks.FlameToSmoke);
                var vel = missile.velocity.LengthFast;
                flameSmokeEmitter.velocityFromCenterMagnitudeMin = ejection ? -vel/2f : (-vel / 8f);
                flameSmokeEmitter.velocityFromCenterMagnitudeMax = ejection ? vel/2f : (vel / 5f);
                flameSmokeEmitter.life = ejection ? mParams.ejectionSmokeDuration : mParams.flameSmokeDuration;

                if (missile.state == SSpaceMissileData.State.FadingOut) {
                    float fadeFactor = 1f - (missile.timeSinceLaunch - missile.fadeTime) / mParams.fadeDuration;
                    var color = bodyObj.MainColor;
                    color.A = fadeFactor;
                    bodyObj.MainColor = color;
                    flameSmokeEmitter.life = flameSmokeEmitter.lifeMax * fadeFactor;
                }

                #if MISSILE_DEBUG
                RectangleF clientRect = OpenTKHelper.GetClientRect();
                var xy = OpenTKHelper.WorldToScreen(
                    missile.displayPosition, ref debugRays.viewProjMat, ref clientRect);
                debugCountdown.Pos = new Vector3(xy.X, xy.Y, 0f);
                debugCountdown.Label = Math.Floor(missile.timeToHit).ToString();
                //debugCountdown.Label = missile.losRate.ToString("G3") + " : " + missile.losRateRate.ToString("G3");

                debugRays.renderState.visible = mParams.debuggingAid;
                debugCountdown.renderState.visible = mParams.debuggingAid;
                #endif
            }

            public class MissileDebugRays : SSObject
            {
                protected readonly SSpaceMissileVisualData _missile;
                public Matrix4 viewProjMat; // maintain this matrix to held 2d countdown renderer 

                public MissileDebugRays(SSpaceMissileVisualData missile)
                {
                    _missile = missile;
                    renderState.castsShadow = false;
                    renderState.receivesShadows = false;
                    renderState.frustumCulling = false;
                }

                public override void Render (SSRenderConfig renderConfig)
                {
                    this.Pos = _missile.displayPosition;
                    base.Render(renderConfig);
                    SSShaderProgram.DeactivateAll();
                    GL.LineWidth(3f);
                    GL.Begin(PrimitiveType.Lines);
                    GL.Color4(Color4.LightCyan);
                    GL.Vertex3(Vector3.Zero);
                    GL.Vertex3(_missile._lataxDebug);
                    GL.Color4(Color4.Magenta);
                    GL.Vertex3(Vector3.Zero);
                    GL.Vertex3(_missile._hitTimeCorrAccDebug);
                    GL.End();

                    viewProjMat = renderConfig.invCameraViewMatrix * renderConfig.projectionMatrix;
                }
            }
        }
    }
}



