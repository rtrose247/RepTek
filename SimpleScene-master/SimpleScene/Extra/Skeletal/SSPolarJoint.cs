﻿using System;
using OpenTK;

namespace SimpleScene.Demos
{
	/// <summary>
	/// See Common Robot Configurations: http://nptel.ac.in/courses/112103174/module7/lec5/3.html
	/// </summary>
	public class SSPolarJoint : SSCustomizedJoint
	{
		public Vector3 thetaAxis = Vector3.UnitY;
		public Vector3 phiAxis = Vector3.UnitZ;
		public Vector3 positionOffset = Vector3.Zero; 

		public SSComparableJointParameter<float> theta 
			= new SSComparableJointParameter<float>(float.NegativeInfinity, float.PositiveInfinity, 0f);
		public SSComparableJointParameter<float> phi
			= new SSComparableJointParameter<float>(float.NegativeInfinity, float.PositiveInfinity, 0f);

		public override SSSkeletalJointLocation computeJointLocation (SSSkeletalJointRuntime joint)
		{
			SSSkeletalJointLocation ret;
			ret.position = positionOffset;

			var thetaRot = Quaternion.FromAxisAngle (thetaAxis, theta.value);
			var phiRot = Quaternion.FromAxisAngle (phiAxis, phi.value);
			ret.orientation = Quaternion.Multiply (thetaRot, phiRot);
			return ret;
		}
	}

	//---------------------------

	public class SSSimpleJointParameter<T>
	{
		public virtual T value { get; set; }
	}

	public class SSComparableJointParameter<T> : SSSimpleJointParameter<T> where T: IComparable
	{
		protected T _min;
		protected T _max;

		public T min {
			get { return _min; }
			set {
				_min = value;
				if (base.value.CompareTo(_min) < 0) {
					base.value = _min;
				}
			}
		}

		public T max {
			get { return _max; }
			set {
				_max = value;
				if (base.value.CompareTo(_max) > 0) {
					base.value = _max;
				}
			}
		}

		public override T value {
			get { return base.value; }
			set {
				if (value.CompareTo(_max) > 0 || value.CompareTo(_min) < 0) {
					var errMsg = string.Format (
						"Joint parameter out of range: {0}; allowed range is [{1}:{2}]", 
						value, _min, _max);
					System.Console.WriteLine (errMsg);
					throw new Exception (errMsg);
				}
				base.value = value;
			}
		}

		public SSComparableJointParameter(T min, T max, T val)
		{
			this.min = min;
			this.max = max;
			base.value = val;
		}
	}


}

