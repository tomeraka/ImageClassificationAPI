using Microsoft.ML.OnnxRuntime;
using Microsoft.ML.OnnxRuntime.Tensors;
using System;
using System.Collections.Generic;
using System.IO;
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

        public static List<string> ReadLabels()
        {
            List<string> LabelList = new List<string>();
            foreach (string line in File.ReadLines(@"onnx-models/labels.txt"))
            {
                LabelList.Append(line);
            }
            return LabelList;
        }

        public static Classification Classify(DenseTensor<float> tensor)
        {
            var inputs = new List<NamedOnnxValue>() { NamedOnnxValue.CreateFromTensor("inputs", tensor) };
            var outputs = new List<string> { };
            using var session = new InferenceSession(@"onnx-models/fine_tuned_mobilenet.onnx");
            using var predictions = session.Run(inputs, outputs);

            var clf = new Classification("Dog", 0.5);
            return clf;
        }
    }
}
