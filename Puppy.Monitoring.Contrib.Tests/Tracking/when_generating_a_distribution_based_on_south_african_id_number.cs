using System;
using Puppy.Monitoring.Contrib.Tracking;
using Xunit.Extensions;

namespace Puppy.Monitoring.Contrib.Tests.Tracking
{
    public class when_generating_a_distribution_based_on_south_african_id_number : Specification
    {
        private readonly SouthAfricanIdNumberDistribution distributor;
        private readonly string id_number;
        private string file_path;

        public when_generating_a_distribution_based_on_south_african_id_number()
        {
            id_number = "8001135009083";
            distributor = new SouthAfricanIdNumberDistribution(id_number, AppDomain.CurrentDomain.BaseDirectory);
        }

        public override void Observe()
        {
            file_path = distributor.GetFileLocation("temp.xml").FullPath;
        }

        private string[] Distribution
        {
            get
            {
                return file_path.Replace(AppDomain.CurrentDomain.BaseDirectory, string.Empty)
                    .Split(@"\".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            }
        }

        [Observation]
        public void the_first_level_of_distribution_is_3()
        {
            Distribution[0].ShouldEqual("3");
        }

        [Observation]
        public void the_second_level_of_distribution_is_01()
        {
            Distribution[1].ShouldEqual("01");
        }

        [Observation]
        public void the_third_level_of_distribution_is_13()
        {
            Distribution[2].ShouldEqual("13");
        }

        [Observation]
        public void the_fourth_level_of_distribution_is_equal_to_the_full_id_number()
        {
            Distribution[3].ShouldEqual(id_number);
        }


    }
}