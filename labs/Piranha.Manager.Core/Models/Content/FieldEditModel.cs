/*
 * Copyright (c) 2019 Håkan Edling
 *
 * This software may be modified and distributed under the terms
 * of the MIT license.  See the LICENSE file for details.
 *
 * https://github.com/piranhacms/piranha.core
 *
 */

using System;
using Piranha.Extend;

namespace Piranha.Manager.Models.Content
{
    public class FieldEditModel
    {
        public string Type { get; set; }
        public IField Model { get; set; }
        public FieldMeta Meta { get; set; }
    }
}