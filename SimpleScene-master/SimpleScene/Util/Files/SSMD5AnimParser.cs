﻿using System;
using System.IO;
using System.Globalization;
using System.Text.RegularExpressions;
using OpenTK;

namespace SimpleScene.Demos
{
	// only used to register with the asset manager
	public class SSSkeletalAnimationMD5 : SSSkeletalAnimation
	{ 
		public SSSkeletalAnimationMD5 (int frameRate,
									   SSSkeletalJoint[] jointInfo,
									   SSSkeletalJointLocation[][] frames,
									   SSAABB[] bounds)
			: base(frameRate, jointInfo, frames, bounds)
		{ }
	}

	public class SSMD5AnimParser : SSMD5Parser
	{
        public static SSSkeletalAnimationMD5 ReadAnimation(string path)
		{
			var parser = new SSMD5AnimParser(path);
			var anim = parser.readAnimation ();
            anim.name = Path.GetFileName(path);
			return anim;
		}

		private enum LocationFlags : byte {
			Tx = 1, Ty = 2, Tz = 4,
			Qx = 8, Qy = 16, Qz = 32
		}

        private SSMD5AnimParser (string path)
			: base(path)
		{
		}

		private SSSkeletalAnimationMD5 readAnimation()
		{
			Match[] matches;

			// header
			seekEntry ("MD5Version", "10");
			seekEntry ("commandline", SSMD5Parser._quotedStrRegex);

			matches = seekEntry ("numFrames", SSMD5Parser._uintRegex);
			var numFrames = Convert.ToInt32 (matches [1].Value);

			matches = seekEntry ("numJoints", SSMD5Parser._uintRegex);
			var numJoints = Convert.ToInt32 (matches [1].Value);

			matches = seekEntry ("frameRate", SSMD5Parser._uintRegex);
			var frameRate = Convert.ToInt32 (matches [1].Value);

			matches = seekEntry ("numAnimatedComponents", SSMD5Parser._uintRegex);
			int numAnimatedComponents = Convert.ToInt32 (matches [1].Value);
			var floatComponents = new float[numAnimatedComponents];

			// hierarchy
			seekEntry ("hierarchy", "{");
			var hierarchy = new SSSkeletalJoint[numJoints];
			var flags = new byte[numJoints];
			for (int j = 0; j < numJoints; ++j) {
				hierarchy [j] = readHierarchyEntry (out flags [j]);
			}
			seekEntry ("}");

			// bounds
			seekEntry ("bounds", "{");
			var bounds = new SSAABB[numFrames];
			for (int f = 0; f < numFrames; ++f) {
				bounds [f] = readBounds ();
			}
			seekEntry ("}");

			// base frame
			seekEntry ("baseframe", "{");
			for (int j = 0; j < numJoints; ++j) {
				hierarchy[j].bindPoseLocation = readBaseFrame ();
			}
			seekEntry ("}");

			// frames
			var frames = new SSSkeletalJointLocation[numFrames][];
			for (int f = 0; f < numFrames; ++f) {
				matches = seekEntry ("frame", SSMD5Parser._uintRegex, "{");
				int frameIdx = Convert.ToInt32 (matches [1].Value);
				frames [frameIdx] = readFrameJoints (flags, hierarchy, floatComponents);
				seekEntry ("}");
			}
			return new SSSkeletalAnimationMD5 (frameRate, hierarchy, frames, bounds);
		}

		private SSSkeletalJoint readHierarchyEntry(out byte flags)
		{
			Match[] matches = seekEntry(
				SSMD5Parser._quotedStrRegex, // name
				SSMD5Parser._intRegex, // parent index
				SSMD5Parser._uintRegex, // flags
				SSMD5Parser._uintRegex // start index (currently not used)
			);
			SSSkeletalJoint ret = new SSSkeletalJoint();
			ret.name = matches[0].Value;
			ret.parentIndex = Convert.ToInt32(matches[1].Value);
			flags = Convert.ToByte(matches[2].Value);
			//m_startIndex = Convert.ToInt32(matches[3].Value);
			return ret;
		}


		private SSAABB readBounds()
		{
			Match[] matches = seekEntry (
				SSMD5Parser._parOpen, 
					SSMD5Parser._floatRegex,
					SSMD5Parser._floatRegex,
					SSMD5Parser._floatRegex,
				SSMD5Parser._parClose,
				SSMD5Parser._parOpen,
					SSMD5Parser._floatRegex,
					SSMD5Parser._floatRegex,
					SSMD5Parser._floatRegex,
				SSMD5Parser._parClose
			);
			SSAABB ret;
			ret.Min.X = (float)Convert.ToDouble (matches [1].Value, CultureInfo.InvariantCulture);
			ret.Min.Y = (float)Convert.ToDouble (matches [2].Value, CultureInfo.InvariantCulture);
			ret.Min.Z = (float)Convert.ToDouble (matches [3].Value, CultureInfo.InvariantCulture);
			ret.Max.X = (float)Convert.ToDouble (matches [6].Value, CultureInfo.InvariantCulture);
			ret.Max.Y = (float)Convert.ToDouble (matches [7].Value, CultureInfo.InvariantCulture);
			ret.Max.Z = (float)Convert.ToDouble (matches [8].Value, CultureInfo.InvariantCulture);
			return ret;
		}

		private SSSkeletalJointLocation readBaseFrame()
		{
			Match[] matches = seekEntry (
				SSMD5Parser._parOpen, 
					SSMD5Parser._floatRegex,
					SSMD5Parser._floatRegex,
					SSMD5Parser._floatRegex,
				SSMD5Parser._parClose,
				SSMD5Parser._parOpen,
					SSMD5Parser._floatRegex,
					SSMD5Parser._floatRegex,
					SSMD5Parser._floatRegex,
					SSMD5Parser._parClose
			);
			SSSkeletalJointLocation loc;
			loc.position.X = (float)Convert.ToDouble (matches [1].Value, CultureInfo.InvariantCulture);
			loc.position.Y = (float)Convert.ToDouble (matches [2].Value, CultureInfo.InvariantCulture);
			loc.position.Z = (float)Convert.ToDouble (matches [3].Value, CultureInfo.InvariantCulture);
			loc.orientation = new Quaternion ();
			loc.orientation.X = (float)Convert.ToDouble (matches [6].Value, CultureInfo.InvariantCulture);
			loc.orientation.Y = (float)Convert.ToDouble (matches [7].Value, CultureInfo.InvariantCulture);
			loc.orientation.Z = (float)Convert.ToDouble (matches [8].Value, CultureInfo.InvariantCulture);
			loc.computeQuatW ();
			return loc;
		}



		private SSSkeletalJointLocation[] readFrameJoints(byte[] jointFlags,
														  SSSkeletalJoint[] jointInfos,
														  float[] floatComponents)
		{
			seekFloats (floatComponents);
			var thisFrameLocations = new SSSkeletalJointLocation[jointInfos.Length];
			int compIdx = 0;
			for (int j = 0; j < jointInfos.Length; ++j) {
				byte flags = jointFlags[j];
				SSSkeletalJoint jointInfo = jointInfos [j];
				SSSkeletalJointLocation loc = jointInfo.bindPoseLocation;
				if ((flags & (byte)LocationFlags.Tx) != 0) {
					loc.position.X = floatComponents [compIdx++];
				}
				if ((flags & (byte)LocationFlags.Ty) != 0) {
					loc.position.Y = floatComponents [compIdx++];
				}
				if ((flags & (byte)LocationFlags.Tz) != 0) {
					loc.position.Z = floatComponents [compIdx++];
				}
				if ((flags & (byte)LocationFlags.Qx) != 0) {
					loc.orientation.X = floatComponents [compIdx++];
				}
				if ((flags & (byte)LocationFlags.Qy) != 0) {
					loc.orientation.Y = floatComponents [compIdx++];
				}
				if ((flags & (byte)LocationFlags.Qz) != 0) {
					loc.orientation.Z = floatComponents [compIdx++];
				}
				loc.computeQuatW ();

				#if false
				if (jointInfo.ParentIndex >= 0) { // has a parent
					SSSkeletalJointLocation parentLoc = thisFrameLocations [jointInfo.ParentIndex];
					loc.Position = parentLoc.Position 
						+ Vector3.Transform (loc.Position, parentLoc.Orientation);
					loc.Orientation = Quaternion.Multiply (parentLoc.Orientation, 
						loc.Orientation);
					loc.Orientation.Normalize ();
				}
				#endif
				thisFrameLocations[j] = loc;
			}
			return thisFrameLocations;
		}
	}
}

