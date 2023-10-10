using bikecompare.cms.media.google;
using OrchardCore.Modules.Manifest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[assembly: Module(
    Name = Constants.ModuleDisplayName,
    Author = "The Crossmarc Team",
    Website = "https://crossmarcsoftware.com",
    Version = "1.0.0"
)]

[assembly: Feature(
    Id = Constants.ModuleName,
    Name = Constants.ModuleDisplayName,
    Description = "Enables support for storing media files and serving them to clients directly from AWS S3.",
    Category = "Hosting",
    IsAlwaysEnabled = true
)]