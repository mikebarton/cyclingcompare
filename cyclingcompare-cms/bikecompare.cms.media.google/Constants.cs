using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bikecompare.cms.media.google
{
    public class Constants
    {
        /// <summary>
        /// The display name for the AWS S3 Media Storage module.
        /// </summary>
        public const string ModuleDisplayName = "Google Cloud Media Storage";

        /// <summary>
        /// The name of the AWS S3 Media Storage module.
        /// </summary>
        public const string ModuleName = "BikeCompare.Cms.Media.Google";

        /// <summary>
        /// The name of the parameter that contains the Parameter Store prefix for data protection keys.
        /// </summary>
        public const string PrefixSetting = "Prefix";
    }
}
