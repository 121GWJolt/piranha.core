/*
 * Copyright (c) 2019 Håkan Edling
 *
 * This software may be modified and distributed under the terms
 * of the MIT license.  See the LICENSE file for details.
 *
 * https://github.com/piranhacms/piranha.core
 *
 */

using System.Collections.Generic;

namespace Piranha.Manager.Models.Content
{
    public class RegionEditModel
    {
        public IList<RegionItemEditModel> Items { get; set; } = new List<RegionItemEditModel>();
        public RegionMeta Meta { get; set; }
    }
}