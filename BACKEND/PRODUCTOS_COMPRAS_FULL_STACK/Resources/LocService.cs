﻿using Microsoft.Extensions.Localization;
using System.Reflection;

namespace PRODUCTOS_COMPRAS_FULL_STACK.Resources
{
    public class LocService
    {
        private readonly IStringLocalizer _localizer;

        public LocService(IStringLocalizerFactory factory)
        {
            var type = typeof(SharedResource);
            var assemblyName = new AssemblyName(type
                            .GetTypeInfo().Assembly.FullName);
            _localizer = factory.Create("SharedResource",
                                                   assemblyName.Name);
        }

        public LocalizedString GetLocalizedHtmlString(string key)
        {
            return _localizer[key];
        }
    }
}
