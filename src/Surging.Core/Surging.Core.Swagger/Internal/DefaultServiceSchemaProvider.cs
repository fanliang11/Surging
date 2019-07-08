﻿using Surging.Core.CPlatform.Engines;
using Surging.Core.CPlatform.Runtime.Server;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Surging.Core.Swagger.Internal
{
   public class DefaultServiceSchemaProvider : IServiceSchemaProvider
    {
        private readonly IServiceEntryProvider _serviceEntryProvider;

        public DefaultServiceSchemaProvider( IServiceEntryProvider serviceEntryProvider)
        {
            _serviceEntryProvider = serviceEntryProvider;
        }

        public IEnumerable<string> GetSchemaFilesPath()
        {
            var result = new List<string>();
            var assemblieFiles = _serviceEntryProvider.GetALLEntries()
                        .Select(p => p.Type.Assembly.Location).Distinct();

            foreach (var assemblieFile in assemblieFiles)
            {
                var fileSpan = assemblieFile.AsSpan();
                var path = $"{fileSpan.Slice(0, fileSpan.LastIndexOf(".")).ToString()}.xml";
                if (File.Exists(path))
                    result.Add(path);
            }
            return result;
        }

        public IEnumerable<string> GetSchemaFilesPath(string annotationXmlDir, IEnumerable<string> dtoXmls = null)
        {
            var result = new List<string>();
            var assemblieFiles = _serviceEntryProvider.GetALLEntries()
                        .Select(p => p.Type.Assembly.Location).Distinct();

            foreach (var assemblieFile in assemblieFiles)
            {
                var fileSpan = assemblieFile.AsSpan();
                var path = $"{fileSpan.Slice(0, fileSpan.LastIndexOf(".")).ToString()}.xml";
                if (!string.IsNullOrEmpty(annotationXmlDir))
                {
                    path = Path.Combine(annotationXmlDir, assemblieFile.Split("/").Last().Replace("dll", "xml"));
                }

                if (File.Exists(path))
                    result.Add(path);
            }
            if (dtoXmls != null && dtoXmls.Any())
            {
                foreach (var dtoxml in dtoXmls)
                {
                    var path = dtoxml;
                    if (!string.IsNullOrEmpty(annotationXmlDir))
                    {
                        path = Path.Combine(annotationXmlDir, dtoxml);
                    }
                    if (File.Exists(path))
                        result.Add(path);
                }
            }
            return result;
        }
    }
}
