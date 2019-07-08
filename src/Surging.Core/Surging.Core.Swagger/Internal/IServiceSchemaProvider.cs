﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Surging.Core.Swagger.Internal
{
   public interface IServiceSchemaProvider
    {
        IEnumerable<string> GetSchemaFilesPath();
        IEnumerable<string> GetSchemaFilesPath(string annotationXmlDir, IEnumerable<string> dtoXmls = null);
    }
}
