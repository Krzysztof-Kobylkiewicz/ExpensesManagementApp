namespace Core.Helpers
{
    public static class MathHelper
    {
        public static double CalculateDominant(IEnumerable<double> data)
        {
            var dominantDict = new Dictionary<double, int>();

            foreach (var value in data)
            {
                if (dominantDict.ContainsKey(value))
                    dominantDict[value]++;
                else
                    dominantDict[value] = 1;
            }

            int maxOccurences = dominantDict.Values.Count != 0 ? dominantDict.Values.Max() : 0;
            double dominant = dominantDict.FirstOrDefault(d => d.Value == maxOccurences).Key;

            return dominant;
        }

        public static double CalculateMedian(double[] data)
        {
            if (data is null || data.Length == 0)
                return 0;

            int n = data.Length;

            return n % 2 == 0 ? (data[n / 2 - 1] + data[(n / 2)]) / 2 : data[(int)(n / 2 + 0.5)];
        }
    }
}
