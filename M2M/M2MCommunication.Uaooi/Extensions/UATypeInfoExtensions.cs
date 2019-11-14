﻿using M2MCommunication.Core.Exceptions;
using System;
using UAOOI.Configuration.Networking.Serialization;

namespace M2MCommunication.Uaooi.Extensions
{
    public static class UATypeInfoExtensions
    {
        public static bool ContainsArray(this UATypeInfo typeInfo)
        {
            if (typeInfo is null)
            {
                return false;
            }
            else
            {
                return typeInfo.ValueRank >= 0;
            }
        }

        public static bool ContainsMultidimensionalArray(this UATypeInfo typeInfo)
        {
            if (typeInfo is null)
            {
                return false;
            }
            else
            {
                return typeInfo.ValueRank == 0 || typeInfo.ValueRank > 1;
            }
        }
    }
}
