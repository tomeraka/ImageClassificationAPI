using Microsoft.ML.OnnxRuntime.Tensors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageClassificationAPI
{
    public class Classification
    {
        public string Prediction { get; set; }
        public double Confidence { get; set; }

        public Classification(string Prediction, double Confidence)
        {
            this.Prediction = Prediction;
            this.Confidence = Confidence;
        }

        public static Classification Classify(DenseTensor<float> tensor)
        {
            var clf = new Classification("Dog", 0.5);
            return clf;
        }
    }
}
