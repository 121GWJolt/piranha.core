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
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Piranha.Manager.Models;
using Piranha.Manager.Services;

namespace Piranha.Manager.Controllers
{
    /// <summary>
    /// Api controller for alias management.
    /// </summary>
    [Area("Manager")]
    [Route("manager/api/media")]
    [ApiController]
    public class MediaApiController : Controller
    {
        private readonly MediaService _service;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public MediaApiController(MediaService service)
        {
            _service = service;
        }

        /// <summary>
        /// Gets the list model.
        /// </summary>
        /// <returns>The list model</returns>
        [Route("list/{folderId:Guid?}")]
        [HttpGet]
        public async Task<MediaListModel> List(Guid? folderId = null)
        {
            return await _service.GetList(folderId);
        }

        [Route("savefolder")]
        [HttpPost]
        public async Task<IActionResult> SaveFolder(MediaFolderModel model)
        {
            try
            {
                await _service.SaveFolder(model);

                var result = await _service.GetList();

                result.Status = new StatusMessage
                {
                    Type = StatusMessage.Success,
                    Body = $"The folder <code>{ model.Name }</code> was saved"
                };

                return Ok(result);
            }
            catch (ValidationException e)
            {
                var result = new AliasListModel();
                result.Status = new StatusMessage
                {
                    Type = StatusMessage.Error,
                    Body = e.Message
                };
                return BadRequest(result);
            }
        }
    }
}