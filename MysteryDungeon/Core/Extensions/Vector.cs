using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MysteryDungeon.Core.Extensions
{
    //public class Vector2i
    //{
    //    public int X;
    //    public int Y;

    //    public Vector2i()
    //    {
    //        X = 0;
    //        Y = 0;
    //    }
    //    public Vector2i(int x, int y)
    //    {
    //        X = x;
    //        Y = y;
    //    }
    //    public Vector2i(Vector2i v)
    //    {
    //        X = v.X;
    //        Y = v.Y;
    //    }

    //    public static Vector2i Zero()
    //        => new Vector2i(0, 0);

    //    public static Vector2i One()
    //        => new Vector2i(1, 1);

    //    public static Vector2i operator +(Vector2i a) => a;
    //    public static Vector2i operator -(Vector2i a) => new Vector2i(-a.X, -a.Y);

    //    public static Vector2i operator +(Vector2i a, Vector2i b)
    //        => new Vector2i(a.X + b.X, a.Y + b.Y);
    //    public static Vector2i operator -(Vector2i a, Vector2i b)
    //        => new Vector2i(a.X - b.X, a.Y - b.Y);
    //    public static Vector2i operator *(Vector2i a, Vector2i b)
    //        => new Vector2i(a.X * b.X, a.Y * b.Y);
    //    public static Vector2i operator /(Vector2i a, Vector2i b)
    //        => new Vector2i(a.X / b.X, a.Y / b.Y);
    //}
    //public class Vector3i : Vector2i
    //{
    //    public int Z;
        
    //    public Vector3i() : base()
    //    {
    //        Z = 0;
    //    }
    //    public Vector3i(int x, int y, int z) : base(x, y)
    //    {
    //        Z = z;
    //    }
    //    public Vector3i(Vector2i v, int z) : base(v)
    //    {
    //        Z = z;
    //    }
    //    public Vector3i(Vector3i v) : base(v.X, v.Y)
    //    {
    //        Z = v.Z;
    //    }

    //    new public static Vector3i Zero()
    //        => new Vector3i(0, 0, 0);

    //    new public static Vector3i One()
    //        => new Vector3i(1, 1, 1);

    //    public static Vector3i operator +(Vector3i a) => a;
    //    public static Vector3i operator -(Vector3i a) => new Vector3i(-a.X, -a.Y, -a.Z);

    //    public static Vector3i operator +(Vector3i a, Vector3i b)
    //        => new Vector3i(a.X + b.X, a.Y + b.Y, a.Z + b.Z);

    //    public static Vector3i operator -(Vector3i a, Vector3i b)
    //        => new Vector3i(a.X - b.X, a.Y - b.Y, a.Z + b.Z);

    //    public static Vector3i operator *(Vector3i a, Vector3i b)
    //        => new Vector3i(a.X * b.X, a.Y * b.Y, a.Z * b.Z);

    //    public static Vector3i operator /(Vector3i a, Vector3i b)
    //        => new Vector3i(a.X / b.X, a.Y / b.Y, a.Z / b.Z);
    //}

    //public class Vector2f
    //{
    //    public float X;
    //    public float Y;

    //    public Vector2f()
    //    {
    //        X = 0;
    //        Y = 0;
    //    }

    //    public Vector2f(float x, float y)
    //    {
    //        X = x;
    //        Y = y;
    //    }

    //    public Vector2f(Vector2f v)
    //    {
    //        X = v.X;
    //        Y = v.Y;
    //    }

    //    public static Vector2f Zero()
    //        => new Vector2f(0.0f, 0.0f);

    //    public static Vector2f One()
    //        => new Vector2f(1.0f, 1.0f);

    //    public static Vector2f operator +(Vector2f a) => a;
    //    public static Vector2f operator -(Vector2f a) => new Vector2f(-a.X, -a.Y);

    //    public static Vector2f operator +(Vector2f a, Vector2f b)
    //        => new Vector2f(a.X + b.X, a.Y + b.Y);

    //    public static Vector2f operator -(Vector2f a, Vector2f b)
    //        => new Vector2f(a.X - b.X, a.Y - b.Y);

    //    public static Vector2f operator *(Vector2f a, Vector2f b)
    //        => new Vector2f(a.X * b.X, a.Y * b.Y);

    //    public static Vector2f operator /(Vector2f a, Vector2f b)
    //        => new Vector2f(a.X / b.X, a.Y / b.Y);
    //}
    //public class Vector3f : Vector2f
    //{
    //    public float Z;

    //    public Vector3f() : base()
    //    {
    //        Z = 0;
    //    }
    //    public Vector3f(float x, float y, float z) : base(x, y)
    //    {
    //        Z = z;
    //    }
    //    public Vector3f(Vector2f v, float z) : base(v)
    //    {
    //        Z = z;
    //    }
    //    public Vector3f(Vector3f v) : base(v.X, v.Y)
    //    {
    //        Z = v.Z;
    //    }

    //    new public static Vector3f Zero()
    //        => new Vector3f(0, 0, 0);

    //    new public static Vector3f One()
    //        => new Vector3f(1, 1, 1);

    //    public static Vector3f operator +(Vector3f a) => a;
    //    public static Vector3f operator -(Vector3f a) => new Vector3f(-a.X, -a.Y, -a.Z);

    //    public static Vector3f operator +(Vector3f a, Vector3f b)
    //        => new Vector3f(a.X + b.X, a.Y + b.Y, a.Z + b.Z);

    //    public static Vector3f operator -(Vector3f a, Vector3f b)
    //        => new Vector3f(a.X - b.X, a.Y - b.Y, a.Z + b.Z);

    //    public static Vector3f operator *(Vector3f a, Vector3f b)
    //        => new Vector3f(a.X * b.X, a.Y * b.Y, a.Z * b.Z);

    //    public static Vector3f operator /(Vector3f a, Vector3f b)
    //        => new Vector3f(a.X / b.X, a.Y / b.Y, a.Z / b.Z);
    //}

    //public class Vector2d
    //{
    //    public double X;
    //    public double Y;

    //    public Vector2d()
    //    {
    //        X = 0;
    //        Y = 0;
    //    }

    //    public Vector2d(double x, double y)
    //    {
    //        X = x;
    //        Y = y;
    //    }

    //    public static Vector2d Zero()
    //        => new Vector2d(0.0, 0.0);

    //    public static Vector2d One()
    //        => new Vector2d(1.0, 1.0);

    //    public static Vector2d operator +(Vector2d a) => a;
    //    public static Vector2d operator -(Vector2d a) => new Vector2d(-a.X, -a.Y);

    //    public static Vector2d operator +(Vector2d a, Vector2d b)
    //        => new Vector2d(a.X + b.X, a.Y + b.Y);

    //    public static Vector2d operator -(Vector2d a, Vector2d b)
    //        => new Vector2d(a.X - b.X, a.Y - b.Y);

    //    public static Vector2d operator *(Vector2d a, Vector2d b)
    //        => new Vector2d(a.X * b.X, a.Y * b.Y);

    //    public static Vector2d operator /(Vector2d a, Vector2d b)
    //        => new Vector2d(a.X / b.X, a.Y / b.Y);
    //}

    public class Vector2<T> //Dit is blessed en cursed tegelijk honestly, TODO: operations zoals dot product etc.
    {
        public T X;
        public T Y;

        public Vector2()
        {
            X = default;
            Y = default;
        }
        public Vector2(T x, T y)
        {
            X = x;
            Y = y;
        }
        public Vector2(Vector2<T> v) : this(v.X, v.Y)
        {

        }

        #region static
        public static Vector2<T> Zero()
            => new Vector2<T>(default, default);
        public static Vector2<T> One()
            => new Vector2<T>((dynamic)default + 1, (dynamic)default + 1);

        //Static operations

        public static Vector2<T> Sum(Vector2<T> augend, Vector2<T> addend)
            => new Vector2<T>((dynamic)augend.X + (dynamic)addend.X, (dynamic)augend.Y + (dynamic)addend.Y);

        public static Vector2<T> Difference(Vector2<T> minuend, Vector2<T> subtrahend)
            => new Vector2<T>((dynamic)minuend.X - (dynamic)subtrahend.X, (dynamic)minuend.Y - (dynamic)subtrahend.Y);

        public static Vector2<T> Product(Vector2<T> multiplicand, Vector2<T> multiplier)
            => new Vector2<T>((dynamic)multiplicand.X * (dynamic)multiplier.X, (dynamic)multiplicand.Y * (dynamic)multiplier.Y);

        public static Vector2<T> Quotient(Vector2<T> dividend, Vector2<T> divisor)
            => new Vector2<T>((dynamic)dividend.X / (dynamic)divisor.X, (dynamic)dividend.Y / (dynamic)divisor.Y);

        //Math functions

        public static Vector2<T> Lerp(Vector2<T> start, Vector2<T> end, float amount)
            => new Vector2<T>(start + (end - start) * new Vector2<T>((T)(dynamic)amount, (T)(dynamic)amount));
        #endregion

        #region operator
        public static Vector2<T> operator +(Vector2<T> v) => v;
        public static Vector2<T> operator -(Vector2<T> v) => new Vector2<T>( -(dynamic)v.X, -(dynamic)v.Y);

        public static Vector2<T> operator +(Vector2<T> addend, Vector2<T> augend)
            => new Vector2<T>((dynamic)addend.X + (dynamic)augend.X, (dynamic)addend.Y + (dynamic)augend.Y);
        public static Vector2<T> operator +(Vector2<T> addend, T augend)
            => new Vector2<T>((dynamic)addend.X + (dynamic)augend, (dynamic)addend.Y + (dynamic)augend);

        public static Vector2<T> operator -(Vector2<T> minuend, Vector2<T> subtrahend)
            => new Vector2<T>((dynamic)minuend.X - (dynamic)subtrahend.X, (dynamic)minuend.Y - (dynamic)subtrahend.Y);
        public static Vector2<T> operator -(Vector2<T> minuend, T subtrahend)
            => new Vector2<T>((dynamic)minuend.X - (dynamic)subtrahend, (dynamic)minuend.Y - (dynamic)subtrahend);

        public static Vector2<T> operator *(Vector2<T> multiplicand, Vector2<T> multiplier)
            => new Vector2<T>((dynamic)multiplicand.X * (dynamic)multiplier.X, (dynamic)multiplicand.Y * (dynamic)multiplier.Y);
        public static Vector2<T> operator *(Vector2<T> multiplicand, T multiplier)
            => new Vector2<T>((dynamic)multiplicand.X * (dynamic)multiplier, (dynamic)multiplicand.Y * (dynamic)multiplier);

        public static Vector2<T> operator /(Vector2<T> dividend, Vector2<T> divisor)
            => new Vector2<T>((dynamic)dividend.X / (dynamic)divisor.X, (dynamic)dividend.Y / (dynamic)divisor.Y);
        public static Vector2<T> operator /(Vector2<T> dividend, T divisor)
            => new Vector2<T>((dynamic)dividend.X / (dynamic)divisor, (dynamic)dividend.Y / (dynamic)divisor);
        #endregion
    }

    public class Vector3<T> : Vector2<T>
    {
        public T Z;

        public Vector3() : base()
        {
            Z = default;
        }
        public Vector3(T x, T y, T z) : base(x, y)
        {
            Z = z;
        }
        public Vector3(Vector3<T> v) : base(v.X, v.Y)
        {
            Z = v.Z;
        }
        public Vector3(Vector2<T> v, T z) : base(v.X, v.Y)
        {
            Z = z;
        }

        #region static
        new public static Vector3<T> Zero()
            => new Vector3<T>(default, default);
        new public static Vector3<T> One()
            => new Vector3<T>((dynamic)default + 1, (dynamic)default + 1, (dynamic)default + 1);

        public static Vector3<T> Sum(Vector3<T> augend, Vector3<T> addend)
            => new Vector3<T>((dynamic)augend.X + (dynamic)addend.X, (dynamic)augend.Y + (dynamic)addend.Y, (dynamic)augend.Z + (dynamic)addend.Z);

        public static Vector3<T> Difference(Vector3<T> minuend, Vector3<T> subtrahend)
            => new Vector3<T>((dynamic)minuend.X - (dynamic)subtrahend.X, (dynamic)minuend.Y - (dynamic)subtrahend.Y, (dynamic)minuend.Z - (dynamic)subtrahend.Z);

        public static Vector3<T> Product(Vector3<T> multiplicand, Vector3<T> multiplier)
            => new Vector3<T>((dynamic)multiplicand.X * (dynamic)multiplier.X, (dynamic)multiplicand.Y * (dynamic)multiplier.Y, (dynamic)multiplicand.Z * (dynamic)multiplier.Z);

        public static Vector3<T> Quotient(Vector3<T> dividend, Vector3<T> divisor)
            => new Vector3<T>((dynamic)dividend.X / (dynamic)divisor.X, (dynamic)dividend.Y / (dynamic)divisor.Y, (dynamic)dividend.Z / (dynamic)divisor.Z);
        #endregion

        #region operator
        public static Vector3<T> operator +(Vector3<T> v) => v;
        public static Vector3<T> operator -(Vector3<T> v) => new Vector3<T>(-(dynamic)v.X, -(dynamic)v.Y, -(dynamic)v.Z);

        public static Vector3<T> operator +(Vector3<T> addend, Vector3<T> augend)
            => new Vector3<T>((dynamic)addend.X + (dynamic)augend.X, (dynamic)addend.Y + (dynamic)augend.Y, (dynamic)addend.Z + (dynamic)augend.Z);

        public static Vector3<T> operator -(Vector3<T> minuend, Vector3<T> subtrahend)
            => new Vector3<T>((dynamic)minuend.X - (dynamic)subtrahend.X, (dynamic)minuend.Y - (dynamic)subtrahend.Y, (dynamic)minuend.Z - (dynamic)subtrahend.Z);

        public static Vector3<T> operator *(Vector3<T> multiplicand, Vector3<T> multiplier)
            => new Vector3<T>((dynamic)multiplicand.X * (dynamic)multiplier.X, (dynamic)multiplicand.Y * (dynamic)multiplier.Y, (dynamic)multiplicand.Z * (dynamic)multiplier.Z);

        public static Vector3<T> operator /(Vector3<T> dividend, Vector3<T> divisor)
            => new Vector3<T>((dynamic)dividend.X / (dynamic)divisor.X, (dynamic)dividend.Y / (dynamic)divisor.Y, (dynamic)dividend.Z / (dynamic)divisor.Z);
        #endregion
    }
}
