/*
 * RData.cs
 *
 * Copyright (C) 2010-2015 by Microsoft Corporation
 *
 * This program is licensed to you under the terms of Version 2.0 of the
 * Apache License. This program is distributed WITHOUT
 * ANY EXPRESS OR IMPLIED WARRANTY, INCLUDING THOSE OF NON-INFRINGEMENT,
 * MERCHANTABILITY OR FITNESS FOR A PARTICULAR PURPOSE. Please refer to the
 * Apache License 2.0 (http://www.apache.org/licenses/LICENSE-2.0) for more details.
 *
 */

using System;

namespace DeployR
{
/// <summary>
/// RData Interface
/// </summary>
/// <remarks></remarks>
    public interface RData
    {
        /// <summary>
        /// Name of R object that this interface represents
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        String Name { get; }
        /// <summary>
        /// R Class of the R object that this interface represents
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        String RClass { get; }
        /// <summary>
        /// DeployR Type of the R object that this interface represents
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        String Type { get; }
        /// <summary>
        /// Value of the R object that this interface represents
        /// </summary>
        /// <value>Raw value case to type Object</value>
        /// <returns></returns>
        /// <remarks></remarks>
        object Value { get; }

    }
}