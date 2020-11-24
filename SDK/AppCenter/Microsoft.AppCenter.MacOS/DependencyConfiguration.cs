﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Microsoft.AppCenter.MacOS.Bindings;

namespace Microsoft.AppCenter
{
    static partial class DependencyConfiguration
    {
        private static IHttpNetworkAdapter _httpNetworkAdapter;
        private static IHttpNetworkAdapter PlatformHttpNetworkAdapter
        {
            get => _httpNetworkAdapter;
            set
            {
                MSACDependencyConfiguration.HttpClient = new MacOSHttpClientAdapter(value);
                _httpNetworkAdapter = value;
            }
        }
    }
}
