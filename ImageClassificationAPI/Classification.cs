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

        public const string MODEL_FILE = "fine_tuned_mobilenet.onnx";
        public const string MODEL_INPUTS = "input_1";
        public const string MODEL_OUTPUTS = "dense";

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
                LabelList.Add(line);
            }
            return LabelList;
        }

        public static Classification[] Classify(DenseTensor<float> tensor)
        {
            var inputs = new List<NamedOnnxValue>() { NamedOnnxValue.CreateFromTensor(MODEL_INPUTS, tensor) };
            var outputs = new List<string> { MODEL_OUTPUTS };
            using var session = new InferenceSession(Path.Combine(@"onnx-models/", MODEL_FILE));
            using var predictions = session.Run(inputs, outputs);

            var labels = ReadLabels();

            var results = (predictions.First().Value as IEnumerable<float>)
                .Select((x, i) => new Classification(labels[i], x))
                .OrderByDescending(x => x.Confidence)
                .ToArray();

            return results;
        }
    }
}
