using System;
using System.Collections.Generic;
using System.Text;

namespace FuelConsumptionApp.Model
{
    class Math
    {
        public static float CalculateInnerProduct(float[] vector1, float[] vector2)
        {
            if (vector1.Length == 0 || vector2.Length == 0 || (vector1.Length != vector2.Length))
                return 0;

            float inner_product = 0;
            for (int i = 0; i < vector1.Length; i++)
            {
                inner_product += vector1[i] * vector2[i];
            }

            return inner_product;
        }
    }
}
