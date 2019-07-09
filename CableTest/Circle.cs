using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Numerics;


namespace CableTest
{
    class Circle
    {
        public float _cRadius;
        public Vector2 _cPosition;
        public Vector2 _cVector;
        public bool IsDone = false;
        public Color _cColor;

        private static Random RND = new Random();


        public Circle(float _nRadius, Vector2 _nPosition)
        {
            this._cRadius = _nRadius;
            this._cVector = new Vector2();
            this._cPosition = _nPosition;

            _cColor = Color.FromArgb(RND.Next(256), RND.Next(256), RND.Next(256));
        }

        public void MoveCircle()
        {
            _cPosition.X += _cVector.X;
            _cPosition.Y += _cVector.Y;
        }





        
    }
}
