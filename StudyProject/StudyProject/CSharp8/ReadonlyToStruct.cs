using System;
using System.Collections.Generic;
using System.Text;

namespace StudyProject.CSharp8
{
    public struct ReadonlyToStruct
    {
        public double X { get; set; }
        public double Y { get; set; }
        public readonly double Distance => Math.Sqrt(X * X + Y * Y);

        public override string ToString() =>
            $"({X},{Y}) is {Distance} from the origin";
    }
}
