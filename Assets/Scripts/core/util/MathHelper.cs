/// <summary>
/// Math helper.
/// </summary>
/* Incorporates functions adapted from asilvadevlib. LICENSE:
Copyright (c) 2011 Alessandro Ribeiro da Silva
http://alessandrosilva.com
alessandro.ribeiro.silva 'at' gmail.com

Alessandro Silva Development Library (aSilvaDevLib).

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.

------

Disclaimer for Robert Penner's Easing Equations license:

TERMS OF USE - EASING EQUATIONS

Open source under the BSD License.

Copyright © 2001 Robert Penner
All rights reserved.

Redistribution and use in source and binary forms, with or without modification, 
are permitted provided that the following conditions are met:

    * Redistributions of source code must retain the above copyright notice, 
      this list of conditions and the following disclaimer.
    * Redistributions in binary form must reproduce the above copyright notice, 
      this list of conditions and the following disclaimer in the documentation 
      and/or other materials provided with the distribution.
    * Neither the name of the author nor the names of contributors may be used to 
      endorse or promote products derived from this software without specific 
      prior written permission.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS"
AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, 
THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR 
PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS 
BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR 
CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE 
GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) 
HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, 
STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING 
IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY 
OF SUCH DAMAGE.
*/

namespace com.eliotlash.core.util {
	public class MathHelper {

		/// <summary>
		/// This implementation of the modulus operator correctly wraps negative numbers, i.e. for safe array indexing.
		/// so n % m becomes mod(n, m)
		/// Based on http://stackoverflow.com/a/10066136/350761
		/// </summary>
		/// <param name="n">N.</param>
		/// <param name="m">M.</param>
		public static int mod(int n, int m) { 
			return ((n % m) + m) % m;
		}

		/// <summary>
		/// Easing equation function for a back (overshooting cubic easing: (s+1)*t^3 - s*t^2) easing out: decelerating from zero velocity.
		/// Based on code from http://svn.alessandrosilva.com/asilvadevlib/trunk/src/asilva/math/EasingEquations.h (BSD-style license, see above)
		/// </summary>
		/// <returns>Interpolated value.</returns>
		/// <param name="startValue">Start value.</param>
		/// <param name="endValue">End value.</param>
		/// <param name="lerp">Value between [0..1] to indicate the interpolation position.</param>
		/// <param name="overshoot">Overshoot.</param>
		public static float easeOutBack(float startValue, float endValue, float lerp, float overshoot = 1.70158f) {
			float delta = endValue - startValue;
			lerp -= 1.0f;
			return delta*( lerp*lerp*((overshoot+1.0f)*lerp + overshoot) + 1.0f ) + startValue;
		}

	}
}
