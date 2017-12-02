using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MJC.CoreAPI.Template.WebAPI.Core.Dtos.Dummy;
using MJC.CoreAPI.Template.WebAPI.Core.Entities;
using MJC.CoreAPI.Template.WebAPI.Filters;
using MJC.CoreAPI.Template.WebAPI.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MJC.CoreAPI.Template.WebAPI.Controllers
{
    [Route("api/dummies")]
    [ValidateModel]
    [ApiVersion("1.0")]
    public class DummyController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private ILogger<DummyController> _logger;
        private IMapper _mapper;

        public DummyController(IUnitOfWork unitOfWork,
            ILogger<DummyController> logger,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        public ActionResult GetAll()
        {
            try
            {
                IEnumerable<Dummy> dummies = _unitOfWork.Dummies.GetDummies();

                if (dummies != null && dummies.Count() > 0)
                {
                    var results = _mapper.Map<IEnumerable<DummyDto>>(dummies);
                    return Ok(results);
                }
                else
                {
                    return NotFound("No dummies found.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex}");
            }

            return BadRequest();
        }

        [HttpGet("{dummyId}", Name = "DummyGet")]
        public ActionResult GetById(int dummyId)
        {
            try
            {
                var dummy = _unitOfWork.Dummies.GetDummy(dummyId);

                if (dummy != null)
                {
                    var result = _mapper.Map<DummyDto>(dummy);
                    return Ok(result);
                }
                else
                {
                    return NotFound("Dummy was not found.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex}");
            }

            return BadRequest();
        }

        [HttpPost]
        public IActionResult Post([FromBody]DummyDtoForCreation model)
        {
            try
            {
                _logger.LogInformation("Creating a new dummy.");
                var dummy = _mapper.Map<Dummy>(model);

                if (!_unitOfWork.Dummies.DummyExists(dummy.Name))
                {
                    _unitOfWork.Dummies.Add(dummy);

                    if (_unitOfWork.Complete())
                    {
                        var newUri = Url.Link("DummyGet", new { dummyId = dummy.Id });
                        return Created(newUri, _mapper.Map<DummyDto>(dummy));
                    }
                    else
                    {
                        _logger.LogWarning($"Could not save dummy to the database.");
                    }
                }
                else
                {
                    return BadRequest("Duplicate exists.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while saving the dummy: {ex}");
                return BadRequest(ex.Message);
            }

            return BadRequest("123");
        }


        [HttpPut("{dummyId}")]
        public IActionResult Put(int dummyId, [FromBody]DummyDtoForUpdate model)
        {
            try
            {
                var oldDummy = _unitOfWork.Dummies.GetDummy(dummyId);

                if (oldDummy == null)
                {
                    return NotFound($"Could not found dummy with id {dummyId}.");
                }

                _mapper.Map(model, oldDummy);

                if (_unitOfWork.Complete())
                {
                    return Ok(_mapper.Map<DummyDtoForUpdate>(oldDummy));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while updating the dummy: {ex}");
            }

            return BadRequest();
        }


        [HttpPatch("{dummyId}")]
        public IActionResult Patch(int dummyId, [FromBody]JsonPatchDocument<Dummy> patchedDummy)
        {
            try
            {
                var oldDummy = _unitOfWork.Dummies.GetDummy(dummyId);

                if (oldDummy == null)
                {
                    return NotFound($"Could not found dummy with id {dummyId}.");
                }

                if (patchedDummy == null)
                {
                    return BadRequest();
                }

                patchedDummy.ApplyTo(oldDummy);

                if (_unitOfWork.Complete())
                {
                    return Ok(_mapper.Map<DummyDtoForUpdate>(oldDummy));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while updating the dummy: {ex}");
            }

            return BadRequest();
        }

        [HttpDelete("{dummyId}")]
        public IActionResult Delete(int dummyId)
        {
            try
            {
                var dummy = _unitOfWork.Dummies.GetDummy(dummyId);

                if (dummy == null)
                {
                    return NotFound($"Could not found dummy with id {dummyId}.");
                }

                _unitOfWork.Dummies.Delete(dummy);

                if (_unitOfWork.Complete())
                {
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while updating the dummy: {ex}");
            }

            return BadRequest();
        }

        [HttpPost("{dummyId}")]
        public IActionResult BlockDummyCreation(int dummyId)
        {
            if (_unitOfWork.Dummies.DummyExists(dummyId))
            {
                return new StatusCodeResult(StatusCodes.Status409Conflict);
            }

            return NotFound();
        }
    }
}
