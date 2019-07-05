﻿using System;
using System.Collections.Generic;
using System.Text;
using NnCase.IR;

namespace NnCase.Evaluation.Operators
{
    public static class OpUtility
    {
        public static RuntimeShape To(Shape shape)
        {
            ValidateShape(shape);
            var inExt = 4 - shape.Count;
            var rtShape = new RuntimeShape();
            for (int i = 0; i < inExt; i++)
                rtShape[i] = 1;
            for (int i = inExt; i < 4; i++)
                rtShape[i] = shape[i - inExt];
            return rtShape;
        }

        public static (RuntimeShape rtInShape, RuntimeShape rtPerm) ExtendTransposeShape(Shape inShape, Shape perm)
        {
            ValidateShape(inShape);
            ValidateShape(perm);

            RuntimeShape rtInShape;
            RuntimeShape rtPerm;

            var inExt = 4 - inShape.Count;
            var permExt = 4 - perm.Count;
            rtInShape = To(inShape);

            for (int i = 0; i < permExt; i++)
                rtPerm[i] = i;
            for (int i = 0; i < perm.Count; i++)
                rtPerm[i + permExt] = perm[i] + inExt;

            return (rtInShape, rtPerm);
        }

        private static void ValidateShape(Shape shape)
        {
            if (shape.Count > 4)
                throw new ArgumentException($"Runtime only support up to 4 rank, but got {shape.Count} rank");
        }
    }
}