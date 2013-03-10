using System;

namespace Puppy.Monitoring.Events
{
    [Serializable]
    public class Categorisation
    {
        public Categorisation(string category) : this(category, "<unknown>")
        {
        }

        public Categorisation(string category, string subCategory) : this(category, subCategory, string.Empty)
        {
        }

        public Categorisation(string category, string subCategory, string segmentation)
        {
            Category = category;
            SubCategory = subCategory;
            Segmentation = segmentation;
        }

        public string Category { get; private set; }
        public string SubCategory { get; private set; }
        public string Segmentation { get; private set; }

        public override string ToString()
        {
            return string.Format("{0}/{1}/{2}", Category, SubCategory, Segmentation);
        }
    }
}