using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Numerics;
using System.Diagnostics;
using System.Windows.Forms;

namespace CableTest
{
    class CableSolver
    {
        public List<Circle> _xCircles = new List<Circle>();
        private Vector2 _mOrigin;
        public int _actCircle = 1;
        public Circle _mainCircle;
        private Timer _mainTimer;
        private Button _mainBN;

        

        public CableSolver(Vector2 _xOrigin, Timer _timer, Button _bn)
        {
            _mOrigin = _xOrigin;
            _mainCircle = new Circle(360, new Vector2(-1000,1000));
            _mainTimer = _timer;
            _mainBN = _bn;
        }

      

        public void CreateCircle(float _cRadius)
        {
            Circle _nCircle = new Circle(_cRadius, _mOrigin);
            _xCircles.Add(_nCircle);
           
        }

        private int Comparer(Circle p1, Circle p2)
        {

            if (p1._cRadius < p2._cRadius)
                return 1;
            else if (p1._cRadius > p2._cRadius)
                return -1;
            else return 0;
        }

        public void SortCircles()
        {
            _xCircles.Sort(Comparer);
            _initCirclePos();
            _mainTimer.Start();
        }       


        private void _initCirclePos()
        {
            _xCircles[0]._cPosition = _mOrigin;
           
            float _add = 0;
            float _degrees = 20;
            for (int i = 1; i < ((_xCircles.Count() / 2) - 1); i++)
            {
                Circle _circle = _xCircles[i];

                if (_circle._cRadius > 20)
                {
                    Vector2 _currVector = SetCirclesvectors(_add);
                    _circle._cPosition.X = _mOrigin.X + _currVector.X;
                    _circle._cPosition.Y = _mOrigin.Y + _currVector.Y;

                    _add += _degrees;
                }
                else
                {
                    Vector2 _currVector = SetCirclesvectors(10 + i);
                    _circle._cPosition.X = _mOrigin.X + _currVector.X;
                    _circle._cPosition.Y = _mOrigin.Y + _currVector.Y;
                }
            }

            _add = 6;
            _degrees = 20;
            for (int i = ((_xCircles.Count() / 2)-1); i < _xCircles.Count(); i++)
            {
                Circle _circle = _xCircles[i];

                if (_circle._cRadius > 20)
                {
                    Vector2 _currVector = SetCirclesvectors(_add);
                    _circle._cPosition.X = _mOrigin.X + _currVector.X;
                    _circle._cPosition.Y = _mOrigin.Y - _currVector.Y;

                    _add += _degrees;
                }
                else
                {
                    Vector2 _currVector = SetCirclesvectors(10 + i);
                    _circle._cPosition.X = _mOrigin.X + _currVector.X;
                    _circle._cPosition.Y = _mOrigin.Y + _currVector.Y;
                }
            }
        }

      


        private Vector2 SetCirclesvectors(double _degrees)
        {
            float x_oncircle = _mOrigin.X + 5 * (float)Math.Cos(_degrees * Math.PI / 180);
            float y_oncircle = _mOrigin.Y + 5 * (float)Math.Sin(_degrees * Math.PI / 180);

            Vector2 _dirVector = Vector2.Subtract(_mOrigin, new Vector2(x_oncircle, y_oncircle));

            return _dirVector;
        }



        public void MoveCircles()
        {

            if (_actCircle == _xCircles.Count())
            {
                _mainTimer.Stop();
                FindMainCircle();
                _mainBN.Enabled = true;
                return;
            }

            Vector2 _oldPos = _xCircles[_actCircle]._cPosition;

            CheckCircles(_actCircle);

            if (_oldPos == _xCircles[_actCircle]._cPosition)
            {
                 _actCircle++;
            }
           
        }

        public void FindMainCircle()
        {
            List<float> _xMin = new List<float>();
            List<float> _yMin = new List<float>();

            foreach (Circle _circle in _xCircles)
            {
                _xMin.Add(_circle._cPosition.X - (_circle._cRadius / 2));
                _yMin.Add(_circle._cPosition.Y - (_circle._cRadius / 2));

            }

            _xMin.Sort();
            _yMin.Sort();

            _mainCircle._cPosition = new Vector2( _xMin[0],  _yMin[0]);
        }


        
        private void CheckCircles(int i)
        {
            Circle _tCircle = _xCircles[i];
            for (int j = 0; j < _xCircles.Count(); j++)
            {
                if (i == j) continue;
               
                float d = Vector2.Distance(_tCircle._cPosition, _xCircles[j]._cPosition);

                if (d < ((_tCircle._cRadius / 2) + (_xCircles[j]._cRadius) / 2))
                {
                    _tCircle._cVector = GetSeparationForce(_tCircle, _xCircles[j]);
                    _tCircle.MoveCircle();
                }

            }
        }

        private Vector2 GetSeparationForce(Circle n1, Circle n2)
        {
            Vector2 steer = new Vector2(0, 0);
            float d = Vector2.Distance(n1._cPosition, n2._cPosition);
            if ((d > 0) && (d < n1._cRadius / 2 + n2._cRadius / 2))
            {
                Vector2 diff = Vector2.Subtract(n1._cPosition, n2._cPosition);
                diff = Vector2.Normalize(diff);
                diff = Vector2.Divide(diff, d);
                steer = Vector2.Add(steer, diff);

            }
            steer = Vector2.Multiply(steer, 300);
            return steer;
        }





    }
}
